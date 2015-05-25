using System.Collections.Generic;

namespace Fashion.ERP.Web.Areas.EngenhariaProduto.Models
{
    public class CriacaoFichaTecnicaModel
    {
        public IList<long> Ids { get; set; }
        
        public IList<MaterialComposicaoCustoCriacaoFichaTecnicaModel> GridItens { get; set; }
    }
}