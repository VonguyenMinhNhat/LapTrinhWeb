using Microsoft.AspNetCore.Mvc;
using VoNguyenMinhNhat_KTGK.Models;
using Microsoft.EntityFrameworkCore;
using VoNguyenMinhNhat_KTGK.Models;

namespace VoMinhTam_Kiemtra.Controllers
{
    public class TaiKhoanController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TaiKhoanController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /TaiKhoan/Dan   gNhap
        public IActionResult DangNhap()
        {
            return View();
        }

        // POST: /TaiKhoan/DangNhap
        [HttpPost]
        public async Task<IActionResult> DangNhap(DangNhapViewModel model)
        {
            if (ModelState.IsValid)
            {
                var sv = await _context.SinhViens
                    .FirstOrDefaultAsync(s => s.MaSV == model.MaSV && s.NgaySinh == model.NgaySinh);

                if (sv != null)
                {
                    HttpContext.Session.SetString("MaSV", sv.MaSV);
                    HttpContext.Session.SetString("HoTen", sv.HoTen);
                    return RedirectToAction("Index", "SinhViens");
                }

                ViewBag.ThongBao = "Sai Mã SV hoặc Ngày Sinh!";
            }

           
            return View(model);
        }

        public IActionResult DangXuat()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("DangNhap");
        }
    }
}
