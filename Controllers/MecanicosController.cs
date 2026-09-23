using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimuladoOficina.Api.Data;
using SimuladoOficina.Api.Models;
using SimuladoOficina.Api.Models.DTOs;

namespace SimuladoOficina.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MecanicosController : Controller
    {
        private readonly AppDbContext _context;

        public MecanicosController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("CriarMecanico")]
        [Authorize (Roles = "admin,mecanico")]
        public async Task<IActionResult> CreateMecanico([FromBody] CriarMecanicoDto dto)
        {
            var novoMecanico = new Mecanico
            {
                Nome = dto.Nome,
                Especialidade = dto.Especialidade,
                Email = dto.Email,
                Telefone = dto.Telefone,
                Cpf = dto.Cpf
            };

            _context.Mecanicos.Add(novoMecanico);

            await _context.SaveChangesAsync();

            return Ok(new { Mensagem = "Mecanico criado" });
        }

        [HttpGet("VerificarMecanico")]
        [AllowAnonymous]
        public async Task<ActionResult<Mecanico>> GetMecanico()
        {
            var mecanicos = await _context.Mecanicos.Select(m => new MecanicoDto
            {
                NomeMecanico = m.Nome,
                EspecialidadeMecanico = m.Especialidade,
                EmailMecanico = m.Email,

                Agendamentos = m.agendamentos
                .Select(a => new AgendamentoSimplesDto
                {
                    Especialidade = a.Especialidade,
                    DiaAgendado = a.DiaAgendado
                })
                .ToList()
            }).ToListAsync();

            return Ok(mecanicos);
        }

        [HttpDelete("DeletarMecanico")]
        [Authorize (Roles = "admin,mecanico")]
        public async Task<IActionResult> DeleteMecanico(int id)
        {
            var mecanico = await _context.Mecanicos.FindAsync(id);

            if(mecanico == null)
            {
                return NotFound(new { Mensagem = "Mecanico não encontrado" });
            }

            _context.Mecanicos.Remove(mecanico);
            await _context.SaveChangesAsync();

            return Ok(new { Mensagem = "Mecanico deletado com sucesso" });
        }
    }
}
