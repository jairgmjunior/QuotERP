using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Fashion.ERP.Web.Areas.Producao.Models
{
    public class PesquisaColecaoProgramadaModel
    {
        [Display(Name = "Remessas")]
        public List<string> RemessasProducao { get; set; }
        
        [Display(Name = "Período de Programação")]
        public DateTime? DataInicial { get; set; }

        [Display(Name = "Até")]
        public DateTime? DataFinal { get; set; }
        
        public IList<GridColecaoProgramadaModel> Grid { get; set; }
    }
}