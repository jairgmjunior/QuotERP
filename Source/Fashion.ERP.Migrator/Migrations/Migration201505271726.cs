using FluentMigrator;

namespace Fashion.ERP.Migrator
{
    [Migration(201505271726)]
    public class Migration201505271726 : Migration
    {
        public override void Up()
        {
            Execute.EmbeddedScript("Fashion.ERP.Migrator.Scripts._201505271726.reservaestoquematerial.sql");
        }

        public override void Down()
        {
        }
    }
}