using FluentMigrator;

namespace Fashion.ERP.Migrator
{
    [Migration(201509231712)]
    public class Migration201509231712 : Migration
    {
        public override void Up()
        {
            Execute.EmbeddedScript("Fashion.ERP.Migrator.Scripts._201509231712.permissao.sql");
        }

        public override void Down()
        {
        }
    }
}