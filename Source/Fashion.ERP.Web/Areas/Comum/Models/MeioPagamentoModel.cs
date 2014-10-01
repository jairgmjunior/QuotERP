using System.ComponentModel.DataAnnotations;
using Fashion.ERP.Web.Models;

namespace Fashion.ERP.Web.Areas.Comum.Models
{
    public class MeioPagamentoModel : IModel
    {
        public long? Id { get; set; }

        [Display(Name = "Descrição")]
        [Required(ErrorMessage = "Informe uma descrição do meio de pagamento.")]
        [StringLength(100, ErrorMessage = "{0} não deve ser maior que {1} caracteres")]
        public string Descricao { get; set; }
    }
}