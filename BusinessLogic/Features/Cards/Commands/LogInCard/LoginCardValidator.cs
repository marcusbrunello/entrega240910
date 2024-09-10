using FluentValidation;

namespace MetaBank.BusinessLogic.Features.Cards.Commands.LogInCard;

public class LoginCardValidator : AbstractValidator<LogInCardCommand>
{
    public LoginCardValidator()
    {
        RuleFor(c => c.CardNumber)
            .NotNull()
            .NotEmpty()
            .CreditCard();

        RuleFor(c => c.Pin)
            .NotNull()
            .NotEmpty()
            .Length(4, 4);
    }
}
