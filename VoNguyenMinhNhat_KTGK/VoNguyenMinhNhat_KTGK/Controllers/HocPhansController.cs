using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VoNguyenMinhNhat_KTGK.Models;

namespace VoNguyenMinhNhat_KTGK.Controllers
{
    public class HocPhansController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HocPhansController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Trang hiển thị danh sách học phần cho sinh viên chọn đăng ký (giống ảnh)
        public IActionResult ListHP()
        {   
            if (HttpContext.Session.GetString("MaSV") == null)
            {
                TempData["ThongBao"] = "Vui lòng đăng nhập để đăng ký học phần.";
                return RedirectToAction("DangNhap", "TaiKhoan");
            }

            var danhSach = _context.HocPhans.ToList();
            return View(danhSach); // Views/HocPhans/ListHP.cshtml
        }

        // Thêm học phần vào giỏ hàng (session)
        public IActionResult DangKy(string maHP)
        {
            if (HttpContext.Session.GetString("MaSV") == null)
                return RedirectToAction("DangNhap", "TaiKhoan");

            var gioHang = HttpContext.Session.GetObject<List<string>>("GioHang") ?? new List<string>();

            if (!gioHang.Contains(maHP))
            {
                gioHang.Add(maHP);
                HttpContext.Session.SetObject("GioHang", gioHang);
            }

            return RedirectToAction("GioHang", "GioHang");
        }
    }
}
