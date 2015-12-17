using FluentMigrator;

namespace Fashion.ERP.Migrator
{
    [Migration(201511031044)]
    public class Migration201511031044 : Migration
    {
        public override void Up()
        {
            Create.Table("programacaoproducaoitem")
                .WithColumn("id").AsInt64().PrimaryKey()
                .WithColumn("idtenant").AsInt64()
                .WithColumn("idempresa").AsInt64()
                .WithColumn("quantidade").AsInt64()
                .WithColumn("programacaoproducao_id").AsInt64()
                .ForeignKey("FK_programacaoproducaoitem_programacaoproducao", "programacaoproducao", "id")
                .WithColumn("fichatecnica_id").AsInt64()
                .ForeignKey("FK_programacaoproducaoitem_fichatecnica", "fichatecnica", "id")
                .WithColumn("programacaoproducaomatrizcorte_id").AsInt64()
                .ForeignKey("FK_programacaoproducaoitem_programacaoproducaomatrizcorte", "programacaoproducaomatrizcorte", "id");

            Execute.EmbeddedScript("Fashion.ERP.Migrator.Scripts._201511031044.programacaoproducaoitem.sql");

            Alter.Table("entradamaterial")
                .AddColumn("observacao")
                .AsString()
                .Nullable();
            Alter.Table("programacaoproducaomaterial")
                .AddColumn("responsavel_id")
                .AsInt64()
                .Nullable()
                .ForeignKey("FK_programacaoproducaomaterial_pessoa", "pessoa", "id");

            Alter.Table("programacaoproducaomaterial")
                .AddColumn("requisitado")
                .AsBoolean()
                .NotNullable()
                .WithDefaultValue(false);

            Alter.Table("programacaoproducaomaterial")
                .AddColumn("reservado")
                .AsBoolean()
                .NotNullable()
                .WithDefaultValue(false);

            Execute.Sql(@"UPDATE programacaoproducaomaterial
                            SET reservado = 
                            ( CASE
                                    WHEN reservamaterial_id is null THEN 0
                                    ELSE 1
                                END
                            )");
        }

        public override void Down()
        {
        }
    }
}