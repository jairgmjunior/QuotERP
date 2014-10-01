using System;

namespace Fashion.ERP.Domain.Financeiro
{
    public class ExtratoBancario : DomainBase<ExtratoBancario>
    {
        public virtual TipoLancamento TipoLancamento { get; set; }
        public virtual DateTime Emissao { get; set; }
        public virtual DateTime? Compensacao { get; set; }
        public virtual string Descricao { get; set; }
        public virtual double Valor { get; set; }
        public virtual bool Compensado { get; set; }
        public virtual bool Cancelado { get; set; }

        public virtual ContaBancaria ContaBancaria { get; set; }
    }
}