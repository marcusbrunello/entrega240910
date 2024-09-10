using MetaBank.BusinessLogic.Abstractions.Authentication;
using MetaBank.Model.Base;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Model.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MetaBank.Persistence.Authentication;

public class JwtTokenService : IJwtService
{
	private readonly IConfiguration _configuration;

	public JwtTokenService(IConfiguration configuration)
	{
		_configuration = configuration;
	}

	//public string GenerateAccessToken(IEnumerable<Claim> claims)
	//{
	//	var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["AuthConfiguration:JwtSecret"]!));

	//	var issuer = _configuration["AuthConfiguration:JwtIssuer"]!;
	//	var audience = _configuration["AuthConfiguration:JwtAudience"]!;
	//	var expires = double.Parse(_configuration["AuthConfiguration:JwtAccessTokenExpirationMinutes"]!);
	//	var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

	//	var token = new JwtSecurityToken(
	//		issuer: issuer,
	//		audience: audience,
	//		claims: claims,
	//		expires: DateTime.Now.AddMinutes(expires),
	//		signingCredentials: creds);

	//	return new JwtSecurityTokenHandler().WriteToken(token);
	//}

	public string GenerateJWTToken(Card card)
	{
		var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["AuthConfiguration:JwtSecret"]!));

		var issuer = _configuration["AuthConfiguration:JwtIssuer"]!;
		var audience = _configuration["AuthConfiguration:JwtAudience"]!;
		var expires = double.Parse(_configuration["AuthConfiguration:AccessTokenExpirationMinutes"]!);
		var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

		var userClaims = new[]
		{
					new Claim(ClaimTypes.NameIdentifier, card.Id.ToString()),
					new Claim(ClaimTypes.Name, card.Number!),
					new Claim(ClaimTypes.Expired, card.TriesLeftToBlock.ToString())
				};
		var token = new JwtSecurityToken(
			issuer: issuer,
			audience: audience,
			claims: userClaims,
			expires: DateTime.Now.AddMinutes(expires),
			signingCredentials: creds
		);

		return new JwtSecurityTokenHandler().WriteToken(token);
	}

	public Task<Result<string>> GetAccessTokenAsync(string cardNumber, string password, CancellationToken cancellationToken = default)
	{
		throw new NotImplementedException();
	}
}