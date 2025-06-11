using Microsoft.AspNetCore.Mvc;
using VoNguyenMinhNhat_KTGK.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace VoNguyenMinhNhat_KTGK.Controllers
{
    public class DangKyController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DangKyController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /DangKy/DangKyHocPhan
        public IActionResult DangKyHocPhan()
        {
            if (HttpContext.Session.GetString("MaSV") == null)
            {
                return RedirectToAction("DangNhap", "TaiKhoan");
            }

            var danhSachHP = _context.HocPhans.ToList();
            return View("ListHP", danhSachHP);
        }

        [HttpPost]
        public async Task<IActionResult> DangKyHocPhan(List<string> selectedHPs)
        {
            var maSV = HttpContext.Session.GetString("MaSV");
            if (maSV == null) return RedirectToAction("DangNhap", "TaiKhoan");

            var dangKy = new DangKy
            {
                NgayDK = DateTime.Now,
                MaSV = maSV
            };
            _context.DangKys.Add(dangKy);
            await _context.SaveChangesAsync();

            foreach (var maHP in selectedHPs)
            {
                var chiTiet = new ChiTietDangKy
                {
                    MaDK = dangKy.MaDK,
                    MaHP = maHP
                };
                _context.ChiTietDangKys.Add(chiTiet);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("DanhSachDaDangKy");
        }

        public IActionResult DanhSachDaDangKy()
        {
            var maSV = HttpContext.Session.GetString("MaSV");
            var ds = _context.DangKys
                .Include(d => d.ChiTietDangKys)
                .ThenInclude(c => c.HocPhan)
                .Where(d => d.MaSV == maSV)
                .ToList();

            return View(ds);
        }
    }
}
