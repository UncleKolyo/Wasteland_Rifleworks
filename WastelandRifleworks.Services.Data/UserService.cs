namespace WastelandRifleworks.Services.Data
{
    using Microsoft.EntityFrameworkCore;

    using WastelandRilfeworks.Data;
    using WastelandRilfeworks.Data.Models;
    using WastelandRifleworks.Services.Data.Intefaces;
    using Web.ViewModels.User;
    using Wasteland_Rifleworks.Data;

    public class UserService : IUserService
    {
        private readonly WastelandRifleworksDbContext dbContext;

        private readonly IEngineerService engineerService;

        private readonly ITagService tagService = null!;

        public UserService(WastelandRifleworksDbContext dbContext, IEngineerService engineerService)
        {
            this.dbContext = dbContext;
            this.engineerService = engineerService;
        }


        public async Task<string> GetFullUsernameNameByIdAsync(string userId)
        {
            ApplicationUser? user = await this.dbContext
                .Users
                .FirstOrDefaultAsync(u => u.Id.ToString() == userId);
            if (await engineerService.EngineerExistsByUserIdAsync(userId))
            {
                Engineer engineer = await this.dbContext.Engineers
                    .FirstAsync(e => e.UserId.ToString() == userId);
                return $"{engineer.Username}";
            }
            else
            {
                return $"{user.Email.Split('@')[0]}";
            }
        }



        public async Task<IEnumerable<UserViewModel>> AllAsync()
        {
            List<UserViewModel> allUsers = await this.dbContext
                .Users
                .Select(u => new UserViewModel()
                {
                    Id = u.Id.ToString(),
                    Email = u.Email,
                    Username = u.UserName
                })
                .ToListAsync();
            foreach (UserViewModel user in allUsers)
            {
                Engineer? engineer = this.dbContext
                    .Engineers
                    .FirstOrDefault(a => a.UserId.ToString() == user.Id);
                if (engineer != null)
                {
                    user.Username = engineer.Username;
                }
            }

            return allUsers;
        }
    }
}
