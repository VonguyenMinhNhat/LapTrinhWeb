using Microsoft.AspNetCore.Mvc;
using VoNguyenMinhNhat_LTWEB_buoi3.Models;
using VoNguyenMinhNhat_LTWEB_buoi3.Repositories;

namespace VoNguyenMinhNhat_LTWEB_buoi3.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        public CategoriesController(IProductRepository productRepository, ICategoryRepository
        categoryRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
        }
        public async Task<IActionResult> Index()
        {
            var categories = await _categoryRepository.GetAllAsync();
            return View(categories);
        }

        public async Task<IActionResult> Display(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }
        [HttpGet]
        [HttpGet]
        public IActionResult Add()
        {
            return View("AddCategory"); // Đổi từ "Add" sang "AddCategory"
        }


        [HttpPost]
        public async Task<IActionResult> Add(Category category)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Dữ liệu nhập không hợp lệ.");
                return View("AddCategory"); // Đảm bảo hiển thị đúng file
            }

            await _categoryRepository.AddAsync(category);
            return RedirectToAction(nameof(Index));
        }



        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id, Category category)
        {
            if (id != category.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(category);
            }

            await _categoryRepository.UpdateAsync(category);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }
        [HttpPost, ActionName("DeleteConfirmed")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            var products = await _productRepository.GetByCategoryIdAsync(id);
            foreach (var product in products)
            {
                product.CategoryId = null;
                await _productRepository.UpdateAsync(product);
            }

            await _categoryRepository.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }



    }
}
