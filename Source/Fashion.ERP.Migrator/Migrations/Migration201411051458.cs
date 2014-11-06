using FluentMigrator;

namespace Fashion.ERP.Migrator
{
    [Migration(201411051458)]
    public class Migration201411051458 : Migration
    {
        public override void Up()
        {
            Execute.EmbeddedScript("Fashion.ERP.Migrator.Scripts._201411051458.permissao.sql");
        }

        public override void Down()
        {
        }
    }
}