using FluentMigrator;

namespace Fashion.ERP.Migrator
{
    [Migration(201412171041)]
    public class Migration201412171041 : Migration
    {
        public override void Up()
        {
            Create.Table("reservamaterial")
                .WithColumn("id").AsInt64().PrimaryKey()
                .WithColumn("idempresa").AsInt64()
                .WithColumn("idtenant").AsInt64()
                .WithColumn("numero").AsInt64()
                .WithColumn("data").AsDateTime()
                .WithColumn("previsaoprimeirautilizacao").AsDateTime()
                .WithColumn("observacao").AsString()
                .WithColumn("referencia").AsString()
                .WithColumn("finalizada").AsBoolean()
                .WithColumn("colecao_id").AsInt64().ForeignKey("FK_reservamaterial_colecao", "colecao", "id")
                .WithColumn("requerente_id").AsInt64().ForeignKey("FK_reservamaterial_requerente", "pessoa", "id")
                .WithColumn("unidade_id").AsInt64().ForeignKey("FK_reservamaterial_unidade", "pessoa", "id");
            
            Create.Table("reservaestoquematerial")
                .WithColumn("id").AsInt64().PrimaryKey()
                .WithColumn("idempresa").AsInt64()
                .WithColumn("idtenant").AsInt64()
                .WithColumn("quantidade").AsInt64();

            Create.Table("reservamaterialitem")
                .WithColumn("id").AsInt64().PrimaryKey()
                .WithColumn("idempresa").AsInt64()
                .WithColumn("idtenant").AsInt64()
                .WithColumn("quantidadereserva").AsInt64()
                .WithColumn("quantidadeatendida").AsInt64()
                .WithColumn("previsaoutilizacao").AsDateTime()
                .WithColumn("situacaoreservamaterialitem").AsString()
                .WithColumn("reservamaterial_id").AsInt64().Nullable().ForeignKey("FK_reservamaterialitem_reservamaterial", "reservamaterial", "id")
                .WithColumn("reservaestoquematerial_id").AsInt64().Nullable().ForeignKey("FK_reservamaterialitem_reservaestoquematerial", "reservaestoquematerial", "id")
                .WithColumn("material_id").AsInt64().ForeignKey("FK_reservamaterialitem_material", "material", "id")
                .WithColumn("reservamaterialitem_id").AsInt64().Nullable().ForeignKey("FK_reservamaterialitem_reservamaterialitem", "reservamaterialitem", "id");
            
            Alter.Table("estoquematerial")
                .AddColumn("reservaestoquematerial_id").AsInt64().Nullable()
                .ForeignKey("FK_estoquematerial_reservaestoquematerial", "reservaestoquematerial", "id");
            
            Execute.EmbeddedScript("Fashion.ERP.Migrator.Scripts._201412171041.sequenciaoperacional.sql");
        }

        public override void Down()
        {
        }
    }
}
