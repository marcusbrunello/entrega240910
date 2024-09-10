namespace MetaBank.BusinessLogic.Features.Withdrawals.Queries.GetWithdrawalsPaginated;

public class WithdrawalsResponse
{
	public string AccountNumber { get; set; }
	public string AmountDetails { get; set; }
	public DateTimeOffset CreatedAt { get; set; }
}
