using FluentMigrator;

namespace Fashion.ERP.Migrator
{
    [Migration(201406300921)]
    public class Migration201406300921 : Migration
    {
        public override void Up()
        {
            Execute.EmbeddedScript("Fashion.ERP.Migrator.Scripts._201406300921.permissao.sql");
        }

        public override void Down()
        {

        }
    }
}