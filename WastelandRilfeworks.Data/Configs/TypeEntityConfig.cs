namespace WastelandRilfeworks.Data.Configs
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Models;
    public class TypeEntityConfig : IEntityTypeConfiguration<Type>
    {
        public void Configure(EntityTypeBuilder<Type> builder)
        {
            builder.HasData(this.GenerateTypes());
        }

        private Type[] GenerateTypes()
        {
            ICollection<Type> types = new HashSet<Type>();

            Type type;
            type = new Type()
            {
                Id = 1,
                Name = "Melee Weapon"
            };
            types.Add(type);

            type = new Type()
            {
                Id = 2,
                Name = "Firearm"
            };
            types.Add(type);

            type = new Type()
            {
                Id = 3,
                Name = "Explosive"
            };
            types.Add(type);

            type = new Type()
            {
                Id = 4,
                Name = "WMD"
            };
            types.Add(type);

            type = new Type()
            {
                Id = 5,
                Name = "Other"
            };
            types.Add(type);

            return types.ToArray();
        }
    }
}
