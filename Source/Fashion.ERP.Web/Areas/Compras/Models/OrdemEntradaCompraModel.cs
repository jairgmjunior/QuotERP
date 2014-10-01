using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Fashion.ERP.Web.Models;

namespace Fashion.ERP.Web.Areas.Compras.Models
{
    public class OrdemEntradaCompraModel : IModel
    {
        public OrdemEntradaCompraModel()
        {
            Itens = new List<long?>();
            PedidoCompras = new List<long>();
            Datas = new List<DateTime>();
            Materiais = new List<long>();
            UnidadeMedidas = new List<long>();
            Quantidades = new List<double>();
            ValorUnitarios = new List<double>();
            ValorTotais = new List<double>();
        }

        public IList<long?> Itens { get; set; }
        public IList<long> PedidoCompras { get; set; }
        public IList<DateTime> Datas { get; set; }
        public IList<long> Materiais { get; set; }
        public IList<long> UnidadeMedidas { get; set; }
        public IList<double> Quantidades { get; set; }
        public IList<double> ValorUnitarios { get; set; }
        public IList<double> ValorTotais { get; set; }

        public long? Id { get; set; }

        [Display(Name = "Unidade estocadora")]
        [Required(ErrorMessage = "Informe a unidade estocadora")]
        public long? UnidadeEstocadora { get; set; }

        [Display(Name = "Número")]
        [Required(ErrorMessage = "Informe o número do pedido de compra")]
        [Range(1, int.MaxValue)]
        public long Numero { get; set; }

        [Display(Name = "Comprador")]
        [Required(ErrorMessage = "Informe o comprador")]
        public long? Comprador { get; set; }

        [Display(Name = "Fornecedor")]
        [Required(ErrorMessage = "Informe o fornecedor")]
        public long? Fornecedor { get; set; }

        [Display(Name = "Data compra")]
        [Required(ErrorMessage = "Informe a data da compra")]
        public DateTime? Data { get; set; }

        [Display(Name = "Observação")]
        [StringLength(4000, ErrorMessage = "{0} não deve ser maior que {1} caracteres")]
        [DataType(DataType.MultilineText)]
        public string Observacao { get; set; }
    }
}