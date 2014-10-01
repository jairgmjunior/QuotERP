using System.Collections.Generic;
using System.Collections.ObjectModel;
using Fashion.Framework.Domain;

namespace Fashion.ERP.Domain.Comum
{
    public class Permissao : DomainObject
    {
        public Permissao()
        {
            _permissoesFilhas = new List<Permissao>();
        }

        private readonly IList<Permissao> _permissoesFilhas;

        public virtual string Descricao { get; set; }

        public virtual bool ExibeNoMenu { get; set; }

        public virtual bool RequerPermissao { get; set; }

        public virtual string Action { get; set; }

        public virtual string Controller { get; set; }

        public virtual string Area { get; set; }

        public virtual Permissao PermissaoPai { get; set; }

        public virtual int Ordem { get; set; }

        public virtual IList<Permissao> PermissoesFilhas
        {
            get
            {
                return new ReadOnlyCollection<Permissao>(_permissoesFilhas);
            }
        }
    }
}
