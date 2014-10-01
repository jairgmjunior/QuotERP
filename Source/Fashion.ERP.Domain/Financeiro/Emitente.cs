using System;
using Fashion.ERP.Domain.Comum;

namespace Fashion.ERP.Domain.Financeiro
{
    public class Emitente : DomainBase<Emitente>
    {
        public virtual string Agencia { get; set; }
        public virtual string Conta { get; set; }
        public virtual string Nome1 { get; set; }
        public virtual string CpfCnpj1 { get; set; }
        public virtual string Documento1 { get; set; }
        public virtual string OrgaoExpedidor1 { get; set; }
        public virtual string Nome2 { get; set; }
        public virtual string CpfCnpj2 { get; set; }
        public virtual string Documento2 { get; set; }
        public virtual string OrgaoExpedidor2 { get; set; }
        public virtual DateTime ClienteDesde { get; set; }
        public virtual bool Ativo { get; set; }

        public virtual Banco Banco { get; set; }
    }
}