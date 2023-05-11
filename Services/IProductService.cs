using System.Collections.Generic; 

namespace WebApplication.Services
{
    public interface IProductService
    {
        IEnumerable<ProductModel> GetAllProducts(); 
        ProductModel GetProductById(int id); 
        void AddProduct(ProductModel product);
        void UpdateProduct(int id, ProductModel product);
        void DeleteProduct(int id); 
    }
} 