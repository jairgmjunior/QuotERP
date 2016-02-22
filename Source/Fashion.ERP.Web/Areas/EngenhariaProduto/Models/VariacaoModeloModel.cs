using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Fashion.ERP.Web.Areas.EngenhariaProduto.Models
{
    public class VariacaoModeloModel
    {
        public long ModeloId { get; set; }

        [Display(Name = "Referência")]
        public string ModeloReferencia { get; set; }

        [Display(Name = "Descrição")]
        public string ModeloDescricao { get; set; }

        [Display(Name = "Estilista")]
        public string ModeloEstilistaNome { get; set; }

        [Display(Name = "Criação")]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime ModeloDataCriacao { get; set; }

        public List<long?> Variacoes { get; set; }
        public List<long> Cores { get; set; }
    }
}