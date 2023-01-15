using Microsoft.AspNetCore.Mvc;

namespace FastFood.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
