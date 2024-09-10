﻿using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace MetaBank.BusinessLogic;

public static class DependencyInjection
{
	public static IServiceCollection AddApplication(this IServiceCollection services)
	{
		services.AddMediatR(configuration =>
		{
			configuration.RegisterServicesFromAssemblies(typeof(DependencyInjection).Assembly);
		});

		return services;
	}
}
