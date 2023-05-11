using System.Collections.Generic;
using System.Data.SqlClient; 

public class CategoryRepository : ICategoryRepository  
{
    private readonly string connectionString = "Server=LAPTOP-R91MV32G;Database=TaskDB;Trusted_Connection=True;";


    public IEnumerable<CategoryModel> GetAllCategories() 
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        { 
            connection.Open();
            string sql = "SELECT CategoryID, CategoryName, Description FROM Categories";
            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    List<CategoryModel> categories = new List<CategoryModel>();

                    while (reader.Read())
                    {
                        CategoryModel model = new CategoryModel();
                        model.CategoryID = (int)reader["CategoryID"];
                        model.CategoryName = reader["CategoryName"].ToString();
                        model.Description = reader["Description"].ToString();
                        categories.Add(model);
                    }

                    return categories;
                }
            }
        }
    } 

    public CategoryModel GetCategoryById(int categoryId)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            string sql = "SELECT CategoryID, CategoryName, Description FROM Categories WHERE CategoryID = @CategoryID";
            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@categoryId", categoryId);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if(reader.Read())
                    {
                        CategoryModel model = new CategoryModel();
                        model.CategoryID = (int)reader["CategoryID"];
                        model.CategoryName = reader["CategoryName"].ToString();
                        model.Description = reader["Description"].ToString();
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

    public void AddCategory(CategoryModel category)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            string query = "INSERT INTO Categories (CategoryID, CategoryName, Description) VALUES (@CategoryID, @CategoryName, @Description)";
            using (SqlCommand command = new SqlCommand(query , connection))
            {
                command.Parameters.AddWithValue("@CategoryID", category.CategoryID); 
                command.Parameters.AddWithValue("@CategoryName", category.CategoryName); 
                command.Parameters.AddWithValue("@Description", category.Description);

                connection.Open();
                command.ExecuteNonQuery(); 
            }
        }
    }

    public void UpdateCategory(CategoryModel category)
    {
        using (SqlConnection connection = new SqlConnection(connectionString)) 
        {
            string sql = "UPDATE Categories SET CategoryName = @CategoryName, Description = @Description WHERE CategoryID = @CategoryID";
            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@CategoryID", category.CategoryID); 
                command.Parameters.AddWithValue("@CategoryName", category.CategoryName); 
                command.Parameters.AddWithValue("@Description", category.Description);

                connection.Open(); 
                command.ExecuteNonQuery(); 
            }
        }
    }

    public void DeleteCategory(int categoryId)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            string sql = "DELETE FROM Categories WHERE CategoryID = @categoryId";
            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@categoryId", categoryId); 
                command.ExecuteNonQuery(); 
            }
        }
    }
} 