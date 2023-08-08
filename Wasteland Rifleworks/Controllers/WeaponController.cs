namespace Wasteland_Rifleworks.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;
    using WastelandRifleworks.Services.Data.Intefaces;
    using WastelandRifleworks.Web.ViewModels.Home;
    using Microsoft.AspNetCore.Hosting;
    using WastelandRilfeworks.Data.Models;
    using WastelandRifleworks.Web.ViewModels.Weapon;
    using WastelandRifleworks.Web.Infrastructure.Extensions;
    using static WastelandRilfeworks.Common.NotificationMessagesConstants;
    using Wasteland_Rifleworks.Data;



    [Authorize]
    public class WeaponController : Controller
    {

        private readonly IWeaponService weaponService;

        private IWebHostEnvironment environment;

        private readonly ITypeService typeService;

        private readonly IEngineerService engineerService;

        private readonly ITagService tagService;

        private readonly WastelandRifleworksDbContext dbContext;

        public WeaponController(IWebHostEnvironment environment, IWeaponService weaponService, ITypeService typeService, IEngineerService engineerService, ITagService tagService, WastelandRifleworksDbContext dbContext)
        {
            this.environment = environment;
            this.weaponService = weaponService;
            this.typeService = typeService;
            this.engineerService = engineerService;
            this.tagService = tagService;
            this.dbContext = dbContext;
        }

        [AllowAnonymous]
        public async Task<IActionResult> All([FromQuery] AllWeaponsQueryModel queryModel)
        {
            string wwwPath = this.environment.WebRootPath;

            AllWeaponsFilteredAndPagedServiceModel serviceModel =
                await weaponService.AllAsync(queryModel, wwwPath);

            queryModel.Weapons = serviceModel.Weapons;
            queryModel.TotalWeapons = serviceModel.TotalWeaponsCount;
            queryModel.Types = await typeService.AllNamesAsync();

            //IEnumerable<AllWeaponViewModel> viewModel =
            //    await this.weaponService.LastTwentyWeapon(wwwPath);
            return View(queryModel);
        }
        [Authorize]
        public async Task<IActionResult> Submit()
        {
            bool isEngineer =
                await this.engineerService.EngineerExistsByUserIdAsync(this.User.GetId()!);
            if (!isEngineer)
            {
                this.TempData[ErrorMessage] = "Only Engineers can submit designs! Duh!";
                return RedirectToAction("Become", "Engineer");
            }

            WeaponFormModel formModel = new WeaponFormModel()
            {
                Types = await this.typeService.AllTypesAsync(),
                Tags = await this.tagService.AllTagsAsync()
            };
            return View(formModel);


        }
        [Authorize]

        [HttpPost]
        public async Task<IActionResult> Submit(List<IFormFile> postedFiles, WeaponFormModel model)
        {
            string? engineerId = await engineerService.GetEnginnerIdByUserIdAsync(User.GetId()!);
            string? engineerUsername = await engineerService.GetEnginnerUsernameByEnginnerIdAsync(engineerId)!;

            var tagIds = new List<int> { model.FirstTagId, model.SecondTagId, model.ThirdTagId, model.ForthTagId, model.FifthTagId };

            if (tagIds.Count != 5)
            {
                TempData[ErrorMessage] = "You need to pick 5 different tags!";
                return RedirectToAction("Submit", "Weapon");
            }

            var selectedTags = dbContext.Tags.Where(t => tagIds.Contains(t.Id)).ToList();


            var weapon = new Weapon
            {
                Name = model.Name,
                Description = model.Description,
                Complexity = model.Complexity,
                Rating = 30,
                TypeId = model.TypeId,
                EngineerId = Guid.Parse(engineerId),
            };

            foreach (var tag in selectedTags)
            {
                weapon.Tags.Add(tag);
            }

            await this.weaponService.InsertWeaponAsync(weapon);

            string path = Path.Combine(this.environment.WebRootPath, "Uploads");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            int numOfPic = 1;

            List<string> uploadedFiles = new List<string>();
            foreach (IFormFile postedFile in postedFiles)
            {
                string fileExtension = Path.GetExtension(postedFile.FileName);
                string fileName = Path.GetFileName($"{weapon.Id}_{numOfPic}_WeaponPic.{fileExtension}");
                using (FileStream stream = new FileStream(Path.Combine(path, fileName), FileMode.Create))
                {
                    postedFile.CopyTo(stream);
                    uploadedFiles.Add(fileName);
                    ViewBag.Message += string.Format("<b>{0}</b> uploaded.<br />", fileName);
                }
                var image = new Image()
                {
                    FileName = fileName,
                };
                weapon.Images.Add(image);
                numOfPic++;
            }

            await this.weaponService.UpdateWeaponAsync(weapon);

            return RedirectToAction("All", "Weapon");
        }


    }
}
