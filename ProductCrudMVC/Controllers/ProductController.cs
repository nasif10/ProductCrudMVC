using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProductCrudMVC.Models;
using ProductCrudMVC.Services;

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
            ViewBag.Categories = _productRepository.GetCategories().Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name });
            IEnumerable<Product> products = _productRepository.GetProducts();
            return View(products);
        }

        [HttpPost]
        public IActionResult Create([FromForm] Product product, IFormFile image)
        {
            try
            {
                _productRepository.InsertProduct(product, image);
                return Json(new { success = true, message = "Record created successfully!" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        public IActionResult Edit(int id)
        {
            Product? product = _productRepository.GetProduct(id);
            if (product == null)
            {
                return NotFound();
            }
            return Json(product);
        }

        [HttpPost]
        public IActionResult Edit([FromForm]Product product, IFormFile image)
        {
            try
            {
                _productRepository.UpdateProduct(product, image);
                return Json(new { success = true, message = "Record edited successfully!" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        public IActionResult Delete(int id)
        {
            try
            {
                _productRepository.DeleteProduct(id);
                TempData["Toast"] = "Toast('Success','Record deleted successfully', 'success')";
            }
            catch (Exception ex)
            {
                TempData["Toast"] = $"Toast('Error','{ex.Message}', 'error')";
            }

            return RedirectToAction("Index");
        }
    }
}
