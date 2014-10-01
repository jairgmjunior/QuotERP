using System;
using System.ComponentModel.DataAnnotations;

namespace Fashion.ERP.Web.Areas.EngenhariaProduto.Models
{
    public class GridProgramacaoBordadoModel
    {
        public long Id { get; set; }

        [Display(Name = "Descrição")]
        public string Descricao { get; set; }

        [Display(Name = "Nome arquivo")]
        public string NomeArquivo { get; set; }

        [Display(Name = "Data")]
        public DateTime Data { get; set; }

        [Display(Name = "Qtd. pontos")]
        public int QuantidadePontos { get; set; }

        [Display(Name = "Qtd. cores")]
        public int QuantidadeCores { get; set; }

        [Display(Name = "Aplicação")]
        public string Aplicacao { get; set; }

        [Display(Name = "Arquivo")]
        public long? Arquivo { get; set; }
    }
}