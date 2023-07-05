using System.ComponentModel.DataAnnotations;

namespace In9Manager.Models
{
    public enum VeiculoCategoria
    {
        Carro, Moto
    }
    public class Veiculo
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório.")]
        [StringLength(30, ErrorMessage = "Campo não pode ser maior que 30 caracteres.")]
        public string Modelo { get; set; } = string.Empty;

        [Required(ErrorMessage = "Campo Obrigatório.")]
        [StringLength(30, ErrorMessage = "Campo não pode ser maior que 30 caracteres.")]
        public string Marca { get; set; } = string.Empty;

        [Required(ErrorMessage = "Campo Obrigatório.")]
        [StringLength(9, ErrorMessage = "Campo não pode ser maior que 9 caracteres.")]
        public string Ano { get; set; } = string.Empty;

        [Required(ErrorMessage = "Campo Obrigatório.")]
        [StringLength(10, ErrorMessage = "Campo não pode ser maior que 10 caracteres.")]
        public string Categoria { get; set; } = string.Empty;

        [StringLength(7, ErrorMessage = "Campo não pode ser maior que 7 caracteres.")]
        [MinLength(7)]
        public string Placa { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Proprietário")]
        public int ClienteID { get; set; }
        public Cliente? Cliente { get; set; }
    }
}
