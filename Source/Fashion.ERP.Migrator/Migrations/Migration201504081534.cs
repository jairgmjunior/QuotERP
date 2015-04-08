using FluentMigrator;

namespace Fashion.ERP.Migrator
{
    [Migration(201504081534)]
    public class Migration201504081534 : Migration
    {
        public override void Up()
        {
            Execute.EmbeddedScript("Fashion.ERP.Migrator.Scripts._201504081534.permissao.sql");
        }

        public override void Down()
        {
        }
    }

}