using Microsoft.AspNetCore.Mvc;
using ProductCrudMVC.Models;
using ProductCrudMVC.Services;

namespace ProductCrudMVC.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public JsonResult GetCategories()
        {
            try
            {
                IEnumerable<Category> categories = _categoryService.GetCategories();
                return Json(new { success = true, data = categories });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        public JsonResult GetCategory(int id)
        {
            try
            {
                Category category = _categoryService.GetCategory(id);

                if (category == null)
                {
                    return Json(null);
                }
                return Json(new { success = true, data = category });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        public JsonResult CreateCategory(Category category)
        {
            try
            {
                bool isInserted = _categoryService.InsertCategory(category);

                if (isInserted)
                    return Json(new { success = true, message = "Record created successfully." });
                else
                    return Json(new { success = false, message = "Failed to create the record." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        public JsonResult UpdateCategory([FromBody] Category category)
        {
            try
            {
                bool isUpdated = _categoryService.UpdateCategory(category);

                if (isUpdated)
                    return Json(new { success = true, message = "Record updated successfully." });
                else
                    return Json(new { success = false, message = "Failed to update the record." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        public JsonResult DeleteCategory(int id)
        {
            try
            {
                bool isDeleted = _categoryService.DeleteCategory(id);

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
