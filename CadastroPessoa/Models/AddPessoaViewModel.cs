namespace CadastroPessoa.Models
{
    public class AddPessoaViewModel
    {
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public DateTime DataNascimento { get; set; }
        public string EstadoCivil { get; set; }
        public string CPF { get; set; }
        public string RG { get; set; }
    }
}
