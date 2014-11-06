using System.ComponentModel.DataAnnotations;

namespace Fashion.ERP.Web.Areas.Financeiro.Models
{
    public class GridRateioDespesaReceita
    {
        public long Id { get; set; }

        [Display(Name = "Valor")]
        public double Valor { get; set; }
    }
}