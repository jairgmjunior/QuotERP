using System;

namespace Fashion.ERP.Domain.Comum
{
    public class Fornecedor : DomainBase<Fornecedor>
    {
        public virtual long Codigo { get; set; }
        public virtual DateTime DataCadastro { get; set; }
        public virtual bool Ativo { get; set; }
        public virtual TipoFornecedor TipoFornecedor { get; set; }
    }
}