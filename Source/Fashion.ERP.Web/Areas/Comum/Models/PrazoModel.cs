using System.ComponentModel.DataAnnotations;
using Fashion.ERP.Web.Models;

namespace Fashion.ERP.Web.Areas.Comum.Models
{
    public class PrazoModel : IModel
    {
        public long? Id { get; set; }

        [Display(Name = "Descrição")]
        [Required(ErrorMessage = "Informe a descrição")]
        [StringLength(60, ErrorMessage = "{0} não deve ser maior que {1} caracteres")]
        public string Descricao { get; set; }

        [Display(Name = "À vista")]
        [Required(ErrorMessage = "Informe se o prazo é 'à vista' ou não.")]
        public bool? AVista { get; set; }

        [Display(Name = "Qtd. parcelas")]
        [Required(ErrorMessage = "Informe a quantidade de parcelas")]
        public int? QuantidadeParcelas { get; set; }

        [Display(Name = "PrazoDescricao 1ª parcela")]
        [Required(ErrorMessage = "Informe o prazo para a primeira parcela")]
        public int? PrazoPrimeiraParcela { get; set; }

        [Display(Name = "Intervalo")]
        public int? Intervalo { get; set; }

        [Display(Name = "Padrão")]
        public bool? Padrao { get; set; }
    }
}