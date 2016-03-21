using System;
using System.ComponentModel.DataAnnotations;
using Fashion.ERP.Web.Models;

namespace Fashion.ERP.Web.Areas.Producao.Models
{
    public class RemessaProducaoCapacidadeProdutivaItemModel : IModel
    {
        public long? Id { get; set; }

        [Display(Name = "Classificação de Dificuldade")]
        [Required(ErrorMessage = "Informe a classificação de dificuldade.")] 
        public string ClassificacaoDificuldade { get; set; }

        [Display(Name = "Capacidade de Produção")]
        [Range(1, 1000000)]
        public long CapacidadeProducao { get; set; }
    }
}