using Fashion.ERP.Domain.Financeiro;
using Fashion.Framework.Mapping;

namespace Fashion.ERP.Mapping.Financeiro
{
    public class OcorrenciaCompensacaoMap : FashionClassMap<OcorrenciaCompensacao>
    {
        public OcorrenciaCompensacaoMap()
            : base("ocorrenciacompensacao", 10)
        {
            Map(x => x.Data).Not.Nullable();
            Map(x => x.ChequeSituacao).Not.Nullable();
            Map(x => x.Historico).Length(4000);
            Map(x => x.Observacao).Length(4000);

            References(x => x.ChequeRecebido).Not.Nullable();
            References(x => x.CompensacaoCheque);
        }
    }
}