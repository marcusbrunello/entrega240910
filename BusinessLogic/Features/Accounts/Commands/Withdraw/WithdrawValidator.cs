using FluentValidation;

namespace MetaBank.BusinessLogic.Features.Accounts.Commands.Withdraw;

public class WithdrawValidator : AbstractValidator<WithdrawCommand>
{
    public WithdrawValidator()
    {
        RuleFor(a => a.CardNumber).CreditCard().WithMessage("Invalid Card Number.");
        RuleFor(a => a.AmountInCents).GreaterThan(0).WithMessage("Invalid amount Input.");
    }
}
