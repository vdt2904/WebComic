using Microsoft.AspNetCore.Mvc;

namespace WebComic.Areas.Admin.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [Area("Admin")]
    public class DashboardController : Controller
    {
        [Route("admin")]
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [Route("admin/error")]
        [HttpGet]
        public IActionResult Error()
        {
            return View();
        }
    }
}
