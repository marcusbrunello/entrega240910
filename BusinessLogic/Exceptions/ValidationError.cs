namespace MetaBank.BusinessLogic.Exceptions;

public sealed record ValidationError(string PropertyName, string ErrorMessage);