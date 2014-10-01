using System.ComponentModel.DataAnnotations;

namespace Fashion.ERP.Web.Areas.Almoxarifado.Models
{
    public class GridMaterialModel
    {
        public long Id { get; set; }
        
        [Display(Name = "Referência")]
        public string Referencia { get; set; }

        [Display(Name = "Descrição")]
        public string Descricao { get; set; }

        [Display(Name = "Marca do material")]
        public string MarcaMaterial { get; set; }

        [Display(Name = "Categoria")]
        public string Categoria { get; set; }

        [Display(Name = "Subcategoria")]
        public string Subcategoria { get; set; }

        [Display(Name = "Família")]
        public string Familia { get; set; }

        [Display(Name = "Unidade de medida")]
        public string UnidadeMedida { get; set; }

        [Display(Name = "Foto")]
        public string Foto { get; set; }

        [Display(Name = "Ativo")]
        public bool Ativo { get; set; }
    }
}