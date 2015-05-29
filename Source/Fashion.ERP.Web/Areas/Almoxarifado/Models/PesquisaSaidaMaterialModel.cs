using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Fashion.ERP.Web.Areas.Almoxarifado.Models
{
    public class PesquisaSaidaMaterialModel
    {
        [Display(Name = "Unidade de retirada")]
        public long? UnidadeRetirada{ get; set; }

        [Display(Name = "Depósito de retirada")]
        public long? DepositoMaterialRetirada { get; set; }

        [Display(Name = "Centro de Custo")]
        public long? CentroCusto { get; set; }
        
        [Display(Name = "Material")]
        public long? Material { get; set; }
        
        [Display(Name = "Data da saída")]
        public DateTime? DataSaidaDe { get; set; }

        [Display(Name = "Até")]
        public DateTime? DataSaidaAte { get; set; }
        
        public string ModoConsulta { get; set; }
    }
}