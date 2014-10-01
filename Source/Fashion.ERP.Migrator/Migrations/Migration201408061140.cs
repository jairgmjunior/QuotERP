using FluentMigrator;

namespace Fashion.ERP.Migrator
{
    [Migration(201408061140)]
    public class Migration201408061140 : Migration
    {
        public override void Up()
        {
            Execute.EmbeddedScript("Fashion.ERP.Migrator.Scripts._201408061140.ConsumoMaterialColecaoView.sql");
        }

        public override void Down()
        {

        }
    }
}
