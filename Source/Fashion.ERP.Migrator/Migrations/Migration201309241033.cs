using FluentMigrator;

namespace Fashion.ERP.Migrator
{
    [Migration(201309241033)]
    public class Migration201309241033 : Migration
    {
        public override void Up()
        {
            Create.Table("marcamaterial")
                .WithColumn("id").AsInt64().PrimaryKey()
                .WithColumn("nome").AsString(100)
                .WithColumn("ativo").AsBoolean();

            // Copiar de marca para marcamaterial
            Execute.Sql("INSERT INTO marcamaterial (id, nome, ativo) SELECT id, nome, ativo FROM marca;");

            Alter.Table("catalogomaterial")
                .AddColumn("marcamaterial_id").AsInt64().Nullable();

            Execute.Sql("UPDATE catalogomaterial SET marcamaterial_id = marca_id;");

            Alter.Table("catalogomaterial")
                .AlterColumn("marcamaterial_id").AsInt64()
                .ForeignKey("FK_catalogomaterial_marcamaterial", "marcamaterial", "id");

            // Exclui a coluna 'marca_id'
            Delete.ForeignKey("FK_catalogomaterial_marca").OnTable("catalogomaterial");
            Delete.Column("marca_id").FromTable("catalogomaterial");

            // Adiciona a marcamaterial à uniquekeys
            Execute.Sql("INSERT INTO uniquekeys (tablename, nexthi) VALUES ('marcamaterial', (SELECT ISNULL(MAX(id), 0) + 1 FROM marcamaterial));");

            // LinhaTravete
            Alter.Table("modelolinhatravete")
                .AddColumn("nome").AsString(50).Nullable();
            Execute.Sql("UPDATE modelolinhatravete SET nome = (SELECT lb.nomelinha FROM linhatravete lb WHERE id = linhatravete_id);");
            Delete.ForeignKey("FK_modelolinhatravete_linhatravete").OnTable("modelolinhatravete");
            Delete.Column("linhatravete_id").FromTable("modelolinhatravete");

            // LinhaBordado
            Alter.Table("modelolinhabordado")
                .AddColumn("nome").AsString(50).Nullable();
            Execute.Sql("UPDATE modelolinhabordado SET nome = (SELECT lb.nomelinha FROM linhabordado lb WHERE id = linhabordado_id);");
            Delete.ForeignKey("FK_modelolinhabordado_linhabordado").OnTable("modelolinhabordado");
            Delete.Column("linhabordado_id").FromTable("modelolinhabordado");

            // LinhaPesponto
            Alter.Table("modelolinhapesponto")
                .AddColumn("nome").AsString(50).Nullable();
            Execute.Sql("UPDATE modelolinhapesponto SET nome = (SELECT lb.nomelinha FROM linhapesponto lb WHERE id = linhapesponto_id);");
            Delete.ForeignKey("FK_modelolinhapesponto_linhapesponto").OnTable("modelolinhapesponto");
            Delete.Column("linhapesponto_id").FromTable("modelolinhapesponto");

            Delete.Table("linhapesponto");
            Delete.Table("linhabordado");
            Delete.Table("linhatravete");

            // Alteração da tabela de categoria
            Alter.Table("categoria")
                .AddColumn("codigoncm").AsString(8).Nullable()
                .AddColumn("generocategoria").AsString(255).Nullable()
                .AddColumn("tipocategoria").AsString(255).Nullable()
                .AddColumn("ativo").AsBoolean().Nullable();
            Execute.Sql("update categoria set codigoncm = '00000000', generocategoria = 'Tecido', tipocategoria = 'UsoConsumo', ativo = 1;");
            Alter.Table("categoria")
                .AlterColumn("nome").AsString(60)
                .AlterColumn("codigoncm").AsString(8)
                .AlterColumn("generocategoria").AsString(255)
                .AlterColumn("tipocategoria").AsString(255)
                .AlterColumn("ativo").AsBoolean();

            // Alteração da tabela subcategoria
            Alter.Table("subcategoria")
                .AddColumn("ativo").AsBoolean().Nullable();
            Execute.Sql("update subcategoria set ativo = 1;");
            Alter.Table("subcategoria")
                .AlterColumn("ativo").AsBoolean();

            // Criar tabela Bordado
            Create.Table("bordado")
                .WithColumn("id").AsInt64().PrimaryKey()
                .WithColumn("descricao").AsString(100).Nullable()
                .WithColumn("pontos").AsString(100).Nullable()
                .WithColumn("aplicacao").AsString(100).Nullable()
                .WithColumn("observacao").AsString(100).Nullable();

            // Criar tabela Tecido
            Create.Table("tecido")
                .WithColumn("id").AsInt64().PrimaryKey()
                .WithColumn("composicao").AsString(200).Nullable()
                .WithColumn("armacao").AsString(100).Nullable()
                .WithColumn("gramatura").AsString(100).Nullable()
                .WithColumn("largura").AsString(100).Nullable()
                .WithColumn("rendimento").AsString(100).Nullable();

            // Alterar Catálogo de material
            Alter.Table("catalogomaterial")
                .AddColumn("bordado_id").AsInt64().Nullable().ForeignKey("FK_catalogomaterial_bordado", "bordado", "id")
                .AddColumn("tecido_id").AsInt64().Nullable().ForeignKey("FK_catalogomaterial_tecido", "tecido", "id");


            // Origem de situação Tributária
            Create.Table("origemsituacaotributaria")
                .WithColumn("id").AsInt64().PrimaryKey().Identity()
                .WithColumn("codigo").AsString(5)
                .WithColumn("descricao").AsString(200);

            Insert.IntoTable("origemsituacaotributaria")
                .Row(new { codigo = "0", descricao = "Nacional, exceto as indicadas nos códigos 3 a 5" })
                .Row(new { codigo = "1", descricao = "Estrangeira - Importação direta, exceto a indicada no código 6" })
                .Row(new { codigo = "2", descricao = "Estrangeira - Adquirida no mercado interno, exceto a indicada no código 7" })
                .Row(new { codigo = "3", descricao = "Nacional, mercadoria ou bem com Conteúdo de Importação superior a 40% (quarenta por cento)" })
                .Row(new { codigo = "4", descricao = "Nacional, cuja produção tenha sido feita em conformidade com os processos produtivos básicos de que tratam o Decreto-Lei nº 288/67 e as Leis n°s 8.248/91, 8.387/91,10.176/01 e 11.484/07" })
                .Row(new { codigo = "5", descricao = "Nacional, mercadoria ou bem com Conteúdo de Importação inferior ou igual a 40% (quarenta por cento)" })
                .Row(new { codigo = "6", descricao = "Estrangeira - Importação direta, sem similar nacional, constante em lista de Resolução CAMEX" })
                .Row(new { codigo = "7", descricao = "Estrangeira - Adquirida no mercado interno, sem similar nacional, constante em lista de Resolução CAMEX" });

            Alter.Table("catalogomaterial")
                .AddColumn("origemsituacaotributaria_id").AsInt64().Nullable();

            Execute.Sql("UPDATE catalogomaterial SET origemsituacaotributaria_id = 1;");

            Alter.Table("catalogomaterial")
                .AlterColumn("origemsituacaotributaria_id").AsInt64()
                .ForeignKey("FK_catalogomaterial_origemsituacaotributaria", "origemsituacaotributaria", "id");

            Delete.Column("origem").FromTable("catalogomaterial");

            Alter.Table("referenciaexterna")
                .AlterColumn("descricao").AsString(100).Nullable()
                .AlterColumn("codigobarra").AsString(128).Nullable()
                .AlterColumn("preco").AsDouble().Nullable();

            Alter.Table("catalogomaterial")
                .AlterColumn("detalhamento").AsString(200).Nullable()
                .AlterColumn("codigobarra").AsString(128).Nullable();

            Alter.Table("familia")
                .AlterColumn("nome").AsString(60);

            Alter.Table("marcamaterial")
                .AlterColumn("nome").AsString(60);

            Alter.Table("subcategoria")
                .AlterColumn("nome").AsString(60);

            Alter.Table("unidademedida")
                .AlterColumn("descricao").AsString(60)
                .AlterColumn("sigla").AsString(10);

            // Adicionar ativo na tabela familia
            Alter.Table("familia")
                .AddColumn("ativo").AsBoolean().Nullable();
            Execute.Sql("UPDATE familia SET ativo = 1;");
            Alter.Table("familia")
                .AlterColumn("ativo").AsBoolean();

            // Adicionar ativo na tabela unidademedida
            Alter.Table("unidademedida")
                .AddColumn("ativo").AsBoolean().Nullable();
            Execute.Sql("UPDATE unidademedida SET ativo = 1;");
            Alter.Table("unidademedida")
                .AlterColumn("ativo").AsBoolean();

            // Funcionário: alterar coluna tipofuncionario -> funcaofuncionario
            Alter.Table("funcionario")
                .AddColumn("funcaofuncionario").AsString(255).Nullable();

            Execute.Sql("UPDATE funcionario SET funcaofuncionario = tipofuncionario;");

            Delete.Column("tipofuncionario").FromTable("funcionario");

            Alter.Table("funcionario")
                .AlterColumn("funcaofuncionario").AsString(255);

            Execute.EmbeddedScript("Fashion.ERP.Migrator.Scripts._201309241033.permissao.sql");
        }

        public override void Down()
        {
            Delete.ForeignKey("FK_catalogomaterial_origemsituacaotributaria").OnTable("catalogomaterial");
            Delete.Table("origemsituacaotributaria");

            Delete.ForeignKey("FK_catalogomaterial_bordado").OnTable("catalogomaterial");
            Delete.ForeignKey("FK_catalogomaterial_tecido").OnTable("catalogomaterial");
            Delete.Column("bordado_id").Column("tecido_id").FromTable("catalogomaterial");
            Delete.Table("tecido");
            Delete.Table("bordado");

            Delete.Table("marcamaterial");
        }
    }
}