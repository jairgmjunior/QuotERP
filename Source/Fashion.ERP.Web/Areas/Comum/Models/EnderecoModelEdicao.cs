using System.ComponentModel.DataAnnotations;

namespace Fashion.ERP.Web.Areas.Comum.Models
{
    public class EnderecoModelEdicao : EnderecoModel
    {
        [Display(Name = "Estado")]
        public long? ufId { get; set; }
    }
}