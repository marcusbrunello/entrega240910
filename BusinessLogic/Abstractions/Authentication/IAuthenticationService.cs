
using Model.Entities;

namespace MetaBank.BusinessLogic.Abstractions.Authentication;

public interface IAuthenticationService
{
	Task<string> RegisterAsync(
		Card card,
		string password,
		CancellationToken cancellationToken = default);
}