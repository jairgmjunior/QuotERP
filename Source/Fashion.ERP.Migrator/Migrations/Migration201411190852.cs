using FluentMigrator;

namespace Fashion.ERP.Migrator
{
    [Migration(201411190852)]
    public class Migration201411190852 : Migration
    {
        public override void Up()
        {
            Alter.Table("material")
                .AlterColumn("ncm")
                .AsString()
                .Nullable();

            Alter.Table("material")
                .AlterColumn("familia_id")
                .AsInt64()
                .Nullable();

            Alter.Table("material")
                .AlterColumn("generofiscal_id")
                .AsInt64()
                .Nullable();

            Execute.EmbeddedScript("Fashion.ERP.Migrator.Scripts._201411190852.atualizacaoUniquekeys.sql");
        }

        public override void Down()
        {
        }
    }
}