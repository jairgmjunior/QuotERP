using System;
using System.ComponentModel.DataAnnotations;

namespace Fashion.ERP.Web.Areas.Producao.Models
{
    public class GridRemessaProducaoModel
    {
        public long Id { get; set; }

        [Display(Name = "Número/Ano")]
        public string NumeroAno { get; set; }

        [Display(Name = "Descrição")]
        public string Descricao { get; set; }

        [Display(Name = "Coleção")]
        public string Colecao { get; set; }

        [Display(Name = "Data de Início")]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime DataInicio { get; set; }

        [Display(Name = "Data Limite")]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime DataLimite { get; set; }

        [Display(Name = "Capacidade Produtiva")]
        public long CapacidadeProdutiva { get; set; }
    }
}