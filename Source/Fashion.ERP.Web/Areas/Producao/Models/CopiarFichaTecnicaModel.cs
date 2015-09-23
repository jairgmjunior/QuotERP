using System.ComponentModel.DataAnnotations;

namespace Fashion.ERP.Web.Areas.Producao.Models
{
    public class CopiarFichaTecnicaModel
    {
        [Display(Name = "Referência")]
        public string ReferenciaOrigem { get; set; }

        [Display(Name = "Descrição")]
        public string DescricaoOrigem { get; set; }

        [Display(Name = "Referência")]
        [Required(ErrorMessage = "Informe a referência para um novo modelo")]
        public string ReferenciaNova { get; set; }

        [Display(Name = "Observação")]
        [DataType(DataType.MultilineText)]
        public string Observacao { get; set; }
    }
}