namespace Wasteland_Rifleworks.Data
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using WastelandRilfeworks.Data.Models;

    public class WastelandRifleworksDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
    {
        public WastelandRifleworksDbContext(DbContextOptions<WastelandRifleworksDbContext> options)
            : base(options)
        {
        }
    }
}