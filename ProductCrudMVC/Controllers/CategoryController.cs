using ProductCrudMVC.Models;
using ProductCrudMVC.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProductCrudMVC.Controllers
{
    public class CategoryController : Controller
    {
        private ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetCategories()
        {
            try
            {
                IEnumerable<Category> categories = _categoryService.GetCategories();
                return Json(categories, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
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
                return Json(category, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult CreateCategory(Category category)
        {
            try
            {
                if (!ModelState.IsValid)
                    throw new InvalidOperationException("Invalid model state!");

                bool isInserted = _categoryService.InsertCategory(category);
                if (isInserted)
                    return Json(new { success = true, message = "Record created successfully." }, JsonRequestBehavior.AllowGet);
                else
                    return Json(new { success = false, message = "Failed to create the record." }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult UpdateCategory(Category category)
        {
            try
            {
                if (!ModelState.IsValid)
                    throw new InvalidOperationException("Invalid model state!");

                bool isUpdated = _categoryService.UpdateCategory(category);
                if (isUpdated)
                    return Json(new { success = true, message = "Record updated successfully." }, JsonRequestBehavior.AllowGet);
                else
                    return Json(new { success = false, message = "Failed to update the record." }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult DeleteCategory(int id)
        {
            try
            {
                bool isDeleted = _categoryService.DeleteCategory(id);
                if (isDeleted)
                    return Json(new { success = true, message = "Record deleted successfully." }, JsonRequestBehavior.AllowGet);
                else
                    return Json(new { success = false, message = "Failed to delete the record." }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}