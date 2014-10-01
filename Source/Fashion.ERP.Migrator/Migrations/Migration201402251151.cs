using FluentMigrator;

namespace Fashion.ERP.Migrator
{
    [Migration(201402251151)]
    public class Migration201402251151 : Migration
    {
        public override void Up()
        {
            Create.Table("fichatecnica")
                .WithColumn("id").AsInt64().PrimaryKey()
                .WithColumn("referencia").AsString(50)
                .WithColumn("descricao").AsString(100)
                .WithColumn("detalhamento").AsString(200).Nullable()
                .WithColumn("sequencia").AsInt32().Nullable()
                .WithColumn("programacaoproducao").AsDateTime().Nullable()
                .WithColumn("datacadastro").AsDateTime()
                .WithColumn("modelagem").AsString(100).Nullable()
                .WithColumn("quantidadeproducao").AsInt32()
                .WithColumn("modelo_id").AsInt64().ForeignKey("FK_fichatecnica_modelo", "modelo", "id")
                .WithColumn("marca_id").AsInt64().ForeignKey("FK_fichatecnica_marca", "marca", "id")
                .WithColumn("colecao_id").AsInt64().ForeignKey("FK_fichatecnica_colecao", "colecao", "id")
                .WithColumn("barra_id").AsInt64().Nullable().ForeignKey("FK_fichatecnica_barra", "barra", "id")
                .WithColumn("segmento_id").AsInt64().Nullable().ForeignKey("FK_fichatecnica_segmento", "segmento", "id")
                .WithColumn("produtobase_id").AsInt64().Nullable().ForeignKey("FK_fichatecnica_produtobase", "produtobase", "id")
                .WithColumn("comprimento_id").AsInt64().Nullable().ForeignKey("FK_fichatecnica_comprimento", "comprimento", "id")
                .WithColumn("natureza_id").AsInt64().ForeignKey("FK_fichatecnica_natureza", "natureza", "id")
                .WithColumn("classificacaodificuldade_id").AsInt64().Nullable().ForeignKey("FK_fichatecnica_classificacaodificuldade", "classificacaodificuldade", "id")
                .WithColumn("grade_id").AsInt64().ForeignKey("FK_fichatecnica_grade", "grade", "id");

            Alter.Table("modelo")
                .AddColumn("anoaprovacao").AsInt32().Nullable()
                .AddColumn("numeroaprovacao").AsInt32().Nullable();

            Execute.EmbeddedScript("Fashion.ERP.Migrator.Scripts._201402251151.permissao.sql");
            Execute.EmbeddedScript("Fashion.ERP.Migrator.Scripts._201402251151.ConsumoMaterialColecaoView.sql");
        }

        public override void Down()
        {
            Delete.Column("anoaprovacao").FromTable("modelo");
            Delete.Column("numeroaprovacao").FromTable("modelo");

            Delete.Table("fichatecnica");
        }
    }
}