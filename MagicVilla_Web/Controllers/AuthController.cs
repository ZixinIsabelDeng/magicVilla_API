using Microsoft.AspNetCore.Mvc;

namespace MagicVilla_Web.Controllers
{
    public class AuthController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
