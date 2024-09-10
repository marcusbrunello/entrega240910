using MetaBank.Model.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Model.Entities;

namespace MetaBank.Persistence
{

	// This class is used to configure the entities in the database
	// SHOULD NOT BE USED IN THE PROJECT
	internal class CardTypeConfiguration : IEntityTypeConfiguration<Card>
	{
		public void Configure(EntityTypeBuilder<Card> builder)
		{
			builder.Property(c => c.Number).HasMaxLength(16);
			builder.Property(c => c.PinToken).HasMaxLength(256);
			builder.Property(c => c.Holder).HasMaxLength(25);
			builder.Property(c => c.TriesLeftToBlock).HasDefaultValue(4);

			builder.Property(w => w.CreatedAt).ValueGeneratedOnAdd();
			builder.Property(w => w.UpdatedAt).ValueGeneratedOnAdd();




		}
	}

	internal class AccountTypeConfiguration : IEntityTypeConfiguration<Account>
	{
		public void Configure(EntityTypeBuilder<Account> builder)
		{
			builder.HasKey(c => c.Id);
			builder.Property(c => c.Number).HasMaxLength(10);
			builder.Property(c => c.CashAvailable).HasDefaultValue(0);

			builder.Property(w => w.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
			builder.HasIndex(w => w.CreatedAt).IsDescending();
			builder.Property(w => w.UpdatedAt).HasDefaultValueSql("GETUTCDATE()");
			builder.HasIndex(w => w.UpdatedAt).IsDescending();

		}
	}

	internal class WithdrawalTypeConfiguration : IEntityTypeConfiguration<Withdrawal>
	{
		public void Configure(EntityTypeBuilder<Withdrawal> builder)
		{
			builder.Property(w => w.CreatedAt).ValueGeneratedOnAdd();
			builder.HasIndex(w => w.CreatedAt).IsDescending();
			builder.Property(w => w.UpdatedAt).ValueGeneratedOnAdd();
			builder.HasIndex(w => w.UpdatedAt).IsDescending();

			builder.HasOne(w => w.Account).WithMany(a => a.Withdrawals).OnDelete(DeleteBehavior.ClientCascade);

		}
	}
}