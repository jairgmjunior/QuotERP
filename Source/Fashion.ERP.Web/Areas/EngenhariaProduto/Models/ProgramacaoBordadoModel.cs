using System;
using System.ComponentModel.DataAnnotations;
using Fashion.ERP.Web.Models;

namespace Fashion.ERP.Web.Areas.EngenhariaProduto.Models
{
    public class ProgramacaoBordadoModel : IModel
    {
        public long? Id { get; set; }

        [Display(Name = "Descrição")]
        [Required(ErrorMessage = "Informe a descrição")]
        [StringLength(100, ErrorMessage = "{0} não deve ser maior que {1} caracteres")]
        public string Descricao { get; set; }

        [Display(Name = "Nome arquivo")]
        [StringLength(100, ErrorMessage = "{0} não deve ser maior que {1} caracteres")]
        public string NomeArquivo { get; set; }

        [Display(Name = "Data")]
        [Required(ErrorMessage = "Informe a data")]
        public DateTime? Data { get; set; }

        [Display(Name = "Qtd. pontos")]
        [Required(ErrorMessage = "Informe a quantidade de pontos")]
        public int QuantidadePontos { get; set; }

        [Display(Name = "Qtd. cores")]
        [Required(ErrorMessage = "Informe a quantidade de cores")]
        public int QuantidadeCores { get; set; }

        [Display(Name = "Aplicação")]
        [StringLength(250, ErrorMessage = "{0} não deve ser maior que {1} caracteres")]
        public string Aplicacao { get; set; }

        [Display(Name = "Observação")]
        [DataType(DataType.MultilineText)]
        [StringLength(250, ErrorMessage = "{0} não deve ser maior que {1} caracteres")]
        public string Observacao { get; set; }

        [Display(Name = "Programador de Bordado")]
        [Required(ErrorMessage = "Informe o programador do bordado")]
        public long? ProgramadorBordado { get; set; }

        public long Modelo { get; set; }

        [Display(Name = "Arquivo")]
        public long? ArquivoId { get; set; }

        // Nome do arquivo no disco
        public string NomeArquivoUpload { get; set; }
    }
}