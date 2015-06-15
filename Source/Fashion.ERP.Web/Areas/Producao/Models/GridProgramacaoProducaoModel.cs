using System;
using System.ComponentModel.DataAnnotations;

namespace Fashion.ERP.Web.Areas.Producao.Models
{
    public class GridProgramacaoProducaoModel
    {
        public long Id { get; set; }
        
        [Display(Name = "Nùmero")]
        public long Numero { get; set; }

        [Display(Name = "Tag")]
        public string Tag { get; set; }

        [Display(Name = "Ano")]
        public long Ano { get; set; }

        [Display(Name = "Descrição")]
        public string Descricao { get; set; }

        [Display(Name = "Referência")]
        public string Referencia { get; set; }

        [Display(Name = "Coleção")]
        public string Colecao { get; set; }

        [Display(Name = "Data Programada")]
        public DateTime DataProgramada { get; set; }

        [Display(Name = "Qtde. Programada")]
        public long QtdeProgramada { get; set; }
    }
}