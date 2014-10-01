using FluentMigrator;

namespace Fashion.ERP.Migrator
{
    [Migration(201402191515)]
    public class Migration201402191515 : Migration
    {
        public override void Up()
        {
            Alter.Table("modelo")
                .AddColumn("dificuldade").AsString(100).Nullable()
                .AddColumn("quantidademix").AsInt32().Nullable()
                .AddColumn("dataremessaproducao").AsDateTime().Nullable();

            // Nova tabela ClassificacaoDificuldade
            Create.Table("classificacaodificuldade")
                .WithColumn("id").AsInt64().PrimaryKey()
                .WithColumn("descricao").AsString(50)
                .WithColumn("criacao").AsBoolean()
                .WithColumn("producao").AsBoolean()
                .WithColumn("ativo").AsBoolean();

            Execute.EmbeddedScript("Fashion.ERP.Migrator.Scripts._201402191515.permissao.sql");
        }

        public override void Down()
        {
            Delete.Table("classificacaodificuldade");

            Delete.Column("dificuldade").FromTable("modelo");
            Delete.Column("quantidademix").FromTable("modelo");
            Delete.Column("dataremessaproducao").FromTable("modelo");
        }
    }
}