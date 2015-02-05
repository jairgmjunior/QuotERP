using System;
using FluentMigrator;

namespace Fashion.ERP.Migrator
{
    [Migration(201502041339)]
    public class Migration201502041339 : Migration
    {
        public override void Up()
        {
            Alter.Table("pedidocompra")
                .AddColumn("dataalteracao")
                .AsDateTime()
                .WithDefaultValue(DateTime.Now)
                .NotNullable();
            
            Alter.Table("entradamaterial")
                .AddColumn("dataalteracao")
                .AsDateTime()
                .WithDefaultValue(DateTime.Now)
                .NotNullable();
            
            Alter.Table("saidamaterial")
                .AddColumn("dataalteracao")
                .AsDateTime()
                .WithDefaultValue(DateTime.Now)
                .NotNullable();
            
            Alter.Table("reservamaterial")
                .AddColumn("dataalteracao")
                .AsDateTime()
                .WithDefaultValue(DateTime.Now)
                .NotNullable();

            Alter.Table("material")
                .AddColumn("dataalteracao")
                .AsDateTime()
                .WithDefaultValue(DateTime.Now)
                .NotNullable();

            Alter.Table("pessoa")
                .AddColumn("dataalteracao")
                .AsDateTime()
                .WithDefaultValue(DateTime.Now)
                .NotNullable();
        }

        public override void Down()
        {
        }
    }
}
