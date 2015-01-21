using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Fashion.ERP.Web.Areas.Compras.Models;
using Fashion.ERP.Web.Models;

namespace Fashion.ERP.Web.Areas.Almoxarifado.Models
{
    public class RequisicaoMaterialBaixaModel : IModel
    {
        public long? Id { get; set; }

        [Display(Name = "Número")]
        public long? Numero { get; set; }

        [Display(Name = "Data")]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime? Data { get; set; }

        [Display(Name = "Observação")]
        [DataType(DataType.MultilineText)]
        public string Observacao { get; set; }

        [Display(Name = "Requerente")]
        public string RequerenteNome { get; set; }

        public long? Requerente { get; set; }

        [Display(Name = "Unidade Requisitada")]
        public string UnidadeRequisitadaNomeFantasia { get; set; }
        
        public long? UnidadeRequisitada { get; set; }

        [Display(Name = "Unidade Requerente")]
        public string UnidadeRequerenteNomeFantasia { get; set; }

        [Display(Name = "Centro de Custo")]
        public string CentroCustoNome { get; set; }

        [Display(Name = "Tipo de Material")]
        public string TipoItemDescricao { get; set; }

        [Display(Name = "Origem")]
        public String Origem { get; set; }

        [Display(Name = "Situação")]
        public string SituacaoRequisicaoMaterialDescricao { get; set; }

        [Display(Name = "Depósito Material")]
        [Required(ErrorMessage = "Informe o depósito material")]
        public long? DepositoMaterial { get; set; }
        
        public IList<GridRequisicaoMaterialItemBaixaModel> GridItems { get; set; } 
    }
}