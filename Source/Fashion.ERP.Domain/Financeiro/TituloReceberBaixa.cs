using System;

namespace Fashion.ERP.Domain.Financeiro
{
    public class TituloReceberBaixa : DomainBase<TituloReceberBaixa>
    {
        public virtual long NumeroBaixa { get; set; }
        public virtual DateTime DataRecebimento { get; set; }
        public virtual string Historico { get; set; }
        public virtual double ValorBaixa { get; set; }
        public virtual double Multa { get; set; }
        public virtual double Juros { get; set; }
        public virtual double Descontos { get; set; }
        public virtual double Despesas { get; set; }
        public virtual double ValorRecebido { get; set; }
        public virtual string Observacao { get; set; }
        public virtual TipoBaixaReceber TipoBaixaReceber { get; set; }

        public virtual TituloReceber TituloReceber { get; set; }
    }
}