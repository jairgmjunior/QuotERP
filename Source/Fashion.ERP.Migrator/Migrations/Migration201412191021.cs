using FluentMigrator;

namespace Fashion.ERP.Migrator
{
    [Migration(201412191021)]
    public class Migration201412191021 : Migration
    {
        public override void Up()
        {
            Delete.Table("reservamaterialitem");
            Delete.Table("reservamaterial");

            Create.Table("reservamaterial")
                .WithColumn("id").AsInt64().PrimaryKey()
                .WithColumn("idempresa").AsInt64()
                .WithColumn("idtenant").AsInt64()
                .WithColumn("numero").AsInt64()
                .WithColumn("data").AsDateTime()
                .WithColumn("dataprogramacao").AsDateTime()
                .WithColumn("observacao").AsString().Nullable()
                .WithColumn("referenciaorigem").AsString()
                .WithColumn("situacaoreservamaterial").AsString()
                .WithColumn("colecao_id").AsInt64().ForeignKey("FK_reservamaterial_colecao", "colecao", "id")
                .WithColumn("requerente_id").AsInt64().ForeignKey("FK_reservamaterial_requerente", "pessoa", "id")
                .WithColumn("unidade_id").AsInt64().ForeignKey("FK_reservamaterial_unidade", "pessoa", "id");

            Create.Table("reservamaterialitemcancelado")
                .WithColumn("id").AsInt64().PrimaryKey()
                .WithColumn("idempresa").AsInt64()
                .WithColumn("idtenant").AsInt64()
                .WithColumn("data").AsDateTime()
                .WithColumn("observacao").AsString()
                .WithColumn("quantidadecancelada").AsDouble();
            
            Create.Table("reservamaterialitem")
                .WithColumn("id").AsInt64().PrimaryKey()
                .WithColumn("idempresa").AsInt64()
                .WithColumn("idtenant").AsInt64()
                .WithColumn("quantidadereserva").AsDouble()
                .WithColumn("quantidadeatendida").AsDouble()
                .WithColumn("situacaoreservamaterial").AsString()
                .WithColumn("reservamaterial_id").AsInt64().Nullable().ForeignKey("FK_reservamaterialitem_reservamaterial", "reservamaterial", "id")
                .WithColumn("reservaestoquematerial_id").AsInt64().Nullable().ForeignKey("FK_reservamaterialitem_reservaestoquematerial", "reservaestoquematerial", "id")
                .WithColumn("material_id").AsInt64().ForeignKey("FK_reservamaterialitem_material", "material", "id")
                .WithColumn("reservamaterialitemcancelado_id").AsInt64().Nullable().ForeignKey("FK_reservamaterialitem_reservamaterialitemcancelado", "reservamaterialitemcancelado", "id")
                .WithColumn("reservamaterialitem_id").AsInt64().Nullable().ForeignKey("FK_reservamaterialitem_reservamaterialitem", "reservamaterialitem", "id");
        }

        public override void Down()
        {
        }
    }
}
