using System;
using System.Collections.Generic; 

namespace WebApplication.Services
{ 
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository; 

        public ProductService(IProductRepository _productRepository)
        {
            this._productRepository = _productRepository; 
        }

        public IEnumerable<ProductModel> GetAllProducts() 
        {
            return _productRepository.GetAllProducts(); 
        }

        public ProductModel GetProductById(int id)
        {
            return _productRepository.GetProductById(id); 
        }

        public void AddProduct(ProductModel product)
        {
            if(string.IsNullOrEmpty(product.ProductName))
            {
                throw new ArgumentException("Product Name cannot be empty!");  
            }

            _productRepository.AddProduct(product); 
        }

        public void UpdateProduct(int id, ProductModel product)
        {
            product.CategoryID = id;
            _productRepository.UpdateProduct(product); 
        }

        public void DeleteProduct(int id)
        {
            _productRepository.DeleteProduct(id); 
        }
    }
}