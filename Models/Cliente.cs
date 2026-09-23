namespace SimuladoOficina.Api.Models
{
    public class Cliente
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public DateTime DataNascimento { get; set; }

        public List<Veiculo>? Veiculos { get; set; }

        public List<Agendamento>? Agendamentos { get; set; }
    }
}
