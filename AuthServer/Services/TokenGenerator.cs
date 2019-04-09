using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AuthServer.Services
{
    public class TokenGenerator : ITokenGenerator
    {
        /*To get JWT token*/
        public string GetJWTToken(string userId)
        {
            var claims = new[]
           {
                new Claim(JwtRegisteredClaimNames.UniqueName, userId),
                new Claim(JwtRegisteredClaimNames.Jti, new Guid().ToString())
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("secret_movie_jwt"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "AuthServer",
                audience: "moviecruiser",
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(15),
                signingCredentials: creds);

            var response = new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token)
            };

            return JsonConvert.SerializeObject(response);
        }
    }
}
