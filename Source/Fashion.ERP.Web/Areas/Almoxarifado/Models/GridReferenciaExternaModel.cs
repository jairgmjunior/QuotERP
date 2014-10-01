using System.ComponentModel.DataAnnotations;

namespace Fashion.ERP.Web.Areas.Almoxarifado.Models
{
    public class GridReferenciaExternaModel
    {
        public long Id { get; set; }

        [Display(Name = "Referência")]
        public string Referencia { get; set; }

        [Display(Name = "Descrição")]
        public string Descricao { get; set; }

        public long MaterialId { get; set; }
        public string MaterialReferencia { get; set; }
        public string MaterialDescricao { get; set; }
        public string Material { get; set; }
    }
}