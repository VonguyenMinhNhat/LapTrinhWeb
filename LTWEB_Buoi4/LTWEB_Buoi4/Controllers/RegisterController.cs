using LTWEB_Buoi4.Models;
using Microsoft.AspNetCore.Mvc;

namespace LTWEB_Buoi4.Controllers
{
    public class RegisterController : Controller
    {
        public IActionResult Index()
        {

            return View();
        }
        [HttpPost]
        public IActionResult Index(user user)
        {
            if (ModelState.IsValid)
            {
                return Content("đăng ký thành công");
            }
            return View(user);
        }
    }
}
