using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProductCrudMVC.Models;
using ProductCrudMVC.Services.Interfaces;

namespace ProductCrudMVC.Controllers
{
    public class ProductController : Controller
    {
        private IProductRepository _productRepository;

        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public IActionResult Index()
        {
            IEnumerable<Product> products = _productRepository.GetProducts();
            return View(products);
        }

        public IActionResult Create()
        {
            ViewBag.Categories = _productRepository.GetCategories().Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name });
            return View();
        }

        [HttpPost]
        public IActionResult Create(Product product, IFormFile image)
        {
            try
            {
                ViewBag.Categories = _productRepository.GetCategories().Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name });

                _productRepository.InsertProduct(product, image);
                TempData["msg"] = new string[] { "Success", "Record created successfully!", "success" };
                return RedirectToAction("Index");

            }
            catch (Exception ex)
            {
                TempData["msg"] = new string[] { "Error", ex.Message, "error" };
            }

            return View();
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Product? product = _productRepository.GetProduct(id);
            if (product == null)
            {
                return NotFound();
            }
            ViewBag.Categories = _productRepository.GetCategories().Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name });
            return View(product);
        }

        [HttpPost]
        public IActionResult Edit(Product product, IFormFile image)
        {
            try
            {
                ViewBag.Categories = _productRepository.GetCategories().Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name });
                
                _productRepository.UpdateProduct(product, image);
                TempData["msg"] = new string[] { "Success", "Record edited successfully!", "success" };
                return RedirectToAction("Index");
                
            }
            catch (Exception ex)
            {
                TempData["msg"] = new string[] { "Error", ex.Message, "error" };
            }

            return View();
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            try
            {
                _productRepository.DeleteProduct(id);
                TempData["msg"] = new string[] { "Success", "Record deleted successfully!", "success" };
            }
            catch (Exception ex)
            {
                TempData["msg"] = new string[] { "Error", ex.Message, "error" };
            }

            return RedirectToAction("Index");
        }
    }
}
