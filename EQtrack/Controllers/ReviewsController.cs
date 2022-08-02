using Microsoft.AspNetCore.Mvc;

namespace EQtrack.Controllers
{
    public class ReviewsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
