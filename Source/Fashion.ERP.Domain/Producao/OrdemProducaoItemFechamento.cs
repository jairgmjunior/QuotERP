using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Fashion.ERP.Domain.Producao
{
    public class OrdemProducaoItemFechamento : DomainBase<OrdemProducaoItemFechamento>
    {
        private readonly IList<OrdemProducaoItemFechamentoSinistro> _ordemProducaoItemFechamentoSinistros;

        public OrdemProducaoItemFechamento()
        {
            _ordemProducaoItemFechamentoSinistros = new List<OrdemProducaoItemFechamentoSinistro>();            
        }

        public virtual DateTime Data { get; set; }
        public virtual double QuantidadeProduzida { get; set; }

        public virtual OrdemProducaoItem OrdemProducaoItem { get; set; }

        #region OrdemProducaoItemFechamentoSinistros

        public virtual IReadOnlyCollection<OrdemProducaoItemFechamentoSinistro> OrdemProducaoItemFechamentoSinistros
        {
            get { return new ReadOnlyCollection<OrdemProducaoItemFechamentoSinistro>(_ordemProducaoItemFechamentoSinistros); }
        }

        public virtual void AddOrdemProducaoItemFechamento(params OrdemProducaoItemFechamentoSinistro[] ordemProducaoItemFechamentoSinistros)
        {
            foreach (var ordemProducaoItemFechamentoSinistro in ordemProducaoItemFechamentoSinistros)
                if (!_ordemProducaoItemFechamentoSinistros.Contains(ordemProducaoItemFechamentoSinistro))
                {
                    ordemProducaoItemFechamentoSinistro.OrdemProducaoItemFechamento = this;
                    _ordemProducaoItemFechamentoSinistros.Add(ordemProducaoItemFechamentoSinistro);
                }
        }

        public virtual void RemoveOrdemProducaoItemFechamento(params OrdemProducaoItemFechamentoSinistro[] ordemProducaoItemFechamentoSinistros)
        {
            foreach (var ordemProducaoItemFechamentoSinistro in ordemProducaoItemFechamentoSinistros)
                if (_ordemProducaoItemFechamentoSinistros.Contains(ordemProducaoItemFechamentoSinistro))
                    _ordemProducaoItemFechamentoSinistros.Remove(ordemProducaoItemFechamentoSinistro);
        }

        #endregion
    }
}