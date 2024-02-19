using Microsoft.AspNetCore.Mvc;
using ProductCrudMVC.Data;
using ProductCrudMVC.Models;

namespace ProductCrudMVC.Controllers
{
    public class CategoryController : Controller
    {
        private readonly AppDbContext _db;

        public CategoryController(AppDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<Category> categories = _db.Categories;
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
                    _db.Categories.Add(category);
                    _db.SaveChanges();
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
            Category? category = _db.Categories.Find(id);
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
                    _db.Categories.Update(category);
                    _db.SaveChanges();
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
                Category? category = _db.Categories.Find(id);
                if (category == null)
                {
                    return NotFound();
                }
                _db.Categories.Remove(category);
                _db.SaveChanges();
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
