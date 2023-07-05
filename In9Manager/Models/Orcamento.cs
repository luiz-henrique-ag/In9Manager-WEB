using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace In9Manager.Models
{
    public enum OrcamentoFormaPagamento
    {
        Crédito, Débito, Pix, Dinheiro
    }
    public enum OrcamentoStatus
    {
        Iniciado, Finalizado
    }

    public class Orcamento
    {
        public int Id { get; set; }
        public int ClienteId { get; set; }
        public Cliente? Cliente { get; set;}

        [Required(ErrorMessage = "Campo Obrigatório.")]
        [Display(Name = "Data de Criação")]
        [DataType(DataType.Date)]
        public DateTime DataCriacao { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "Campo Obrigatório.")]
        [Display(Name = "Data de Validade")]
        [DataType(DataType.Date)]
        public DateTime DataValidade { get; set; } = DateTime.Now.AddDays(7);

        [Required(ErrorMessage = "Campo Obrigatório.")]
        [Display(Name = "Valor Total")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal ValorTotal { get; set; }

        [Display(Name = "Placa do Veículo")]
        [StringLength(50, ErrorMessage = "Identificador não pode ser maior que 50 caracteres.")]
        public string PlacaVeiculo { get; set; } = string.Empty;

        [Required(ErrorMessage = "Campo Obrigatório.")]
        [Range(0, 100.0)]
        public double Desconto { get; set; } = 0;

        [Required(ErrorMessage = "Campo Obrigatório.")]
        [Display(Name = "Valor Final")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal ValorFinal { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório.")]
        public int Status { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório.")]
        [Display(Name = "Forma de Pagamento")]
        public int FormaPagamento { get; set; }

        [Display(Name = "Número de Parcelas")]
        public int NumeroParcelas { get; set; } = 0;

        public ICollection<OrcamentoServico>? Servicos { get; set; }

        public void SetarValorFinal()
        {
            ValorFinal = ValorTotal*(decimal)(1 + (Desconto/100));
        }

    }
}
