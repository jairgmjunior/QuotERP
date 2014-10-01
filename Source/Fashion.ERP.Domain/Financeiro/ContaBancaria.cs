using System;
using Fashion.ERP.Domain.Comum;

namespace Fashion.ERP.Domain.Financeiro
{
    public class ContaBancaria : DomainBase<ContaBancaria>
    {
        public virtual string Agencia { get; set; }
        public virtual string NomeAgencia { get; set; }
        public virtual string Conta { get; set; }
        public virtual TipoContaBancaria TipoContaBancaria { get; set; }
        public virtual string Gerente { get; set; }
        public virtual DateTime? Abertura { get; set; }
        public virtual string Telefone { get; set; }

        public virtual Banco Banco { get; set; }
    }
}