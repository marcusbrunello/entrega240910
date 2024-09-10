using Azure;
using MetaBank.BusinessLogic.Contracts.Interfaces.Persistence;
using MetaBank.Model.Entities;
using MetaBank.Persistence;
using Microsoft.EntityFrameworkCore;
using Model.Entities;

namespace MetaBank.Persistence.Repositories
{
	public class WithdrawalRepository : Repository<Withdrawal>, IWithdrawalRepository
	{
		protected new readonly ApplicationDbContext _applicationDbContext;

		public WithdrawalRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
		{
			_applicationDbContext = applicationDbContext;
		}

		public async Task<IEnumerable<Withdrawal>> GetWithdrawalsByCardNumber(string cardNumber, int pageNumber = 1, int pageSize = 10, CancellationToken cancellationToken = default)
		{
			return await _applicationDbContext.Withdrawals
				.Include(w => w.Account)
				.Include(w => w.Account.Card)
				.Where(w => w.Account.Card.Number == cardNumber)
				.AsNoTracking()
				.OrderByDescending(w => w.CreatedAt)
				.Skip((pageNumber - 1) * pageSize)
				.Take(pageSize)
				.ToListAsync();
		}
	}
}
