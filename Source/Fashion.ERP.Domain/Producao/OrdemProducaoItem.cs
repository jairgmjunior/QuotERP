using System.Collections.Generic;
using System.Collections.ObjectModel;
using Fashion.ERP.Domain.Comum;

namespace Fashion.ERP.Domain.Producao
{
    public class OrdemProducaoItem : DomainBase<OrdemProducaoItem>
    {
        private readonly IList<OrdemProducaoItemFechamento> _ordemProducaoItemFechamentos;

        public OrdemProducaoItem()
        {
            _ordemProducaoItemFechamentos = new List<OrdemProducaoItemFechamento>();            
        }

        public virtual int QuantidadeSolicitada { get; set; }
        public virtual int QuantidadeAdicional { get; set; }
        public virtual int QuantidadeCancelada { get; set; }
        public virtual SituacaoOrdemProducao SituacaoOrdemProducao { get; set; }

        public virtual FichaTecnicaVariacaoMatriz FichaTecnicaVariacaoMatriz { get; set; }
        public virtual Tamanho Tamanho { get; set; }
        public virtual OrdemProducao OrdemProducao { get; set; }

        #region OrdemProducaoItemFechamentos

        public virtual IReadOnlyCollection<OrdemProducaoItemFechamento> OrdemProducaoItemFechamentos
        {
            get { return new ReadOnlyCollection<OrdemProducaoItemFechamento>(_ordemProducaoItemFechamentos); }
        }

        public virtual void AddOrdemProducaoItemFechamento(params OrdemProducaoItemFechamento[] ordemProducaoItemFechamentos)
        {
            foreach (var ordemProducaoItemFechamento in ordemProducaoItemFechamentos)
                if (!_ordemProducaoItemFechamentos.Contains(ordemProducaoItemFechamento))
                {
                    ordemProducaoItemFechamento.OrdemProducaoItem = this;
                    _ordemProducaoItemFechamentos.Add(ordemProducaoItemFechamento);
                }
        }

        public virtual void RemoveOrdemProducaoItemFechamento(params OrdemProducaoItemFechamento[] ordemProducaoItemFechamentos)
        {
            foreach (var ordemProducaoItemFechamento in ordemProducaoItemFechamentos)
                if (_ordemProducaoItemFechamentos.Contains(ordemProducaoItemFechamento))
                    _ordemProducaoItemFechamentos.Remove(ordemProducaoItemFechamento);
        }

        #endregion
    }
}