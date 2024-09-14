using Microsoft.EntityFrameworkCore;
using ProductCrudMVC.Data;
using ProductCrudMVC.Models;

namespace ProductCrudMVC.Services
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _db;
        private readonly IWebHostEnvironment webHostEnvironment;

        public ProductRepository(AppDbContext db, IWebHostEnvironment webHostEnvironment)
        {
            _db = db;
            this.webHostEnvironment = webHostEnvironment;
        }

        public IEnumerable<Product> GetProducts()
        {
            return _db.Products.Include(p => p.Category);
        }

        public IEnumerable<Category> GetCategories()
        {
            return _db.Categories;
        }

        public Product? GetProduct(int id)
        {
            return _db.Products.Find(id);
        }

        public void InsertProduct(Product product, IFormFile image)
        {
            if (image != null && image.Length > 0)
            {
                if (image.Length > 500 * 1024)
                {
                    throw new Exception("Image size exceeds 500 KB.");
                }
                string fileName = $"{DateTime.Now.ToString("yyMMddHHmmss")}_{Path.GetFileName(image.FileName)}";
                var filePath = Path.Combine(webHostEnvironment.WebRootPath, "Images", fileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    image.CopyTo(stream);
                }
                product.Image = fileName;
            }

            _db.Products.Add(product);
            _db.SaveChanges();
        }

        public void UpdateProduct(Product product, IFormFile image)
        {
            Product? existingProduct = _db.Products.Find(product.Id);

            if (image == null)
            {
                product.Image = existingProduct.Image;
            }
            else
            {
                if (image.Length > 500 * 1024)
                {
                    throw new Exception("Image size exceeds 500 KB.");
                }

                if (!string.IsNullOrEmpty(existingProduct.Image))
                {
                    string prevFilePath = Path.Combine(webHostEnvironment.WebRootPath, "images", existingProduct.Image);
                    if (File.Exists(prevFilePath))
                    {
                        File.Delete(prevFilePath);
                    }
                }

                string fileName = $"{DateTime.Now.ToString("yyMMddHHmmss")}_{Path.GetFileName(image.FileName)}";
                var filePath = Path.Combine(webHostEnvironment.WebRootPath, "images", fileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    image.CopyTo(stream);
                }
                product.Image = fileName;
            }
            _db.Entry(existingProduct).CurrentValues.SetValues(product);
            _db.SaveChanges();
        }

        public void DeleteProduct(int id)
        {
            Product? product = _db.Products.Find(id);
            if (product != null)
            {
                _db.Products.Remove(product);
                _db.SaveChanges();
            }
        }
    }
}
