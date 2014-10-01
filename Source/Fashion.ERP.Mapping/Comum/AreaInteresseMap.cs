using Fashion.ERP.Domain.Comum;
using Fashion.Framework.Mapping;

namespace Fashion.ERP.Mapping.Comum
{
    public class AreaInteresseMap : FashionClassMap<AreaInteresse>
    {
        public AreaInteresseMap()
            : base("areainteresse", 0)
        {
            Map(x => x.Nome).Length(100).Not.Nullable();
        }
    }
}