using FluentMigrator;

namespace Fashion.ERP.Migrator
{
    [Migration(201411071557)]
    public class Migration201411071557 : Migration
    {
        public override void Up()
        {
            Alter.Table("recebimentocompra")
                .AddColumn("entradamaterial_id")
                .AsInt64()
                .Nullable()
                .ForeignKey("FK_recebimentocompra_entradamaterial", "entradamaterial", "id");
        }

        public override void Down()
        {
        }
    }
}