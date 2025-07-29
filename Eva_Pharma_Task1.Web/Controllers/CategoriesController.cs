using Microsoft.AspNetCore.Mvc;
using Eva_Pharma_Task1.DAL;
using Eva_Pharma_Task1.Models;
using System.Linq;
using System.Threading.Tasks;
using Eva_Pharma_Task1.DAL.Repositories;

namespace Eva_Pharma_Task1.Web.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoriesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Index()
        {
            var categories = await _unitOfWork.Categories.GetAllAsync();
            return View(categories);
        }

        public async Task<IActionResult> Details(int id)
        {
            var category = await _unitOfWork.Categories.GetCategoryAsync(id);
            if (category == null)
                return NotFound();

            return View(category);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Categories category)
        {
            if (ModelState.IsValid)
            {
                await _unitOfWork.Categories.AddAsync(category);
                await _unitOfWork.SaveAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(category);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var category = await _unitOfWork.Categories.GetCategoryAsync(id);
            if (category == null)
                return NotFound();

            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Categories category)
        {
            if (ModelState.IsValid)
            {
                await _unitOfWork.Categories.UpdateAsync(category);
                await _unitOfWork.SaveAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(category);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var category = await _unitOfWork.Categories.GetCategoryAsync(id);
            if (category == null)
                return NotFound();

            return View(category);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var category = await _unitOfWork.Categories.GetCategoryAsync(id);
            if (category == null)
                return NotFound();

            await _unitOfWork.Categories.DeleteAsync(id);
            await _unitOfWork.SaveAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Search(string query)
        {
            var allCategories = await _unitOfWork.Categories.GetAllAsync();

            var filtered = string.IsNullOrWhiteSpace(query)
                ? allCategories
                : allCategories.Where(c =>
                    !string.IsNullOrWhiteSpace(c.catName) &&
                    c.catName.Contains(query, System.StringComparison.OrdinalIgnoreCase));

            return Json(filtered.Select(c => new
            {
                c.Id,
                c.catName,
                c.markedAsDeleted
            }));
        }
    }
}
