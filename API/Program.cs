using Microsoft.EntityFrameworkCore.Design;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Events;
using Serilog.Templates;
using Serilog.Templates.Themes;
using SerilogTracing;
using MetaBank.Api;
using MetaBank.Persistence;
using MetaBank.BusinessLogic;
using MetaBank.Api.Extensions;

// Uncommented durying development only.
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateBootstrapLogger();
//

try
{
    //Testing Logger at startup.
    Log.Information("Starting up from: {card} at {dir}. ProcessID: {proc}", Environment.UserName, Environment.CurrentDirectory, Environment.ProcessId);
    ////Supporting Activity Diagnossis.
    using var listener = new ActivityListenerConfiguration()
        .Instrument.AspNetCoreRequests()
        .TraceToSharedLogger();

    var builder = WebApplication.CreateBuilder();

    builder.Services.AddLogger(builder.Configuration);

    builder.Services.AddApplication();
	builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(option =>
	{
		option.SwaggerDoc("v1", new OpenApiInfo { Title = "Demo API", Version = "v1" });
		option.AddSecurityDefinition(
			"Bearer",
			new OpenApiSecurityScheme
			{
				In = ParameterLocation.Header,
				Description = "Please enter a valid token",
				Name = "Authorization",
				Type = SecuritySchemeType.Http,
				BearerFormat = "JWT",
				Scheme = "Bearer"
			}
		);
		option.AddSecurityRequirement(
			new OpenApiSecurityRequirement
			{
			{
				new OpenApiSecurityScheme
				{
					Reference = new OpenApiReference
					{
						Type = ReferenceType.SecurityScheme,
						Id = "Bearer"
					}
				},
				new string[] { }
			}
			}
		);
	});
    builder.Services.AddAuthentication(builder.Configuration);
	builder.Services.AddAuthorization();
	builder.Services.AddControllers();

    builder.Services.AddInfrastructure(builder.Configuration);
    
    var app = builder.Build();
    //app.UseSerilogRequestLogging();

    if (app.Environment.IsDevelopment())
    {
    app.UseSwagger();
    app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();
    app.UseAuthentication();
	app.UseAuthorization();
	app.UseCustomExceptionHandler();
	app.MapControllers();

	app.Run();

}
catch (Exception ex)
{
    Log.Fatal(ex, "Unhandled exception");
}
finally
{
    Log.CloseAndFlush();
}
