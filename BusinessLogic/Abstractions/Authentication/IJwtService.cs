
using MetaBank.Model.Base;
using Model.Entities;
using System.Security.Claims;

namespace MetaBank.BusinessLogic.Abstractions.Authentication;

public interface IJwtService
{
	Task<Result<string>> GetAccessTokenAsync(
		string cardNumber,
		string password,
		CancellationToken cancellationToken = default);

	public string GenerateJWTToken(Card card);
}