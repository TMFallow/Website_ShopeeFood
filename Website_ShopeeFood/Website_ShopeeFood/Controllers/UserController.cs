using Microsoft.AspNetCore.Mvc;

namespace Website_ShopeeFood.Controllers
{
    public class UserController : Controller
    {
        [HttpGet]
        public IActionResult UpdateUserInfo()
        {
            return View();
        }
    }
}
