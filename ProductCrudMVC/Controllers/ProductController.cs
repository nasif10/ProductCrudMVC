using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProductCrudMVC.Data;
using ProductCrudMVC.Models;

namespace ProductCrudMVC.Controllers
{
    public class ProductController : Controller
    {
        private readonly AppDbContext _db;

        public ProductController(AppDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<Product> products = _db.Products.Include(p => p.Category);
            return View(products);
        }

        public IActionResult Create()
        {
            ViewBag.Categories = _db.Categories.Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name });
            return View();
        }

        [HttpPost]
        public IActionResult Create(Product product)
        {
            try
            {
                ViewBag.Categories = _db.Categories.Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name });
                if (ModelState.IsValid)
                {
                    _db.Products.Add(product);
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
            Product? product = _db.Products.Find(id);
            if (product == null)
            {
                return NotFound();
            }
            ViewBag.Categories = _db.Categories.Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name });
            return View(product);
        }

        [HttpPost]
        public IActionResult Edit(Product product)
        {
            try
            {
                ViewBag.Categories = _db.Categories.Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name });
                if (ModelState.IsValid)
                {
                    _db.Products.Update(product);
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
                Product? product = _db.Products.Find(id);
                if (product == null)
                {
                    return NotFound();
                }
                if (ModelState.IsValid)
                {
                    _db.Products.Remove(product);
                    _db.SaveChanges();
                    TempData["msg"] = new string[] { "Success", "Record deleted successfully!", "success" };
                }
            }
            catch (Exception ex)
            {
                TempData["msg"] = new string[] { "Error", ex.Message, "error" };
            }

            return RedirectToAction("Index");
        }
    }
}
