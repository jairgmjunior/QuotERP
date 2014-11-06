using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Fashion.ERP.Web.Areas.Financeiro.Models
{
    public class BaixaTituloReceberModel
    {
        public BaixaTituloReceberModel()
        {
            BaixaTitulos = new List<BaixaItemTituloReceberModel>();
        }

        public long Id { get; set; }

        [Display(Name = "Unidade")]
        public string Unidade { get; set; }

        [Display(Name = "Situação título")]
        public string SituacaoTitulo { get; set; }

        [Display(Name = "Número/Parcela")]
        public string NumeroParcela { get; set; }

        [Display(Name = "Plano")]
        public string Plano { get; set; }

        [Display(Name = "Vencimento")]
        public string Vencimento { get; set; }

        [Display(Name = "Valor título")]
        public double Valor { get; set; }

        [Display(Name = "Histórico")]
        public string Historico { get; set; }

        [Display(Name = "Observação")]
        public string Observacao { get; set; }

        [Display(Name = "Cliente")]
        public string Cliente { get; set; }

        [Display(Name = "Banco")]
        public string Banco { get; set; }

        public IList<BaixaItemTituloReceberModel> BaixaTitulos { get; set; } 
    }
}