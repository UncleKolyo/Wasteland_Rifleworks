using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Wasteland_Rifleworks.Data;
using WastelandRilfeworks.Data.Models;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);


string connectionString
    = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<WastelandRifleworksDbContext>(options =>
    options.UseSqlServer(connectionString));


builder.Services.AddDefaultIdentity<ApplicationUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
})
    .AddEntityFrameworkStores<WastelandRifleworksDbContext>();
builder.Services.AddControllersWithViews();

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
