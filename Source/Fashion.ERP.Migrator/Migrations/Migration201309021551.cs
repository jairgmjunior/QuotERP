using FluentMigrator;

namespace Fashion.ERP.Migrator
{
    [Migration(201309021551)]
    public class Migration201309021551 : Migration
    {
        public override void Up()
        {
            Create.Table("grade")
                .WithColumn("id").AsInt64().PrimaryKey()
                .WithColumn("descricao").AsString(50)
                .WithColumn("datacriacao").AsDateTime()
                .WithColumn("ativo").AsBoolean();

            Create.Table("gradetamanho")
                .WithColumn("ordem").AsInt32()
                .WithColumn("tamanho_id").AsInt64().ForeignKey("FK_gradetamanho_tamanho", "tamanho", "id")
                .WithColumn("grade_id").AsInt64().ForeignKey("FK_gradetamanho_grade", "grade", "id");

            Create.Table("segmento")
                .WithColumn("id").AsInt64().PrimaryKey()
                .WithColumn("descricao").AsString(50)
                .WithColumn("ativo").AsBoolean();

            Create.Table("linhatravete")
                .WithColumn("id").AsInt64().PrimaryKey()
                .WithColumn("cor").AsString(10)
                .WithColumn("nomelinha").AsString(50);

            Create.Table("linhabordado")
                .WithColumn("id").AsInt64().PrimaryKey()
                .WithColumn("cor").AsString(10)
                .WithColumn("nomelinha").AsString(50);

            Create.Table("linhapesponto")
                .WithColumn("id").AsInt64().PrimaryKey()
                .WithColumn("cor").AsString(10)
                .WithColumn("nomelinha").AsString(50);

            Create.Table("modelo")
                .WithColumn("id").AsInt64().PrimaryKey()
                .WithColumn("referencia").AsString(20)
                .WithColumn("descricao").AsString(100)
                .WithColumn("tecido").AsString(50).Nullable()
                .WithColumn("detalhamento").AsString(4000)
                .WithColumn("datacriacao").AsDateTime()
                .WithColumn("aprovado").AsBoolean().Nullable()
                .WithColumn("dataaprovacao").AsDateTime().Nullable()
                .WithColumn("observacao").AsString(4000).Nullable()
                .WithColumn("cos").AsDouble().Nullable()
                .WithColumn("passante").AsDouble().Nullable()
                .WithColumn("entrepernas").AsDouble().Nullable()
                .WithColumn("localizacao").AsString(100).Nullable()
                .WithColumn("tamanhopadrao").AsString(50)
                .WithColumn("linhacasa").AsString(50)
                .WithColumn("lavada").AsString(4000)
                .WithColumn("grade_id").AsInt64().ForeignKey("FK_modelo_grade", "grade", "id")
                .WithColumn("colecao_id").AsInt64().ForeignKey("FK_modelo_colecao", "colecao", "id")
                .WithColumn("classificacao_id").AsInt64().ForeignKey("FK_modelo_classificacao", "classificacao", "id")
                .WithColumn("segmento_id").AsInt64().ForeignKey("FK_modelo_segmento", "segmento", "id")
                .WithColumn("natureza_id").AsInt64().ForeignKey("FK_modelo_natureza", "natureza", "id")
                .WithColumn("barra_id").AsInt64().ForeignKey("FK_modelo_barra", "barra", "id")
                .WithColumn("comprimento_id").AsInt64().ForeignKey("FK_modelo_comprimento", "comprimento", "id")
                .WithColumn("marca_id").AsInt64().ForeignKey("FK_modelo_marca", "marca", "id")
                .WithColumn("produtobase_id").AsInt64().ForeignKey("FK_modelo_produtobase", "produtobase", "id")
                .WithColumn("artigo_id").AsInt64().ForeignKey("FK_modelo_artigo", "artigo", "id")
                .WithColumn("estilista_id").AsInt64().ForeignKey("FK_modelo_estilista", "pessoa", "id")
                .WithColumn("modelista_id").AsInt64().Nullable().ForeignKey("FK_modelo_modelista", "pessoa", "id");

            Create.Table("modelolinhatravete")
                .WithColumn("linhatravete_id").AsInt64().ForeignKey("FK_modelolinhatravete_linhatravete", "linhatravete", "id")
                .WithColumn("modelo_id").AsInt64().ForeignKey("FK_modelolinhatravete_modelo", "modelo", "id");

            Create.Table("modelolinhabordado")
                .WithColumn("linhabordado_id").AsInt64().ForeignKey("FK_modelolinhabordado_linhabordado", "linhabordado", "id")
                .WithColumn("modelo_id").AsInt64().ForeignKey("FK_modelolinhabordado_modelo", "modelo", "id");

            Create.Table("modelolinhapesponto")
                .WithColumn("linhapesponto_id").AsInt64().ForeignKey("FK_modelolinhapesponto_linhapesponto", "linhapesponto", "id")
                .WithColumn("modelo_id").AsInt64().ForeignKey("FK_modelolinhapesponto_modelo", "modelo", "id");

            Create.Table("modelofoto")
                .WithColumn("id").AsInt64().PrimaryKey()
                .WithColumn("impressao").AsBoolean()
                .WithColumn("padrao").AsBoolean()
                .WithColumn("foto_id").AsInt64().ForeignKey("FK_modelofoto_arquivo", "arquivo", "id")
                .WithColumn("modelo_id").AsInt64().ForeignKey("FK_modelofoto_modelo", "modelo", "id");

            Alter.Table("marca").AddColumn("ativo").AsBoolean().WithDefaultValue(true);

            Execute.EmbeddedScript("Fashion.ERP.Migrator.Scripts._201309021551.permissao.sql");
        }

        public override void Down()
        {
            Delete.Column("ativo").FromTable("marca");

            Delete.Table("modelofoto");
            Delete.Table("modelolinhapesponto");
            Delete.Table("modelolinhabordado");
            Delete.Table("modelolinhatravete");
            Delete.Table("modelo");
            Delete.Table("linhapesponto");
            Delete.Table("linhabordado");
            Delete.Table("linhatravete");
            Delete.Table("segmento");
            Delete.Table("grade");
            Delete.Table("gradetamanho");

        }
    }
}