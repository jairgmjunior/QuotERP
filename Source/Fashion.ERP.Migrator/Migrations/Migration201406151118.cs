using FluentMigrator;

namespace Fashion.ERP.Migrator
{
    [Migration(201406151118)]
    public class Migration201406151118 : Migration
    {
        public override void Up()
        {
            Create.Table("parametromodulocompra")
                .WithColumn("id").AsInt64().PrimaryKey()
                .WithColumn("validarecebimentopedido").AsBoolean();

            Execute.EmbeddedScript("Fashion.ERP.Migrator.Scripts._201406151118.permissao.sql");
        }

        public override void Down()
        {
            Delete.Table("parametromodulocompra");
        }
    }
}
