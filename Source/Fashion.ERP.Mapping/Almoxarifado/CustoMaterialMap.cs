using Fashion.ERP.Domain.Almoxarifado;
using Fashion.Framework.Mapping;

namespace Fashion.ERP.Mapping.Almoxarifado
{
    public class CustoMaterialMap : EmpresaClassMap<CustoMaterial>
    {
        public CustoMaterialMap()
            : base("customaterial", 10)
        {
            Map(x => x.Data).Not.Nullable();
            Map(x => x.CustoAquisicao).Not.Nullable();
            Map(x => x.CustoMedio).Not.Nullable();
            Map(x => x.Custo).Not.Nullable();
            Map(x => x.Ativo).Not.Nullable();

            References(x => x.Fornecedor).Not.Nullable();
            References(x => x.CustoAnterior).Nullable();
        }
    }
}