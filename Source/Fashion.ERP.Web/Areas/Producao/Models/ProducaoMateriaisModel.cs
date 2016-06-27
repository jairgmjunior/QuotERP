using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Fashion.ERP.Web.Models;

namespace Fashion.ERP.Web.Areas.Producao.Models
{
    public class ProducaoMateriaisModel : IModel
    {
        public long? Id { get; set; }

        [Display(Name = "Lote/Ano")]
        public string LoteAno{ get; set; }

        [Display(Name = "Referência")]
        public string Referencia { get; set; }

        [Display(Name = "Remessa")]
        public string RemessaProducao { get; set; }
        
        [Display(Name = "Descrição")]
        public string Descricao { get; set; }
        
        //[Display(Name = "Data Programada")]
        //[DisplayFormat(DataFormatString = "{0:d}")]
        //public DateTime? DataProgramada { get; set; }
        
        [Display(Name = "Situação")]
        public String SituacaoProducao { get; set; }
        
        public IEnumerable<FotoTituloModel> Fotos { get; set; }

        public IList<GridProducaoMaterialModel> GridItens { get; set; }
    }
}