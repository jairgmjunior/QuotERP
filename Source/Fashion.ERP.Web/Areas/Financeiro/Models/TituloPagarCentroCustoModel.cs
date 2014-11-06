using System.ComponentModel.DataAnnotations;

namespace Fashion.ERP.Web.Areas.Financeiro.Models
{
    public class TituloPagarCentroCustoModel
    {
        public long? Id { get; set; }
        public long? TituloPagarId { get; set; }

        [Display(Name = "Centro de custo")]
        [Required(ErrorMessage = "Informe o centro de custo.")]
        public string CentroCustoId { get; set; }

        public double RateioCentroCusto { get; set; }

        [Display(Name = "Valor")]
        [Required(ErrorMessage = "Informe o valor do centro de custo.")]
        public double ValorCentroCusto { get; set; }  
    }
}