using System.ComponentModel.DataAnnotations;

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
        public DateTime DataCriacao { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "Campo Obrigatório.")]
        [Display(Name = "Data de Validade")]
        public DateTime DataValidade { get; set; } = DateTime.Now.AddDays(7);

        [Required(ErrorMessage = "Campo Obrigatório.")]
        [Display(Name = "Valor Total")]
        [Range(0, 100.0)]
        public double ValorTotal { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório.")]
        [Display(Name = "Placa do Veículo")]
        [StringLength(7)]
        [MinLength(7)]
        public string PlacaVeiculo { get; set; } = string.Empty;

        [Required(ErrorMessage = "Campo Obrigatório.")]
        [Range(0, 100.0)]
        public double Desconto { get; set; } = 0;

        [Required(ErrorMessage = "Campo Obrigatório.")]
        [Display(Name = "Valor Final")]
        [Range(0, 100.0)]
        public double ValorFinal { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório.")]
        [StringLength(1)]
        public string Status { get; set; } = "I";

        [Required(ErrorMessage = "Campo Obrigatório.")]
        [Display(Name = "Forma de Pagamento")]
        public int FormaPagamento { get; set; }

        [Display(Name = "Número de Parcelas")]
        public int NumeroParcelas { get; set; } = 0;

        public ICollection<OrcamentoServico>? Servicos { get; set; }

        public void SetarValorFinal()
        {
            ValorFinal = ValorTotal*(1 + (Desconto/100));
        }

    }
}
