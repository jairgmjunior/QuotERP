using System.ComponentModel.DataAnnotations;

namespace Fashion.ERP.Web.Areas.EngenhariaProduto.Models
{
    public class GridMaterialComposicaoModel
    {
        public long? Id { get; set; }
        
        [Display(Name = "Departamento")]
        public long Departamento { get; set; }
        
        [Display(Name = "Id setor")]
        public long? IdSetor { get; set; }

        [Display(Name = "Variacao")]
        public long? Variacao { get; set; }

        [Display(Name = "Cor")]
        public long? Cor { get; set; }

        [Display(Name = "Tamanho")]
        public long? Tamanho { get; set; }

        [Display(Name = "Referência")]
        public long Referencia { get; set; }

        [Display(Name = "Unidade Medida")]
        public long UnidadeMedida { get; set; }

        [Display(Name = "Quantidade")]
        public double Quantidade { get; set; } 
    }
}