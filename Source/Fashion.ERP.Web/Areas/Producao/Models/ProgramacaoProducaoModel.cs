using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Fashion.ERP.Domain.Producao;
using Fashion.ERP.Web.Models;

namespace Fashion.ERP.Web.Areas.Producao.Models
{
    public class ProgramacaoProducaoModel : IModel
    {
        public long? Id { get; set; }

        [Display(Name = "Lote/Ano")]
        public long? Lote { get; set; }

        [Display(Name = "Ano")]
        public long? Ano { get; set; }

        [Display(Name = "Data")]
        public DateTime? Data { get; set; }

        [Display(Name = "Data Programada")]
        [Required(ErrorMessage = "Informe a data programada")]
        public DateTime? DataProgramada { get; set; }

        [Display(Name = "Observação")]
        [DataType(DataType.MultilineText)]
        public string Observacao { get; set; }

        [Display(Name = "Responsável")]
        [Required(ErrorMessage = "Informe o responsável")]
        public long? Funcionario { get; set; }
        
        [Display(Name = "Remessa")]
        [Required(ErrorMessage = "Informe a remessa de produção")]
        public long? RemessaProducao { get; set; }

        [Display(Name = "Quantidade")]
        [Required(ErrorMessage = "Informe a quantidade")]
        public long Quantidade { get; set; }

        [Display(Name = "Situação")]
        public SituacaoProgramacaoProducao SituacaoProgramacaoProducao { get; set; }

        public IList<ProgramacaoProducaoItemModel> GridProgramacaoProducaoItens { get; set; }
    }
}