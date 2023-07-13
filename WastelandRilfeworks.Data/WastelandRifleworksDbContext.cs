namespace Wasteland_Rifleworks.Data
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using WastelandRilfeworks.Data.Models;
    using System.Reflection;

    public class WastelandRifleworksDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
    {
        public WastelandRifleworksDbContext(DbContextOptions<WastelandRifleworksDbContext> options)
            : base(options)
        {

        }

        public DbSet<Engineer> Engineers { get; set; } = null!;

        public DbSet<Tag> Tags { get; set; } = null!;

        public DbSet<Type> Types { get; set; } = null!;

        public DbSet<Weapon> Weapons { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            Assembly configAssembly = Assembly.GetAssembly(typeof(WastelandRifleworksDbContext)) ??
                Assembly.GetExecutingAssembly();
            builder.ApplyConfigurationsFromAssembly(configAssembly);



            base.OnModelCreating(builder);
        }

    }
}