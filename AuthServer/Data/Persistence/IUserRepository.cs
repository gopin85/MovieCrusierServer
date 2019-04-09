using AuthServer.Data.Models;
using System.Collections.Generic;

namespace AuthServer.Data.Persistence
{
    /*User Repository Interface*/
    public interface IUserRepository
    {
        /*To check user exists in DB*/
        bool IsUserExists(string UserId);

        /*To login user*/
        User Login(string UserId, string Password);

        /*To register user*/
        User RegisterUser(User UserDetails);
    }

}
