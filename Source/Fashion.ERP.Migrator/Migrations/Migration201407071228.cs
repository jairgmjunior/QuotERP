using FluentMigrator;

namespace Fashion.ERP.Migrator
{
    [Migration(201407071228)]
    public class Migration201407071228 : Migration
    {
        public override void Up()
        {
            Create.Table("empresa")
                .WithColumn("id").AsInt64().PrimaryKey()
                .WithColumn("codigo").AsInt64()
                .WithColumn("datacadastro").AsDateTime()
                .WithColumn("dataatualizacao").AsDateTime().Nullable()
                .WithColumn("ativo").AsBoolean();

            Alter.Table("pessoa")
                .AddColumn("empresa_id").AsInt64().Nullable().ForeignKey("FK_pessoa_empresa", "empresa", "id");

            Execute.EmbeddedScript("Fashion.ERP.Migrator.Scripts._201407071228.permissao.sql");
        }

        public override void Down()
        {
            Delete.Table("empresa");
            Delete.Column("empresa_id").FromTable("pessoa");
        }
    }
}