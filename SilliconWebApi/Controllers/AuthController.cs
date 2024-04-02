using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SilliconWebApi.Filters;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace SilliconWebApi.Controllers
{
    public class AuthController(IConfiguration configuration) : Controller
    {
        private readonly IConfiguration _configuration = configuration;

        [UseApiKey]
        [HttpPost]
        [Route("token")]
        public IActionResult GetToken()
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Secret"]!);
            var tokenDescripter = new SecurityTokenDescriptor
            {
                Expires = DateTime.UtcNow.AddMinutes(30),
                Issuer = _configuration["Jwt:Issuer"],
                Audience = _configuration["Jwt:Audience"],
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescripter);
            var tokenString = tokenHandler.WriteToken(token);

            return Ok(tokenString);
        }
    }
}
