using Fashion.ERP.Domain.Almoxarifado;
using Fashion.Framework.Mapping;

namespace Fashion.ERP.Mapping.Almoxarifado
{
    public class CategoriaMap : FashionClassMap<Categoria>
    {
        public CategoriaMap()
            : base("categoria", 0)
        {
            Map(x => x.Nome).Length(60).Not.Nullable();
            Map(x => x.CodigoNcm).Length(8).Not.Nullable();
            Map(x => x.GeneroCategoria).Not.Nullable();
            Map(x => x.TipoCategoria).Not.Nullable();
            Map(x => x.Ativo).Not.Nullable();
        }
    }
}