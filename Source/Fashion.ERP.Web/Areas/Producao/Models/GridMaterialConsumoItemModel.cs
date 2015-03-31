using System;
using System.ComponentModel.DataAnnotations;
using Fashion.ERP.Web.Models;

namespace Fashion.ERP.Web.Areas.Producao.Models
{
    public class GridMaterialConsumoItemModel : IModel
    {
        public long? Id { get; set; }

        [Display(Name = "Variação")]
        [Required(ErrorMessage = "Informe a variação")]
        public string Variacao { get; set; }

        [Display(Name = "Tamanho")]
        [Required(ErrorMessage = "Informe o tamanho")]
        public string Tamanho { get; set; }

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

        [Display(Name = "Compõe Custo")]
        public Boolean? CompoeCusto { get; set; }

        [Display(Name = "Departamento")]
        [Required(ErrorMessage = "Informe o departamento")]
        public string DepartamentoProducao { get; set; }
    }
}