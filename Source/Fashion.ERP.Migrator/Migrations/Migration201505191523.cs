using FluentMigrator;

namespace Fashion.ERP.Migrator
{
    [Migration(201505191523)]
    public class Migration201505191523 : Migration
    {
        public override void Up()
        {
            Execute.EmbeddedScript("Fashion.ERP.Migrator.Scripts._201505191523.permissao.sql");
        }

        public override void Down()
        {
        }
    }
}