using Microsoft.AspNetCore.Mvc;

namespace WebComic.Areas.Admin.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [Area("Admin")]
    public class GenreController : Controller
    {
        [Route("admin/genre")]
        [HttpGet]
        public IActionResult ListGenre(int? page)
        {
            if (page == null)
            {
                page = 1;
            }
            ViewBag.Page = page; 
            return View();
        }
        [Route("admin/genre/add")]
        [HttpGet]
        public IActionResult AddGenre()
        {
            return View();
        }
		[Route("admin/genre/{id}")]
		[HttpGet]
		public IActionResult EditGenre(int id)
		{
            ViewBag.GenreId = id;
            return View();
		}
	}
}
