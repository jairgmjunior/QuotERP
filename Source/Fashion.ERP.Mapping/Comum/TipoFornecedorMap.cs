using Fashion.ERP.Domain.Comum;
using Fashion.Framework.Mapping;

namespace Fashion.ERP.Mapping.Comum
{
    public class TipoFornecedorMap : FashionClassMap<TipoFornecedor>
    {
        public TipoFornecedorMap()
            : base("tipofornecedor", 0)
        {
            Map(x => x.Descricao).Not.Nullable().Length(100);
        }
    }
}