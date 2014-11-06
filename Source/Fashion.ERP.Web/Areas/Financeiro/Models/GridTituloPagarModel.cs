using System;
using System.ComponentModel.DataAnnotations;

namespace Fashion.ERP.Web.Areas.Financeiro.Models
{
    public class GridTituloPagarModel
    {
        public long Id { get; set; }

        [Display(Name = "U.E.")]        
        public long UnidadeEstocadora { get; set; }

        [Display(Name = "Número")]
        public string Numero { get; set; }
        public int Parcela { get; set; }
        public int Plano { get; set; }
        public string Fornecedor { get; set; }
        public DateTime Vencimento { get; set; }
        public double Valor { get; set; }

        [Display(Name = "Saldo devedor")]
        public double SaldoDevedor { get; set; }

        [Display(Name = "Situação título")]
        public string SituacaoTitulo { get; set; }
    }
}