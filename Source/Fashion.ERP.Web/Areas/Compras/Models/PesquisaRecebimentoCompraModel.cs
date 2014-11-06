using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Fashion.ERP.Domain.Compras;

namespace Fashion.ERP.Web.Areas.Compras.Models
{
    public class PesquisaRecebimentoCompraModel
    {
        [Display(Name = "Unidade")]
        public long? UnidadeEstocadora { get; set; }

        [Display(Name = "Número")]
        public long? Numero { get; set; }

        [Display(Name = "Fornecedor")]
        public long? Fornecedor { get; set; }

        [Display(Name = "Referência")]
        public long? Material { get; set; }

        [Display(Name = "Comprador")]
        public long? Comprador { get; set; }
        
        [Display(Name = "Data")]
        public DateTime? DataInicio { get; set; }

        [Display(Name = "Até")]
        public DateTime? DataFim { get; set; }
        
        [Display(Name = "Pedido de compra")]
        public long? NumeroPedidoCompra { get; set; }
        
        [Display(Name = "Situação")]
        public SituacaoRecebimentoCompra? SituacaoRecebimentoCompra { get; set; }
        
        [Display(Name = "Valor")]
        public double? ValorInicio { get; set; }

        [Display(Name = "Até")]
        public double? ValorFim { get; set; }

        public string ModoConsulta { get; set; }

        [Display(Name = "Agrupar por")]
        public string AgruparPor { get; set; }

        [Display(Name = "Ordenar por")]
        public string OrdenarPor { get; set; }

        [Display(Name = "em")]
        public string OrdenarEm { get; set; }

        public IList<GridRecebimentoCompraModel> Grid { get; set; }
    }
}