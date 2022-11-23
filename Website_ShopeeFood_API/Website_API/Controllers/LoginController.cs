using Microsoft.AspNetCore.Mvc;

namespace Website_API.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
