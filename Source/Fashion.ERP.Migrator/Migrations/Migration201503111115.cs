using FluentMigrator;

namespace Fashion.ERP.Migrator
{
    [Migration(201503111115)]
    public class Migration201503111115 : Migration
    {
        public override void Up()
        {
            Execute.EmbeddedScript("Fashion.ERP.Migrator.Scripts._201503111115.permissao.sql");
        }

        public override void Down()
        {
        }
    }
}