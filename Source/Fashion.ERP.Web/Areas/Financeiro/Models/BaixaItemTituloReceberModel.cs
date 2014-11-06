using System;
using System.ComponentModel.DataAnnotations;
using Fashion.ERP.Web.Models;

namespace Fashion.ERP.Web.Areas.Financeiro.Models
{
    public class BaixaItemTituloReceberModel : IModel
    {
        public long? Id { get; set; }

        public long TituloReceberId { get; set; }

        [Display(Name = "Recebimento")]
        [Required(ErrorMessage = "Informe a data de recebimento")]
        public DateTime DataRecebimento { get; set; }

        public double Juro { get; set; }
        public double Despesa { get; set; }
        public double Desconto { get; set; }

        [Display(Name = "Valor baixa")]
        [Required(ErrorMessage = "Informe o valor da baixa")]
        public double ValorBaixa { get; set; }

        [Display(Name = "Valor total")]
        public double ValorTotal { get; set; }
    }
}