using FluentMigrator;

namespace Fashion.ERP.Migrator
{
    [Migration(201408211313)]
    public class Migration201408211313 : Migration
    {
        public override void Up()
        {
            Execute.Sql("UPDATE sequenciaproducao SET ordem = ordem - 1");
        }

        public override void Down()
        {
            Execute.Sql("UPDATE sequenciaproducao SET ordem = ordem + 1");
        }
    }
}
