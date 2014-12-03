using FluentMigrator;

namespace Fashion.ERP.Migrator
{
    [Migration(201412030148)]
    public class Migration201412030148 : Migration
    {
        public override void Up()
        {
            Execute.EmbeddedScript("Fashion.ERP.Migrator.Scripts._201412030148.permissao.sql");
        }

        public override void Down()
        {
        }
    }
}
