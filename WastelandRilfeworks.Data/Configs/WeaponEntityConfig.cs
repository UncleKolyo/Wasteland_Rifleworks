namespace WastelandRilfeworks.Data.Configs
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Models;



    public class WeaponEntityConfig : IEntityTypeConfiguration<Weapon>
    {
        public void Configure(EntityTypeBuilder<Weapon> builder)
        {
            builder
                .HasOne(w => w.Type)
                .WithMany(ty => ty.Weapons)
                .HasForeignKey(w => w.TypeId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(w => w.Engineer)
                .WithMany(e => e.EngineeredWeapons)
                .HasForeignKey(w => w.EngineerId)
                .OnDelete(DeleteBehavior.Restrict);

        }

    }
}
