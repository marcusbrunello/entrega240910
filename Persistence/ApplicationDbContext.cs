using Model.Entities;
using Microsoft.EntityFrameworkCore;
using MetaBank.Model.Entities;
using MetaBank.Model.Base;
using System.Data;
using MetaBank.BusinessLogic.Exceptions;
using Azure.Core;

namespace MetaBank.Persistence
{
	public class ApplicationDbContext : DbContext, IUnitOfWork
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
		{
		}

		public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
		{
			try
			{
				var result = await base.SaveChangesAsync(cancellationToken);

				return result;
			}
			catch (DbUpdateConcurrencyException ex)
			{
				throw new ConcurrencyException("Concurrency exception occurred.", ex);
			}
		}


		public DbSet<Card> Cards { get; set; }
		public DbSet<Account> Accounts { get; set; }
		public DbSet<Withdrawal> Withdrawals { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			new AccountTypeConfiguration().Configure(modelBuilder.Entity<Account>());
			new CardTypeConfiguration().Configure(modelBuilder.Entity<Card>());
			new WithdrawalTypeConfiguration().Configure(modelBuilder.Entity<Withdrawal>());

			var cardId1 = -1;
			var cardId2 = -2;
			var cardId3 = -3;

			var hash = BCrypt.Net.BCrypt.HashPassword("1234");

			Card card1 = new()
			{
				Id = cardId1,
				Number = "1234123412341234",
				PinToken = hash,
				Holder = "Sam Smith",
				CreatedAt = DateTime.UtcNow,
				UpdatedAt = DateTime.UtcNow
			};
			Card card2 = new()
			{
				Id = cardId2,
				Number = "4321432143214321",
				PinToken = hash,
				Holder = "Joe Doe",
				CreatedAt = DateTime.UtcNow,
				UpdatedAt = DateTime.UtcNow
			};
			Card card3 = new()
			{
				Id = cardId3,
				Number = "1111222233334444",
				PinToken = hash,
				Holder = "Greta Garbo",
				CreatedAt = DateTime.UtcNow,
				UpdatedAt = DateTime.UtcNow
			};

			modelBuilder.Entity<Card>().HasData(
				card1, card2, card3
			);

			var accountId1 = -1;
			var accountId2 = -2;
			var accountId3 = -3;

			Account account1 = new()
			{
				Id = accountId1,
				Number = "1234567890",
				CashAvailable = 10000000,
				CardId = cardId1,
				CreatedAt = DateTime.UtcNow,
				UpdatedAt = DateTime.UtcNow
			};
			Account account2 = new()
			{
				Id = accountId2,
				Number = "0987654321",
				CardId = cardId2,
				CashAvailable = 555555555,
				CreatedAt = DateTime.UtcNow,
				UpdatedAt = DateTime.UtcNow
			};
			Account account3 = new()
			{
				Id = accountId3,
				Number = "9876543210",
				CardId = cardId3,
				CashAvailable = 1111111111,
				CreatedAt = DateTime.UtcNow,
				UpdatedAt = DateTime.UtcNow
			};

			modelBuilder.Entity<Account>().HasData(
				account1, account2, account3
			);

			base.OnModelCreating(modelBuilder);
		}

	}
}
