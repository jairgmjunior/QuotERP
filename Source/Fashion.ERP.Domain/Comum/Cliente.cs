using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Fashion.ERP.Domain.Comum
{
    public class Cliente : DomainBase<Cliente>
    {
        private readonly IList<Dependente> _dependentes;
        private readonly IList<Referencia> _referencias;

        public Cliente()
        {
            _dependentes = new List<Dependente>();
            _referencias = new List<Referencia>();
        }

        public virtual long Codigo { get; set; }
        public virtual Sexo Sexo { get; set; }
        public virtual EstadoCivil EstadoCivil { get; set; }
        public virtual string NomeMae { get; set; }
        public virtual DateTime? DataValidade { get; set; }
        public virtual string Observacao { get; set; }
        public virtual DateTime DataCadastro { get; set; }

        public virtual Profissao Profissao { get; set; }
        public virtual AreaInteresse AreaInteresse { get; set; }

        #region Dependentes
        public virtual IReadOnlyCollection<Dependente> Dependentes
        {
            get { return new ReadOnlyCollection<Dependente>(_dependentes); }
        }

        public virtual void AddDependente(params Dependente[] dependentes)
        {
            foreach (var dependente in dependentes)
            {
                if (!_dependentes.Contains(dependente))
                {
                    //dependente.Cliente = this;
                    _dependentes.Add(dependente);
                }
            }
        }

        public virtual void RemoveDependente(params Dependente[] dependentes)
        {
            foreach (var dependente in dependentes)
            {
                if (_dependentes.Contains(dependente))
                    _dependentes.Remove(dependente);
            }
        }

        #endregion

        #region Referencias
        public virtual IReadOnlyCollection<Referencia> Referencias
        {
            get { return new ReadOnlyCollection<Referencia>(_referencias); }
        }

        public virtual void AddReferencia(params Referencia[] referencias)
        {
            foreach (var referencia in referencias)
            {
                if (!_referencias.Contains(referencia))
                {
                    referencia.Cliente = this;
                    _referencias.Add(referencia);
                }
            }
        }

        public virtual void RemoveReferencia(params Referencia[] referencias)
        {
            foreach (var referencia in referencias)
            {
                if (_referencias.Contains(referencia))
                    _referencias.Remove(referencia);
            }
        }

        #endregion
    }
}