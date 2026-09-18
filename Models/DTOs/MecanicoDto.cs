namespace SimuladoOficina.Api.Models.DTOs
{
    public class MecanicoDto
    {
        public string NomeMecanico { get; set; }
        public string EspecialidadeMecanico { get; set; }
        public string EmailEmail { get; set; }

        public List<AgendamentoSimplesDto>? Agendamentos { get; set; }
    }
}
