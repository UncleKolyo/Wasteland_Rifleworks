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

    [Authorize]
    public class WeaponController : Controller
    {

        private readonly IWeaponService weaponService;

        private IWebHostEnvironment environment;

        private readonly ITypeService typeService;

        private readonly IEngineerService engineerService;

        public WeaponController(IWebHostEnvironment environment, IWeaponService weaponService, ITypeService typeService, IEngineerService engineerService)
        {
            this.environment = environment;
            this.weaponService = weaponService;
            this.typeService = typeService;
            this.engineerService = engineerService;
        }

        [AllowAnonymous]
        public async Task<IActionResult> All()
        {
            string wwwPath = this.environment.WebRootPath;

            IEnumerable<IndexViewModel> viewModel =
                await this.weaponService.LastTwentyWeapon(wwwPath);
            return View(viewModel);
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
                Types = await this.typeService.AllTypesAsync()
            };
            return View(formModel);

        }
        [Authorize]

        [HttpPost]
        public async Task<IActionResult> Submit(List<IFormFile> postedFiles, WeaponFormModel model)
        {
            string? engineerId = await engineerService.GetEnginnerIdByUserIdAsync(User.GetId()!);

            var weapon = new Weapon
            {
                Name = model.Name,
                Description = model.Description,
                Complexity = model.Complexity,
                Rating = 30,
                TypeId = model.TypeId,
                EngineerId = Guid.Parse(engineerId),

            };

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

        //[HttpPost]
        //public async Task<IActionResult> Submit(List<IFormFile> postedFiles, WeaponFormModel model)
        //{

        //    var weapon = new Weapon
        //    {
        //        Name = model.Name,
        //        Description = model.Description,
        //        Complexity = model.Complexity,
        //        Rating = 30,
        //        TypeId = model.TypeId,
        //        EngineerId = Guid.Parse("49A1D5FD-AFEF-4F2B-9CCA-1BA79361DB32"),

        //    };

        //    await this.weaponService.InsertWeaponAsync(weapon);

        //    string path = Path.Combine(this.environment.WebRootPath, "Uploads");
        //    if (!Directory.Exists(path))
        //    {
        //        Directory.CreateDirectory(path);
        //    }

        //    int numOfPic = 1;

        //    List<string> uploadedFiles = new List<string>();
        //    foreach (IFormFile postedFile in postedFiles)
        //    {
        //        string fileExtension = Path.GetExtension(postedFile.FileName);
        //        string fileName = Path.GetFileName($"{weapon.Id}_{numOfPic}_WeaponPic.{fileExtension}");
        //        using (FileStream stream = new FileStream(Path.Combine(path, fileName), FileMode.Create))
        //        {
        //            postedFile.CopyTo(stream);
        //            uploadedFiles.Add(fileName);
        //            ViewBag.Message += string.Format("<b>{0}</b> uploaded.<br />", fileName);
        //        }
        //        var image = new Image()
        //        {
        //            FileName = fileName,
        //        };
        //        weapon.Images.Add(image);
        //        numOfPic++;
        //    }

        //    await this.weaponService.UpdateWeaponAsync(weapon);

        //    return RedirectToAction("All", "Weapon");
        //}




        //    try
        //    {
        //        string path = Path.Combine(this.environment.WebRootPath, "Uploads");
        //        if (!Directory.Exists(path))
        //        {
        //            Directory.CreateDirectory(path);
        //        }

        //        int numOfPic = 1;

        //        List<string> uploadedFiles = new List<string>();
        //        foreach (IFormFile postedFile in postedFiles)
        //        {
        //            string fileExtension = Path.GetExtension(postedFile.FileName);
        //            string fileName = Path.GetFileName($"{model.Id}_{numOfPic}_WeaponPic.{fileExtension}");
        //            using (FileStream stream = new FileStream(Path.Combine(path, fileName), FileMode.Create))
        //            {
        //                postedFile.CopyTo(stream);
        //                uploadedFiles.Add(fileName);
        //                ViewBag.Message += string.Format("<b>{0}</b> uploaded.<br />", fileName);
        //            }
        //            var image = new Image()
        //            {
        //                FileName = fileName,
        //            };
        //            model.Images.Add(image);
        //            numOfPic++;
        //        }

        //        string? enginnerId = await this.engineerService.GetEnginnerIdByUserIdAsync(this.User.GetId()!);

        //        await this.weaponService.InsertWeaponAsync(model, enginnerId!);
        //    }
        //    catch (Exception _)
        //    {

        //        this.ModelState.AddModelError(string.Empty, "You broke the internet! - Ivaylo Kenov 2022");

        //        model.Types = await this.typeService.AllTypesAsync();

        //        return this.View(model);
        //    }

        //    return this.RedirectToAction("All", "Weapon");

        //}

    }
}
