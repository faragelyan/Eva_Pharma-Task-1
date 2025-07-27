using Eva_Pharma_Task1.DAL.Repositories;
using Eva_Pharma_Task1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

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
        public async Task<IActionResult> Details(int ID)
        {
            var product = await productRepository.GetProductAsync(ID);
            if (product == null)
            {
                return NotFound();
            }
            return View("Details", product);
        }

        [HttpPost]
        public async Task<IActionResult> Create()
        {
            var categories = await categoryRepository.GetAllAsync();

            ViewBag.Categories = categories.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.catName
            }).ToList();

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

            var categories = await categoryRepository.GetAllAsync();

            ViewBag.Categories = categories.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.catName
            }).ToList();

            return View(product);
        }
        
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var product = await productRepository.GetProductAsync(id);
            if (product == null)
                return NotFound();

            var categories = await categoryRepository.GetAllAsync();
            ViewBag.Categories = categories.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.catName
            }).ToList();

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

            var categories = await categoryRepository.GetAllAsync();
            ViewBag.Categories = categories.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.catName
            }).ToList();

            return View("Edit", product);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var product = await productRepository.GetProductAsync(id);
            if (product == null)
                return NotFound();

            return View("Delete", product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmDelete(int id)
        {
            var product = await productRepository.GetProductAsync(id);
            if (product == null)
                return NotFound();

            await productRepository.DeleteAsync(id);
            await productRepository.SaveAsync();
            return RedirectToAction("Index");
        }
    }
}
