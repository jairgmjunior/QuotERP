using System;
using FluentMigrator;

namespace Fashion.ERP.Migrator
{
    [Migration(201502131608)]
    public class Migration201502131608 : Migration
    {
        public override void Up()
        {
            Delete.Table("fichatecnica");
            
            Create.Table("fichatecnicafoto")
                .WithColumn("id").AsInt64().PrimaryKey()
                .WithColumn("idtenant").AsInt64()
                .WithColumn("idempresa").AsInt64()
                .WithColumn("descricao").AsString(100)
                .WithColumn("padrao").AsBoolean()
                .WithColumn("impressao").AsBoolean()
                .WithColumn("arquivo_id").AsInt64().ForeignKey("FK_fichatecnicafoto_arquivo", "arquivo", "id");

            Create.Table("fichatecnicamatriz")
                .WithColumn("id").AsInt64().PrimaryKey()
                .WithColumn("idtenant").AsInt64()
                .WithColumn("idempresa").AsInt64()
                .WithColumn("grade_id").AsInt64().ForeignKey("FK_fichatecnicamatriz_grade", "grade", "id");

            Create.Table("fichatecnicavariacaomatriz")
                .WithColumn("id").AsInt64().PrimaryKey()
                .WithColumn("idtenant").AsInt64()
                .WithColumn("idempresa").AsInt64()
                .WithColumn("variacao_id").AsInt64().ForeignKey("FK_fichatecnicavariacaomatriz_variacao", "variacao", "id")
                .WithColumn("fichatecnicamatriz_id").AsInt64().ForeignKey("FK_fichatecnicavariacaomatriz_fichatecnicamatriz", "fichatecnicamatriz", "id");
            
            Create.Table("fichatecnicavariacaomatrizcor")
                .WithColumn("fichatecnicavariacaomatriz_id").AsInt64().ForeignKey("FK_fichatecnicavariacaomatrizcor_fichatecnicavariacaomatriz", "fichatecnicavariacaomatriz", "id")
                .WithColumn("cor_id").AsInt64().ForeignKey("FK_fichatecnicavariacaomatrizcor_cor", "cor", "id");

            Create.Table("fichatecnica")
                .WithColumn("id").AsInt64().PrimaryKey()
                .WithColumn("idtenant").AsInt64()
                .WithColumn("idempresa").AsInt64()
                .WithColumn("tipo").AsString()
                .WithColumn("tag").AsString(100)
                .WithColumn("ano").AsDouble()
                .WithColumn("descricao").AsString(100)
                .WithColumn("detalhamento").AsString(200).Nullable()
                .WithColumn("observacao").AsString(4000).Nullable()
                .WithColumn("silk").AsString(200).Nullable()
                .WithColumn("bordado").AsString(200).Nullable()
                .WithColumn("pedraria").AsString(200).Nullable()
                .WithColumn("tempomaximoproducao").AsDouble().Nullable()
                .WithColumn("quantidadeproducaoaprovada").AsDouble().Nullable()
                .WithColumn("datacadastro").AsDateTime()
                .WithColumn("dataalteracao").AsDateTime()
                .WithColumn("artigo_id").AsInt64().ForeignKey("FK_fichatecnica_artigo", "artigo", "id")
                .WithColumn("colecao_id").AsInt64().ForeignKey("FK_fichatecnica_colecao", "colecao", "id")
                .WithColumn("marca_id").AsInt64().ForeignKey("FK_fichatecnica_marca", "marca", "id")
                .WithColumn("segmento_id").AsInt64().ForeignKey("FK_fichatecnica_segmento", "segmento", "id")
                .WithColumn("natureza_id").AsInt64().ForeignKey("FK_fichatecnica_natureza", "natureza", "id")
                .WithColumn("classificacao_id").AsInt64().ForeignKey("FK_fichatecnica_classificacao", "classificacao", "id")
                .WithColumn("classificacaodificuldade_id").AsInt64().ForeignKey("FK_fichatecnica_classificacaodificuldade", "classificacaodificuldade", "id")
                .WithColumn("fichatecnicamatriz_id").AsInt64().ForeignKey("FK_fichatecnica_fichatecnicamatriz", "fichatecnicamatriz", "id")
                .WithColumn("lavada").AsString(200).Nullable()
                .WithColumn("pesponto").AsString(200).Nullable()
                .WithColumn("cos").AsString(100).Nullable()
                .WithColumn("passante").AsString(100).Nullable()
                .WithColumn("entrepernas").AsString(100).Nullable()
                .WithColumn("boca").AsString(100).Nullable()
                .WithColumn("produtobase_id").AsInt64().Nullable().ForeignKey("FK_fichatecnica_produtobase", "produtobase", "id")
                .WithColumn("barra_id").AsInt64().Nullable().ForeignKey("FK_fichatecnica_barra", "barra", "id")
                .WithColumn("comprimento_id").AsInt64().Nullable().ForeignKey("FK_fichatecnica_comprimento", "comprimento", "id")
                .WithColumn("modelo_id").AsInt64().Nullable().ForeignKey("FK_fichatecnica_modelo", "modelo", "id");
            }

        public override void Down()
        {
        }
    }
}