using System.ComponentModel.DataAnnotations;

namespace Fashion.ERP.Web.Areas.Financeiro.Models
{
    public class GridTituloReceberModel
    {
        public long Id { get; set; }

        [Display(Name = "U.E.")]
        public long UnidadeEstocadora { get; set; }

        [Display(Name = "Número")]
        public string Numero { get; set; }
        public int Plano { get; set; }
        public string Cliente { get; set; }
        public double Valor { get; set; }

        [Display(Name = "Saldo")]
        public double SaldoDevedor { get; set; }

        [Display(Name = "Situação")]
        public string SituacaoTitulo { get; set; }
    }
}