using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Fashion.ERP.Domain.Almoxarifado;

namespace Fashion.ERP.Web.Areas.Almoxarifado.Models
{
    public class PesquisaReservaMaterialModel
    {
        [Display(Name = "Referência de Origem")]
        public string ReferenciaOrigem { get; set; }

        [Display(Name = "Unidade")]
        public long? Unidade { get; set; }

        [Display(Name = "Material")]
        public long? Material { get; set; }
        
        [Display(Name = "Referência Externa")]
        public string ReferenciaExterna { get; set; }

        [Display(Name = "Número")]
        public long? Numero { get; set; }

        [Display(Name = "Data de Programação")]
        public DateTime? DataProgramacaoInicio { get; set; }

        [Display(Name = "Até")]
        public DateTime? DataProgramacaoFim { get; set; }
        
        [Display(Name = "Situação")]
        public SituacaoReservaMaterial? SituacaoReservaMaterial { get; set; }
        
        public string ModoConsulta { get; set; }

        [Display(Name = "Tipo")]
        public string TipoRelatorio { get; set; }

        [Display(Name = "Agrupar por")]
        public string AgruparPor { get; set; }

        [Display(Name = "Ordenar por")]
        public string OrdenarPor { get; set; }

        [Display(Name = "em")]
        public string OrdenarEm { get; set; }

        public IList<GridReservaMaterialModel> Grid { get; set; }
    }
}