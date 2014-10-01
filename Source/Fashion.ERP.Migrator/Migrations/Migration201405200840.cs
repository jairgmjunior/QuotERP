using FluentMigrator;

namespace Fashion.ERP.Migrator
{
    [Migration(201405200840)]
    public class Migration201405200840 : Migration
    {
        public override void Up()
        {
            Execute.EmbeddedScript("Fashion.ERP.Migrator.Scripts._201405200840.permissao.sql");
        }

        public override void Down()
        {
        }
    }
}
