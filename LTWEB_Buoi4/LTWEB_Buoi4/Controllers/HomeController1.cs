using LTWEB_Buoi4.Models;
using Microsoft.AspNetCore.Mvc;

namespace LTWEB_Buoi4.Controllers
{
    public class HomeController1 : Controller
    {
        public IActionResult Index()
        {
            var actor = new List<Actor>
            {
                new Actor {name = "Phương Anh Đào", height =100, Role="Mai"},
                new Actor {name = "Tuấn Trần", height =170, Role="Dương(Sâu)"},
                new Actor {name = "Trấn Thành", height =150, Role="Ông Hoàng"},
            };
            return View(actor);
        }
    }
}
