using FluentMigrator;

namespace Fashion.ERP.Migrator
{
    [Migration(201410031432)]
    public class Migration201410031432 : Migration
    {
        public override void Up()
        {
            Execute.EmbeddedScript("Fashion.ERP.Migrator.Scripts._201410031432.atualizeViewConsumoMaterial.sql");
        }

        public override void Down()
        {
        }
    }
}