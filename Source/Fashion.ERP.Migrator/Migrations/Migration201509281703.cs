using FluentMigrator;

namespace Fashion.ERP.Migrator
{
    [Migration(201509281703)]
    public class Migration201509281703 : Migration
    {
        public override void Up()
        {
            Execute.EmbeddedScript("Fashion.ERP.Migrator.Scripts._201509281703.permissao.sql");
        }

        public override void Down()
        {
        }
    }
}