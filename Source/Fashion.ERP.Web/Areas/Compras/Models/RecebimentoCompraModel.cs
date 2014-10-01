using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Fashion.ERP.Domain.Compras;
using Fashion.ERP.Web.Models;

namespace Fashion.ERP.Web.Areas.Compras.Models
{
    public class RecebimentoCompraModel : IModel
    {
        public long? Id { get; set; }

        [Display(Name = "Número")]
        [Required(ErrorMessage = "Informe o número do pedido de compra")]
        [Range(1, int.MaxValue)]
        public long Numero { get; set; }
        
        [Display(Name = "Situação")]
        public SituacaoRecebimentoCompra SituacaoRecebimentoCompra { get; set; }
        
        [Display(Name = "Data")]
        [Required(ErrorMessage = "Informe a data")]
        public DateTime Data { get; set; }

        [Display(Name = "Valor")]
        [Required(ErrorMessage = "Informe o valor")]
        public double Valor { get; set; }

        [Display(Name = "Observação")]
        [StringLength(4000, ErrorMessage = "{0} não deve ser maior que {1} caracteres")]
        [DataType(DataType.MultilineText)]
        public string Observacao { get; set; }

        [Display(Name = "Comprador")]
        [Required(ErrorMessage = "Informe o comprador")]
        public long? Comprador { get; set; }

        [Display(Name = "Fornecedor")]
        [Required(ErrorMessage = "Informe o fornecedor")]
        public long? Fornecedor { get; set; }

        [Display(Name = "Unidade")]
        [Required(ErrorMessage = "Informe a unidade")]
        public long? Unidade { get; set; }

        public IList<RecebimentoCompraItemModel> Itens { get; set; }
    }
}