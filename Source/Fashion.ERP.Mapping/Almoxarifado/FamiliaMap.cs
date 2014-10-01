using Fashion.ERP.Domain.Almoxarifado;
using Fashion.Framework.Mapping;

namespace Fashion.ERP.Mapping.Almoxarifado
{
    public class FamiliaMap : FashionClassMap<Familia>
    {
        public FamiliaMap()
            : base("familia", 0)
        {
            Map(x => x.Nome).Length(60).Not.Nullable();
            Map(x => x.Ativo).Not.Nullable();
        }
    }
}