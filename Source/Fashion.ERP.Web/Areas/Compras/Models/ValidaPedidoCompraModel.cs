using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Fashion.ERP.Web.Areas.Compras.Models
{
    public class ValidaPedidoCompraModel
    {
        public long? Id { get; set; }

        public bool Autorizado { get; set; }

        [Display(Name = "Unidade estocadora")]
        public string UnidadeEstocadora { get; set; }

        [Display(Name = "Número")]
        public long Numero { get; set; }

        [Display(Name = "Fornecedor")]
        public string Fornecedor { get; set; }

        [Display(Name = "Comprador")]
        public string Comprador { get; set; }

        [Display(Name = "Prazo")]
        public string Prazo { get; set; }

        [Display(Name = "Observação")]
        public string Observacao { get; set; }

        [Display(Name = "Situação")]
        public string SituacaoCompra { get; set; }

        [Display(Name = "Data da compra")]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime? DataCompra { get; set; }

        [Display(Name = "Previsão de entrega")]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime? PrevisaoEntrega { get; set; }

        [Display(Name = "Meio de pagamento")]
        public string MeioPagamento { get; set; }

        [Display(Name = "Valor da compra")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public double ValorCompra { get; set; }

        [Display(Name = "Funcionário")]
        [Required(ErrorMessage = "Informe o fornecedor")]
        public long? Funcionario { get; set; }

        [Display(Name = "Assinatura")]
        [Required(ErrorMessage = "Informe sua senha")]
        public string Assinatura { get; set; }

        [Display(Name = "Observação")]
        [StringLength(4000, ErrorMessage = "{0} não deve ser maior que {1} caracteres")]
        [DataType(DataType.MultilineText)]
        public string ObservacaoValidacao { get; set; }

        public IList<ValidaPedidoCompraItemModel> GridItensPedidoCompra { get; set; }
    }
}