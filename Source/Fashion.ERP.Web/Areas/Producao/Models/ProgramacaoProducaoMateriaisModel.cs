using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Fashion.ERP.Domain.Producao;
using Fashion.ERP.Web.Models;

namespace Fashion.ERP.Web.Areas.Producao.Models
{
    public class ProgramacaoProducaoMateriaisModel : IModel
    {
        public long? Id { get; set; }

        [Display(Name = "Lote/Ano")]
        public string LoteAno{ get; set; }

        [Display(Name = "Referência")]
        public string Referencia { get; set; }

        [Display(Name = "Coleção Programada")]
        public string Colecao { get; set; }

        [Display(Name = "Unidade")]
        public long? UnidadeGeral { get; set; }

        [Display(Name = "Descrição")]
        public string Descricao { get; set; }
        
        [Display(Name = "Estilista")]
        public string Estilista { get; set; }
        
        [Display(Name = "Data Programada")]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime? DataProgramada { get; set; }

        [Display(Name = "Qtde. Programada")]
        public long Quantidade { get; set; }

        [Display(Name = "Situação")]
        public String SituacaoProgramacaoProducao { get; set; }
        
        public IEnumerable<FotoTituloModel> Fotos { get; set; }

        public IList<GridProgramacaoProducaoMaterialModel> GridItens { get; set; }
    }
}