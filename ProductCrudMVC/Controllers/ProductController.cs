using Microsoft.AspNetCore.Mvc;
using ProductCrudMVC.Models;
using ProductCrudMVC.Services;

namespace ProductCrudMVC.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public JsonResult GetProducts()
        {
            try
            {
                IEnumerable<Product> products = _productService.GetProducts();
                return Json(new { success = true, data = products });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        public JsonResult GetProduct(int id)
        {
            try
            {
                Product product = _productService.GetProduct(id);
                return Json(new { success = true, data = product });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        public JsonResult CreateProduct(Product product, IFormFile? image)
        {
            try
            {
                bool isInserted = _productService.InsertProduct(product, image);

                if (isInserted)
                    return Json(new { success = true, message = "Record created successfully!" });
                else
                    return Json(new { success = false, message = "Failed to create the record." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        public JsonResult UpdateProduct(Product product, IFormFile? image)
        {
            try
            {
                bool isUpdated = _productService.UpdateProduct(product, image);

                if (isUpdated)
                    return Json(new { success = true, message = "Record updated successfully!" });
                else
                    return Json(new { success = false, message = "Failed to update the record." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        public JsonResult DeleteProduct(int id)
        {
            try
            {
                bool isDeleted = _productService.DeleteProduct(id);

                if (isDeleted)
                    return Json(new { success = true, message = "Record deleted successfully." });
                else
                    return Json(new { success = false, message = "Failed to delete the record." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }
    }
}
