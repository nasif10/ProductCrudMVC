using Microsoft.AspNetCore.Mvc;
using ProductCrudMVC.Models;
using ProductCrudMVC.Services.Interfaces;

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

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category category)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _categoryRepository.InsertCategory(category);
                    TempData["msg"] = new string[] { "Success", "Record created successfully!", "success" };
                    return RedirectToAction("Index");
                }
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
            Category? category = _categoryRepository.GetCategory(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        [HttpPost]
        public IActionResult Edit(Category category)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _categoryRepository.UpdateCategory(category);
                    TempData["msg"] = new string[] { "Success", "Record edited successfully!", "success" };
                    return RedirectToAction("Index");
                }
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
                _categoryRepository.DeleteCategory(id);
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
