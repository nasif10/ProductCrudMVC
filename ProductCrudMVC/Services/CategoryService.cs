using ProductCrudMVC.Data;
using ProductCrudMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProductCrudMVC.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly DbAccess _dbAccess;
        public CategoryService()
        {
            _dbAccess = new DbAccess();
        }

        public IEnumerable<Category> GetCategories()
        {
            try
            {
                IEnumerable<Category> categories = _dbAccess.GetResultList<Category>("sp_category", "getcategories");
                return categories;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Category GetCategory(int id)
        {
            try
            {
                Category candidate = _dbAccess.GetResultSingle<Category>("sp_category", "getcategory", id);
                return candidate;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool InsertCategory(Category category)
        {
            try
            {
                bool isInserted = _dbAccess.ExecuteCommand("sp_category", "insertcategory", category.Name);
                return isInserted;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool UpdateCategory(Category category)
        {
            try
            {
                bool isUpdated = _dbAccess.ExecuteCommand("sp_category", "updatecategory", category.Id, category.Name);
                return isUpdated;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool DeleteCategory(int id)
        {
            try
            {
                bool isDeleted = _dbAccess.ExecuteCommand("sp_category", "deletecategory", id);
                return isDeleted;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}