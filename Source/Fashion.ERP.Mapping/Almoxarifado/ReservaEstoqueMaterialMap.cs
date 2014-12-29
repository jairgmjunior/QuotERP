using Fashion.ERP.Domain.Almoxarifado;
using Fashion.Framework.Mapping;

namespace Fashion.ERP.Mapping.Almoxarifado
{
    public class ReservaEstoqueMaterialMap : EmpresaClassMap<ReservaEstoqueMaterial>
    {
        public ReservaEstoqueMaterialMap() : base("reservaestoquematerial", 0)
        {
            Map(x => x.Quantidade).Not.Nullable();
            References(x => x.Material).Not.Nullable();
            References(x => x.Unidade).Not.Nullable();
        }
    }
}