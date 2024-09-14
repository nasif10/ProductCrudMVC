using Microsoft.AspNetCore.Mvc;
using ProductCrudMVC.Models;
using ProductCrudMVC.Services;

namespace ProductCrudMVC.Controllers
{
    public class CategoryController : Controller
    {
        private ICategoryRepository _categoryRepository;

        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public IActionResult Index()
        {
            IEnumerable<Category> categories = _categoryRepository.GetCategories();
            return View(categories);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Category category)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _categoryRepository.InsertCategory(category);
                    return Json(new { success = true, message = "Record created successfully!" });
                }
                else
                {
                    var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                    return Json(new { success = false, message = "Validation failed.", errors = errors });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        public IActionResult Edit(int id)
        {
            Category? category = _categoryRepository.GetCategory(id);
            if (category == null)
            {
                return NotFound();
            }
            return Json(category);
        }

        [HttpPost]
        public IActionResult Edit([FromBody] Category category)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _categoryRepository.UpdateCategory(category);
                    return Json(new { success = true, message = "Record edited successfully!" });
                }
                else
                {
                    var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                    return Json(new { success = false, message = "Validation failed.", errors = errors });
                }
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
                _categoryRepository.DeleteCategory(id);
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
