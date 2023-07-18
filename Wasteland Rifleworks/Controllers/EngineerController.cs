namespace Wasteland_Rifleworks.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;

    [Authorize]

    public class EngineerController : Controller
    {
        public async Task<IActionResult> Become()
        {
            return View();
        }
    }
}
