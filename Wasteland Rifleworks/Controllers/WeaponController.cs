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
            queryModel.Tags = await tagService.AllNamesAsync();

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
        public async Task<IActionResult> Submit(List<IFormFile> postedFiles, WeaponFormModel model, IFormFile weaponSchematic)
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
                ShortDescription = model.ShortDescription,
                FullDescription = model.FullDescription,
                Complexity = model.Complexity,
                Rating = 30,
                TypeId = model.TypeId,
                EngineerId = Guid.Parse(engineerId),
            };

            foreach (var tag in selectedTags)
            {
                weapon.Tags.Add(tag);
            }

            

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

            string schemaExtension = Path.GetExtension(weaponSchematic.FileName);
            string schemaName = Path.GetFileName($"{weapon.Id}_Schematic.{schemaExtension}");
            using (FileStream stream = new FileStream(Path.Combine(path, schemaName), FileMode.Create))
            {
                weaponSchematic.CopyTo(stream);
                ViewBag.Message += string.Format("<b>{0}</b> uploaded.<br />", schemaName);
            }
            var Schematic = new Schematic()
            {
                FileName = schemaName,
            };
            weapon.WeaponSchematic = Schematic;

            await this.weaponService.InsertWeaponAsync(weapon);

            await this.weaponService.UpdateWeaponAsync(weapon);

            return RedirectToAction("All", "Weapon");
        }

        public async Task<IActionResult> Mine()
        {
            List<AllWeaponViewModel> myWeapons = new List<AllWeaponViewModel>();

            string userId = this.User.GetId()!;

            string? engineerId = await this.engineerService.GetEnginnerIdByUserIdAsync(userId);
            myWeapons.AddRange(await this.weaponService.AllByEngineerIdAsync(engineerId));
            
            return this.View(myWeapons);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Details(string id)
        {
            //bool weaponExists = await weaponService
            //    .ex(id);
            //if (!houseExists)
            //{
            //    TempData[ErrorMessage] = "House with the provided id does not exist!";

            //    return RedirectToAction("All", "House");
            //}
                WeaponDetailsViewModel viewModel = await weaponService
                    .GetDetailsByIdAsync(id);

                return View(viewModel);
        }

        public async Task<IActionResult> DownloadSchema(string id)
        {
            string path = Path.Combine(this.environment.WebRootPath, "Uploads");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            var memory = new MemoryStream();
            using (var stream = new FileStream(path, FileMode.Open, FileAccess.ReadWrite))
            { 
            await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            var contentType = "APPLICATION/octec-stream";

            var fileName = Path.GetFileName(path);
            return File(memory, contentType, fileName);
        }
    }
}
