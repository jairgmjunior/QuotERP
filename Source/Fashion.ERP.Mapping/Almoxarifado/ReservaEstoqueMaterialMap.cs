using Fashion.ERP.Domain.Almoxarifado;
using Fashion.Framework.Mapping;

namespace Fashion.ERP.Mapping.Almoxarifado
{
    public class ReservaEstoqueMaterialMap : EmpresaClassMap<ReservaEstoqueMaterial>
    {
        public ReservaEstoqueMaterialMap() : base("reservaestoquematerial", 0)
        {
            Map(x => x.Quantidade).Not.Nullable();

            HasMany(x => x.ReservaMaterialItems)
                .KeyNullable();
        }
    }
}