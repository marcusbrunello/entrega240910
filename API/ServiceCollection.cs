using Serilog;
using Serilog.Events;
using Serilog.Templates;
using Serilog.Templates.Themes;

namespace MetaBank.Api;

public static class ServiceCollection
{
	public static IServiceCollection AddLogger(this IServiceCollection services, IConfiguration configuration)
	{
		services.AddSerilog((services, lc) => lc
				.ReadFrom.Configuration(configuration)
				.ReadFrom.Services(services)
				.MinimumLevel.Override("Microsoft.AspNetCore.Hosting", LogEventLevel.Warning)
				.MinimumLevel.Override("Microsoft.AspNetCore.Mvc", LogEventLevel.Warning)
				.MinimumLevel.Override("Microsoft.AspNetCore.Routing", LogEventLevel.Warning)
				.Enrich.FromLogContext()
				.WriteTo.Console(new ExpressionTemplate(
					// Include trace and span ids when present.
					"[{@t:HH:mm:ss} {@l:u3}{#if @tr is not null} ({substring(@tr,0,4)}:{substring(@sp,0,4)}){#end}] {SourceContext} {@m}\n{@x}",
					theme: TemplateTheme.Code)));

		return services;
	}
}
