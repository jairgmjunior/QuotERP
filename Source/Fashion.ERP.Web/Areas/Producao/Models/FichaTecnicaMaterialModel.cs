using System.Collections.Generic;
using Fashion.ERP.Web.Models;

namespace Fashion.ERP.Web.Areas.Producao.Models
{
    public class FichaTecnicaMaterialModel : IModel
    {
        public long? Id { get; set; }

        public IList<GridMaterialComposicaoCustoMatrizModel> GridMaterialComposicaoCustoMatriz { get; set; }
        public IList<GridMaterialConsumoItemModel> GridMaterialConsumoItem { get; set; }
        public IList<GridMaterialConsumoMatrizModel> GridMaterialConsumoMatriz { get; set; }
    }
}