using FluentMigrator;

namespace Fashion.ERP.Migrator
{
    [Migration(201509031413)]
    public class Migration201509031413 : Migration
    {
        public override void Up()
        {
            Execute.EmbeddedScript("Fashion.ERP.Migrator.Scripts._201509031413.permissao.sql");
        }

        public override void Down()
        {
        }
    }
}