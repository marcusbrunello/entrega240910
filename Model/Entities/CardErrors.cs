
using MetaBank.Model.Base;

namespace MetaBank.Model.Entities;

public static class CardErrors
{
	public static Error NotFound = new(
		"Card.NotFound",
		"The Card with the specified identifier was not found");

	public static Error InvalidCredentials = new(
		"Card.InvalidCredentials",
		"The provided credentials were invalid");
}