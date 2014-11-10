using Fashion.ERP.Domain.Compras;
using Fashion.Framework.Mapping;

namespace Fashion.ERP.Mapping.Compras
{
    public class RecebimentoCompraMap : FashionClassMap<RecebimentoCompra>
    {
        public RecebimentoCompraMap()
            : base("recebimentocompra", 0)
        {
            Map(x => x.Numero).Not.Nullable();
            Map(x => x.SituacaoRecebimentoCompra).Not.Nullable();
            Map(x => x.Data).Not.Nullable();
            Map(x => x.DataAlteracao).Not.Nullable();
            Map(x => x.Observacao).Length(4000);
            Map(x => x.Valor).Not.Nullable();
            
            References(x => x.Unidade).Not.Nullable();
            References(x => x.Fornecedor).Not.Nullable();
            References(x => x.EntradaMaterial);

            HasMany(x => x.ConferenciaEntradaMateriais);
            
            HasMany(x => x.RecebimentoCompraItens)
                .Not.KeyNullable()
                .Cascade.AllDeleteOrphan();
            
            HasMany(x => x.DetalhamentoRecebimentoCompraItens)
                .Not.KeyNullable();

            HasManyToMany(x => x.PedidoCompras)
                .Table("recebimentocomprapedidocompra")
                .ParentKeyColumn("recebimentocompra_id")
                .ChildKeyColumn("pedidocompra_id");
        }
    }
}