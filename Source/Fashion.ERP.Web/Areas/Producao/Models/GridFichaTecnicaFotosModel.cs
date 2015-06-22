using System.ComponentModel.DataAnnotations;
using Fashion.ERP.Web.Models;

namespace Fashion.ERP.Web.Areas.Producao.Models
{
    public class GridFichaTecnicaFotosModel : IModel
    {
        public long? Id { get; set; }
        
        [Display(Name = "Impressão")]
        public bool Impressao { get; set; }

        [Display(Name = "Padrão")]
        public bool Padrao { get; set; }

        [Display(Name = "Foto")]
        public string FotoNome { get; set; }

        [Display(Name = "Título")]
        public string FotoTitulo { get; set; }
    }
}