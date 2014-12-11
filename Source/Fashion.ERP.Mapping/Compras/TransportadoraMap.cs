using Fashion.ERP.Domain.Compras;
using Fashion.Framework.Mapping;

namespace Fashion.ERP.Mapping.Compras
{
    public class TransportadoraMap : FashionClassMap<Transportadora>
    {
        public TransportadoraMap()
            : base("transportadora", 10)
        {
            Map(x => x.Codigo).Not.Nullable();
            Map(x => x.DataCadastro).Not.Nullable();
            Map(x => x.Ativo).Not.Nullable();
        }
    }
}