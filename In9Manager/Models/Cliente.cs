using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace In9Manager.Models
{
    public enum Sexo
    {
        M,F
    }

    [Index(nameof(CPF))]
    public class Cliente
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Nome { get; set; }

        [Required]
        [StringLength(14)]
        public string CPF { get; set; }

        [Required]
        [StringLength(14)]
        public string Telefone { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "E-Mail")]
        public string Email { get; set; }

        [Required]
        [StringLength(1)]
        public string Sexo { get; set; }

        [Required]
        [Display(Name = "Data de Nascimento")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DataNascimento { get; set; }

        public int Idade
        {
            get
            {
                return DateTime.Now.Year - DataNascimento.Year;
            }
        }

        public DateTime DataCadastro { get; set; } = DateTime.UtcNow;
        public ICollection<Veiculo>? Veiculos { get; set; }
    }

}
