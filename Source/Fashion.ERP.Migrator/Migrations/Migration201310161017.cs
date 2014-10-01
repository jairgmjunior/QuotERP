using FluentMigrator;

namespace Fashion.ERP.Migrator
{
    [Migration(201310161017)]
    public class Migration201310161017 : Migration
    {
        public override void Up()
        {
            // Adicionar Cor
            Create.Table("cor")
                .WithColumn("id").AsInt64().PrimaryKey()
                .WithColumn("nome").AsString(100)
                .WithColumn("ativo").AsBoolean();

            // Adicionar Variação
            Create.Table("variacao")
                .WithColumn("id").AsInt64().PrimaryKey()
                .WithColumn("nome").AsString(100)
                .WithColumn("modelo_id").AsInt64().ForeignKey("FK_variacao_modelo", "modelo", "id");

            Create.Table("variacaocor")
                .WithColumn("variacao_id").AsInt64().ForeignKey("FK_variacaocor_variacao", "variacao", "id")
                .WithColumn("cor_id").AsInt64().ForeignKey("FK_variacaocor_cor", "cor", "id");

            // Adicionar Sequência produtiva
            Create.Table("sequenciaprodutiva")
                .WithColumn("id").AsInt64().PrimaryKey()
                .WithColumn("ordem").AsInt32()
                .WithColumn("dataentrada").AsDateTime().Nullable()
                .WithColumn("datasaida").AsDateTime().Nullable()
                .WithColumn("departamentoproducao_id").AsInt64().ForeignKey(
                    "FK_sequenciaprodutiva_departamentoproducao", "departamentoproducao", "id")
                .WithColumn("modelo_id").AsInt64().ForeignKey("FK_sequenciaprodutiva_modelo", "modelo", "id");

            Create.Table("sequenciaprodutivacatalogomaterial")
                .WithColumn("sequenciaprodutiva_id").AsInt64().ForeignKey("FK_sequenciaprodutivacatalogomaterial_sequenciaprodutiva", "sequenciaprodutiva", "id")
                .WithColumn("catalogomaterial_id").AsInt64().ForeignKey("FK_sequenciaprodutivacatalogomaterial_catalogomaterial", "catalogomaterial", "id");

            Alter.Table("modelo")
                .AlterColumn("referencia").AsString(50)
                .AlterColumn("tecido").AsString(60).Nullable()
                .AlterColumn("detalhamento").AsString(200)
                .AlterColumn("lavada").AsString(200).Nullable()
                .AddColumn("datamodelagem").AsDateTime().Nullable()
                .AddColumn("boca").AsDouble().Nullable()
                .AlterColumn("tamanhopadrao").AsString(10).Nullable()
                .AlterColumn("linhacasa").AsString(100).Nullable()
                .AddColumn("modelagem").AsString(100).Nullable()
                .AddColumn("etiquetamarca").AsString(100).Nullable()
                .AddColumn("etiquetacomposicao").AsString(100).Nullable()
                .AddColumn("tag").AsString(100).Nullable()
                .AddColumn("tamanho_id").AsInt64().Nullable().ForeignKey("FK_modelo_tamanho", "tamanho", "id");

            Alter.Table("artigo")
                .AlterColumn("descricao").AsString(60);

            Alter.Table("barra")
                .AlterColumn("descricao").AsString(60);

            Alter.Table("classificacao")
                .AlterColumn("descricao").AsString(60);

            Alter.Table("colecao")
                .AlterColumn("descricao").AsString(60);

            Alter.Table("comprimento")
                .AlterColumn("descricao").AsString(60);

            Alter.Table("tamanho")
                .AlterColumn("descricao").AsString(60);

            Alter.Table("natureza")
                .AlterColumn("descricao").AsString(60);

            Alter.Table("produtobase")
                .AlterColumn("descricao").AsString(60);

            Alter.Table("segmento")
                .AlterColumn("descricao").AsString(60);

            Alter.Table("grade")
                .AlterColumn("descricao").AsString(60);

            // Alterar menu -> permissao.sql
            Execute.EmbeddedScript("Fashion.ERP.Migrator.Scripts._201310161017.permissao.sql");
            Execute.EmbeddedScript("Fashion.ERP.Migrator.Scripts._201310161017.permissao2.sql");

            // Alterar tabela SequenciaProducao
            Execute.Sql("sp_rename 'sequenciaprodutiva', 'sequenciaproducao'");
            
            Alter.Table("sequenciaproducao")
                .AddColumn("setorproducao_id").AsInt64().Nullable().ForeignKey("FK_sequenciaproducao_setorproducao", "setorproducao", "id");

            Delete.Table("sequenciaprodutivacatalogomaterial");

            // Materiais de composição
            Create.Table("materialcomposicaomodelo")
                .WithColumn("id").AsInt64().PrimaryKey()
                .WithColumn("quantidade").AsDouble()
                .WithColumn("unidademedida_id").AsInt64().ForeignKey("FK_materialcomposicaomodelo_unidademedida", "unidademedida", "id")
                .WithColumn("catalogomaterial_id").AsInt64().ForeignKey("FK_materialcomposicaomodelo_catalogomaterial", "catalogomaterial", "id")
                .WithColumn("tamanho_id").AsInt64().Nullable().ForeignKey("FK_materialcomposicaomodelo_tamanho", "tamanho", "id")
                .WithColumn("cor_id").AsInt64().Nullable().ForeignKey("FK_materialcomposicaomodelo_cor", "cor", "id")
                .WithColumn("variacao_id").AsInt64().Nullable().ForeignKey("FK_materialcomposicaomodelo_variacao", "variacao", "id")
                .WithColumn("sequenciaproducao_id").AsInt64().ForeignKey("FK_materialcomposicaomodelo_sequenciaproducao", "sequenciaproducao", "id");

            Alter.Table("modelo")
                .AddColumn("complemento").AsString(50).Nullable();

            Alter.Table("modelofoto")
                .AlterColumn("modelo_id").AsInt64().Nullable();
        }

        public override void Down()
        {

        }
    }
}