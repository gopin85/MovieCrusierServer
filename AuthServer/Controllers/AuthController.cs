using System;
using Microsoft.AspNetCore.Mvc;
using AuthServer.Data.Models;
using AuthServer.Services;

namespace AuthServer.Controllers
{
    /*Authorization controller*/
    [Route("auth")]
    public class AuthController : Controller
    {
        private readonly IUserService _userService;
        private readonly ITokenGenerator _tokenGenerator;

        /*Constructor*/
        public AuthController(IUserService userService, ITokenGenerator tokenGenerator)
        {
            _userService = userService;
            _tokenGenerator = tokenGenerator;
        }

        /*User Registeration*/
        [HttpPost]
        [Route("register")]
        public IActionResult Register([FromBody] User user)
        {
            try
            {
                if (_userService.IsUserExists(user.UserId) == true)
                {
                    return StatusCode(409, "A user already exists with this id");
                }
                else
                {
                    _userService.RegisterUser(user);
                    return StatusCode(201, "You are successfully registered");
                }
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }

        /*User Login*/
        [HttpPost]
        [Route("login")]
        public IActionResult Login([FromBody] User user)
        {
            try
            {
                var serviceResult = _userService.Login(user.UserId, user.Password);
                var value = _tokenGenerator.GetJWTToken(user.UserId);
                return Ok(value);
            }

            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}
