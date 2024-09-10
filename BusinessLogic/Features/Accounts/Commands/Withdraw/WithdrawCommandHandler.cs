using MetaBank.BusinessLogic.Abstractions.Clock;
using MetaBank.BusinessLogic.Abstractions.Messaging;
using MetaBank.BusinessLogic.Contracts.Interfaces.Persistence;
using MetaBank.Model.Base;
using MetaBank.Model.Entities;
using System.Globalization;


namespace MetaBank.BusinessLogic.Features.Accounts.Commands.Withdraw;

internal sealed class WithdrawCommandHandler(
	IDateTimeProvider dateTimeProvider,
	ICardRepository cardRepository,
	IAccountRepository accountRepository,
	IWithdrawalRepository withdrawalRepository,
	IUnitOfWork unitOfWork) : ICommandHandler<WithdrawCommand, WithdrawResponse>
{
	private readonly IDateTimeProvider _dateTimeProvider = dateTimeProvider;
	private readonly ICardRepository _cardRepository = cardRepository;
	private readonly IAccountRepository _accountRepository = accountRepository;
	private readonly IWithdrawalRepository _withdrawalRepository = withdrawalRepository;
	private readonly IUnitOfWork _unitOfWork = unitOfWork;

	public async Task<Result<WithdrawResponse>> Handle(
		WithdrawCommand request,
		CancellationToken cancellationToken)
	{
		var card = await _cardRepository.FindCardByNumber(request.CardNumber, cancellationToken);

		var account = await _accountRepository.FindAccountByCardNumber(request.CardNumber, cancellationToken);

		if (account is null)
		{
			return Result.Failure<WithdrawResponse>(AccountErrors.NotFound);
		}

		if (account.CashAvailable < request.AmountInCents)
		{
			return Result.Failure<WithdrawResponse>(AccountErrors.NotEnoughFunds);
		}

		var response = await GenerateTransaction(account, request.AmountInCents, cancellationToken);

		await _unitOfWork.SaveChangesAsync();

		return Result.Success(response);
	}

	private async Task<WithdrawResponse> GenerateTransaction(Account account, long amountInCents, CancellationToken cancellationToken)
	{
		var withdrawal = new Withdrawal
		{
			Account = account,
			Amount = (uint)amountInCents,
			CreatedAt = _dateTimeProvider.UtcNow,
			UpdatedAt = _dateTimeProvider.UtcNow
		};

		string intBunch = amountInCents.ToString().Substring(0, amountInCents.ToString().Length - 2);
		string decimalSeparator = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;
		string decimalBunch = amountInCents.ToString().Substring(amountInCents.ToString().Length - 2);

		string amountDetails = intBunch + decimalSeparator + decimalBunch;

		_withdrawalRepository.Add(withdrawal);

		account.CashAvailable -= amountInCents;

		_accountRepository.Update(account);

		await _unitOfWork.SaveChangesAsync();

		return new WithdrawResponse
		{
			AccountNumber = account!.Number,
			AmountDetails = amountDetails,
			TransactionDateTime = _dateTimeProvider.UtcNow
		};
	}

}