using Fashion.ERP.Domain.Compras;
using Fashion.Framework.Mapping;
using FluentNHibernate.Mapping;

namespace Fashion.ERP.Mapping.Compras
{
    public class OrdemEntradaCompraMap : FashionClassMap<OrdemEntradaCompra>
    {
        public OrdemEntradaCompraMap()
            : base("ordementradacompra", 0)
        {
            Map(x => x.Numero).Not.Nullable();
            Map(x => x.SituacaoOrdemEntradaCompra).Not.Nullable();
            Map(x => x.Data).Not.Nullable();
            Map(x => x.DataAlteracao).Not.Nullable();
            Map(x => x.Observacao).Length(4000);

            References(x => x.UnidadeEstocadora).Not.Nullable();
            References(x => x.Comprador).Not.Nullable();
            References(x => x.ConferenciaEntradaMaterial).Not.Nullable();
            References(x => x.Fornecedor).Not.Nullable();

            HasManyToMany(x => x.PedidoCompras)
                .Table("ordementradacomprapedidocompra")
                .ParentKeyColumn("ordementradacompra_id")
                .ChildKeyColumn("pedidocompra_id")
                .Access.CamelCaseField(Prefix.Underscore);
        }
    }
}