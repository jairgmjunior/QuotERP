using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Fashion.ERP.Domain.Producao;
using Fashion.ERP.Web.Models;

namespace Fashion.ERP.Web.Areas.Producao.Models
{
    public class ProgramacaoProducaoRequisicaoModel : IModel
    {
        public long? Id { get; set; }
        
        [Display(Name = "Lote/Ano")]
        public string LoteAno { get; set; }
        
        [Required(ErrorMessage = "Informe o requerente")]
        [Display(Name = "Requerente")]
        public long? Funcionario { get; set; }

        [Required(ErrorMessage = "Informe o centro de custo")]
        [Display(Name = "Centro de Custo")]
        public long? CentroCusto { get; set; }

        [Required(ErrorMessage = "Informe a unidade requerente.")]
        [Display(Name = "Unidade Requerente")]
        public long? UnidadeRequerente { get; set; }
        
        [Display(Name = "Data Programada")]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime? DataProgramada { get; set; }

        [Display(Name = "Qtde. Programada")]
        public long Quantidade { get; set; }

        [Display(Name = "Remessa")]
        public string RemessaProducao { get; set; }

        [Display(Name = "Situação")]
        public String SituacaoProgramacaoProducao { get; set; }

        public Boolean Requisitado { get; set; }

        public IEnumerable<FotoTituloModel> Fotos { get; set; }

        public IList<GridProgramacaoProducaoRequisicaoModel> GridItens { get; set; }
    }
}