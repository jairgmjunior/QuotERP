using Fashion.ERP.Domain.Comum;
using Fashion.Framework.Mapping;

namespace Fashion.ERP.Mapping.Comum
{
    public class VariacaoMap : FashionClassMap<Variacao>
    {
        public VariacaoMap()
            : base("variacao", 10)
        {
            Map(x => x.Nome).Length(100).Not.Nullable();
            Map(x => x.Ativo).Not.Nullable();
        }
    }
}