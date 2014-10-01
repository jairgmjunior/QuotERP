using System.ComponentModel.DataAnnotations;
using Fashion.ERP.Web.Models;

namespace Fashion.ERP.Web.Areas.Almoxarifado.Models
{
    public class UnidadeMedidaModel: IModel
    {
        public long? Id { get; set; }

        [Display(Name = "Sigla")]
        [Required(ErrorMessage = "Informe a sigla")]
        [StringLength(10, ErrorMessage = "{0} não deve ser maior que {1} caracteres")]
        public string Sigla { get; set; }

        [Display(Name = "Descrição")]
        [Required(ErrorMessage = "Informe a descrição")]
        [StringLength(60, ErrorMessage = "{0} não deve ser maior que {1} caracteres")]
        public string Descricao { get; set; }

        [Display(Name = "Fator multiplicativo (%)")]
        [Required(ErrorMessage = "Informe o fator multiplicativo")]
        public double FatorMultiplicativo { get; set; }
    }
}