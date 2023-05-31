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
        public string TipoLogradouro { get; set; }

        [StringLength(40, ErrorMessage = "Máximo 40 caracteres.")]
        public string Logradouro { get; set; }

        [Display(Name = "Número")]
        public int NumeroLogradouro { get; set; }

        [StringLength(40, ErrorMessage = "Máximo 40 caracteres.")]
        public string Bairro { get; set; }

        [StringLength(40, ErrorMessage = "Máximo 40 caracteres.")]
        public string Complemento { get; set; }

        [StringLength(40, ErrorMessage = "Máximo 40 caracteres.")]
        public string Cidade { get; set; }

        [StringLength(2)]
        public string Estado { get; set; }

        [Display(Name = "CEP")]
        [StringLength(8)]
        public string Cep { get; set; }

        public int ClienteId { get; set; }
        public Cliente? Cliente { get; set; }
    }
}
