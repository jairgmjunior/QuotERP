using FluentMigrator;

namespace Fashion.ERP.Migrator
{
    [Migration(201402031628)]
    public class Migration201402031628 : Migration
    {
        public override void Up()
        {
            // Prazo
            Create.Table("prazo")
                .WithColumn("id").AsInt64().PrimaryKey()
                .WithColumn("descricao").AsString(100)
                .WithColumn("avista").AsBoolean()
                .WithColumn("quantidadeparcelas").AsInt32()
                .WithColumn("prazoprimeiraparcela").AsInt32()
                .WithColumn("intervalo").AsInt32().Nullable()
                .WithColumn("ativo").AsBoolean()
                .WithColumn("padrao").AsBoolean();

            // Remover obrigatoriedade do campo Barra no cadastro de Modelo
            Delete.ForeignKey("FK_modelo_barra").OnTable("modelo");
            Delete.ForeignKey("FK_modelo_produtobase").OnTable("modelo");
            Delete.ForeignKey("FK_modelo_comprimento").OnTable("modelo");
            Alter.Table("modelo")
                .AlterColumn("barra_id").AsInt64().Nullable().ForeignKey("FK_modelo_barra", "barra", "id")
                .AlterColumn("produtobase_id").AsInt64().Nullable().ForeignKey("FK_modelo_produtobase", "produtobase", "id")
                .AlterColumn("comprimento_id").AsInt64().Nullable().ForeignKey("FK_modelo_comprimento", "comprimento", "id");

            Execute.EmbeddedScript("Fashion.ERP.Migrator.Scripts._201402031628.permissao.sql");
        }

        public override void Down()
        {
            Delete.Table("prazo");

            Alter.Table("modelo")
                .AlterColumn("barra_id").AsInt64().ForeignKey("FK_modelo_barra", "barra", "id")
                .AlterColumn("produtobase_id").AsInt64().ForeignKey("FK_modelo_produtobase", "produtobase", "id")
                .AlterColumn("comprimento_id").AsInt64().ForeignKey("FK_modelo_comprimento", "comprimento", "id");

            // TODO: Remover o prazo do menu
        }
    }
}