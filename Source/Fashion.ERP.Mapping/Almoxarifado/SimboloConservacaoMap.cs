using Fashion.ERP.Domain.Almoxarifado;
using Fashion.Framework.Mapping;
using FluentNHibernate.Mapping;

namespace Fashion.ERP.Mapping.Almoxarifado
{
    public class SimboloConservacaoMap : FashionClassMap<SimboloConservacao>
    {
        public SimboloConservacaoMap()
            : base("simboloconservacao", 0)
        {
            Map(x => x.Descricao).Length(200).Not.Nullable();
            Map(x => x.CategoriaConservacao).Not.Nullable();
            References(x => x.Foto).Cascade.All().Fetch.Join().LazyLoad(Laziness.False);
        }
    }
}
