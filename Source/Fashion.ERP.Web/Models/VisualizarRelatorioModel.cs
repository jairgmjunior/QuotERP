using System.Collections.Generic;
using Fashion.ERP.Domain;

namespace Fashion.ERP.Web.Models
{
    public class VisualizarRelatorioModel
    {
        public long? Id { get; set; }

        public string Nome { get; set; }

        public IList<string> NomeParametro { get; set; }
        public IList<TipoRelatorioParametro> TipoParametro { get; set; }
        public IList<string> ValorParametro { get; set; }
    }
}