using System;

namespace Fashion.ERP.Domain.Compras
{
    public class Transportadora : DomainBase<Transportadora>
    {
        public virtual long Codigo { get; set; }
        public virtual DateTime DataCadastro { get; set; }
        public virtual bool Ativo { get; set; }
    }
}