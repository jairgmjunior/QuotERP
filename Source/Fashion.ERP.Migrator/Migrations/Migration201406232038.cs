using FluentMigrator;

namespace Fashion.ERP.Migrator
{
    [Migration(201406232038)]
    public class Migration201406232038 : Migration
    {
        public override void Up()
        {
            Create.Table("procedimentomodulocompras")
                .WithColumn("id").AsInt64().PrimaryKey()
                .WithColumn("codigo").AsInt64()
                .WithColumn("descricao").AsString(60);

            Create.Table("procedimentomodulocomprasfuncionario")
                .WithColumn("procedimentomodulocompras_id").AsInt64().ForeignKey("FK_procedimentomodulocomprasfuncionario_procedimentomodulocompras", "procedimentomodulocompras", "id")
                .WithColumn("funcionario_id").AsInt64().ForeignKey("FK_procedimentomodulocomprasfuncionario_funcionario", "pessoa", "id");

            Execute.EmbeddedScript("Fashion.ERP.Migrator.Scripts._201406232038.permissao.sql");
        }

        public override void Down()
        {
            Delete.Table("procedimentomodulocompras");
            Delete.Table("procedimentomodulocomprasfuncionario");
        }
    }
}