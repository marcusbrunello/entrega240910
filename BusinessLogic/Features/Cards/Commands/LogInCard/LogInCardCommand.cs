using MetaBank.BusinessLogic.Abstractions.Messaging;

namespace MetaBank.BusinessLogic.Features.Cards.Commands.LogInCard;

public sealed record LogInCardCommand(string CardNumber, string Pin)
    : ICommand<AccessTokenResponse>;