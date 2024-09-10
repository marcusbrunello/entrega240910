using MediatR;
using MetaBank.BusinessLogic.Abstractions.Authentication;
using MetaBank.BusinessLogic.Abstractions.Messaging;
using MetaBank.BusinessLogic.Contracts.Interfaces.Persistence;
using MetaBank.BusinessLogic.Features.Cards.Commands.LogInCard;
using MetaBank.Model.Base;
using MetaBank.Model.Entities;
using System.Globalization;
using System.Net.NetworkInformation;

namespace MetaBank.BusinessLogic.Features.Withdrawals.Queries.GetWithdrawalsPaginated;

internal sealed class WithdrawalsPaginatedQueryHandler : IQueryHandler<WithdrawalsPaginatedQuery, IEnumerable<WithdrawalsResponse>>
{
	private readonly IWithdrawalRepository _withdrawalRepository;

	public WithdrawalsPaginatedQueryHandler(IWithdrawalRepository withdrawalRepository)
	{
		_withdrawalRepository = withdrawalRepository;
	}

	public async Task<Result<IEnumerable<WithdrawalsResponse>>> Handle(
					WithdrawalsPaginatedQuery request,
					CancellationToken cancellationToken)
	{
		try
		{
			var withdrawals = await _withdrawalRepository.GetWithdrawalsByCardNumber(request.CardNumber, request.PageNumber, 10, cancellationToken);

			if (withdrawals == null)
			{
				return Result.Failure<IEnumerable<WithdrawalsResponse>>(WithdrawalErrors.UnknownError);
			}

			var withdrawalsResponse = withdrawals.Select(o => new WithdrawalsResponse
			{
				AccountNumber = o.Account.Number,
				AmountDetails = o.Amount.ToString().Substring(0, o.Amount.ToString().Length - 2)+ CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator+ o.Amount.ToString().Substring(o.Amount.ToString().Length - 2),
				CreatedAt = o.CreatedAt,
			});

			return Result.Success<IEnumerable<WithdrawalsResponse>>(withdrawalsResponse);


		}
		catch
		{
			return Result.Failure<IEnumerable<WithdrawalsResponse>>(CardErrors.InvalidCredentials);
		}
	}

}
