using Eva_Pharma_Task1.DAL;
using Eva_Pharma_Task1.DAL.Repositories;
using Eva_Pharma_Task1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using System.Threading.Tasks;

namespace Eva_Pharma_Task1.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var products = await _unitOfWork.Products.GetAllAsync();
            return View("Index", products);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var product = await _unitOfWork.Products.GetProductAsync(id);
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
                await _unitOfWork.Products.AddAsync(product);
                await _unitOfWork.SaveAsync();
                return RedirectToAction(nameof(Index));
            }

            await LoadCategoriesAsync();
            return View(product);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var product = await _unitOfWork.Products.GetProductAsync(id);
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
                await _unitOfWork.Products.UpdateAsync(product);
                await _unitOfWork.SaveAsync();
                return RedirectToAction(nameof(Index));
            }

            await LoadCategoriesAsync();
            return View("Edit", product);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _unitOfWork.Products.GetProductAsync(id);
            if (product == null)
                return NotFound();

            return View("Delete", product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _unitOfWork.Products.GetProductAsync(id);
            if (product == null)
                return NotFound();

            await _unitOfWork.Products.DeleteAsync(id);
            await _unitOfWork.SaveAsync();
            return RedirectToAction(nameof(Index));
        }

        private async Task LoadCategoriesAsync()
        {
            var categories = await _unitOfWork.Categories.GetAllAsync();
            ViewBag.Categories = categories
                .Where(c => !c.markedAsDeleted)
                .Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.catName
                }).ToList();
        }
    }
}
