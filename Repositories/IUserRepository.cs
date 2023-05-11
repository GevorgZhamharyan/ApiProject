public interface IUserRepository
{
    UserModel GetByUsernameAndPassword(string username, string password);
    UserModel GetByUsername(string username);
    UserModel GetByEmail(string email);
    void Create(UserModel user); 
} 