using ProductCrudMVC.Models;

namespace ProductCrudMVC.Services.Interfaces
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetProducts();
        Product GetProduct(int? id);
        void InsertProduct(Product product, IFormFile image);
        void UpdateProduct(Product product, IFormFile image);
        void DeleteProduct(int? id);
        IEnumerable<Category> GetCategories();
    }
}
