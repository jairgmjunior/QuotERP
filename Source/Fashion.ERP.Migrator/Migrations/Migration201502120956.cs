using FluentMigrator;

namespace Fashion.ERP.Migrator
{
    [Migration(201502120956)]
    public class Migration201502120956 : Migration
    {
        public override void Up()
        {
            Alter.Table("modelo")
                .AddColumn("fichatecnica_id")
                .AsInt64()
                .Nullable()
                .ForeignKey("FK_modelo_fichatecnica", "fichatecnica", "id");

            Execute.EmbeddedScript("Fashion.ERP.Migrator.Scripts._201502120956.modelofichatecnica.sql");

            Delete.ForeignKey("FK_fichatecnica_modelo").OnTable("fichatecnica");
            Delete.Column("modelo_id").FromTable("fichatecnica");
        }

        public override void Down()
        {
        }
    }
}