using Fashion.ERP.Domain.Comum;
using Fashion.Framework.Mapping;

namespace Fashion.ERP.Mapping.Comum
{
    public class ReferenciaMap : FashionClassMap<Referencia>
    {
        public ReferenciaMap()
            : base("referencia", 10)
        {
            Map(x => x.TipoReferencia).Not.Nullable();
            Map(x => x.Nome).Not.Nullable().Length(100);
            Map(x => x.Telefone).Length(20);
            Map(x => x.Celular).Length(20);
            Map(x => x.Observacao).Length(4000);

            References(x => x.Cliente);
        }
    }
}