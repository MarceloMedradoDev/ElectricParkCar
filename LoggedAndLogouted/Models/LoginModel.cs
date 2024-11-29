using System.ComponentModel.DataAnnotations;

namespace LoggedAndLogouted.Models
{
    public class LoginModel
    {
        [Key]
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Login { get; set; }
        public string Senha { get; set; }
        public DateTime DataDeCadastro { get; set; }
    }
}