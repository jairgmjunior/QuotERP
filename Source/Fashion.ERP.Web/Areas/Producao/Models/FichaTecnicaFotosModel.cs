using System.Collections.Generic;
using Fashion.ERP.Web.Models;

namespace Fashion.ERP.Web.Areas.Producao.Models
{
    public class FichaTecnicaFotosModel : IModel
    {
        public long? Id { get; set; }
        
        public IList<GridFichaTecnicaFotosModel> GridFotos { get; set; }
    }
}