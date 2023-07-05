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

        [Required(ErrorMessage = "Campo Obrigatório.")]
        [StringLength(50)]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório.")]
        [StringLength(14)]
        public string CPF { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório.")]
        [StringLength(14)]
        public string Telefone { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório.")]
        [EmailAddress]
        [Display(Name = "E-Mail")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório.")]
        [StringLength(1)]
        public string Sexo { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório.")]
        [Display(Name = "Data de Nascimento")]
        [DataType(DataType.Date)]
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
