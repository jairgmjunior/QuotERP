using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Fashion.ERP.Web.Models;

namespace Fashion.ERP.Web.Areas.Producao.Models
{
    public class ProducaoProgramacaoModel : IModel
    {
        public long? Id { get; set; }

        [Display(Name = "Lote/Ano")]
        public long? Lote { get; set; }

        [Display(Name = "Ano")]
        public long? Ano { get; set; }

        [Display(Name = "Data")]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime? Data { get; set; }

        [Display(Name = "Observação")]
        public string Observacao { get; set; }

        [Display(Name = "Descrição")]
        public string Descricao { get; set; }

        [Display(Name = "Responsável")]
        public string ResponsavelProducao { get; set; }

        [Display(Name = "Remessa")]
        public string RemessaProducao { get; set; }

        [Display(Name = "Remessa")]
        public long IdRemessaProducao { get; set; }

        [Display(Name = "Situação")]
        public String SituacaoProducao { get; set; }

        [Display(Name = "Tipo de Produção")]
        public String TipoProducao { get; set; }

        [Display(Name = "Responsável")]
        [Required(ErrorMessage = "Informe o responsável")]
        public long? Funcionario { get; set; }

        [Display(Name = "Data Programada")]
        [Required(ErrorMessage = "Informe a data programada")]
        public DateTime? DataProgramacao { get; set; }

        [Display(Name = "Observação")]
        public String ObservacaoProgramacao { get; set; }
        
        [Display(Name = "Quantidade")]
        [Required(ErrorMessage = "Informe a quantidade")]
        public long Quantidade { get; set; }

        public IList<ProducaoProgramacaoItemModel> GridProducaoItens { get; set; }
    }
}