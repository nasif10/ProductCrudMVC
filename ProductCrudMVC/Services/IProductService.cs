using ProductCrudMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ProductCrudMVC.Services
{
    public interface IProductService
    {
        IEnumerable<Product> GetProducts();
        Product GetProduct(int id);
        bool InsertProduct(Product product, HttpPostedFileBase image);
        bool UpdateProduct(Product product, HttpPostedFileBase image);
        bool DeleteProduct(int id);
    }
}
