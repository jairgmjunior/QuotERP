using FluentMigrator;

namespace Fashion.ERP.Migrator
{
    [Migration(201605021625)]
    public class Migration201605021625 : Migration
    {
        public override void Up()
        {
            Execute.Sql("delete from uniquekeys where tablename = 'remessaproducao';");
            Execute.Sql("INSERT INTO uniquekeys (tablename, nexthi) VALUES ('remessaproducao', (SELECT ISNULL(MAX(id), 0) + 1 FROM remessaproducao));");
        }

        public override void Down()
        {
            
        }
    }
}
