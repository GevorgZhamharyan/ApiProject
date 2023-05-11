using System.Collections.Generic; 

public interface IProductRepository
{
    IEnumerable<ProductModel> GetAllProducts();
    ProductModel GetProductById(int productId);
    void AddProduct(ProductModel product);
    void UpdateProduct(ProductModel product);
    void DeleteProduct(int productId); 
} 