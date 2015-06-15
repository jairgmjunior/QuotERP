using System.Collections.Generic;
using Fashion.ERP.Domain.Comum;

namespace Fashion.ERP.Domain.EngenhariaProduto
{
    public class ModeloAprovacaoMatrizCorte : DomainEmpresaBase<ModeloAprovacaoMatrizCorte>
    {
        private readonly IList<ModeloAprovacaoMatrizCorteItem> _modeloAprovacaoMatrizCorteItens = new List<ModeloAprovacaoMatrizCorteItem>();

        public virtual TipoEnfestoTecido TipoEnfestoTecido { get; set; }

        public virtual IList<ModeloAprovacaoMatrizCorteItem> ModeloAprovacaoMatrizCorteItens
        {
            get { return _modeloAprovacaoMatrizCorteItens; }
        }
    }
}