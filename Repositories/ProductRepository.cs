using System.Collections.Generic;
using System.Data.SqlClient;

public class ProductRepository : IProductRepository 
{
    private readonly string connectionString = "Server=LAPTOP-R91MV32G;Database=TaskDB;Trusted_Connection=True;"; 

    public IEnumerable<ProductModel> GetAllProducts()
    {
        using (SqlConnection connection  = new SqlConnection(connectionString)) 
        {
            connection.Open();
            string sql = "SELECT CategoryID, ProductName, ProductPrice FROM Products"; 
            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    List<ProductModel> products = new List<ProductModel>(); 

                    while(reader.Read())
                    {
                        ProductModel model = new ProductModel();
                        model.CategoryID = (int)reader["CategoryID"];
                        model.ProductName = reader["ProductName"].ToString();
                        model.ProductPrice = (int)reader["ProductPrice"];
                        products.Add(model); 
                    }

                    return products; 
                }
            }
        }
    }

    public ProductModel GetProductById(int productId) 
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            string sql = "SELECT CategoryID, ProductName, ProductPrice FROM Products WHERE CategoryID = @productId"; 
            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@productId", productId);  
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if(reader.Read())
                    {
                        ProductModel model = new ProductModel();
                        model.CategoryID = (int)reader["CategoryID"];
                        model.ProductName = reader["ProductName"].ToString();
                        model.ProductPrice = (int)reader["ProductPrice"];
                        return model;
                    }
                    else
                    {
                        return null; 
                    }
                }
            }
        }
    }

    public void AddProduct(ProductModel product)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            string query = "INSERT INTO Products (CategoryID, ProductName, ProductPrice) VALUES (@CategoryID, @ProductName, @ProductPrice)";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@CategoryID" , product.CategoryID); 
                command.Parameters.AddWithValue("@ProductName", product.ProductName);
                command.Parameters.AddWithValue("@ProductPrice", product.ProductPrice);

                connection.Open();
                command.ExecuteNonQuery(); 
            }
        }
    }

    public void UpdateProduct(ProductModel product) 
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            string sql = "UPDATE Products SET ProductName = @ProductName, ProductPrice = @ProductPrice WHERE CategoryID = @CategoryID";
            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@CategoryID", product.CategoryID);
                command.Parameters.AddWithValue("@ProductName", product.ProductName);
                command.Parameters.AddWithValue("@ProductPrice", product.ProductPrice);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    } 

    public void DeleteProduct(int productId)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            string sql = "DELETE FROM Products WHERE CategoryID = @productId";
            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@productId", productId);
                command.ExecuteNonQuery(); 
            }
        }
    }
} 