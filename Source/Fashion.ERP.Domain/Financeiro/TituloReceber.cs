using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Fashion.ERP.Domain.Comum;

namespace Fashion.ERP.Domain.Financeiro
{
    public class TituloReceber : DomainEmpresaBase<TituloReceber>
    {
        private readonly IList<TituloReceberBaixa> _tituloReceberBaixas;
        private readonly IList<DespesaTituloReceber> _despesaTituloRecebers;

        public TituloReceber()
        {
            _tituloReceberBaixas = new List<TituloReceberBaixa>();
            _despesaTituloRecebers = new List<DespesaTituloReceber>();
        }

        public virtual string Numero { get; set; }
        public virtual int Parcela { get; set; }
        public virtual int Plano { get; set; }
        public virtual DateTime Emissao { get; set; }
        public virtual DateTime Vencimento { get; set; }
        public virtual DateTime Prorrogacao { get; set; }
        public virtual double Valor { get; set; }
        public virtual double SaldoDevedor { get; set; }
        public virtual double ValorDespesas { get; set; }
        public virtual string Historico { get; set; }
        public virtual string Observacao { get; set; }
        public virtual bool Provisorio { get; set; }
        public virtual SituacaoTitulo SituacaoTitulo { get; set; }
        public virtual DateTime DataCadastro { get; set; }
        public virtual DateTime DataAlteracao { get; set; }
        public virtual OrigemTituloReceber OrigemTituloReceber { get; set; }

        public virtual Pessoa Cliente { get; set; }
        public virtual Pessoa Funcionario { get; set; }
        public virtual Pessoa Unidade { get; set; }
        public virtual Banco Banco { get; set; }

        #region TituloReceberBaixas
        public virtual IReadOnlyCollection<TituloReceberBaixa> TituloReceberBaixas
        {
            get { return new ReadOnlyCollection<TituloReceberBaixa>(_tituloReceberBaixas); }
        }

        public virtual void AddTituloReceberBaixa(params TituloReceberBaixa[] tituloReceberBaixas)
        {
            foreach (var tituloReceberBaixa in tituloReceberBaixas)
            {
                tituloReceberBaixa.TituloReceber = this;
                _tituloReceberBaixas.Add(tituloReceberBaixa);
            }
        }

        public virtual void RemoveTituloReceberBaixa(params TituloReceberBaixa[] tituloReceberBaixas)
        {
            foreach (var tituloReceberBaixa in tituloReceberBaixas)
            {
                if (_tituloReceberBaixas.Contains(tituloReceberBaixa))
                    _tituloReceberBaixas.Remove(tituloReceberBaixa);
            }
        }

        public virtual void ClearTituloReceberBaixa()
        {
            _tituloReceberBaixas.Clear();
        }

        #endregion

        #region DespesaTituloRecebers
        public virtual IReadOnlyCollection<DespesaTituloReceber> DespesaTituloRecebers
        {
            get { return new ReadOnlyCollection<DespesaTituloReceber>(_despesaTituloRecebers); }
        }

        public virtual void AddDespesaTituloReceber(params DespesaTituloReceber[] despesaTituloRecebers)
        {
            foreach (var despesaTituloReceber in despesaTituloRecebers)
            {
                despesaTituloReceber.TituloReceber = this;
                _despesaTituloRecebers.Add(despesaTituloReceber);
            }
        }

        public virtual void RemoveDespesaTituloReceber(params DespesaTituloReceber[] despesaTituloRecebers)
        {
            foreach (var despesaTituloReceber in despesaTituloRecebers)
            {
                if (_despesaTituloRecebers.Contains(despesaTituloReceber))
                    _despesaTituloRecebers.Remove(despesaTituloReceber);
            }
        }

        public virtual void ClearDespesaTituloReceber()
        {
            _despesaTituloRecebers.Clear();
        }

        #endregion
    }
}