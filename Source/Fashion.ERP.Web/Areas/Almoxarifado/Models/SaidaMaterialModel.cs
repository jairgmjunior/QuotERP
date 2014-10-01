using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Fashion.ERP.Web.Helpers.Attributes;
using Fashion.ERP.Web.Models;

namespace Fashion.ERP.Web.Areas.Almoxarifado.Models
{
    public class SaidaMaterialModel : IModel
    {
        public SaidaMaterialModel()
        {
            SaidaItemMateriais = new List<long?>();
            Materiais = new List<long>();
            Quantidades = new List<double>();
        }

        public long? Id { get; set; }

        [Display(Name = "Data saída")]
        [Required(ErrorMessage = "Informe a data da saída")]
        public DateTime DataSaida { get; set; }

        [Display(Name = "Unidade retirada")]
        [Required(ErrorMessage = "Informe a unidade da retirada")]
        public long? UnidadeOrigem { get; set; }

        [Display(Name = "Depósito retirada")]
        public long? DepositoMaterialOrigem { get; set; }

        [Display(Name = "Centro de custo")]
        public long? CentroCusto { get; set; }

        [Display(Name = "Unidade")]
        public long? UnidadeDestino { get; set; }

        [Display(Name = "Depósito")]
        [Different("DepositoMaterialOrigem", ErrorMessage = "O depósito de destino deve ser diferente do depósito de origem.")]
        public long? DepositoMaterialDestino { get; set; }

        public IList<long?> SaidaItemMateriais { get; set; }
        public IList<long> Materiais { get; set; }
        public IList<double> Quantidades { get; set; }
    }
}