using Fashion.ERP.Domain.Almoxarifado;
using Fashion.Framework.Mapping;

namespace Fashion.ERP.Mapping.Almoxarifado
{
    public class ConferenciaEntradaMaterialItemMap : FashionClassMap<ConferenciaEntradaMaterialItem>
    {
        public ConferenciaEntradaMaterialItemMap()
            : base("conferenciaentradamaterialitem", 10)
        {
            Map(x => x.Quantidade).Not.Nullable();
            Map(x => x.QuantidadeConferida).Not.Nullable();
            Map(x => x.SituacaoConferencia).Not.Nullable();

            References(x => x.UnidadeMedida).Not.Nullable();
            References(x => x.Material).Not.Nullable();
        }
    }
}