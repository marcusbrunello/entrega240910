using MetaBank.Model.Base;

namespace Model.Entities
{
    public class Card : Entity
	{
		public required string Number { get; init; }
		public required string PinToken { get; set; }
		public string? Holder { get; set; }
		public int TriesLeftToBlock { get; set; } = (int)EnumTriesToBlock.MAXATTEMPTSLEFT;

		//public Card() { }

	}
}