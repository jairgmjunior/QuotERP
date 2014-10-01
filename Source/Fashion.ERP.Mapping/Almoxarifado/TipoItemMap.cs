using Fashion.ERP.Domain.Almoxarifado;
using Fashion.Framework.Mapping;

namespace Fashion.ERP.Mapping.Almoxarifado
{
    public class TipoItemMap : FashionClassMap<TipoItem>
    {
        public TipoItemMap()
            : base("tipoitem", 0)
        {
            Map(x => x.Descricao).Length(100).Not.Nullable();
            Map(x => x.Codigo).Length(2).Not.Nullable();
        }
    }
}