using System;

namespace Fashion.ERP.Domain.Comum
{
    public class Empresa : DomainTenantBase<Empresa>
    {
        public virtual long Codigo { get; set; }
        public virtual DateTime DataCadastro { get; set; }
        public virtual DateTime? DataAtualizacao { get; set; }
        public virtual bool Ativo { get; set; }
    }
}