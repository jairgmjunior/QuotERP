using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Fashion.ERP.Web.Helpers.Attributes;
using Fashion.ERP.Web.Models;
using System;
using Fashion.Framework.Common.Validators;

namespace Fashion.ERP.Web.Areas.Almoxarifado.Models
{
    public class EntradaMaterialModel : IModel
    {
        public EntradaMaterialModel()
        {
            EntradaItemMateriais = new List<long?>();
            Materiais = new List<long>();
            UnidadeMedidas = new List<long>();
            QuantidadeCompras = new List<double>();
            Quantidades = new List<double>();
        }

        public long? Id { get; set; }

        [Display(Name = "Data de entrada")]
        [Required(ErrorMessage = "Informe a data da entrada")]
        public DateTime DataEntrada { get; set; }

        [Display(Name = "Unidade de destino")]
        [Required(ErrorMessage = "Informe a unidade de destino")]
        public long? UnidadeDestino { get; set; }

        [Display(Name = "Depósito de destino")]
        [Required(ErrorMessage = "Informe o depósito de destino")]
        public long? DepositoMaterialDestino { get; set; }

        [Display(Name = "Unidade")]
        public long? UnidadeOrigem { get; set; }

        [Display(Name = "Depósito")]
        [Different("DepositoMaterialDestino", ErrorMessage = "O depósito de origem deve ser diferente do depósito de destino.")]
        public long? DepositoMaterialOrigem { get; set; }

        [Display(Name = "Fornecedor")]
        public long? Fornecedor { get; set; }

        [Display(Name = "Observação")]
        [DataType(DataType.MultilineText)]
        public string Observacao { get; set; }

        public IList<long?> EntradaItemMateriais { get; set; }
        public IList<long> Materiais { get; set; }
        public IList<long> UnidadeMedidas { get; set; }
        public IList<double> QuantidadeCompras { get; set; }
        public IList<double> Quantidades { get; set; }
    }
}