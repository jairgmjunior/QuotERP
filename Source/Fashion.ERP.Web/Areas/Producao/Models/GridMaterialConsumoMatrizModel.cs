using System;
using System.ComponentModel.DataAnnotations;
using Fashion.ERP.Web.Models;

namespace Fashion.ERP.Web.Areas.Producao.Models
{
    public class GridMaterialConsumoMatrizModel : IModel
    {
        public long? Id { get; set; }
        
        [Display(Name = "Referência")]
        [Required(ErrorMessage = "Informe a referência")]
        public string Referencia { get; set; }

        [Display(Name = "Descrição")]
        public string Descricao { get; set; }

        [Display(Name = "Unidade")]
        public string UnidadeMedida { get; set; }

        [Display(Name = "Custo Unit.(R$)")]
        [Required(ErrorMessage = "Informe o custo")]
        public double? Custo { get; set; }

        [Display(Name = "Custo Tot.(R$)")]
        public double? CustoTotal { get; set; }
        
        [Display(Name = "Quantidade")]
        [Required(ErrorMessage = "Informe a quantidade")]
        public double? Quantidade { get; set; }

        [Display(Name = "Departamento")]
        [Required(ErrorMessage = "Informe o departamento")]
        public string DepartamentoProducao { get; set; }

        [Display(Name = "Foto")]
        public string Foto { get; set; }
    }
}