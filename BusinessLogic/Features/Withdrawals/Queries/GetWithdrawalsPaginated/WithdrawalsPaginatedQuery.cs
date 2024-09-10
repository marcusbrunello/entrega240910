using MetaBank.BusinessLogic.Abstractions.Messaging;


namespace MetaBank.BusinessLogic.Features.Withdrawals.Queries.GetWithdrawalsPaginated;

public record WithdrawalsPaginatedQuery(string CardNumber, int PageNumber = 1, int PageSize = 10) : IQuery<IEnumerable<WithdrawalsResponse>>;
