using FluentMigrator;

namespace Fashion.ERP.Migrator
{
    [Migration(201506250931)]
    public class Migration201506250931 : Migration
    {
        public override void Up()
        {
            Alter.Table("fichatecnica")
                .AlterColumn("segmento_id")
                .AsInt64()
                .Nullable();

            Alter.Table("fichatecnicamodelagem")
                .AlterColumn("arquivo_id")
                .AsInt64()
                .Nullable();

            Delete.Column("quantidadeproducaoaprovada").FromTable("fichatecnica");

            Execute.EmbeddedScript("Fashion.ERP.Migrator.Scripts._201506250931.permissao.sql");
        }

        public override void Down()
        {
        }
    }
}