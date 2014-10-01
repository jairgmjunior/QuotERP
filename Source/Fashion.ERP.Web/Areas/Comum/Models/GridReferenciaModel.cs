using System.ComponentModel.DataAnnotations;

namespace Fashion.ERP.Web.Areas.Comum.Models
{
    public class GridReferenciaModel
    {
        public long Id { get; set; }

        [Display(Name = "Tipo")]
        public string TipoReferencia { get; set; }

        [Display(Name = "Nome")]
        public string Nome { get; set; }

        [Display(Name = "Telefone")]
        public string Telefone { get; set; }

        [Display(Name = "Celular")]
        public string Celular { get; set; }

        [Display(Name = "Observação")]
        public string Observacao { get; set; } 
    }
}