using Fashion.ERP.Domain.Comum;
using Fashion.Framework.Mapping;

namespace Fashion.ERP.Mapping.Comum
{
    public class ColecaoMap : FashionClassMap<Colecao>
    {
        public ColecaoMap()
            : base("colecao", 0)
        {
            Map(x => x.Descricao).Length(60).Not.Nullable();
            Map(x => x.Ativo).Not.Nullable();
        } 
    }
}