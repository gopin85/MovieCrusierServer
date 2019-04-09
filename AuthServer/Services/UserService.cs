using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using AuthServer.Data.Models;
using AuthServer.Data.Persistence;
using System.Collections.Generic;

namespace AuthServer.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        /*Constructor*/
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;

        }

        /*To check user exists*/
        public bool IsUserExists(string userId)
        {
            var isUserExists = _userRepository.IsUserExists(userId);
            return isUserExists;
        }

        /*To login user*/
        public User Login(string UserId, string Password)
        {
            var user = _userRepository.Login(UserId, Password);
            return user;
        }

        /*To register user*/
        public User RegisterUser(User UserDetails)
        {
            var user = _userRepository.RegisterUser(UserDetails);
            return user;
        }


    }
}
