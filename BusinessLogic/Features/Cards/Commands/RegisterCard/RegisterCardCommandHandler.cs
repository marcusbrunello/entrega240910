using MetaBank.BusinessLogic.Abstractions.Authentication;
using MetaBank.BusinessLogic.Abstractions.Messaging;
using MetaBank.BusinessLogic.Contracts.Interfaces.Persistence;
using MetaBank.Model.Base;
using Model.Entities;
using System.Reflection;

namespace MetaBank.BusinessLogic.Features.Cards.Commands.RegisterCard;

internal sealed class RegisterCardCommandHandler : ICommandHandler<RegisterCardCommand, int>
{
    private readonly ICardRepository _cardRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IJwtService _jwtService; // Add the IJwtService dependency

    public RegisterCardCommandHandler(
        ICardRepository cardRepository,
        IUnitOfWork unitOfWork,
        IJwtService jwtService) // Add the IJwtService parameter
    {
        _cardRepository = cardRepository;
        _unitOfWork = unitOfWork;
        _jwtService = jwtService; // Assign the jwtService parameter to the _jwtService field
    }

    public async Task<Result<int>> Handle(
        RegisterCardCommand request,
        CancellationToken cancellationToken)
    {
        var existingCard = await _cardRepository.FindCardByNumber(request.Number);
        if (existingCard != null)
            throw new Exception("Invalid Operation.");

        var card = new Card
        {
            Number = request.Number,
            PinToken = BCrypt.Net.BCrypt.HashPassword(request.PinToken),
            Holder = request.Holder,
            UpdatedAt = DateTime.UtcNow, // Set UpdatedAt to the current UTC time
            CreatedAt = DateTime.UtcNow
        };

        _cardRepository.Add(card);

        await _unitOfWork.SaveChangesAsync();

        return card.Id;
    }
}
