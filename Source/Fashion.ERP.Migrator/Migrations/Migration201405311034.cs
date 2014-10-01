using FluentMigrator;

namespace Fashion.ERP.Migrator
{
    [Migration(201405311034)]
    public class Migration201405311034 : Migration
    {
        public override void Up()
        {
            // PedidoCompra
            Create.Table("pedidocompra")
                .WithColumn("id").AsInt64().PrimaryKey()
                .WithColumn("numero").AsInt64()
                .WithColumn("datacompra").AsDateTime()
                .WithColumn("previsaofaturamento").AsDateTime()
                .WithColumn("previsaoentrega").AsDateTime()
                .WithColumn("dataentrega").AsDateTime().Nullable()
                .WithColumn("tipocobrancafrete").AsString(255)
                .WithColumn("valorfrete").AsDouble()
                .WithColumn("valordesconto").AsDouble()
                .WithColumn("valorcompra").AsDouble()
                .WithColumn("observacao").AsString(4000).Nullable()
                .WithColumn("autorizado").AsBoolean()
                .WithColumn("dataautorizacao").AsDateTime().Nullable()
                .WithColumn("observacaoautorizacao").AsString(4000).Nullable()
                .WithColumn("situacaocompra").AsString(255)
                .WithColumn("contato").AsString(50)
                .WithColumn("comprador_id").AsInt64().ForeignKey("FK_pedidocompra_comprador", "pessoa", "id")
                .WithColumn("fornecedor_id").AsInt64().ForeignKey("FK_pedidocompra_fornecedor", "pessoa", "id")
                .WithColumn("unidadeestocadora_id").AsInt64().ForeignKey("FK_pedidocompra_unidadeestocadora", "pessoa", "id")
                .WithColumn("prazo_id").AsInt64().Nullable().ForeignKey("FK_pedidocompra_prazo", "prazo", "id")
                .WithColumn("meiopagamento_id").AsInt64().Nullable().ForeignKey("FK_pedidocompra_meiopagamento", "meiopagamento", "id");

            // PedidoCompraItem
            Create.Table("pedidocompraitem")
                .WithColumn("id").AsInt64().PrimaryKey()
                .WithColumn("quantidade").AsDouble()
                .WithColumn("valorunitario").AsDouble()
                .WithColumn("previsaoentrega").AsDateTime().Nullable()
                .WithColumn("quantidadeentrega").AsDouble()
                .WithColumn("dataentrega").AsDateTime().Nullable()
                .WithColumn("situacaocompra").AsString(255)
                .WithColumn("pedidocompra_id").AsInt64().ForeignKey("FK_pedidocompraitem_pedidocompra", "pedidocompra", "id")
                .WithColumn("catalogomaterial_id").AsInt64().ForeignKey("FK_pedidocompraitem_catalogomaterial", "catalogomaterial", "id")
                .WithColumn("unidademedida_id").AsInt64().ForeignKey("FK_pedidocompraitem_unidademedida", "unidademedida", "id");
                
            Execute.EmbeddedScript("Fashion.ERP.Migrator.Scripts._201405311034.permissao.sql");
        }

        public override void Down()
        {
            Delete.Table("pedidocompra");
            Delete.Table("pedidocompraitem");
        }
    }
}
