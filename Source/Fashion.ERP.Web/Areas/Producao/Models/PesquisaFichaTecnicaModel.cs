using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Fashion.ERP.Web.Areas.Producao.Models
{
    public class PesquisaFichaTecnicaModel
    {
        [Display(Name = "Dificuldade")]
        public long? ClassificacaoDificuldade { get; set; }
        
        [Display(Name = "Data de Criação")]
        public DateTime? DataCadastro { get; set; }

        [Display(Name = "Até")]
        public DateTime? DataCadastroAte { get; set; }
        
        [Display(Name = "Estilista")]
        public long? Estilista { get; set; }

        [Display(Name = "Referência")]
        public String Referencia { get; set; }

        [Display(Name = "Tag")]
        public string Tag { get; set; }

        [Display(Name = "Ano")]
        public long? Ano { get; set; }
        
        [Display(Name = "Descrição")]
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
        
        public IList<GridFichaTecnicaModel> Grid { get; set; }
    }
}  