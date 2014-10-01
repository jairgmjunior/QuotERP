using System.ComponentModel.DataAnnotations;

namespace Fashion.ERP.Web.Areas.Comum.Models
{
    public class GridCentroCustoModel
    {
        public long Id { get; set; }

        [Display(Name = "Código")]
        public long Codigo { get; set; }

        public string Nome { get; set; }

        public bool Ativo { get; set; } 
    }
}