using Microsoft.AspNetCore.Mvc;

namespace Website_ShopeeFood.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult LogIn()
        {
            return View();
        }
    }
}
