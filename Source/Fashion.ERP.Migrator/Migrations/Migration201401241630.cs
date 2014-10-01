using FluentMigrator;

namespace Fashion.ERP.Migrator
{
    [Migration(201401241630)]
    public class Migration201401241630 : Migration
    {
        public override void Up()
        {
            // Aprovação do modelo
            Alter.Table("modelo")
                .AddColumn("observacaoaprovacao").AsString(250).Nullable();

            Execute.EmbeddedScript("Fashion.ERP.Migrator.Scripts._201401241630.permissao.sql");
        }

        public override void Down()
        {
            Delete.Column("observacaoaprovacao").FromTable("modelo");
        }
    }
}