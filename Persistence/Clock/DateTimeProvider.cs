using MetaBank.BusinessLogic.Abstractions.Clock;

namespace MetaBank.Persistence.Clock;

internal sealed class DateTimeProvider : IDateTimeProvider
{
	public DateTime UtcNow => DateTime.UtcNow;
}