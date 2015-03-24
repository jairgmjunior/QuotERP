using FluentMigrator;

namespace Fashion.ERP.Migrator
{
    [Migration(201503231600)]
    public class Migration201503231600 : Migration
    {
        public override void Up()
        {
            Create.Table("fichatecnicasequenciaoperacional")
                .WithColumn("id").AsInt64().PrimaryKey()
                .WithColumn("idtenant").AsInt64()
                .WithColumn("idempresa").AsInt64()
                .WithColumn("custo").AsDouble()
                .WithColumn("tempo").AsDouble()
                .WithColumn("pesoprodutividade").AsDouble()
                .WithColumn("setorproducao_id").AsInt64()
                .ForeignKey("FK_fichatecnicasequenciaoperacional_setorproducao", "setorproducao", "id")
                .WithColumn("departamentoproducao_id").AsInt64()
                .ForeignKey("FK_fichatecnicasequenciaoperacional_departamentoproducao", "departamentoproducao", "id")
                .WithColumn("operacaoproducao_id").AsInt64()
                .ForeignKey("FK_fichatecnicasequenciaoperacional_operacaoproducao", "operacaoproducao", "id")
                .WithColumn("fichatecnica_id").AsInt64()
                .ForeignKey("FK_fichatecnicasequenciaoperacional_fichatecnica", "fichatecnica", "id");

            //Execute.EmbeddedScript("Fashion.ERP.Migrator.Scripts._201513031748.permissao.sql");
        }

        public override void Down()
        {
        }
    }
}