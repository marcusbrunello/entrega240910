using MetaBank.Model.Base;

namespace MetaBank.Model.Entities;

public static class AccountErrors
{
	public static Error NotFound = new(
		"Account.NotFound",
		"The Account with the specified identifier was not found");

	public static Error NotEnoughFunds = new(
	"Account.NotEnoughFunds",
	"The Account has not enough funds.");
}