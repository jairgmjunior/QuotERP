using FluentMigrator;

namespace Fashion.ERP.Migrator
{
    [Migration(201503201408)]
    public class Migration201503201408 : Migration
    {
        public override void Up()
        {
            Create.Table("modeloaprovado")
                .WithColumn("id").AsInt64().PrimaryKey()
                .WithColumn("tag").AsString()
                .WithColumn("ano").AsInt64()
                .WithColumn("data").AsDateTime()
                .WithColumn("observacao").AsString()
                .WithColumn("quantidade").AsInt64()
                .WithColumn("dataprogramacaoproducao").AsDateTime()
                .WithColumn("colecao_id").AsInt64()
                .ForeignKey("FK_modeloaprovado_colecao", "colecao", "id")
                .WithColumn("classificacaodificuldade_id").AsInt64()
                .ForeignKey("FK_modeloaprovado_classificacaodificuldade", "classificacaodificuldade", "id")
                .WithColumn("funcionario_id").AsInt64()
                .ForeignKey("FK_modeloaprovado_pessoa", "pessoa", "id");

            Alter.Table("modelo")
                .AddColumn("modeloaprovado_id")
                .AsInt64()
                .Nullable()
                .ForeignKey("FK_modelo_modeloaprovado", "modeloaprovado", "id");
            
            Delete.Column("tag").FromTable("modelo");
            Delete.Column("observacaoaprovacao").FromTable("modelo");
            Delete.Column("quantidademix").FromTable("modelo");
            Delete.Column("dataaprovacao").FromTable("modelo");
            Delete.Column("dataprevisaoenvio").FromTable("modelo");

            Delete.ForeignKey("FK_fichatecnica_modelo").OnTable("fichatecnica");
            Delete.Column("modelo_id").FromTable("fichatecnica");

            Alter.Table("fichatecnica")
                .AddColumn("modeloaprovado_id")
                .AsInt64()
                .Nullable()
                .ForeignKey("FK_fichatecnica_modeloaprovado", "modeloaprovado", "id");
        }

        public override void Down()
        {
        }
    }
}