using FluentMigrator;

namespace Fashion.ERP.Migrator
{
    [Migration(201504021522)]
    public class Migration201504021522 : Migration
    {
        public override void Up()
        {
            Execute.EmbeddedScript("Fashion.ERP.Migrator.Scripts._201504021522.permissao.sql");
        }

        public override void Down()
        {
        }
    }
}