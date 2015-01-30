using System;
using FluentMigrator;

namespace Fashion.ERP.Migrator
{
    [Migration(201501292315)]
    public class Migration201501292315 : Migration
    {
        public override void Up()
        {
            Execute.EmbeddedScript("Fashion.ERP.Migrator.Scripts._201501292315.permissao.sql");
        }

        public override void Down()
        {
        }
    }
}
