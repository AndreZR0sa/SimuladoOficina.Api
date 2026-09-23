namespace SimuladoOficina.Api.Models.DTOs
{
    public class AgendamentoDto
    {
        public int Id { get; set; }
        public string Especialidade { get; set; }
        public DateTime DiaAgendado { get; set; }

        public string NomeCliente { get; set; }

        public string NomeMecanico { get; set; }
        public string EspecialidadeMecanico { get; set; }

        public string MarcaVeiculo { get; set; }
        public string ModeloVeiculo { get; set; }
        public string PlacaVeiculo { get; set; }
    }
}
