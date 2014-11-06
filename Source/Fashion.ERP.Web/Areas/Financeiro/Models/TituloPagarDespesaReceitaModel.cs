using System.ComponentModel.DataAnnotations;

namespace Fashion.ERP.Web.Areas.Financeiro.Models
{
    public class TituloPagarDespesaReceitaModel
    {
        public long? Id { get; set; }
        public long? TituloPagarId { get; set; }

        [Display(Name = "Tipo de despesa")]
        [Required(ErrorMessage = "Informe o tipo de despesa.")]
        public string DespesaReceitaId { get; set; }

        [Display(Name = "Rateio")]
        public double RateioDespesaReceita { get; set; }

        [Display(Name = "Valor")]
        [Required(ErrorMessage = "Informe o valor da despesa.")]
        public double ValorDespesaReceita { get; set; }  
    }
}