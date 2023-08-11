namespace Wasteland_Rifleworks.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Hosting;

    using WastelandRifleworks.Services.Data.Intefaces;
    using Wasteland_Rifleworks.Data;
    using WastelandRilfeworks.Data.Models;
    using WastelandRifleworks.Web.ViewModels.Weapon;
    using WastelandRifleworks.Web.Infrastructure.Extensions;

    using static WastelandRilfeworks.Common.NotificationMessagesConstants;   



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

            return View(queryModel);
        }
        [Authorize]
        public async Task<IActionResult> Submit() 
        {
            bool isEngineer = await this.engineerService.EngineerExistsByUserIdAsync(this.User.GetId()!);
            if (!isEngineer)
            {
                this.TempData[ErrorMessage] = "Only Engineers can submit designs! Duh!";
                return RedirectToAction("Become", "Engineer");
            }

            WeaponFormModel formModel = new WeaponFormModel()
            {
                Types = await this.typeService.AllTypesAsync(),
                Tags = await this.tagService.AllTagsAsync(),
                // Set values for hidden tag selects to match the value of the first tag
                SecondTagId = await this.tagService.GetFirstTagIdAsync(),
                ThirdTagId = await this.tagService.GetFirstTagIdAsync(),
                // ... Repeat for FourthTagId and FifthTagId ...
            };

            return View(formModel);


        }
        [Authorize]

        [HttpPost]
        public async Task<IActionResult> Submit(List<IFormFile> postedFiles, WeaponFormModel model, IFormFile weaponSchematic)
        {
            string? engineerId = await engineerService.GetEnginnerIdByUserIdAsync(User.GetId()!);
            string? engineerUsername = await engineerService.GetEnginnerUsernameByEnginnerIdAsync(engineerId!)!;

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
                EngineerId = Guid.Parse(engineerId!),
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
                string fileName = Path.GetFileName($"{model.Name}_{numOfPic}_WeaponPic.{fileExtension}");
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
            myWeapons.AddRange(await this.weaponService.AllByEngineerIdAsync(engineerId!));
            
            return this.View(myWeapons);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Details(string id)
        {
                WeaponDetailsViewModel viewModel = await weaponService
                    .GetDetailsByIdAsync(id);


            return View(viewModel);
        }

        public IActionResult DownloadSchematic(string fileName)
        {
            var filePath = Path.Combine(environment.WebRootPath, "Uploads", fileName);

            if (!System.IO.File.Exists(filePath))
            {
                return NotFound();
            }

            var memory = new MemoryStream();
            using (var stream = new FileStream(filePath, FileMode.Open))
            {
                stream.CopyTo(memory);
            }
            memory.Position = 0;

            return File(memory, GetContentType(filePath), Path.GetFileName(filePath));
        }

        private string GetContentType(string path)
        {
            var types = GetMimeTypes();
            var ext = Path.GetExtension(path).ToLowerInvariant();
            return types.TryGetValue(ext, out var type) ? type : "application/octet-stream";
        }

        private Dictionary<string, string> GetMimeTypes()
        {
            return new Dictionary<string, string>
    {
        { ".pdf", "application/pdf" },
        // Add more mime types as needed
    };
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            bool weaponExists = await weaponService
                .ExistsByIdAsync(id);
            if (!weaponExists)
            {
                TempData[ErrorMessage] = "Weapon with the provided id does not exist!";

                return RedirectToAction("All", "House");

                //return this.NotFound(); -> If you want to return 404 page
            }

            bool isUserEngineer = await engineerService
                .EngineerExistsByUserIdAsync(User.GetId()!);
            if (!isUserEngineer && !User.IsAdmin())
            {
                TempData[ErrorMessage] = "You must become an engineer in order to edit house info!";

                return RedirectToAction("Become", "Engineer");
            }

            string? engineerId =
              await engineerService.GetEnginnerIdByUserIdAsync(User.GetId()!);
            bool isEngineerCreator = await weaponService
                .IsEngineertWithIdCreatorOfWeaponWithIdAsync(id, engineerId!);
            if (!isEngineerCreator && !User.IsAdmin())
            {
                TempData[ErrorMessage] = "You must be the creator of the weapon you want to edit!";

                return RedirectToAction("Mine", "Weapon");
            }

            WeaponFormModel formModel = await weaponService
                    .GetWeaponForEditbyIdAsync(id);
                formModel.Types = await typeService.AllTypesAsync();
                formModel.Tags = await tagService.AllTagsAsync();


                return View(formModel);

        }

        [HttpPost]
        public async Task<IActionResult> Edit(string id, WeaponFormModel model)
        {

            bool weaponExists = await weaponService
                .ExistsByIdAsync(id);
            if (!weaponExists)
            {
                TempData[ErrorMessage] = "Weapon with the provided id does not exist!";

                return RedirectToAction("All", "Weapon");
            }

            bool isUserEngineer = await engineerService
                .EngineerExistsByUserIdAsync(User.GetId()!);
            if (!isUserEngineer && !User.IsAdmin())
            {
                TempData[ErrorMessage] = "You must become an engineer in order to edit house info!";

                return RedirectToAction("Become", "Engineer");
            }

            string? engineerId =
              await engineerService.GetEnginnerIdByUserIdAsync(User.GetId()!);
            bool isEngineerCreator = await weaponService
                .IsEngineertWithIdCreatorOfWeaponWithIdAsync(id, engineerId!);
            if (!isEngineerCreator && !User.IsAdmin())
            {
                TempData[ErrorMessage] = "You must be the creator of the weapon you want to edit!";

                return RedirectToAction("Mine", "Weapon");
            }

            try
            {
                await weaponService.EditWeaponByIdAndFormModelAsync(id, model);
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty,
                    "Something broke. Idk, contact and Admin or something, or try again...");
                model.Types = await typeService.AllTypesAsync();
                model.Tags = await  tagService.AllTagsAsync();

                return View(model);
            }

            TempData[SuccessMessage] = "Weapon was edited successfully!";
            return RedirectToAction("Details", "Weapon", new { id });
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            bool weaponExists = await weaponService
               .ExistsByIdAsync(id);
            if (!weaponExists)
            {
                TempData[ErrorMessage] = "Weapon with the provided id does not exist!";

                return RedirectToAction("All", "Weapon");
            }

            bool isUserEngineer = await engineerService
                .EngineerExistsByUserIdAsync(User.GetId()!);
            if (!isUserEngineer && !User.IsAdmin())
            {
                TempData[ErrorMessage] = "You must become an engineer in order to edit house info!";

                return RedirectToAction("Become", "Engineer");
            }

            string? engineerId =
              await engineerService.GetEnginnerIdByUserIdAsync(User.GetId()!);
            bool isEngineerCreator = await weaponService
                .IsEngineertWithIdCreatorOfWeaponWithIdAsync(id, engineerId!);
            if (!isEngineerCreator && !User.IsAdmin())
            {
                TempData[ErrorMessage] = "You must be the creator of the weapon you want to edit!";

                return RedirectToAction("Mine", "Weapon");
            }

            try
            {
                WeaponPreDeleteViewModel viewModel =
                    await weaponService.GetWeaponForDeleteByIdAsync(id);

                return View(viewModel);
            }
            catch (Exception)
            {
                return View("Error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id, WeaponPreDeleteViewModel model)
        {
            bool weaponExists = await weaponService
               .ExistsByIdAsync(id);
            if (!weaponExists)
            {
                TempData[ErrorMessage] = "Weapon with the provided id does not exist!";

                return RedirectToAction("All", "Weapon");
            }

            bool isUserEngineer = await engineerService
                .EngineerExistsByUserIdAsync(User.GetId()!);
            if (!isUserEngineer && !User.IsAdmin())
            {
                TempData[ErrorMessage] = "You must become an engineer in order to edit house info!";

                return RedirectToAction("Become", "Engineer");
            }

            string? engineerId =
              await engineerService.GetEnginnerIdByUserIdAsync(User.GetId()!);
            bool isEngineerCreator = await weaponService
                .IsEngineertWithIdCreatorOfWeaponWithIdAsync(id, engineerId!);
            if (!isEngineerCreator && !User.IsAdmin())
            {
                TempData[ErrorMessage] = "You must be the creator of the weapon you want to edit!";

                return RedirectToAction("Mine", "Weapon");
            }

            try
            {
                await weaponService.DeleteWeaponByIdAsync(id);

                TempData[WarningMessage] = "The house was successfully deleted!";
                return RedirectToAction("Mine", "Weapon");
            }
            catch (Exception)
            {
                return View("Error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> ToggleLike(string weaponId)
        {
            string userId = this.User.GetId()!;

            string? engineerId = await this.engineerService.GetEnginnerIdByUserIdAsync(userId);

            await weaponService.ToggleUserReactionAsync(engineerId, weaponId, ReactionType.Like);

            // Redirect back to the details page
            return RedirectToAction("Details", new { id = weaponId });
        }

        [HttpPost]
        public async Task<IActionResult> ToggleDislike(string weaponId)
        {
            string userId = this.User.GetId()!;

            string? engineerId = await this.engineerService.GetEnginnerIdByUserIdAsync(userId);

            await weaponService.ToggleUserReactionAsync(engineerId, weaponId, ReactionType.Dislike);

            // Redirect back to the details page
            return RedirectToAction("Details", new { id = weaponId });
        }
    }
} 
