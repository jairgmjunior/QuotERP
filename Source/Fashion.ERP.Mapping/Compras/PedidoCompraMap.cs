using Fashion.ERP.Domain.Compras;
using Fashion.Framework.Mapping;
using FluentNHibernate.Mapping;

namespace Fashion.ERP.Mapping.Compras
{
    public class PedidoCompraMap : FashionClassMap<PedidoCompra>
    {
        public PedidoCompraMap()
            : base("pedidocompra", 0)
        {
            Map(x => x.Numero).Not.Nullable();
            Map(x => x.DataCompra).Not.Nullable();
            Map(x => x.PrevisaoFaturamento).Not.Nullable();
            Map(x => x.PrevisaoEntrega).Not.Nullable();
            Map(x => x.TipoCobrancaFrete).Not.Nullable();
            Map(x => x.ValorFrete).Not.Nullable();
            Map(x => x.ValorDesconto).Not.Nullable();
            Map(x => x.ValorCompra).Not.Nullable();
            Map(x => x.Observacao).Length(4000);
            Map(x => x.Autorizado).Not.Nullable();
            Map(x => x.DataAutorizacao);
            Map(x => x.ObservacaoAutorizacao).Length(4000);
            Map(x => x.SituacaoCompra).Not.Nullable();
            Map(x => x.Contato).Length(50);

            References(x => x.Comprador).Not.Nullable();
            References(x => x.Fornecedor).Not.Nullable();
            References(x => x.UnidadeEstocadora).Not.Nullable();
            References(x => x.Prazo);
            References(x => x.MeioPagamento);

            HasMany(x => x.PedidoCompraItens)
                .Not.LazyLoad()
                .Inverse()
                .Cascade.AllDeleteOrphan()
                .Access.CamelCaseField(Prefix.Underscore);
        }
    }
}