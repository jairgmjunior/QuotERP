using System.ComponentModel.DataAnnotations;
using Fashion.ERP.Web.Models;

namespace Fashion.ERP.Web.Areas.Comum.Models
{
    public class TipoFornecedorModel : IModel
    {
        public long? Id { get; set; }

        [Display(Name = "Descrição")]
        [StringLength(100, ErrorMessage = "{0} não deve ser maior que {1} caracteres")]
        [Required(ErrorMessage = "Informe uma descrição")]
        public string Descricao { get; set; }
    }
}