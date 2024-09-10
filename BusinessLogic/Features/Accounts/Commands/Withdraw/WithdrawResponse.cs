
namespace MetaBank.BusinessLogic.Features.Accounts.Commands.Withdraw;

public class WithdrawResponse
{
    public string AccountNumber { get; set; }
    public string AmountDetails { get; set; }
    public DateTime TransactionDateTime { get; set; }
}
