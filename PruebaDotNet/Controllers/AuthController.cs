using Microsoft.AspNetCore.Mvc;
using PruebaDotNet.Models;

namespace PruebaDotNet.Controllers
{

    public class AuthController : Controller
    {
        private readonly IConfiguration _configuration; 
        public AuthController(IConfiguration configuration) { _configuration = configuration; }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginModel loginModel)
        {
            // Credenciales de prueba
            var adminEmail = "admin@prueba.com";
            var adminPassword = "Admin123";
            var clienteEmail = "cliente@prueba.com";
            var clientePassword = "Cliente123";
            if ((loginModel.Email == adminEmail &&
                 loginModel.Password == adminPassword) ||
                (loginModel.Email == clienteEmail &&
                 loginModel.Password == clientePassword))
            {
                var role = loginModel.Email == adminEmail ? "Admin" : "Cliente";
                var token = GenerateJwtToken(loginModel.Email, role);
                return Ok(new { Token = token });
            }
            return Unauthorized("Credenciales inválidas.");
        }

        private string GenerateJwtToken(string email, string role)
        {
            //var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"])); var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256); var claims = new[] { new Claim(ClaimTypes.Email, email), new Claim(ClaimTypes.Role, role) }; var token = new JwtSecurityToken(issuer: _configuration["Jwt:Issuer"], audience: _configuration["Jwt:Audience"], claims: claims, expires: DateTime.Now.AddHours(1), signingCredentials: credentials); return new JwtSecurityTokenHandler().WriteToken(token);
            return "";
        }
    }
}
