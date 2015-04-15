using Fashion.ERP.Web.Models;

namespace Fashion.ERP.Web.Areas.Producao.Models
{
    public class FichaTecnicaModel : IModel
    {
        public long? Id { get; set; }

        public FichaTecnicaBasicosModel FichaTecnicaBasicosModel { get; set; }
    }
}