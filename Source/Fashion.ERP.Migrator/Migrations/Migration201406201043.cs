using FluentMigrator;

namespace Fashion.ERP.Migrator
{
    [Migration(201406201043)]
    public class Migration201406201043 : Migration
    {
        public override void Up()
        {
            Create.Table("despesaReceita")
                .WithColumn("id").AsInt64().PrimaryKey()
                .WithColumn("descricao").AsString(60)
                .WithColumn("ativo").AsBoolean()
                .WithColumn("tipoDespesaReceita").AsString(255);

            Execute.EmbeddedScript("Fashion.ERP.Migrator.Scripts._201406201043.permissao.sql");
        }

        public override void Down()
        {
            Delete.Table("despesaReceita");
        }
    }
}