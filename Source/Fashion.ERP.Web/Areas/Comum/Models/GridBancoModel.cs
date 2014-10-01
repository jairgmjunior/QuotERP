using System.ComponentModel.DataAnnotations;

namespace Fashion.ERP.Web.Areas.Comum.Models
{
    public class GridBancoModel
    {
        public long Id { get; set; }
        
        [Display(Name = "Código")]
        public int Codigo { get; set; }
        
        [Display(Name = "Nome")]
        public string Nome { get; set; }
    }
}