using System.Collections.Generic;
using System.Threading.Tasks;

public interface IUserService
{
    UserModel Authenticate(string username, string password);
    UserModel Register(UserModel user); 
} 