using MetaBank.BusinessLogic.Abstractions.Messaging;

namespace MetaBank.BusinessLogic.Features.Accounts.Queries.GetAccountDetails;

public sealed record GetAccountDetailsQuery(
    string CardNumber) : IQuery<AccountResponse>;