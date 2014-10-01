using System.ComponentModel.DataAnnotations;

namespace Fashion.ERP.Web.Areas.Compras.Models
{
    public class GridAutorizacoesModel
    {
        public long Id { get; set; }

        [Display(Name = "Procedimentos")]
        public string Procedimento { get; set; }
    }
}