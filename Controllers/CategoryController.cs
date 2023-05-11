using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections.Generic;
using System; 
using Microsoft.AspNetCore.Http;
using System.Linq;

namespace MyNamespace.Controllers 
{
    [ApiController] 
    [Route("[controller]")]   
    public class CategoryController : Controller  
    {
        private readonly ICategoryRepository categoryRepository; 


        public CategoryController(ICategoryRepository categoryRepository) 
        {
            this.categoryRepository = categoryRepository; 
        }

        [HttpGet]
        public ActionResult<IEnumerable<CategoryModel>> Get()
        {
            try
            {
                var categories = categoryRepository.GetAllCategories();

                if (categories == null || categories.Count() == 0) 
                {
                    return NotFound("No categories found.");
                }

                return Ok(categories);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return StatusCode(500, "An error occurred while retrieving categories.");
            } 
        }

        [HttpGet("{id}")]
        public ActionResult<CategoryModel> Get(int id)
        {
            try
            {
                var category = categoryRepository.GetCategoryById(id);

                if (category == null)
                {
                    return NotFound($"Category with ID {id} not found.");
                }

                return category;
            }
            catch (Exception ex)  
            {
                Console.WriteLine(ex.Message); 
                return StatusCode(500, "Internal server error");
            }
        } 

        [HttpPost]
        public ActionResult Post([FromBody] CategoryModel category)
        {
            try
            {
                categoryRepository.AddCategory(category);
                return Ok("Record created successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest("Error creating record: " + ex.Message);
            }
        } 

        [HttpPut("{id}")] 
        public ActionResult Put(int id, [FromBody] CategoryModel category)
        {
            try
            {
                var existingCategory = categoryRepository.GetCategoryById(id);
                if (existingCategory == null)
                {
                    return NotFound($"Category with ID {id} not found.");
                }

                category.CategoryID = id;
                categoryRepository.UpdateCategory(category);

                return Ok($"Category with ID {id} updated successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while updating the category: {ex.Message}");
            } 
        }

        [HttpDelete("{id}")] 
        public ActionResult Delete(int id)
        {
            try
            {
                categoryRepository.DeleteCategory(id);
                return Ok("Category deleted successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);  
            }
        }

    }
}