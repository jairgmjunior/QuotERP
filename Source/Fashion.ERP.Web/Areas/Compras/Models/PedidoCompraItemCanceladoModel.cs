using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Fashion.ERP.Domain.Compras;
using Fashion.ERP.Web.Areas.Comum.Models;
using Fashion.ERP.Web.Models;

namespace Fashion.ERP.Web.Areas.Compras.Models
{
    public class PedidoCompraItemCanceladoModel : IModel
    {
        public long? Id { get; set; }

        [Display(Name = "Observação")]
        [StringLength(4000, ErrorMessage = "{0} não deve ser maior que {1} caracteres")]
        [DataType(DataType.MultilineText)]
        public string Observacao { get; set; }
             
        [Display(Name = "Motivo Cancelamento")]
        [Required(ErrorMessage = "Informe o Motivo do Cancelamento")]
        public long MotivoCancelamento { get; set; }

        [Display(Name = "Data cancelamento")]
        [Required(ErrorMessage = "Informe a data de cancelamento")]
        public DateTime DataCancelamento { get; set; }

        [Display(Name = "Unidade estocadora")]     
        public long? UnidadeEstocadora { get; set; }

        [Display(Name = "Número")]
        public long Numero { get; set; }           

        [Display(Name = "Fornecedor")]
        public long? Fornecedor { get; set; }

        [Display(Name = "Valor compra")]
        public double ValorCompra { get; set; }

        [Display(Name = "PrazoDescricao")]
        public long? Prazo { get; set; }

        [Display(Name = "Meio de pagamento")]
        public long? MeioPagamento { get; set; }

        [Display(Name = "Situação compra")]
        public SituacaoCompra SituacaoCompra { get; set; }

        [Display(Name = "Comprador")]
        public long? Comprador { get; set; }

        [Display(Name = "Data compra")]
        public DateTime? DataCompra { get; set; }

        public IList<GridPedidoCompraItemCanceladoModel> GridItemCancelado { get; set; } 
       
    }
}