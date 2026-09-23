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
        [Authorize (Roles = "admin,cliente")]
        public async Task<IActionResult> CreateVeiculo([FromBody] CriarVeiculoDto dto)
        {
            var cliente = await _context.Clientes
                .FirstOrDefaultAsync(c => c.Id == dto.ClienteId);

            if (cliente == null)
                return NotFound("Cliente não encontrado.");

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

            return Ok(new { Mensagem = "Veiculo criado com sucesso" });
        }
    }
}
