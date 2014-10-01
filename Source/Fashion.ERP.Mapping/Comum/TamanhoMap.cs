using Fashion.ERP.Domain.Comum;
using Fashion.Framework.Mapping;

namespace Fashion.ERP.Mapping.Comum
{
    public class TamanhoMap : FashionClassMap<Tamanho>
    {
        public TamanhoMap()
            : base("tamanho", 0)
        {
            Map(x => x.Descricao).Length(60).Not.Nullable();
            Map(x => x.Sigla).Length(10).Not.Nullable();
            Map(x => x.Ativo).Not.Nullable();
        }
    }
}