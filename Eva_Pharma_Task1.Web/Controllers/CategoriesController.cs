using Microsoft.AspNetCore.Mvc;
using Eva_Pharma_Task1.DAL.Repositories;
using Eva_Pharma_Task1.Models;
using System.Threading.Tasks;

namespace Eva_Pharma_Task1.Web.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ICategoryRepository categoryRepository;

        public CategoriesController(ICategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }

        public async Task<IActionResult> Index()
        {
            var categories = await categoryRepository.GetAllAsync();
            return View("Index", categories);
        }

        public async Task<IActionResult> Details(int id)
        {
            var category = await categoryRepository.GetCategoryAsync(id);
            if (category == null)
                return NotFound();

            return View("Details", category);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View("Create");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Categories category)
        {
            if (ModelState.IsValid)
            {
                await categoryRepository.AddAsync(category);
                await categoryRepository.SaveAsync();
                return RedirectToAction("Index");
            }

            return View("Create", category);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var category = await categoryRepository.GetCategoryAsync(id);
            if (category == null)
                return NotFound();

            return View("Edit", category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Categories category)
        {
            if (ModelState.IsValid)
            {
                await categoryRepository.UpdateAsync(category);
                await categoryRepository.SaveAsync();
                return RedirectToAction(nameof(Index));
            }

            return View("Edit", category);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var category = await categoryRepository.GetCategoryAsync(id);
            if (category == null)
                return NotFound();

            return View("Delete", category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmDelete(int id)
        {
            var category = await categoryRepository.GetCategoryAsync(id);
            if (category == null)
                return NotFound();

            await categoryRepository.DeleteAsync(id);
            await categoryRepository.SaveAsync();
            return RedirectToAction("Index");
        }
        [HttpGet]
        [HttpGet]
        public async Task<IActionResult> Search(string query)
        {
            var allCategories = await categoryRepository.GetAllAsync();

            var filtered = string.IsNullOrWhiteSpace(query)
                ? allCategories
                : allCategories.Where(c =>
                    !string.IsNullOrEmpty(c.catName) &&
                    c.catName.Contains(query, StringComparison.OrdinalIgnoreCase));

            return Json(filtered.Select(c => new
            {
                id = c.Id,
                catName = c.catName,
                markedAsDeleted = c.markedAsDeleted
            }));
        }




    }
}
