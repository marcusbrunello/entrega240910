using MetaBank.Model.Base;

namespace MetaBank.Model.Entities
{
	public class Withdrawal : Entity
	{
		public uint Amount { get; set; } = 0;

		public Account Account { get; init; } = null!;

	}
}
