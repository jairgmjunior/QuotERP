using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Fashion.ERP.Domain.Producao;

namespace Fashion.ERP.Web.Areas.Producao.Models
{
    public class PesquisaOrdemProducaoModel
    {
        [Display(Name = "Número")]
        public long? Numero { get; set; }

        [Display(Name = "Tag")]
        [StringLength(100, ErrorMessage = "{0} não deve ser maior que {1} caracteres")]
        public string Tag { get; set; }

        [Display(Name = "Ano")]
        public long? Ano { get; set; }

        [Display(Name = "Data OP")]
        public DateTime? DataOrdemProducao { get; set; }

        [Display(Name = "Data programação")]
        public DateTime? DataProgramacao { get; set; }

        [Display(Name = "Coleção")]
        public long? Colecao { get; set; }

        [Display(Name = "Classificação")]
        public long? Classificacao { get; set; }

        [Display(Name = "Situação")]
        public SituacaoOrdemProducao? SituacaoOrdemProducao { get; set; }

        [Display(Name = "Tipo OP")]
        public TipoOrdemProducao? TipoOrdemProducao { get; set; }

        [Display(Name = "Data corte")]
        public DateTime? DataCorte { get; set; }

        [Display(Name = "Data previsão")]
        public DateTime? DataPrevisao { get; set; }

        [Display(Name = "Natureza")]
        public long? Natureza { get; set; }

        [Display(Name = "Artigo")]
        public long? Artigo { get; set; }

        public string ModoConsulta { get; set; }

        [Display(Name = "Agrupar por")]
        public string AgruparPor { get; set; }

        [Display(Name = "Ordenar por")]
        public string OrdenarPor { get; set; }

        [Display(Name = "em")]
        public string OrdenarEm { get; set; }

        public IList<GridOrdemProducaoModel> Grid { get; set; } 
    }
}