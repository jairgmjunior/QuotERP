using FluentMigrator;

namespace Fashion.ERP.Migrator
{
    [Migration(201510081127)]
    public class Migration201510081127 : Migration
    {
        public override void Up()
        {
            Alter.Table("modeloaprovacao")
            .AddColumn("grade_id")
            .AsInt64()
            .Nullable();

            Execute.EmbeddedScript("Fashion.ERP.Migrator.Scripts._201510081127.permissao.sql");
        }

        public override void Down()
        {
        }
    }
}