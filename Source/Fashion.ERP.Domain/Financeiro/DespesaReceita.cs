using System;

namespace Fashion.ERP.Domain.Financeiro
{
    public class DespesaReceita : DomainBase<DespesaReceita>
    {
        public virtual string Descricao { get; set; }
        public virtual bool Ativo { get; set; }
        public virtual TipoDespesaReceita TipoDespesaReceita { get; set; }
    }
}