using Fashion.ERP.Domain.Almoxarifado;
using Fashion.Framework.Mapping;

namespace Fashion.ERP.Mapping.Almoxarifado
{
    public class UnidadeMedidaMap : FashionClassMap<UnidadeMedida>
    {
        public UnidadeMedidaMap()
            : base("unidademedida", 0)
        {
            Map(x => x.Descricao).Length(60).Not.Nullable();
            Map(x => x.Sigla).Length(10).Not.Nullable();
            Map(x => x.FatorMultiplicativo).Not.Nullable();
            Map(x => x.Ativo).Not.Nullable();
        }
    }
}