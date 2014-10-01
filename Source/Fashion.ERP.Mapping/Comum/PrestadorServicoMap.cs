using Fashion.ERP.Domain.Comum;
using Fashion.Framework.Mapping;
using FluentNHibernate.Mapping;

namespace Fashion.ERP.Mapping.Comum
{
    public class PrestadorServicoMap : FashionClassMap<PrestadorServico>
    {
        public PrestadorServicoMap()
            : base("prestadorservico", 10)
        {
            Map(x => x.Codigo).Not.Nullable();
            Map(x => x.Comissao).Not.Nullable();
            Map(x => x.DataCadastro).Not.Nullable();
            Map(x => x.Ativo).Not.Nullable();

            HasMany(x => x.TipoPrestadorServicos)
                .Table("tipoprestadorservicoref").KeyColumn("id").Element("tipoprestadorservico")
                .Cascade.AllDeleteOrphan()
                .Access.CamelCaseField(Prefix.Underscore);

            References(x => x.Unidade);
        }
    }
}