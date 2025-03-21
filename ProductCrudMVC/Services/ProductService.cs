using ProductCrudMVC.Data;
using ProductCrudMVC.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace ProductCrudMVC.Services
{
    public class ProductService : IProductService
    {
        private readonly DbAccess _dbAccess;
        public ProductService()
        {
            _dbAccess = new DbAccess();
        }

        public IEnumerable<Product> GetProducts()
        {
            try
            {
                IEnumerable<Product> products = _dbAccess.GetResultList<Product>("sp_product", "getproducts");
                return products;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Product GetProduct(int id)
        {
            try
            {
                Product product = _dbAccess.GetResultSingle<Product>("sp_product", "getproduct", id);
                return product;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool InsertProduct(Product product, HttpPostedFileBase image)
        {
            try
            {
                if (image != null && image.ContentLength > 0)
                {
                    string fileName = DateTime.Now.ToString("yyMMddHHmmss") + "_" + Path.GetFileName(image.FileName);
                    string filePath = Path.Combine(HttpContext.Current.Server.MapPath("~/Images"), fileName);
                    image.SaveAs(filePath);
                    product.Image = fileName;
                }

                bool isInserted = _dbAccess.ExecuteCommand("sp_product", "insertproduct", product.Name, product.CategoryId, product.Description, product.Image, product.Price);
                return isInserted;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool UpdateProduct(Product product, HttpPostedFileBase image)
        {
            try
            {
                if (image != null && image.ContentLength > 0)
                {
                    string fileName = DateTime.Now.ToString("yyMMddHHmmss") + "_" + Path.GetFileName(image.FileName);
                    string filePath = Path.Combine(HttpContext.Current.Server.MapPath("~/Images"), fileName);
                    image.SaveAs(filePath);
                    product.Image = fileName;
                }

                bool isUpdated = _dbAccess.ExecuteCommand("sp_product", "updateproduct", product.Id, product.Name, product.CategoryId, product.Image, product.Description, product.Price);
                return isUpdated;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool DeleteProduct(int id)
        {
            try
            {
                bool isDeleted = _dbAccess.ExecuteCommand("sp_product", "deleteproduct", id);
                return isDeleted;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}