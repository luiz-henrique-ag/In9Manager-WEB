using System.ComponentModel.DataAnnotations;

namespace In9Manager.Models.ViewModels
{
    public class ClienteViewModel
    {
        public Cliente Cliente { get; set; }
        public ClienteEndereco ClienteEndereco { get; set; }

        [Display(Name = "Veículos")]
        public ICollection<Veiculo>? Veiculos { get; set; }
    }
}
