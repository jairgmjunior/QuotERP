using Fashion.ERP.Domain.Almoxarifado;
using Fashion.Framework.Mapping;

namespace Fashion.ERP.Mapping.Almoxarifado
{
    public class ConferenciaEntradaMaterialMap : FashionClassMap<ConferenciaEntradaMaterial>
    {
        public ConferenciaEntradaMaterialMap()
            : base("conferenciaentradamaterial", 0)
        {
            Map(x => x.Numero).Not.Nullable();
            Map(x => x.Data).Not.Nullable();
            Map(x => x.DataAtualizacao).Not.Nullable();
            Map(x => x.Observacao).Length(4000);
            Map(x => x.Conferido).Not.Nullable();
            Map(x => x.Autorizado).Not.Nullable();

            References(x => x.Comprador).Not.Nullable();

            HasMany(x => x.ConferenciaEntradaMaterialItens)
                .Not.KeyNullable()
                .Cascade.AllDeleteOrphan();
        } 
    }
}