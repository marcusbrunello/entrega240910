using MetaBank.BusinessLogic.Abstractions.Authentication;
using MetaBank.BusinessLogic.Contracts.Interfaces.Persistence;
using MetaBank.Persistence;
using Microsoft.EntityFrameworkCore;
using Model.Entities;
using System;
using MetaBank.Persistence.Authentication;
using MetaBank.Model.Base;
using Microsoft.Extensions.Options;

namespace MetaBank.Persistence.Repositories;

public class CardRepository : Repository<Card>, ICardRepository
{
	protected new readonly ApplicationDbContext _applicationDbContext;

	public CardRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
	{
		_applicationDbContext = applicationDbContext;
	}


	public async Task<Card?> FindCardByNumber(string number, CancellationToken cancellationToken = default)
	{
		return await _applicationDbContext.Cards.FirstOrDefaultAsync(card => card.Number == number);
	}

	public async Task LoginSuccesful(int id, CancellationToken cancellationToken = default)
	{
		Card? c = await _applicationDbContext.Cards.FirstOrDefaultAsync(card => card.Id == id);
		if (c == null)
		{
			throw new Exception("Card not found");
		}
		c.TriesLeftToBlock = (int)EnumTriesToBlock.MAXATTEMPTSLEFT;
		await _applicationDbContext.SaveChangesAsync();

		return;
	}

	public async Task LoginFailed(int id, CancellationToken cancellationToken = default)
	{
		Card? c = await _applicationDbContext.Cards.FirstOrDefaultAsync(card => card.Id == id);
		if (c == null)
		{
			throw new Exception("Card not found");
		}
		c.TriesLeftToBlock -= 1;
		await _applicationDbContext.SaveChangesAsync();

		return;
	}
}
