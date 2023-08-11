using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Wasteland_Rifleworks.Data;
using WastelandRifleworks.Services.Data.Intefaces;
using WastelandRifleworks.Web.Infrastructure.Extensions;
using WastelandRilfeworks.Data.Models;
using Microsoft.AspNetCore.Identity;
using static WastelandGeneralConstants.WastelandGeneralConstants;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);


string connectionString
    = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<WastelandRifleworksDbContext>(options =>
    options.UseSqlServer(connectionString));


builder.Services.AddDefaultIdentity<ApplicationUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = builder.Configuration.GetValue<bool>("Identity:SignIn:RequireConfirmedAccounts");
    options.Password.RequireLowercase = builder.Configuration.GetValue<bool>("Identity:Password:RequireLowercase");
    options.Password.RequireUppercase = builder.Configuration.GetValue<bool>("Identity:Password:RequireUppercase");
    options.Password.RequireNonAlphanumeric = builder.Configuration.GetValue<bool>("Identity:Password:RequireNonAlphanumeric");
    options.Password.RequiredLength = builder.Configuration.GetValue<int>("Identity:Password:RequiredLength");
})
    .AddRoles<IdentityRole<Guid>>()
    .AddEntityFrameworkStores<WastelandRifleworksDbContext>();

builder.Services.AddApplicationServices(typeof(IWeaponService));

builder.Services.AddControllersWithViews();

// Configure the HTTP request pipeline.

builder.Services.ConfigureApplicationCookie(cfg =>
{
    cfg.LoginPath = "/User/Login";
    cfg.AccessDeniedPath = "/Home/Error/401";
});

builder.Services
    .AddControllersWithViews()
    .AddMvcOptions(options =>
    {
        options.Filters.Add<AutoValidateAntiforgeryTokenAttribute>();
    });

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error/500");
    app.UseStatusCodePagesWithRedirects("/Home/Error?statusCode={0}");

    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.SeedAdministrator(DevAdminEmail);



app.UseEndpoints(config =>
{
    config.MapControllerRoute(
  name: "areas",
  pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

    config.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

    //config.MapDefaultControllerRoute();
    config.MapRazorPages();
});

app.Run();
