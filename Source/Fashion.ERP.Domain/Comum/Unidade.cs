using System;

namespace Fashion.ERP.Domain.Comum
{
    public class Unidade : DomainEmpresaBase<Unidade>
    {
        public virtual long Codigo { get; set; }
        public virtual DateTime DataAbertura { get; set; }
        public virtual DateTime DataCadastro { get; set; }
        public virtual bool Ativo { get; set; }
    }
}