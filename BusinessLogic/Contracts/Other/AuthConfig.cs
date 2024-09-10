namespace MetaBank.BusinessLogic.Contracts.Other
{
	public class AuthConfiguration
	{
		public string OriginCors { get; set; }
		public string JwtSecret { get; set; }
		public string JwtIssuer { get; set; }
		public string JwtAudience { get; set; }
	}
}
