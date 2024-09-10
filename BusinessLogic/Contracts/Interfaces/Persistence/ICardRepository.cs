using MetaBank.BusinessLogic.Abstractions.Authentication;
using Model.Entities;

namespace MetaBank.BusinessLogic.Contracts.Interfaces.Persistence;

public interface ICardRepository
{

	void Add(Card Card);

	Task<Card?> FindCardByNumber(string number, CancellationToken cancellationToken = default);

	Task LoginSuccesful(int id, CancellationToken cancellationToken = default);
	Task LoginFailed(int id, CancellationToken cancellationToken = default);

}
