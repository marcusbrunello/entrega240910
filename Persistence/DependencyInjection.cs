using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using MetaBank.BusinessLogic.Contracts.Interfaces.Persistence;
using MetaBank.Persistence.Repositories;
using MetaBank.Model.Base;
using MetaBank.BusinessLogic.Abstractions.Data;
using MetaBank.Persistence.Data;
using MetaBank.BusinessLogic.Abstractions.Authentication;
using MetaBank.Persistence.Authentication;
using MetaBank.BusinessLogic.Abstractions.Clock;
using MetaBank.Persistence.Clock;

namespace MetaBank.Persistence
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddInfrastructure(
			this IServiceCollection services,
			IConfiguration configuration)
		{
			services.AddTransient<IDateTimeProvider, DateTimeProvider>();

			AddPersistenceServices(services, configuration);

			return services;
		}

		private static void AddPersistenceServices(IServiceCollection services, IConfiguration configuration)
		{
			var connectionString = configuration.GetConnectionString("DefaultConnection");

			services.AddTransient<IDateTimeProvider, DateTimeProvider>();

			services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString,
														builder => builder.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

			services.AddSingleton<IJwtService>(_ =>
				new JwtTokenService(configuration));

			services.AddScoped<ICardRepository, CardRepository>();
			services.AddScoped<IWithdrawalRepository, WithdrawalRepository>();
			services.AddScoped<IAccountRepository, AccountRepository>();
			services.AddScoped<IUnitOfWork, UnitOfWork>();

			services.AddSingleton<ISqlConnectionFactory>(_ =>
				new SqlConnectionFactory(connectionString));

		}
	}


}
