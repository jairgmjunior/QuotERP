using FluentMigrator;

namespace Fashion.ERP.Migrator
{
    [Migration(201605131717)]
    public class Migration201605131717 : Migration
    {
        public override void Up()
        {
            Create.Table("producaomatrizcorte")
                .WithColumn("id").AsInt64().PrimaryKey()
                .WithColumn("idtenant").AsInt64()
                .WithColumn("idempresa").AsInt64()
                .WithColumn("tipoenfestotecido").AsString();
            
            Create.Table("producaomatrizcorteitem")
                .WithColumn("id").AsInt64().PrimaryKey()
                .WithColumn("idtenant").AsInt64()
                .WithColumn("idempresa").AsInt64()
                .WithColumn("quantidadeprogramada").AsInt64()
                .WithColumn("quantidadeproducao").AsInt64()
                .WithColumn("quantidadeadicional").AsInt64()
                .WithColumn("quantidadecorte").AsInt64()
                .WithColumn("quantidadevezes").AsInt64()
                .WithColumn("tamanho_id").AsInt64()
                .ForeignKey("FK_producaomatrizcorteitem_tamanho", "tamanho", "id")
                .WithColumn("producaomatrizcorte_id").AsInt64()
                .ForeignKey("FK_producaomatrizcorteitem_producaomatrizcorte", "producaomatrizcorte", "id");

            Create.Table("producaoprogramacao")
                .WithColumn("id").AsInt64().PrimaryKey()
                .WithColumn("idtenant").AsInt64()
                .WithColumn("idempresa").AsInt64()
                .WithColumn("data").AsDate()
                .WithColumn("dataprogramada").AsDate()
                .WithColumn("observacao").AsString().Nullable()
                .WithColumn("quantidade").AsInt64()
                .WithColumn("unidade_id").AsInt64()
                .ForeignKey("FK_producaoprogramacao_unidade", "pessoa", "id")
                .WithColumn("funcionario_id").AsInt64()
                .ForeignKey("FK_producaoprogramacao_responsavel", "pessoa", "id");

            Create.Table("producao")
                .WithColumn("id").AsInt64().PrimaryKey()
                .WithColumn("idtenant").AsInt64()
                .WithColumn("idempresa").AsInt64()
                .WithColumn("data").AsDate()
                .WithColumn("dataalteracao").AsDate()
                .WithColumn("observacao").AsString().Nullable()
                .WithColumn("lote").AsInt64()
                .WithColumn("ano").AsInt64()
                .WithColumn("descricao").AsString().Nullable()
                .WithColumn("tipoproducao").AsString()
                .WithColumn("situacaoproducao").AsString()
                .WithColumn("producaoprogramacao_id").AsInt64().Nullable()
                .ForeignKey("FK_producao_producaoprogramacao", "producaoprogramacao", "id")
                .WithColumn("remessaproducao_id").AsInt64()
                .ForeignKey("FK_producao_remessaproducao", "remessaproducao", "id");

            Create.Table("producaoitem")
                .WithColumn("id").AsInt64().PrimaryKey()
                .WithColumn("idtenant").AsInt64()
                .WithColumn("idempresa").AsInt64()
                .WithColumn("quantidadeprogramada").AsInt64()
                .WithColumn("quantidadeproducao").AsInt64()
                .WithColumn("fichatecnica_id").AsInt64()
                .ForeignKey("FK_producaoitem_fichatecnica", "fichatecnica", "id")
                .WithColumn("producaomatrizcorte_id").AsInt64()
                .ForeignKey("FK_producaoitem_producaomatrizcorte", "producaomatrizcorte", "id")
                .WithColumn("producao_id").AsInt64()
                .ForeignKey("FK_producaoitem_producao", "producao", "id");
            
            Create.Table("producaoitemmaterial")
                .WithColumn("id").AsInt64().PrimaryKey()
                .WithColumn("idtenant").AsInt64()
                .WithColumn("idempresa").AsInt64()
                .WithColumn("quantidadeprogramada").AsInt64()
                .WithColumn("quantidadenecessidade").AsInt64()
                .WithColumn("quantidadeusada").AsInt64()
                .WithColumn("quantidadecancelada").AsInt64()
                .WithColumn("departamentoproducao_id").AsInt64()
                .ForeignKey("FK_producaoitemmaterial_departamentoproducao", "departamentoproducao", "id")
                .WithColumn("responsavel_id").AsInt64().Nullable()
                .ForeignKey("FK_producaoitemmaterial_pessoa", "pessoa", "id")
                .WithColumn("material_id").AsInt64()
                .ForeignKey("FK_producaoitemmaterial_material", "material", "id")
                .WithColumn("producaoitem_id").AsInt64()
                .ForeignKey("FK_producaoitemmaterial_producaoitem", "producaoitem", "id")
                .WithColumn("producaoitemmaterial_id").AsInt64().Nullable()
                .ForeignKey("FK_producaoitemmaterial_producaoitemmaterial", "producaoitemmaterial", "id");
        }

        public override void Down()
        {
            
        }
    }
}
