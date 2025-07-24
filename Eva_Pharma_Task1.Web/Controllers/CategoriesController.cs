using Microsoft.AspNetCore.Mvc;
using Eva_Pharma_Task1.DAL.Repositories;
using Eva_Pharma_Task1.Models;

namespace Eva_Pharma_Task1.Web.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ICategoryRepository categoryRepository;

        public CategoriesController(ICategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }

        public IActionResult Index()
        {
            var categories = categoryRepository.GetAll();
            return View("Index", categories);
        }

        public IActionResult Details(int id)
        {
            var category = categoryRepository.GetCategory(id);
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
        public IActionResult Create(Categories category)
        {
            if (ModelState.IsValid)
            {
                categoryRepository.Add(category);
                categoryRepository.Save();
                return RedirectToAction("Index");
            }

            return View("Create", category);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var category = categoryRepository.GetCategory(id);
            if (category == null)
                return NotFound();

            return View("Edit", category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Categories category)
        {
            if (ModelState.IsValid)
            {
                categoryRepository.Update(category);
                categoryRepository.Save();
                return RedirectToAction(nameof(Index));
            }

            return View("Edit", category);
        }


        [HttpGet]
        public IActionResult Delete(int id)
        {
            var category = categoryRepository.GetCategory(id);
            if (category == null)
                return NotFound();

            return View("Delete", category);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ConfirmDelete(int id)
        {
            var category = categoryRepository.GetCategory(id);
            if (category == null)
                return NotFound();

            categoryRepository.Delete(id);
            categoryRepository.Save();
            return RedirectToAction("Index");
        }

        
    }
}
