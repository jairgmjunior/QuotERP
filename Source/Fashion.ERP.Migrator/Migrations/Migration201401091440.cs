using FluentMigrator;

namespace Fashion.ERP.Migrator
{
    [Migration(201401091440)]
    public class Migration201401091440 : Migration
    {
        public override void Up()
        {
            // Programação Bordado
            Create.Table("programacaobordado")
                .WithColumn("id").AsInt64().PrimaryKey()
                .WithColumn("descricao").AsString(100)
                .WithColumn("nomearquivo").AsString(100).Nullable()
                .WithColumn("data").AsDate()
                .WithColumn("quantidadepontos").AsInt32()
                .WithColumn("quantidadecores").AsInt32()
                .WithColumn("aplicacao").AsString(250).Nullable()
                .WithColumn("observacao").AsString(250).Nullable()
                .WithColumn("arquivo_id").AsInt64().Nullable().ForeignKey("FK_programacaobordado_arquivo", "arquivo", "id")
                .WithColumn("modelo_id").AsInt64().ForeignKey("FK_programacaobordado_modelo", "modelo", "id")
                .WithColumn("programadorbordado_id").AsInt64().ForeignKey("FK_programacaobordado_programadorbordado", "pessoa", "id");

            Execute.EmbeddedScript("Fashion.ERP.Migrator.Scripts._201401091440.permissao.sql");
        }

        public override void Down()
        {
            Delete.ForeignKey("FK_programacaobordado_arquivo").OnTable("programacaobordado");
            Delete.ForeignKey("FK_programacaobordado_modelo").OnTable("programacaobordado");
            Delete.ForeignKey("FK_programacaobordado_programadorbordado").OnTable("programacaobordado");
            Delete.Table("programacaobordado");
        }
    }
}