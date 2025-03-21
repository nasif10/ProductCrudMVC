using ProductCrudMVC.Models;
using ProductCrudMVC.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProductCrudMVC.Controllers
{
    public class ProductController : Controller
    {
        private IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetProducts()
        {
            try
            {
                IEnumerable<Product> products = _productService.GetProducts();
                return Json(products, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetProduct(int id)
        {
            try
            {
                Product product = _productService.GetProduct(id);
                return Json(product, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult CreateProduct(Product product, HttpPostedFileBase image)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    throw new InvalidOperationException("Invalid model state!");
                }

                bool isInserted = _productService.InsertProduct(product, image);
                if (isInserted)
                    return Json(new { success = true, message = "Record created successfully!" }, JsonRequestBehavior.AllowGet);
                else
                    return Json(new { success = false, message = "Failed to create the record." }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult UpdateProduct(Product product, HttpPostedFileBase image)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    throw new InvalidOperationException("Invalid model state!");
                }

                bool isUpdated = _productService.UpdateProduct(product, image);
                if (isUpdated)
                    return Json(new { success = true, message = "Record updated successfully!" }, JsonRequestBehavior.AllowGet);
                else
                    return Json(new { success = false, message = "Failed to update the record." }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult DeleteProduct(int id)
        {
            try
            {
                bool isDeleted = _productService.DeleteProduct(id);
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