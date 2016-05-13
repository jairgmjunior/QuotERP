using System.Collections.Generic;
using Fashion.ERP.Domain.Comum;

namespace Fashion.ERP.Domain.Producao
{
    public class ProducaoMatrizCorte : DomainEmpresaBase<ProducaoMatrizCorte>
    {
        private readonly IList<ProducaoMatrizCorteItem> _producaoMatrizCorteItens = new List<ProducaoMatrizCorteItem>();

        public virtual TipoEnfestoTecido TipoEnfestoTecido { get; set; }

        public virtual IList<ProducaoMatrizCorteItem> ProducaoMatrizCorteItens
        {
            get { return _producaoMatrizCorteItens; }
        }
    }
}