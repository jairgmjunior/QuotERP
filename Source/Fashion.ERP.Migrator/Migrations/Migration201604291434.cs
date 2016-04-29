using FluentMigrator;

namespace Fashion.ERP.Migrator
{
    [Migration(201604291434)]
    public class Migration201604291434 : Migration
    {
        public override void Up()
        {
            Execute.EmbeddedScript("Fashion.ERP.Migrator.Scripts._201604291434.permissao.sql");
        }

        public override void Down()
        {
            
        }
    }
}
