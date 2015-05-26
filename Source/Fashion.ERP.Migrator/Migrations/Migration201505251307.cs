using FluentMigrator;

namespace Fashion.ERP.Migrator
{
    [Migration(201505251307)]
    public class Migration201505251307 : Migration
    {
        public override void Up()
        {
            Execute.EmbeddedScript("Fashion.ERP.Migrator.Scripts._201505251307.modelomaterialconsumo.sql");
        }

        public override void Down()
        {
        }
    }
}