using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Fashion.ERP.Domain.Compras;
using Fashion.ERP.Web.Models;

namespace Fashion.ERP.Web.Areas.Compras.Models
{
    public class PedidoCompraModel : IModel
    {
        public long? Id { get; set; }

        [Display(Name = "Número")]
        [Range(1, int.MaxValue)]
        public long? Numero { get; set; }

        [Display(Name = "Data da compra")]
        [Required(ErrorMessage = "Informe a data da compra")]
        public DateTime? DataCompra { get; set; }

        [Display(Name = "Previsão de faturamento")]
        [Required(ErrorMessage = "Informe a data da previsão do faturamento")]
        public DateTime? PrevisaoFaturamento { get; set; }

        [Display(Name = "Previsão de entrega")]
        [Required(ErrorMessage = "Informe a data de previsão da entrega")]
        public DateTime? PrevisaoEntrega { get; set; }

        [Display(Name = "Frete")]
        [Required(ErrorMessage = "Informe o tipo de frete")]
        public TipoCobrancaFrete TipoCobrancaFrete { get; set; }

        [Display(Name = "Valor do frete")]
        [Required(ErrorMessage = "Informe o valor do frete")]
        [UIHint("currency5casasdecimais")]
        public double ValorFrete { get; set; }

        [Display(Name = "Valor do desconto")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        [Required(ErrorMessage = "Informe o valor do desconto")]
        [UIHint("currency5casasdecimais")]
        public double ValorDescontoTotal { get; set; }

        [Display(Name = "Valor dos encargos")]
        [Required(ErrorMessage = "Informe o valor dos encargos")]
        [UIHint("currency5casasdecimais")]
        public double ValorEncargos { get; set; }

        [Display(Name = "Valor da embalagem")]
        [Required(ErrorMessage = "Informe o valor da embalagem")]
        [UIHint("currency5casasdecimais")]
        public double ValorEmbalagem { get; set; }

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

        [Display(Name = "Unidade estocadora")]
        [Required(ErrorMessage = "Informe a unidade estocadora")]
        public long? UnidadeEstocadora { get; set; }

        [Display(Name = "Prazo")]
        public long? Prazo { get; set; }

        [Display(Name = "Meio de pagamento")]
        public long? MeioPagamento { get; set; }

        [Display(Name = "Situação")]
        public SituacaoCompra SituacaoCompra { get; set; }

        [Display(Name = "Contato")]
        [StringLength(50, ErrorMessage = "{0} não deve ser maior que {1} caracteres")]
        public string Contato { get; set; }

        [Display(Name = "Data da autorização")]
        public virtual DateTime? DataAutorizacao { get; set; }

        [Display(Name = "Autorizado por")]
        public virtual String FuncionarioAutorizador { get; set; }

        public virtual bool Autorizado { get; set; }
        
        [Display(Name = "Transportadora")]
        public long? Transportadora { get; set; }
        
        [Display(Name = "Valor da compra")]
        [Required(ErrorMessage = "Informe o valor da compra")]
        [UIHint("currency5casasdecimais")]
        public double? ValorCompra { get; set; }
        
        [Display(Name = "Valor das mercadorias")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public double? ValorMercadorias { get; set; }

        public IList<GridPedidoCompraModel> Grid { get; set; }

        public IList<GridPedidoCompraItem> GridItens { get; set; }
    }
}