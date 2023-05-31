using System.ComponentModel.DataAnnotations;

namespace In9Manager.Models
{
    public class Usuario
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório")]
        [StringLength(50, ErrorMessage = "Máximo 50 caracteres.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório")]
        [StringLength(50, ErrorMessage = "Máximo 50 caracteres.")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório")]
        [StringLength(14)]
        [Display(Name = "CPF")]
        public string Cpf { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório")]
        public string Senha { get; set; }

        public int Permissao { get; set; } = 0;
    }
}
