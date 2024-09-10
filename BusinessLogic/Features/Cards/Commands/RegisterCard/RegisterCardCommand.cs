using MetaBank.BusinessLogic.Abstractions.Messaging;

namespace MetaBank.BusinessLogic.Features.Cards.Commands.RegisterCard;

public sealed record RegisterCardCommand(
        string Number,
        string PinToken,
        string Holder) : ICommand<int>;