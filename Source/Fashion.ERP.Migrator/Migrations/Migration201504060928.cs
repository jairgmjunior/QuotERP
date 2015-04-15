using FluentMigrator;

namespace Fashion.ERP.Migrator
{
    [Migration(201504060928)]
    public class Migration201504060928 : Migration
    {
        public override void Up()
        {
            Alter.Table("fichatecnica")
                .AlterColumn("marca_id")
                .AsInt64()
                .Nullable();

            Execute.EmbeddedScript("Fashion.ERP.Migrator.Scripts._201504060928.permissao.sql");
        }

        public override void Down()
        {
        }
    }
}