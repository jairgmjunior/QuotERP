using Fashion.ERP.Domain.Comum;
using Fashion.Framework.Mapping;

namespace Fashion.ERP.Mapping.Comum
{
    public class GrauDependenciaMap : FashionClassMap<GrauDependencia>
    {
        public GrauDependenciaMap()
            : base("graudependencia", 0)
        {
            Map(x => x.Descricao).Not.Nullable().Length(100);
        }
    }
}