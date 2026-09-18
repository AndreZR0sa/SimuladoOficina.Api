using Microsoft.EntityFrameworkCore;

namespace SimuladoOficina.Api.Models
{
    public class Agendamento
    {
        public int Id { get; set; }
        public DateTime DiaAgendado { get; set; }
        public string Especialidade { get; set; }

        public int ClienteId { get; set; }
        [DeleteBehavior(DeleteBehavior.NoAction)]
        public Cliente? Cliente { get; set; }

        public int VeiculoId { get; set; }
        [DeleteBehavior(DeleteBehavior.NoAction)]
        public Veiculo? Veiculo { get; set; }

        public int MecanicoId { get; set; }
        [DeleteBehavior(DeleteBehavior.NoAction)]
        public Mecanico? Mecanico { get; set; }
    }
}
