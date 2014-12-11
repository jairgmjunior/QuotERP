using FluentMigrator;

namespace Fashion.ERP.Migrator
{
    [Migration(201412101433)]
    public class Migration201412101433 : Migration
    {
        public override void Up()
        {
            Create.Table("processooperacional")
                .WithColumn("id").AsInt64().PrimaryKey()
                .WithColumn("descricao").AsString(60)
                .WithColumn("ativo").AsBoolean();

            Create.Table("transportadora")
                .WithColumn("id").AsInt64().PrimaryKey()
                .WithColumn("codigo").AsInt64()
                .WithColumn("datacadastro").AsDateTime()
                .WithColumn("ativo").AsBoolean();

            Execute.EmbeddedScript("Fashion.ERP.Migrator.Scripts._201412101433.processooperacional.sql");
        }

        public override void Down()
        {
        }
    }
}
