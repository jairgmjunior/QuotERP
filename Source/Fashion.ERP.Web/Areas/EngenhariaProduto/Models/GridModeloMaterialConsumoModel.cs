using System.ComponentModel.DataAnnotations;

namespace Fashion.ERP.Web.Areas.EngenhariaProduto.Models
{
    public class GridModeloMaterialConsumoModel
    {
        public long? Id { get; set; }
        
        [Display(Name = "Departamento de Consumo")]
        public string DepartamentoProducao { get; set; }

        [Display(Name = "Und.")]
        public string UnidadeMedida { get; set; }

        [Display(Name = "Referência")]
        public string Referencia { get; set; }

        [Display(Name = "Descrição")]
        public string Descricao { get; set; }

        [Display(Name = "Quantidade")]
        public double Quantidade { get; set; }
        
        [Display(Name = "Foto")]
        public string Foto { get; set; }
    }
}