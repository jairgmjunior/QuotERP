using System.ComponentModel.DataAnnotations;

namespace Fashion.ERP.Web.Areas.Comum.Models
{
    public class GridContatoModel
    {
        public long Id { get; set; }

        [Display(Name = "Tipo")]
        public string TipoContato { get; set; }
        
        [Display(Name = "Nome")]
        public string Nome { get; set; }

        [Display(Name = "Telefone")]
        public string Telefone { get; set; }

        [Display(Name = "Operadora")]
        public string Operadora { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; } 
    }
}