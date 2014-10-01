using FluentMigrator;

namespace Fashion.ERP.Migrator
{
    [Migration(201301010000)] // 01/01/2013 00:00 (yyyyMMddhhmm)
    public class Migration201301010000 : Migration
    {
        public override void Up()
        {
            #region Tabelas

            Create.Table("uniquekeys")
                .WithColumn("tablename").AsString(100).PrimaryKey()
                .WithColumn("nexthi").AsInt64();

            Create.Table("arquivo")
                .WithColumn("id").AsInt64().PrimaryKey()
                .WithColumn("nome").AsString(100)
                .WithColumn("titulo").AsString(100)
                .WithColumn("data").AsDateTime()
                .WithColumn("extensao").AsString(10)
                .WithColumn("tamanho").AsDouble();

            Create.Table("profissao")
                .WithColumn("id").AsInt64().PrimaryKey()
                .WithColumn("nome").AsString(100);

            Create.Table("areainteresse")
                .WithColumn("id").AsInt64().PrimaryKey()
                .WithColumn("nome").AsString(100);

            Create.Table("funcionario")
                .WithColumn("id").AsInt64().PrimaryKey()
                .WithColumn("codigo").AsInt64()
                .WithColumn("percentualcomissao").AsDouble()
                .WithColumn("datacadastro").AsDateTime()
                .WithColumn("datadesligamento").AsDateTime().Nullable()
                .WithColumn("tipofuncionario").AsString(255)
                .WithColumn("ativo").AsBoolean();

            Create.Table("tipofornecedor")
                .WithColumn("id").AsInt64().PrimaryKey()
                .WithColumn("descricao").AsString(100);

            Create.Table("fornecedor")
                .WithColumn("id").AsInt64().PrimaryKey()
                .WithColumn("codigo").AsInt64()
                .WithColumn("datacadastro").AsDateTime()
                .WithColumn("ativo").AsBoolean()
                .WithColumn("tipofornecedor_id").AsInt64().ForeignKey("FK_fornecedor_tipofornecedor", "tipofornecedor", "id");

            Create.Table("unidade")
                .WithColumn("id").AsInt64().PrimaryKey()
                .WithColumn("codigo").AsInt64()
                .WithColumn("dataabertura").AsDateTime()
                .WithColumn("datacadastro").AsDateTime()
                .WithColumn("ativo").AsBoolean();

            Create.Table("cliente")
                .WithColumn("id").AsInt64().PrimaryKey()
                .WithColumn("codigo").AsInt64()
                .WithColumn("sexo").AsString(255).Nullable()
                .WithColumn("estadocivil").AsString(255).Nullable()
                .WithColumn("nomemae").AsString(100).Nullable()
                .WithColumn("datavalidade").AsDateTime().Nullable()
                .WithColumn("datacadastro").AsDateTime()
                .WithColumn("observacao").AsString(4000).Nullable()
                .WithColumn("profissao_id").AsInt64().Nullable().ForeignKey("FK_cliente_profissao", "profissao", "id")
                .WithColumn("areainteresse_id").AsInt64().Nullable().ForeignKey("FK_cliente_areainteresse", "areainteresse", "id");

            Create.Table("tipoprestadorservicoref")
                .WithColumn("id").AsInt64()
                .WithColumn("tipoprestadorservico").AsInt64();

            Create.Table("prestadorservico")
                .WithColumn("id").AsInt64().PrimaryKey()
                .WithColumn("codigo").AsInt64()
                .WithColumn("comissao").AsDouble()
                .WithColumn("datacadastro").AsDateTime()
                .WithColumn("ativo").AsBoolean()
                .WithColumn("unidade_id").AsInt64();

            Create.Table("pessoa")
                .WithColumn("id").AsInt64().PrimaryKey()
                .WithColumn("tipopessoa").AsString()
                .WithColumn("cpfcnpj").AsString(18).Nullable()
                .WithColumn("nome").AsString(100)
                .WithColumn("nomefantasia").AsString(100).Nullable()
                .WithColumn("documentoidentidade").AsString(20).Nullable()
                .WithColumn("orgaoexpedidor").AsString(20).Nullable()
                .WithColumn("inscricaoestadual").AsString(20).Nullable()
                .WithColumn("inscricaomunicipal").AsString(20).Nullable()
                .WithColumn("inscricaosuframa").AsString(9).Nullable()
                .WithColumn("datanascimento").AsDateTime().Nullable()
                .WithColumn("site").AsString(100).Nullable()
                .WithColumn("datacadastro").AsDateTime()
                .WithColumn("foto_id").AsInt64().Nullable().ForeignKey("FK_pessoa_foto", "arquivo", "id")
                .WithColumn("fornecedor_id").AsInt64().Nullable().ForeignKey("FK_pessoa_fornecedor", "fornecedor", "id")
                .WithColumn("cliente_id").AsInt64().Nullable().ForeignKey("FK_pessoa_cliente", "cliente", "id")
                .WithColumn("prestadorservico_id").AsInt64().Nullable().ForeignKey("FK_pessoa_prestadorservico", "prestadorservico", "id")
                .WithColumn("unidade_id").AsInt64().Nullable().ForeignKey("FK_pessoa_unidade", "unidade", "id")
                .WithColumn("funcionario_id").AsInt64().Nullable().ForeignKey("FK_pessoa_funcionario", "funcionario", "id");

            Create.ForeignKey("FK_prestadorservico_unidade").FromTable("prestadorservico").ForeignColumn("unidade_id").ToTable("pessoa").PrimaryColumn("id");

            Create.Table("usuario")
               .WithColumn("id").AsInt64().PrimaryKey()
               .WithColumn("login").AsString(50).NotNullable()
               .WithColumn("senha").AsString(96).NotNullable()
               .WithColumn("nome").AsString(50).Nullable()
               .WithColumn("funcionario_id").AsInt64().Nullable().ForeignKey("FK_usuario_funcionario", "pessoa", "id");

            Create.Table("perfildeacesso")
               .WithColumn("id").AsInt64().PrimaryKey()
               .WithColumn("nome").AsString(50).NotNullable();

            Create.Table("permissao")
               .WithColumn("id").AsInt64().PrimaryKey()
               .WithColumn("descricao").AsString(50).NotNullable()
               .WithColumn("action").AsString(50).Nullable()
               .WithColumn("area").AsString(50).Nullable()
               .WithColumn("controller").AsString(50).Nullable()
               .WithColumn("exibenomenu").AsBoolean().NotNullable()
               .WithColumn("requerpermissao").AsBoolean().NotNullable()
               .WithColumn("permissaopai_id").AsInt64().Nullable().ForeignKey("FK_permissao_permissao", "permissao", "id");

            Create.Table("permissaotoperfildeacesso")
               .WithColumn("perfildeacesso_id").AsInt64().NotNullable().ForeignKey("FK_permissaotoperfildeacesso_perfildeacesso", "perfildeacesso", "id")
               .WithColumn("permissao_id").AsInt64().NotNullable().ForeignKey("FK_permissaotoperfildeacesso_permissao", "permissao", "id");

            Create.Table("perfildeacessotousuario")
               .WithColumn("usuario_id").AsInt64().NotNullable().ForeignKey("FK_perfildeacessotousuario_usuario", "usuario", "id")
               .WithColumn("perfildeacesso_id").AsInt64().NotNullable().ForeignKey("FK_perfildeacessotousuario_perfildeacesso", "perfildeacesso", "id");

            Create.Table("permissaotousuario")
               .WithColumn("usuario_id").AsInt64().NotNullable().ForeignKey("FK_permissaotousuario_usuario", "usuario", "id")
               .WithColumn("permissao_id").AsInt64().NotNullable().ForeignKey("FK_permissaotousuario_permissao", "permissao", "id");

            Create.Table("contato")
                .WithColumn("id").AsInt64().PrimaryKey()
                .WithColumn("tipocontato").AsString()
                .WithColumn("nome").AsString(100)
                .WithColumn("telefone").AsString(14).Nullable()
                .WithColumn("operadora").AsString(20).Nullable()
                .WithColumn("email").AsString(100).Nullable()
                .WithColumn("pessoa_id").AsInt64().Nullable().ForeignKey("FK_contato_pessoa", "pessoa", "id");

            Create.Table("banco")
                .WithColumn("id").AsInt64().PrimaryKey()
                .WithColumn("codigo").AsInt32()
                .WithColumn("nome").AsString(100);

            Create.Table("informacaobancaria")
                .WithColumn("id").AsInt64().PrimaryKey()
                .WithColumn("agencia").AsString(6)
                .WithColumn("conta").AsString(20)
                .WithColumn("tipoconta").AsString()
                .WithColumn("dataabertura").AsDateTime().Nullable()
                .WithColumn("titular").AsString(100).Nullable()
                .WithColumn("telefone").AsString(14).Nullable()
                .WithColumn("pessoa_id").AsInt64().Nullable().ForeignKey("FK_informacaobancaria_pessoa", "pessoa", "id")
                .WithColumn("banco_id").AsInt64().ForeignKey("FK_informacaobancaria_banco", "banco", "id");

            Create.Table("pais")
                .WithColumn("id").AsInt64().PrimaryKey()
                .WithColumn("nome").AsString(100)
                .WithColumn("codigobacen").AsInt32();

            Create.Table("uf")
                .WithColumn("id").AsInt64().PrimaryKey()
                .WithColumn("nome").AsString(100)
                .WithColumn("sigla").AsString(2)
                .WithColumn("codigoibge").AsInt32()
                .WithColumn("pais_id").AsInt64().ForeignKey("FK_uf_pais", "pais", "id");

            Create.Table("cidade")
                .WithColumn("id").AsInt64().PrimaryKey()
                .WithColumn("nome").AsString()
                .WithColumn("codigoibge").AsInt32()
                .WithColumn("uf_id").AsInt64().ForeignKey("FK_cidade_uf", "uf", "id");

            Create.Table("endereco")
                .WithColumn("id").AsInt64().PrimaryKey()
                .WithColumn("tipoendereco").AsString()
                .WithColumn("logradouro").AsString(100)
                .WithColumn("numero").AsString(10).Nullable()
                .WithColumn("complemento").AsString(100).Nullable()
                .WithColumn("bairro").AsString(100)
                .WithColumn("cep").AsString(9)
                .WithColumn("pessoa_id").AsInt64().Nullable().ForeignKey("FK_endereco_pessoa", "pessoa", "id")
                .WithColumn("cidade_id").AsInt64().ForeignKey("FK_endereco_cidade", "cidade", "id");

            Create.Table("referencia")
                .WithColumn("id").AsInt64().PrimaryKey()
                .WithColumn("tiporeferencia").AsString(255)
                .WithColumn("nome").AsString(100)
                .WithColumn("telefone").AsString(20)
                .WithColumn("celular").AsString(20)
                .WithColumn("observacao").AsString(4000)
                .WithColumn("cliente_id").AsInt64().ForeignKey("FK_referencia_cliente", "cliente", "id");

            Create.Table("graudependencia")
                .WithColumn("id").AsInt64().PrimaryKey()
                .WithColumn("descricao").AsString(100);

            Create.Table("dependente")
                .WithColumn("id").AsInt64().PrimaryKey()
                .WithColumn("nome").AsString(20)
                .WithColumn("cpf").AsString(14)
                .WithColumn("rg").AsString(20)
                .WithColumn("orgaoexpedidor").AsString(100)
                .WithColumn("cliente_id").AsInt64().ForeignKey("FK_dependente_cliente", "cliente", "id")
                .WithColumn("graudependencia_id").AsInt64().ForeignKey("FK_dependente_graudependencia", "graudependencia", "id");

            Create.Table("emitente")
                .WithColumn("id").AsInt64().PrimaryKey()
                .WithColumn("agencia").AsString(6)
                .WithColumn("conta").AsString(8)
                .WithColumn("nome1").AsString(100)
                .WithColumn("cpfcnpj1").AsString(18)
                .WithColumn("documento1").AsString(20).Nullable()
                .WithColumn("orgaoexpedidor1").AsString(20).Nullable()
                .WithColumn("nome2").AsString(100).Nullable()
                .WithColumn("cpfcnpj2").AsString(18).Nullable()
                .WithColumn("documento2").AsString(20).Nullable()
                .WithColumn("orgaoexpedidor2").AsString(20).Nullable()
                .WithColumn("clientedesde").AsDateTime()
                .WithColumn("ativo").AsBoolean()
                .WithColumn("banco_id").AsInt64().ForeignKey("FK_emitente_banco", "banco", "id");

            Create.Table("chequerecebido")
                .WithColumn("id").AsInt64().PrimaryKey()
                .WithColumn("comp").AsInt32().Nullable()
                .WithColumn("agencia").AsString(6)
                .WithColumn("conta").AsString(8)
                .WithColumn("numerocheque").AsString(6)
                .WithColumn("cmc7").AsString(35).Nullable()
                .WithColumn("valor").AsDouble()
                .WithColumn("nominal").AsString(100).Nullable()
                .WithColumn("dataemissao").AsDateTime()
                .WithColumn("datavencimento").AsDateTime()
                .WithColumn("dataprorrogacao").AsDateTime().Nullable()
                .WithColumn("praca").AsString(100)
                .WithColumn("historico").AsString(4000).Nullable()
                .WithColumn("observacao").AsString(4000).Nullable()
                .WithColumn("saldo").AsDouble()
                .WithColumn("compensado").AsBoolean()
                .WithColumn("cliente_id").AsInt64().ForeignKey("FK_chequerecebido_cliente", "pessoa", "id")
                .WithColumn("banco_id").AsInt64().ForeignKey("FK_chequerecebido_banco", "banco", "id")
                .WithColumn("emitente_id").AsInt64().ForeignKey("FK_chequerecebido_emitente", "emitente", "id")
                .WithColumn("unidade_id").AsInt64().ForeignKey("FK_chequerecebido_unidade", "pessoa", "id");

            Create.Table("baixachequerecebido")
                .WithColumn("id").AsInt64().PrimaryKey()
                .WithColumn("data").AsDateTime()
                .WithColumn("taxajuros").AsDouble()
                .WithColumn("valorjuros").AsDouble()
                .WithColumn("valordesconto").AsDouble()
                .WithColumn("valor").AsDouble()
                .WithColumn("historico").AsString(4000).Nullable()
                .WithColumn("observacao").AsString(4000).Nullable()
                .WithColumn("chequerecebido_id").AsInt64().ForeignKey("FK_baixachequerecebido_chequerecebido", "chequerecebido", "id")
                .WithColumn("cobrador_id").AsInt64().Nullable().ForeignKey("FK_baixachequerecebido_cobrador", "pessoa", "id");

            Create.Table("chequerecebidoprestadorservico")
                .WithColumn("chequerecebido_id").AsInt64().PrimaryKey()
                    .ForeignKey("FK_chequerecebidoprestadorservico_chequerecebido", "chequerecebido", "id")
                .WithColumn("prestadorservico_id").AsInt64().PrimaryKey()
                    .ForeignKey("FK_chequerecebidoprestadorservico_prestadorservico", "pessoa", "id");

            Create.Table("chequerecebidofuncionario")
                .WithColumn("chequerecebido_id").AsInt64().PrimaryKey()
                    .ForeignKey("FK_chequerecebidofuncionario_chequerecebido", "chequerecebido", "id")
                .WithColumn("funcionario_id").AsInt64().PrimaryKey()
                    .ForeignKey("FK_chequerecebidofuncionario_funcionario", "pessoa", "id");

            Create.Table("compensacaocheque")
                .WithColumn("id").AsInt64().PrimaryKey()
                .WithColumn("codigo").AsInt32()
                .WithColumn("descricao").AsString(256);

            Create.Table("ocorrenciacompensacao")
                .WithColumn("id").AsInt64().PrimaryKey()
                .WithColumn("data").AsDateTime()
                .WithColumn("chequesituacao").AsString(256)
                .WithColumn("historico").AsString(4000).Nullable()
                .WithColumn("observacao").AsString(4000).Nullable()
                .WithColumn("chequerecebido_id").AsInt64().ForeignKey("FK_ocorrenciacompensacao_chequerecebido", "chequerecebido", "id")
                .WithColumn("compensacaocheque_id").AsInt64().Nullable().ForeignKey("FK_ocorrenciacompensacao_compensacaocheque", "compensacaocheque", "id");

            Create.Table("meiopagamento")
                .WithColumn("id").AsInt64().PrimaryKey()
                .WithColumn("descricao").AsString(100);

            Create.Table("recebimentochequerecebido")
                .WithColumn("id").AsInt64().PrimaryKey()
                .WithColumn("valor").AsDouble()
                .WithColumn("baixachequerecebido_id").AsInt64().ForeignKey("FK_recebimentochequerecebido_baixachequerecebido", "baixachequerecebido", "id")
                .WithColumn("meiopagamento_id").AsInt64().ForeignKey("FK_recebimentochequerecebido_meiopagamento", "meiopagamento", "id");

            Create.Table("contabancaria")
                .WithColumn("id").AsInt64().PrimaryKey()
                .WithColumn("agencia").AsString(6)
                .WithColumn("nomeagencia").AsString(50).Nullable()
                .WithColumn("conta").AsString(8)
                .WithColumn("tipocontabancaria").AsString(256)
                .WithColumn("gerente").AsString(50).Nullable()
                .WithColumn("abertura").AsDateTime().Nullable()
                .WithColumn("telefone").AsString(20).Nullable()
                .WithColumn("banco_id").AsInt64().ForeignKey("FK_contabancaria_banco", "banco", "id");

            Create.Table("extratobancario")
                .WithColumn("id").AsInt64().PrimaryKey()
                .WithColumn("tipolancamento").AsString(256)
                .WithColumn("emissao").AsDateTime()
                .WithColumn("compensacao").AsDateTime().Nullable()
                .WithColumn("descricao").AsString(100).Nullable()
                .WithColumn("valor").AsDouble()
                .WithColumn("compensado").AsBoolean()
                .WithColumn("cancelado").AsBoolean()
                .WithColumn("contabancaria_id").AsInt64().ForeignKey("FK_extratobancario_contabancaria", "contabancaria", "id");

            #endregion

            #region Scripts

            Execute.EmbeddedScript("Fashion.ERP.Migrator.Scripts._201301010000.cidade.sql");
            Execute.Sql("INSERT INTO uniquekeys (tablename, nexthi) VALUES ('cidade', 2782);");

            Execute.EmbeddedScript("Fashion.ERP.Migrator.Scripts._201301010000.banco.sql");
            Execute.EmbeddedScript("Fashion.ERP.Migrator.Scripts._201301010000.compensacaocheque.sql");
            Execute.EmbeddedScript("Fashion.ERP.Migrator.Scripts._201301010000.permissao.sql");

            #endregion

            #region Inserção do usuário administrador do sistema
            Insert.IntoTable("usuario").Row(new { id = 1, nome = "ADMINISTRADOR", login = "ADMIN", senha = "liGPElArb3rbEoweVxjMZv3cLrloK/aJuLia63zzTWCswhb5ybo94sPEE0+FWQSZYMV11AE6qCQKUsE9sTOYONZ1ajqj" }); // senha: admin
            Insert.IntoTable("uniquekeys").Row(new { tablename = "usuario", nexthi = 1 });

            Insert.IntoTable("permissaotousuario").Row(new { usuario_id = 1, permissao_id = 1 });
            Insert.IntoTable("permissaotousuario").Row(new { usuario_id = 1, permissao_id = 2 });
            Insert.IntoTable("permissaotousuario").Row(new { usuario_id = 1, permissao_id = 3 });
            Insert.IntoTable("permissaotousuario").Row(new { usuario_id = 1, permissao_id = 4 });
            Insert.IntoTable("permissaotousuario").Row(new { usuario_id = 1, permissao_id = 5 });
            Insert.IntoTable("uniquekeys").Row(new { tablename = "permissaotousuario", nexthi = 1 });

            #endregion Inserção do usuário administrador do sistema
        }

        public override void Down()
        {
            throw new System.NotImplementedException();
        }
    }
}
