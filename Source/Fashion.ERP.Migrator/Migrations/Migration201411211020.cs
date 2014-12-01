using FluentMigrator;

namespace Fashion.ERP.Migrator
{
    [Migration(201411211020)]
    public class Migration201411211020 : Migration
    {
        public override void Up()
        {
            Execute.EmbeddedScript("Fashion.ERP.Migrator.Scripts._201411211020.permissao.sql");
        }

        public override void Down()
        {
        }
    }
}