using FluentMigrator;

namespace Fashion.ERP.Migrator
{
    [Migration(201409031649)]
    public class Migration201409031649 : Migration
    {
        public override void Up()
        {
            Create.Table("pedidocompraitemcancelado")
                  .WithColumn("id").AsInt64().PrimaryKey()
                  .WithColumn("data").AsDateTime().NotNullable()
                  .WithColumn("quantidadecancelada").AsDouble()
                  .WithColumn("observacao").AsString(4000).Nullable()
                  .WithColumn("motivocancelamentopedidocompra_id").AsInt64().NotNullable()
                  .ForeignKey("FK_pedidocompraitemcancelado_motivocancelamentopedidocompra",
                              "motivocancelamentopedidocompra", "id");

            Alter.Table("pedidocompraitem")
                 .AddColumn("pedidocompraitemcancelado_id")
                 .AsInt64()
                 .Nullable()
                 .ForeignKey("FK_pedidocompraitemcancelado_pedidocompraitem",  "pedidocompraitemcancelado", "id");
            
            Execute.EmbeddedScript("Fashion.ERP.Migrator.Scripts._201409031649.permissao.sql");
        }

        public override void Down()
        {
            Delete.Table("pedidocompraitemcancelado");
           
        }
    }
}
