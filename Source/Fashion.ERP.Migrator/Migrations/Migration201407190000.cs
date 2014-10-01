using FluentMigrator;

namespace Fashion.ERP.Migrator
{
    [Migration(201407190000)]
    public class Migration201407190000 : Migration
    {
        public override void Up()
        {
            // OrdemEntradaCompra
            Create.Table("ordementradacompra")
                .WithColumn("id").AsInt64().PrimaryKey()
                .WithColumn("numero").AsInt64()
                .WithColumn("situacaoordementradacompra").AsString(254)
                .WithColumn("data").AsDateTime()
                .WithColumn("dataalteracao").AsDateTime()
                .WithColumn("observacao").AsString(4000).Nullable()
                .WithColumn("unidadeestocadora_id").AsInt64().ForeignKey("FK_ordementradacompra_unidadeestocadora", "pessoa", "id")
                .WithColumn("comprador_id").AsInt64().ForeignKey("FK_ordementradacompra_comprador", "pessoa", "id")
                .WithColumn("fornecedor_id").AsInt64().ForeignKey("fk_ordementradacompra_fornecedor", "pessoa", "id");

            // Many-to-many: OrdemEntradaCompra <-> PedidoCompra
            Create.Table("ordementradacomprapedidocompra")
                .WithColumn("ordementradacompra_id").AsInt64().ForeignKey("FK_ordementradacomprapedidocompra_ordementradacompra", "ordementradacompra", "id")
                .WithColumn("pedidocompra_id").AsInt64().ForeignKey("FK_ordementradacomprapedidocompra_pedidocompra", "pedidocompra", "id");

            // OrdemEntradaCatalogoMaterial
            Create.Table("ordementradacatalogomaterial")
                .WithColumn("id").AsInt64().PrimaryKey()
                .WithColumn("numero").AsInt64()
                .WithColumn("situacaoordementrada").AsString(254)
                .WithColumn("data").AsDateTime()
                .WithColumn("dataatualizacao").AsDateTime()
                .WithColumn("observacao").AsString(4000).Nullable()
                .WithColumn("comprador_id").AsInt64().ForeignKey("FK_ordementradacatalogomaterial_comprador", "pessoa", "id");

            // OrdemEntradaItemCatalogoMaterial
            Create.Table("ordementradaitemcatalogomaterial")
                .WithColumn("id").AsInt64().PrimaryKey()
                .WithColumn("quantidade").AsDouble()
                .WithColumn("pedidocompraitem_id").AsInt64().ForeignKey("fk_ordementradaitemcatalogomaterial_pedidocompraitem", "pedidocompraitem", "id")
                .WithColumn("ordementradacatalogomaterial_id").AsInt64().ForeignKey("fk_ordementradaitemcatalogomaterial_ordementradacatalogomaterial", "ordementradacatalogomaterial", "id");

            Alter.Table("ordementradacompra")
                .AddColumn("ordementradacatalogomaterial_id").AsInt64().ForeignKey("fk_ordementradacompra_ordementradacatalogomaterial", "ordementradacatalogomaterial", "id");

            Execute.EmbeddedScript("Fashion.ERP.Migrator.Scripts._201407190000.permissao.sql");
        }

        public override void Down()
        {
            Delete.Table("ordementradaitemcatalogomaterial");
            Delete.Table("ordementradacatalogomaterial");
            Delete.Table("ordementradacomprapedidocompra");
            Delete.Table("ordementradacompra");
        }
    }
}