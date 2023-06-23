using System.ComponentModel.DataAnnotations;

namespace In9Manager.Models
{
    public enum Permissao 
    { 
        Administrador = 1, Comum = 2
    }
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

        [Display(Name = "Permissão")]
        public int Permissao { get; set; } = 2;
    }
}
