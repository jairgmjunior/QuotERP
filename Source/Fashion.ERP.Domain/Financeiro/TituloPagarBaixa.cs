using System;

namespace Fashion.ERP.Domain.Financeiro
{
    public class TituloPagarBaixa : DomainEmpresaBase<TituloPagarBaixa>
    {
        //todo retirar essa propriedade deve ser retirada do domínio 
        public virtual long NumeroBaixa { get; set; }
        public virtual DateTime DataPagamento { get; set; }
        public virtual DateTime DataAlteracao { get; set; }
        public virtual double Juros { get; set; }
        public virtual double Descontos { get; set; }
        public virtual double Despesas { get; set; }
        public virtual double Valor { get; set; }
        public virtual string Historico { get; set; }
        public virtual string Observacao { get; set; }

        public virtual TituloPagar TituloPagar { get; set; }
    }
}