using FluentMigrator;

namespace Fashion.ERP.Migrator
{
    [Migration(201504141028)]
    public class Migration201504141028 : Migration
    {
        public override void Up()
        {
            Execute.EmbeddedScript("Fashion.ERP.Migrator.Scripts._201504141028.permissao.sql");
        }

        public override void Down()
        {
        }
    }

}