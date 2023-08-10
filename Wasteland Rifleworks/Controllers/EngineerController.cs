namespace Wasteland_Rifleworks.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;

    using WastelandRifleworks.Web.Infrastructure.Extensions;
    using WastelandRifleworks.Web.ViewModels.Engineer;
    using WastelandRifleworks.Services.Data.Intefaces;

    using static WastelandRilfeworks.Common.NotificationMessagesConstants;


    [Authorize]

    public class EngineerController : Controller
    {
        private readonly IEngineerService engineerService;

        public EngineerController(IEngineerService engineerService)
        {
            this.engineerService = engineerService;
        }

            [HttpGet]
        public async Task<IActionResult> Become()
        {
            string? userId = this.User.GetId();
            bool isEngineer = await this.engineerService.EngineerExistsByUserIdAsync(userId!);
            if (isEngineer)
            {
                TempData[ErrorMessage] = "You already are an Engineer! You should know that!";
                return RedirectToAction("Index", "Home");
            }
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Become(BecomeEngineerFormModel model)
        {
            string? userId = User.GetId();
            bool isAgent = await engineerService.EngineerExistsByUserIdAsync(userId!);
            if (isAgent)
            {
                TempData[ErrorMessage] = "You are already an agent!";

                return RedirectToAction("Index", "Home");
            }

            try
            {
                await engineerService.Create(userId!, model);
            }
            catch (Exception)
            {
                TempData[ErrorMessage] =
                    "Something broke. Try again or contact an Admin. Sorry we are Engineers not IT guys or whatever!";

                return RedirectToAction("Index", "Home");
            }
            TempData[SuccessMessage] = "Congratulations, you are now one of us! One of us! One of us!";
            return RedirectToAction("All", "Weapon");
            
        }
    }
}
