using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace MyNamespace.Controllers
{
    [ApiController] 
    [Route("[controller]")] 
    public class ProductController : Controller 
    {
        private readonly IProductRepository productRepository; 


        public ProductController(IProductRepository productRepository)
        {
            this.productRepository = productRepository; 
        }


        [HttpGet]
        public ActionResult<IEnumerable<ProductModel>> Get()
        {
            try
            {
                var products = productRepository.GetAllProducts(); 

                if (products == null || products.Count() == 0)
                {
                    return NotFound("No products found.");
                }

                return Ok(products); 
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return StatusCode(500, "An error occurred while retrieving products."); 
            }
        }


        [HttpGet("{id}")]
        public ActionResult<ProductModel> Get(int id)
        {
            try
            {
                var product = productRepository.GetProductById(id);

                if (product == null)
                {
                    return NotFound($"Product with ID {id} not found.");
                }

                return product; 
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, "Internal server error");
            }
        }


        [HttpPost]
        public ActionResult Post([FromBody] ProductModel product) 
        {
            try
            {
                productRepository.AddProduct(product);
                return Ok("Record created successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest("Error creating record: " + ex.Message);
            }
        }


        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] ProductModel product) 
        {
            try
            {
                var existingProduct = productRepository.GetProductById(id);
                if (existingProduct == null) 
                {
                    return NotFound($"Product with ID {id} not found.");
                }

                product.CategoryID = id;
                productRepository.UpdateProduct(product); 

                return Ok($"Product with ID {id} updated successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while updating the product: {ex.Message}"); 
            }
        }


        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                productRepository.DeleteProduct(id);
                return Ok("Product deleted successfully."); 
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}