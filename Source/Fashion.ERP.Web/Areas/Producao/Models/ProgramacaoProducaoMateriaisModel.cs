using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Fashion.ERP.Domain.Comum;
using Fashion.ERP.Web.Models;

namespace Fashion.ERP.Web.Areas.Producao.Models
{
    public class ProgramacaoProducaoMateriaisModel : IModel
    {
        public long? Id { get; set; }

        [Display(Name = "Tag")]
        public string Tag{ get; set; }

        [Display(Name = "Ano")]
        public long Ano { get; set; }

        [Display(Name = "Referência")]
        public string Referencia { get; set; }

        [Display(Name = "Coleção")]
        public string Colecao { get; set; }

        [Display(Name = "Descrição")]
        public string Descricao { get; set; }
        
        [Display(Name = "Estilista")]
        public string Estilista { get; set; }
        
        [Display(Name = "Data Programada")]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime? DataProgramada { get; set; }

        [Display(Name = "Qtde. Programada")]
        public long Quantidade { get; set; }
        
        public IEnumerable<string> Fotos { get; set; }

        public IList<GridProgramacaoProducaoMaterialModel> GridItens { get; set; }
    }
}