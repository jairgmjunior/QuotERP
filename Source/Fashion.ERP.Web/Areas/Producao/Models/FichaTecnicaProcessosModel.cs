using System.Collections.Generic;
using Fashion.ERP.Web.Models;

namespace Fashion.ERP.Web.Areas.Producao.Models
{
    public class FichaTecnicaProcessosModel : IModel
    {
        public long? Id { get; set; }

        public IList<GridFichaTecnicaProcessosModel> GridFichaTecnicaProcessos { get; set; }
    }
}