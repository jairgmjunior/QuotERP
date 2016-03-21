using System;
using System.ComponentModel.DataAnnotations;

namespace Fashion.ERP.Web.Areas.Producao.Models
{
    public class PesquisaRemessaProducaoModel
    {
        [Display(Name = "Número/Ano")]
        public long? Numero { get; set; }

        [Display(Name = "Ano")]
        public long? Ano { get; set; }

        [Display(Name = "Descrição")]
        public string Descricao { get; set; }

        [Display(Name = "Coleção")]
        public long? Colecao { get; set; }

        [Display(Name = "Data de Início")]
        public DateTime? DataInicio { get; set; }

        [Display(Name = "Até")]
        public DateTime? DataInicioAte { get; set; }

        [Display(Name = "Data Limite")]
        public DateTime? DataLimite { get; set; }

        [Display(Name = "Até")]
        public DateTime? DataLimiteAte { get; set; }
    }
}