using Microsoft.AspNetCore.Mvc;
using VoNguyenMinhNhat_KTGK.Models;
using Microsoft.AspNetCore.Http;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;

namespace VoNguyenMinhNhat_KTGK.Controllers
{
    public class GioHangController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GioHangController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Hiển thị giỏ hàng
        public IActionResult GioHang()
        {
            var gioHang = HttpContext.Session.GetObject<List<string>>("GioHang") ?? new List<string>();
            var dsHP = _context.HocPhans.Where(hp => gioHang.Contains(hp.MaHP)).ToList();

            ViewBag.SoLuong = dsHP.Count;
            ViewBag.TongTinChi = dsHP.Sum(hp => hp.SoTinChi);
            return View(dsHP);
        }

        // Thêm học phần vào giỏ
        public IActionResult Them(string maHP)
        {
            var gioHang = HttpContext.Session.GetObject<List<string>>("GioHang") ?? new List<string>();
            if (!gioHang.Contains(maHP))
                gioHang.Add(maHP);

            HttpContext.Session.SetObject("GioHang", gioHang);
            return RedirectToAction("GioHang");
        }

        // Xóa một học phần
        public IActionResult Xoa(string maHP)
        {
            var gioHang = HttpContext.Session.GetObject<List<string>>("GioHang") ?? new List<string>();
            gioHang.Remove(maHP);
            HttpContext.Session.SetObject("GioHang", gioHang);
            return RedirectToAction("GioHang");
        }

        // Xóa tất cả học phần
        public IActionResult XoaTatCa()
        {
            HttpContext.Session.Remove("GioHang");
            return RedirectToAction("GioHang");
        }

        // Hiển thị trang xác nhận
        public IActionResult DatHang()
        {
            var maSV = HttpContext.Session.GetString("MaSV");
            if (maSV == null)
                return RedirectToAction("DangNhap", "TaiKhoan");

            var sv = _context.SinhViens.Include(s => s.NganhHoc).FirstOrDefault(s => s.MaSV == maSV);
            var gioHang = HttpContext.Session.GetObject<List<string>>("GioHang") ?? new List<string>();
            var dsHP = _context.HocPhans.Where(hp => gioHang.Contains(hp.MaHP)).ToList();

            ViewBag.SinhVien = sv;
            ViewBag.DanhSachHP = dsHP;
            return View();
        }

        // Xác nhận đăng ký -> lưu vào DB
        [HttpPost]
        public async Task<IActionResult> XacNhanDatHang()
        {
            var maSV = HttpContext.Session.GetString("MaSV");
            if (maSV == null)
                return RedirectToAction("DangNhap", "TaiKhoan");

            var gioHang = HttpContext.Session.GetObject<List<string>>("GioHang") ?? new List<string>();
            if (gioHang.Count == 0) return RedirectToAction("GioHang");

            var dk = new DangKy
            {
                MaSV = maSV,
                NgayDK = DateTime.Now
            };
            _context.DangKys.Add(dk);
            await _context.SaveChangesAsync();

            foreach (var maHP in gioHang)
            {
                var ct = new ChiTietDangKy
                {
                    MaDK = dk.MaDK,
                    MaHP = maHP
                };
                _context.ChiTietDangKys.Add(ct);
            }

            await _context.SaveChangesAsync();
            HttpContext.Session.Remove("GioHang");

            return RedirectToAction("XacNhanDonHang");
        }

        public IActionResult XacNhanDonHang()
        {
            return View();
        }
    }

    // Extension methods
    public static class SessionExtensions
    {
        public static void SetObject<T>(this ISession session, string key, T value)
        {
            session.SetString(key, JsonSerializer.Serialize(value));
        }

        public static T GetObject<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default : JsonSerializer.Deserialize<T>(value);
        }
    }
}
