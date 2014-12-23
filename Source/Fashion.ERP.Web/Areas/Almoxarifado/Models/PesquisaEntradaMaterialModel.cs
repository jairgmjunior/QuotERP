using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Fashion.ERP.Web.Areas.Almoxarifado.Models
{
    public class PesquisaEntradaMaterialModel
    {
        [Display(Name = "Unidade de destino")]
        public long? UnidadeDestino { get; set; }

        [Display(Name = "Depósito destino")]
        public long? DepositoMaterialDestino { get; set; }

        [Display(Name = "Fornecedor")]
        public long? Fornecedor { get; set; }

        [Display(Name = "Unidade de origem")]
        public long? UnidadeOrigem { get; set; }

        [Display(Name = "Depósito origem")]
        public long? DepositoMaterialOrigem { get; set; }

        [Display(Name = "Referência")]
        [StringLength(20, ErrorMessage = "{0} não deve ser maior que {1} caracteres")]
        public string Referencia { get; set; }

        [Display(Name = "Data da entrada")]
        public DateTime? DataEntradaDe { get; set; }

        [Display(Name = "Até")]
        public DateTime? DataEntradaAte { get; set; }

        public string ModoConsulta { get; set; }

        [Display(Name = "Agrupar por")]
        public string AgruparPor { get; set; }

        [Display(Name = "Ordenar por")]
        public string OrdenarPor { get; set; }

        [Display(Name = "em")]
        public string OrdenarEm { get; set; }

        public IList<GridEntradaMaterialModel> Grid { get; set; } 
    }
}