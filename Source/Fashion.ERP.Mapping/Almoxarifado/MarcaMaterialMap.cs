using Fashion.ERP.Domain.Almoxarifado;
using Fashion.Framework.Mapping;

namespace Fashion.ERP.Mapping.Almoxarifado
{
    public class MarcaMaterialMap : FashionClassMap<MarcaMaterial>
    {
        public MarcaMaterialMap()
            : base("marcamaterial", 0)
        {
            Map(x => x.Nome).Length(60).Not.Nullable();
            Map(x => x.Ativo).Not.Nullable();
        }
    }
}