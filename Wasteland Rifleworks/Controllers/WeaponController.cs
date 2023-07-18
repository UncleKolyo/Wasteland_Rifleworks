namespace Wasteland_Rifleworks.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;

    [Authorize]
    public class WeaponController : Controller
    {
        private Microsoft.AspNetCore.Hosting.IHostingEnvironment Environment;

        public WeaponController(Microsoft.AspNetCore.Hosting.IHostingEnvironment _environment)
        {
            Environment = _environment;
        }

        private readonly string wwwRootDir =
            Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");

        [AllowAnonymous]
        public async Task<IActionResult> All()
        {
            return View();
        }

        public async Task<IActionResult> Submit()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Submit(List<IFormFile> postedFiles)
        {
            string wwwPath = this.Environment.WebRootPath;
            string contentPath = this.Environment.ContentRootPath;

            string path = Path.Combine(this.Environment.WebRootPath, "Uploads");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            int numOfPic = 1;

            List<string> uploadedFiles = new List<string>();
            foreach (IFormFile postedFile in postedFiles)
            {
                string fileName = Path.GetFileName($"{1}_{numOfPic}_WeaponPic");
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
