using System.ComponentModel.DataAnnotations;
using Fashion.ERP.Web.Models;

namespace Fashion.ERP.Web.Areas.Producao.Models
{
    public class ProducaoItemModel : IModel
    {
        public long? Id { get; set; }

        [Display(Name = "Tag/Ano")]
        public string TagAno { get; set; }

        [Display(Name = "Referência")]
        public string Referencia { get; set; }

        [Display(Name = "Descrição")]
        public string Descricao { get; set; }

        [Display(Name = "Estilista")]
        public string Estilista { get; set; }
        
        [Display(Name = "Foto")]
        public string Foto { get; set; }
        
        public string MatrizCorteJson { get; set; }
    }
}