namespace SimuladoOficina.Api.Models.DTOs
{
    public class CriarAgendamentoDto
    {
        public string Especialidade { get; set; }
        public DateTime DiaAgendado { get; set; }

        public int ClienteId { get; set; }
        public int MecanicoId { get; set; }
        public int VeiculoId { get; set; }
    }
}
