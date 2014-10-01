using FluentMigrator;

namespace Fashion.ERP.Migrator
{
    [Migration(201408041044)]
    public class Migration201408041044 : Migration
    {
        public override void Up()
        {
            Execute.EmbeddedScript("Fashion.ERP.Migrator.Scripts._201408041044.permissao.sql");
        }

        public override void Down()
        {

        }
    }
}
