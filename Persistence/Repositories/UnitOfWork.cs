using MetaBank.BusinessLogic.Contracts.Interfaces.Persistence;
using MetaBank.Model.Base;
using MetaBank.Persistence;

namespace MetaBank.Persistence.Repositories
{
	public class UnitOfWork(ICardRepository cards, IWithdrawalRepository withdrawals, IAccountRepository account, ApplicationDbContext applicationDbContext) : IUnitOfWork
	{
		private readonly ApplicationDbContext _applicationDbContext = applicationDbContext;

		public ICardRepository Cards { get; } = cards;
		public IWithdrawalRepository Withdrawals { get; } = withdrawals;
		public IAccountRepository Accounts { get; } = account;




		public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
		{
			return await _applicationDbContext.SaveChangesAsync(cancellationToken);
		}
		
		public void Dispose()
		{
			GC.SuppressFinalize(this);
		}
	}
}
