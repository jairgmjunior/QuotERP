using FluentMigrator;

namespace Fashion.ERP.Migrator
{
    [Migration(201409121005)]
    public class Migration201409121005 : Migration
    {
        public override void Up()
        {
            Alter.Table("modelo")
                .AddColumn("dataprevisaoenvio").AsDateTime().Nullable();
            
            Delete.Column("dataentrega").FromTable("pedidocompra");

            Execute.EmbeddedScript("Fashion.ERP.Migrator.Scripts._201409121005.createrecebimentocompra.sql");
        }

        public override void Down()
        {
        }
    }
}