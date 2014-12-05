using FluentMigrator;

namespace Fashion.ERP.Migrator
{
    [Migration(201412041239)]
    public class Migration201412041239 : Migration
    {
        public override void Up()
        {
            Execute.EmbeddedScript("Fashion.ERP.Migrator.Scripts._201412041239.permissao.sql");
        }

        public override void Down()
        {
        }
    }
}
