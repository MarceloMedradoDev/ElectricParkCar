using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using LoggedAndLogouted.Models;
using LoggedAndLogouted.Service;
using System.Linq;

namespace LoggedAndLogouted.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IUserInterface _userService;

        // Construtor para injeção de dependências
        public AuthController(IConfiguration config, IUserInterface userService)
        {
            _config = config;
            _userService = userService;

            if (string.IsNullOrWhiteSpace(_config["Jwt:Key"]))
            {
                throw new ArgumentException("A chave JWT não está configurada corretamente no appsettings.json.");
            }
        }

        // Endpoint de login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] ReadLoginModel readLoginModel)
        {
            // Chama o serviço para verificar as credenciais no banco de dados
            var loginResponse = await _userService.GetLogin(readLoginModel);

            if (loginResponse.Sucesso)
            {
                // Gera o token JWT para o usuário autenticado
                var token = GenerateToken(readLoginModel.Login);
                return Ok(new { Token = token });
            }

            return Unauthorized("Credenciais inválidas.");
        }

        // Método privado para gerar o token JWT
        private string GenerateToken(string username)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.Role, "User") // Defina a role do usuário (pode ser alterado)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
