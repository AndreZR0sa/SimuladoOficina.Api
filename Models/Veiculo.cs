namespace SimuladoOficina.Api.Models
{
    public class Veiculo
    {
        public int Id { get; set; }
        public string Placa { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public int AnoFabricacao { get; set; }
        public string Problema { get; set; }

        public int ClienteId { get; set; }
        public Cliente? Cliente { get; set; }

        public List<Agendamento> agendamentos { get; set; }
    }
}
