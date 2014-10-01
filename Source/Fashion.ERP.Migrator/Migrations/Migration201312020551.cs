using FluentMigrator;

namespace Fashion.ERP.Migrator
{
    [Migration(201312020551)]
    public class Migration201312020551 : Migration
    {
        public override void Up()
        {
            // Cria uma tabela para auditoria
            Create.Table("auditoria")
                .WithColumn("operacao").AsString(7)
                .WithColumn("tabela").AsString(100)
                .WithColumn("registro").AsInt64()
                .WithColumn("usuario").AsInt64()
                .WithColumn("login").AsString(50)
                .WithColumn("detalhe").AsCustom("nvarchar(max)").Nullable()
                .WithColumn("data").AsDateTime();

            // Depósito
            Create.Table("depositomaterial")
                .WithColumn("id").AsInt64().PrimaryKey()
                .WithColumn("nome").AsString(60)
                .WithColumn("dataabertura").AsDateTime()
                .WithColumn("ativo").AsBoolean()
                .WithColumn("unidade_id").AsInt64().ForeignKey("FK_depositomaterial_unidade", "pessoa", "id");

            Create.Table("depositomaterialfuncionario")
                .WithColumn("depositomaterial_id").AsInt64().ForeignKey("FK_depositomaterialfuncionario_deposito", "depositomaterial", "id")
                .WithColumn("funcionario_id").AsInt64().ForeignKey("FK_depositomaterialfuncionario_funcionario", "pessoa", "id");

            // EstoqueCatalogoMaterial
            Create.Table("estoquecatalogomaterial")
                .WithColumn("id").AsInt64().PrimaryKey()
                .WithColumn("quantidade").AsDouble()
                .WithColumn("reserva").AsDouble()
                .WithColumn("catalogomaterial_id").AsInt64().ForeignKey("FK_estoquecatalogomaterial_catalogomaterial", "catalogomaterial", "id")
                .WithColumn("depositomaterial_id").AsInt64().ForeignKey("FK_estoquecatalogomaterial_depositomaterial", "depositomaterial", "id");

            // EntradaCatalogoMaterial
            Create.Table("entradacatalogomaterial")
                .WithColumn("id").AsInt64().PrimaryKey()
                .WithColumn("dataentrada").AsDateTime()
                .WithColumn("depositomaterialdestino_id").AsInt64().ForeignKey("FK_entradacatalogomaterial_depositomaterialdestino", "depositomaterial", "id")
                .WithColumn("depositomaterialorigem_id").AsInt64().Nullable().ForeignKey("FK_entradacatalogomaterial_depositomaterialorigem", "depositomaterial", "id")
                .WithColumn("fornecedor_id").AsInt64().Nullable().ForeignKey("FK_entradacatalogomaterial_fornecedor","pessoa", "id");

            // EntradaItemCatalogoMaterial
            Create.Table("entradaitemcatalogomaterial")
                .WithColumn("id").AsInt64().PrimaryKey()
                .WithColumn("quantidadecompra").AsDouble()
                .WithColumn("fatormultiplicativo").AsDouble()
                .WithColumn("quantidade").AsDouble()
                .WithColumn("entradacatalogomaterial_id").AsInt64().ForeignKey("FK_entradaitemcatalogomaterial_entradacatalogomaterial", "entradacatalogomaterial", "id")
                .WithColumn("catalogomaterial_id").AsInt64().ForeignKey("FK_entradaitemcatalogomaterial_catalogomaterial", "catalogomaterial", "id")
                .WithColumn("unidademedida_id").AsInt64().ForeignKey("FK_entradaitemcatalogomaterial_unidademedida", "unidademedida", "id");

            // CentroCusto
            Create.Table("centrocusto")
                .WithColumn("id").AsInt64().PrimaryKey()
                .WithColumn("codigo").AsInt64()
                .WithColumn("nome").AsString(60)
                .WithColumn("ativo").AsBoolean();

            // SaidaCatalogoMaterial
            Create.Table("saidacatalogomaterial")
                .WithColumn("id").AsInt64().PrimaryKey()
                .WithColumn("datasaida").AsDateTime()
                .WithColumn("depositomaterialdestino_id").AsInt64().Nullable().ForeignKey("FK_saidacatalogomaterial_depositomaterialdestino", "depositomaterial", "id")
                .WithColumn("depositomaterialorigem_id").AsInt64().ForeignKey("FK_saidacatalogomaterial_depositomaterialorigem", "depositomaterial", "id")
                .WithColumn("centrocusto_id").AsInt64().Nullable().ForeignKey("FK_saidacatalogomaterial_centrocusto", "centrocusto", "id");

            // SaidaItemCatalogoMaterial
            Create.Table("saidaitemcatalogomaterial")
                .WithColumn("id").AsInt64().PrimaryKey()
                .WithColumn("quantidade").AsDouble()
                .WithColumn("saidacatalogomaterial_id").AsInt64().ForeignKey("FK_saidaitemcatalogomaterial_saidacatalogomaterial", "saidacatalogomaterial", "id")
                .WithColumn("catalogomaterial_id").AsInt64().ForeignKey("FK_saidaitemcatalogomaterial_catalogomaterial", "catalogomaterial", "id");

            // Alteração no cadastro de Modelo
            Alter.Table("modelo")
                .AddColumn("tecidocomplementar").AsString(100).Nullable()
                .AddColumn("forro").AsString(100).Nullable()
                .AddColumn("ziperbraguilha").AsString(100).Nullable()
                .AddColumn("ziperdetalhe").AsString(100).Nullable();

            Alter.Table("setorproducao")
                .AlterColumn("nome").AsString(100);

            Alter.Table("operacaoproducao")
                .AlterColumn("descricao").AsString(100);

            Execute.EmbeddedScript("Fashion.ERP.Migrator.Scripts._201312020551.permissao.sql");
            Execute.EmbeddedScript("Fashion.ERP.Migrator.Scripts._201312020551.ExtratoItemEstoqueView.sql");
            Execute.EmbeddedScript("Fashion.ERP.Migrator.Scripts._201312020551.SaldoEstoqueCatalogoMaterial.sql");
        }

        public override void Down()
        {
        }
    }
}