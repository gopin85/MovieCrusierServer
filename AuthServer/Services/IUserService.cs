using AuthServer.Data.Models;
using System.Collections.Generic;

namespace AuthServer.Services
{
    /*Userservice interface*/
    public interface IUserService
    {
        /*To check user exists*/
        bool IsUserExists(string userId);

        /*To login user*/
        User Login(string UserId, string Password);

        /*To register user*/
        User RegisterUser(User UserDetails);
    }
}
