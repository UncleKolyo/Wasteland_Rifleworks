namespace WastelandRifework.Services.Tests
{
    using Wasteland_Rifleworks.Data;
    using WastelandRilfeworks.Data.Models;

    public static class DatabaseSeeder
    {
        public static ApplicationUser EngineerUser;
        public static ApplicationUser User;
        public static Engineer engineer;

        public static void SeedDatabase(WastelandRifleworksDbContext dbContext)
        {
            EngineerUser = new ApplicationUser()
            {
                UserName = "KolyoTheCreator@kolyo.com",
                NormalizedUserName = "KOLYOTHECREATOR@KOLYO.COM",
                Email = "KolyoTheCreator@kolyo.com",
                NormalizedEmail = "KOLYOTHECREATOR@KOLYO.COM",
                EmailConfirmed = true,
                PasswordHash = "AQAAAAEAACcQAAAAEC44NYCTaldi9D2CtxQU3wQSVLOxF7SeQOko3O+I9mQKL4r7vOqZiqNBvIWx86byzw==",
                ConcurrencyStamp = "abeb6ef0-d87b-4ac9-877c-36a0b6abc112",
                SecurityStamp = "3OIWPPMVZJDWEN2AVMJTINAEG4H6KCP2",
                TwoFactorEnabled = false,

            };
            User = new ApplicationUser()
            {
                UserName = "OsmanPasha@Osman.com",
                NormalizedUserName = "OSMANPASHA@OSMAN.COM",
                Email = "OsmanPasha@Osman.com",
                NormalizedEmail = "OSMANPASHA@OSMAN.COM",
                EmailConfirmed = true,
                PasswordHash = "AQAAAAEAACcQAAAAEID9ZOexL49mUrN+To4ugNXLJlUVlcfrKVPwYywgpugromL9HZuWmAdfOanE9dTpzA==",
                ConcurrencyStamp = "544e96dd-969b-459a-9274-32e4cbf49072",
                SecurityStamp = "DUHFQ75U5T7XGIUNU6SPPBMSI53R3FDP",
                TwoFactorEnabled = false,

            };
            engineer = new Engineer()
            {
                Username = "Hasan",
                UserId = EngineerUser.Id
            };

            var tags = new List<Tag>
            {
                new Tag { Name = "Assault Rifle" },
                new Tag { Name = "Sniper Rifle" },
                new Tag { Name = "Pistol" },
                new Tag { Name = "Shotgun" },
                new Tag { Name = "Submachine Gun" },

            };

            var types = new List<Type>
            {
                new Type { Name = "Assault Rifle" },
                new Type { Name = "Sniper Rifle" },
                new Type { Name = "Pistol" },
                new Type { Name = "Shotgun" },
                new Type { Name = "Submachine Gun" },

            };

            var weapons = new List<Weapon>
            {
                 new Weapon
                {
                    Name = "Assault Rifle Weapon",
                    ShortDescription = "Short description 1",
                    FullDescription = "Full description 1",
                    Complexity = 3,
                    Rating = 4,
                    TypeId = 1,
                    EngineerId = EngineerUser.Id,
                },
                new Weapon
                {
                    Name = "Sniper Rifle Weapon",
                    ShortDescription = "Short description 2",
                    FullDescription = "Full description 2",
                    Complexity = 5,
                    Rating = 3,
                    TypeId = 2,
                    EngineerId = EngineerUser.Id,
                },
                new Weapon
                {
                    Name = "Pistol Weapon",
                    ShortDescription = "Short description 3",
                    FullDescription = "Full description 3",
                    Complexity = 2,
                    Rating = 5,
                    TypeId = 3,
                    EngineerId = EngineerUser.Id,
                },
            };

            dbContext.Types.AddRange(types);

            dbContext.Weapons.AddRange(weapons);

            dbContext.Tags.AddRange(tags);

            dbContext.Users.Add(EngineerUser);
            dbContext.Users.Add(User);
            dbContext.Engineers.Add(engineer);

            dbContext.SaveChanges();
        }
    }
}
