using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Fashion.ERP.Domain.Financeiro
{
    public class DespesaTituloReceber : DomainBase<DespesaTituloReceber>
    {
        private readonly IList<DespesaReceita> _despesaReceitas;

        public DespesaTituloReceber()
        {
            _despesaReceitas = new List<DespesaReceita>();
        }

        public virtual DateTime Data { get; set; }
        public virtual string Historico { get; set; }
        public virtual double Valor { get; set; }
        public virtual bool AgregarValorTitulo { get; set; }

        public virtual TituloReceber TituloReceber { get; set; }

        #region DespesaReceitas
        public virtual IReadOnlyCollection<DespesaReceita> DespesaReceitas
        {
            get { return new ReadOnlyCollection<DespesaReceita>(_despesaReceitas); }
        }

        public virtual void AddDespesaReceita(params DespesaReceita[] despesaReceitas)
        {
            foreach (var despesaReceita in despesaReceitas)
            {
                _despesaReceitas.Add(despesaReceita);
            }
        }

        public virtual void RemoveDespesaReceita(params DespesaReceita[] despesaReceitas)
        {
            foreach (var despesaReceita in despesaReceitas)
            {
                if (_despesaReceitas.Contains(despesaReceita))
                    _despesaReceitas.Remove(despesaReceita);
            }
        }

        public virtual void ClearDespesaReceita()
        {
            _despesaReceitas.Clear();
        }

        #endregion
    }
}