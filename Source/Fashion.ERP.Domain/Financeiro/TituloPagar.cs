using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Fashion.ERP.Domain.Comum;

namespace Fashion.ERP.Domain.Financeiro
{
    public class TituloPagar : DomainEmpresaBase<TituloPagar>
    {
        private readonly IList<TituloPagarBaixa> _tituloPagarBaixas;
        private readonly IList<RateioCentroCusto> _rateioCentroCustos;
        private readonly IList<RateioDespesaReceita> _rateioDespesaReceitas;

        public TituloPagar()
        {
            _tituloPagarBaixas = new List<TituloPagarBaixa>();
            _rateioCentroCustos = new List<RateioCentroCusto>();
            _rateioDespesaReceitas = new List<RateioDespesaReceita>();
        }

        public virtual string Numero { get; set; }
        public virtual int Parcela { get; set; }
        public virtual int Plano { get; set; }
        public virtual DateTime Emissao { get; set; }
        public virtual DateTime Vencimento { get; set; }
        public virtual DateTime Prorrogacao { get; set; }
        public virtual double Valor { get; set; }
        public virtual double SaldoDevedor { get; set; }
        public virtual string Historico { get; set; }
        public virtual string Observacao { get; set; }
        public virtual bool Provisorio { get; set; }
        public virtual SituacaoTitulo SituacaoTitulo { get; set; }
        public virtual DateTime DataCadastro { get; set; }
        public virtual DateTime DataAlteracao { get; set; }

        public virtual Pessoa Fornecedor { get; set; }
        public virtual Pessoa Unidade { get; set; }
        public virtual Banco Banco { get; set; }

        #region TituloPagarBaixas
        public virtual IReadOnlyCollection<TituloPagarBaixa> TituloPagarBaixas
        {
            get { return new ReadOnlyCollection<TituloPagarBaixa>(_tituloPagarBaixas); }
        }

        public virtual void AddTituloPagarBaixa(params TituloPagarBaixa[] tituloPagarBaixas)
        {
            foreach (var tituloPagarBaixa in tituloPagarBaixas)
            {
                tituloPagarBaixa.TituloPagar = this;
                _tituloPagarBaixas.Add(tituloPagarBaixa);
            }
        }

        public virtual void RemoveTituloPagarBaixa(params TituloPagarBaixa[] tituloPagarBaixas)
        {
            foreach (var tituloPagarBaixa in tituloPagarBaixas)
            {
                if (_tituloPagarBaixas.Contains(tituloPagarBaixa))
                    _tituloPagarBaixas.Remove(tituloPagarBaixa);
            }
        }

        public virtual void ClearTituloPagarBaixa()
        {
            _tituloPagarBaixas.Clear();
        }

        #endregion

        #region RateioCentroCustos
        public virtual IReadOnlyCollection<RateioCentroCusto> RateioCentroCustos
        {
            get { return new ReadOnlyCollection<RateioCentroCusto>(_rateioCentroCustos); }
        }

        public virtual void AddRateioCentroCusto(params RateioCentroCusto[] rateioCentroCustos)
        {
            foreach (var rateioCentroCusto in rateioCentroCustos)
            {
                rateioCentroCusto.TituloPagar = this;
                _rateioCentroCustos.Add(rateioCentroCusto);
            }
        }

        public virtual void RemoveRateioCentroCusto(params RateioCentroCusto[] rateioCentroCustos)
        {
            foreach (var rateioCentroCusto in rateioCentroCustos)
            {
                if (_rateioCentroCustos.Contains(rateioCentroCusto))
                    _rateioCentroCustos.Remove(rateioCentroCusto);
            }
        }

        public virtual void ClearRateioCentroCusto()
        {
            _rateioCentroCustos.Clear();
        }

        #endregion

        #region RateioDespesaReceitas
        public virtual IReadOnlyCollection<RateioDespesaReceita> RateioDespesaReceitas
        {
            get { return new ReadOnlyCollection<RateioDespesaReceita>(_rateioDespesaReceitas); }
        }

        public virtual void AddRateioDespesaReceita(params RateioDespesaReceita[] rateioDespesaReceitas)
        {
            foreach (var rateioDespesaReceita in rateioDespesaReceitas)
            {
                rateioDespesaReceita.TituloPagar = this;
                _rateioDespesaReceitas.Add(rateioDespesaReceita);
            }
        }

        public virtual void RemoveRateioDespesaReceita(params RateioDespesaReceita[] rateioDespesaReceitas)
        {
            foreach (var rateioDespesaReceita in rateioDespesaReceitas)
            {
                if (_rateioDespesaReceitas.Contains(rateioDespesaReceita))
                    _rateioDespesaReceitas.Remove(rateioDespesaReceita);
            }
        }

        public virtual void ClearRateioDespesaReceita()
        {
            _rateioDespesaReceitas.Clear();
        }

        #endregion
    }
}