namespace MetaBank.BusinessLogic.Abstractions.Clock;

public interface IDateTimeProvider
{
    DateTime UtcNow { get; }
}