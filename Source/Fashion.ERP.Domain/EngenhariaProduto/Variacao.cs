using System.Collections.Generic;
using System.Collections.ObjectModel;
using Fashion.ERP.Domain.Comum;

namespace Fashion.ERP.Domain.EngenhariaProduto
{
    public class Variacao : DomainBase<Variacao>
    {
        private readonly IList<Cor> _cores;

        public Variacao()
        {
            _cores = new List<Cor>();
        }

        public virtual string Nome { get; set; }

        #region Cores
        public virtual IReadOnlyCollection<Cor> Cores
        {
            get { return new ReadOnlyCollection<Cor>(_cores); }
        }

        public virtual void AddCor(params Cor[] cores)
        {
            foreach (var cor in cores)
            {
                _cores.Add(cor);
            }
        }

        public virtual void RemoveCor(params Cor[] cores)
        {
            foreach (var cor in cores)
            {
                if (_cores.Contains(cor))
                    _cores.Remove(cor);
            }
        }

        #endregion
    }
}