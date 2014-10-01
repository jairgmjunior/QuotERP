using System.ComponentModel.DataAnnotations;

namespace Fashion.ERP.Web.Areas.Comum.Models
{
    public class GridProfissaoModel
    {
        public long Id { get; set; }
        
        [Display(Name = "Nome")]
        public string Nome { get; set; } 
    }
}