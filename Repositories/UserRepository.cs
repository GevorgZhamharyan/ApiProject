using System.Data.SqlClient;

public class UserRepository : IUserRepository
{
    private readonly string connectionString = "Server=LAPTOP-R91MV32G;Database=TaskDB;Trusted_Connection=True;";

    public UserModel GetByUsernameAndPassword(string username, string password)
    {
        using (var connection = new SqlConnection(connectionString))
        {
            connection.Open(); 
            var query = "SELECT * FROM Users WHERE Username=@Username AND Password=@Password";
            using (var command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Username", username);
                command.Parameters.AddWithValue("@Password", password);

                using (var reader = command.ExecuteReader()) 
                {
                    if (reader.Read())
                    {
                        var user = new UserModel
                        {
                            Username = (string)reader["Username"],
                            Password = (string)reader["Password"],
                            Email = (string)reader["Email"] 
                        };
                        return user;
                    }
                }
            }
        }

        return null;
    }

    public UserModel GetByUsername(string username)
    {
        using (var connection = new SqlConnection(connectionString))
        {
            connection.Open();
            var query = "SELECT * FROM Users WHERE Username=@Username";
            using (var command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Username", username);

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        var user = new UserModel
                        {
                            Username = (string)reader["Username"],
                            Password = (string)reader["Password"],
                            Email = (string)reader["Email"]
                        };
                        return user;
                    }
                }
            }
        }

        return null;
    }

    public UserModel GetByEmail(string email)
    {
        using (var connection = new SqlConnection(connectionString))
        {
            connection.Open();
            var query = "SELECT * FROM Users WHERE Email=@Email";
            using (var command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Email", email);

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        var user = new UserModel
                        {
                            Username = (string)reader["Username"],
                            Password = (string)reader["Password"],
                            Email = (string)reader["Email"]
                        };
                        return user;
                    }
                }
            }
        }

        return null;
    }

    public void Create(UserModel user)
    {
        using (var connection = new SqlConnection(connectionString))
        {
            connection.Open();
            var query = "INSERT INTO Users (Username, Password, Email) VALUES (@Username, @Password, @Email)";
            using (var command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Username", user.Username);
                command.Parameters.AddWithValue("@Password", user.Password);
                command.Parameters.AddWithValue("@Email", user.Email);
                command.ExecuteNonQuery();
            }
        }
    }
} 