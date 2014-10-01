using System.ComponentModel.DataAnnotations;

namespace Fashion.ERP.Web.Areas.Financeiro.Models
{
    public class GridDespesaReceitaModel
    {
        public long Id { get; set; }

        [Display(Name = "Descrição")]
        public string Descricao { get; set; }

        [Display(Name = "Ativo")]
        public bool Ativo { get; set; }

        [Display(Name = "Tipo")]
        public string TipoDespesaReceita { get; set; }
    }
}