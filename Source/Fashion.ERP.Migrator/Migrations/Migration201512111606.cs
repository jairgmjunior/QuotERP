using FluentMigrator;

namespace Fashion.ERP.Migrator
{
    [Migration(201512111606)]
    public class Migration201512111606 : Migration
    {
        public override void Up()
        {
            Execute.Sql(@"update programacaoproducaomaterial set reservamaterial_id = null where requisitado = 1;");
        }

        public override void Down()
        {
            
        }
    }
}
