using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Sprint_3.DTOs;
using Sprint_3.Models;
using Sprint_3.Repository.Interface;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Sprint_3.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUsuario _usuarioRepository;
        private readonly IConfiguration _config;

        public AuthController(IUsuario usuarioRepository, IConfiguration config)
        {
            _usuarioRepository = usuarioRepository;
            _config = config;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO dto)
        {
            var usuario = await _usuarioRepository.Login(dto.Email);

            if (usuario == null)
                return Unauthorized("Email não encontrado");

            var token = GerarToken(usuario);

            return Ok(new
            {
                Token = token,
                Usuario = usuario.Nome,
                Email = usuario.Email
            });
        }

        private string GerarToken(Usuario usuario)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
                new Claim(ClaimTypes.Email, usuario.Email),
                new Claim(ClaimTypes.Name, usuario.Nome)
            };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_config["Jwt:Key"])
            );

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
