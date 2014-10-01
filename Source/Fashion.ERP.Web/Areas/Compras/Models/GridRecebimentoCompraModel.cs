using System;
using System.ComponentModel.DataAnnotations;
using Fashion.ERP.Domain.Compras;
using Fashion.ERP.Web.Models;

namespace Fashion.ERP.Web.Areas.Compras.Models
{
    public class GridRecebimentoCompraModel : IModel
    {
        public long? Id { get; set; }

        [Display(Name = "Número")]
        public long? Numero { get; set; }

        [Display(Name = "Fornecedor")]
        public string FornecedorNome { get; set; }
        
        [Display(Name = "Data")]
        public DateTime? Data { get; set; }
        
        [Display(Name = "Valor")]
        public double Valor { get; set; }
        
        [Display(Name = "Situação")]
        public SituacaoRecebimentoCompra SituacaoRecebimentoCompra { get; set; }
    }
}