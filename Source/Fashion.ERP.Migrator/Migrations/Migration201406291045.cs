using FluentMigrator;

namespace Fashion.ERP.Migrator
{
    [Migration(201406291045)]
    public class Migration201406291045 : Migration
    {
        public override void Up()
        {
            // ProcedimentoModuloCompras
            Delete.ForeignKey("FK_procedimentomodulocomprasfuncionario_funcionario").OnTable("procedimentomodulocomprasfuncionario");
            Delete.ForeignKey("FK_procedimentomodulocomprasfuncionario_procedimentomodulocompras").OnTable("procedimentomodulocomprasfuncionario");
            Delete.Table("procedimentomodulocomprasfuncionario");
            Delete.Table("procedimentomodulocompras");

            Create.Table("procedimentomodulocompras")
                .WithColumn("id").AsInt64().Identity().PrimaryKey()
                .WithColumn("codigo").AsInt64()
                .WithColumn("descricao").AsString(60);

            Create.Table("procedimentomodulocomprasfuncionario")
                .WithColumn("procedimentomodulocompras_id").AsInt64().ForeignKey("FK_procedimentomodulocomprasfuncionario_procedimentomodulocompras", "procedimentomodulocompras", "id")
                .WithColumn("funcionario_id").AsInt64().ForeignKey("FK_procedimentomodulocomprasfuncionario_funcionario", "pessoa", "id");

            Insert.IntoTable("procedimentomodulocompras")
                .Row(new { codigo = 1, descricao = "Validar pedido de compra" })
                .Row(new { codigo = 2, descricao = "Atualizar o preço de custo de mercadoria" });

            Execute.EmbeddedScript("Fashion.ERP.Migrator.Scripts._201406291045.permissao.sql");
        }

        public override void Down()
        {
            Delete.Table("procedimentomodulocompras");
            Delete.Table("procedimentomodulocomprasfuncionario");
        }
    }
}