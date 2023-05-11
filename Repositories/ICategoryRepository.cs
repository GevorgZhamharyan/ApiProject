using System.Collections.Generic;

public interface ICategoryRepository
{
    IEnumerable<CategoryModel> GetAllCategories();
    CategoryModel GetCategoryById(int categoryId);
    void AddCategory(CategoryModel category);
    void UpdateCategory(CategoryModel category);
    void DeleteCategory(int categoryId);  
} 