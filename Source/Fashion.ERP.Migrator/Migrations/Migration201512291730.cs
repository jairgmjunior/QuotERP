using FluentMigrator;

namespace Fashion.ERP.Migrator
{
    [Migration(201512291730)]
    public class Migration201512291730 : Migration
    {
        public override void Up()
        {
            Execute.EmbeddedScript("Fashion.ERP.Migrator.Scripts._201512291730.permissao.sql");
        }

        public override void Down()
        {
            
        }
    }
}
