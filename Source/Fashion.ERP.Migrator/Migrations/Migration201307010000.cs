using FluentMigrator;

namespace Fashion.ERP.Migrator
{
    [Migration(201307010000)] // 01/07/2013 00:00
    public class Migration201307010000 : Migration
    {
        public override void Up()
        {
            #region Tabelas
            Create.Table("unidademedida")
                .WithColumn("id").AsInt64().PrimaryKey()
                .WithColumn("descricao").AsString(100)
                .WithColumn("sigla").AsString(5)
                .WithColumn("fatormultiplicativo").AsDouble();

            Create.Table("marca")
                .WithColumn("id").AsInt64().PrimaryKey()
                .WithColumn("nome").AsString(100);

            Create.Table("generofiscal")
                .WithColumn("id").AsInt64().PrimaryKey()
                .WithColumn("descricao").AsString(500)
                .WithColumn("codigo").AsString(2);

            Create.Table("familia")
                .WithColumn("id").AsInt64().PrimaryKey()
                .WithColumn("nome").AsString(100);

            Create.Table("tipoitem")
                .WithColumn("id").AsInt64().PrimaryKey()
                .WithColumn("descricao").AsString(100)
                .WithColumn("codigo").AsString(2);

            Create.Table("categoria")
                .WithColumn("id").AsInt64().PrimaryKey()
                .WithColumn("nome").AsString(100);

            Create.Table("subcategoria")
                .WithColumn("id").AsInt64().PrimaryKey()
                .WithColumn("nome").AsString(100)
                .WithColumn("categoria_id").AsInt64().ForeignKey("FK_subcategoria_categoria", "categoria", "id");

            Create.Table("catalogomaterial")
                .WithColumn("id").AsInt64().PrimaryKey()
                .WithColumn("referencia").AsString(20)
                .WithColumn("descricao").AsString(100)
                .WithColumn("detalhamento").AsString(4000)
                .WithColumn("codigobarra").AsString(50).Nullable()
                .WithColumn("ncm").AsString(8)
                .WithColumn("aliquota").AsDouble()
                .WithColumn("pesobruto").AsDouble()
                .WithColumn("pesoliquido").AsDouble()
                .WithColumn("origem").AsString(256)
                .WithColumn("localizacao").AsString(100).Nullable()
                .WithColumn("ativo").AsBoolean()
                .WithColumn("foto_id").AsInt64().ForeignKey("FK_catalogomaterial_foto", "arquivo", "id")
                .WithColumn("unidademedida_id").AsInt64().ForeignKey("FK_catalogomaterial_unidademedida", "unidademedida", "id")
                .WithColumn("marca_id").AsInt64().ForeignKey("FK_catalogomaterial_marca", "marca", "id")
                .WithColumn("subcategoria_id").AsInt64().ForeignKey("FK_catalogomaterial_subcategoria", "subcategoria", "id")
                .WithColumn("tipoitem_id").AsInt64().ForeignKey("FK_catalogomaterial_tipoitem", "tipoitem", "id")
                .WithColumn("familia_id").AsInt64().ForeignKey("FK_catalogomaterial_familia", "familia", "id")
                .WithColumn("generofiscal_id").AsInt64().ForeignKey("FK_catalogomaterial_generofiscal", "generofiscal", "id");

            Create.Table("referenciaexterna")
                .WithColumn("id").AsInt64().PrimaryKey()
                .WithColumn("referencia").AsString(20)
                .WithColumn("descricao").AsString(100)
                .WithColumn("codigobarra").AsString(8).Nullable()
                .WithColumn("preco").AsDouble()
                .WithColumn("catalogomaterial_id").AsInt64().ForeignKey("FK_referenciaexterna_catalogomaterial", "catalogomaterial", "id")
                .WithColumn("fornecedor_id").AsInt64().ForeignKey("FK_referenciaexterna_fornecedor", "pessoa", "id");
            #endregion

            #region Scripts

            Execute.EmbeddedScript("Fashion.ERP.Migrator.Scripts._201307010000.permissao.sql");
            Execute.EmbeddedScript("Fashion.ERP.Migrator.Scripts._201307010000.generofiscal.sql");
            Execute.EmbeddedScript("Fashion.ERP.Migrator.Scripts._201307010000.tipoitem.sql");

            #endregion
        }

        public override void Down()
        {
            Delete.Table("referenciaexterna");
            Delete.Table("catalogomaterial");
            Delete.Table("subcategoria");
            Delete.Table("categoria");
            Delete.Table("tipoitem");
            Delete.Table("familia");
            Delete.Table("generofiscal");
            Delete.Table("marca");
            Delete.Table("unidademedida");
        }
    }
}