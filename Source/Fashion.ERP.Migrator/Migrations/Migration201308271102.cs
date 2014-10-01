using FluentMigrator;

namespace Fashion.ERP.Migrator
{
    [Migration(201308271102)] // 27/08/2013 11:02
    public class Migration201308271102 : Migration
    {
        public override void Up()
        {
            Create.Table("artigo")
                .WithColumn("id").AsInt64().PrimaryKey()
                .WithColumn("descricao").AsString(50)
                .WithColumn("ativo").AsBoolean();

            Create.Table("classificacao")
                .WithColumn("id").AsInt64().PrimaryKey()
                .WithColumn("descricao").AsString(50)
                .WithColumn("ativo").AsBoolean();

            Create.Table("colecao")
                .WithColumn("id").AsInt64().PrimaryKey()
                .WithColumn("descricao").AsString(50)
                .WithColumn("ativo").AsBoolean();

            Create.Table("comprimento")
                .WithColumn("id").AsInt64().PrimaryKey()
                .WithColumn("descricao").AsString(50)
                .WithColumn("ativo").AsBoolean();

            Create.Table("natureza")
                .WithColumn("id").AsInt64().PrimaryKey()
                .WithColumn("descricao").AsString(50)
                .WithColumn("ativo").AsBoolean();

            Create.Table("produtobase")
                .WithColumn("id").AsInt64().PrimaryKey()
                .WithColumn("descricao").AsString(50)
                .WithColumn("ativo").AsBoolean();

            Create.Table("barra")
                .WithColumn("id").AsInt64().PrimaryKey()
                .WithColumn("descricao").AsString(50)
                .WithColumn("ativo").AsBoolean();

            // Altera a coluna CodigoBarra da tabela ReferenciaExterna para aceitar até 50 caracteres.
            Alter.Column("codigobarra")
                .OnTable("referenciaexterna")
                .AsString(50).Nullable();

            Create.Table("departamentoproducao")
               .WithColumn("id").AsInt64().PrimaryKey()
               .WithColumn("nome").AsString(50)
               .WithColumn("criacao").AsBoolean()
               .WithColumn("producao").AsBoolean()
               .WithColumn("ativo").AsBoolean();

            Create.Table("relatorio")
                .WithColumn("id").AsInt64().PrimaryKey()
                .WithColumn("nome").AsString(100)
                .WithColumn("arquivo_id").AsInt64().ForeignKey("FK_relatorio_arquivo", "arquivo", "id");

            Create.Table("relatorioparametro")
                .WithColumn("id").AsInt64().PrimaryKey()
                .WithColumn("nome").AsString(100)
                .WithColumn("tiporelatorioparametro").AsString(255)
                .WithColumn("relatorio_id").AsInt64().ForeignKey("FK_relatorioparametro_relatorio", "relatorio", "id");

            Alter.Column("nome").OnTable("arquivo").AsString(260);

            Create.Table("setorproducao")
                .WithColumn("id").AsInt64().PrimaryKey()
                .WithColumn("nome").AsString(50)
                .WithColumn("ativo").AsBoolean()
                .WithColumn("departamentoproducao_id").AsInt64().ForeignKey("FK_setorproducao_departamentoproducao", "departamentoproducao", "id");

            Create.Table("operacaoproducao")
                .WithColumn("id").AsInt64().PrimaryKey()
                .WithColumn("descricao").AsString(50)
                .WithColumn("tempo").AsDouble()
                .WithColumn("custo").AsDouble()
                .WithColumn("ativo").AsBoolean()
                .WithColumn("setorproducao_id").AsInt64().ForeignKey("FK_operacaoproducao_setorproducao", "setorproducao", "id");

            Create.Table("tamanho")
                .WithColumn("id").AsInt64().PrimaryKey()
                .WithColumn("descricao").AsString(50)
                .WithColumn("sigla").AsString(10)
                .WithColumn("ativo").AsBoolean();

            Execute.EmbeddedScript("Fashion.ERP.Migrator.Scripts._201308271102.permissao.sql");
        }

        public override void Down()
        {
            Delete.Table("tamanho");
            Delete.Table("relatorioparametro");
            Delete.Table("relatorio");
            Delete.Table("operacaoproducao");
            Delete.Table("setorproducao");
            Delete.Table("departamentoproducao");

            Delete.Table("artigo");
            Delete.Table("classificacao");
            Delete.Table("colecao");
            Delete.Table("comprimento");
            Delete.Table("natureza");
            Delete.Table("produtobase");
            Delete.Table("tipobarra");
        }
    }
}