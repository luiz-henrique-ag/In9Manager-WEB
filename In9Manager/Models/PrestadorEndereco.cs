using System.ComponentModel.DataAnnotations;

namespace In9Manager.Models
{
    public class PrestadorEndereco
    {
        public int Id { get; set; }

        [Display(Name = "Tipo")]
        [StringLength(10)]
        public string TipoLogradouro { get; set; } = string.Empty;

        [StringLength(40, ErrorMessage = "Máximo 40 caracteres.")]
        public string Logradouro { get; set; } = string.Empty;

        [Display(Name = "Número")]
        public int NumeroLogradouro { get; set; } = 0;

        [StringLength(40, ErrorMessage = "Máximo 40 caracteres.")]
        public string Bairro { get; set; } = string.Empty;

        [StringLength(40, ErrorMessage = "Máximo 40 caracteres.")]
        public string Complemento { get; set; } = string.Empty;

        [StringLength(40, ErrorMessage = "Máximo 40 caracteres.")]
        public string Cidade { get; set; } = string.Empty;

        [StringLength(2)]
        public string Estado { get; set; } = string.Empty;

        [Display(Name = "CEP")]
        [StringLength(9)]
        public string Cep { get; set; } = string.Empty;

        public int PrestadorId { get; set; }
        public Prestador? Prestador { get; set; }
    }
}
