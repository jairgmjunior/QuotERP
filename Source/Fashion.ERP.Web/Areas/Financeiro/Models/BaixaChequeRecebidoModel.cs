using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Fashion.ERP.Web.Models;
using System;

namespace Fashion.ERP.Web.Areas.Financeiro.Models
{
    public class BaixaChequeRecebidoModel : IModel
    {
        public long? Id { get; set; }

        [Display(Name = "Data da baixa")]
        [Required(ErrorMessage = "Informe a data da baixa")]
        public DateTime? Data { get; set; }
        
        [Display(Name = "Taxa de juros (%)")]
        public double TaxaJuros { get; set; }
        
        [Display(Name = "Valor do juros")]
        public double ValorJuros { get; set; }
        
        [Display(Name = "Desconto")]
        public double ValorDesconto { get; set; }
        
        [Display(Name = "Valor da baixa")]
        public double Valor { get; set; }

        [Display(Name = "Valor total")]
        public double ValorTotal { get; set; }

        [Display(Name = "Histórico")]
        [StringLength(4000, ErrorMessage = "{0} não deve ser maior que {1} caracteres")]
        public string Historico { get; set; }
        
        [Display(Name = "Observação")]
        [StringLength(4000, ErrorMessage = "{0} não deve ser maior que {1} caracteres")]
        public string Observacao { get; set; }

        [Display(Name = "Cheque")]
        public long ChequeRecebido { get; set; }

        public List<RecebimentoChequeRecebidoModel> Recebimentos { get; set; }
    }
}