using System;
using FluentMigrator;

namespace Fashion.ERP.Migrator
{
    [Migration(201502100952)]
    public class Migration201502100952 : Migration
    {
        public override void Up()
        {
            Execute.Sql("EXEC sp_rename 'variacao', 'variacaoantiga';");
            Execute.Sql("sp_rename 'dbo.PK_variacao', 'PK_variacaoantiga';");

            Create.Table("variacao")
                .WithColumn("id").AsInt64().PrimaryKey()
                .WithColumn("nome").AsString()
                .WithColumn("ativo").AsBoolean();
            
            Create.Table("variacaomodelo")
                .WithColumn("id").AsInt64().PrimaryKey()
                .WithColumn("variacao_id").AsInt64()
                .ForeignKey("FK_variacaomodelo_variacao", "variacao", "id")
                .WithColumn("modelo_id").AsInt64().Nullable()
                .ForeignKey("FK_variacaomodelo_modelo", "modelo", "id");
            
            Create.Table("variacaomodelocor")
                .WithColumn("variacaomodelo_id").AsInt64()
                .ForeignKey("FK_variacaomodelocor_variacaomodelo", "variacaomodelo", "id")
                .WithColumn("cor_id").AsInt64()
                .ForeignKey("FK_variacaomodelocor_cor", "cor", "id");
            
            Alter.Table("materialcomposicaomodelo")
                .AddColumn("variacaomodelo_id")
                .AsInt64()
                .Nullable()
                .ForeignKey("FK_materialcomposicaomodelo_variacaomodelo", "variacaomodelo", "id");

            Execute.EmbeddedScript("Fashion.ERP.Migrator.Scripts._201502100952.variacaomodelo.sql");

            Delete.ForeignKey("FK_materialcomposicaomodelo_variacao").OnTable("materialcomposicaomodelo");
            Delete.Column("variacao_id").FromTable("materialcomposicaomodelo");
            
            Delete.Table("variacaocor");
            Delete.Table("variacaoantiga");
        }

        public override void Down()
        {
        }
    }
}