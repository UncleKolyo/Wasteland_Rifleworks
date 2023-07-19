namespace Wasteland_Rifleworks.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;
	using WastelandRifleworks.Services.Data.Intefaces;
	using WastelandRifleworks.Web.ViewModels.Home;
    using Microsoft.AspNetCore.Hosting;


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

        public async Task<IActionResult> Submit()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Submit(List<IFormFile> postedFiles)
        {
            string wwwPath = this.environment.WebRootPath;
            string contentPath = this.environment.ContentRootPath;

            Console.WriteLine($"wwwPath:{wwwPath}");
            Console.WriteLine($"contentPath:{contentPath}");

            string path = Path.Combine(this.environment.WebRootPath, "Uploads");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            int numOfPic = 1;

            List<string> uploadedFiles = new List<string>();
            foreach (IFormFile postedFile in postedFiles)
            {
                string fileExtension = Path.GetFileName(postedFile.FileName.Split('.')[1]);
                string fileName = Path.GetFileName($"{1}_{numOfPic}_WeaponPic.{fileExtension}");
                using (FileStream stream = new FileStream(Path.Combine(path, fileName), FileMode.Create))
                {
                    postedFile.CopyTo(stream);
                    uploadedFiles.Add(fileName);
                    ViewBag.Message += string.Format("<b>{0}</b> uploaded.<br />", fileName);
                }
                numOfPic++;
            }

            return View();
        }

        //[HttpPost]
        //public async Task<IActionResult> Submit(IFormFile formFile)
        //{
        //    if (formFile != null)
        //    {
        //        var path = Path.Combine
        //            (wwwRootDir, DateTime.Now.Ticks.ToString() + Path.GetExtension(formFile.FileName));

        //        using (var stream = new FileStream(path, FileMode.Create))
        //        {
        //            await formFile.CopyToAsync(stream);
        //        }
        //        return RedirectToAction("Submit");
        //    }
        //    return View();

        //}
    }
}
