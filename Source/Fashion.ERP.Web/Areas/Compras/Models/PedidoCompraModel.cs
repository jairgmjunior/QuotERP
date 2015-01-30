using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Fashion.ERP.Domain.Compras;
using Fashion.ERP.Web.Areas.Comum.Models;
using Fashion.ERP.Web.Models;

namespace Fashion.ERP.Web.Areas.Compras.Models
{
    public class PedidoCompraModel : IModel
    {
        public PedidoCompraModel()
        {
            PedidoCompraItens = new List<long?>();
            Materiais = new List<long>();
            UnidadeMedidas = new List<long>();
            Referenciaexterna = new List<long>();
            ValorUnitarios = new List<double>();
            ValorTotais = new List<double>();
            Quantidades = new List<double>();
            SituacaoCompras = new List<SituacaoCompra>();
        }

        public IList<long?> PedidoCompraItens { get; set; }
        public IList<long> Materiais { get; set; }
        public IList<long> UnidadeMedidas { get; set; }
        public IList<long> Referenciaexterna { get; set; }
        public IList<double> ValorUnitarios { get; set; }
        public IList<double> ValorTotais { get; set; }
        public IList<double> Quantidades { get; set; }
        public IList<SituacaoCompra> SituacaoCompras { get; set; }

        public long? Id { get; set; }

        [Display(Name = "Número")]
        [Required(ErrorMessage = "Informe o número do pedido de compra")]
        [Range(1, int.MaxValue)]
        public long Numero { get; set; }

        [Display(Name = "Data compra")]
        [Required(ErrorMessage = "Informe a data da compra")]
        public DateTime? DataCompra { get; set; }

        [Display(Name = "Previsão faturamento")]
        [Required(ErrorMessage = "Informe a data da previsão do faturamento")]
        public DateTime? PrevisaoFaturamento { get; set; }

        [Display(Name = "Previsão entrega")]
        [Required(ErrorMessage = "Informe a data de previsão da entrega")]
        public DateTime? PrevisaoEntrega { get; set; }

        [Display(Name = "Frete")]
        [Required(ErrorMessage = "Informe o tipo de frete")]
        public TipoCobrancaFrete TipoCobrancaFrete { get; set; }

        [Display(Name = "Valor frete")]
        [Required(ErrorMessage = "Informe o valor do frete")]
        [UIHint("currency4casasdecimais")]
        public double ValorFrete { get; set; }

        [Display(Name = "Valor desconto")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        [Required(ErrorMessage = "Informe o valor do desconto")]
        [UIHint("currency4casasdecimais")]
        public double ValorDesconto { get; set; }

        [Display(Name = "Valor compra")]
        [Required(ErrorMessage = "Informe o valor da compra")]
        [UIHint("currency4casasdecimais")]
        public double ValorCompra { get; set; }

        [Display(Name = "Valor encargos")]
        [Required(ErrorMessage = "Informe o valor dos encargos")]
        [UIHint("currency4casasdecimais")]
        public double ValorEncargos { get; set; }

        [Display(Name = "Valor embalagem")]
        [Required(ErrorMessage = "Informe o valor da embalagem")]
        [UIHint("currency4casasdecimais")]
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

        [Display(Name = "Transportadora")]
        public long? Transportadora { get; set; }

        [Display(Name = "Valor Líquido")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public double? ValorLiquido { get; set; }


        [Display(Name = "Valor Mercadorias")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public double? ValorMercadorias { get; set; }

        public IList<GridPedidoCompraModel> Grid { get; set; }

        public IList<GridPedidoCompraItem> GridItens { get; set; }
        public IList<GridPedidoCompraItemDetalhe> GridPedidoItemDetalhe { get; set; }
    }
}