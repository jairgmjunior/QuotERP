using FluentMigrator;

namespace Fashion.ERP.Migrator
{
    [Migration(201412291428)]
    public class Migration201412291428 : Migration
    {
        public override void Up()
        {
            Delete.ForeignKey("FK_reservamaterialitem_reservaestoquematerial").OnTable("reservamaterialitem");
            Delete.Column("reservaestoquematerial_id").FromTable("reservamaterialitem");
            
            Delete.ForeignKey("FK_reservamaterialitem_reservamaterialitem").OnTable("reservamaterialitem");
            Delete.Column("reservamaterialitem_id").FromTable("reservamaterialitem");
            
            Delete.ForeignKey("FK_estoquematerial_reservaestoquematerial").OnTable("estoquematerial");
            Delete.Column("reservaestoquematerial_id").FromTable("estoquematerial");

            Delete.Table("reservaestoquematerial");

            Create.Table("reservaestoquematerial")
                .WithColumn("id").AsInt64().PrimaryKey()
                .WithColumn("idempresa").AsInt64()
                .WithColumn("idtenant").AsInt64()
                .WithColumn("quantidade").AsInt64()
                .WithColumn("material_id").AsInt64().ForeignKey("FK_reservaestoquematerial_material", "material", "id")
                .WithColumn("unidade_id").AsInt64().ForeignKey("FK_reservaestoquematerial_unidade", "pessoa", "id");
        }

        public override void Down()
        {
        }
    }
}
