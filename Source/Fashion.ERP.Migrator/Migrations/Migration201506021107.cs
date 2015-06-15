using FluentMigrator;

namespace Fashion.ERP.Migrator
{
    [Migration(201506021107)]
    public class Migration201506021107 : Migration
    {
        public override void Up()
        {
            Delete.Table("fichatecnicamaterialcomposicaocusto");
            Delete.Table("fichatecnicamaterialconsumo");
            Delete.Table("fichatecnicamaterialconsumovariacao");
            
            Create.Table("fichatecnicamaterialcomposicaocusto")
                .WithColumn("id").AsInt64().PrimaryKey()
                .WithColumn("idtenant").AsInt64()
                .WithColumn("idempresa").AsInt64()
                .WithColumn("custo").AsDouble()
                .WithColumn("material_id").AsInt64()
                .ForeignKey("FK_fichatecnicamaterialcomposicaocusto_material", "material", "id");

            Create.Table("fichatecnicamaterialconsumo")
                .WithColumn("id").AsInt64().PrimaryKey()
                .WithColumn("idtenant").AsInt64()
                .WithColumn("idempresa").AsInt64()
                .WithColumn("custo").AsDouble()
                .WithColumn("quantidade").AsDouble()
                .WithColumn("departamentoproducao_id").AsInt64()
                .ForeignKey("FK_fichatecnicamaterialconsumo_departamentoproducao", "departamentoproducao", "id")
                .WithColumn("material_id").AsInt64()
                .ForeignKey("FK_fichatecnicamaterialconsumo_material", "material", "id");

            Create.Table("fichatecnicamaterialconsumovariacao")
                .WithColumn("id").AsInt64().PrimaryKey()
                .WithColumn("idtenant").AsInt64()
                .WithColumn("idempresa").AsInt64()
                .WithColumn("custo").AsDouble()
                .WithColumn("quantidade").AsDouble()
                .WithColumn("compoecusto").AsBoolean()
                .WithColumn("tamanho_id").AsInt64()
                .ForeignKey("FK_fichatecnicamaterialconsumovariacao_tamanho", "tamanho", "id")
                .WithColumn("fichatecnicavariacaomatriz_id").AsInt64()
                .ForeignKey("FK_fichatecnicamaterialconsumovariacao_fichatecnicavariacaomatriz", "fichatecnicavariacaomatriz", "id")
                .WithColumn("departamentoproducao_id").AsInt64()
                .ForeignKey("FK_fichatecnicamaterialconsumovariacao_departamentoproducao", "departamentoproducao", "id")
                .WithColumn("material_id").AsInt64()
                .ForeignKey("FK_fichatecnicamaterialconsumovariacao_material", "material", "id");

            Alter.Table("fichatecnicamaterialcomposicaocusto")
                .AddColumn("fichatecnica_id")
                .AsInt64()
                .Nullable()
                .ForeignKey("FK_fichatecnicamaterialcomposicaocusto_fichatecnica", "fichatecnica", "id");

            Alter.Table("fichatecnicamaterialconsumo")
                .AddColumn("fichatecnica_id")
                .AsInt64()
                .Nullable()
                .ForeignKey("FK_fichatecnicamaterialconsumomatriz_fichatecnica", "fichatecnica", "id");

            Alter.Table("fichatecnicamaterialconsumovariacao")
                .AddColumn("fichatecnica_id")
                .AsInt64()
                .Nullable()
                .ForeignKey("FK_fichatecnicamaterialconsumovariacao_fichatecnica", "fichatecnica", "id");
            
            Delete.Column("tempomaximoproducao").FromTable("fichatecnica");

            Alter.Table("fichatecnica")
                .AddColumn("estilista_id")
                .AsInt64()
                .Nullable()
                .ForeignKey("FK_fichatecnica_estilista", "pessoa", "id");

            Alter.Table("fichatecnica")
                .AddColumn("referencia")
                .AsString()
                .NotNullable();
        }

        public override void Down()
        {
        }
    }
}