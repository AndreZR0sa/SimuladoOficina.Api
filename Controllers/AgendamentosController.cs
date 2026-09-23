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
    public class AgendamentosController : Controller
    {
        private readonly AppDbContext _context;

        public AgendamentosController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("CriarAgendamento")]
        [Authorize(Roles = "admin,mecanico,cliente")]
        public async Task<IActionResult> CreateAgendamento([FromBody] CriarAgendamentoDto dto)
        {
            var veiculo = await _context.Veiculos
                .FirstOrDefaultAsync(v => v.Id == dto.VeiculoId);

            if (veiculo == null)
                return NotFound("Veículo não encontrado.");

            var mecanico = await _context.Mecanicos
                .FirstOrDefaultAsync(m => m.Id == dto.MecanicoId);

            if (mecanico == null)
                return NotFound("Mecânico não encontrado.");

            var novoAgendamento = new Agendamento
            {
                DiaAgendado = dto.DiaAgendado,
                Especialidade = dto.Especialidade,

                VeiculoId = dto.VeiculoId,
                ClienteId = dto.ClienteId,
                MecanicoId = dto.MecanicoId
            };

            _context.Agendamentos.Add(novoAgendamento);

            await _context.SaveChangesAsync();

            return Ok(new { Mensagem = "Agendamento criado com sucesso" });
        }

        [HttpGet("VerificarAgendamento")]
        [AllowAnonymous]
        public async Task<ActionResult<List<AgendamentoDto>>> GetAgendamento()
        {
            var agendamentos = await _context.Agendamentos.Include(a => a.Cliente)
                                                          .Include(a => a.Mecanico)
                                                          .Include(a => a.Veiculo)
                                                          .Select(a => new AgendamentoDto
                                                          {
                                                              Id = a.Id,
                                                              DiaAgendado = a.DiaAgendado,
                                                              Especialidade = a.Especialidade,

                                                              NomeCliente = a.Cliente.Nome,

                                                              NomeMecanico = a.Mecanico.Nome,
                                                              EspecialidadeMecanico = a.Mecanico.Especialidade,

                                                              MarcaVeiculo = a.Veiculo.Marca,
                                                              ModeloVeiculo = a.Veiculo.Modelo,
                                                              PlacaVeiculo = a.Veiculo.Placa
                                                          }).ToListAsync();

            return Ok(agendamentos);
        }

        [HttpDelete("DeletarAgendamento")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteAgendamento(int id)
        {
            var agenda = await _context.Agendamentos.FindAsync(id);

            if(agenda == null)
            {
                return NotFound(new { Mensagem = "Agendamento não encontrado" });
            }

            _context.Agendamentos.Remove(agenda);
            await _context.SaveChangesAsync();
                
            return Ok();
        }
    }
}
