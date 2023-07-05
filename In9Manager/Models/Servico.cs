using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace In9Manager.Models
{
    public enum TipoMaodeObra
    {
        Terceirizada = 1, Local = 2
    }
    public class Servico
    {
        public int Id { get; set; }
        
        [Required]
        [StringLength(50)]
        public string Nome { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Descrição")]
        public string Descricao { get; set; }

        [Required]
        [StringLength(20)]
        public string Categoria { get; set; }

        [Required]
        [Display(Name = "Tipo de Mão de Obra")]
        public int TipoMaoDeObra { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Valor { get; set; }

        [Display(Name = "Data de Atualização")]
        public DateTime DataAtualizacao { get; set; } = DateTime.Now;
    }
}
