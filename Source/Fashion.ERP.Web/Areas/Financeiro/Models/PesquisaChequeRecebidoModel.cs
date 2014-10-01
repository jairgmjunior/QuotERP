using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Fashion.ERP.Domain;

namespace Fashion.ERP.Web.Areas.Financeiro.Models
{
    public class PesquisaChequeRecebidoModel : IChequeRecebidoDropdownModel
    {
        [Display(Name = "Emitente")]
        public string Emitente { get; set; }

        [Display(Name = "Cliente")]
        public string Cliente { get; set; }

        [Display(Name = "Nº cheque")]
        public string NumeroCheque { get; set; }

        [Display(Name = "Unidade")]
        public long? Unidade { get; set; }
        
        [Display(Name = "Vencimento")]
        public DateTime? VencimentoDe { get; set; }

        [Display(Name = "Até")]
        public DateTime? VencimentoAte { get; set; }

        [Display(Name = "Compensado")]
        public SimNao? Compensado { get; set; }

        public IList<GridChequeRecebidoModel> Grid { get; set; }
    }
}