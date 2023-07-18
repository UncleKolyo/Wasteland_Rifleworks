namespace WastelandRifleworks.Web.Infrastructure.Extensions
{
	using Microsoft.Extensions.DependencyInjection;
	using WastelandRifleworks.Services.Data.Intefaces;
	using WastelandRifleworks.Services.Data;
	using System.Reflection;

	public static class WebApplicationBuilderExtensions
	{
		public static void AddApplicationServices(this IServiceCollection services, Type serviceType)
		{
			Assembly? serviceAssembly = Assembly.GetAssembly(serviceType);
			if (serviceType == null)
			{
				throw new InvalidOperationException("You broke the internet man. Invalid Service Provided");
			}

			Type[] serviceTypes = serviceAssembly.GetTypes()
				.Where(t => t.Name.EndsWith("Service") && !t.IsInterface)
				.ToArray();
			foreach (Type st in serviceTypes)
			{
				Type? interfacetype = st
					.GetInterface($"I{st.Name}");
				if (interfacetype == null)
				{
					throw new InvalidOperationException($"No interface provided :I{st.Name}");
				}

				services.AddScoped(interfacetype, st);
			}
			services.AddScoped<IWeaponService, WeaponService>();
		}
	}
}
