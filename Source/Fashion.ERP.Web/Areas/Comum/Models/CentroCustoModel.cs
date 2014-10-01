using System.ComponentModel.DataAnnotations;
using Fashion.ERP.Web.Models;

namespace Fashion.ERP.Web.Areas.Comum.Models
{
    public class CentroCustoModel : IModel
    {
        public long? Id { get; set; }

        [Display(Name = "Código")]
        public long Codigo { get; set; }

        [Display(Name = "Nome")]
        [Required(ErrorMessage = "Informe o nome")]
        [StringLength(60, ErrorMessage = "{0} não deve ser maior que {1} caracteres")]
        public string Nome { get; set; }
    }
}