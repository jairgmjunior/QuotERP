using FluentMigrator;

namespace Fashion.ERP.Migrator
{
    [Migration(201412231650)]
    public class Migration201412231650 : Migration
    {
        public override void Up()
        {
            // Cria o menu da tela Reserva Material
            Execute.EmbeddedScript("Fashion.ERP.Migrator.Scripts._201412231650.permissao.sql");
        }

        public override void Down()
        {
        }
    }
}
