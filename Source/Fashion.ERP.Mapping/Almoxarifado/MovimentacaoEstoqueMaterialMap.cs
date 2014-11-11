using Fashion.ERP.Domain.Almoxarifado;
using Fashion.Framework.Mapping;

namespace Fashion.ERP.Mapping.Almoxarifado
{
    public class MovimentacaoEstoqueMaterialMap: FashionClassMap<MovimentacaoEstoqueMaterial>
    {
        public MovimentacaoEstoqueMaterialMap()
            : base("movimentacaoestoquematerial", 10)
        {
            Map(x => x.Quantidade).Not.Nullable();
            Map(x => x.Data).Not.Nullable();
            Map(x => x.TipoMovimentacaoEstoqueMaterial).Not.Nullable();

            References(x => x.EstoqueMaterial).Not.Nullable().Cascade.SaveUpdate();
        } 
    }
}