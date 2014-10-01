using FluentMigrator;

namespace Fashion.ERP.Migrator
{
    [Migration(201407141034)]
    public class Migration201407141034 : Migration
    {
        public override void Up()
        {
            Create.UniqueConstraint("cpf_cnpj_uniqueconstraint").OnTable("pessoa").Column("cpfcnpj");
        }

        public override void Down()
        {
            Delete.UniqueConstraint("cpf_cnpj_uniqueconstraint").FromTable("pessoa");
        }
    }
}