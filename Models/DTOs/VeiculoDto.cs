namespace SimuladoOficina.Api.Models.DTOs
{
    public class VeiculoDto
    {
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public string Placa { get; set; }
        public int AnoFabricacao { get; set; }

        public int ClienteId { get; set; }
        public string NomeCliente { get; set; }

        public List<AgendamentoSimplesDto> Agendamentos { get; set; }
    }
}
