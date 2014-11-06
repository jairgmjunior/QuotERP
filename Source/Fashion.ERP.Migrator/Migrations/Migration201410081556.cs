using FluentMigrator;

namespace Fashion.ERP.Migrator
{
    [Migration(201410081556)]
    public class Migration201410081556 : Migration
    {
        public override void Up()
        {
            Alter.Table("detalhamentorecebimentocompraitem")
                .AddColumn("id")
                .AsInt64()
                .NotNullable()
                .PrimaryKey();

            Alter.Table("detalhamentorecebimentocompraitem")
                .AddColumn("recebimentocompraitem_id")
                .AsInt64()
                .Nullable()
                .ForeignKey("FK_detalhamentorecebimentocompraitem_recebimentocompraitem", "recebimentocompraitem", "id");

            Execute.EmbeddedScript("Fashion.ERP.Migrator.Scripts._201410081556.atualizerecebimentocompra.sql");
        }

        public override void Down()
        {
        }
    }
}