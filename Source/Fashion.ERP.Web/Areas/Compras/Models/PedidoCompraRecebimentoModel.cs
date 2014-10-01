using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Fashion.ERP.Web.Models;

namespace Fashion.ERP.Web.Areas.Compras.Models
{
    public class PedidoCompraRecebimentoModel : IModel
    {
        public long? Id { get; set; }

        [Display(Name = "Número")]
        public long Numero { get; set; }

        [Display(Name = "Data")]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime DataCompra { get; set; }
        
        [Display(Name = "Previsão de entrega")]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime PrevisaoEntrega { get; set; }
        
        [Display(Name = "Fornecedor")]
        public string FornecedorNome { get; set; }

        [Display(Name = "Comprador")]
        public string CompradorNome { get; set; }

        public IList<PedidoCompraItemRecebimentoModel> Grid { get; set; }
    }
}