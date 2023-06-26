namespace In9Manager.Models
{
    public class OrcamentoServico
    {
        public int Id { get; set; }
        
        public int ServicoId { get; set; }
        public Servico? Servico { get; set; }

        public int OrcamentoId { get; set; }
        public Orcamento? Orcamento { get; set; }
    }
}
