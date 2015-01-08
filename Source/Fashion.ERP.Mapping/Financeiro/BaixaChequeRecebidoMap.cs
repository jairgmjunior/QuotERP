using Fashion.ERP.Domain.Financeiro;
using Fashion.Framework.Mapping;
using FluentNHibernate.Mapping;

namespace Fashion.ERP.Mapping.Financeiro
{
    public class BaixaChequeRecebidoMap : FashionClassMap<BaixaChequeRecebido>
    {
        public BaixaChequeRecebidoMap()
            : base("baixachequerecebido", 100)
        {
            Map(x => x.Data).Not.Nullable();
            Map(x => x.Despesa).Not.Nullable();
            Map(x => x.ValorJuros).Not.Nullable();
            Map(x => x.ValorDesconto).Not.Nullable();
            Map(x => x.Valor).Not.Nullable();
            Map(x => x.Historico).Length(4000);
            Map(x => x.Observacao).Length(4000);

            References(x => x.ChequeRecebido).Not.Nullable();
            References(x => x.Cobrador);

            HasMany(x => x.RecebimentoChequeRecebidos)
                .Inverse()
                .Cascade.AllDeleteOrphan()
                .Access.CamelCaseField(Prefix.Underscore);
        }
    }
}