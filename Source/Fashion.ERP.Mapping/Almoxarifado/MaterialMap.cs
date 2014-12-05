using Fashion.ERP.Domain.Almoxarifado;
using Fashion.Framework.Mapping;
using FluentNHibernate.Mapping;

namespace Fashion.ERP.Mapping.Almoxarifado
{
    public class MaterialMap : FashionClassMap<Material>
    {
        public MaterialMap()
            : base("material", 10)
        {
            Map(x => x.Referencia).Length(20).Not.Nullable();
            Map(x => x.Descricao).Length(100).Not.Nullable();
            Map(x => x.Detalhamento).Length(200);
            Map(x => x.CodigoBarra).Length(128);
            Map(x => x.Ncm).Length(8);
            Map(x => x.Aliquota).Not.Nullable();
            Map(x => x.PesoBruto).Not.Nullable();
            Map(x => x.PesoLiquido).Not.Nullable();
            Map(x => x.Localizacao).Length(100);
            Map(x => x.Ativo).Not.Nullable();

            References(x => x.OrigemSituacaoTributaria).Not.Nullable();
            References(x => x.Foto).Cascade.All().Fetch.Join().LazyLoad(Laziness.False);
            References(x => x.UnidadeMedida).Not.Nullable();
            References(x => x.MarcaMaterial).Not.Nullable();
            References(x => x.Subcategoria).Not.Nullable();
            References(x => x.TipoItem).Not.Nullable();
            References(x => x.Familia);
            References(x => x.GeneroFiscal);
            References(x => x.Bordado).Cascade.All();
            References(x => x.Tecido).Cascade.All();

            HasMany(x => x.ReferenciaExternas)
                .Not.LazyLoad()
                .Inverse()
                .Cascade.AllDeleteOrphan()
                .Access.CamelCaseField(Prefix.Underscore);

            HasMany(x => x.CustoMaterials)
                .Not.KeyNullable()
                .Cascade.AllDeleteOrphan();
        }
    }
}