using ProductCrudMVC.Data;
using ProductCrudMVC.Models;

namespace ProductCrudMVC.Services
{
    public interface IProductService
    {
        IEnumerable<Product> GetProducts();
        Product GetProduct(int id);
        bool InsertProduct(Product product, IFormFile? image);
        bool UpdateProduct(Product product, IFormFile? image);
        bool DeleteProduct(int id);
    }

    public class ProductService : IProductService
    {
        private readonly DbAccess _dbAccess;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProductService(DbAccess dbAccess, IWebHostEnvironment webHostEnvironment)
        {
            _dbAccess = dbAccess;
            _webHostEnvironment = webHostEnvironment;
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

        public bool InsertProduct(Product product, IFormFile? image)
        {
            try
            {
                if (image != null && image.Length > 0)
                {
                    string fileName = DateTime.Now.ToString("yyMMddHHmmss") + "_" + Path.GetFileName(image.FileName);
                    string filePath = Path.Combine(_webHostEnvironment.WebRootPath, "images", fileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        image.CopyTo(stream);
                    }
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

        public bool UpdateProduct(Product product, IFormFile? image)
        {
            try
            {
                if (image != null && image.Length > 0)
                {
                    if (!string.IsNullOrEmpty(product.Image))
                    {
                        string filePath1 = Path.Combine(_webHostEnvironment.WebRootPath, "images", product.Image);
                        if (File.Exists(filePath1))
                        {
                            File.Delete(filePath1);
                        }
                    }

                    string fileName = DateTime.Now.ToString("yyMMddHHmmss") + "_" + Path.GetFileName(image.FileName);
                    string filePath = Path.Combine(_webHostEnvironment.WebRootPath, "images", fileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        image.CopyTo(stream);
                    }
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
                Product product = _dbAccess.GetResultSingle<Product>("sp_product", "getproduct", id);

                if (!string.IsNullOrEmpty(product.Image))
                {
                    string filePath1 = Path.Combine(_webHostEnvironment.WebRootPath, "images", product.Image);
                    if (File.Exists(filePath1))
                    {
                        File.Delete(filePath1);
                    }
                }

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
