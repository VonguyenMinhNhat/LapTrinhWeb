using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VoNguyenMinhNhat_KTGK.Models;

namespace VoNguyenMinhNhat_KTGK.Controllers
{
    public class SinhViensController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SinhViensController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: SinhViens
        public async Task<IActionResult> Index()
        {
            var dbcontext = _context.SinhViens.Include(s => s.NganhHoc);
            return View(await dbcontext.ToListAsync());
        }

        // GET: SinhViens/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
                return NotFound();

            var sinhVien = await _context.SinhViens
                .Include(s => s.NganhHoc)
                .FirstOrDefaultAsync(m => m.MaSV == id);

            if (sinhVien == null)
                return NotFound();

            return View(sinhVien);
        }

        // GET: SinhViens/Create
        public IActionResult Create()
        {
            ViewData["MaNganh"] = new SelectList(_context.NganhHocs, "MaNganh", "MaNganh");
            return View();
        }

        // POST: SinhViens/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaSV,HoTen,GioiTinh,NgaySinh,Hinh,MaNganh")] SinhVien sinhVien, IFormFile ImageFile)
        {
            if (ModelState.IsValid)
            {
                if (ImageFile != null && ImageFile.Length > 0)
                {
                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(ImageFile.FileName);
                    var imageFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");

                    if (!Directory.Exists(imageFolder))
                        Directory.CreateDirectory(imageFolder);

                    var filePath = Path.Combine(imageFolder, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await ImageFile.CopyToAsync(stream);
                    }

                    sinhVien.Hinh = "/images/" + fileName;
                }

                _context.Add(sinhVien);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["MaNganh"] = new SelectList(_context.NganhHocs, "MaNganh", "MaNganh", sinhVien.MaNganh);
            return View(sinhVien);
        }

        // GET: SinhViens/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
                return NotFound();

            var sinhVien = await _context.SinhViens.FindAsync(id);
            if (sinhVien == null)
                return NotFound();

            ViewData["MaNganh"] = new SelectList(_context.NganhHocs, "MaNganh", "MaNganh", sinhVien.MaNganh);
            return View(sinhVien);
        }

        // POST: SinhViens/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("MaSV,HoTen,GioiTinh,NgaySinh,Hinh,MaNganh")] SinhVien sinhVien, IFormFile ImageFile)
        {
            if (id != sinhVien.MaSV)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    if (ImageFile != null && ImageFile.Length > 0)
                    {
                        var fileName = Guid.NewGuid().ToString() + Path.GetExtension(ImageFile.FileName);
                        var imageFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");

                        if (!Directory.Exists(imageFolder))
                            Directory.CreateDirectory(imageFolder);

                        var filePath = Path.Combine(imageFolder, fileName);

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await ImageFile.CopyToAsync(stream);
                        }

                        sinhVien.Hinh = "/images/" + fileName;
                    }

                    _context.Update(sinhVien);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SinhVienExists(sinhVien.MaSV))
                        return NotFound();
                    else
                        throw;
                }

                return RedirectToAction(nameof(Index));
            }

            ViewData["MaNganh"] = new SelectList(_context.NganhHocs, "MaNganh", "MaNganh", sinhVien.MaNganh);
            return View(sinhVien);
        }

        // GET: SinhViens/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
                return NotFound();

            var sinhVien = await _context.SinhViens
                .Include(s => s.NganhHoc)
                .FirstOrDefaultAsync(m => m.MaSV == id);

            if (sinhVien == null)
                return NotFound();

            return View(sinhVien);
        }

        // POST: SinhViens/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var sinhVien = await _context.SinhViens.FindAsync(id);
            if (sinhVien != null)
            {
                _context.SinhViens.Remove(sinhVien);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool SinhVienExists(string id)
        {
            return _context.SinhViens.Any(e => e.MaSV == id);
        }
    }
}
