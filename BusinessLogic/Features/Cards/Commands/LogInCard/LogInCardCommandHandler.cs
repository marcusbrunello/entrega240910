using MetaBank.BusinessLogic.Abstractions.Authentication;
using MetaBank.BusinessLogic.Abstractions.Messaging;
using MetaBank.BusinessLogic.Contracts.Interfaces.Persistence;
using MetaBank.Model.Base;
using MetaBank.Model.Entities;

namespace MetaBank.BusinessLogic.Features.Cards.Commands.LogInCard;

internal sealed class WithdrawalsPaginatedQueryHandler : ICommandHandler<LogInCardCommand, AccessTokenResponse>
{
    private readonly ICardRepository _cardRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IJwtService _jwtService;

    public WithdrawalsPaginatedQueryHandler(ICardRepository cardRepository, IUnitOfWork unitOfWork, IJwtService jwtService)
    {
        _cardRepository = cardRepository;
        _unitOfWork = unitOfWork;
        _jwtService = jwtService;
    }

    public async Task<Result<AccessTokenResponse>> Handle(
                    LogInCardCommand request,
                    CancellationToken cancellationToken)
    {
        try
        {
            var card = await _cardRepository.FindCardByNumber(request.CardNumber, cancellationToken);

            if (card?.TriesLeftToBlock > 0 &&
				BCrypt.Net.BCrypt.Verify(request.Pin, card?.PinToken))
            {
                await _cardRepository.LoginSuccesful(card!.Id);
                await _unitOfWork.SaveChangesAsync();

                var token = _jwtService.GenerateJWTToken(card!);

                return Result.Success(new AccessTokenResponse(token));
            }

            await _cardRepository.LoginFailed(card!.Id);
            await _unitOfWork.SaveChangesAsync();

            throw new Exception("Invalid Credentials");

        }
        catch
        {
            return Result.Failure<AccessTokenResponse>(CardErrors.InvalidCredentials);
        }
    }
}
