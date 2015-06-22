using FluentMigrator;

namespace Fashion.ERP.Migrator
{
    [Migration(201506171508)]
    public class Migration201506171508 : Migration
    {
        public override void Up()
        {
            Create.Table("fichatecnicamodelagem")
                .WithColumn("id").AsInt64().PrimaryKey()
                .WithColumn("idtenant").AsInt64()
                .WithColumn("idempresa").AsInt64()
                .WithColumn("observacao").AsString().Nullable()
                .WithColumn("datamodelagem").AsDateTime()
                .WithColumn("modelista_id").AsInt64()
                .ForeignKey("FK_fichatecnicamodelagem_modelista", "pessoa", "id")
                .WithColumn("arquivo_id").AsInt64()
                .ForeignKey("FK_fichatecnicamodelagem_arquivo", "arquivo", "id");

            Create.Table("fichatecnicamodelagemmedida")
                .WithColumn("id").AsInt64().PrimaryKey()
                .WithColumn("idtenant").AsInt64()
                .WithColumn("idempresa").AsInt64()
                .WithColumn("descricaomedida").AsString()
                .WithColumn("fichatecnicamodelagem_id").AsInt64()
                .ForeignKey("FK_fichatecnicamodelagemmedida_fichatecnicamodelagem", "fichatecnicamodelagem", "id");
            
            Create.Table("fichatecnicamodelagemmedidaitem")
                .WithColumn("id").AsInt64().PrimaryKey()
                .WithColumn("idtenant").AsInt64()
                .WithColumn("idempresa").AsInt64()
                .WithColumn("medida").AsDouble()
                .WithColumn("tamanho_id").AsInt64()
                .ForeignKey("FK_fichatecnicamodelagemmedidaitem_tamanho", "tamanho", "id")
                .WithColumn("fichatecnicamodelagemmedida_id").AsInt64()
                .ForeignKey("FK_fichatecnicamodelagemmedidaitem_fichatecnicamodelagemmedida", "fichatecnicamodelagemmedida", "id");

            Alter.Table("fichatecnica")
                .AddColumn("fichatecnicamodelagem_id")
                .AsInt64()
                .Nullable()
                .ForeignKey("FK_fichatecnica_fichatecnicamodelagem", "fichatecnicamodelagem", "id");

            Alter.Table("fichatecnicafoto")
                .AddColumn("fichatecnica_id")
                .AsInt64()
                .Nullable()
                .ForeignKey("FK_fichatecnicafoto_fichatecnica", "fichatecnica", "id");

            Execute.EmbeddedScript("Fashion.ERP.Migrator.Scripts._201506171508.permissao.sql");
        }

        public override void Down()
        {
        }
    }
}