namespace SimuladoOficina.Api.Models.DTOs
{
    public class ClienteDto
    {
        public string NomeCliente { get; set; }
        public string EmailCliente { get; set; }

        public List<AgendamentoSimplesDto>? Agendamentos { get; set; }

        public List<VeiculoSimplesDto>? Veiculos { get; set; }
    }
}
