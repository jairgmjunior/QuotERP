using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Fashion.ERP.Domain.Compras;

namespace Fashion.ERP.Web.Areas.Compras.Models
{
    public class PesquisaOrdemEntradaCompraModel
    {
        [Display(Name = "Número")]
        public long? Numero { get; set; }

        [Display(Name = "Unidade estocadora")]
        public long? UnidadeEstocadora { get; set; }

        [Display(Name = "Fornecedor")]
        public long? Fornecedor { get; set; }

        [Display(Name = "Situação")]
        public SituacaoOrdemEntradaCompra? SituacaoOrdemEntradaCompra { get; set; }

        [Display(Name = "Data")]
        public DateTime? DataInicial { get; set; }
        
        [Display(Name = "Data Final")]
        public DateTime? DataFinal { get; set; }

        public IList<GridOrdemEntradaCompraModel> Grid { get; set; } 
    }
}