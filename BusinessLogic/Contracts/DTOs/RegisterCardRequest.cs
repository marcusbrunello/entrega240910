namespace MetaBank.BusinessLogic.Contracts.DTOs;

public sealed record RegisterCardRequest(
	string Number,
	string PinToken,
	string Holder,
	string TriesLeftToBlock);