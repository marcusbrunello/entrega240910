using MetaBank.BusinessLogic.Contracts.Interfaces.Persistence;
using MetaBank.Model.Entities;
using Microsoft.EntityFrameworkCore;

namespace MetaBank.Persistence.Repositories;

internal class AccountRepository : Repository<Account>, IAccountRepository
{
	new readonly ApplicationDbContext _applicationDbContext;

	public AccountRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
	{
		_applicationDbContext = applicationDbContext;
	}

	public async Task<Account?> FindAccountByCardNumber(string number, CancellationToken cancellationToken = default)
	{
		var account = await _applicationDbContext.Accounts
								 .Include(a => a.Card)
								 .FirstOrDefaultAsync(a => a.Card.Number == number);

		return account;
	}
}