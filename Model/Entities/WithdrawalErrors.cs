using MetaBank.Model.Base;

namespace MetaBank.Model.Entities;

public static class WithdrawalErrors
{
	public static Error UnknownError = new(
		"UnknownError",
		"UnknownError");


}