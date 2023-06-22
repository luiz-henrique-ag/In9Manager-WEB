using System.ComponentModel.DataAnnotations;

namespace In9Manager.Models
{
    public enum TipoPrestador
    {
        Terceirizado, Local
    }
    public class Prestador
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório.")]
        [StringLength(50, ErrorMessage = "Campo não pode ser maior que 50 caracteres.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório.")]
        [StringLength(14)]
        public string CPF { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório.")]
        [Display(Name = "Tipo do Prestador")]
        public int TipoPrestador { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório.")]
        [StringLength(14)]
        public string Telefone { get; set; }
    }
}
