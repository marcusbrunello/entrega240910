using FluentValidation;

namespace MetaBank.BusinessLogic.Features.Withdrawals.Queries.GetWithdrawalsPaginated;

internal sealed class WithdrawalsPaginatedQueryValidator : AbstractValidator<WithdrawalsPaginatedQuery>
{
	public WithdrawalsPaginatedQueryValidator()
	{
		RuleFor(c => c.CardNumber).CreditCard();

		RuleFor(c => c.PageNumber).GreaterThan(0);
	}
}