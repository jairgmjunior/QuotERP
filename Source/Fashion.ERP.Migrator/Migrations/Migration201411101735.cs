using FluentMigrator;

namespace Fashion.ERP.Migrator
{
    [Migration(201411101735)]
    public class Migration201411101735 : Migration
    {
        public override void Up()
        {
            Execute.EmbeddedScript("Fashion.ERP.Migrator.Scripts._201411101735.estoquematerial.sql");
        }

        public override void Down()
        {
        }
    }
}