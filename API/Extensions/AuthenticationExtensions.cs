using MetaBank.Api.Extensions;
using MetaBank.BusinessLogic.Contracts.Other;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

namespace MetaBank.Api.Extensions;

public static class AuthenticationExtensions
{
	public static IServiceCollection AddAuthentication(this IServiceCollection services, IConfiguration configuration)
	{
		var authSectionConfiguration = configuration.GetSection("AuthConfiguration");
		services.Configure<AuthConfiguration>(authSectionConfiguration);

		var AuthConfiguration = authSectionConfiguration.Get<AuthConfiguration>();

		var key = Encoding.ASCII.GetBytes(AuthConfiguration.JwtSecret);
		var Issuer = AuthConfiguration.JwtIssuer;
		var Audience = AuthConfiguration.JwtAudience;

		services.AddHttpContextAccessor();

		services.AddAuthentication(x =>
		{
			x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
			x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
		})
		.AddJwtBearer(x =>
		{
			x.Events = new JwtBearerEvents
			{
				OnTokenValidated = context =>
				{
					var cardIdentifier = context.Principal?.Identity?.Name;
					var cardTriesToBlock = context.Principal?.FindFirst(ClaimTypes.Expired)?.Value;
					return Task.CompletedTask;
				},

				OnAuthenticationFailed = context =>
				{
					if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
					{
						context.Response.Headers.Append("Token-Expired", "true");
					}
					return Task.CompletedTask;
				}
			};
			x.RequireHttpsMetadata = false;
			x.SaveToken = false;
			x.TokenValidationParameters = new TokenValidationParameters
			{
				ValidateIssuerSigningKey = true,
				IssuerSigningKey = new SymmetricSecurityKey(key),
				ValidateIssuer = true,
				ValidIssuer = Issuer,
				ValidateAudience = true,
				ValidAudience = Audience,
				ValidateLifetime = true,
				ClockSkew = TimeSpan.Zero
			};
		});
		return services;
	}
}
