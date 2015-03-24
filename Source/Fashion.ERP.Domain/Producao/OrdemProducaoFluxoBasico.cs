using System.Collections.Generic;
using System.Collections.ObjectModel;
using Fashion.ERP.Domain.Comum;

namespace Fashion.ERP.Domain.Producao
{
    public class OrdemProducaoFluxoBasico : DomainBase<OrdemProducaoFluxoBasico>
    {
        private readonly IList<DepartamentoProducao> _departamentoProducoes;

        public OrdemProducaoFluxoBasico()
        {
            _departamentoProducoes = new List<DepartamentoProducao>();
        }

        #region DepartamentoProducoes

        public virtual IReadOnlyCollection<DepartamentoProducao> DepartamentoProducoes
        {
            get { return new ReadOnlyCollection<DepartamentoProducao>(_departamentoProducoes); }
        }

        public virtual void AddDepartamentoProducao(params DepartamentoProducao[] departamentoProducoes)
        {
            foreach (var departamentoProducao in departamentoProducoes)
            {    if (!_departamentoProducoes.Contains(departamentoProducao))
                {
                    _departamentoProducoes.Add(departamentoProducao);
                }
            }
        }

        public virtual void RemoveDepartamentoProducao(params DepartamentoProducao[] departamentoProducoes)
        {
            foreach (var departamentoProducao in departamentoProducoes)
                if (_departamentoProducoes.Contains(departamentoProducao))
                    _departamentoProducoes.Remove(departamentoProducao);
        }

        public virtual void ClearDepartamentoProducao()
        {
            _departamentoProducoes.Clear();
        }

        #endregion
    }
}