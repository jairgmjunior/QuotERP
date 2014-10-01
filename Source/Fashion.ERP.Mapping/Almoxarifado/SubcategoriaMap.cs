using Fashion.ERP.Domain.Almoxarifado;
using Fashion.Framework.Mapping;

namespace Fashion.ERP.Mapping.Almoxarifado
{
    public class SubcategoriaMap : FashionClassMap<Subcategoria>
    {
        public SubcategoriaMap()
            : base("subcategoria", 0)
        {
            Map(x => x.Nome).Length(60).Not.Nullable();
            Map(x => x.Ativo).Not.Nullable();

            References(x => x.Categoria);
        }
    }
}