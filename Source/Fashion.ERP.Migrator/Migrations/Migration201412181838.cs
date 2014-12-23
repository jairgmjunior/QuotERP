using FluentMigrator;

namespace Fashion.ERP.Migrator
{
    [Migration(201412181838)]
    public class Migration201412181838 : Migration
    {
        public override void Up()
        {
            // Cria o menu da tela Cheque recebido -> Devolução
            Execute.EmbeddedScript("Fashion.ERP.Migrator.Scripts._201412181838.permissao.sql");
        }

        public override void Down()
        {
        }
    }
}