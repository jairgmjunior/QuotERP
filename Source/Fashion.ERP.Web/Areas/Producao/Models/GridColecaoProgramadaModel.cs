using System.ComponentModel.DataAnnotations;
using Fashion.ERP.Web.Models;

namespace Fashion.ERP.Web.Areas.Producao.Models
{
    public class GridColecaoProgramadaModel : IModel
    {
        public long? Id { get; set; }

        [Display(Name = "Coleção")]
        public string Colecao { get; set; }

        [Display(Name = "Fichas Técnicas Programadas")]
        public long QtdeFichasTecnicasProgramadas { get; set; }
    }
}