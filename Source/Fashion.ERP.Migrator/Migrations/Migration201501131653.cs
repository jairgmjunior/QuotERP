using FluentMigrator;

namespace Fashion.ERP.Migrator
{
    [Migration(201501131653)]
    public class Migration201501131653 : Migration
    {
        public override void Up()
        {
            Alter.Table("reservamaterial")
                .AlterColumn("referenciaorigem")
                .AsString()
                .Nullable();
            
            // Cria o Baixar na tela Requisição de  Material
            Execute.EmbeddedScript("Fashion.ERP.Migrator.Scripts._201501131653.permissao.sql");
        }

        public override void Down()
        {
        }
    }
}