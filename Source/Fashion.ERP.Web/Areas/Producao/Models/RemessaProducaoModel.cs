using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Fashion.ERP.Web.Models;

namespace Fashion.ERP.Web.Areas.Producao.Models
{
    public class RemessaProducaoModel : IModel
    {
        public long? Id { get; set; }

        [Display(Name = "Observação")]
        [DataType(DataType.MultilineText)]
        public string Observacao { get; set; }

        [Display(Name = "Descrição")]
        [Required(ErrorMessage = "Informe a descrição")]
        public string Descricao { get; set; }

        [Display(Name = "Coleção")]
        [Required(ErrorMessage = "Informe a coleção")]
        public long? Colecao { get; set; }

        [Display(Name = "Número/Ano")]
        public long? Numero { get; set; }

        [Display(Name = "Ano")]
        public long? Ano { get; set; }
        
        public bool PossuiNumero { get; set; }

        [Display(Name = "Data de Início")]
        [Required(ErrorMessage = "Informe a data de início")]
        public DateTime? DataInicio { get; set; }

        [Display(Name = "Data Limite")]
        [Required(ErrorMessage = "Informe a data limite")]
        public DateTime? DataLimite { get; set; }

        public IList<RemessaProducaoCapacidadeProdutivaItemModel> GridRemessaProducaoCapacidadeProdutiva { get; set; }
    }
}