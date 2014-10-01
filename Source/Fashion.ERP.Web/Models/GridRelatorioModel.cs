using System.ComponentModel.DataAnnotations;

namespace Fashion.ERP.Web.Models
{
    public class GridRelatorioModel
    {
        public long Id { get; set; }

        [Display(Name = "Nome")]
        public string Nome { get; set; } 
    }
}