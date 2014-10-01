using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Fashion.Framework.Common.Utils;
using Fashion.Framework.Domain;

namespace Fashion.ERP.Domain.Comum
{
    public class Usuario : DomainObject
    {
        public virtual string Nome { get; set; }
        public virtual string Login { get; set; }
        public virtual string Senha { get; set; }
        public virtual bool Administrador { get; set; }
        public virtual bool ConcedeAcesso { get; set; }

        public virtual Pessoa Funcionario { get; set; }

        #region PerfisDeAcesso
        private IList<PerfilDeAcesso> _perfisDeAcesso;

        public virtual IList<PerfilDeAcesso> PerfisDeAcesso
        {
            get
            {
                _perfisDeAcesso = _perfisDeAcesso ?? new List<PerfilDeAcesso>();
                return new ReadOnlyCollection<PerfilDeAcesso>(_perfisDeAcesso);
            }
        }

        public virtual void AddPerfilDeAcesso(PerfilDeAcesso item)
        {
            _perfisDeAcesso = _perfisDeAcesso ?? new List<PerfilDeAcesso>();
            if (!_perfisDeAcesso.Contains(item))
                _perfisDeAcesso.Add(item);
        }

        public virtual void RemovePerfilDeAcesso(PerfilDeAcesso item)
        {
            _perfisDeAcesso = _perfisDeAcesso ?? new List<PerfilDeAcesso>();
            if (_perfisDeAcesso.Contains(item))
                _perfisDeAcesso.Remove(item);
        }

        public virtual void ClearPerfisDeAcesso()
        {
            _permissoes = _permissoes ?? new List<Permissao>();
            _permissoes.Clear();
        }

        public virtual void AddRangePerfisDeAcesso(IList<PerfilDeAcesso> items)
        {
            _permissoes = _permissoes ?? new List<Permissao>();
            foreach (var item in items)
                AddPerfilDeAcesso(item);
        }
        #endregion

        #region Permissoes
        private IList<Permissao> _permissoes;

        public virtual IList<Permissao> Permissoes
        {
            get
            {
                _permissoes = _permissoes ?? new List<Permissao>();
                return new ReadOnlyCollection<Permissao>(_permissoes);
            }
        }

        public virtual void AddPermissao(Permissao item)
        {
            _permissoes = _permissoes ?? new List<Permissao>();
            if (!_permissoes.Contains(item))
            {
                if (item.PermissaoPai != null)
                    AddPermissao(item.PermissaoPai);
                _permissoes.Add(item);
            }
        }

        public virtual void RemovePermissao(Permissao item)
        {
            _permissoes = _permissoes ?? new List<Permissao>();
            if (_permissoes.Contains(item))
                _permissoes.Remove(item);
        }

        public virtual void ClearPermissoes()
        {
            _permissoes = _permissoes ?? new List<Permissao>();
            _permissoes.Clear();
        }

        public virtual void AddRangePermissoes(IList<Permissao> items)
        {
            _permissoes = _permissoes ?? new List<Permissao>();
            foreach (var item in items)
                AddPermissao(item);
        }
        #endregion

        #region CriptografarSenha
        public virtual string CriptografarSenha(string senha)
        {
            return (Senha = SimpleHash.ComputeHash(senha, HashAlgorithmType.Sha512, null));
        }
        #endregion

        #region Autenticar
        public virtual bool Autenticar(string senha)
        {
            return SimpleHash.VerifyHash(senha, HashAlgorithmType.Sha512, Senha);
        }
        #endregion
    }
}
