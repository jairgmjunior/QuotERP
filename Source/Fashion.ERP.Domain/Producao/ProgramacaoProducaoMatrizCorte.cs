using System.Collections.Generic;
using Fashion.ERP.Domain.Comum;

namespace Fashion.ERP.Domain.Producao
{
    public class ProgramacaoProducaoMatrizCorte : DomainEmpresaBase<ProgramacaoProducaoMatrizCorte>
    {
        private readonly IList<ProgramacaoProducaoMatrizCorteItem> _programacaoProducaoMatrizCorteItens = new List<ProgramacaoProducaoMatrizCorteItem>();

        public virtual TipoEnfestoTecido TipoEnfestoTecido { get; set; }

        public virtual IList<ProgramacaoProducaoMatrizCorteItem> ProgramacaoProducaoMatrizCorteItens
        {
            get { return _programacaoProducaoMatrizCorteItens; }
        }
    }
}