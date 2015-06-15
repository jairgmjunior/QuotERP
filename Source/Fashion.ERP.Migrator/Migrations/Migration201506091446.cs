using FluentMigrator;

namespace Fashion.ERP.Migrator
{
    [Migration(201506091446)]
    public class Migration201506091446 : Migration
    {
        public override void Up()
        {
            Create.Table("programacaoproducaomatrizcorte")
                .WithColumn("id").AsInt64().PrimaryKey()
                .WithColumn("idtenant").AsInt64()
                .WithColumn("idempresa").AsInt64()
                .WithColumn("tipoenfestotecido").AsString();
            
            Create.Table("programacaoproducaomatrizcorteitem")
                .WithColumn("id").AsInt64().PrimaryKey()
                .WithColumn("idtenant").AsInt64()
                .WithColumn("idempresa").AsInt64()
                .WithColumn("quantidade").AsInt64()
                .WithColumn("quantidadevezes").AsInt64()
                .WithColumn("tamanho_id").AsInt64()
                .ForeignKey("FK_programacaoproducaomatrizcorteitem_tamanho", "tamanho", "id")
                .WithColumn("programacaoproducaomatrizcorte_id").AsInt64()
                .ForeignKey("FK_programacaoproducaomatrizcorteitem_programacaoproducaomatrizcorte", "programacaoproducaomatrizcorte", "id");
                
            Create.Table("programacaoproducao")
                .WithColumn("id").AsInt64().PrimaryKey()
                .WithColumn("idtenant").AsInt64()
                .WithColumn("idempresa").AsInt64()
                .WithColumn("numero").AsInt64()
                .WithColumn("data").AsDate()
                .WithColumn("dataprogramada").AsDate()
                .WithColumn("dataalteracao").AsDate()
                .WithColumn("observacao").AsString().Nullable()
                .WithColumn("quantidade").AsInt64()
                .WithColumn("funcionario_id").AsInt64()
                .ForeignKey("FK_programacaoproducao_funcionario", "pessoa", "id")
                .WithColumn("colecao_id").AsInt64()
                .ForeignKey("FK_programacaoproducao_colecao", "colecao", "id")
                .WithColumn("fichatecnica_id").AsInt64()
                .ForeignKey("FK_programacaoproducao_fichatecnica", "fichatecnica", "id")
                .WithColumn("programacaoproducaomatrizcorte_id").AsInt64()
                .ForeignKey("FK_programacaoproducao_programacaoproducaomatrizcorte", "programacaoproducaomatrizcorte", "id");


            Execute.EmbeddedScript("Fashion.ERP.Migrator.Scripts._201506091446.permissao.sql");
        }

        public override void Down()
        {
        }
    }
}