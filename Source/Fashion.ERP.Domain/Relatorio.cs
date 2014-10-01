using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Fashion.ERP.Domain
{
    public class Relatorio : DomainBase<Relatorio>
    {
        private readonly IList<RelatorioParametro> _relatorioParametros;

        public Relatorio()
        {
            _relatorioParametros = new List<RelatorioParametro>();
        }

        public virtual Arquivo Arquivo { get; set; }
        public virtual string Nome { get; set; }

        #region RelatorioParametros

        public virtual IReadOnlyCollection<RelatorioParametro> RelatorioParametros
        {
            get { return new ReadOnlyCollection<RelatorioParametro>(_relatorioParametros); }
        }

        public virtual void AddRelatorioParametro(params RelatorioParametro[] relatorioParametros)
        {
            foreach (var endereco in relatorioParametros)
            {
                if (!_relatorioParametros.Contains(endereco))
                {
                    endereco.Relatorio = this;
                    _relatorioParametros.Add(endereco);
                }
            }
        }

        public virtual void RemoveRelatorioParametro(params RelatorioParametro[] relatorioParametros)
        {
            foreach (var endereco in relatorioParametros)
            {
                if (_relatorioParametros.Contains(endereco))
                    _relatorioParametros.Remove(endereco);
            }
        }

        #endregion
    }
}