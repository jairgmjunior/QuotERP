using FluentMigrator;

namespace Fashion.ERP.Migrator
{
    [Migration(201501231346)]
    public class Migration201501231346 : Migration
    {
        public override void Up()
        {
            Create.Table("simboloconservacao")
                .WithColumn("id").AsInt64().PrimaryKey()
                .WithColumn("descricao").AsString(100)
                .WithColumn("categoriaconservacao").AsString(255)
                .WithColumn("foto_id").AsInt64();

            Execute.EmbeddedScript("Fashion.ERP.Migrator.Scripts._201501231346.permissao.sql");
        }

        public override void Down()
        {
        }
    }
}
