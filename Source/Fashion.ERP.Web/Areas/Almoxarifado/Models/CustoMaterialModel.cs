using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Fashion.ERP.Web.Areas.Almoxarifado.Models
{
    public class CustoMaterialModel
    {
        public long Id { get; set; }

        [Display(Name = "Referência")]
        public string Material { get; set; }

        [Display(Name = "Descrição")]
        public string Descricao { get; set; }

        [Display(Name = "Custo no cadastro do material")]
        [DisplayFormat(DataFormatString = "{0:C5}")]
        public double CustoAtual { get; set; }

        [Display(Name = "Unidade de medida")]
        public string UnidadeMedida { get; set; }
        
        public string Foto { get; set; }
        
        public List<GridCustoMaterialModel> Grid { get; set; }
    }
}