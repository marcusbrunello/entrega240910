using MetaBank.Model.Base;
using Model.Entities;

namespace MetaBank.Model.Entities;

public class Account : Entity
{
	public string? Number { get; set; }

	public long CashAvailable { get; set; }

	public Card Card { get; set; }

	public int CardId { get; set; }

	public ICollection<Withdrawal> Withdrawals { get; } = new List<Withdrawal>();
}
