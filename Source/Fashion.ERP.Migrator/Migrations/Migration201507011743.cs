using FluentMigrator;

namespace Fashion.ERP.Migrator
{
    [Migration(201507011743)]
    public class Migration201507011743 : Migration
    {
        public override void Up()
        {
            // Cria a coluna 'Despesa'
            Alter.Table("baixachequerecebido")
                .AddColumn("despesa").AsDouble().Nullable();
            Update.Table("baixachequerecebido").Set(new { despesa = 0}).AllRows();
            Alter.Table("baixachequerecebido")
                .AlterColumn("despesa").AsDouble();

            // Remove a coluna 'TaxaJuros'
            Delete.Column("taxajuros").FromTable("baixachequerecebido");
        }

        public override void Down()
        {
            // Remove a coluna 'Despesa'
            Delete.Column("despesa").FromTable("baixachequerecebido");

            // Recria a coluna 'TaxaJuros'
            Alter.Table("baixachequerecebido")
                .AddColumn("taxajuros").AsDouble().Nullable();
            Update.Table("baixachequerecebido").Set(new { taxajuros = 0 }).AllRows();
            Alter.Table("baixachequerecebido")
                .AlterColumn("taxajuros").AsDouble();
        }
    }
}