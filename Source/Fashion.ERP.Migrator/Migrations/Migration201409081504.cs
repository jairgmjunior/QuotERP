using FluentMigrator;

namespace Fashion.ERP.Migrator
{
    [Migration(201409081504)]
    public class Migration201409081504 : Migration
    {
        public override void Up()
        {
            Execute.Sql(@"UPDATE permissao
	            SET controller = 'Material'
	            where controller = 'CatalogoMaterial'");

            Execute.Sql(@"UPDATE permissao
	            SET descricao = 'Material'
	            where descricao = 'Catálogo de material'");

            Execute.EmbeddedScript("Fashion.ERP.Migrator.Scripts._201409081504.renomeieCatalogoMaterial.sql");
        }

        public override void Down()
        {
        }
    }
}