using FluentMigrator;

namespace Fashion.ERP.Migrator
{
    [Migration(201412021716)]
    public class Migration201412021716 : Migration
    {
        public override void Up()
        {
            // CustoMaterial
            Create.Table("customaterial")
                .WithColumn("id").AsInt64().PrimaryKey()
                .WithColumn("idtenant").AsInt64()
                .WithColumn("idempresa").AsInt64()
                .WithColumn("data").AsDateTime()
                .WithColumn("custoaquisicao").AsDouble()
                .WithColumn("customedio").AsDouble()
                .WithColumn("custo").AsDouble()
                .WithColumn("ativo").AsBoolean()
                .WithColumn("fornecedor_id").AsInt64().ForeignKey("FK_customaterial_fornecedor", "pessoa", "id")
                .WithColumn("material_id").AsInt64().ForeignKey("FK_customaterial_material", "material", "id")
                .WithColumn("custoanterior_id").AsInt64().Nullable().ForeignKey("FK_customaterial_customaterial", "customaterial", "id");
        }

        public override void Down()
        {
            Delete.Table("customaterial");
        }
    }
}