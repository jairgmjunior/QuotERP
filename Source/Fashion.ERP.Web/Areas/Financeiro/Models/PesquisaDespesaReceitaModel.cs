using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Fashion.ERP.Domain.Financeiro;
using Fashion.ERP.Web.Models;

namespace Fashion.ERP.Web.Areas.Financeiro.Models
{
    public class PesquisaDespesaReceitaModel : IPesquisaModel
    {   
        [Display(Name = "Descrição")]
        [StringLength(100, ErrorMessage = "{0} não deve ser maior que {1} caracteres")]
        public string Descricao { get; set; }

        [Display(Name = "Ativo")]
        public bool? Ativo { get; set; }

        public string ModoConsulta { get; set; }

        [Display(Name = "Tipo")]
        public string TipoRelatorio { get; set; }

        [Display(Name = "Agrupar por")]
        public string AgruparPor { get; set; }

        [Display(Name = "Ordenar por")]
        public string OrdenarPor { get; set; }

        [Display(Name = "em")]
        public string OrdenarEm { get; set; }

        [Display(Name = "Tipo")]
        public TipoDespesaReceita? TipoDespesaReceita { get; set; }

        public IList<GridDespesaReceitaModel> Grid { get; set; }
    }
}