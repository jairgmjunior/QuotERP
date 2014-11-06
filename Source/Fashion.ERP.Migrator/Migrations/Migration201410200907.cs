
using FluentMigrator;

namespace Fashion.ERP.Migrator
{
    [Migration(201410200907)]
    public class Migration201410200907 : Migration
    {
        public override void Up()
        {
            Execute.EmbeddedScript("Fashion.ERP.Migrator.Scripts._201410200907.permissao.sql");
        }

        public override void Down()
        {
        }
    }
}