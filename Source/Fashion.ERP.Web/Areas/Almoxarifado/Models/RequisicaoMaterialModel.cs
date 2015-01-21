using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Fashion.ERP.Domain.Almoxarifado;
using Fashion.ERP.Web.Models;

namespace Fashion.ERP.Web.Areas.Almoxarifado.Models
{
    public class RequisicaoMaterialModel : IModel
    {
        public long? Id { get; set; }
        
        [Display(Name = "Número")]
        public long? Numero { get; set; }

        [Display(Name = "Data")]
        public DateTime? Data { get; set; }
        
        [Display(Name = "Observação")]
        [DataType(DataType.MultilineText)]
        public string Observacao { get; set; }

        [Display(Name = "Requerente")]
        [Required(ErrorMessage = "Informe o requerente")]
        public long? Requerente { get; set; }
        
        [Display(Name = "Unidade Requisitada")]
        [Required(ErrorMessage = "Informe a unidade requisitada")]
        public long? UnidadeRequisitada { get; set; }

        [Display(Name = "Unidade Requerente")]
        [Required(ErrorMessage = "Informe a unidade requerente")]
        public long? UnidadeRequerente { get; set; }
        
        [Display(Name = "Centro de Custo")]
        [Range(1, int.MaxValue)]
        [Required(ErrorMessage = "Informe o centro de custo")]
        public long? CentroCusto { get; set; }

        [Display(Name = "Tipo de Material")]
        [Required(ErrorMessage = "Informe o tipo de material")]
        public long TipoItem { get; set; }
        
        [Display(Name = "Origem")]
        public String Origem { get; set; }

        [Display(Name = "Situação")]
        public SituacaoRequisicaoMaterial SituacaoRequisicaoMaterial{ get; set; }

        public IList<RequisicaoMaterialItemModel> GridItens { get; set; }
    }
}