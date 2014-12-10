using FluentMigrator;

namespace Fashion.ERP.Migrator
{
    [Migration(201412051446)]
    public class Migration201412051446 : Migration
    {
        public override void Up()
        {
             Alter.Table("recebimentocompraitem")
                .AddColumn("customaterial_id")
                .AsInt64()
                .Nullable()
                .ForeignKey("FK_recebimentocompraitem_customaterial", "customaterial", "id");
        }

        public override void Down()
        {
        }
    }
}