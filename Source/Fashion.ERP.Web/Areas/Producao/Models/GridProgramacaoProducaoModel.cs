using System;
using System.ComponentModel.DataAnnotations;
using Fashion.ERP.Domain.Producao;

namespace Fashion.ERP.Web.Areas.Producao.Models
{
    public class GridProgramacaoProducaoModel
    {
        public long Id { get; set; }
        
        [Display(Name = "Lote/Ano")]
        public string LoteAno { get; set; }

        [Display(Name = "Coleção")]
        public string Colecao { get; set; }
        
        [Display(Name = "Responsável")]
        public string Responsavel { get; set; }

        [Display(Name = "Data Programada")]
        public DateTime DataProgramada { get; set; }

        [Display(Name = "Qtde. Programada")]
        public long QtdeProgramada { get; set; }

        [Display(Name = "Qtde. Fichas Técnicas")]
        public long QtdeFichasTecnicas { get; set; }
        
        [Display(Name = "Situação")]
        public SituacaoProgramacaoProducao SituacaoProgramacaoProducao { get; set; }
    }
}