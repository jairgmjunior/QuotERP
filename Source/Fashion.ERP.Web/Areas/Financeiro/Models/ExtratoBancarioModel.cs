using System;
using System.ComponentModel.DataAnnotations;
using Fashion.ERP.Domain.Financeiro;
using Fashion.ERP.Web.Models;

namespace Fashion.ERP.Web.Areas.Financeiro.Models
{
    public class ExtratoBancarioModel : IModel
    {
        public long? Id { get; set; }

        [Display(Name = "Lançamento")]
        [Required(ErrorMessage = "Informe o tipo de lançamento")]
        public TipoLancamento? TipoLancamento { get; set; }

        [Display(Name = "Emissão")]
        [Required(ErrorMessage = "Informe a data de emissão")]
        public DateTime? Emissao { get; set; }

        [Display(Name = "Compensação")]
        public DateTime? Compensacao { get; set; }

        [Display(Name = "Descrição")]
        [StringLength(100, ErrorMessage = "{0} não deve ser maior que {1} caracteres")]
        public string Descricao { get; set; }

        [Display(Name = "Valor")]
        [Required(ErrorMessage = "Informe o valor do lançamento")]
        public double Valor { get; set; }

        [Display(Name = "Compensado")]
        [Required(ErrorMessage = "Informe se foi compensado")]
        public bool Compensado { get; set; }

        [Display(Name = "Cancelado")]
        [Required(ErrorMessage = "Informe se foi cancelado")]
        public bool Cancelado { get; set; }

        [Display(Name = "Conta bancária")]
        [Required(ErrorMessage = "Informe a conta bancária")]
        public long? ContaBancaria { get; set; }
    }
}