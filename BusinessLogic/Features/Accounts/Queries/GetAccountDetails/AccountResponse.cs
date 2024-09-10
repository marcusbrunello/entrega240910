namespace MetaBank.BusinessLogic.Features.Accounts.Queries.GetAccountDetails;

public sealed class AccountResponse
{
    public string Number { get; init; }
    public string Holder { get; init; }
    public string CashAvailable { get; init; }
    public DateTimeOffset LastWithdrawal { get; init; }

}