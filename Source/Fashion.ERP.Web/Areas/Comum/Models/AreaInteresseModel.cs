using System.ComponentModel.DataAnnotations;
using Fashion.ERP.Web.Models;

namespace Fashion.ERP.Web.Areas.Comum.Models
{
    public class AreaInteresseModel : IModel
    {
        public long? Id { get; set; }

        [Display(Name = "Nome")]
        [Required(ErrorMessage = "Informe o nome da área de interesse")]
        [StringLength(100, ErrorMessage = "{0} não deve ser maior que {1} caracteres")]
        public string Nome { get; set; }
    }
}