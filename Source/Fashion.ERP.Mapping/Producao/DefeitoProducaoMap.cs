using Fashion.ERP.Domain.Producao;
using Fashion.Framework.Mapping;

namespace Fashion.ERP.Mapping.Producao
{
    public class DefeitoProducaoMap : FashionClassMap<DefeitoProducao>
    {
        public DefeitoProducaoMap()
            : base("defeitoproducao", 1)
        {
            Map(x => x.Descricao).Length(60).Not.Nullable();
        }
    }
}