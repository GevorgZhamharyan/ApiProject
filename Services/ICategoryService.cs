using System.Collections.Generic;

namespace WebApplication.Services
{
    public interface ICategoryService 
    {
        IEnumerable<CategoryModel> GetAllCategories(); 
        CategoryModel GetCategoryById(int id); 
        void AddCategory(CategoryModel category);
        void UpdateCategory(int id, CategoryModel category);
        void DeleteCategory(int id); 
    }
} 