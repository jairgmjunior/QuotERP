using System;

namespace Fashion.ERP.Domain.Financeiro
{
    public class OcorrenciaCompensacao : DomainBase<OcorrenciaCompensacao>
    {
        public virtual DateTime Data { get; set; }
        public virtual ChequeSituacao ChequeSituacao { get; set; }
        public virtual string Historico { get; set; }
        public virtual string Observacao { get; set; }

        public virtual ChequeRecebido ChequeRecebido { get; set; }
        public virtual CompensacaoCheque CompensacaoCheque { get; set; }
    }
}