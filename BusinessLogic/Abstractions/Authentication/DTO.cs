namespace MetaBank.BusinessLogic.Abstractions.Authentication;

public class LoginRequest
{
	public string CardNumber { get; set; }
	public string Pin { get; set; }
}

public class LoginResponse
{
	public string? Token { get; set; } = null;
}


public class JwtSettings
{
	public string JwtSecret { get; set; }
	public string JwtIssuer { get; set; }
	public string JwtAudience { get; set; }
	public int AccessTokenExpirationMinutes { get; set; }
}