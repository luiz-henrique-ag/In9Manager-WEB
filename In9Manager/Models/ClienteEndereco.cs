using System.ComponentModel.DataAnnotations;

namespace In9Manager.Models
{
    public enum Estados
    {
        AC, AL, AP, AM, BA, CE, DF, ES, GO, MA, MT, MS, MG, PA, PB, PR, PE, PI, RJ, RN, RS, RO, RR, SC, SP, SE, TO 
    }
    public enum TipoLogradouro
    {
        Praça, Rua, Avenida
    }

    public class ClienteEndereco
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

        public int ClienteId { get; set; }
        public Cliente? Cliente { get; set; }
    }
}
