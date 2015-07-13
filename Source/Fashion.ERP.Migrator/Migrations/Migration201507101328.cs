using FluentMigrator;

namespace Fashion.ERP.Migrator
{
    [Migration(201507101328)]
    public class Migration201507101328 : Migration
    {
        public override void Up()
        {
            Execute.EmbeddedScript("Fashion.ERP.Migrator.Scripts._201507101328.permissao.sql");
        }

        public override void Down()
        {
        }
    }
}