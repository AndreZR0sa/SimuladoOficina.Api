namespace SimuladoOficina.Api.Models.DTOs
{
    public class CriarVeiculoDto
    {
        public string Placa { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public int AnoFabricacao { get; set; }
        public string Problema { get; set; }

        public int ClienteId { get; set; }
    }
}
