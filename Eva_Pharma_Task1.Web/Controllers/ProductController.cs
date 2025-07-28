using Eva_Pharma_Task1.DAL.Repositories;
using Eva_Pharma_Task1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;
using System.Linq;

namespace Eva_Pharma_Task1.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository productRepository;
        private readonly ICategoryRepository categoryRepository;

        public ProductController(IProductRepository productRepository, ICategoryRepository categoryRepository)
        {
            this.productRepository = productRepository;
            this.categoryRepository = categoryRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var products = await productRepository.GetAllAsync();
            return View("Index", products);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var product = await productRepository.GetProductAsync(id);
            if (product == null)
                return NotFound();

            return View("Details", product);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            await LoadCategoriesAsync();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product product)
        {
            if (ModelState.IsValid)
            {
                await productRepository.AddAsync(product);
                await productRepository.SaveAsync();
                return RedirectToAction(nameof(Index));
            }

            await LoadCategoriesAsync();
            return View(product);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var product = await productRepository.GetProductAsync(id);
            if (product == null)
                return NotFound();

            await LoadCategoriesAsync();
            return View("Edit", product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                await productRepository.UpdateAsync(product);
                await productRepository.SaveAsync();
                return RedirectToAction(nameof(Index));
            }

            await LoadCategoriesAsync();
            return View("Edit", product);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await productRepository.GetProductAsync(id);
            if (product == null)
                return NotFound();

            return View("Delete", product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await productRepository.GetProductAsync(id);
            if (product == null)
                return NotFound();

            await productRepository.DeleteAsync(id);
            await productRepository.SaveAsync();
            return RedirectToAction(nameof(Index));
        }


        private async Task LoadCategoriesAsync()
        {
            var categories = await categoryRepository.GetAllAsync();
            ViewBag.Categories = categories.Where(c=>!c.markedAsDeleted).Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.catName
            }).ToList();
        }
    }
}
