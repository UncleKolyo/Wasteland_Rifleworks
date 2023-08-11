namespace WastelandRifleworks.Web.Infrastructure.Extensions
{
	using Microsoft.Extensions.DependencyInjection;

	using WastelandRifleworks.Services.Data.Intefaces;
	using WastelandRifleworks.Services.Data;

	using System.Reflection;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Identity;
	using static WastelandGeneralConstants.WastelandGeneralConstants;


	public static class WebApplicationBuilderExtensions
	{
		public static void AddApplicationServices(this IServiceCollection services, Type serviceType)
		{
			Assembly? serviceAssembly = Assembly.GetAssembly(serviceType);
			if (serviceType == null)
			{
				throw new InvalidOperationException("You broke the internet man. Invalid Service Provided");
			}

			Type[] serviceTypes = serviceAssembly!.GetTypes()
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

		public static IApplicationBuilder SeedAdministrator(this IApplicationBuilder app, string email)
		{
			using IServiceScope scopedServices = app.ApplicationServices.CreateScope();

			IServiceProvider serviceProvider = scopedServices.ServiceProvider;

			UserManager<WastelandRilfeworks.Data.Models.ApplicationUser> userManager =
				serviceProvider.GetRequiredService<UserManager<WastelandRilfeworks.Data.Models.ApplicationUser>>();
			RoleManager<IdentityRole<Guid>> roleManager =
				serviceProvider.GetRequiredService<RoleManager<IdentityRole<Guid>>>();

			Task.Run(async () =>
			{
				if (await roleManager.RoleExistsAsync(AdminRoleName))
				{
					return;
				}

				IdentityRole<Guid> role =
					new IdentityRole<Guid>(AdminRoleName);

				await roleManager.CreateAsync(role);

                WastelandRilfeworks.Data.Models.ApplicationUser adminUser =
					await userManager.FindByEmailAsync(email);

				await userManager.AddToRoleAsync(adminUser, AdminRoleName);
			})
			.GetAwaiter()
			.GetResult();

			return app;
		}
	}
}
