using FluentMigrator;

namespace Fashion.ERP.Migrator
{
    [Migration(201501050947)]
    public class Migration201501050947 : Migration
    {
        public override void Up()
        {
            Create.Table("requisicaomaterial")
                .WithColumn("id").AsInt64().PrimaryKey()
                .WithColumn("idempresa").AsInt64()
                .WithColumn("idtenant").AsInt64()
                .WithColumn("numero").AsInt64()
                .WithColumn("data").AsDateTime()
                .WithColumn("observacao").AsString().Nullable()
                .WithColumn("origem").AsString().Nullable()
                .WithColumn("situacaorequisicaomaterial").AsString()
                .WithColumn("reservamaterial_id").AsInt64().Nullable().ForeignKey("FK_requisicaomaterial_reservamaterial", "reservamaterial", "id")
                .WithColumn("centrocusto_id").AsInt64().ForeignKey("FK_requisicaomaterial_centrocusto", "centrocusto", "id")
                .WithColumn("requerente_id").AsInt64().ForeignKey("FK_requisicaomaterial_requerente", "pessoa", "id")
                .WithColumn("unidaderequerente_id").AsInt64().ForeignKey("FK_requisicaomaterial_unidaderequerente", "pessoa", "id")
                .WithColumn("unidaderequisitada_id").AsInt64().ForeignKey("FK_requisicaomaterial_unidaderequisitada", "pessoa", "id")
                .WithColumn("tipoitem_id").AsInt64().ForeignKey("FK_requisicaomaterial_tipoitem", "tipoitem", "id");

            Create.Table("requisicaomaterialitemcancelado")
                .WithColumn("id").AsInt64().PrimaryKey()
                .WithColumn("idempresa").AsInt64()
                .WithColumn("idtenant").AsInt64()
                .WithColumn("data").AsDateTime()
                .WithColumn("observacao").AsString()
                .WithColumn("quantidadecancelada").AsDouble();

            Create.Table("requisicaomaterialitem")
                .WithColumn("id").AsInt64().PrimaryKey()
                .WithColumn("idempresa").AsInt64()
                .WithColumn("idtenant").AsInt64()
                .WithColumn("quantidadesolicitada").AsDouble()
                .WithColumn("quantidadeatendida").AsDouble()
                .WithColumn("situacaorequisicaomaterial").AsString()
                .WithColumn("requisicaomaterial_id").AsInt64().ForeignKey("FK_requisicaomaterialitem_requisicaomaterial", "requisicaomaterial", "id")
                .WithColumn("material_id").AsInt64().ForeignKey("FK_requisicaomaterialitem_material", "material", "id")
                .WithColumn("requisicaomaterialitemcancelado_id").AsInt64().Nullable().ForeignKey("FK_requisicaomaterialitem_requisicaomaterialitemcancelado", "requisicaomaterialitemcancelado", "id");

            Alter.Table("reservamaterial")
                .AlterColumn("dataprogramacao")
                .AsDateTime()
                .Nullable();

            Alter.Table("reservamaterial")
                .AlterColumn("colecao_id")
                .AsInt64()
                .Nullable();

            Alter.Table("saidamaterial")
                .AddColumn("requisicaomaterial_id")
                .AsInt64()
                .Nullable()
                .ForeignKey("FK_saidamaterial_requisicaomaterial", "requisicaomaterial", "id");

            Execute.Sql("update tipoitem set descricao = UPPER(descricao);");

            // Cria o menu da tela Requisição de  Material
            //Execute.EmbeddedScript("Fashion.ERP.Migrator.Scripts._201501050947.permissao.sql");
        }

        public override void Down()
        {
        }
    }
}