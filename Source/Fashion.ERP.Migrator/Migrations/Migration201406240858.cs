using FluentMigrator;

namespace Fashion.ERP.Migrator
{
    [Migration(201406240858)]
    public class Migration201406240858: Migration
    {
        public override void Up()
        {
            Create.Table("motivocancelamentopedidocompra")
                .WithColumn("id").AsInt64().PrimaryKey()
                .WithColumn("descricao").AsString(60)
                .WithColumn("ativo").AsBoolean();

            Execute.EmbeddedScript("Fashion.ERP.Migrator.Scripts._201406240858.permissao.sql");
        }

        public override void Down()
        {
            Delete.Table("motivocancelamentopedidocompra");
        }
    }
}