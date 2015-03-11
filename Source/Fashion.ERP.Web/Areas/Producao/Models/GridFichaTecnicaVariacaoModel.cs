using System.ComponentModel.DataAnnotations;

namespace Fashion.ERP.Web.Areas.Producao.Models
{
    public class GridFichaTecnicaVariacaoModel
    {
        public long Id { get; set; }

        [Display(Name = "Variação")]
        public string Variacao { get; set; }

        [Display(Name = "Cor")]
        public string Cor { get; set; }
    }
}