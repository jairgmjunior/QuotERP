using System;
using System.ComponentModel.DataAnnotations;

namespace Fashion.ERP.Web.Areas.Financeiro.Models
{
    public class ListaBaixaChequeRecebidoModel
    {
        public long Id { get; set; }

        [Display(Name = "Data baixa")]
        public DateTime? Data { get; set; }

        [Display(Name = "Valor baixa")]
        public double Valor { get; set; }

        [Display(Name = "Juros")]
        public double ValorJuros { get; set; }

        [Display(Name = "Despesas")]
        public double Despesa { get; set; }

        [Display(Name = "Descontos")]
        public double ValorDesconto { get; set; }

        [Display(Name = "Valor total")]
        public double ValorTotal { get; set; }

        [Display(Name = "Observação")]
        public string Observacao { get; set; }
    }
}