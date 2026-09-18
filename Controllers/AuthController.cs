using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SimuladoOficina.Api.Data;
using SimuladoOficina.Api.Models;
using SimuladoOficina.Api.Models.DTOs;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SimuladoOficina.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AuthController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            if (await _context.Usuarios.AnyAsync(e => e.Email == registerDto.Email))
            {
                return StatusCode(500, new { Mensagem = "Usuário já cadastrado" });
            }

            var novoUsuario = new Usuario()
            {
                Email = registerDto.Email,
                Nome = registerDto.Nome,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(registerDto.Password),
                Role = registerDto.Role,
            };

            await _context.Usuarios.AddAsync(novoUsuario);
            _context.SaveChanges();

            return Ok(new { Mensagem = "Usuário criado com sucesso" });
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var user = await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == loginDto.Email);

            var passwordIsValid = BCrypt.Net.BCrypt.Verify(loginDto.Password, user.PasswordHash);

            if (user != null && passwordIsValid)
            {
                var token = CriarToken(user);

                return Ok(new { token });
            }

            return Unauthorized(new { Mensagem = "Credenciais inválidas" });
        }

        private string CriarToken(Usuario usuario)
        {
            var claims = new List<Claim>
            {
                new Claim("nome", usuario.Nome),
                new Claim("Role", usuario.Role)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("minhachaveSecretaSenai927M@rilia2026"));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(claims: claims, signingCredentials: creds, expires: DateTime.Now.AddHours(2));

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }
    }
}
