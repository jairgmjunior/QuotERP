using System;

namespace Fashion.ERP.Domain.Comum
{
    public class InformacaoBancaria : DomainBase<InformacaoBancaria>
    {
        public virtual string Agencia { get; set; }
        public virtual string Conta { get; set; }
        public virtual TipoConta? TipoConta { get; set; }
        public virtual DateTime? DataAbertura { get; set; }
        public virtual string Titular { get; set; }
        public virtual string Telefone { get; set; }

        public virtual Pessoa Pessoa { get; set; }
        public virtual Banco Banco { get; set; }
    }
}