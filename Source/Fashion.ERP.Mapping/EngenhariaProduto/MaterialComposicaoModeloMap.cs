using Fashion.ERP.Domain.EngenhariaProduto;
using Fashion.Framework.Mapping;

namespace Fashion.ERP.Mapping.EngenhariaProduto
{
    public class MaterialComposicaoModeloMap : FashionClassMap<MaterialComposicaoModelo>
    {
        public MaterialComposicaoModeloMap()
            : base("materialcomposicaomodelo", 10)
        {
            Map(x => x.Quantidade).Not.Nullable();

            References(x => x.UnidadeMedida).Not.Nullable();
            References(x => x.Material).Not.Nullable();
            References(x => x.Tamanho);
            References(x => x.Cor);
            References(x => x.Variacao);
        }
    }
}