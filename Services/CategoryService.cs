using System;
using System.Collections.Generic; 


namespace WebApplication.Services
{ 
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository categoryRepository; 
        public CategoryService(ICategoryRepository categoryRepository) 
        {
            this.categoryRepository = categoryRepository; 
        } 

        public IEnumerable<CategoryModel> GetAllCategories()
        {
            return categoryRepository.GetAllCategories();
        }

        public CategoryModel GetCategoryById(int id)
        {
            return categoryRepository.GetCategoryById(id);
        } 

        public void AddCategory(CategoryModel category) 
        {
            //We can perform any necessary validation before calling the repository method
            if (string.IsNullOrEmpty(category.CategoryName))
            {
                throw new ArgumentException("Category name cannot be empty!");
            } 

            categoryRepository.AddCategory(category); 
        }

        public void UpdateCategory(int id, CategoryModel category)
        {
            if (string.IsNullOrEmpty(category.Description))
            {
                throw new ArgumentException("Description cannot be empty!");
            }

            category.CategoryID = id;
            categoryRepository.UpdateCategory(category); 
        }

        public void DeleteCategory(int id) 
        {
            categoryRepository.DeleteCategory(id); 
        }

    }
}