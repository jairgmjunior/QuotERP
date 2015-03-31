using FluentMigrator;

namespace Fashion.ERP.Migrator
{
    [Migration(201503241312)]
    public class Migration201503241312 : Migration
    {
        public override void Up()
        {
            Create.Table("materialcomposicaocustomatriz")
                .WithColumn("id").AsInt64().PrimaryKey()
                .WithColumn("idtenant").AsInt64()
                .WithColumn("idempresa").AsInt64()
                .WithColumn("custo").AsDouble()
                .WithColumn("material_id").AsInt64()
                .ForeignKey("FK_materialcomposicaocustomatriz_material", "material", "id");

            Create.Table("materialconsumomatriz")
                .WithColumn("id").AsInt64().PrimaryKey()
                .WithColumn("idtenant").AsInt64()
                .WithColumn("idempresa").AsInt64()
                .WithColumn("custo").AsDouble()
                .WithColumn("quantidade").AsDouble()
                .WithColumn("departamentoproducao_id").AsInt64()
                .ForeignKey("FK_materialconsumomatriz_departamentoproducao", "departamentoproducao", "id")
                .WithColumn("material_id").AsInt64()
                .ForeignKey("FK_materialconsumomatriz_material", "material", "id");

            Create.Table("materialconsumoitem")
                .WithColumn("id").AsInt64().PrimaryKey()
                .WithColumn("idtenant").AsInt64()
                .WithColumn("idempresa").AsInt64()
                .WithColumn("custo").AsDouble()
                .WithColumn("quantidade").AsDouble()
                .WithColumn("compoecusto").AsBoolean()
                .WithColumn("tamanho_id").AsInt64()
                .ForeignKey("FK_materialconsumoitem_tamanho", "tamanho", "id")
                .WithColumn("fichatecnicavariacaomatriz_id").AsInt64()
                .ForeignKey("FK_materialconsumoitem_fichatecnicavariacaomatriz", "fichatecnicavariacaomatriz", "id")
                .WithColumn("departamentoproducao_id").AsInt64()
                .ForeignKey("FK_materialconsumoitem_departamentoproducao", "departamentoproducao", "id")
                .WithColumn("material_id").AsInt64()
                .ForeignKey("FK_materialconsumoitem_material", "material", "id");

            Alter.Table("materialcomposicaocustomatriz")
                .AddColumn("fichatecnica_id")
                .AsInt64()
                .Nullable()
                .ForeignKey("FK_materialcomposicaocustomatriz_fichatecnica", "fichatecnica", "id");

            Alter.Table("materialconsumomatriz")
                .AddColumn("fichatecnicamatriz_id")
                .AsInt64()
                .Nullable()
                .ForeignKey("FK_materialconsumomatriz_fichatecnicamatriz", "fichatecnicamatriz", "id");

            Alter.Table("materialconsumoitem")
                .AddColumn("fichatecnicamatriz_id")
                .AsInt64()
                .Nullable()
                .ForeignKey("FK_materialconsumoitem_fichatecnicamatriz", "fichatecnicamatriz", "id");
        }

        public override void Down()
        {
        }
    }
}