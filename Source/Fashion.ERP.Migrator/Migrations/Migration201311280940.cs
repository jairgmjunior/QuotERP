using FluentMigrator;

namespace Fashion.ERP.Migrator
{
    [Migration(201311280940)]
    public class Migration201311280940 : Migration
    {
        public override void Up()
        {
            // Tirar obrigatoriedade da foto no catálogo de material
            Alter.Table("modelo")
                .AlterColumn("segmento_id").AsInt64().Nullable(); //.ForeignKey("FK_modelo_segmento", "segmento", "id");

            // Atualizar a FuncaoFuncionario
            Execute.Sql("UPDATE funcionario SET funcaofuncionario = 'SupervisorLoja' WHERE funcaofuncionario = 'Supervisor';");

            Alter.Table("referencia")
                .AlterColumn("telefone").AsString(20).Nullable()
                .AlterColumn("celular").AsString(20).Nullable()
                .AlterColumn("observacao").AsString(4000).Nullable();
        }

        public override void Down()
        {
            
        }
    }
}