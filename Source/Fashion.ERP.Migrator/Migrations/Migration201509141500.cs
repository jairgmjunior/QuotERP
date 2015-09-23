using FluentMigrator;

namespace Fashion.ERP.Migrator
{
    [Migration(201509141500)]
    public class Migration201509141500 : Migration
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