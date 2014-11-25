using FluentMigrator;

namespace Fashion.ERP.Migrator
{
    [Migration(201411211534)]
    public class Migration201411211534 : Migration
    {
        public override void Up()
        {
            Alter.Table("operacaoproducao")
                .AddColumn("pesoProdutividade")
                .AsDouble()
                .Nullable();

            Execute.EmbeddedScript("Fashion.ERP.Migrator.Scripts._201411211534.permissao.sql");
        }

        public override void Down()
        {
        }
    }
}
