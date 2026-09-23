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
    public class ClienteController : Controller
    {
        private readonly AppDbContext _context;

        public ClienteController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("CriarCliente")]
        [Authorize (Roles = "admim,cliente")]
        public async Task<IActionResult> CreateCliente([FromBody] CriarClienteDto dto)
        {
            var novoCliente = new Cliente
            {
                Nome = dto.Nome,
                Cpf = dto.Cpf,
                Telefone = dto.Telefone,
                Email = dto.Email,
                DataNascimento = dto.DataNascimento
            };

            _context.Clientes.Add(novoCliente);

            await _context.SaveChangesAsync();

            return Ok(new { Mensagem = "Cliente criado com sucesso" });
        }

        [HttpGet("VerificarCliente")]
        [AllowAnonymous]
        public async Task<ActionResult<ClienteDto>> GetCliente()
        {
            var clientes = await _context.Clientes
        .Select(c => new ClienteDto
        {
            NomeCliente = c.Nome,
            EmailCliente = c.Email,

            Agendamentos = c.Agendamentos
                .Select(a => new AgendamentoSimplesDto
                {
                    Especialidade = a.Especialidade,
                    DiaAgendado = a.DiaAgendado
                })
                .ToList(),

            Veiculos = c.Veiculos
                .Select(v => new VeiculoSimplesDto
                {
                    Marca = v.Marca,
                    Modelo = v.Modelo,
                    Placa = v.Placa
                })
                .ToList()
        })
        .ToListAsync();
        }
    }
}
