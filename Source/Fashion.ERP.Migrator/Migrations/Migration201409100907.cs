using FluentMigrator;

namespace Fashion.ERP.Migrator
{
    [Migration(201409100907)]
    public class Migration201409100907 : Migration
    {
        public override void Up()
        {
            Execute.EmbeddedScript("Fashion.ERP.Migrator.Scripts._201409100907.createtabelasconferencia.sql");
        }

        public override void Down()
        {
        }
    }
}