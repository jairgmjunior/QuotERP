using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Fashion.ERP.Domain.Comum;

namespace Fashion.ERP.Domain.Almoxarifado
{
    public class DepositoMaterial : DomainBase<DepositoMaterial>
    {
        private readonly IList<Pessoa> _funcionarios;

        public DepositoMaterial()
        {
            _funcionarios = new List<Pessoa>();
        }

        public virtual string Nome { get; set; }
        public virtual DateTime DataAbertura { get; set; }
        public virtual bool Ativo { get; set; }

        public virtual Pessoa Unidade { get; set; }

        #region Funcionarios
        public virtual IReadOnlyCollection<Pessoa> Funcionarios
        {
            get { return new ReadOnlyCollection<Pessoa>(_funcionarios); }
        }

        public virtual void AddFuncionario(params Pessoa[] funcionarios)
        {
            foreach (var funcionario in funcionarios.Where(p => p.Funcionario != null))
            {
                _funcionarios.Add(funcionario);
            }
        }

        public virtual void RemoveFuncionario(params Pessoa[] funcionarios)
        {
            foreach (var funcionario in funcionarios)
            {
                if (_funcionarios.Contains(funcionario))
                    _funcionarios.Remove(funcionario);
            }
        }

        public virtual void ClearFuncionario()
        {
            _funcionarios.Clear();
        }

        #endregion
    }
}