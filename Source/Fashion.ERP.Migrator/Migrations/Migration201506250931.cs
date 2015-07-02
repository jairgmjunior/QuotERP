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
            
            Create.Table("programacaoproducaomaterial")
                .WithColumn("id").AsInt64().PrimaryKey()
                .WithColumn("idtenant").AsInt64()
                .WithColumn("idempresa").AsInt64()
                .WithColumn("quantidade").AsDouble()
                .WithColumn("reservamaterial_id").AsInt64().Nullable()
                .ForeignKey("FK_programacaoproducaomaterial_reservamaterial", "reservamaterial", "id")
                .WithColumn("material_id").AsInt64()
                .ForeignKey("FK_programacaoproducaomaterial_material", "material", "id")
                .WithColumn("departamentoproducao_id").AsInt64()
                .ForeignKey("FK_programacaoproducaomaterial_departamentoproducao", "departamentoproducao", "id")
                .WithColumn("programacaoproducao_id").AsInt64()
                .ForeignKey("FK_programacaoproducaomaterial_programacaoproducao", "programacaoproducao", "id");

            Execute.EmbeddedScript("Fashion.ERP.Migrator.Scripts._201506250931.permissao.sql");
            Execute.EmbeddedScript("Fashion.ERP.Migrator.Scripts._201506250931.migracaodadosfichatecnica.sql");
        }

        public override void Down()
        {
        }
    }
}