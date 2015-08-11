using FluentMigrator;

namespace Fashion.ERP.Migrator
{
    [Migration(201507311457)]
    public class Migration201507311457 : Migration
    {
        public override void Up()
        {
            Alter.Table("reservamaterial")
            .AddColumn("requisicaomaterial_id")
            .AsInt64()
            .Nullable()
            .ForeignKey("FK_reservamaterial_requisicaomaterial", "requisicaomaterial", "id");

            Execute.EmbeddedScript("Fashion.ERP.Migrator.Scripts._201507311457.permissao.sql");
            
            Delete.ForeignKey("FK_requisicaomaterial_reservamaterial").OnTable("requisicaomaterial");
            Delete.Column("reservamaterial_id").FromTable("requisicaomaterial");
        }

        public override void Down()
        {
        }
    }
}