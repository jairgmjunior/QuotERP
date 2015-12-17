using System;
using System.ComponentModel.DataAnnotations;
using Fashion.ERP.Domain.Producao;

namespace Fashion.ERP.Web.Areas.Producao.Models
{
    public class PesquisaProgramacaoProducaoModel
    {
        [Display(Name = "Tag")]
        public string Tag { get; set; }

        [Display(Name = "Ano")]
        public long? Ano { get; set; }

        [Display(Name = "Lote/Ano")]
        public long? Lote { get; set; }
        
        public long? AnoLote { get; set; }
        
        [Display(Name = "Referência")]
        public string Referencia { get; set; }

        [Display(Name = "Coleção")]
        public long? Colecao { get; set; }
        
        [Display(Name = "Data de Cadastro")]
        public DateTime? DataCadastro { get; set; }

        [Display(Name = "Até")]
        public DateTime? DataCadastroAte { get; set; }

        [Display(Name = "Data Programada")]
        public DateTime? DataProgramada { get; set; }

        [Display(Name = "Até")]
        public DateTime? DataProgramadaAte { get; set; }

        [Display(Name = "Situação")]
        public SituacaoProgramacaoProducao? SituacaoProgramacaoProducao { get; set; }

        public string ModoConsulta { get; set; }

        [Display(Name = "Agrupar por")]
        public string AgruparPor { get; set; }

        [Display(Name = "Ordenar por")]
        public string OrdenarPor { get; set; }

        [Display(Name = "em")]
        public string OrdenarEm { get; set; }
    }
}