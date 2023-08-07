namespace Wasteland_Rifleworks.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;
    using WastelandRifleworks.Services.Data.Intefaces;
    using WastelandRifleworks.Web.ViewModels.Home;
    using Microsoft.AspNetCore.Hosting;
    using WastelandRilfeworks.Data.Models;

    [Authorize]
    public class WeaponController : Controller
    {

        private readonly IWeaponService weaponService;

        private IWebHostEnvironment environment;

        public WeaponController(IWebHostEnvironment environment, IWeaponService weaponService)
        {
            this.environment = environment;
            this.weaponService = weaponService;
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
            return View();
        }
        [Authorize]

        [HttpPost]
        public async Task<IActionResult> Submit(List<IFormFile> postedFiles)
        {
            var weapon = new Weapon
            {
                Name = "test",
                Description = "testDesc",
                Complexity = 50,
                Rating = 45,
                TypeId = 2,
                EngineerId = Guid.Parse("49A1D5FD-AFEF-4F2B-9CCA-1BA79361DB32"),
                Tags = new List<Tag> { }

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

            return View();
        }

    }
}
