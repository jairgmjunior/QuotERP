using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Fashion.ERP.Web.Models;

namespace Fashion.ERP.Web.Areas.Compras.Models
{
    public class PedidoCompraCancelamentoModel : IModel
    {
        public long? Id { get; set; }
        
        [Display(Name = "Número")]
        public long Numero { get; set; }

        [Display(Name = "Fornecedor")]
        public string FornecedorNome { get; set; }

        [Display(Name = "Valor da compra")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public double ValorCompra { get; set; }

        [Display(Name = "Prazo")]
        public string PrazoDescricao { get; set; }

        [Display(Name = "Meio de pagamento")]
        public string MeioPagamentoDescricao { get; set; }

        [Display(Name = "Situação")]
        public String SituacaoCompraDescricao { get; set; }

        [Display(Name = "Comprador")]
        public string CompradorNome { get; set; }
        
        [Display(Name = "Data da compra")]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime? DataCompra { get; set; }

        [Display(Name = "Previsão de entrega")]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime? PrevisaoEntrega { get; set; }

        [Display(Name = "Observação")]
        public string Observacao { get; set; }
        
        public bool VerificaMarcados { get; set; }

        [Display(Name = "Observação")]
        [StringLength(4000, ErrorMessage = "{0} não deve ser maior que {1} caracteres")]
        [DataType(DataType.MultilineText)]
        public string ObservacaoCancelamento { get; set; }

        [Display(Name = "Motivo do cancelamento")]
        [Required(ErrorMessage = "Informe o Motivo do Cancelamento")]
        public long MotivoCancelamento { get; set; }
        
        [Display(Name = "Unidade estocadora")]
        public string UnidadeEstocadoraNomeFantasia { get; set; }

        public IList<GridPedidoCompraItemCanceladoModel> GridItemCancelado { get; set; }

    }
}