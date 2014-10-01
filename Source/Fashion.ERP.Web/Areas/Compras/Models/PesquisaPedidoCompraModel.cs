using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Fashion.ERP.Domain.Compras;

namespace Fashion.ERP.Web.Areas.Compras.Models
{
    public class PesquisaPedidoCompraModel
    {
        [Display(Name = "Unidade estocadora")]
        public long? UnidadeEstocadora { get; set; }

        [Display(Name = "Número")]
        public long? Numero { get; set; }

        [Display(Name = "Fornecedor")]
        public long? Fornecedor { get; set; }

        [Display(Name = "Situação")]
        public SituacaoCompra? SituacaoCompra { get; set; }

        [Display(Name = "Data compra")]
        public DateTime? DataCompraInicio { get; set; }

        [Display(Name = "Até")]
        public DateTime? DataCompraFim { get; set; }

        [Display(Name = "Valor compra")]
        public double? ValorCompraInicio { get; set; }

        [Display(Name = "Até")]
        public double? ValorCompraFim { get; set; }

        [Display(Name = "Previsão faturamento")]
        public DateTime? PrevisaoFaturamentoInicio { get; set; }

        [Display(Name = "Até")]
        public DateTime? PrevisaoFaturamentoFim { get; set; }

        [Display(Name = "Previsão entrega")]
        public DateTime? PrevisaoEntregaInicio { get; set; }

        [Display(Name = "Até")]
        public DateTime? PrevisaoEntregaFim { get; set; }

        [Display(Name = "Referência")]
        public long? Material { get; set; }

        [Display(Name = "Comprador")]
        public long? Comprador { get; set; }

        public string ModoConsulta { get; set; }

        [Display(Name = "Agrupar por")]
        public string AgruparPor { get; set; }

        [Display(Name = "Ordenar por")]
        public string OrdenarPor { get; set; }

        [Display(Name = "em")]
        public string OrdenarEm { get; set; }

        public IList<GridPedidoCompraModel> Grid { get; set; }
    }
}