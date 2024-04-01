using ProductCrudMVC.Data;
using ProductCrudMVC.Models;
using ProductCrudMVC.Services.Interfaces;

namespace ProductCrudMVC.Services.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AppDbContext _db;

        public CategoryRepository(AppDbContext db)
        {
            _db = db;
        }

        public IEnumerable<Category> GetCategories()
        {
            return _db.Categories;
        }

        public Category GetCategory(int? id)
        {
            return _db.Categories.Find(id);
        }

        public void InsertCategory(Category category)
        {
            _db.Categories.Add(category);
            _db.SaveChanges();
        }

        public void UpdateCategory(Category category)
        {
            _db.Categories.Update(category);
            _db.SaveChanges();
        }

        public void DeleteCategory(int? id)
        {
            Category? category = _db.Categories.Find(id);
            if (category != null)
            {
                _db.Categories.Remove(category);
                _db.SaveChanges();
            }
        }
    }
}
