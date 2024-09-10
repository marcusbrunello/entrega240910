using FluentValidation;

namespace MetaBank.BusinessLogic.Features.Cards.Commands.RegisterCard;

internal sealed class WithdrawalsPaginatedQueryValidator : AbstractValidator<RegisterCardCommand>
{
    public WithdrawalsPaginatedQueryValidator()
    {
        RuleFor(c => c.Number).CreditCard();

        RuleFor(c => c.Holder).MaximumLength(25);

        RuleFor(c => c.PinToken).NotEmpty().Matches("^\\d{ 4}$");
    }
}