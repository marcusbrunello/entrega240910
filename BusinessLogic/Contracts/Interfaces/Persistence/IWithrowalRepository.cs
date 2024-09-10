
using MetaBank.Model.Entities;
using Model.Entities;

namespace MetaBank.BusinessLogic.Contracts.Interfaces.Persistence;

public interface IWithdrawalRepository
{
	void Add(Withdrawal Withdrawal);

	Task<IEnumerable<Withdrawal>> GetWithdrawalsByCardNumber(
		string cardNumber, int pageNumber = 1, int pageSize = 10, CancellationToken cancellationToken = default);

}