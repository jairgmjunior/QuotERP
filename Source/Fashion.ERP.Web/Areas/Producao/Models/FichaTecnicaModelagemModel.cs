using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Fashion.ERP.Web.Models;

namespace Fashion.ERP.Web.Areas.Producao.Models
{
    public class FichaTecnicaModelagemModel : IModel
    {
        public long? Id { get; set; }

        [Display(Name = "Data da Modelagem")]
        [Required(ErrorMessage = "Informe o modelista")]
        public DateTime? DataModelagem { get; set; }
        
        [Display(Name = "Nome do arquivo")]
        [StringLength(100, ErrorMessage = "{0} não deve ser maior que {1} caracteres")]
        public string NomeArquivo { get; set; }
        
        [Display(Name = "Observação")]
        [DataType(DataType.MultilineText)]
        [StringLength(250, ErrorMessage = "{0} não deve ser maior que {1} caracteres")]
        public string Observacao { get; set; }

        [Display(Name = "Modelista")]
        [Required(ErrorMessage = "Informe o modelista")]
        public long? Modelista { get; set; }
        
        // Nome do arquivo no disco
        [Display(Name = "Arquivo")]
        public string NomeArquivoUpload { get; set; }
        
        [Display(Name = "Download")]
        public long? Arquivo { get; set; }

        public IList<GridFichaTecnicaModelagemMedidaModel> GridMedidas { get; set; }
    }
}