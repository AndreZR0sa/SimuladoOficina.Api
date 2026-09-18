namespace SimuladoOficina.Api.Models
{
    public class Mecanico
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Especialidade { get; set; }
        public string Telefone { get; set; }
        public string Cpf { get; set; }
        public string Email { get; set; }

        public List<Agendamento>? agendamentos { get; set; }
    }
}
