using FluentMigrator;

namespace Fashion.ERP.Migrator
{
    [Migration(201411051737)]
    public class Migration201411051737 : Migration
    {
        public override void Up()
        {
            Execute.EmbeddedScript("Fashion.ERP.Migrator.Scripts._201411051737.permissao.sql");
        }

        public override void Down()
        {
        }
    }
}