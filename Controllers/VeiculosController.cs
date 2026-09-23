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
    public class VeiculosController : Controller
    {
        private readonly AppDbContext _context;

        public VeiculosController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("CriarVeiculo")]
        [Authorize(Roles = "admin,cliente")]
        public async Task<IActionResult> CreateVeiculo([FromBody] CriarVeiculoDto dto)
        {
            var cliente = await _context.Clientes.FindAsync(dto.ClienteId);

            if (cliente == null)
            {
                return NotFound("Cliente não encontrado.");
            }

            var novoVeiculo = new Veiculo
            {
                Marca = dto.Marca,
                Modelo = dto.Modelo,
                Placa = dto.Placa,
                AnoFabricacao = dto.AnoFabricacao,
                Problema = dto.Problema,
                ClienteId = dto.ClienteId
            };

            _context.Veiculos.Add(novoVeiculo);

            await _context.SaveChangesAsync();

            return Ok(new
            {
                Mensagem = "Veículo criado com sucesso"
            });
        }

        [HttpGet("VerificarVeiculo")]
        [AllowAnonymous]
        public async Task<ActionResult<VeiculoDto>> GetVeiculo()
        {
            var veiculos = await _context.Veiculos.Include(v => v.Cliente).Select(v => new VeiculoDto
            {
                Marca = v.Marca,
                Modelo = v.Modelo,
                Placa = v.Placa,
                AnoFabricacao = v.AnoFabricacao,

                NomeCliente = v.Cliente.Nome
            }).ToListAsync();

            return Ok(veiculos);
        }

        [HttpDelete("DeletarVeiculo")]
        [Authorize (Roles = "admin")]
        public async Task<IActionResult> DeleteVeiculo(int id)
        {
            var veiculo = await _context.Veiculos.FindAsync(id);

            if(veiculo == null)
            {
                return NotFound(new { Mensagem = "Veiculo não encontrado" });
            }

            _context.Veiculos.Remove(veiculo);
            await _context.SaveChangesAsync();

            return Ok(new { Mensagem = "Veiculo deleta com sucesso" });
        }
    }
}
