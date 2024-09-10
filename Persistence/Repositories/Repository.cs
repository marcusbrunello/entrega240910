
using MetaBank.Model.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace MetaBank.Persistence.Repositories;

public abstract class Repository<T>
	where T : Entity
{
	protected readonly ApplicationDbContext _applicationDbContext;

	protected Repository(ApplicationDbContext applicationDbContext)
	{
		_applicationDbContext = applicationDbContext;
	}

	public async Task<T?> GetByIdAsync(
		int id,
		CancellationToken cancellationToken = default)
	{
		return await _applicationDbContext
			.Set<T>()
			.FirstOrDefaultAsync(Card => Card.Id == id, cancellationToken);
	}

	public void Add(T entity)
	{
		_applicationDbContext.Add(entity);
	}

	public void Update(T entity)
	{
		_applicationDbContext.Update(entity);
	}

}