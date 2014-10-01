using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Fashion.ERP.Domain.Comum
{
    public class PrestadorServico : DomainBase<PrestadorServico>
    {
        private readonly IList<TipoPrestadorServico> _tipoPrestadorServicos;

        public PrestadorServico()
        {
            _tipoPrestadorServicos = new List<TipoPrestadorServico>();
        }

        public virtual Pessoa Unidade { get; set; }

        public virtual long Codigo { get; set; }
        public virtual double Comissao { get; set; }
        public virtual DateTime DataCadastro { get; set; }
        public virtual bool Ativo { get; set; }

        #region TipoPrestadorServicos
        
        public virtual IReadOnlyCollection<TipoPrestadorServico> TipoPrestadorServicos
        {
            get { return new ReadOnlyCollection<TipoPrestadorServico>(_tipoPrestadorServicos); }
        }

        public virtual void AddTipoPrestadorServico(params TipoPrestadorServico[] tipoPrestadorServicos)
        {
            foreach (var item in tipoPrestadorServicos.Where(p => !_tipoPrestadorServicos.Contains(p)))
            {
                _tipoPrestadorServicos.Add(item);
            }
        }

        public virtual void RemoveTipoPrestadorServico(TipoPrestadorServico tipoPrestadorServico)
        {
            if (!_tipoPrestadorServicos.Contains(tipoPrestadorServico)) return;

            _tipoPrestadorServicos.Remove(tipoPrestadorServico);
        }
        
        #endregion
    }
}