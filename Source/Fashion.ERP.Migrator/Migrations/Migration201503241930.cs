using FluentMigrator;

namespace Fashion.ERP.Migrator
{
    [Migration(201503241930)]
    public class Migration201503241930 : Migration
    {
        public override void Up()
        {
            Create.Table("ordemproducaofluxobasico")
                .WithColumn("id").AsInt64().PrimaryKey();

            Create.Table("ordemproducaofluxobasicodepartamentoproducao")
                .WithColumn("ordemproducaofluxobasico_id").AsInt64().ForeignKey("FK_ordemproducaofluxobasicodepartamentoproducao_ordemproducaofluxobasico", "ordemproducaofluxobasico", "id")
                .WithColumn("departamentoproducao_id").AsInt64().ForeignKey("FK_ordemproducaofluxobasicodepartamentoproducao_departamentoproducao", "departamentoproducao", "id");

            Create.Table("ordemproducao")
                .WithColumn("id").AsInt64().PrimaryKey()
                .WithColumn("idtenant").AsInt64()
                .WithColumn("idempresa").AsInt64()
                .WithColumn("numero").AsInt64()
                .WithColumn("data").AsDateTime()
                .WithColumn("dataprogramacao").AsDateTime()
                .WithColumn("dataprevisao").AsDateTime()
                .WithColumn("observacao").AsString(4000).Nullable()
                .WithColumn("tipoordemproducao").AsString().Nullable()
                .WithColumn("situacaoordemproducao").AsString().Nullable()
                .WithColumn("fichatecnica_id").AsInt64().ForeignKey("FK_ordemproducao_fichatecnica", "fichatecnica", "id")
                .WithColumn("ordemproducaofluxobasico_id").AsInt64().ForeignKey("FK_ordemproducao_ordemproducaofluxobasico", "ordemproducaofluxobasico", "id").Nullable();

            Create.Table("ordemproducaomaterial")
                .WithColumn("id").AsInt64().PrimaryKey()
                .WithColumn("quantidade").AsDouble()
                .WithColumn("quantidadesubstituida").AsDouble()
                .WithColumn("requisitado").AsBoolean()
                .WithColumn("material_id").AsInt64().ForeignKey("FK_ordemproducaomaterial_material", "material", "id")
                .WithColumn("ordemproducaomaterialpai_id").AsInt64().ForeignKey("FK_ordemproducaomaterial_ordemproducaomaterialpai", "ordemproducaomaterial", "id").Nullable()
                .WithColumn("departamentoproducao_id").AsInt64().ForeignKey("FK_ordemproducaomaterial_departamentoproducao", "departamentoproducao", "id").Nullable()
                .WithColumn("ordemproducao_id").AsInt64().ForeignKey("FK_ordemproducaomaterial_ordemproducao", "ordemproducao", "id");

            Create.Table("ordemproducaoitem")
                .WithColumn("id").AsInt64().PrimaryKey()
                .WithColumn("quantidadesolicitada").AsInt32()
                .WithColumn("quantidadeadicional").AsInt32()
                .WithColumn("quantidadecancelada").AsInt32()
                .WithColumn("SituacaoOrdemProducao").AsString()
                .WithColumn("fichatecnicavariacaomatriz_id").AsInt64().ForeignKey("FK_ordemproducaoitem_fichatecnicavariacaomatriz", "fichatecnicavariacaomatriz", "id")
                .WithColumn("tamanho_id").AsInt64().ForeignKey("FK_ordemproducaoitem_tamanho", "tamanho", "id")
                .WithColumn("ordemproducao_id").AsInt64().ForeignKey("FK_ordemproducaoitem_ordemproducao", "ordemproducao", "id");

            Create.Table("defeitoproducao")
                .WithColumn("id").AsInt64().PrimaryKey()
                .WithColumn("descricao").AsString(60);

            Create.Table("ordemproducaoitemfechamento")
                .WithColumn("id").AsInt64().PrimaryKey()
                .WithColumn("data").AsDateTime()
                .WithColumn("quantidadeproduzida").AsDouble()
                .WithColumn("ordemproducaoitem_id").AsInt64().ForeignKey("FK_ordemproducaoitemfechamento_ordemproducaoitem", "ordemproducaoitem", "id");
            
            Create.Table("ordemproducaoitemfechamentosinistro")
                .WithColumn("id").AsInt64().PrimaryKey()
                .WithColumn("quantidade").AsDouble()
                .WithColumn("tiposinistrofechamentoordemproducao").AsString()
                .WithColumn("defeitoproducao_id").AsInt64().ForeignKey("FK_ordemproducaoitemfechamentosinistro_defeitoproducao", "defeitoproducao", "id")
                .WithColumn("ordemproducaoitemfechamento_id").AsInt64().ForeignKey("FK_ordemproducaoitemfechamentosinistro_ordemproducaoitemfechamento", "ordemproducaoitemfechamento", "id");
        }

        public override void Down()
        {
            Delete.Table("ordemproducaoitemfechamentosinistro");
            Delete.Table("ordemproducaoitemfechamento");
            Delete.Table("defeitoproducao");
            Delete.Table("ordemproducaoitem");
            Delete.Table("ordemproducaomaterial");
            Delete.Table("ordemproducao");
            Delete.Table("ordemproducaofluxobasicodepartamentoproducao");
            Delete.Table("ordemproducaofluxobasico");
        }
    }
}