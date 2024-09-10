using MetaBank.BusinessLogic.Abstractions.Messaging;

namespace MetaBank.BusinessLogic.Features.Accounts.Commands.Withdraw;

public sealed record WithdrawCommand(string CardNumber, long AmountInCents) : ICommand<WithdrawResponse>;