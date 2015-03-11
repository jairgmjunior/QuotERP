using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Fashion.ERP.Domain;
using Fashion.ERP.Web.Areas.Financeiro.Models;

namespace Fashion.ERP.Web.Areas.Producao.Models
{
    public class PesquisaFichaTecnicaModel 
    {
        [Display(Name = "Tag")]
        public string Tag { get; set; }

        [Display(Name = "Ano")]
        public long? Ano { get; set; }
        
        [Display(Name = "Descricao")]
        public string Descricao { get; set; }

        [Display(Name = "Natureza")]
        public long? Natureza { get; set; }

        [Display(Name = "Barra")]
        public long? Barra { get; set; }

        [Display(Name = "Artigo")]
        public long? Artigo { get; set; }

        [Display(Name = "Segmento")]
        public long? Segmento { get; set; }

        [Display(Name = "Classificação")]
        public long? Classificacao { get; set; }

        [Display(Name = "Marca")]
        public long? Marca { get; set; }

        [Display(Name = "Comprimento")]
        public long? Comprimento { get; set; }

        [Display(Name = "Período de cadastro")]
        public DateTime? PeriodoCadastro { get; set; }

        [Display(Name = "Até")]
        public DateTime? PeriodoCadastroAte { get; set; }

        public string ModoConsulta { get; set; }

        [Display(Name = "Tipo")]
        public string TipoRelatorio { get; set; }

        [Display(Name = "Agrupar por")]
        public string AgruparPor { get; set; }

        [Display(Name = "Ordenar por")]
        public string OrdenarPor { get; set; }

        [Display(Name = "em")]
        public string OrdenarEm { get; set; }

        public IList<GridFichaTecnicaModel> Grid { get; set; }
    }
}