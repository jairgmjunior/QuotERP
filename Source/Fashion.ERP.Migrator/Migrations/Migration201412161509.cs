using FluentMigrator;

namespace Fashion.ERP.Migrator
{
    [Migration(201412161509)]
    public class Migration201412161509 : Migration
    {
        public override void Up()
        {
            Execute.EmbeddedScript("Fashion.ERP.Migrator.Scripts._201412161509.transportadora.sql");
        }

        public override void Down()
        {
        }
    } 
}