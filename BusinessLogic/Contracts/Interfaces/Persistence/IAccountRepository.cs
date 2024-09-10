using MetaBank.Model.Entities;
using Model.Entities;

namespace MetaBank.BusinessLogic.Contracts.Interfaces.Persistence;

public interface IAccountRepository
{
	void Add(Account Account);

	void Update(Account Account);

	Task<Account?> GetByIdAsync(int id, CancellationToken cancellationToken = default);

	Task<Account?> FindAccountByCardNumber(string number, CancellationToken cancellationToken = default);

}