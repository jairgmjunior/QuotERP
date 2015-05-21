using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Fashion.ERP.Domain.EngenhariaProduto;
using Fashion.ERP.Web.Models;

namespace Fashion.ERP.Web.Areas.EngenhariaProduto.Models
{
    public class CriacaoFichaTecnicaModel : IModel
    {
        public long? Id { get; set; }

        public string Forro { get; set; }

        [Display(Name = "Tag/Ano")]
        public string Tag { get; set; }

        [Display(Name = "Ano")]
        public long Ano { get; set; }
        
        [Display(Name = "Dificuldade")]
        public long ClassificacaoDificuldade { get; set; }
        
        [Display(Name = "Referência")]
        public string Referencia { get; set; }

        public IList<ModeloAprovacaoMatrizCorteItemModel> GridItens { get; set; }
    }
}