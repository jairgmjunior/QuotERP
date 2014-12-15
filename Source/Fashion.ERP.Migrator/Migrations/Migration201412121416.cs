using FluentMigrator;

namespace Fashion.ERP.Migrator
{
    [Migration(201412121416)]
    public class Migration201412121416 : Migration
    {
        public override void Up()
        {
            Execute.Sql("update pessoa set cpfcnpj = REPLACE(REPLACE(REPLACE(cpfcnpj,'/',''),'.',''),'-','') ");
            
            Delete.UniqueConstraint("cpf_cnpj_uniqueconstraint").FromTable("pessoa");

            Alter.Table("pessoa")
                .AlterColumn("cpfcnpj")
                .AsInt64()
                .Nullable();

            Create.UniqueConstraint("cpf_cnpj_uniqueconstraint").OnTable("pessoa").Column("cpfcnpj");
        }

        public override void Down()
        {
        }
    }
}