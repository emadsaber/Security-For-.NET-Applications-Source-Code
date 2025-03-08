using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JwtAuthentication.APIs.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthorizationController : Controller
    {
        private readonly IConfiguration _configuration;

        public record UserInfo(string username, string email, string role);

        public AuthorizationController(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

        [HttpPost("login")]
        public IActionResult Login(string username, string password)
        {
            if(!CheckCredentials(username, password))
            {
                return Unauthorized();
            }
            var token = GenerateJwtToken(GetUser(username));
            return Ok(new { token });
        }


        private string GenerateJwtToken(UserInfo user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:Issuer"],
                audience: _configuration["JWT:Audience"],
                claims: new List<Claim>()
                {
                    new Claim(ClaimTypes.Name, user.username),
                    new Claim(ClaimTypes.Email, user.email),
                    new Claim(ClaimTypes.Role, user.role)
                },
                expires: DateTime.Now.AddMinutes(Convert.ToInt32(_configuration["JWT:ExpirationInMinutes"])),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private bool CheckCredentials(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                return false;
            }

            // Check the username and password against the database
            return true;
        }

        private UserInfo GetUser(string username)
        {
            // Get the user from the database
            return new(username, $"{username}@expertdevs.com", "Admin");
        }

    }
}
