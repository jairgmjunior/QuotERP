using FluentMigrator;

namespace Fashion.ERP.Migrator
{
    [Migration(201504201721)]
    public class Migration201504201721 : Migration
    {
        public override void Up()
        {
            Execute.EmbeddedScript("Fashion.ERP.Migrator.Scripts._201504201721.modeloavaliacao.sql");
        }

        public override void Down()
        {
        }
    }
}