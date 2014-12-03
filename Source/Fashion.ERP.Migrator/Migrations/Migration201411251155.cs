using FluentMigrator;

namespace Fashion.ERP.Migrator
{
    [Migration(201411251155)]
    public class Migration201411251155 : Migration
    {
        public override void Up()
        {
            Execute.EmbeddedScript("Fashion.ERP.Migrator.Scripts._201411251155.permissao.sql");
        }

        public override void Down()
        {
        }
    }
}
