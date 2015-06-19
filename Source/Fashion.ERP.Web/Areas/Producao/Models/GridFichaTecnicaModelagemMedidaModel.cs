using System.ComponentModel.DataAnnotations;
using Fashion.ERP.Web.Models;

namespace Fashion.ERP.Web.Areas.Producao.Models
{
    public class GridFichaTecnicaModelagemMedidaModel : IModel
    {
        public long? Id { get; set; }

        [Display(Name = "Tamanho")]
        [Required(ErrorMessage = "Informe o tamanho")]
        public string Tamanho { get; set; }

        [Display(Name = "Descrição")]
        [Required(ErrorMessage = "Informe a descrição")]
        public string DescricaoMedida { get; set; }

        [Display(Name = "Medida")]
        public double Medida { get; set; }
    }
}