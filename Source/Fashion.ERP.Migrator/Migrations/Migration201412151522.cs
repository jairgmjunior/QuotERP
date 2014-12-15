using FluentMigrator;

namespace Fashion.ERP.Migrator
{
    [Migration(201412151522)]
    public class Migration201412151522 : Migration
    {
        public override void Up()
        {
            Delete.UniqueConstraint("cpf_cnpj_uniqueconstraint").FromTable("pessoa");

            Alter.Table("pessoa")
                .AddColumn("cpfcnpj2")
                .AsInt64()
                .Nullable();

            Execute.Sql("update pessoa set cpfcnpj2 = cpfcnpj");

            Alter.Table("pessoa")
                .AlterColumn("cpfcnpj")
                .AsString()
                .Nullable();
            
            Execute.Sql("update pessoa set cpfcnpj = FORMAT (cpfcnpj2, '00000000000000') where tipopessoa = 'Juridica'");
            Execute.Sql("update pessoa set cpfcnpj = FORMAT (cpfcnpj2, '00000000000')  where tipopessoa = 'Fisica'");
            
            Create.UniqueConstraint("cpf_cnpj_uniqueconstraint").OnTable("pessoa").Column("cpfcnpj");

            Delete.Column("cpfcnpj2").FromTable("pessoa");
        }

        public override void Down()
        {
        }
    }
}