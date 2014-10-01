using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Fashion.Framework.Domain;

namespace Fashion.ERP.Domain.Comum
{
    public class PerfilDeAcesso : DomainObject
    {
        private IList<Permissao> _permissoes;

        public PerfilDeAcesso()
        {
            _permissoes = new List<Permissao>();
        }

        public virtual string Nome { get; set; }

        #region Permissoes
        public virtual ReadOnlyCollection<Permissao> Permissoes
        {
            get { return new ReadOnlyCollection<Permissao>(_permissoes); }
        }

        public virtual void ClearPermissoes()
        {
            _permissoes = _permissoes ?? new List<Permissao>();
            _permissoes.Clear();
        }

        public virtual void AddRangePermissoes(IList<Permissao> permissoes)
        {
            _permissoes = _permissoes ?? new List<Permissao>();
            foreach (var permisao in permissoes)
                AddPermissao(permisao);
        }

        public virtual void AddPermissao(Permissao item)
        {
            _permissoes = _permissoes ?? new List<Permissao>();
            if (_permissoes.All(x => x.Id != item.Id))
            {
                if (item.PermissaoPai != null)
                    AddPermissao(item.PermissaoPai);
                _permissoes.Add(item);
            }
        }

        public virtual void RemovePermissao(Permissao item)
        {
            _permissoes = _permissoes ?? new List<Permissao>();
            if (_permissoes.Any(x => x.Id != item.Id))
                _permissoes.Remove(item);
        }
        #endregion
    }
}
