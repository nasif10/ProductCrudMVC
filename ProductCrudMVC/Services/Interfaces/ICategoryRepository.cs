﻿using ProductCrudMVC.Models;

namespace ProductCrudMVC.Services.Interfaces
{
    public interface ICategoryRepository
    {
        IEnumerable<Category> GetCategories();
        Category GetCategory(int? id);
        void InsertCategory(Category category);
        void UpdateCategory(Category category);
        void DeleteCategory(int? id);
    }
}
