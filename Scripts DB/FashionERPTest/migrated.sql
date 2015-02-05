/* Using Database sqlserver2012 and Connection String server=.\SQLEXPRESS;Database=FashionERPTest;User Id=sa;Password=123456; */
/* VersionMigration migrating ================================================ */

/* Beginning Transaction */
/* CreateTable VersionInfo */
CREATE TABLE [dbo].[VersionInfo] ([Version] BIGINT NOT NULL)

/* Committing Transaction */
/* VersionMigration migrated */

/* VersionUniqueMigration migrating ========================================== */

/* Beginning Transaction */
/* CreateIndex VersionInfo (Version) */
CREATE UNIQUE CLUSTERED INDEX [UC_Version] ON [dbo].[VersionInfo] ([Version] ASC)

/* AlterTable VersionInfo */
/* No SQL statement executed. */

/* CreateColumn VersionInfo AppliedOn DateTime */
ALTER TABLE [dbo].[VersionInfo] ADD [AppliedOn] DATETIME

/* Committing Transaction */
/* VersionUniqueMigration migrated */

/* VersionDescriptionMigration migrating ===================================== */

/* Beginning Transaction */
/* AlterTable VersionInfo */
/* No SQL statement executed. */

/* CreateColumn VersionInfo Description String */
ALTER TABLE [dbo].[VersionInfo] ADD [Description] NVARCHAR(1024)

/* Committing Transaction */
/* VersionDescriptionMigration migrated */

/* 201301010000: Migration201301010000 migrating ============================= */

/* Beginning Transaction */
/* CreateTable uniquekeys */
CREATE TABLE [dbo].[uniquekeys] ([tablename] NVARCHAR(100) NOT NULL, [nexthi] BIGINT NOT NULL, CONSTRAINT [PK_uniquekeys] PRIMARY KEY ([tablename]))

/* CreateTable arquivo */
CREATE TABLE [dbo].[arquivo] ([id] BIGINT NOT NULL, [nome] NVARCHAR(100) NOT NULL, [titulo] NVARCHAR(100) NOT NULL, [data] DATETIME NOT NULL, [extensao] NVARCHAR(10) NOT NULL, [tamanho] DOUBLE PRECISION NOT NULL, CONSTRAINT [PK_arquivo] PRIMARY KEY ([id]))

/* CreateTable profissao */
CREATE TABLE [dbo].[profissao] ([id] BIGINT NOT NULL, [nome] NVARCHAR(100) NOT NULL, CONSTRAINT [PK_profissao] PRIMARY KEY ([id]))

/* CreateTable areainteresse */
CREATE TABLE [dbo].[areainteresse] ([id] BIGINT NOT NULL, [nome] NVARCHAR(100) NOT NULL, CONSTRAINT [PK_areainteresse] PRIMARY KEY ([id]))

/* CreateTable funcionario */
CREATE TABLE [dbo].[funcionario] ([id] BIGINT NOT NULL, [codigo] BIGINT NOT NULL, [percentualcomissao] DOUBLE PRECISION NOT NULL, [datacadastro] DATETIME NOT NULL, [datadesligamento] DATETIME, [tipofuncionario] NVARCHAR(255) NOT NULL, [ativo] BIT NOT NULL, CONSTRAINT [PK_funcionario] PRIMARY KEY ([id]))

/* CreateTable tipofornecedor */
CREATE TABLE [dbo].[tipofornecedor] ([id] BIGINT NOT NULL, [descricao] NVARCHAR(100) NOT NULL, CONSTRAINT [PK_tipofornecedor] PRIMARY KEY ([id]))

/* CreateTable fornecedor */
CREATE TABLE [dbo].[fornecedor] ([id] BIGINT NOT NULL, [codigo] BIGINT NOT NULL, [datacadastro] DATETIME NOT NULL, [ativo] BIT NOT NULL, [tipofornecedor_id] BIGINT NOT NULL, CONSTRAINT [PK_fornecedor] PRIMARY KEY ([id]))

/* CreateForeignKey FK_fornecedor_tipofornecedor fornecedor(tipofornecedor_id) tipofornecedor(id) */
ALTER TABLE [dbo].[fornecedor] ADD CONSTRAINT [FK_fornecedor_tipofornecedor] FOREIGN KEY ([tipofornecedor_id]) REFERENCES [dbo].[tipofornecedor] ([id])

/* CreateTable unidade */
CREATE TABLE [dbo].[unidade] ([id] BIGINT NOT NULL, [codigo] BIGINT NOT NULL, [dataabertura] DATETIME NOT NULL, [datacadastro] DATETIME NOT NULL, [ativo] BIT NOT NULL, CONSTRAINT [PK_unidade] PRIMARY KEY ([id]))

/* CreateTable cliente */
CREATE TABLE [dbo].[cliente] ([id] BIGINT NOT NULL, [codigo] BIGINT NOT NULL, [sexo] NVARCHAR(255), [estadocivil] NVARCHAR(255), [nomemae] NVARCHAR(100), [datavalidade] DATETIME, [datacadastro] DATETIME NOT NULL, [observacao] NVARCHAR(4000), [profissao_id] BIGINT, [areainteresse_id] BIGINT, CONSTRAINT [PK_cliente] PRIMARY KEY ([id]))

/* CreateForeignKey FK_cliente_profissao cliente(profissao_id) profissao(id) */
ALTER TABLE [dbo].[cliente] ADD CONSTRAINT [FK_cliente_profissao] FOREIGN KEY ([profissao_id]) REFERENCES [dbo].[profissao] ([id])

/* CreateForeignKey FK_cliente_areainteresse cliente(areainteresse_id) areainteresse(id) */
ALTER TABLE [dbo].[cliente] ADD CONSTRAINT [FK_cliente_areainteresse] FOREIGN KEY ([areainteresse_id]) REFERENCES [dbo].[areainteresse] ([id])

/* CreateTable tipoprestadorservicoref */
CREATE TABLE [dbo].[tipoprestadorservicoref] ([id] BIGINT NOT NULL, [tipoprestadorservico] BIGINT NOT NULL)

/* CreateTable prestadorservico */
CREATE TABLE [dbo].[prestadorservico] ([id] BIGINT NOT NULL, [codigo] BIGINT NOT NULL, [comissao] DOUBLE PRECISION NOT NULL, [datacadastro] DATETIME NOT NULL, [ativo] BIT NOT NULL, [unidade_id] BIGINT NOT NULL, CONSTRAINT [PK_prestadorservico] PRIMARY KEY ([id]))

/* CreateTable pessoa */
CREATE TABLE [dbo].[pessoa] ([id] BIGINT NOT NULL, [tipopessoa] NVARCHAR(255) NOT NULL, [cpfcnpj] NVARCHAR(18), [nome] NVARCHAR(100) NOT NULL, [nomefantasia] NVARCHAR(100), [documentoidentidade] NVARCHAR(20), [orgaoexpedidor] NVARCHAR(20), [inscricaoestadual] NVARCHAR(20), [inscricaomunicipal] NVARCHAR(20), [inscricaosuframa] NVARCHAR(9), [datanascimento] DATETIME, [site] NVARCHAR(100), [datacadastro] DATETIME NOT NULL, [foto_id] BIGINT, [fornecedor_id] BIGINT, [cliente_id] BIGINT, [prestadorservico_id] BIGINT, [unidade_id] BIGINT, [funcionario_id] BIGINT, CONSTRAINT [PK_pessoa] PRIMARY KEY ([id]))

/* CreateForeignKey FK_pessoa_foto pessoa(foto_id) arquivo(id) */
ALTER TABLE [dbo].[pessoa] ADD CONSTRAINT [FK_pessoa_foto] FOREIGN KEY ([foto_id]) REFERENCES [dbo].[arquivo] ([id])

/* CreateForeignKey FK_pessoa_fornecedor pessoa(fornecedor_id) fornecedor(id) */
ALTER TABLE [dbo].[pessoa] ADD CONSTRAINT [FK_pessoa_fornecedor] FOREIGN KEY ([fornecedor_id]) REFERENCES [dbo].[fornecedor] ([id])

/* CreateForeignKey FK_pessoa_cliente pessoa(cliente_id) cliente(id) */
ALTER TABLE [dbo].[pessoa] ADD CONSTRAINT [FK_pessoa_cliente] FOREIGN KEY ([cliente_id]) REFERENCES [dbo].[cliente] ([id])

/* CreateForeignKey FK_pessoa_prestadorservico pessoa(prestadorservico_id) prestadorservico(id) */
ALTER TABLE [dbo].[pessoa] ADD CONSTRAINT [FK_pessoa_prestadorservico] FOREIGN KEY ([prestadorservico_id]) REFERENCES [dbo].[prestadorservico] ([id])

/* CreateForeignKey FK_pessoa_unidade pessoa(unidade_id) unidade(id) */
ALTER TABLE [dbo].[pessoa] ADD CONSTRAINT [FK_pessoa_unidade] FOREIGN KEY ([unidade_id]) REFERENCES [dbo].[unidade] ([id])

/* CreateForeignKey FK_pessoa_funcionario pessoa(funcionario_id) funcionario(id) */
ALTER TABLE [dbo].[pessoa] ADD CONSTRAINT [FK_pessoa_funcionario] FOREIGN KEY ([funcionario_id]) REFERENCES [dbo].[funcionario] ([id])

/* CreateForeignKey FK_prestadorservico_unidade prestadorservico(unidade_id) pessoa(id) */
ALTER TABLE [dbo].[prestadorservico] ADD CONSTRAINT [FK_prestadorservico_unidade] FOREIGN KEY ([unidade_id]) REFERENCES [dbo].[pessoa] ([id])

/* CreateTable usuario */
CREATE TABLE [dbo].[usuario] ([id] BIGINT NOT NULL, [login] NVARCHAR(50) NOT NULL, [senha] NVARCHAR(96) NOT NULL, [nome] NVARCHAR(50), [funcionario_id] BIGINT, CONSTRAINT [PK_usuario] PRIMARY KEY ([id]))

/* CreateForeignKey FK_usuario_funcionario usuario(funcionario_id) pessoa(id) */
ALTER TABLE [dbo].[usuario] ADD CONSTRAINT [FK_usuario_funcionario] FOREIGN KEY ([funcionario_id]) REFERENCES [dbo].[pessoa] ([id])

/* CreateTable perfildeacesso */
CREATE TABLE [dbo].[perfildeacesso] ([id] BIGINT NOT NULL, [nome] NVARCHAR(50) NOT NULL, CONSTRAINT [PK_perfildeacesso] PRIMARY KEY ([id]))

/* CreateTable permissao */
CREATE TABLE [dbo].[permissao] ([id] BIGINT NOT NULL, [descricao] NVARCHAR(50) NOT NULL, [action] NVARCHAR(50), [area] NVARCHAR(50), [controller] NVARCHAR(50), [exibenomenu] BIT NOT NULL, [requerpermissao] BIT NOT NULL, [permissaopai_id] BIGINT, CONSTRAINT [PK_permissao] PRIMARY KEY ([id]))

/* CreateForeignKey FK_permissao_permissao permissao(permissaopai_id) permissao(id) */
ALTER TABLE [dbo].[permissao] ADD CONSTRAINT [FK_permissao_permissao] FOREIGN KEY ([permissaopai_id]) REFERENCES [dbo].[permissao] ([id])

/* CreateTable permissaotoperfildeacesso */
CREATE TABLE [dbo].[permissaotoperfildeacesso] ([perfildeacesso_id] BIGINT NOT NULL, [permissao_id] BIGINT NOT NULL)

/* CreateForeignKey FK_permissaotoperfildeacesso_perfildeacesso permissaotoperfildeacesso(perfildeacesso_id) perfildeacesso(id) */
ALTER TABLE [dbo].[permissaotoperfildeacesso] ADD CONSTRAINT [FK_permissaotoperfildeacesso_perfildeacesso] FOREIGN KEY ([perfildeacesso_id]) REFERENCES [dbo].[perfildeacesso] ([id])

/* CreateForeignKey FK_permissaotoperfildeacesso_permissao permissaotoperfildeacesso(permissao_id) permissao(id) */
ALTER TABLE [dbo].[permissaotoperfildeacesso] ADD CONSTRAINT [FK_permissaotoperfildeacesso_permissao] FOREIGN KEY ([permissao_id]) REFERENCES [dbo].[permissao] ([id])

/* CreateTable perfildeacessotousuario */
CREATE TABLE [dbo].[perfildeacessotousuario] ([usuario_id] BIGINT NOT NULL, [perfildeacesso_id] BIGINT NOT NULL)

/* CreateForeignKey FK_perfildeacessotousuario_usuario perfildeacessotousuario(usuario_id) usuario(id) */
ALTER TABLE [dbo].[perfildeacessotousuario] ADD CONSTRAINT [FK_perfildeacessotousuario_usuario] FOREIGN KEY ([usuario_id]) REFERENCES [dbo].[usuario] ([id])

/* CreateForeignKey FK_perfildeacessotousuario_perfildeacesso perfildeacessotousuario(perfildeacesso_id) perfildeacesso(id) */
ALTER TABLE [dbo].[perfildeacessotousuario] ADD CONSTRAINT [FK_perfildeacessotousuario_perfildeacesso] FOREIGN KEY ([perfildeacesso_id]) REFERENCES [dbo].[perfildeacesso] ([id])

/* CreateTable permissaotousuario */
CREATE TABLE [dbo].[permissaotousuario] ([usuario_id] BIGINT NOT NULL, [permissao_id] BIGINT NOT NULL)

/* CreateForeignKey FK_permissaotousuario_usuario permissaotousuario(usuario_id) usuario(id) */
ALTER TABLE [dbo].[permissaotousuario] ADD CONSTRAINT [FK_permissaotousuario_usuario] FOREIGN KEY ([usuario_id]) REFERENCES [dbo].[usuario] ([id])

/* CreateForeignKey FK_permissaotousuario_permissao permissaotousuario(permissao_id) permissao(id) */
ALTER TABLE [dbo].[permissaotousuario] ADD CONSTRAINT [FK_permissaotousuario_permissao] FOREIGN KEY ([permissao_id]) REFERENCES [dbo].[permissao] ([id])

/* CreateTable contato */
CREATE TABLE [dbo].[contato] ([id] BIGINT NOT NULL, [tipocontato] NVARCHAR(255) NOT NULL, [nome] NVARCHAR(100) NOT NULL, [telefone] NVARCHAR(14), [operadora] NVARCHAR(20), [email] NVARCHAR(100), [pessoa_id] BIGINT, CONSTRAINT [PK_contato] PRIMARY KEY ([id]))

/* CreateForeignKey FK_contato_pessoa contato(pessoa_id) pessoa(id) */
ALTER TABLE [dbo].[contato] ADD CONSTRAINT [FK_contato_pessoa] FOREIGN KEY ([pessoa_id]) REFERENCES [dbo].[pessoa] ([id])

/* CreateTable banco */
CREATE TABLE [dbo].[banco] ([id] BIGINT NOT NULL, [codigo] INT NOT NULL, [nome] NVARCHAR(100) NOT NULL, CONSTRAINT [PK_banco] PRIMARY KEY ([id]))

/* CreateTable informacaobancaria */
CREATE TABLE [dbo].[informacaobancaria] ([id] BIGINT NOT NULL, [agencia] NVARCHAR(6) NOT NULL, [conta] NVARCHAR(20) NOT NULL, [tipoconta] NVARCHAR(255) NOT NULL, [dataabertura] DATETIME, [titular] NVARCHAR(100), [telefone] NVARCHAR(14), [pessoa_id] BIGINT, [banco_id] BIGINT NOT NULL, CONSTRAINT [PK_informacaobancaria] PRIMARY KEY ([id]))

/* CreateForeignKey FK_informacaobancaria_pessoa informacaobancaria(pessoa_id) pessoa(id) */
ALTER TABLE [dbo].[informacaobancaria] ADD CONSTRAINT [FK_informacaobancaria_pessoa] FOREIGN KEY ([pessoa_id]) REFERENCES [dbo].[pessoa] ([id])

/* CreateForeignKey FK_informacaobancaria_banco informacaobancaria(banco_id) banco(id) */
ALTER TABLE [dbo].[informacaobancaria] ADD CONSTRAINT [FK_informacaobancaria_banco] FOREIGN KEY ([banco_id]) REFERENCES [dbo].[banco] ([id])

/* CreateTable pais */
CREATE TABLE [dbo].[pais] ([id] BIGINT NOT NULL, [nome] NVARCHAR(100) NOT NULL, [codigobacen] INT NOT NULL, CONSTRAINT [PK_pais] PRIMARY KEY ([id]))

/* CreateTable uf */
CREATE TABLE [dbo].[uf] ([id] BIGINT NOT NULL, [nome] NVARCHAR(100) NOT NULL, [sigla] NVARCHAR(2) NOT NULL, [codigoibge] INT NOT NULL, [pais_id] BIGINT NOT NULL, CONSTRAINT [PK_uf] PRIMARY KEY ([id]))

/* CreateForeignKey FK_uf_pais uf(pais_id) pais(id) */
ALTER TABLE [dbo].[uf] ADD CONSTRAINT [FK_uf_pais] FOREIGN KEY ([pais_id]) REFERENCES [dbo].[pais] ([id])

/* CreateTable cidade */
CREATE TABLE [dbo].[cidade] ([id] BIGINT NOT NULL, [nome] NVARCHAR(255) NOT NULL, [codigoibge] INT NOT NULL, [uf_id] BIGINT NOT NULL, CONSTRAINT [PK_cidade] PRIMARY KEY ([id]))

/* CreateForeignKey FK_cidade_uf cidade(uf_id) uf(id) */
ALTER TABLE [dbo].[cidade] ADD CONSTRAINT [FK_cidade_uf] FOREIGN KEY ([uf_id]) REFERENCES [dbo].[uf] ([id])

/* CreateTable endereco */
CREATE TABLE [dbo].[endereco] ([id] BIGINT NOT NULL, [tipoendereco] NVARCHAR(255) NOT NULL, [logradouro] NVARCHAR(100) NOT NULL, [numero] NVARCHAR(10), [complemento] NVARCHAR(100), [bairro] NVARCHAR(100) NOT NULL, [cep] NVARCHAR(9) NOT NULL, [pessoa_id] BIGINT, [cidade_id] BIGINT NOT NULL, CONSTRAINT [PK_endereco] PRIMARY KEY ([id]))

/* CreateForeignKey FK_endereco_pessoa endereco(pessoa_id) pessoa(id) */
ALTER TABLE [dbo].[endereco] ADD CONSTRAINT [FK_endereco_pessoa] FOREIGN KEY ([pessoa_id]) REFERENCES [dbo].[pessoa] ([id])

/* CreateForeignKey FK_endereco_cidade endereco(cidade_id) cidade(id) */
ALTER TABLE [dbo].[endereco] ADD CONSTRAINT [FK_endereco_cidade] FOREIGN KEY ([cidade_id]) REFERENCES [dbo].[cidade] ([id])

/* CreateTable referencia */
CREATE TABLE [dbo].[referencia] ([id] BIGINT NOT NULL, [tiporeferencia] NVARCHAR(255) NOT NULL, [nome] NVARCHAR(100) NOT NULL, [telefone] NVARCHAR(20) NOT NULL, [celular] NVARCHAR(20) NOT NULL, [observacao] NVARCHAR(4000) NOT NULL, [cliente_id] BIGINT NOT NULL, CONSTRAINT [PK_referencia] PRIMARY KEY ([id]))

/* CreateForeignKey FK_referencia_cliente referencia(cliente_id) cliente(id) */
ALTER TABLE [dbo].[referencia] ADD CONSTRAINT [FK_referencia_cliente] FOREIGN KEY ([cliente_id]) REFERENCES [dbo].[cliente] ([id])

/* CreateTable graudependencia */
CREATE TABLE [dbo].[graudependencia] ([id] BIGINT NOT NULL, [descricao] NVARCHAR(100) NOT NULL, CONSTRAINT [PK_graudependencia] PRIMARY KEY ([id]))

/* CreateTable dependente */
CREATE TABLE [dbo].[dependente] ([id] BIGINT NOT NULL, [nome] NVARCHAR(20) NOT NULL, [cpf] NVARCHAR(14) NOT NULL, [rg] NVARCHAR(20) NOT NULL, [orgaoexpedidor] NVARCHAR(100) NOT NULL, [cliente_id] BIGINT NOT NULL, [graudependencia_id] BIGINT NOT NULL, CONSTRAINT [PK_dependente] PRIMARY KEY ([id]))

/* CreateForeignKey FK_dependente_cliente dependente(cliente_id) cliente(id) */
ALTER TABLE [dbo].[dependente] ADD CONSTRAINT [FK_dependente_cliente] FOREIGN KEY ([cliente_id]) REFERENCES [dbo].[cliente] ([id])

/* CreateForeignKey FK_dependente_graudependencia dependente(graudependencia_id) graudependencia(id) */
ALTER TABLE [dbo].[dependente] ADD CONSTRAINT [FK_dependente_graudependencia] FOREIGN KEY ([graudependencia_id]) REFERENCES [dbo].[graudependencia] ([id])

/* CreateTable emitente */
CREATE TABLE [dbo].[emitente] ([id] BIGINT NOT NULL, [agencia] NVARCHAR(6) NOT NULL, [conta] NVARCHAR(8) NOT NULL, [nome1] NVARCHAR(100) NOT NULL, [cpfcnpj1] NVARCHAR(18) NOT NULL, [documento1] NVARCHAR(20), [orgaoexpedidor1] NVARCHAR(20), [nome2] NVARCHAR(100), [cpfcnpj2] NVARCHAR(18), [documento2] NVARCHAR(20), [orgaoexpedidor2] NVARCHAR(20), [clientedesde] DATETIME NOT NULL, [ativo] BIT NOT NULL, [banco_id] BIGINT NOT NULL, CONSTRAINT [PK_emitente] PRIMARY KEY ([id]))

/* CreateForeignKey FK_emitente_banco emitente(banco_id) banco(id) */
ALTER TABLE [dbo].[emitente] ADD CONSTRAINT [FK_emitente_banco] FOREIGN KEY ([banco_id]) REFERENCES [dbo].[banco] ([id])

/* CreateTable chequerecebido */
CREATE TABLE [dbo].[chequerecebido] ([id] BIGINT NOT NULL, [comp] INT, [agencia] NVARCHAR(6) NOT NULL, [conta] NVARCHAR(8) NOT NULL, [numerocheque] NVARCHAR(6) NOT NULL, [cmc7] NVARCHAR(35), [valor] DOUBLE PRECISION NOT NULL, [nominal] NVARCHAR(100), [dataemissao] DATETIME NOT NULL, [datavencimento] DATETIME NOT NULL, [dataprorrogacao] DATETIME, [praca] NVARCHAR(100) NOT NULL, [historico] NVARCHAR(4000), [observacao] NVARCHAR(4000), [saldo] DOUBLE PRECISION NOT NULL, [compensado] BIT NOT NULL, [cliente_id] BIGINT NOT NULL, [banco_id] BIGINT NOT NULL, [emitente_id] BIGINT NOT NULL, [unidade_id] BIGINT NOT NULL, CONSTRAINT [PK_chequerecebido] PRIMARY KEY ([id]))

/* CreateForeignKey FK_chequerecebido_cliente chequerecebido(cliente_id) pessoa(id) */
ALTER TABLE [dbo].[chequerecebido] ADD CONSTRAINT [FK_chequerecebido_cliente] FOREIGN KEY ([cliente_id]) REFERENCES [dbo].[pessoa] ([id])

/* CreateForeignKey FK_chequerecebido_banco chequerecebido(banco_id) banco(id) */
ALTER TABLE [dbo].[chequerecebido] ADD CONSTRAINT [FK_chequerecebido_banco] FOREIGN KEY ([banco_id]) REFERENCES [dbo].[banco] ([id])

/* CreateForeignKey FK_chequerecebido_emitente chequerecebido(emitente_id) emitente(id) */
ALTER TABLE [dbo].[chequerecebido] ADD CONSTRAINT [FK_chequerecebido_emitente] FOREIGN KEY ([emitente_id]) REFERENCES [dbo].[emitente] ([id])

/* CreateForeignKey FK_chequerecebido_unidade chequerecebido(unidade_id) pessoa(id) */
ALTER TABLE [dbo].[chequerecebido] ADD CONSTRAINT [FK_chequerecebido_unidade] FOREIGN KEY ([unidade_id]) REFERENCES [dbo].[pessoa] ([id])

/* CreateTable baixachequerecebido */
CREATE TABLE [dbo].[baixachequerecebido] ([id] BIGINT NOT NULL, [data] DATETIME NOT NULL, [taxajuros] DOUBLE PRECISION NOT NULL, [valorjuros] DOUBLE PRECISION NOT NULL, [valordesconto] DOUBLE PRECISION NOT NULL, [valor] DOUBLE PRECISION NOT NULL, [historico] NVARCHAR(4000), [observacao] NVARCHAR(4000), [chequerecebido_id] BIGINT NOT NULL, [cobrador_id] BIGINT, CONSTRAINT [PK_baixachequerecebido] PRIMARY KEY ([id]))

/* CreateForeignKey FK_baixachequerecebido_chequerecebido baixachequerecebido(chequerecebido_id) chequerecebido(id) */
ALTER TABLE [dbo].[baixachequerecebido] ADD CONSTRAINT [FK_baixachequerecebido_chequerecebido] FOREIGN KEY ([chequerecebido_id]) REFERENCES [dbo].[chequerecebido] ([id])

/* CreateForeignKey FK_baixachequerecebido_cobrador baixachequerecebido(cobrador_id) pessoa(id) */
ALTER TABLE [dbo].[baixachequerecebido] ADD CONSTRAINT [FK_baixachequerecebido_cobrador] FOREIGN KEY ([cobrador_id]) REFERENCES [dbo].[pessoa] ([id])

/* CreateTable chequerecebidoprestadorservico */
CREATE TABLE [dbo].[chequerecebidoprestadorservico] ([chequerecebido_id] BIGINT NOT NULL, [prestadorservico_id] BIGINT NOT NULL, CONSTRAINT [PK_chequerecebidoprestadorservico] PRIMARY KEY ([chequerecebido_id], [prestadorservico_id]))

/* CreateForeignKey FK_chequerecebidoprestadorservico_chequerecebido chequerecebidoprestadorservico(chequerecebido_id) chequerecebido(id) */
ALTER TABLE [dbo].[chequerecebidoprestadorservico] ADD CONSTRAINT [FK_chequerecebidoprestadorservico_chequerecebido] FOREIGN KEY ([chequerecebido_id]) REFERENCES [dbo].[chequerecebido] ([id])

/* CreateForeignKey FK_chequerecebidoprestadorservico_prestadorservico chequerecebidoprestadorservico(prestadorservico_id) pessoa(id) */
ALTER TABLE [dbo].[chequerecebidoprestadorservico] ADD CONSTRAINT [FK_chequerecebidoprestadorservico_prestadorservico] FOREIGN KEY ([prestadorservico_id]) REFERENCES [dbo].[pessoa] ([id])

/* CreateTable chequerecebidofuncionario */
CREATE TABLE [dbo].[chequerecebidofuncionario] ([chequerecebido_id] BIGINT NOT NULL, [funcionario_id] BIGINT NOT NULL, CONSTRAINT [PK_chequerecebidofuncionario] PRIMARY KEY ([chequerecebido_id], [funcionario_id]))

/* CreateForeignKey FK_chequerecebidofuncionario_chequerecebido chequerecebidofuncionario(chequerecebido_id) chequerecebido(id) */
ALTER TABLE [dbo].[chequerecebidofuncionario] ADD CONSTRAINT [FK_chequerecebidofuncionario_chequerecebido] FOREIGN KEY ([chequerecebido_id]) REFERENCES [dbo].[chequerecebido] ([id])

/* CreateForeignKey FK_chequerecebidofuncionario_funcionario chequerecebidofuncionario(funcionario_id) pessoa(id) */
ALTER TABLE [dbo].[chequerecebidofuncionario] ADD CONSTRAINT [FK_chequerecebidofuncionario_funcionario] FOREIGN KEY ([funcionario_id]) REFERENCES [dbo].[pessoa] ([id])

/* CreateTable compensacaocheque */
CREATE TABLE [dbo].[compensacaocheque] ([id] BIGINT NOT NULL, [codigo] INT NOT NULL, [descricao] NVARCHAR(256) NOT NULL, CONSTRAINT [PK_compensacaocheque] PRIMARY KEY ([id]))

/* CreateTable ocorrenciacompensacao */
CREATE TABLE [dbo].[ocorrenciacompensacao] ([id] BIGINT NOT NULL, [data] DATETIME NOT NULL, [chequesituacao] NVARCHAR(256) NOT NULL, [historico] NVARCHAR(4000), [observacao] NVARCHAR(4000), [chequerecebido_id] BIGINT NOT NULL, [compensacaocheque_id] BIGINT, CONSTRAINT [PK_ocorrenciacompensacao] PRIMARY KEY ([id]))

/* CreateForeignKey FK_ocorrenciacompensacao_chequerecebido ocorrenciacompensacao(chequerecebido_id) chequerecebido(id) */
ALTER TABLE [dbo].[ocorrenciacompensacao] ADD CONSTRAINT [FK_ocorrenciacompensacao_chequerecebido] FOREIGN KEY ([chequerecebido_id]) REFERENCES [dbo].[chequerecebido] ([id])

/* CreateForeignKey FK_ocorrenciacompensacao_compensacaocheque ocorrenciacompensacao(compensacaocheque_id) compensacaocheque(id) */
ALTER TABLE [dbo].[ocorrenciacompensacao] ADD CONSTRAINT [FK_ocorrenciacompensacao_compensacaocheque] FOREIGN KEY ([compensacaocheque_id]) REFERENCES [dbo].[compensacaocheque] ([id])

/* CreateTable meiopagamento */
CREATE TABLE [dbo].[meiopagamento] ([id] BIGINT NOT NULL, [descricao] NVARCHAR(100) NOT NULL, CONSTRAINT [PK_meiopagamento] PRIMARY KEY ([id]))

/* CreateTable recebimentochequerecebido */
CREATE TABLE [dbo].[recebimentochequerecebido] ([id] BIGINT NOT NULL, [valor] DOUBLE PRECISION NOT NULL, [baixachequerecebido_id] BIGINT NOT NULL, [meiopagamento_id] BIGINT NOT NULL, CONSTRAINT [PK_recebimentochequerecebido] PRIMARY KEY ([id]))

/* CreateForeignKey FK_recebimentochequerecebido_baixachequerecebido recebimentochequerecebido(baixachequerecebido_id) baixachequerecebido(id) */
ALTER TABLE [dbo].[recebimentochequerecebido] ADD CONSTRAINT [FK_recebimentochequerecebido_baixachequerecebido] FOREIGN KEY ([baixachequerecebido_id]) REFERENCES [dbo].[baixachequerecebido] ([id])

/* CreateForeignKey FK_recebimentochequerecebido_meiopagamento recebimentochequerecebido(meiopagamento_id) meiopagamento(id) */
ALTER TABLE [dbo].[recebimentochequerecebido] ADD CONSTRAINT [FK_recebimentochequerecebido_meiopagamento] FOREIGN KEY ([meiopagamento_id]) REFERENCES [dbo].[meiopagamento] ([id])

/* CreateTable contabancaria */
CREATE TABLE [dbo].[contabancaria] ([id] BIGINT NOT NULL, [agencia] NVARCHAR(6) NOT NULL, [nomeagencia] NVARCHAR(50), [conta] NVARCHAR(8) NOT NULL, [tipocontabancaria] NVARCHAR(256) NOT NULL, [gerente] NVARCHAR(50), [abertura] DATETIME, [telefone] NVARCHAR(20), [banco_id] BIGINT NOT NULL, CONSTRAINT [PK_contabancaria] PRIMARY KEY ([id]))

/* CreateForeignKey FK_contabancaria_banco contabancaria(banco_id) banco(id) */
ALTER TABLE [dbo].[contabancaria] ADD CONSTRAINT [FK_contabancaria_banco] FOREIGN KEY ([banco_id]) REFERENCES [dbo].[banco] ([id])

/* CreateTable extratobancario */
CREATE TABLE [dbo].[extratobancario] ([id] BIGINT NOT NULL, [tipolancamento] NVARCHAR(256) NOT NULL, [emissao] DATETIME NOT NULL, [compensacao] DATETIME, [descricao] NVARCHAR(100), [valor] DOUBLE PRECISION NOT NULL, [compensado] BIT NOT NULL, [cancelado] BIT NOT NULL, [contabancaria_id] BIGINT NOT NULL, CONSTRAINT [PK_extratobancario] PRIMARY KEY ([id]))

/* CreateForeignKey FK_extratobancario_contabancaria extratobancario(contabancaria_id) contabancaria(id) */
ALTER TABLE [dbo].[extratobancario] ADD CONSTRAINT [FK_extratobancario_contabancaria] FOREIGN KEY ([contabancaria_id]) REFERENCES [dbo].[contabancaria] ([id])

/* ExecuteEmbeddedSqlScript Fashion.ERP.Migrator.Scripts._201301010000.cidade.sql */
INSERT INTO pais (id, nome, codigobacen) VALUES (1, 'AFEGANISTAO' ,132);
INSERT INTO pais (id, nome, codigobacen) VALUES (2, 'ALBANIA, REPUBLICA DA' ,175);
INSERT INTO pais (id, nome, codigobacen) VALUES (3, 'ALEMANHA' ,230);
INSERT INTO pais (id, nome, codigobacen) VALUES (4, 'BURKINA FASO' ,310);
INSERT INTO pais (id, nome, codigobacen) VALUES (5, 'ANDORRA' ,370);
INSERT INTO pais (id, nome, codigobacen) VALUES (6, 'ANGOLA' ,400);
INSERT INTO pais (id, nome, codigobacen) VALUES (7, 'ANGUILLA' ,418);
INSERT INTO pais (id, nome, codigobacen) VALUES (8, 'ANTIGUA E BARBUDA' ,434);
INSERT INTO pais (id, nome, codigobacen) VALUES (9, 'ANTILHAS HOLANDESAS' ,477);
INSERT INTO pais (id, nome, codigobacen) VALUES (10, 'ARABIA SAUDITA' ,531);
INSERT INTO pais (id, nome, codigobacen) VALUES (11, 'ARGELIA' ,590);
INSERT INTO pais (id, nome, codigobacen) VALUES (12, 'ARGENTINA' ,639);
INSERT INTO pais (id, nome, codigobacen) VALUES (13, 'ARMENIA, REPUBLICA DA' ,647);
INSERT INTO pais (id, nome, codigobacen) VALUES (14, 'ARUBA' ,655);
INSERT INTO pais (id, nome, codigobacen) VALUES (15, 'AUSTRALIA' ,698);
INSERT INTO pais (id, nome, codigobacen) VALUES (16, 'AUSTRIA' ,728);
INSERT INTO pais (id, nome, codigobacen) VALUES (17, 'AZERBAIJAO, REPUBLICA DO' ,736);
INSERT INTO pais (id, nome, codigobacen) VALUES (18, 'BAHAMAS, ILHAS' ,779);
INSERT INTO pais (id, nome, codigobacen) VALUES (19, 'BAHREIN, ILHAS' ,809);
INSERT INTO pais (id, nome, codigobacen) VALUES (20, 'BANGLADESH' ,817);
INSERT INTO pais (id, nome, codigobacen) VALUES (21, 'BARBADOS' ,833);
INSERT INTO pais (id, nome, codigobacen) VALUES (22, 'BELARUS, REPUBLICA DA' ,850);
INSERT INTO pais (id, nome, codigobacen) VALUES (23, 'BELGICA' ,876);
INSERT INTO pais (id, nome, codigobacen) VALUES (24, 'BELIZE' ,884);
INSERT INTO pais (id, nome, codigobacen) VALUES (25, 'BERMUDAS' ,906);
INSERT INTO pais (id, nome, codigobacen) VALUES (26, 'MIANMAR (BIRMANIA)' ,930);
INSERT INTO pais (id, nome, codigobacen) VALUES (27, 'BOLIVIA, ESTADO PLURINACIONAL DA' ,973);
INSERT INTO pais (id, nome, codigobacen) VALUES (28, 'BOSNIA-HERZEGOVINA (REPUBLICA DA)' ,981);
INSERT INTO pais (id, nome, codigobacen) VALUES (29, 'BOTSUANA' ,1015);
INSERT INTO pais (id, nome, codigobacen) VALUES (30, 'BRASIL' ,1058);
INSERT INTO pais (id, nome, codigobacen) VALUES (31, 'BRUNEI' ,1082);
INSERT INTO pais (id, nome, codigobacen) VALUES (32, 'BULGARIA, REPUBLICA DA' ,1112);
INSERT INTO pais (id, nome, codigobacen) VALUES (33, 'BURUNDI' ,1155);
INSERT INTO pais (id, nome, codigobacen) VALUES (34, 'BUTAO' ,1198);
INSERT INTO pais (id, nome, codigobacen) VALUES (35, 'CABO VERDE, REPUBLICA DE' ,1279);
INSERT INTO pais (id, nome, codigobacen) VALUES (36, 'CAYMAN, ILHAS' ,1376);
INSERT INTO pais (id, nome, codigobacen) VALUES (37, 'CAMBOJA' ,1414);
INSERT INTO pais (id, nome, codigobacen) VALUES (38, 'CAMAROES' ,1457);
INSERT INTO pais (id, nome, codigobacen) VALUES (39, 'CANADA' ,1490);
INSERT INTO pais (id, nome, codigobacen) VALUES (40, 'GUERNSEY, ILHA DO CANAL (INCLUI ALDERNEY E SARK)' ,1504);
INSERT INTO pais (id, nome, codigobacen) VALUES (41, 'JERSEY, ILHA DO CANAL' ,1508);
INSERT INTO pais (id, nome, codigobacen) VALUES (42, 'CANARIAS, ILHAS' ,1511);
INSERT INTO pais (id, nome, codigobacen) VALUES (43, 'CAZAQUISTAO, REPUBLICA DO' ,1538);
INSERT INTO pais (id, nome, codigobacen) VALUES (44, 'CATAR' ,1546);
INSERT INTO pais (id, nome, codigobacen) VALUES (45, 'CHILE' ,1589);
INSERT INTO pais (id, nome, codigobacen) VALUES (46, 'CHINA, REPUBLICA POPULAR' ,1600);
INSERT INTO pais (id, nome, codigobacen) VALUES (47, 'FORMOSA (TAIWAN)' ,1619);
INSERT INTO pais (id, nome, codigobacen) VALUES (48, 'CHIPRE' ,1635);
INSERT INTO pais (id, nome, codigobacen) VALUES (49, 'COCOS(KEELING),ILHAS' ,1651);
INSERT INTO pais (id, nome, codigobacen) VALUES (50, 'COLOMBIA' ,1694);
INSERT INTO pais (id, nome, codigobacen) VALUES (51, 'COMORES, ILHAS' ,1732);
INSERT INTO pais (id, nome, codigobacen) VALUES (52, 'CONGO' ,1775);
INSERT INTO pais (id, nome, codigobacen) VALUES (53, 'COOK, ILHAS' ,1830);
INSERT INTO pais (id, nome, codigobacen) VALUES (54, 'COREIA (DO NORTE), REP.POP.DEMOCRATICA' ,1872);
INSERT INTO pais (id, nome, codigobacen) VALUES (55, 'COREIA (DO SUL), REPUBLICA DA' ,1902);
INSERT INTO pais (id, nome, codigobacen) VALUES (56, 'COSTA DO MARFIM' ,1937);
INSERT INTO pais (id, nome, codigobacen) VALUES (57, 'CROACIA (REPUBLICA DA)' ,1953);
INSERT INTO pais (id, nome, codigobacen) VALUES (58, 'COSTA RICA' ,1961);
INSERT INTO pais (id, nome, codigobacen) VALUES (59, 'COVEITE' ,1988);
INSERT INTO pais (id, nome, codigobacen) VALUES (60, 'CUBA' ,1996);
INSERT INTO pais (id, nome, codigobacen) VALUES (61, 'BENIN' ,2291);
INSERT INTO pais (id, nome, codigobacen) VALUES (62, 'DINAMARCA' ,2321);
INSERT INTO pais (id, nome, codigobacen) VALUES (63, 'DOMINICA,ILHA' ,2356);
INSERT INTO pais (id, nome, codigobacen) VALUES (64, 'EQUADOR' ,2399);
INSERT INTO pais (id, nome, codigobacen) VALUES (65, 'EGITO' ,2402);
INSERT INTO pais (id, nome, codigobacen) VALUES (66, 'ERITREIA' ,2437);
INSERT INTO pais (id, nome, codigobacen) VALUES (67, 'EMIRADOS ARABES UNIDOS' ,2445);
INSERT INTO pais (id, nome, codigobacen) VALUES (68, 'ESPANHA' ,2453);
INSERT INTO pais (id, nome, codigobacen) VALUES (69, 'ESLOVENIA, REPUBLICA DA' ,2461);
INSERT INTO pais (id, nome, codigobacen) VALUES (70, 'ESLOVACA, REPUBLICA' ,2470);
INSERT INTO pais (id, nome, codigobacen) VALUES (71, 'ESTADOS UNIDOS' ,2496);
INSERT INTO pais (id, nome, codigobacen) VALUES (72, 'ESTONIA, REPUBLICA DA' ,2518);
INSERT INTO pais (id, nome, codigobacen) VALUES (73, 'ETIOPIA' ,2534);
INSERT INTO pais (id, nome, codigobacen) VALUES (74, 'FALKLAND (ILHAS MALVINAS)' ,2550);
INSERT INTO pais (id, nome, codigobacen) VALUES (75, 'FEROE, ILHAS' ,2593);
INSERT INTO pais (id, nome, codigobacen) VALUES (76, 'FILIPINAS' ,2674);
INSERT INTO pais (id, nome, codigobacen) VALUES (77, 'FINLANDIA' ,2712);
INSERT INTO pais (id, nome, codigobacen) VALUES (78, 'FRANCA' ,2755);
INSERT INTO pais (id, nome, codigobacen) VALUES (79, 'GABAO' ,2810);
INSERT INTO pais (id, nome, codigobacen) VALUES (80, 'GAMBIA' ,2852);
INSERT INTO pais (id, nome, codigobacen) VALUES (81, 'GANA' ,2895);
INSERT INTO pais (id, nome, codigobacen) VALUES (82, 'GEORGIA, REPUBLICA DA' ,2917);
INSERT INTO pais (id, nome, codigobacen) VALUES (83, 'GIBRALTAR' ,2933);
INSERT INTO pais (id, nome, codigobacen) VALUES (84, 'GRANADA' ,2976);
INSERT INTO pais (id, nome, codigobacen) VALUES (85, 'GRECIA' ,3018);
INSERT INTO pais (id, nome, codigobacen) VALUES (86, 'GROENLANDIA' ,3050);
INSERT INTO pais (id, nome, codigobacen) VALUES (87, 'GUADALUPE' ,3093);
INSERT INTO pais (id, nome, codigobacen) VALUES (88, 'GUAM' ,3131);
INSERT INTO pais (id, nome, codigobacen) VALUES (89, 'GUATEMALA' ,3174);
INSERT INTO pais (id, nome, codigobacen) VALUES (90, 'GUIANA FRANCESA' ,3255);
INSERT INTO pais (id, nome, codigobacen) VALUES (91, 'GUINE' ,3298);
INSERT INTO pais (id, nome, codigobacen) VALUES (92, 'GUINE-EQUATORIAL' ,3310);
INSERT INTO pais (id, nome, codigobacen) VALUES (93, 'GUINE-BISSAU' ,3344);
INSERT INTO pais (id, nome, codigobacen) VALUES (94, 'GUIANA' ,3379);
INSERT INTO pais (id, nome, codigobacen) VALUES (95, 'HAITI' ,3417);
INSERT INTO pais (id, nome, codigobacen) VALUES (96, 'HONDURAS' ,3450);
INSERT INTO pais (id, nome, codigobacen) VALUES (97, 'HONG KONG' ,3514);
INSERT INTO pais (id, nome, codigobacen) VALUES (98, 'HUNGRIA, REPUBLICA DA' ,3557);
INSERT INTO pais (id, nome, codigobacen) VALUES (99, 'IEMEN' ,3573);
INSERT INTO pais (id, nome, codigobacen) VALUES (100, 'MAN, ILHA DE' ,3595);
INSERT INTO pais (id, nome, codigobacen) VALUES (101, 'INDIA' ,3611);
INSERT INTO pais (id, nome, codigobacen) VALUES (102, 'INDONESIA' ,3654);
INSERT INTO pais (id, nome, codigobacen) VALUES (103, 'IRAQUE' ,3697);
INSERT INTO pais (id, nome, codigobacen) VALUES (104, 'IRA, REPUBLICA ISLAMICA DO' ,3727);
INSERT INTO pais (id, nome, codigobacen) VALUES (105, 'IRLANDA' ,3751);
INSERT INTO pais (id, nome, codigobacen) VALUES (106, 'ISLANDIA' ,3794);
INSERT INTO pais (id, nome, codigobacen) VALUES (107, 'ISRAEL' ,3832);
INSERT INTO pais (id, nome, codigobacen) VALUES (108, 'ITALIA' ,3867);
INSERT INTO pais (id, nome, codigobacen) VALUES (109, 'JAMAICA' ,3913);
INSERT INTO pais (id, nome, codigobacen) VALUES (110, 'JOHNSTON, ILHAS' ,3964);
INSERT INTO pais (id, nome, codigobacen) VALUES (111, 'JAPAO' ,3999);
INSERT INTO pais (id, nome, codigobacen) VALUES (112, 'JORDANIA' ,4030);
INSERT INTO pais (id, nome, codigobacen) VALUES (113, 'KIRIBATI' ,4111);
INSERT INTO pais (id, nome, codigobacen) VALUES (114, 'LAOS, REP.POP.DEMOCR.DO' ,4200);
INSERT INTO pais (id, nome, codigobacen) VALUES (115, 'LEBUAN,ILHAS' ,4235);
INSERT INTO pais (id, nome, codigobacen) VALUES (116, 'LESOTO' ,4260);
INSERT INTO pais (id, nome, codigobacen) VALUES (117, 'LETONIA, REPUBLICA DA' ,4278);
INSERT INTO pais (id, nome, codigobacen) VALUES (118, 'LIBANO' ,4316);
INSERT INTO pais (id, nome, codigobacen) VALUES (119, 'LIBERIA' ,4340);
INSERT INTO pais (id, nome, codigobacen) VALUES (120, 'LIBIA' ,4383);
INSERT INTO pais (id, nome, codigobacen) VALUES (121, 'LIECHTENSTEIN' ,4405);
INSERT INTO pais (id, nome, codigobacen) VALUES (122, 'LITUANIA, REPUBLICA DA' ,4421);
INSERT INTO pais (id, nome, codigobacen) VALUES (123, 'LUXEMBURGO' ,4456);
INSERT INTO pais (id, nome, codigobacen) VALUES (124, 'MACAU' ,4472);
INSERT INTO pais (id, nome, codigobacen) VALUES (125, 'MACEDONIA, ANT.REP.IUGOSLAVA' ,4499);
INSERT INTO pais (id, nome, codigobacen) VALUES (126, 'MADAGASCAR' ,4502);
INSERT INTO pais (id, nome, codigobacen) VALUES (127, 'MADEIRA, ILHA DA' ,4525);
INSERT INTO pais (id, nome, codigobacen) VALUES (128, 'MALASIA' ,4553);
INSERT INTO pais (id, nome, codigobacen) VALUES (129, 'MALAVI' ,4588);
INSERT INTO pais (id, nome, codigobacen) VALUES (130, 'MALDIVAS' ,4618);
INSERT INTO pais (id, nome, codigobacen) VALUES (131, 'MALI' ,4642);
INSERT INTO pais (id, nome, codigobacen) VALUES (132, 'MALTA' ,4677);
INSERT INTO pais (id, nome, codigobacen) VALUES (133, 'MARIANAS DO NORTE' ,4723);
INSERT INTO pais (id, nome, codigobacen) VALUES (134, 'MARROCOS' ,4740);
INSERT INTO pais (id, nome, codigobacen) VALUES (135, 'MARSHALL,ILHAS' ,4766);
INSERT INTO pais (id, nome, codigobacen) VALUES (136, 'MARTINICA' ,4774);
INSERT INTO pais (id, nome, codigobacen) VALUES (137, 'MAURICIO' ,4855);
INSERT INTO pais (id, nome, codigobacen) VALUES (138, 'MAURITANIA' ,4880);
INSERT INTO pais (id, nome, codigobacen) VALUES (139, 'MAYOTTE (ILHAS FRANCESAS)' ,4885);
INSERT INTO pais (id, nome, codigobacen) VALUES (140, 'MIDWAY, ILHAS' ,4901);
INSERT INTO pais (id, nome, codigobacen) VALUES (141, 'MEXICO' ,4936);
INSERT INTO pais (id, nome, codigobacen) VALUES (142, 'MOLDAVIA, REPUBLICA DA' ,4944);
INSERT INTO pais (id, nome, codigobacen) VALUES (143, 'MONACO' ,4952);
INSERT INTO pais (id, nome, codigobacen) VALUES (144, 'MONGOLIA' ,4979);
INSERT INTO pais (id, nome, codigobacen) VALUES (145, 'MONTENEGRO' ,4985);
INSERT INTO pais (id, nome, codigobacen) VALUES (146, 'MICRONESIA' ,4995);
INSERT INTO pais (id, nome, codigobacen) VALUES (147, 'MONTSERRAT,ILHAS' ,5010);
INSERT INTO pais (id, nome, codigobacen) VALUES (148, 'MOCAMBIQUE' ,5053);
INSERT INTO pais (id, nome, codigobacen) VALUES (149, 'NAMIBIA' ,5070);
INSERT INTO pais (id, nome, codigobacen) VALUES (150, 'NAURU' ,5088);
INSERT INTO pais (id, nome, codigobacen) VALUES (151, 'CHRISTMAS,ILHA (NAVIDAD)' ,5118);
INSERT INTO pais (id, nome, codigobacen) VALUES (152, 'NEPAL' ,5177);
INSERT INTO pais (id, nome, codigobacen) VALUES (153, 'NICARAGUA' ,5215);
INSERT INTO pais (id, nome, codigobacen) VALUES (154, 'NIGER' ,5258);
INSERT INTO pais (id, nome, codigobacen) VALUES (155, 'NIGERIA' ,5282);
INSERT INTO pais (id, nome, codigobacen) VALUES (156, 'NIUE,ILHA' ,5312);
INSERT INTO pais (id, nome, codigobacen) VALUES (157, 'NORFOLK,ILHA' ,5355);
INSERT INTO pais (id, nome, codigobacen) VALUES (158, 'NORUEGA' ,5380);
INSERT INTO pais (id, nome, codigobacen) VALUES (159, 'NOVA CALEDONIA' ,5428);
INSERT INTO pais (id, nome, codigobacen) VALUES (160, 'PAPUA NOVA GUINE' ,5452);
INSERT INTO pais (id, nome, codigobacen) VALUES (161, 'NOVA ZELANDIA' ,5487);
INSERT INTO pais (id, nome, codigobacen) VALUES (162, 'VANUATU' ,5517);
INSERT INTO pais (id, nome, codigobacen) VALUES (163, 'OMA' ,5568);
INSERT INTO pais (id, nome, codigobacen) VALUES (164, 'PACIFICO,ILHAS DO (POSSESSAO DOS EUA)' ,5665);
INSERT INTO pais (id, nome, codigobacen) VALUES (165, 'PAISES BAIXOS (HOLANDA)' ,5738);
INSERT INTO pais (id, nome, codigobacen) VALUES (166, 'PALAU' ,5754);
INSERT INTO pais (id, nome, codigobacen) VALUES (167, 'PAQUISTAO' ,5762);
INSERT INTO pais (id, nome, codigobacen) VALUES (168, 'PANAMA' ,5800);
INSERT INTO pais (id, nome, codigobacen) VALUES (169, 'PARAGUAI' ,5860);
INSERT INTO pais (id, nome, codigobacen) VALUES (170, 'PERU' ,5894);
INSERT INTO pais (id, nome, codigobacen) VALUES (171, 'PITCAIRN,ILHA' ,5932);
INSERT INTO pais (id, nome, codigobacen) VALUES (172, 'POLINESIA FRANCESA' ,5991);
INSERT INTO pais (id, nome, codigobacen) VALUES (173, 'POLONIA, REPUBLICA DA' ,6033);
INSERT INTO pais (id, nome, codigobacen) VALUES (174, 'PORTUGAL' ,6076);
INSERT INTO pais (id, nome, codigobacen) VALUES (175, 'PORTO RICO' ,6114);
INSERT INTO pais (id, nome, codigobacen) VALUES (176, 'QUENIA' ,6238);
INSERT INTO pais (id, nome, codigobacen) VALUES (177, 'QUIRGUIZ, REPUBLICA' ,6254);
INSERT INTO pais (id, nome, codigobacen) VALUES (178, 'REINO UNIDO' ,6289);
INSERT INTO pais (id, nome, codigobacen) VALUES (179, 'REPUBLICA CENTRO-AFRICANA' ,6408);
INSERT INTO pais (id, nome, codigobacen) VALUES (180, 'REPUBLICA DOMINICANA' ,6475);
INSERT INTO pais (id, nome, codigobacen) VALUES (181, 'REUNIAO, ILHA' ,6602);
INSERT INTO pais (id, nome, codigobacen) VALUES (182, 'ZIMBABUE' ,6653);
INSERT INTO pais (id, nome, codigobacen) VALUES (183, 'ROMENIA' ,6700);
INSERT INTO pais (id, nome, codigobacen) VALUES (184, 'RUANDA' ,6750);
INSERT INTO pais (id, nome, codigobacen) VALUES (185, 'RUSSIA, FEDERACAO DA' ,6769);
INSERT INTO pais (id, nome, codigobacen) VALUES (186, 'SALOMAO, ILHAS' ,6777);
INSERT INTO pais (id, nome, codigobacen) VALUES (187, 'SAARA OCIDENTAL' ,6858);
INSERT INTO pais (id, nome, codigobacen) VALUES (188, 'EL SALVADOR' ,6874);
INSERT INTO pais (id, nome, codigobacen) VALUES (189, 'SAMOA' ,6904);
INSERT INTO pais (id, nome, codigobacen) VALUES (190, 'SAMOA AMERICANA' ,6912);
INSERT INTO pais (id, nome, codigobacen) VALUES (191, 'SAO CRISTOVAO E NEVES,ILHAS' ,6955);
INSERT INTO pais (id, nome, codigobacen) VALUES (192, 'SAN MARINO' ,6971);
INSERT INTO pais (id, nome, codigobacen) VALUES (193, 'SAO PEDRO E MIQUELON' ,7005);
INSERT INTO pais (id, nome, codigobacen) VALUES (194, 'SAO VICENTE E GRANADINAS' ,7056);
INSERT INTO pais (id, nome, codigobacen) VALUES (195, 'SANTA HELENA' ,7102);
INSERT INTO pais (id, nome, codigobacen) VALUES (196, 'SANTA LUCIA' ,7153);
INSERT INTO pais (id, nome, codigobacen) VALUES (197, 'SAO TOME E PRINCIPE, ILHAS' ,7200);
INSERT INTO pais (id, nome, codigobacen) VALUES (198, 'SENEGAL' ,7285);
INSERT INTO pais (id, nome, codigobacen) VALUES (199, 'SEYCHELLES' ,7315);
INSERT INTO pais (id, nome, codigobacen) VALUES (200, 'SERRA LEOA' ,7358);
INSERT INTO pais (id, nome, codigobacen) VALUES (201, 'SERVIA' ,7370);
INSERT INTO pais (id, nome, codigobacen) VALUES (202, 'CINGAPURA' ,7412);
INSERT INTO pais (id, nome, codigobacen) VALUES (203, 'SIRIA, REPUBLICA ARABE DA' ,7447);
INSERT INTO pais (id, nome, codigobacen) VALUES (204, 'SOMALIA' ,7480);
INSERT INTO pais (id, nome, codigobacen) VALUES (205, 'SRI LANKA' ,7501);
INSERT INTO pais (id, nome, codigobacen) VALUES (206, 'SUAZILANDIA' ,7544);
INSERT INTO pais (id, nome, codigobacen) VALUES (207, 'AFRICA DO SUL' ,7560);
INSERT INTO pais (id, nome, codigobacen) VALUES (208, 'SUDAO' ,7595);
INSERT INTO pais (id, nome, codigobacen) VALUES (209, 'SUECIA' ,7641);
INSERT INTO pais (id, nome, codigobacen) VALUES (210, 'SUICA' ,7676);
INSERT INTO pais (id, nome, codigobacen) VALUES (211, 'SURINAME' ,7706);
INSERT INTO pais (id, nome, codigobacen) VALUES (212, 'TADJIQUISTAO, REPUBLICA DO' ,7722);
INSERT INTO pais (id, nome, codigobacen) VALUES (213, 'TAILANDIA' ,7765);
INSERT INTO pais (id, nome, codigobacen) VALUES (214, 'TANZANIA, REP.UNIDA DA' ,7803);
INSERT INTO pais (id, nome, codigobacen) VALUES (215, 'TERRITORIO BRIT.OC.INDICO' ,7820);
INSERT INTO pais (id, nome, codigobacen) VALUES (216, 'DJIBUTI' ,7838);
INSERT INTO pais (id, nome, codigobacen) VALUES (217, 'CHADE' ,7889);
INSERT INTO pais (id, nome, codigobacen) VALUES (218, 'TCHECA, REPUBLICA' ,7919);
INSERT INTO pais (id, nome, codigobacen) VALUES (219, 'TIMOR LESTE' ,7951);
INSERT INTO pais (id, nome, codigobacen) VALUES (220, 'TOGO' ,8001);
INSERT INTO pais (id, nome, codigobacen) VALUES (221, 'TOQUELAU,ILHAS' ,8052);
INSERT INTO pais (id, nome, codigobacen) VALUES (222, 'TONGA' ,8109);
INSERT INTO pais (id, nome, codigobacen) VALUES (223, 'TRINIDAD E TOBAGO' ,8150);
INSERT INTO pais (id, nome, codigobacen) VALUES (224, 'TUNISIA' ,8206);
INSERT INTO pais (id, nome, codigobacen) VALUES (225, 'TURCAS E CAICOS,ILHAS' ,8230);
INSERT INTO pais (id, nome, codigobacen) VALUES (226, 'TURCOMENISTAO, REPUBLICA DO' ,8249);
INSERT INTO pais (id, nome, codigobacen) VALUES (227, 'TURQUIA' ,8273);
INSERT INTO pais (id, nome, codigobacen) VALUES (228, 'TUVALU' ,8281);
INSERT INTO pais (id, nome, codigobacen) VALUES (229, 'UCRANIA' ,8311);
INSERT INTO pais (id, nome, codigobacen) VALUES (230, 'UGANDA' ,8338);
INSERT INTO pais (id, nome, codigobacen) VALUES (231, 'URUGUAI' ,8451);
INSERT INTO pais (id, nome, codigobacen) VALUES (232, 'UZBEQUISTAO, REPUBLICA DO' ,8478);
INSERT INTO pais (id, nome, codigobacen) VALUES (233, 'VATICANO, EST.DA CIDADE DO' ,8486);
INSERT INTO pais (id, nome, codigobacen) VALUES (234, 'VENEZUELA' ,8508);
INSERT INTO pais (id, nome, codigobacen) VALUES (235, 'VIETNA' ,8583);
INSERT INTO pais (id, nome, codigobacen) VALUES (236, 'VIRGENS,ILHAS (BRITANICAS)' ,8630);
INSERT INTO pais (id, nome, codigobacen) VALUES (237, 'VIRGENS,ILHAS (E.U.A.)' ,8664);
INSERT INTO pais (id, nome, codigobacen) VALUES (238, 'FIJI' ,8702);
INSERT INTO pais (id, nome, codigobacen) VALUES (239, 'WAKE, ILHA' ,8737);
INSERT INTO pais (id, nome, codigobacen) VALUES (240, 'CONGO, REPUBLICA DEMOCRATICA DO' ,8885);
INSERT INTO pais (id, nome, codigobacen) VALUES (241, 'ZAMBIA' ,8907);

INSERT INTO uf(id, nome, sigla, codigoibge, pais_id)VALUES (1,'ACRE', 'AC', 12, 30);
INSERT INTO uf(id, nome, sigla, codigoibge, pais_id)VALUES (2,'ALAGOAS', 'AL', 27, 30);
INSERT INTO uf(id, nome, sigla, codigoibge, pais_id)VALUES (3,'AMAPÁ', 'AP', 16, 30);
INSERT INTO uf(id, nome, sigla, codigoibge, pais_id)VALUES (4,'AMAZONAS', 'AM', 13, 30);
INSERT INTO uf(id, nome, sigla, codigoibge, pais_id)VALUES (5,'BAHIA', 'BA', 29, 30);
INSERT INTO uf(id, nome, sigla, codigoibge, pais_id)VALUES (6,'CEARÁ', 'CE', 23, 30);
INSERT INTO uf(id, nome, sigla, codigoibge, pais_id)VALUES (7,'DISTRITO FEDERAL', 'DF', 53, 30);
INSERT INTO uf(id, nome, sigla, codigoibge, pais_id)VALUES (8,'ESPÍRITO SANTO', 'ES', 32, 30);
INSERT INTO uf(id, nome, sigla, codigoibge, pais_id)VALUES (9,'GOIÁS', 'GO', 52, 30);
INSERT INTO uf(id, nome, sigla, codigoibge, pais_id)VALUES (10,'MARANHÃO', 'MA', 21, 30);
INSERT INTO uf(id, nome, sigla, codigoibge, pais_id)VALUES (11,'MATO GROSSO', 'MT', 51, 30);
INSERT INTO uf(id, nome, sigla, codigoibge, pais_id)VALUES (12,'MATO GROSSO DO SUL', 'MS', 50, 30);
INSERT INTO uf(id, nome, sigla, codigoibge, pais_id)VALUES (13,'MINAS GERAIS', 'MG', 31, 30);
INSERT INTO uf(id, nome, sigla, codigoibge, pais_id)VALUES (14,'PARÁ', 'PA', 15, 30);
INSERT INTO uf(id, nome, sigla, codigoibge, pais_id)VALUES (15,'PARAÍBA', 'PB', 25, 30);
INSERT INTO uf(id, nome, sigla, codigoibge, pais_id)VALUES (16,'PARANÁ', 'PR', 41, 30);
INSERT INTO uf(id, nome, sigla, codigoibge, pais_id)VALUES (17,'PERNAMBUCO', 'PE', 26, 30);
INSERT INTO uf(id, nome, sigla, codigoibge, pais_id)VALUES (18,'PIAUÍ', 'PI', 22, 30);
INSERT INTO uf(id, nome, sigla, codigoibge, pais_id)VALUES (19,'RIO DE JANEIRO', 'RJ', 33, 30);
INSERT INTO uf(id, nome, sigla, codigoibge, pais_id)VALUES (20,'RIO GRANDE DO NORTE', 'RN', 24, 30);
INSERT INTO uf(id, nome, sigla, codigoibge, pais_id)VALUES (21,'RIO GRANDE DO SUL', 'RS', 43, 30);
INSERT INTO uf(id, nome, sigla, codigoibge, pais_id)VALUES (22,'RONDÔNIA', 'RO', 11, 30);
INSERT INTO uf(id, nome, sigla, codigoibge, pais_id)VALUES (23,'RORAIMA', 'RR', 14, 30);
INSERT INTO uf(id, nome, sigla, codigoibge, pais_id)VALUES (24,'SANTA CATARINA', 'SC', 42, 30);
INSERT INTO uf(id, nome, sigla, codigoibge, pais_id)VALUES (25,'SÃO PAULO', 'SP', 35, 30);
INSERT INTO uf(id, nome, sigla, codigoibge, pais_id)VALUES (26,'SERGIPE', 'SE', 28, 30);
INSERT INTO uf(id, nome, sigla, codigoibge, pais_id)VALUES (27,'TOCANTINS', 'TO', 17, 30);

INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1,'ABADIA DE GOIÁS',5200050,9);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2,'ABADIA DOS DOURADOS',3100104,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3,'ABADIÂNIA',5200100,9);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4,'ABAETÉ',3100203,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5,'ABAETETUBA',1500107,14);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (6,'ABAIARA',2300101,6);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (7,'ABAÍRA',2900108,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (8,'ABARÉ',2900207,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (9,'ABATIÁ',4100103,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (10,'ABDON BATISTA',4200051,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (11,'ABEL FIGUEIREDO',1500131,14);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (12,'ABELARDO LUZ',4200101,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (13,'ABRE CAMPO',3100302,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (14,'ABREU E LIMA',2600054,17);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (15,'ABREULÂNDIA',1700251,27);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (16,'ACAIACA',3100401,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (17,'AÇAILÂNDIA',2100055,10);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (18,'ACAJUTIBA',2900306,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (19,'ACARÁ',1500206,14);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (20,'ACARAPÉ',2300150,6);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (21,'ACARAÚ',2300200,6);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (22,'ACARI',2400109,20);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (23,'ACAUÃ',2200053,18);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (24,'ACEGUÁ',4300034,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (25,'ACOPIARA',2300309,6);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (26,'ACORIZAL',5100102,11);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (27,'ACRELÂNDIA',1200013,1);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (28,'ACREÚNA',5200134,9);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (29,'AÇU',2400208,20);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (30,'AÇUCENA',3100500,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (31,'ADAMANTINA',3500105,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (32,'ADELÂNDIA',5200159,9);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (33,'ADOLFO',3500204,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (34,'ADRIANÓPOLIS',4100202,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (35,'ADUSTINA',2900355,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (36,'AFOGADOS DA INGAZEIRA',2600104,17);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (37,'AFONSO BEZERRA',2400307,20);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (38,'AFONSO CLÁUDIO',3200102,8);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (39,'AFONSO CUNHA',2100105,10);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (40,'AFRÂNIO',2600203,17);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (41,'AFUÁ',1500305,14);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (42,'AGRESTINA',2600302,17);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (43,'AGRICOLÂNDIA',2200103,18);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (44,'AGROLÂNDIA',4200200,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (45,'AGRONÔMICA',4200309,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (46,'ÁGUA AZUL DO NORTE',1500347,14);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (47,'ÁGUA BOA',3100609,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (48,'ÁGUA BOA',5100201,11);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (49,'ÁGUA BRANCA',2700102,2);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (50,'ÁGUA BRANCA',2500106,15);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (51,'ÁGUA BRANCA',2200202,18);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (52,'ÁGUA CLARA',5000203,12);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (53,'ÁGUA COMPRIDA',3100708,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (54,'ÁGUA DOCE',4200408,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (55,'ÁGUA DOCE DO MARANHÃO',2100154,10);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (56,'ÁGUA DOCE DO NORTE',3200169,8);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (57,'ÁGUA FRIA',2900405,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (58,'ÁGUA FRIA DE GOIÁS',5200175,9);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (59,'ÁGUA LIMPA',5200209,9);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (60,'ÁGUA NOVA',2400406,20);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (61,'ÁGUA PRETA',2600401,17);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (62,'ÁGUA SANTA',4300059,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (63,'AGUAÍ',3500303,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (64,'AGUANIL',3100807,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (65,'ÁGUAS BELAS',2600500,17);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (66,'ÁGUAS DA PRATA',3500402,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (67,'ÁGUAS DE CHAPECÓ',4200507,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (68,'ÁGUAS DE LINDÓIA',3500501,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (69,'ÁGUAS DE SANTA BÁRBARA',3500550,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (70,'ÁGUAS DE SÃO PEDRO',3500600,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (71,'ÁGUAS FORMOSAS',3100906,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (72,'ÁGUAS FRIAS',4200556,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (73,'ÁGUAS LINDAS DE GOIÁS',5200258,9);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (74,'ÁGUAS MORNAS',4200606,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (75,'ÁGUAS VERMELHAS',3101003,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (76,'AGUDO',4300109,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (77,'AGUDOS',3500709,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (78,'AGUDOS DO SUL',4100301,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (79,'ÁGUIA BRANCA',3200136,8);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (80,'AGUIAR',2500205,15);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (81,'AGUIARNÓPOLIS',1700301,27);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (82,'AIMORÉS',3101102,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (83,'AIQUARA',2900603,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (84,'AIUABA',2300408,6);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (85,'AIURUOCA',3101201,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (86,'AJURICABA',4300208,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (87,'ALAGOA',3101300,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (88,'ALAGOA GRANDE',2500304,15);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (89,'ALAGOA NOVA',2500403,15);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (90,'ALAGOINHA',2600609,17);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (91,'ALAGOINHA',2500502,15);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (92,'ALAGOINHA DO PIAUÍ',2200251,18);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (93,'ALAGOINHAS',2900702,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (94,'ALAMBARI',3500758,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (95,'ALBERTINA',3101409,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (96,'ALCÂNTARA',2100204,10);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (97,'ALCÂNTARAS',2300507,6);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (98,'ALCANTIL',2500536,15);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (99,'ALCINÓPOLIS',5000252,12);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (100,'ALCOBAÇA',2900801,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (101,'ALDEIAS ALTAS',2100303,10);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (102,'ALECRIM',4300307,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (103,'ALEGRE',3200201,8);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (104,'ALEGRETE',4300406,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (105,'ALEGRETE DO PIAUÍ',2200277,18);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (106,'ALEGRIA',4300455,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (107,'ALÉM PARAÍBA',3101508,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (108,'ALENQUER',1500404,14);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (109,'ALEXANDRIA',2400505,20);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (110,'ALEXÂNIA',5200308,9);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (111,'ALFENAS',3101607,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (112,'ALFREDO CHAVES',3200300,8);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (113,'ALFREDO MARCONDES',3500808,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (114,'ALFREDO VASCONCELOS',3101631,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (115,'ALFREDO WAGNER',4200705,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (116,'ALGODÃO DE JANDAÍRA',2500577,15);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (117,'ALHANDRA',2500601,15);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (118,'ALIANÇA',2600708,17);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (119,'ALIANÇA DO TOCANTINS',1700350,27);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (120,'ALMADINA',2900900,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (121,'ALMAS',1700400,27);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (122,'ALMEIRIM',1500503,14);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (123,'ALMENARA',3101706,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (124,'ALMINO AFONSO',2400604,20);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (125,'ALMIRANTE TAMANDARÉ',4100400,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (126,'ALMIRANTE TAMANDARÉ DO SUL',4300471,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (127,'ALOÂNDIA',5200506,9);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (128,'ALPERCATA',3101805,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (129,'ALPESTRE',4300505,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (130,'ALPINÓPOLIS',3101904,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (131,'ALTA FLORESTA',5100250,11);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (132,'ALTA FLORESTA D''OESTE',1100015,22);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (133,'ALTAIR',3500907,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (134,'ALTAMIRA',1500602,14);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (135,'ALTAMIRA DO MARANHÃO',2100402,10);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (136,'ALTAMIRA DO PARANÁ',4100459,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (137,'ALTANEIRA',2300606,6);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (138,'ALTEROSA',3102001,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (139,'ALTINHO',2600807,17);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (140,'ALTINÓPOLIS',3501004,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (141,'ALTO ALEGRE',4300554,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (142,'ALTO ALEGRE',3501103,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (143,'ALTO ALEGRE',1400050,23);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (144,'ALTO ALEGRE DO MARANHÃO',2100436,10);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (145,'ALTO ALEGRE DO PINDARÉ',2100477,10);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (146,'ALTO ALEGRE DOS PARECIS',1100379,22);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (147,'ALTO ARAGUAIA',5100300,11);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (148,'ALTO BELA VISTA',4200754,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (149,'ALTO BOA VISTA',5100359,11);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (150,'ALTO CAPARAÓ',3102050,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (151,'ALTO DO RODRIGUES',2400703,20);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (152,'ALTO FELIZ',4300570,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (153,'ALTO GARÇAS',5100409,11);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (154,'ALTO HORIZONTE',5200555,9);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (155,'ALTO JEQUITIBÁ',3153509,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (156,'ALTO LONGÁ',2200301,18);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (157,'ALTO PARAGUAI',5100508,11);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (158,'ALTO PARAÍSO',1100403,22);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (159,'ALTO PARAÍSO DE GOIÁS',5200605,9);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (160,'ALTO PARANÁ',4100608,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (161,'ALTO PARNAÍBA',2100501,10);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (162,'ALTO PIQUIRI',4100707,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (163,'ALTO RIO DOCE',3102100,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (164,'ALTO RIO NOVO',3200359,8);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (165,'ALTO SANTO',2300705,6);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (166,'ALTO TAQUARI',5100607,11);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (167,'ALTÔNIA',4100509,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (168,'ALTOS',2200400,18);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (169,'ALUMÍNIO',3501152,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (170,'ALVARÃES',1300029,4);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (171,'ALVARENGA',3102209,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (172,'ÁLVARES FLORENCE',3501202,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (173,'ÁLVARES MACHADO',3501301,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (174,'ÁLVARO DE CARVALHO',3501400,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (175,'ALVINLÂNDIA',3501509,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (176,'ALVINÓPOLIS',3102308,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (177,'ALVORADA',1700707,27);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (178,'ALVORADA',4300604,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (179,'ALVORADA D''OESTE',1100346,22);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (180,'ALVORADA DE MINAS',3102407,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (181,'ALVORADA DO GURGUÉIA',2200459,18);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (182,'ALVORADA DO NORTE',5200803,9);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (183,'ALVORADA DO SUL',4100806,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (184,'AMAJARI',1400027,23);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (185,'AMAMBAÍ',5000609,12);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (186,'AMAPÁ',1600105,3);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (187,'AMAPÁ DO MARANHÃO',2100550,10);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (188,'AMAPORÃ',4100905,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (189,'AMARAJI',2600906,17);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (190,'AMARAL FERRADOR',4300638,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (191,'AMARALINA',5200829,9);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (192,'AMARANTE',2200509,18);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (193,'AMARANTE DO MARANHÃO',2100600,10);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (194,'AMARGOSA',2901007,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (195,'AMATURÁ',1300060,4);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (196,'AMÉLIA RODRIGUES',2901106,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (197,'AMÉRICA DOURADA',2901155,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (198,'AMERICANA',3501608,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (199,'AMERICANO DO BRASIL',5200852,9);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (200,'AMÉRICO BRASILIENSE',3501707,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (201,'AMÉRICO DE CAMPOS',3501806,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (202,'AMETISTA DO SUL',4300646,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (203,'AMONTADA',2300754,6);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (204,'AMORINÓPOLIS',5200902,9);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (205,'AMPARO',3501905,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (206,'AMPARO',2500734,15);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (207,'AMPARO DE SÃO FRANCISCO',2800100,26);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (208,'AMPARO DO SERRA',3102506,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (209,'AMPÉRE',4101002,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (210,'ANADIA',2700201,2);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (211,'ANAGÉ',2901205,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (212,'ANAHY',4101051,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (213,'ANAJÁS',1500701,14);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (214,'ANAJATUBA',2100709,10);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (215,'ANALÂNDIA',3502002,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (216,'ANAMÃ',1300086,4);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (217,'ANANÁS',1701002,27);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (218,'ANANINDEUA',1500800,14);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (219,'ANÁPOLIS',5201108,9);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (220,'ANAPU',1500859,14);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (221,'ANAPURUS',2100808,10);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (222,'ANASTÁCIO',5000708,12);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (223,'ANAURILÂNDIA',5000807,12);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (224,'ANCHIETA',4200804,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (225,'ANCHIETA',3200409,8);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (226,'ANDARAÍ',2901304,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (227,'ANDIRÁ',4101101,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (228,'ANDORINHA',2901353,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (229,'ANDRADAS',3102605,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (230,'ANDRADINA',3502101,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (231,'ANDRÉ DA ROCHA',4300661,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (232,'ANDRELÂNDIA',3102803,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (233,'ANGATUBA',3502200,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (234,'ANGELÂNDIA',3102852,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (235,'ANGÉLICA',5000856,12);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (236,'ANGELIM',2601003,17);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (237,'ANGELINA',4200903,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (238,'ANGICAL',2901403,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (239,'ANGICAL DO PIAUÍ',2200608,18);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (240,'ANGICO',1701051,27);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (241,'ANGICOS',2400802,20);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (242,'ANGRA DOS REIS',3300100,19);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (243,'ANGUERA',2901502,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (244,'ÂNGULO',4101150,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (245,'ANHANGÜERA',5201207,9);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (246,'ANHEMBI',3502309,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (247,'ANHUMAS',3502408,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (248,'ANICUNS',5201306,9);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (249,'ANÍSIO DE ABREU',2200707,18);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (250,'ANITA GARIBALDI',4201000,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (251,'ANITÁPOLIS',4201109,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (252,'ANORI',1300102,4);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (253,'ANTA GORDA',4300703,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (254,'ANTAS',2901601,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (255,'ANTONINA',4101200,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (256,'ANTONINA DO NORTE',2300804,6);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (257,'ANTÔNIO ALMEIDA',2200806,18);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (258,'ANTÔNIO CARDOSO',2901700,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (259,'ANTÔNIO CARLOS',3102902,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (260,'ANTÔNIO CARLOS',4201208,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (261,'ANTÔNIO DIAS',3103009,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (262,'ANTÔNIO GONÇALVES',2901809,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (263,'ANTÔNIO JOÃO',5000906,12);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (264,'ANTÔNIO MARTINS',2400901,20);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (265,'ANTÔNIO OLINTO',4101309,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (266,'ANTÔNIO PRADO',4300802,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (267,'ANTÔNIO PRADO DE MINAS',3103108,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (268,'APARECIDA',3502507,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (269,'APARECIDA',2500775,15);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (270,'APARECIDA D''OESTE',3502606,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (271,'APARECIDA DE GOIÂNIA',5201405,9);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (272,'APARECIDA DO RIO DOCE',5201454,9);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (273,'APARECIDA DO RIO NEGRO',1701101,27);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (274,'APARECIDA DO TABOADO',5001003,12);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (275,'APERIBÉ',3300159,19);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (276,'APIACÁ',3200508,8);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (277,'APIACÁS',5100805,11);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (278,'APIAÍ',3502705,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (279,'APICUM-AÇU',2100832,10);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (280,'APIÚNA',4201257,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (281,'APODI',2401008,20);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (282,'APORÁ',2901908,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (283,'APORÉ',5201504,9);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (284,'APUAREMA',2901957,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (285,'APUCARANA',4101408,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (286,'APUÍ',1300144,4);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (287,'APUIARÉS',2300903,6);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (288,'AQUIDABÃ',2800209,26);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (289,'AQUIDAUANA',5001102,12);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (290,'AQUIRAZ',2301000,6);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (291,'ARABUTÃ',4201273,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (292,'ARAÇAGI',2500809,15);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (293,'ARAÇAÍ',3103207,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (294,'ARACAJU',2800308,26);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (295,'ARAÇARIGUAMA',3502754,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (296,'ARAÇÁS',2902054,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (297,'ARACATI',2301109,6);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (298,'ARACATU',2902005,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (299,'ARAÇATUBA',3502804,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (300,'ARACI',2902104,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (301,'ARACITABA',3103306,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (302,'ARAÇOIABA',2601052,17);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (303,'ARACOIABA',2301208,6);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (304,'ARAÇOIABA DA SERRA',3502903,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (305,'ARACRUZ',3200607,8);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (306,'ARAÇU',5201603,9);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (307,'ARAÇUAÍ',3103405,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (308,'ARAGARÇAS',5201702,9);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (309,'ARAGOIÂNIA',5201801,9);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (310,'ARAGOMINAS',1701309,27);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (311,'ARAGUACEMA',1701903,27);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (312,'ARAGUAÇU',1702000,27);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (313,'ARAGUAIANA',5101001,11);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (314,'ARAGUAÍNA',1702109,27);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (315,'ARAGUAINHA',5101209,11);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (316,'ARAGUANÃ',1702158,27);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (317,'ARAGUANÃ',2100873,10);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (318,'ARAGUAPAZ',5202155,9);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (319,'ARAGUARI',3103504,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (320,'ARAGUATINS',1702208,27);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (321,'ARAIOSES',2100907,10);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (322,'ARAL MOREIRA',5001243,12);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (323,'ARAMARI',2902203,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (324,'ARAMBARÉ',4300851,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (325,'ARAME',2100956,10);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (326,'ARAMINA',3503000,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (327,'ARANDU',3503109,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (328,'ARANTINA',3103603,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (329,'ARAPEÍ',3503158,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (330,'ARAPIRACA',2700300,2);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (331,'ARAPOEMA',1702307,27);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (332,'ARAPONGA',3103702,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (333,'ARAPONGAS',4101507,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (334,'ARAPORÃ',3103751,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (335,'ARAPOTI',4101606,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (336,'ARAPUÁ',3103801,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (337,'ARAPUÃ',4101655,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (338,'ARAPUTANGA',5101258,11);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (339,'ARAQUARI',4201307,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (340,'ARARA',2500908,15);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (341,'ARARANGUÁ',4201406,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (342,'ARARAQUARA',3503208,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (343,'ARARAS',3503307,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (344,'ARARENDÁ',2301257,6);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (345,'ARARI',2101004,10);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (346,'ARARICÁ',4300877,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (347,'ARARIPE',2301307,6);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (348,'ARARIPINA',2601102,17);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (349,'ARARUAMA',3300209,19);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (350,'ARARUNA',2501005,15);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (351,'ARARUNA',4101705,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (352,'ARATACA',2902252,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (353,'ARATIBA',4300901,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (354,'ARATUBA',2301406,6);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (355,'ARATUÍPE',2902302,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (356,'ARAUÁ',2800407,26);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (357,'ARAUCÁRIA',4101804,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (358,'ARAÚJOS',3103900,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (359,'ARAXÁ',3104007,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (360,'ARCEBURGO',3104106,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (361,'ARCO-ÍRIS',3503356,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (362,'ARCOS',3104205,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (363,'ARCOVERDE',2601201,17);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (364,'AREADO',3104304,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (365,'AREAL',3300225,19);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (366,'AREALVA',3503406,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (367,'AREIA',2501104,15);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (368,'AREIA BRANCA',2800506,26);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (369,'AREIA BRANCA',2401107,20);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (370,'AREIA DE BARAÚNAS',2501153,15);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (371,'AREIAL',2501203,15);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (372,'AREIAS',3503505,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (373,'AREIÓPOLIS',3503604,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (374,'ARENÁPOLIS',5101308,11);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (375,'ARENÓPOLIS',5202353,9);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (376,'ARÊS',2401206,20);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (377,'ARGIRITA',3104403,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (378,'ARICANDUVA',3104452,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (379,'ARINOS',3104502,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (380,'ARIPUANÃ',5101407,11);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (381,'ARIQUEMES',1100023,22);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (382,'ARIRANHA',3503703,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (383,'ARIRANHA DO IVAÍ',4101853,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (384,'ARMAÇÃO DOS BÚZIOS',3300233,19);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (385,'ARMAZÉM',4201505,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (386,'ARNEIROZ',2301505,6);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (387,'AROAZES',2200905,18);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (388,'AROEIRAS',2501302,15);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (389,'ARRAIAL',2201002,18);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (390,'ARRAIAL DO CABO',3300258,19);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (391,'ARRAIAS',1702406,27);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (392,'ARROIO DO MEIO',4301008,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (393,'ARROIO DO PADRE',4301073,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (394,'ARROIO DO SAL',4301057,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (395,'ARROIO DO TIGRE',4301206,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (396,'ARROIO DOS RATOS',4301107,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (397,'ARROIO GRANDE',4301305,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (398,'ARROIO TRINTA',4201604,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (399,'ARTUR NOGUEIRA',3503802,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (400,'ARUANÃ',5202502,9);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (401,'ARUJÁ',3503901,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (402,'ARVOREDO',4201653,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (403,'ARVOREZINHA',4301404,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (404,'ASCURRA',4201703,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (405,'ASPÁSIA',3503950,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (406,'ASSAÍ',4101903,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (407,'ASSARÉ',2301604,6);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (408,'ASSIS',3504008,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (409,'ASSIS BRASIL',1200054,1);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (410,'ASSIS CHATEAUBRIAND',4102000,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (411,'ASSUNÇÃO',2501351,15);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (412,'ASSUNÇÃO DO PIAUÍ',2201051,18);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (413,'ASTOLFO DUTRA',3104601,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (414,'ASTORGA',4102109,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (415,'ATALAIA',2700409,2);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (416,'ATALAIA',4102208,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (417,'ATALAIA DO NORTE',1300201,4);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (418,'ATALANTA',4201802,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (419,'ATALÉIA',3104700,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (420,'ATIBAIA',3504107,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (421,'ATÍLIO VIVACQUA',3200706,8);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (422,'AUGUSTINÓPOLIS',1702554,27);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (423,'AUGUSTO CORRÊA',1500909,14);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (424,'AUGUSTO DE LIMA',3104809,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (425,'AUGUSTO PESTANA',4301503,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (426,'AUGUSTO SEVERO',2401305,20);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (427,'ÁUREA',4301552,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (428,'AURELINO LEAL',2902401,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (429,'AURIFLAMA',3504206,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (430,'AURILÂNDIA',5202601,9);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (431,'AURORA',4201901,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (432,'AURORA',2301703,6);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (433,'AURORA DO PARÁ',1500958,14);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (434,'AURORA DO TOCANTINS',1702703,27);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (435,'AUTAZES',1300300,4);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (436,'AVAÍ',3504305,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (437,'AVANHANDAVA',3504404,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (438,'AVARÉ',3504503,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (439,'AVEIRO',1501006,14);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (440,'AVELINO LOPES',2201101,18);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (441,'AVELINÓPOLIS',5202809,9);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (442,'AXIXÁ',2101103,10);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (443,'AXIXÁ DO TOCANTINS',1702901,27);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (444,'BABAÇULÂNDIA',1703008,27);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (445,'BACABAL',2101202,10);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (446,'BACABEIRA',2101251,10);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (447,'BACURI',2101301,10);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (448,'BACURITUBA',2101350,10);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (449,'BADY BASSITT',3504602,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (450,'BAEPENDI',3104908,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (451,'BAGÉ',4301602,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (452,'BAGRE',1501105,14);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (453,'BAÍA DA TRAIÇÃO',2501401,15);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (454,'BAÍA FORMOSA',2401404,20);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (455,'BAIANÓPOLIS',2902500,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (456,'BAIÃO',1501204,14);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (457,'BAIXA GRANDE',2902609,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (458,'BAIXA GRANDE DO RIBEIRO',2201150,18);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (459,'BAIXIO',2301802,6);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (460,'BAIXO GUANDU',3200805,8);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (461,'BALBINOS',3504701,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (462,'BALDIM',3105004,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (463,'BALIZA',5203104,9);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (464,'BALNEÁRIO ARROIO DO SILVA',4201950,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (465,'BALNEÁRIO BARRA DO SUL',4202057,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (466,'BALNEÁRIO CAMBORIÚ',4202008,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (467,'BALNEÁRIO GAIVOTA',4202073,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (468,'BALNEÁRIO PINHAL',4301636,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (469,'BALSA NOVA',4102307,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (470,'BÁLSAMO',3504800,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (471,'BALSAS',2101400,10);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (472,'BAMBUÍ',3105103,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (473,'BANABUIÚ',2301851,6);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (474,'BANANAL',3504909,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (475,'BANANEIRAS',2501500,15);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (476,'BANDEIRA',3105202,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (477,'BANDEIRA DO SUL',3105301,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (478,'BANDEIRANTE',4202081,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (479,'BANDEIRANTES',5001508,12);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (480,'BANDEIRANTES',4102406,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (481,'BANDEIRANTES DO TOCANTINS',1703057,27);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (482,'BANNACH',1501253,14);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (483,'BANZAÊ',2902658,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (484,'BARÃO',4301651,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (485,'BARÃO DE ANTONINA',3505005,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (486,'BARÃO DE COCAIS',3105400,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (487,'BARÃO DE COTEGIPE',4301701,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (488,'BARÃO DE GRAJAÚ',2101509,10);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (489,'BARÃO DE MELGAÇO',5101605,11);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (490,'BARÃO DE MONTE ALTO',3105509,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (491,'BARÃO DO TRIUNFO',4301750,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (492,'BARAÚNA',2501534,15);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (493,'BARAÚNA',2401453,20);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (494,'BARBACENA',3105608,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (495,'BARBALHA',2301901,6);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (496,'BARBOSA',3505104,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (497,'BARBOSA FERRAZ',4102505,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (498,'BARCARENA',1501303,14);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (499,'BARCELONA',2401503,20);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (500,'BARCELOS',1300409,4);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (501,'BARIRI',3505203,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (502,'BARRA',2902708,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (503,'BARRA BONITA',3505302,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (504,'BARRA BONITA',4202099,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (505,'BARRA D''ALCÂNTARA',2201176,18);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (506,'BARRA DA ESTIVA',2902807,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (507,'BARRA DE GUABIRABA',2601300,17);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (508,'BARRA DE SANTA ROSA',2501609,15);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (509,'BARRA DE SANTANA',2501575,15);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (510,'BARRA DE SANTO ANTÔNIO',2700508,2);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (511,'BARRA DE SÃO FRANCISCO',3200904,8);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (512,'BARRA DE SÃO MIGUEL',2700607,2);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (513,'BARRA DE SÃO MIGUEL',2501708,15);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (514,'BARRA DO BUGRES',5101704,11);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (515,'BARRA DO CHAPÉU',3505351,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (516,'BARRA DO CHOÇA',2902906,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (517,'BARRA DO CORDA',2101608,10);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (518,'BARRA DO GARÇAS',5101803,11);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (519,'BARRA DO GUARITA',4301859,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (520,'BARRA DO JACARÉ',4102703,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (521,'BARRA DO MENDES',2903003,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (522,'BARRA DO OURO',1703073,27);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (523,'BARRA DO PIRAÍ',3300308,19);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (524,'BARRA DO QUARAÍ',4301875,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (525,'BARRA DO RIBEIRO',4301909,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (526,'BARRA DO RIO AZUL',4301925,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (527,'BARRA DO ROCHA',2903102,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (528,'BARRA DO TURVO',3505401,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (529,'BARRA DOS COQUEIROS',2800605,26);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (530,'BARRA FUNDA',4301958,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (531,'BARRA LONGA',3105707,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (532,'BARRA MANSA',3300407,19);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (533,'BARRA VELHA',4202107,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (534,'BARRACÃO',4301800,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (535,'BARRACÃO',4102604,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (536,'BARRAS',2201200,18);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (537,'BARREIRA',2301950,6);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (538,'BARREIRAS',2903201,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (539,'BARREIRAS DO PIAUÍ',2201309,18);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (540,'BARREIRINHA',1300508,4);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (541,'BARREIRINHAS',2101707,10);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (542,'BARREIROS',2601409,17);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (543,'BARRETOS',3505500,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (544,'BARRINHA',3505609,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (545,'BARRO',2302008,6);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (546,'BARRO ALTO',2903235,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (547,'BARRO ALTO',5203203,9);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (548,'BARRO DURO',2201408,18);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (549,'BARRO PRETO',2903300,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (550,'BARROCAS',2903276,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (551,'BARROLÂNDIA',1703107,27);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (552,'BARROQUINHA',2302057,6);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (553,'BARROS CASSAL',4302006,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (554,'BARROSO',3105905,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (555,'BARUERI',3505708,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (556,'BASTOS',3505807,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (557,'BATAGUASSU',5001904,12);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (558,'BATAIPORÃ',5002001,12);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (559,'BATALHA',2700706,2);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (560,'BATALHA',2201507,18);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (561,'BATATAIS',3505906,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (562,'BATURITÉ',2302107,6);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (563,'BAURU',3506003,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (564,'BAYEUX',2501807,15);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (565,'BEBEDOURO',3506102,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (566,'BEBERIBE',2302206,6);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (567,'BELA CRUZ',2302305,6);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (568,'BELA VISTA',5002100,12);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (569,'BELA VISTA DA CAROBA',4102752,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (570,'BELA VISTA DE GOIÁS',5203302,9);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (571,'BELA VISTA DE MINAS',3106002,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (572,'BELA VISTA DO MARANHÃO',2101772,10);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (573,'BELA VISTA DO PARAÍSO',4102802,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (574,'BELA VISTA DO PIAUÍ',2201556,18);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (575,'BELA VISTA DO TOLDO',4202131,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (576,'BELÁGUA',2101731,10);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (577,'BELÉM',2700805,2);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (578,'BELÉM',2501906,15);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (579,'BELÉM',1501402,14);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (580,'BELÉM DE MARIA',2601508,17);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (581,'BELÉM DE SÃO FRANCISCO',2601607,17);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (582,'BELÉM DO BREJO DO CRUZ',2502003,15);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (583,'BELÉM DO PIAUÍ',2201572,18);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (584,'BELFORD ROXO',3300456,19);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (585,'BELMIRO BRAGA',3106101,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (586,'BELMONTE',2903409,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (587,'BELMONTE',4202156,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (588,'BELO CAMPO',2903508,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (589,'BELO HORIZONTE',3106200,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (590,'BELO JARDIM',2601706,17);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (591,'BELO MONTE',2700904,2);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (592,'BELO ORIENTE',3106309,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (593,'BELO VALE',3106408,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (594,'BELTERRA',1501451,14);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (595,'BENEDITINOS',2201606,18);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (596,'BENEDITO LEITE',2101806,10);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (597,'BENEDITO NOVO',4202206,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (598,'BENEVIDES',1501501,14);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (599,'BENJAMIN CONSTANT',1300607,4);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (600,'BENJAMIN CONSTANT DO SUL',4302055,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (601,'BENTO DE ABREU',3506201,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (602,'BENTO FERNANDES',2401602,20);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (603,'BENTO GONÇALVES',4302105,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (604,'BEQUIMÃO',2101905,10);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (605,'BERILO',3106507,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (606,'BERIZAL',3106655,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (607,'BERNARDINO BATISTA',2502052,15);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (608,'BERNARDINO DE CAMPOS',3506300,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (609,'BERNARDO DO MEARIM',2101939,10);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (610,'BERNARDO SAYÃO',1703206,27);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (611,'BERTIOGA',3506359,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (612,'BERTOLÍNIA',2201705,18);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (613,'BERTÓPOLIS',3106606,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (614,'BERURI',1300631,4);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (615,'BETÂNIA',2601805,17);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (616,'BETÂNIA DO PIAUÍ',2201739,18);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (617,'BETIM',3106705,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (618,'BEZERROS',2601904,17);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (619,'BIAS FORTES',3106804,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (620,'BICAS',3106903,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (621,'BIGUAÇU',4202305,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (622,'BILAC',3506409,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (623,'BIQUINHAS',3107000,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (624,'BIRIGUI',3506508,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (625,'BIRITIBA-MIRIM',3506607,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (626,'BIRITINGA',2903607,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (627,'BITURUNA',4102901,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (628,'BLUMENAU',4202404,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (629,'BOA ESPERANÇA',3107109,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (630,'BOA ESPERANÇA',3201001,8);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (631,'BOA ESPERANÇA',4103008,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (632,'BOA ESPERANÇA DO IGUAÇU',4103024,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (633,'BOA ESPERANÇA DO SUL',3506706,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (634,'BOA HORA',2201770,18);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (635,'BOA NOVA',2903706,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (636,'BOA VENTURA',2502102,15);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (637,'BOA VENTURA DE SÃO ROQUE',4103040,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (638,'BOA VIAGEM',2302404,6);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (639,'BOA VISTA',2502151,15);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (640,'BOA VISTA',1400100,23);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (641,'BOA VISTA DA APARECIDA',4103057,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (642,'BOA VISTA DAS MISSÕES',4302154,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (643,'BOA VISTA DO BURICÁ',4302204,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (644,'BOA VISTA DO CADEADO',4302220,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (645,'BOA VISTA DO GURUPI',2101970,10);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (646,'BOA VISTA DO INCRA',4302238,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (647,'BOA VISTA DO RAMOS',1300680,4);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (648,'BOA VISTA DO SUL',4302253,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (649,'BOA VISTA DO TUPIM',2903805,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (650,'BOCA DA MATA',2701001,2);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (651,'BOCA DO ACRE',1300706,4);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (652,'BOCAINA',3506805,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (653,'BOCAINA',2201804,18);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (654,'BOCAINA DE MINAS',3107208,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (655,'BOCAINA DO SUL',4202438,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (656,'BOCAIÚVA',3107307,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (657,'BOCAIÚVA DO SUL',4103107,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (658,'BODÓ',2401651,20);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (659,'BODOCÓ',2602001,17);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (660,'BODOQUENA',5002159,12);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (661,'BOFETE',3506904,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (662,'BOITUVA',3507001,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (663,'BOM CONSELHO',2602100,17);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (664,'BOM DESPACHO',3107406,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (665,'BOM JARDIM',2602209,17);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (666,'BOM JARDIM',3300506,19);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (667,'BOM JARDIM',2102002,10);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (668,'BOM JARDIM DA SERRA',4202503,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (669,'BOM JARDIM DE GOIÁS',5203401,9);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (670,'BOM JARDIM DE MINAS',3107505,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (671,'BOM JESUS',4302303,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (672,'BOM JESUS',2502201,15);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (673,'BOM JESUS',4202537,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (674,'BOM JESUS',2401701,20);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (675,'BOM JESUS',2201903,18);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (676,'BOM JESUS DA LAPA',2903904,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (677,'BOM JESUS DA PENHA',3107604,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (678,'BOM JESUS DA SERRA',2903953,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (679,'BOM JESUS DAS SELVAS',2102036,10);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (680,'BOM JESUS DE GOIÁS',5203500,9);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (681,'BOM JESUS DO AMPARO',3107703,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (682,'BOM JESUS DO ARAGUAIA',5101852,11);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (683,'BOM JESUS DO GALHO',3107802,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (684,'BOM JESUS DO ITABAPOANA',3300605,19);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (685,'BOM JESUS DO NORTE',3201100,8);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (686,'BOM JESUS DO OESTE',4202578,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (687,'BOM JESUS DO SUL',4103156,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (688,'BOM JESUS DO TOCANTINS',1703305,27);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (689,'BOM JESUS DO TOCANTINS',1501576,14);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (690,'BOM JESUS DOS PERDÕES',3507100,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (691,'BOM LUGAR',2102077,10);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (692,'BOM PRINCÍPIO',4302352,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (693,'BOM PRINCÍPIO DO PIAUÍ',2201919,18);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (694,'BOM PROGRESSO',4302378,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (695,'BOM REPOUSO',3107901,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (696,'BOM RETIRO',4202602,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (697,'BOM RETIRO DO SUL',4302402,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (698,'BOM SUCESSO',3108008,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (699,'BOM SUCESSO',2502300,15);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (700,'BOM SUCESSO',4103206,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (701,'BOM SUCESSO DE ITARARÉ',3507159,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (702,'BOM SUCESSO DO SUL',4103222,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (703,'BOMBINHAS',4202453,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (704,'BONFIM',3108107,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (705,'BONFIM',1400159,23);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (706,'BONFIM DO PIAUÍ',2201929,18);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (707,'BONFINÓPOLIS',5203559,9);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (708,'BONFINÓPOLIS DE MINAS',3108206,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (709,'BONINAL',2904001,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (710,'BONITO',2904050,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (711,'BONITO',2602308,17);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (712,'BONITO',5002209,12);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (713,'BONITO',1501600,14);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (714,'BONITO DE MINAS',3108255,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (715,'BONITO DE SANTA FÉ',2502409,15);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (716,'BONÓPOLIS',5203575,9);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (717,'BOQUEIRÃO',2502508,15);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (718,'BOQUEIRÃO DO LEÃO',4302451,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (719,'BOQUEIRÃO DO PIAUÍ',2201945,18);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (720,'BOQUIM',2800670,26);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (721,'BOQUIRA',2904100,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (722,'BORÁ',3507209,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (723,'BORACÉIA',3507308,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (724,'BORBA',1300805,4);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (725,'BORBOREMA',3507407,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (726,'BORBOREMA',2502706,15);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (727,'BORDA DA MATA',3108305,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (728,'BOREBI',3507456,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (729,'BORRAZÓPOLIS',4103305,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (730,'BOSSOROCA',4302501,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (731,'BOTELHOS',3108404,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (732,'BOTUCATU',3507506,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (733,'BOTUMIRIM',3108503,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (734,'BOTUPORÃ',2904209,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (735,'BOTUVERÁ',4202701,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (736,'BOZANO',4302584,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (737,'BRAÇO DO NORTE',4202800,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (738,'BRAÇO DO TROMBUDO',4202859,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (739,'BRAGA',4302600,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (740,'BRAGANÇA',1501709,14);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (741,'BRAGANÇA PAULISTA',3507605,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (742,'BRAGANEY',4103354,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (743,'BRANQUINHA',2701100,2);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (744,'BRÁS PIRES',3108701,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (745,'BRASIL NOVO',1501725,14);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (746,'BRASILÂNDIA',5002308,12);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (747,'BRASILÂNDIA DE MINAS',3108552,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (748,'BRASILÂNDIA DO SUL',4103370,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (749,'BRASILÂNDIA DO TOCANTINS',1703602,27);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (750,'BRASILÉIA',1200104,1);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (751,'BRASILEIRA',2201960,18);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (752,'BRASÍLIA',5300108,7);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (753,'BRASÍLIA DE MINAS',3108602,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (754,'BRASNORTE',5101902,11);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (755,'BRASÓPOLIS',3108909,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (756,'BRAÚNA',3507704,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (757,'BRAÚNAS',3108800,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (758,'BRAZABRANTES',5203609,9);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (759,'BREJÃO',2602407,17);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (760,'BREJETUBA',3201159,8);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (761,'BREJINHO',2602506,17);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (762,'BREJINHO',2401800,20);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (763,'BREJINHO DE NAZARÉ',1703701,27);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (764,'BREJO',2102101,10);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (765,'BREJO ALEGRE',3507753,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (766,'BREJO DA MADRE DE DEUS',2602605,17);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (767,'BREJO DE AREIA',2102150,10);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (768,'BREJO DO CRUZ',2502805,15);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (769,'BREJO DO PIAUÍ',2201988,18);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (770,'BREJO DOS SANTOS',2502904,15);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (771,'BREJO GRANDE',2800704,26);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (772,'BREJO GRANDE DO ARAGUAIA',1501758,14);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (773,'BREJO SANTO',2302503,6);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (774,'BREJÕES',2904308,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (775,'BREJOLÂNDIA',2904407,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (776,'BREU BRANCO',1501782,14);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (777,'BREVES',1501808,14);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (778,'BRITÂNIA',5203807,9);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (779,'BROCHIER',4302659,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (780,'BRODÓSQUI',3507803,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (781,'BROTAS',3507902,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (782,'BROTAS DE MACAÚBAS',2904506,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (783,'BRUMADINHO',3109006,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (784,'BRUMADO',2904605,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (785,'BRUNÓPOLIS',4202875,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (786,'BRUSQUE',4202909,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (787,'BUENO BRANDÃO',3109105,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (788,'BUENÓPOLIS',3109204,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (789,'BUENOS AIRES',2602704,17);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (790,'BUERAREMA',2904704,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (791,'BUGRE',3109253,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (792,'BUÍQUE',2602803,17);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (793,'BUJARI',1200138,1);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (794,'BUJARU',1501907,14);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (795,'BURI',3508009,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (796,'BURITAMA',3508108,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (797,'BURITI',2102200,10);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (798,'BURITI ALEGRE',5203906,9);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (799,'BURITI BRAVO',2102309,10);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (800,'BURITI DE GOIÁS',5203939,9);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (801,'BURITI DO TOCANTINS',1703800,27);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (802,'BURITI DOS LOPES',2202000,18);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (803,'BURITI DOS MONTES',2202026,18);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (804,'BURITICUPU',2102325,10);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (805,'BURITINÓPOLIS',5203962,9);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (806,'BURITIRAMA',2904753,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (807,'BURITIRANA',2102358,10);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (808,'BURITIS',3109303,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (809,'BURITIS',1100452,22);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (810,'BURITIZAL',3508207,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (811,'BURITIZEIRO',3109402,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (812,'BUTIÁ',4302709,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (813,'CAAPIRANGA',1300839,4);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (814,'CAAPORÃ',2503001,15);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (815,'CAARAPÓ',5002407,12);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (816,'CAATIBA',2904803,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (817,'CABACEIRAS',2503100,15);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (818,'CABACEIRAS DO PARAGUAÇU',2904852,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (819,'CABECEIRA GRANDE',3109451,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (820,'CABECEIRAS',5204003,9);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (821,'CABECEIRAS DO PIAUÍ',2202059,18);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (822,'CABEDELO',2503209,15);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (823,'CABIXI',1100031,22);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (824,'CABO DE SANTO AGOSTINHO',2602902,17);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (825,'CABO FRIO',3300704,19);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (826,'CABO VERDE',3109501,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (827,'CABRÁLIA PAULISTA',3508306,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (828,'CABREÚVA',3508405,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (829,'CABROBÓ',2603009,17);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (830,'CAÇADOR',4203006,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (831,'CAÇAPAVA',3508504,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (832,'CAÇAPAVA DO SUL',4302808,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (833,'CACAULÂNDIA',1100601,22);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (834,'CACEQUI',4302907,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (835,'CÁCERES',5102504,11);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (836,'CACHOEIRA',2904902,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (837,'CACHOEIRA ALTA',5204102,9);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (838,'CACHOEIRA DA PRATA',3109600,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (839,'CACHOEIRA DE GOIÁS',5204201,9);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (840,'CACHOEIRA DE MINAS',3109709,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (841,'CACHOEIRA DE PAJEÚ',3102704,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (842,'CACHOEIRA DO ARARI',1502004,14);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (843,'CACHOEIRA DO PIRIÁ',1501956,14);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (844,'CACHOEIRA DO SUL',4303004,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (845,'CACHOEIRA DOS ÍNDIOS',2503308,15);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (846,'CACHOEIRA DOURADA',3109808,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (847,'CACHOEIRA DOURADA',5204250,9);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (848,'CACHOEIRA GRANDE',2102374,10);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (849,'CACHOEIRA PAULISTA',3508603,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (850,'CACHOEIRAS DE MACACU',3300803,19);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (851,'CACHOEIRINHA',1703826,27);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (852,'CACHOEIRINHA',4303103,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (853,'CACHOEIRINHA',2603108,17);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (854,'CACHOEIRO DE ITAPEMIRIM',3201209,8);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (855,'CACIMBA DE AREIA',2503407,15);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (856,'CACIMBA DE DENTRO',2503506,15);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (857,'CACIMBAS',2503555,15);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (858,'CACIMBINHAS',2701209,2);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (859,'CACIQUE DOBLE',4303202,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (860,'CACOAL',1100049,22);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (861,'CACONDE',3508702,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (862,'CAÇU',5204300,9);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (863,'CACULÉ',2905008,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (864,'CAÉM',2905107,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (865,'CAETANÓPOLIS',3109907,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (866,'CAETANOS',2905156,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (867,'CAETÉ',3110004,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (868,'CAETÉS',2603207,17);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (869,'CAETITÉ',2905206,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (870,'CAFARNAUM',2905305,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (871,'CAFEARA',4103404,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (872,'CAFELÂNDIA',3508801,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (873,'CAFELÂNDIA',4103453,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (874,'CAFEZAL DO SUL',4103479,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (875,'CAIABU',3508900,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (876,'CAIANA',3110103,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (877,'CAIAPÔNIA',5204409,9);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (878,'CAIBATÉ',4303301,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (879,'CAIBI',4203105,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (880,'CAIÇARA',4303400,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (881,'CAIÇARA',2503605,15);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (882,'CAIÇARA DO NORTE',2401859,20);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (883,'CAIÇARA DO RIO DO VENTO',2401909,20);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (884,'CAICÓ',2402006,20);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (885,'CAIEIRAS',3509007,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (886,'CAIRU',2905404,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (887,'CAIUÁ',3509106,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (888,'CAJAMAR',3509205,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (889,'CAJAPIÓ',2102408,10);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (890,'CAJARI',2102507,10);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (891,'CAJATI',3509254,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (892,'CAJAZEIRAS',2503704,15);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (893,'CAJAZEIRAS DO PIAUÍ',2202075,18);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (894,'CAJAZEIRINHAS',2503753,15);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (895,'CAJOBI',3509304,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (896,'CAJUEIRO',2701308,2);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (897,'CAJUEIRO DA PRAIA',2202083,18);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (898,'CAJURI',3110202,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (899,'CAJURU',3509403,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (900,'CALÇADO',2603306,17);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (901,'CALÇOENE',1600204,3);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (902,'CALDAS',3110301,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (903,'CALDAS BRANDÃO',2503803,15);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (904,'CALDAS NOVAS',5204508,9);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (905,'CALDAZINHA',5204557,9);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (906,'CALDEIRÃO GRANDE',2905503,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (907,'CALDEIRÃO GRANDE DO PIAUÍ',2202091,18);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (908,'CALIFÓRNIA',4103503,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (909,'CALMON',4203154,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (910,'CALUMBI',2603405,17);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (911,'CAMACAN',2905602,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (912,'CAMAÇARI',2905701,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (913,'CAMACHO',3110400,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (914,'CAMALAÚ',2503902,15);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (915,'CAMAMU',2905800,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (916,'CAMANDUCAIA',3110509,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (917,'CAMAPUÃ',5002605,12);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (918,'CAMAQUÃ',4303509,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (919,'CAMARAGIBE',2603454,17);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (920,'CAMARGO',4303558,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (921,'CAMBARÁ',4103602,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (922,'CAMBARÁ DO SUL',4303608,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (923,'CAMBÉ',4103701,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (924,'CAMBIRA',4103800,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (925,'CAMBORIÚ',4203204,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (926,'CAMBUCI',3300902,19);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (927,'CAMBUÍ',3110608,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (928,'CAMBUQUIRA',3110707,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (929,'CAMETÁ',1502103,14);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (930,'CAMOCIM',2302602,6);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (931,'CAMOCIM DE SÃO FÉLIX',2603504,17);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (932,'CAMPANÁRIO',3110806,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (933,'CAMPANHA',3110905,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (934,'CAMPESTRE',3111002,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (935,'CAMPESTRE',2701357,2);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (936,'CAMPESTRE DA SERRA',4303673,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (937,'CAMPESTRE DE GOIÁS',5204607,9);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (938,'CAMPESTRE DO MARANHÃO',2102556,10);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (939,'CAMPINA DA LAGOA',4103909,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (940,'CAMPINA DAS MISSÕES',4303707,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (941,'CAMPINA DO MONTE ALEGRE',3509452,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (942,'CAMPINA DO SIMÃO',4103958,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (943,'CAMPINA GRANDE',2504009,15);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (944,'CAMPINA GRANDE DO SUL',4104006,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (945,'CAMPINA VERDE',3111101,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (946,'CAMPINAÇU',5204656,9);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (947,'CAMPINÁPOLIS',5102603,11);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (948,'CAMPINAS',3509502,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (949,'CAMPINAS DO PIAUÍ',2202109,18);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (950,'CAMPINAS DO SUL',4303806,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (951,'CAMPINORTE',5204706,9);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (952,'CAMPO ALEGRE',2701407,2);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (953,'CAMPO ALEGRE',4203303,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (954,'CAMPO ALEGRE DE GOIÁS',5204805,9);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (955,'CAMPO ALEGRE DE LOURDES',2905909,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (956,'CAMPO ALEGRE DO FIDALGO',2202117,18);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (957,'CAMPO AZUL',3111150,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (958,'CAMPO BELO',3111200,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (959,'CAMPO BELO DO SUL',4203402,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (960,'CAMPO BOM',4303905,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (961,'CAMPO BONITO',4104055,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (962,'CAMPO DE SANTANA',2516409,15);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (963,'CAMPO DO BRITO',2801009,26);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (964,'CAMPO DO MEIO',3111309,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (965,'CAMPO DO TENENTE',4104105,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (966,'CAMPO ERÊ',4203501,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (967,'CAMPO FLORIDO',3111408,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (968,'CAMPO FORMOSO',2906006,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (969,'CAMPO GRANDE',2701506,2);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (970,'CAMPO GRANDE',5002704,12);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (971,'CAMPO GRANDE DO PIAUÍ',2202133,18);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (972,'CAMPO LARGO',4104204,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (973,'CAMPO LARGO DO PIAUÍ',2202174,18);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (974,'CAMPO LIMPO DE GOIÁS',5204854,9);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (975,'CAMPO LIMPO PAULISTA',3509601,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (976,'CAMPO MAGRO',4104253,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (977,'CAMPO MAIOR',2202208,18);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (978,'CAMPO MOURÃO',4104303,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (979,'CAMPO NOVO',4304002,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (980,'CAMPO NOVO DE RONDÔNIA',1100700,22);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (981,'CAMPO NOVO DO PARECIS',5102637,11);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (982,'CAMPO REDONDO',2402105,20);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (983,'CAMPO VERDE',5102678,11);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (984,'CAMPOS ALTOS',3111507,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (985,'CAMPOS BELOS',5204904,9);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (986,'CAMPOS BORGES',4304101,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (987,'CAMPOS DE JÚLIO',5102686,11);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (988,'CAMPOS DO JORDÃO',3509700,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (989,'CAMPOS DOS GOYTACAZES',3301009,19);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (990,'CAMPOS GERAIS',3111606,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (991,'CAMPOS LINDOS',1703842,27);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (992,'CAMPOS NOVOS',4203600,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (993,'CAMPOS NOVOS PAULISTA',3509809,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (994,'CAMPOS SALES',2302701,6);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (995,'CAMPOS VERDES',5204953,9);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (996,'CAMUTANGA',2603603,17);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (997,'CANA VERDE',3111903,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (998,'CANAÃ',3111705,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (999,'CANAÃ DOS CARAJÁS',1502152,14);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1000,'CANABRAVA DO NORTE',5102694,11);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1001,'CANANÉIA',3509908,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1002,'CANAPI',2701605,2);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1003,'CANÁPOLIS',3111804,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1004,'CANÁPOLIS',2906105,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1005,'CANARANA',2906204,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1006,'CANARANA',5102702,11);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1007,'CANAS',3509957,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1008,'CANAVIEIRA',2202251,18);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1009,'CANAVIEIRAS',2906303,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1010,'CANDEAL',2906402,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1011,'CANDEIAS',3112000,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1012,'CANDEIAS',2906501,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1013,'CANDEIAS DO JAMARI',1100809,22);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1014,'CANDELÁRIA',4304200,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1015,'CANDIBA',2906600,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1016,'CÂNDIDO DE ABREU',4104402,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1017,'CÂNDIDO GODÓI',4304309,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1018,'CÂNDIDO MENDES',2102606,10);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1019,'CÂNDIDO MOTA',3510005,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1020,'CÂNDIDO RODRIGUES',3510104,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1021,'CÂNDIDO SALES',2906709,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1022,'CANDIOTA',4304358,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1023,'CANDÓI',4104428,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1024,'CANELA',4304408,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1025,'CANELINHA',4203709,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1026,'CANGUARETAMA',2402204,20);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1027,'CANGUÇU',4304507,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1028,'CANHOBA',2801108,26);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1029,'CANHOTINHO',2603702,17);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1030,'CANINDÉ',2302800,6);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1031,'CANINDÉ DE SÃO FRANCISCO',2801207,26);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1032,'CANITAR',3510153,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1033,'CANOAS',4304606,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1034,'CANOINHAS',4203808,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1035,'CANSANÇÃO',2906808,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1036,'CANTÁ',1400175,23);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1037,'CANTAGALO',3112059,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1038,'CANTAGALO',4104451,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1039,'CANTAGALO',3301108,19);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1040,'CANTANHEDE',2102705,10);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1041,'CANTO DO BURITI',2202307,18);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1042,'CANUDOS',2906824,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1043,'CANUDOS DO VALE',4304614,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1044,'CANUTAMA',1300904,4);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1045,'CAPANEMA',1502202,14);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1046,'CAPANEMA',4104501,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1047,'CAPÃO ALTO',4203253,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1048,'CAPÃO BONITO',3510203,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1049,'CAPÃO BONITO DO SUL',4304622,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1050,'CAPÃO DA CANOA',4304630,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1051,'CAPÃO DO CIPÓ',4304655,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1052,'CAPÃO DO LEÃO',4304663,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1053,'CAPARAÓ',3112109,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1054,'CAPELA',2701704,2);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1055,'CAPELA',2801306,26);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1056,'CAPELA DE SANTANA',4304689,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1057,'CAPELA DO ALTO',3510302,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1058,'CAPELA DO ALTO ALEGRE',2906857,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1059,'CAPELA NOVA',3112208,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1060,'CAPELINHA',3112307,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1061,'CAPETINGA',3112406,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1062,'CAPIM',2504033,15);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1063,'CAPIM BRANCO',3112505,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1064,'CAPIM GROSSO',2906873,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1065,'CAPINÓPOLIS',3112604,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1066,'CAPINZAL',4203907,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1067,'CAPINZAL DO NORTE',2102754,10);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1068,'CAPISTRANO',2302909,6);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1069,'CAPITÃO',4304697,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1070,'CAPITÃO ANDRADE',3112653,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1071,'CAPITÃO DE CAMPOS',2202406,18);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1072,'CAPITÃO ENÉAS',3112703,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1073,'CAPITÃO GERVÁSIO OLIVEIRA',2202455,18);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1074,'CAPITÃO LEÔNIDAS MARQUES',4104600,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1075,'CAPITÃO POÇO',1502301,14);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1076,'CAPITÓLIO',3112802,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1077,'CAPIVARI',3510401,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1078,'CAPIVARI DE BAIXO',4203956,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1079,'CAPIVARI DO SUL',4304671,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1080,'CAPIXABA',1200179,1);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1081,'CAPOEIRAS',2603801,17);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1082,'CAPUTIRA',3112901,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1083,'CARAÃ',4304713,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1084,'CARACARAÍ',1400209,23);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1085,'CARACOL',2202505,18);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1086,'CARACOL',5002803,12);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1087,'CARAGUATATUBA',3510500,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1088,'CARAÍ',3113008,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1089,'CARAÍBAS',2906899,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1090,'CARAMBEÍ',4104659,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1091,'CARANAÍBA',3113107,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1092,'CARANDAÍ',3113206,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1093,'CARANGOLA',3113305,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1094,'CARAPEBUS',3300936,19);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1095,'CARAPICUÍBA',3510609,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1096,'CARATINGA',3113404,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1097,'CARAUARI',1301001,4);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1098,'CARAÚBAS',2504074,15);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1099,'CARAÚBAS',2402303,20);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1100,'CARAÚBAS DO PIAUÍ',2202539,18);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1101,'CARAVELAS',2906907,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1102,'CARAZINHO',4304705,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1103,'CARBONITA',3113503,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1104,'CARDEAL DA SILVA',2907004,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1105,'CARDOSO',3510708,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1106,'CARDOSO MOREIRA',3301157,19);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1107,'CAREAÇU',3113602,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1108,'CAREIRO',1301100,4);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1109,'CAREIRO DA VÁRZEA',1301159,4);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1110,'CARIACICA',3201308,8);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1111,'CARIDADE',2303006,6);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1112,'CARIDADE DO PIAUÍ',2202554,18);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1113,'CARINHANHA',2907103,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1114,'CARIRA',2801405,26);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1115,'CARIRÉ',2303105,6);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1116,'CARIRI DO TOCANTINS',1703867,27);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1117,'CARIRIAÇU',2303204,6);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1118,'CARIÚS',2303303,6);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1119,'CARLINDA',5102793,11);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1120,'CARLÓPOLIS',4104709,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1121,'CARLOS BARBOSA',4304804,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1122,'CARLOS CHAGAS',3113701,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1123,'CARLOS GOMES',4304853,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1124,'CARMÉSIA',3113800,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1125,'CARMO',3301207,19);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1126,'CARMO DA CACHOEIRA',3113909,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1127,'CARMO DA MATA',3114006,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1128,'CARMO DE MINAS',3114105,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1129,'CARMO DO CAJURU',3114204,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1130,'CARMO DO PARANAÍBA',3114303,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1131,'CARMO DO RIO CLARO',3114402,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1132,'CARMO DO RIO VERDE',5205000,9);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1133,'CARMOLÂNDIA',1703883,27);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1134,'CARMÓPOLIS',2801504,26);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1135,'CARMÓPOLIS DE MINAS',3114501,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1136,'CARNAÍBA',2603900,17);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1137,'CARNAÚBA DOS DANTAS',2402402,20);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1138,'CARNAUBAIS',2402501,20);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1139,'CARNAUBAL',2303402,6);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1140,'CARNAUBEIRA DA PENHA',2603926,17);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1141,'CARNEIRINHO',3114550,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1142,'CARNEIROS',2701803,2);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1143,'CAROEBE',1400233,23);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1144,'CAROLINA',2102804,10);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1145,'CARPINA',2604007,17);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1146,'CARRANCAS',3114600,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1147,'CARRAPATEIRA',2504108,15);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1148,'CARRASCO BONITO',1703891,27);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1149,'CARUARU',2604106,17);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1150,'CARUTAPERA',2102903,10);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1151,'CARVALHÓPOLIS',3114709,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1152,'CARVALHOS',3114808,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1153,'CASA BRANCA',3510807,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1154,'CASA GRANDE',3114907,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1155,'CASA NOVA',2907202,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1156,'CASCA',4304903,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1157,'CASCALHO RICO',3115003,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1158,'CASCAVEL',4104808,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1159,'CASCAVEL',2303501,6);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1160,'CASEARA',1703909,27);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1161,'CASEIROS',4304952,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1162,'CASIMIRO DE ABREU',3301306,19);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1163,'CASINHAS',2604155,17);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1164,'CASSERENGUE',2504157,15);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1165,'CÁSSIA',3115102,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1166,'CÁSSIA DOS COQUEIROS',3510906,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1167,'CASSILÂNDIA',5002902,12);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1168,'CASTANHAL',1502400,14);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1169,'CASTANHEIRA',5102850,11);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1170,'CASTANHEIRAS',1100908,22);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1171,'CASTELÂNDIA',5205059,9);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1172,'CASTELO',3201407,8);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1173,'CASTELO DO PIAUÍ',2202604,18);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1174,'CASTILHO',3511003,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1175,'CASTRO',4104907,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1176,'CASTRO ALVES',2907301,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1177,'CATAGUASES',3115300,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1178,'CATALÃO',5205109,9);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1179,'CATANDUVA',3511102,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1180,'CATANDUVAS',4105003,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1181,'CATANDUVAS',4204004,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1182,'CATARINA',2303600,6);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1183,'CATAS ALTAS',3115359,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1184,'CATAS ALTAS DA NORUEGA',3115409,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1185,'CATENDE',2604205,17);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1186,'CATIGUÁ',3511201,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1187,'CATINGUEIRA',2504207,15);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1188,'CATOLÂNDIA',2907400,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1189,'CATOLÉ DO ROCHA',2504306,15);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1190,'CATU',2907509,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1191,'CATUÍPE',4305009,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1192,'CATUJI',3115458,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1193,'CATUNDA',2303659,6);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1194,'CATURAÍ',5205208,9);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1195,'CATURAMA',2907558,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1196,'CATURITÉ',2504355,15);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1197,'CATUTI',3115474,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1198,'CAUCAIA',2303709,6);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1199,'CAVALCANTE',5205307,9);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1200,'CAXAMBU',3115508,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1201,'CAXAMBU DO SUL',4204103,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1202,'CAXIAS',2103000,10);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1203,'CAXIAS DO SUL',4305108,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1204,'CAXINGÓ',2202653,18);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1205,'CEARÁ-MIRIM',2402600,20);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1206,'CEDRAL',3511300,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1207,'CEDRAL',2103109,10);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1208,'CEDRO',2604304,17);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1209,'CEDRO',2303808,6);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1210,'CEDRO DE SÃO JOÃO',2801603,26);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1211,'CEDRO DO ABAETÉ',3115607,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1212,'CELSO RAMOS',4204152,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1213,'CENTENÁRIO',1704105,27);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1214,'CENTENÁRIO',4305116,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1215,'CENTENÁRIO DO SUL',4105102,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1216,'CENTRAL',2907608,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1217,'CENTRAL DE MINAS',3115706,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1218,'CENTRAL DO MARANHÃO',2103125,10);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1219,'CENTRALINA',3115805,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1220,'CENTRO DO GUILHERME',2103158,10);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1221,'CENTRO NOVO DO MARANHÃO',2103174,10);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1222,'CEREJEIRAS',1100056,22);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1223,'CERES',5205406,9);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1224,'CERQUEIRA CÉSAR',3511409,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1225,'CERQUILHO',3511508,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1226,'CERRITO',4305124,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1227,'CERRO AZUL',4105201,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1228,'CERRO BRANCO',4305132,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1229,'CERRO CORÁ',2402709,20);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1230,'CERRO GRANDE',4305157,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1231,'CERRO GRANDE DO SUL',4305173,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1232,'CERRO LARGO',4305207,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1233,'CERRO NEGRO',4204178,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1234,'CESÁRIO LANGE',3511607,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1235,'CÉU AZUL',4105300,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1236,'CEZARINA',5205455,9);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1237,'CHÃ DE ALEGRIA',2604403,17);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1238,'CHÃ GRANDE',2604502,17);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1239,'CHÃ PRETA',2701902,2);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1240,'CHÁCARA',3115904,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1241,'CHALÉ',3116001,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1242,'CHAPADA',4305306,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1243,'CHAPADA DA NATIVIDADE',1705102,27);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1244,'CHAPADA DE AREIA',1704600,27);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1245,'CHAPADA DO NORTE',3116100,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1246,'CHAPADA DOS GUIMARÃES',5103007,11);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1247,'CHAPADA GAÚCHA',3116159,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1248,'CHAPADÃO DO CÉU',5205471,9);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1249,'CHAPADÃO DO LAGEADO',4204194,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1250,'CHAPADÃO DO SUL',5002951,12);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1251,'CHAPADINHA',2103208,10);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1252,'CHAPECÓ',4204202,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1253,'CHARQUEADA',3511706,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1254,'CHARQUEADAS',4305355,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1255,'CHARRUA',4305371,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1256,'CHAVAL',2303907,6);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1257,'CHAVANTES',3557204,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1258,'CHAVES',1502509,14);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1259,'CHIADOR',3116209,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1260,'CHIAPETA',4305405,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1261,'CHOPINZINHO',4105409,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1262,'CHORÓ',2303931,6);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1263,'CHOROZINHO',2303956,6);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1264,'CHORROCHÓ',2907707,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1265,'CHUÍ',4305439,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1266,'CHUPINGUAIA',1100924,22);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1267,'CHUVISCA',4305447,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1268,'CIANORTE',4105508,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1269,'CÍCERO DANTAS',2907806,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1270,'CIDADE GAÚCHA',4105607,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1271,'CIDADE OCIDENTAL',5205497,9);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1272,'CIDELÂNDIA',2103257,10);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1273,'CIDREIRA',4305454,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1274,'CIPÓ',2907905,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1275,'CIPOTÂNEA',3116308,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1276,'CIRÍACO',4305504,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1277,'CLARAVAL',3116407,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1278,'CLARO DOS POÇÕES',3116506,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1279,'CLÁUDIA',5103056,11);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1280,'CLÁUDIO',3116605,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1281,'CLEMENTINA',3511904,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1282,'CLEVELÂNDIA',4105706,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1283,'COARACI',2908002,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1284,'COARI',1301209,4);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1285,'COCAL',2202703,18);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1286,'COCAL DE TELHA',2202711,18);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1287,'COCAL DO SUL',4204251,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1288,'COCAL DOS ALVES',2202729,18);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1289,'COCALINHO',5103106,11);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1290,'COCALZINHO DE GOIÁS',5205513,9);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1291,'COCOS',2908101,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1292,'CODAJÁS',1301308,4);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1293,'CODÓ',2103307,10);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1294,'COELHO NETO',2103406,10);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1295,'COIMBRA',3116704,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1296,'COITÉ DO NÓIA',2702009,2);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1297,'COIVARAS',2202737,18);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1298,'COLARES',1502608,14);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1299,'COLATINA',3201506,8);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1300,'COLÍDER',5103205,11);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1301,'COLINA',3512001,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1302,'COLINAS',4305587,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1303,'COLINAS',2103505,10);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1304,'COLINAS DO SUL',5205521,9);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1305,'COLINAS DO TOCANTINS',1705508,27);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1306,'COLMÉIA',1716703,27);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1307,'COLNIZA',5103254,11);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1308,'COLÔMBIA',3512100,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1309,'COLOMBO',4105805,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1310,'COLÔNIA DO GURGUÉIA',2202752,18);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1311,'COLÔNIA DO PIAUÍ',2202778,18);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1312,'COLÔNIA LEOPOLDINA',2702108,2);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1313,'COLORADO',4305603,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1314,'COLORADO',4105904,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1315,'COLORADO DO OESTE',1100064,22);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1316,'COLUNA',3116803,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1317,'COMBINADO',1705557,27);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1318,'COMENDADOR GOMES',3116902,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1319,'COMENDADOR LEVY GASPARIAN',3300951,19);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1320,'COMERCINHO',3117009,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1321,'COMODORO',5103304,11);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1322,'CONCEIÇÃO',2504405,15);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1323,'CONCEIÇÃO DA APARECIDA',3117108,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1324,'CONCEIÇÃO DA BARRA',3201605,8);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1325,'CONCEIÇÃO DA BARRA DE MINAS',3115201,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1326,'CONCEIÇÃO DA FEIRA',2908200,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1327,'CONCEIÇÃO DAS ALAGOAS',3117306,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1328,'CONCEIÇÃO DAS PEDRAS',3117207,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1329,'CONCEIÇÃO DE IPANEMA',3117405,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1330,'CONCEIÇÃO DE MACABU',3301405,19);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1331,'CONCEIÇÃO DO ALMEIDA',2908309,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1332,'CONCEIÇÃO DO ARAGUAIA',1502707,14);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1333,'CONCEIÇÃO DO CANINDÉ',2202802,18);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1334,'CONCEIÇÃO DO CASTELO',3201704,8);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1335,'CONCEIÇÃO DO COITÉ',2908408,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1336,'CONCEIÇÃO DO JACUÍPE',2908507,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1337,'CONCEIÇÃO DO LAGO-AÇU',2103554,10);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1338,'CONCEIÇÃO DO MATO DENTRO',3117504,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1339,'CONCEIÇÃO DO PARÁ',3117603,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1340,'CONCEIÇÃO DO RIO VERDE',3117702,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1341,'CONCEIÇÃO DO TOCANTINS',1705607,27);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1342,'CONCEIÇÃO DOS OUROS',3117801,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1343,'CONCHAL',3512209,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1344,'CONCHAS',3512308,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1345,'CONCÓRDIA',4204301,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1346,'CONCÓRDIA DO PARÁ',1502756,14);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1347,'CONDADO',2604601,17);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1348,'CONDADO',2504504,15);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1349,'CONDE',2908606,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1350,'CONDE',2504603,15);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1351,'CONDEÚBA',2908705,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1352,'CONDOR',4305702,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1353,'CÔNEGO MARINHO',3117836,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1354,'CONFINS',3117876,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1355,'CONFRESA',5103353,11);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1356,'CONGO',2504702,15);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1357,'CONGONHAL',3117900,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1358,'CONGONHAS',3118007,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1359,'CONGONHAS DO NORTE',3118106,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1360,'CONGONHINHAS',4106001,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1361,'CONQUISTA',3118205,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1362,'CONQUISTA D''OESTE',5103361,11);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1363,'CONSELHEIRO LAFAIETE',3118304,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1364,'CONSELHEIRO MAIRINCK',4106100,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1365,'CONSELHEIRO PENA',3118403,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1366,'CONSOLAÇÃO',3118502,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1367,'CONSTANTINA',4305801,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1368,'CONTAGEM',3118601,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1369,'CONTENDA',4106209,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1370,'CONTENDAS DO SINCORÁ',2908804,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1371,'COQUEIRAL',3118700,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1372,'COQUEIRO BAIXO',4305835,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1373,'COQUEIRO SECO',2702207,2);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1374,'COQUEIROS DO SUL',4305850,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1375,'CORAÇÃO DE JESUS',3118809,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1376,'CORAÇÃO DE MARIA',2908903,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1377,'CORBÉLIA',4106308,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1378,'CORDEIRO',3301504,19);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1379,'CORDEIRÓPOLIS',3512407,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1380,'CORDEIROS',2909000,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1381,'CORDILHEIRA ALTA',4204350,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1382,'CORDISBURGO',3118908,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1383,'CORDISLÂNDIA',3119005,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1384,'COREAÚ',2304004,6);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1385,'COREMAS',2504801,15);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1386,'CORGUINHO',5003108,12);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1387,'CORIBE',2909109,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1388,'CORINTO',3119104,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1389,'CORNÉLIO PROCÓPIO',4106407,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1390,'COROACI',3119203,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1391,'COROADOS',3512506,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1392,'COROATÁ',2103604,10);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1393,'COROMANDEL',3119302,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1394,'CORONEL BARROS',4305871,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1395,'CORONEL BICACO',4305900,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1396,'CORONEL DOMINGOS SOARES',4106456,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1397,'CORONEL EZEQUIEL',2402808,20);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1398,'CORONEL FABRICIANO',3119401,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1399,'CORONEL FREITAS',4204400,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1400,'CORONEL JOÃO PESSOA',2402907,20);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1401,'CORONEL JOÃO SÁ',2909208,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1402,'CORONEL JOSÉ DIAS',2202851,18);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1403,'CORONEL MACEDO',3512605,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1404,'CORONEL MARTINS',4204459,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1405,'CORONEL MURTA',3119500,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1406,'CORONEL PACHECO',3119609,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1407,'CORONEL PILAR',4305934,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1408,'CORONEL SAPUCAIA',5003157,12);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1409,'CORONEL VIVIDA',4106506,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1410,'CORONEL XAVIER CHAVES',3119708,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1411,'CÓRREGO DANTA',3119807,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1412,'CÓRREGO DO BOM JESUS',3119906,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1413,'CÓRREGO DO OURO',5205703,9);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1414,'CÓRREGO FUNDO',3119955,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1415,'CÓRREGO NOVO',3120003,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1416,'CORREIA PINTO',4204558,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1417,'CORRENTE',2202901,18);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1418,'CORRENTES',2604700,17);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1419,'CORRENTINA',2909307,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1420,'CORTÊS',2604809,17);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1421,'CORUMBÁ',5003207,12);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1422,'CORUMBÁ DE GOIÁS',5205802,9);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1423,'CORUMBAÍBA',5205901,9);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1424,'CORUMBATAÍ',3512704,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1425,'CORUMBATAÍ DO SUL',4106555,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1426,'CORUMBIARA',1100072,22);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1427,'CORUPÁ',4204509,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1428,'CORURIPE',2702306,2);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1429,'COSMÓPOLIS',3512803,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1430,'COSMORAMA',3512902,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1431,'COSTA MARQUES',1100080,22);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1432,'COSTA RICA',5003256,12);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1433,'COTEGIPE',2909406,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1434,'COTIA',3513009,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1435,'COTIPORÃ',4305959,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1436,'COTRIGUAÇU',5103379,11);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1437,'COUTO DE MAGALHÃES',1706001,27);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1438,'COUTO DE MAGALHÃES DE MINAS',3120102,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1439,'COXILHA',4305975,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1440,'COXIM',5003306,12);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1441,'COXIXOLA',2504850,15);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1442,'CRAÍBAS',2702355,2);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1443,'CRATEÚS',2304103,6);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1444,'CRATO',2304202,6);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1445,'CRAVINHOS',3513108,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1446,'CRAVOLÂNDIA',2909505,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1447,'CRICIÚMA',4204608,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1448,'CRISÓLITA',3120151,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1449,'CRISÓPOLIS',2909604,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1450,'CRISSIUMAL',4306007,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1451,'CRISTAIS',3120201,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1452,'CRISTAIS PAULISTA',3513207,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1453,'CRISTAL',4306056,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1454,'CRISTAL DO SUL',4306072,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1455,'CRISTALÂNDIA',1706100,27);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1456,'CRISTALÂNDIA DO PIAUÍ',2203008,18);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1457,'CRISTÁLIA',3120300,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1458,'CRISTALINA',5206206,9);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1459,'CRISTIANO OTONI',3120409,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1460,'CRISTIANÓPOLIS',5206305,9);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1461,'CRISTINA',3120508,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1462,'CRISTINÁPOLIS',2801702,26);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1463,'CRISTINO CASTRO',2203107,18);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1464,'CRISTÓPOLIS',2909703,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1465,'CRIXÁS',5206404,9);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1466,'CRIXÁS DO TOCANTINS',1706258,27);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1467,'CROATÁ',2304236,6);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1468,'CROMÍNIA',5206503,9);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1469,'CRUCILÂNDIA',3120607,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1470,'CRUZ',2304251,6);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1471,'CRUZ ALTA',4306106,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1472,'CRUZ DAS ALMAS',2909802,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1473,'CRUZ DO ESPÍRITO SANTO',2504900,15);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1474,'CRUZ MACHADO',4106803,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1475,'CRUZÁLIA',3513306,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1476,'CRUZALTENSE',4306130,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1477,'CRUZEIRO',3513405,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1478,'CRUZEIRO DA FORTALEZA',3120706,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1479,'CRUZEIRO DO IGUAÇU',4106571,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1480,'CRUZEIRO DO OESTE',4106605,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1481,'CRUZEIRO DO SUL',4306205,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1482,'CRUZEIRO DO SUL',4106704,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1483,'CRUZEIRO DO SUL',1200203,1);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1484,'CRUZETA',2403004,20);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1485,'CRUZÍLIA',3120805,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1486,'CRUZMALTINA',4106852,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1487,'CUBATÃO',3513504,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1488,'CUBATI',2505006,15);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1489,'CUIABÁ',5103403,11);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1490,'CUITÉ',2505105,15);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1491,'CUITÉ DE MAMANGUAPE',2505238,15);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1492,'CUITEGI',2505204,15);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1493,'CUJUBIM',1100940,22);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1494,'CUMARI',5206602,9);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1495,'CUMARU',2604908,17);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1496,'CUMARU DO NORTE',1502764,14);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1497,'CUMBE',2801900,26);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1498,'CUNHA',3513603,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1499,'CUNHA PORÃ',4204707,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1500,'CUNHATAÍ',4204756,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1501,'CUPARAQUE',3120839,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1502,'CUPIRA',2605004,17);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1503,'CURAÇÁ',2909901,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1504,'CURIMATÁ',2203206,18);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1505,'CURIONÓPOLIS',1502772,14);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1506,'CURITIBA',4106902,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1507,'CURITIBANOS',4204806,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1508,'CURIÚVA',4107009,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1509,'CURRAIS',2203230,18);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1510,'CURRAIS NOVOS',2403103,20);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1511,'CURRAL DE CIMA',2505279,15);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1512,'CURRAL DE DENTRO',3120870,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1513,'CURRAL NOVO DO PIAUÍ',2203271,18);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1514,'CURRAL VELHO',2505303,15);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1515,'CURRALINHO',1502806,14);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1516,'CURRALINHOS',2203255,18);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1517,'CURUÁ',1502855,14);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1518,'CURUÇÁ',1502905,14);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1519,'CURURUPU',2103703,10);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1520,'CURVELO',3120904,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1521,'CURVELVÂNDIA',5103437,11);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1522,'CUSTÓDIA',2605103,17);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1523,'CUTIAS',1600212,3);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1524,'DAMIANÓPOLIS',5206701,9);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1525,'DAMIÃO',2505352,15);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1526,'DAMOLÂNDIA',5206800,9);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1527,'DARCINÓPOLIS',1706506,27);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1528,'DÁRIO MEIRA',2910008,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1529,'DATAS',3121001,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1530,'DAVID CANABARRO',4306304,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1531,'DAVINÓPOLIS',5206909,9);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1532,'DAVINÓPOLIS',2103752,10);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1533,'DELFIM MOREIRA',3121100,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1534,'DELFINÓPOLIS',3121209,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1535,'DELMIRO GOUVEIA',2702405,2);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1536,'DELTA',3121258,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1537,'DEMERVAL LOBÃO',2203305,18);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1538,'DENISE',5103452,11);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1539,'DEODÁPOLIS',5003454,12);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1540,'DEPUTADO IRAPUAN PINHEIRO',2304269,6);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1541,'DERRUBADAS',4306320,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1542,'DESCALVADO',3513702,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1543,'DESCANSO',4204905,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1544,'DESCOBERTO',3121308,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1545,'DESTERRO',2505402,15);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1546,'DESTERRO DE ENTRE RIOS',3121407,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1547,'DESTERRO DO MELO',3121506,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1548,'DEZESSEIS DE NOVEMBRO',4306353,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1549,'DIADEMA',3513801,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1550,'DIAMANTE',2505600,15);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1551,'DIAMANTE D''OESTE',4107157,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1552,'DIAMANTE DO NORTE',4107108,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1553,'DIAMANTE DO SUL',4107124,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1554,'DIAMANTINA',3121605,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1555,'DIAMANTINO',5103502,11);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1556,'DIANÓPOLIS',1707009,27);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1557,'DIAS D''ÁVILA',2910057,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1558,'DILERMANDO DE AGUIAR',4306379,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1559,'DIOGO DE VASCONCELOS',3121704,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1560,'DIONÍSIO',3121803,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1561,'DIONÍSIO CERQUEIRA',4205001,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1562,'DIORAMA',5207105,9);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1563,'DIRCE REIS',3513850,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1564,'DIRCEU ARCOVERDE',2203354,18);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1565,'DIVINA PASTORA',2802007,26);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1566,'DIVINÉSIA',3121902,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1567,'DIVINO',3122009,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1568,'DIVINO DAS LARANJEIRAS',3122108,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1569,'DIVINO DE SÃO LOURENÇO',3201803,8);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1570,'DIVINOLÂNDIA',3513900,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1571,'DIVINOLÂNDIA DE MINAS',3122207,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1572,'DIVINÓPOLIS',3122306,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1573,'DIVINÓPOLIS DE GOIÁS',5208301,9);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1574,'DIVINÓPOLIS DO TOCANTINS',1707108,27);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1575,'DIVISA ALEGRE',3122355,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1576,'DIVISA NOVA',3122405,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1577,'DIVISÓPOLIS',3122454,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1578,'DOBRADA',3514007,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1579,'DOIS CÓRREGOS',3514106,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1580,'DOIS IRMÃOS',4306403,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1581,'DOIS IRMÃOS DAS MISSÕES',4306429,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1582,'DOIS IRMÃOS DO BURITI',5003488,12);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1583,'DOIS IRMÃOS DO TOCANTINS',1707207,27);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1584,'DOIS LAJEADOS',4306452,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1585,'DOIS RIACHOS',2702504,2);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1586,'DOIS VIZINHOS',4107207,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1587,'DOLCINÓPOLIS',3514205,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1588,'DOM AQUINO',5103601,11);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1589,'DOM BASÍLIO',2910107,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1590,'DOM BOSCO',3122470,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1591,'DOM CAVATI',3122504,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1592,'DOM ELISEU',1502939,14);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1593,'DOM EXPEDITO LOPES',2203404,18);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1594,'DOM FELICIANO',4306502,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1595,'DOM INOCÊNCIO',2203453,18);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1596,'DOM JOAQUIM',3122603,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1597,'DOM MACEDO COSTA',2910206,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1598,'DOM PEDRITO',4306601,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1599,'DOM PEDRO',2103802,10);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1600,'DOM PEDRO DE ALCÂNTARA',4306551,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1601,'DOM SILVÉRIO',3122702,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1602,'DOM VIÇOSO',3122801,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1603,'DOMINGOS MARTINS',3201902,8);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1604,'DOMINGOS MOURÃO',2203420,18);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1605,'DONA EMMA',4205100,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1606,'DONA EUZÉBIA',3122900,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1607,'DONA FRANCISCA',4306700,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1608,'DONA INÊS',2505709,15);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1609,'DORES DE CAMPOS',3123007,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1610,'DORES DE GUANHÃES',3123106,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1611,'DORES DO INDAIÁ',3123205,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1612,'DORES DO RIO PRETO',3202009,8);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1613,'DORES DO TURVO',3123304,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1614,'DORESÓPOLIS',3123403,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1615,'DORMENTES',2605152,17);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1616,'DOURADINA',4107256,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1617,'DOURADINA',5003504,12);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1618,'DOURADO',3514304,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1619,'DOURADOQUARA',3123502,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1620,'DOURADOS',5003702,12);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1621,'DOUTOR CAMARGO',4107306,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1622,'DOUTOR MAURÍCIO CARDOSO',4306734,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1623,'DOUTOR PEDRINHO',4205159,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1624,'DOUTOR RICARDO',4306759,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1625,'DOUTOR SEVERIANO',2403202,20);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1626,'DOUTOR ULYSSES',4128633,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1627,'DOVERLÂNDIA',5207253,9);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1628,'DRACENA',3514403,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1629,'DUARTINA',3514502,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1630,'DUAS BARRAS',3301603,19);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1631,'DUAS ESTRADAS',2505808,15);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1632,'DUERÉ',1707306,27);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1633,'DUMONT',3514601,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1634,'DUQUE BACELAR',2103901,10);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1635,'DUQUE DE CAXIAS',3301702,19);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1636,'DURANDÉ',3123528,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1637,'ECHAPORÃ',3514700,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1638,'ECOPORANGA',3202108,8);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1639,'EDEALINA',5207352,9);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1640,'EDÉIA',5207402,9);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1641,'EIRUNEPÉ',1301407,4);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1642,'ELDORADO',3514809,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1643,'ELDORADO',5003751,12);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1644,'ELDORADO DO SUL',4306767,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1645,'ELDORADO DOS CARAJÁS',1502954,14);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1646,'ELESBÃO VELOSO',2203503,18);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1647,'ELIAS FAUSTO',3514908,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1648,'ELISEU MARTINS',2203602,18);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1649,'ELISIÁRIO',3514924,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1650,'ELÍSIO MEDRADO',2910305,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1651,'ELÓI MENDES',3123601,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1652,'EMAS',2505907,15);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1653,'EMBAÚBA',3514957,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1654,'EMBU',3515004,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1655,'EMBU-GUAÇU',3515103,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1656,'EMILIANÓPOLIS',3515129,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1657,'ENCANTADO',4306809,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1658,'ENCANTO',2403301,20);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1659,'ENCRUZILHADA',2910404,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1660,'ENCRUZILHADA DO SUL',4306908,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1661,'ENÉAS MARQUES',4107405,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1662,'ENGENHEIRO BELTRÃO',4107504,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1663,'ENGENHEIRO CALDAS',3123700,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1664,'ENGENHEIRO COELHO',3515152,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1665,'ENGENHEIRO NAVARRO',3123809,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1666,'ENGENHEIRO PAULO DE FRONTIN',3301801,19);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1667,'ENGENHO VELHO',4306924,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1668,'ENTRE FOLHAS',3123858,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1669,'ENTRE RIOS',2910503,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1670,'ENTRE RIOS',4205175,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1671,'ENTRE RIOS DE MINAS',3123908,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1672,'ENTRE RIOS DO OESTE',4107538,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1673,'ENTRE RIOS DO SUL',4306957,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1674,'ENTRE-IJUÍS',4306932,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1675,'ENVIRA',1301506,4);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1676,'EPITACIOLÂNDIA',1200252,1);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1677,'EQUADOR',2403400,20);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1678,'EREBANGO',4306973,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1679,'ERECHIM',4307005,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1680,'ERERÊ',2304277,6);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1681,'ÉRICO CARDOSO',2900504,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1682,'ERMO',4205191,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1683,'ERNESTINA',4307054,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1684,'ERVAL GRANDE',4307203,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1685,'ERVAL SECO',4307302,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1686,'ERVAL VELHO',4205209,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1687,'ERVÁLIA',3124005,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1688,'ESCADA',2605202,17);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1689,'ESMERALDA',4307401,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1690,'ESMERALDAS',3124104,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1691,'ESPERA FELIZ',3124203,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1692,'ESPERANÇA',2506004,15);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1693,'ESPERANÇA DO SUL',4307450,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1694,'ESPERANÇA NOVA',4107520,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1695,'ESPERANTINA',2203701,18);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1696,'ESPERANTINA',1707405,27);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1697,'ESPERANTINÓPOLIS',2104008,10);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1698,'ESPIGÃO ALTO DO IGUAÇU',4107546,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1699,'ESPIGÃO D''OESTE',1100098,22);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1700,'ESPINOSA',3124302,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1701,'ESPÍRITO SANTO',2403509,20);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1702,'ESPÍRITO SANTO DO DOURADO',3124401,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1703,'ESPÍRITO SANTO DO PINHAL',3515186,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1704,'ESPÍRITO SANTO DO TURVO',3515194,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1705,'ESPLANADA',2910602,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1706,'ESPUMOSO',4307500,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1707,'ESTAÇÃO',4307559,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1708,'ESTÂNCIA',2802106,26);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1709,'ESTÂNCIA VELHA',4307609,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1710,'ESTEIO',4307708,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1711,'ESTIVA',3124500,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1712,'ESTIVA GERBI',3557303,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1713,'ESTREITO',2104057,10);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1714,'ESTRELA',4307807,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1715,'ESTRELA D''OESTE',3515202,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1716,'ESTRELA DALVA',3124609,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1717,'ESTRELA DE ALAGOAS',2702553,2);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1718,'ESTRELA DO INDAIÁ',3124708,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1719,'ESTRELA DO NORTE',3515301,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1720,'ESTRELA DO NORTE',5207501,9);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1721,'ESTRELA DO SUL',3124807,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1722,'ESTRELA VELHA',4307815,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1723,'EUCLIDES DA CUNHA',2910701,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1724,'EUCLIDES DA CUNHA PAULISTA',3515350,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1725,'EUGÊNIO DE CASTRO',4307831,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1726,'EUGENÓPOLIS',3124906,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1727,'EUNÁPOLIS',2910727,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1728,'EUSÉBIO',2304285,6);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1729,'EWBANK DA CÂMARA',3125002,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1730,'EXTREMA',3125101,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1731,'EXTREMOZ',2403608,20);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1732,'EXU',2605301,17);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1733,'FAGUNDES',2506103,15);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1734,'FAGUNDES VARELA',4307864,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1735,'FAINA',5207535,9);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1736,'FAMA',3125200,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1737,'FARIA LEMOS',3125309,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1738,'FARIAS BRITO',2304301,6);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1739,'FARO',1503002,14);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1740,'FAROL',4107553,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1741,'FARROUPILHA',4307906,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1742,'FARTURA',3515400,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1743,'FARTURA DO PIAUÍ',2203750,18);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1744,'FÁTIMA',2910750,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1745,'FÁTIMA',1707553,27);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1746,'FÁTIMA DO SUL',5003801,12);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1747,'FAXINAL',4107603,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1748,'FAXINAL DO SOTURNO',4308003,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1749,'FAXINAL DOS GUEDES',4205308,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1750,'FAXINALZINHO',4308052,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1751,'FAZENDA NOVA',5207600,9);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1752,'FAZENDA RIO GRANDE',4107652,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1753,'FAZENDA VILANOVA',4308078,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1754,'FEIJÓ',1200302,1);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1755,'FEIRA DA MATA',2910776,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1756,'FEIRA DE SANTANA',2910800,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1757,'FEIRA GRANDE',2702603,2);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1758,'FEIRA NOVA',2802205,26);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1759,'FEIRA NOVA',2605400,17);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1760,'FEIRA NOVA DO MARANHÃO',2104073,10);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1761,'FELÍCIO DOS SANTOS',3125408,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1762,'FELIPE GUERRA',2403707,20);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1763,'FELISBURGO',3125606,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1764,'FELIXLÂNDIA',3125705,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1765,'FELIZ',4308102,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1766,'FELIZ DESERTO',2702702,2);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1767,'FELIZ NATAL',5103700,11);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1768,'FÊNIX',4107702,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1769,'FERNANDES PINHEIRO',4107736,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1770,'FERNANDES TOURINHO',3125804,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1771,'FERNANDO DE NORONHA',2605459,17);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1772,'FERNANDO FALCÃO',2104081,10);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1773,'FERNANDO PEDROZA',2403756,20);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1774,'FERNANDO PRESTES',3515608,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1775,'FERNANDÓPOLIS',3515509,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1776,'FERNÃO',3515657,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1777,'FERRAZ DE VASCONCELOS',3515707,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1778,'FERREIRA GOMES',1600238,3);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1779,'FERREIROS',2605509,17);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1780,'FERROS',3125903,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1781,'FERVEDOURO',3125952,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1782,'FIGUEIRA',4107751,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1783,'FIGUEIRÓPOLIS',1707652,27);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1784,'FIGUEIRÓPOLIS D''OESTE',5103809,11);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1785,'FILADÉLFIA',2910859,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1786,'FILADÉLFIA',1707702,27);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1787,'FIRMINO ALVES',2910909,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1788,'FIRMINÓPOLIS',5207808,9);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1789,'FLEXEIRAS',2702801,2);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1790,'FLOR DA SERRA DO SUL',4107850,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1791,'FLOR DO SERTÃO',4205357,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1792,'FLORA RICA',3515806,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1793,'FLORAÍ',4107801,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1794,'FLORÂNIA',2403806,20);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1795,'FLOREAL',3515905,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1796,'FLORES',2605608,17);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1797,'FLORES DA CUNHA',4308201,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1798,'FLORES DE GOIÁS',5207907,9);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1799,'FLORES DO PIAUÍ',2203800,18);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1800,'FLORESTA',4107900,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1801,'FLORESTA',2605707,17);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1802,'FLORESTA AZUL',2911006,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1803,'FLORESTA DO ARAGUAIA',1503044,14);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1804,'FLORESTA DO PIAUÍ',2203859,18);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1805,'FLORESTAL',3126000,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1806,'FLORESTÓPOLIS',4108007,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1807,'FLORIANO',2203909,18);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1808,'FLORIANO PEIXOTO',4308250,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1809,'FLORIANÓPOLIS',4205407,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1810,'FLÓRIDA',4108106,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1811,'FLÓRIDA PAULISTA',3516002,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1812,'FLORÍNIA',3516101,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1813,'FONTE BOA',1301605,4);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1814,'FONTOURA XAVIER',4308300,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1815,'FORMIGA',3126109,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1816,'FORMIGUEIRO',4308409,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1817,'FORMOSA',5208004,9);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1818,'FORMOSA DA SERRA NEGRA',2104099,10);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1819,'FORMOSA DO OESTE',4108205,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1820,'FORMOSA DO RIO PRETO',2911105,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1821,'FORMOSA DO SUL',4205431,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1822,'FORMOSO',5208103,9);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1823,'FORMOSO',3126208,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1824,'FORMOSO DO ARAGUAIA',1708205,27);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1825,'FORQUETINHA',4308433,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1826,'FORQUILHA',2304350,6);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1827,'FORQUILHINHA',4205456,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1828,'FORTALEZA',2304400,6);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1829,'FORTALEZA DE MINAS',3126307,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1830,'FORTALEZA DO TABOCÃO',1708254,27);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1831,'FORTALEZA DOS NOGUEIRAS',2104107,10);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1832,'FORTALEZA DOS VALOS',4308458,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1833,'FORTIM',2304459,6);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1834,'FORTUNA',2104206,10);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1835,'FORTUNA DE MINAS',3126406,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1836,'FOZ DO IGUAÇU',4108304,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1837,'FOZ DO JORDÃO',4108452,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1838,'FRAIBURGO',4205506,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1839,'FRANCA',3516200,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1840,'FRANCINÓPOLIS',2204006,18);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1841,'FRANCISCO ALVES',4108320,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1842,'FRANCISCO AYRES',2204105,18);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1843,'FRANCISCO BADARÓ',3126505,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1844,'FRANCISCO BELTRÃO',4108403,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1845,'FRANCISCO DANTAS',2403905,20);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1846,'FRANCISCO DUMONT',3126604,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1847,'FRANCISCO MACEDO',2204154,18);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1848,'FRANCISCO MORATO',3516309,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1849,'FRANCISCO SÁ',3126703,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1850,'FRANCISCO SANTOS',2204204,18);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1851,'FRANCISCÓPOLIS',3126752,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1852,'FRANCO DA ROCHA',3516408,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1853,'FRECHEIRINHA',2304509,6);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1854,'FREDERICO WESTPHALEN',4308508,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1855,'FREI GASPAR',3126802,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1856,'FREI INOCÊNCIO',3126901,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1857,'FREI LAGONEGRO',3126950,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1858,'FREI MARTINHO',2506202,15);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1859,'FREI MIGUELINHO',2605806,17);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1860,'FREI PAULO',2802304,26);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1861,'FREI ROGÉRIO',4205555,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1862,'FRONTEIRA',3127008,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1863,'FRONTEIRA DOS VALES',3127057,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1864,'FRONTEIRAS',2204303,18);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1865,'FRUTA DE LEITE',3127073,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1866,'FRUTAL',3127107,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1867,'FRUTUOSO GOMES',2404002,20);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1868,'FUNDÃO',3202207,8);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1869,'FUNILÂNDIA',3127206,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1870,'GABRIEL MONTEIRO',3516507,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1871,'GADO BRAVO',2506251,15);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1872,'GÁLIA',3516606,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1873,'GALILÉIA',3127305,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1874,'GALINHOS',2404101,20);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1875,'GALVÃO',4205605,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1876,'GAMELEIRA',2605905,17);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1877,'GAMELEIRA DE GOIÁS',5208152,9);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1878,'GAMELEIRAS',3127339,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1879,'GANDU',2911204,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1880,'GARANHUNS',2606002,17);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1881,'GARARU',2802403,26);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1882,'GARÇA',3516705,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1883,'GARIBALDI',4308607,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1884,'GAROPABA',4205704,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1885,'GARRAFÃO DO NORTE',1503077,14);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1886,'GARRUCHOS',4308656,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1887,'GARUVA',4205803,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1888,'GASPAR',4205902,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1889,'GASTÃO VIDIGAL',3516804,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1890,'GAÚCHA DO NORTE',5103858,11);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1891,'GAURAMA',4308706,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1892,'GAVIÃO',2911253,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1893,'GAVIÃO PEIXOTO',3516853,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1894,'GEMINIANO',2204352,18);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1895,'GENERAL CÂMARA',4308805,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1896,'GENERAL CARNEIRO',4108502,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1897,'GENERAL CARNEIRO',5103908,11);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1898,'GENERAL MAYNARD',2802502,26);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1899,'GENERAL SALGADO',3516903,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1900,'GENERAL SAMPAIO',2304608,6);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1901,'GENTIL',4308854,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1902,'GENTIO DO OURO',2911303,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1903,'GETULINA',3517000,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1904,'GETÚLIO VARGAS',4308904,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1905,'GILBUÉS',2204402,18);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1906,'GIRAU DO PONCIANO',2702900,2);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1907,'GIRUÁ',4309001,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1908,'GLAUCILÂNDIA',3127354,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1909,'GLICÉRIO',3517109,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1910,'GLÓRIA',2911402,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1911,'GLÓRIA D''OESTE',5103957,11);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1912,'GLÓRIA DE DOURADOS',5004007,12);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1913,'GLÓRIA DO GOITÁ',2606101,17);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1914,'GLORINHA',4309050,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1915,'GODOFREDO VIANA',2104305,10);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1916,'GODOY MOREIRA',4108551,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1917,'GOIABEIRA',3127370,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1918,'GOIANA',2606200,17);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1919,'GOIANÁ',3127388,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1920,'GOIANÁPOLIS',5208400,9);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1921,'GOIANDIRA',5208509,9);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1922,'GOIANÉSIA',5208608,9);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1923,'GOIANÉSIA DO PARÁ',1503093,14);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1924,'GOIÂNIA',5208707,9);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1925,'GOIANINHA',2404200,20);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1926,'GOIANIRA',5208806,9);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1927,'GOIANORTE',1708304,27);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1928,'GOIÁS',5208905,9);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1929,'GOIATINS',1709005,27);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1930,'GOIATUBA',5209101,9);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1931,'GOIOERÊ',4108601,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1932,'GOIOXIM',4108650,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1933,'GONÇALVES',3127404,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1934,'GONÇALVES DIAS',2104404,10);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1935,'GONGOGI',2911501,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1936,'GONZAGA',3127503,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1937,'GOUVÊA',3127602,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1938,'GOUVELÂNDIA',5209150,9);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1939,'GOVERNADOR ARCHER',2104503,10);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1940,'GOVERNADOR CELSO RAMOS',4206009,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1941,'GOVERNADOR DIX-SEPT ROSADO',2404309,20);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1942,'GOVERNADOR EDISON LOBÃO',2104552,10);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1943,'GOVERNADOR EUGÊNIO BARROS',2104602,10);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1944,'GOVERNADOR JORGE TEIXEIRA',1101005,22);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1945,'GOVERNADOR LINDENBERG',3202256,8);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1946,'GOVERNADOR LUIZ ROCHA',2104628,10);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1947,'GOVERNADOR MANGABEIRA',2911600,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1948,'GOVERNADOR NEWTON BELLO',2104651,10);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1949,'GOVERNADOR NUNES FREIRE',2104677,10);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1950,'GOVERNADOR VALADARES',3127701,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1951,'GRAÇA',2304657,6);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1952,'GRAÇA ARANHA',2104701,10);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1953,'GRACHO CARDOSO',2802601,26);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1954,'GRAJAÚ',2104800,10);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1955,'GRAMADO',4309100,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1956,'GRAMADO DOS LOUREIROS',4309126,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1957,'GRAMADO XAVIER',4309159,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1958,'GRANDES RIOS',4108700,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1959,'GRANITO',2606309,17);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1960,'GRANJA',2304707,6);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1961,'GRANJEIRO',2304806,6);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1962,'GRÃO MOGOL',3127800,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1963,'GRÃO PARÁ',4206108,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1964,'GRAVATÁ',2606408,17);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1965,'GRAVATAÍ',4309209,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1966,'GRAVATAL',4206207,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1967,'GROAÍRAS',2304905,6);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1968,'GROSSOS',2404408,20);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1969,'GRUPIARA',3127909,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1970,'GUABIJU',4309258,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1971,'GUABIRUBA',4206306,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1972,'GUAÇUÍ',3202306,8);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1973,'GUADALUPE',2204501,18);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1974,'GUAÍBA',4309308,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1975,'GUAIÇARA',3517208,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1976,'GUAIMBÊ',3517307,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1977,'GUAÍRA',4108809,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1978,'GUAÍRA',3517406,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1979,'GUAIRAÇÁ',4108908,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1980,'GUAIÚBA',2304954,6);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1981,'GUAJARÁ',1301654,4);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1982,'GUAJARÁ-MIRIM',1100106,22);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1983,'GUAJERU',2911659,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1984,'GUAMARÉ',2404507,20);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1985,'GUAMIRANGA',4108957,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1986,'GUANAMBI',2911709,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1987,'GUANHÃES',3128006,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1988,'GUAPÉ',3128105,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1989,'GUAPIAÇU',3517505,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1990,'GUAPIARA',3517604,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1991,'GUAPIMIRIM',3301850,19);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1992,'GUAPIRAMA',4109005,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1993,'GUAPÓ',5209200,9);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1994,'GUAPORÉ',4309407,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1995,'GUAPOREMA',4109104,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1996,'GUARÁ',3517703,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1997,'GUARABIRA',2506301,15);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1998,'GUARAÇAÍ',3517802,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (1999,'GUARACI',4109203,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2000,'GUARACI',3517901,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2001,'GUARACIABA',4206405,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2002,'GUARACIABA',3128204,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2003,'GUARACIABA DO NORTE',2305001,6);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2004,'GUARACIAMA',3128253,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2005,'GUARAÍ',1709302,27);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2006,'GUARAÍTA',5209291,9);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2007,'GUARAMIRANGA',2305100,6);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2008,'GUARAMIRIM',4206504,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2009,'GUARANÉSIA',3128303,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2010,'GUARANI',3128402,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2011,'GUARANI D''OESTE',3518008,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2012,'GUARANI DAS MISSÕES',4309506,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2013,'GUARANI DE GOIÁS',5209408,9);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2014,'GUARANIAÇU',4109302,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2015,'GUARANTÃ',3518107,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2016,'GUARANTÃ DO NORTE',5104104,11);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2017,'GUARAPARI',3202405,8);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2018,'GUARAPUAVA',4109401,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2019,'GUARAQUEÇABA',4109500,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2020,'GUARARÁ',3128501,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2021,'GUARARAPES',3518206,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2022,'GUARAREMA',3518305,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2023,'GUARATINGA',2911808,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2024,'GUARATINGUETÁ',3518404,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2025,'GUARATUBA',4109609,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2026,'GUARDA-MOR',3128600,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2027,'GUAREÍ',3518503,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2028,'GUARIBA',3518602,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2029,'GUARIBAS',2204550,18);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2030,'GUARINOS',5209457,9);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2031,'GUARUJÁ',3518701,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2032,'GUARUJÁ DO SUL',4206603,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2033,'GUARULHOS',3518800,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2034,'GUATAMBU',4206652,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2035,'GUATAPARÁ',3518859,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2036,'GUAXUPÉ',3128709,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2037,'GUIA LOPES DA LAGUNA',5004106,12);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2038,'GUIDOVAL',3128808,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2039,'GUIMARÃES',2104909,10);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2040,'GUIMARÂNIA',3128907,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2041,'GUIRATINGA',5104203,11);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2042,'GUIRICEMA',3129004,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2043,'GURINHATÃ',3129103,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2044,'GURINHÉM',2506400,15);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2045,'GURJÃO',2506509,15);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2046,'GURUPÁ',1503101,14);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2047,'GURUPI',1709500,27);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2048,'GUZOLÂNDIA',3518909,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2049,'HARMONIA',4309555,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2050,'HEITORAÍ',5209606,9);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2051,'HELIODORA',3129202,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2052,'HELIÓPOLIS',2911857,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2053,'HERCULÂNDIA',3519006,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2054,'HERVAL',4307104,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2055,'HERVAL D''OESTE',4206702,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2056,'HERVEIRAS',4309571,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2057,'HIDROLÂNDIA',5209705,9);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2058,'HIDROLÂNDIA',2305209,6);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2059,'HIDROLINA',5209804,9);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2060,'HOLAMBRA',3519055,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2061,'HONÓRIO SERPA',4109658,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2062,'HORIZONTE',2305233,6);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2063,'HORIZONTINA',4309605,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2064,'HORTOLÂNDIA',3519071,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2065,'HUGO NAPOLEÃO',2204600,18);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2066,'HULHA NEGRA',4309654,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2067,'HUMAITÁ',4309704,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2068,'HUMAITÁ',1301704,4);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2069,'HUMBERTO DE CAMPOS',2105005,10);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2070,'IACANGA',3519105,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2071,'IACIARA',5209903,9);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2072,'IACRI',3519204,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2073,'IAÇU',2911907,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2074,'IAPU',3129301,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2075,'IARAS',3519253,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2076,'IATI',2606507,17);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2077,'IBAITI',4109708,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2078,'IBARAMA',4309753,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2079,'IBARETAMA',2305266,6);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2080,'IBATÉ',3519303,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2081,'IBATEGUARA',2703007,2);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2082,'IBATIBA',3202454,8);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2083,'IBEMA',4109757,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2084,'IBERTIOGA',3129400,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2085,'IBIÁ',3129509,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2086,'IBIAÇÁ',4309803,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2087,'IBIAÍ',3129608,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2088,'IBIAM',4206751,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2089,'IBIAPINA',2305308,6);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2090,'IBIARA',2506608,15);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2091,'IBIASSUCÊ',2912004,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2092,'IBICARAÍ',2912103,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2093,'IBICARÉ',4206801,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2094,'IBICOARA',2912202,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2095,'IBICUÍ',2912301,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2096,'IBICUITINGA',2305332,6);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2097,'IBIMIRIM',2606606,17);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2098,'IBIPEBA',2912400,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2099,'IBIPITANGA',2912509,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2100,'IBIPORÃ',4109807,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2101,'IBIQUERA',2912608,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2102,'IBIRÁ',3519402,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2103,'IBIRACATU',3129657,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2104,'IBIRACI',3129707,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2105,'IBIRAÇU',3202504,8);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2106,'IBIRAIARAS',4309902,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2107,'IBIRAJUBA',2606705,17);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2108,'IBIRAMA',4206900,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2109,'IBIRAPITANGA',2912707,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2110,'IBIRAPUÃ',2912806,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2111,'IBIRAPUITÃ',4309951,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2112,'IBIRAREMA',3519501,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2113,'IBIRATAIA',2912905,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2114,'IBIRITÉ',3129806,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2115,'IBIRUBÁ',4310009,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2116,'IBITIARA',2913002,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2117,'IBITINGA',3519600,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2118,'IBITIRAMA',3202553,8);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2119,'IBITITÁ',2913101,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2120,'IBITIÚRA DE MINAS',3129905,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2121,'IBITURUNA',3130002,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2122,'IBIÚNA',3519709,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2123,'IBOTIRAMA',2913200,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2124,'ICAPUÍ',2305357,6);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2125,'IÇARA',4207007,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2126,'ICARAÍ DE MINAS',3130051,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2127,'ICARAÍMA',4109906,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2128,'ICATU',2105104,10);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2129,'ICÉM',3519808,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2130,'ICHU',2913309,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2131,'ICÓ',2305407,6);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2132,'ICONHA',3202603,8);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2133,'IELMO MARINHO',2404606,20);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2134,'IEPÊ',3519907,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2135,'IGACI',2703106,2);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2136,'IGAPORÃ',2913408,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2137,'IGARAÇU DO TIETÊ',3520004,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2138,'IGARACY',2502607,15);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2139,'IGARAPAVA',3520103,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2140,'IGARAPÉ',3130101,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2141,'IGARAPÉ DO MEIO',2105153,10);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2142,'IGARAPÉ GRANDE',2105203,10);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2143,'IGARAPÉ-AÇU',1503200,14);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2144,'IGARAPÉ-MIRI',1503309,14);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2145,'IGARASSU',2606804,17);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2146,'IGARATÁ',3520202,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2147,'IGARATINGA',3130200,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2148,'IGRAPIÚNA',2913457,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2149,'IGREJA NOVA',2703205,2);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2150,'IGREJINHA',4310108,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2151,'IGUABA GRANDE',3301876,19);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2152,'IGUAÍ',2913507,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2153,'IGUAPE',3520301,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2154,'IGUARACI',2606903,17);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2155,'IGUARAÇU',4110003,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2156,'IGUATAMA',3130309,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2157,'IGUATEMI',5004304,12);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2158,'IGUATU',4110052,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2159,'IGUATU',2305506,6);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2160,'IJACI',3130408,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2161,'IJUÍ',4310207,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2162,'ILHA COMPRIDA',3520426,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2163,'ILHA DAS FLORES',2802700,26);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2164,'ILHA GRANDE',2204659,18);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2165,'ILHA SOLTEIRA',3520442,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2166,'ILHABELA',3520400,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2167,'ILHÉUS',2913606,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2168,'ILHOTA',4207106,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2169,'ILICÍNEA',3130507,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2170,'ILÓPOLIS',4310306,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2171,'IMACULADA',2506707,15);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2172,'IMARUÍ',4207205,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2173,'IMBAÚ',4110078,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2174,'IMBÉ',4310330,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2175,'IMBÉ DE MINAS',3130556,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2176,'IMBITUBA',4207304,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2177,'IMBITUVA',4110102,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2178,'IMBUIA',4207403,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2179,'IMIGRANTE',4310363,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2180,'IMPERATRIZ',2105302,10);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2181,'INÁCIO MARTINS',4110201,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2182,'INACIOLÂNDIA',5209937,9);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2183,'INAJÁ',4110300,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2184,'INAJÁ',2607000,17);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2185,'INCONFIDENTES',3130606,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2186,'INDAIABIRA',3130655,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2187,'INDAIAL',4207502,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2188,'INDAIATUBA',3520509,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2189,'INDEPENDÊNCIA',4310405,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2190,'INDEPENDÊNCIA',2305605,6);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2191,'INDIANA',3520608,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2192,'INDIANÓPOLIS',4110409,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2193,'INDIANÓPOLIS',3130705,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2194,'INDIAPORÃ',3520707,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2195,'INDIARA',5209952,9);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2196,'INDIAROBA',2802809,26);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2197,'INDIAVAÍ',5104500,11);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2198,'INGÁ',2506806,15);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2199,'INGAÍ',3130804,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2200,'INGAZEIRA',2607109,17);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2201,'INHACORÁ',4310413,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2202,'INHAMBUPE',2913705,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2203,'INHANGAPI',1503408,14);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2204,'INHAPI',2703304,2);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2205,'INHAPIM',3130903,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2206,'INHAÚMAS',3131000,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2207,'INHUMA',2204709,18);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2208,'INHUMAS',5210000,9);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2209,'INIMUTABA',3131109,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2210,'INOCÊNCIA',5004403,12);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2211,'INÚBIA PAULISTA',3520806,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2212,'IOMERÊ',4207577,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2213,'IPABA',3131158,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2214,'IPAMERI',5210109,9);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2215,'IPANEMA',3131208,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2216,'IPANGUAÇU',2404705,20);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2217,'IPAPORANGA',2305654,6);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2218,'IPATINGA',3131307,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2219,'IPAUÇU',3520905,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2220,'IPAUMIRIM',2305704,6);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2221,'IPÊ',4310439,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2222,'IPECAETÁ',2913804,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2223,'IPERÓ',3521002,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2224,'IPEÚNA',3521101,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2225,'IPIAÇU',3131406,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2226,'IPIAÚ',2913903,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2227,'IPIGUÁ',3521150,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2228,'IPIRÁ',2914000,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2229,'IPIRA',4207601,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2230,'IPIRANGA',4110508,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2231,'IPIRANGA DE GOIÁS',5210158,9);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2232,'IPIRANGA DO PIAUÍ',2204808,18);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2233,'IPIRANGA DO SUL',4310462,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2234,'IPIXUNA',1301803,4);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2235,'IPIXUNA DO PARÁ',1503457,14);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2236,'IPOJUCA',2607208,17);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2237,'IPORÃ',4110607,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2238,'IPORÁ',5210208,9);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2239,'IPORÃ DO OESTE',4207650,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2240,'IPORANGA',3521200,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2241,'IPU',2305803,6);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2242,'IPUÃ',3521309,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2243,'IPUAÇU',4207684,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2244,'IPUBI',2607307,17);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2245,'IPUEIRA',2404804,20);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2246,'IPUEIRAS',2305902,6);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2247,'IPUEIRAS',1709807,27);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2248,'IPUIÚNA',3131505,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2249,'IPUMIRIM',4207700,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2250,'IPUPIARA',2914109,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2251,'IRACEMA',2306009,6);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2252,'IRACEMA',1400282,23);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2253,'IRACEMA DO OESTE',4110656,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2254,'IRACEMÁPOLIS',3521408,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2255,'IRACEMINHA',4207759,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2256,'IRAÍ',4310504,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2257,'IRAÍ DE MINAS',3131604,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2258,'IRAJUBA',2914208,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2259,'IRAMAIA',2914307,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2260,'IRANDUBA',1301852,4);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2261,'IRANI',4207809,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2262,'IRAPUÃ',3521507,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2263,'IRAPURU',3521606,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2264,'IRAQUARA',2914406,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2265,'IRARÁ',2914505,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2266,'IRATI',4110706,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2267,'IRATI',4207858,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2268,'IRAUÇUBA',2306108,6);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2269,'IRECÊ',2914604,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2270,'IRETAMA',4110805,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2271,'IRINEÓPOLIS',4207908,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2272,'IRITUIA',1503507,14);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2273,'IRUPI',3202652,8);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2274,'ISAÍAS COELHO',2204907,18);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2275,'ISRAELÂNDIA',5210307,9);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2276,'ITÁ',4208005,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2277,'ITAARA',4310538,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2278,'ITABAIANA',2802908,26);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2279,'ITABAIANA',2506905,15);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2280,'ITABAIANINHA',2803005,26);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2281,'ITABELA',2914653,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2282,'ITABERÁ',3521705,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2283,'ITABERABA',2914703,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2284,'ITABERAÍ',5210406,9);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2285,'ITABI',2803104,26);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2286,'ITABIRA',3131703,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2287,'ITABIRINHA DE MANTENA',3131802,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2288,'ITABIRITO',3131901,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2289,'ITABORAÍ',3301900,19);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2290,'ITABUNA',2914802,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2291,'ITACAJÁ',1710508,27);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2292,'ITACAMBIRA',3132008,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2293,'ITACARAMBI',3132107,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2294,'ITACARÉ',2914901,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2295,'ITACOATIARA',1301902,4);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2296,'ITACURUBA',2607406,17);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2297,'ITACURUBI',4310553,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2298,'ITAETÉ',2915007,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2299,'ITAGI',2915106,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2300,'ITAGIBÁ',2915205,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2301,'ITAGIMIRIM',2915304,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2302,'ITAGUAÇU',3202702,8);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2303,'ITAGUAÇU DA BAHIA',2915353,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2304,'ITAGUAÍ',3302007,19);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2305,'ITAGUAJÉ',4110904,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2306,'ITAGUARA',3132206,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2307,'ITAGUARI',5210562,9);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2308,'ITAGUARU',5210604,9);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2309,'ITAGUATINS',1710706,27);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2310,'ITAÍ',3521804,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2311,'ITAÍBA',2607505,17);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2312,'ITAIÇABA',2306207,6);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2313,'ITAINÓPOLIS',2205003,18);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2314,'ITAIÓPOLIS',4208104,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2315,'ITAIPAVA DO GRAJAÚ',2105351,10);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2316,'ITAIPÉ',3132305,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2317,'ITAIPULÂNDIA',4110953,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2318,'ITAITINGA',2306256,6);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2319,'ITAITUBA',1503606,14);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2320,'ITAJÁ',2404853,20);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2321,'ITAJÁ',5210802,9);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2322,'ITAJAÍ',4208203,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2323,'ITAJOBI',3521903,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2324,'ITAJU',3522000,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2325,'ITAJU DO COLÔNIA',2915403,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2326,'ITAJUBÁ',3132404,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2327,'ITAJUÍPE',2915502,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2328,'ITALVA',3302056,19);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2329,'ITAMARACÁ',2607604,17);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2330,'ITAMARAJU',2915601,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2331,'ITAMARANDIBA',3132503,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2332,'ITAMARATI',1301951,4);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2333,'ITAMARATI DE MINAS',3132602,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2334,'ITAMARI',2915700,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2335,'ITAMBACURI',3132701,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2336,'ITAMBARACÁ',4111001,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2337,'ITAMBÉ',2915809,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2338,'ITAMBÉ',4111100,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2339,'ITAMBÉ',2607653,17);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2340,'ITAMBÉ DO MATO DENTRO',3132800,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2341,'ITAMOGI',3132909,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2342,'ITAMONTE',3133006,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2343,'ITANAGRA',2915908,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2344,'ITANHAÉM',3522109,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2345,'ITANHANDU',3133105,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2346,'ITANHÉM',2916005,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2347,'ITANHOMI',3133204,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2348,'ITAOBIM',3133303,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2349,'ITAÓCA',3522158,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2350,'ITAOCARA',3302106,19);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2351,'ITAPACI',5210901,9);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2352,'ITAPAGÉ',2306306,6);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2353,'ITAPAGIPE',3133402,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2354,'ITAPARICA',2916104,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2355,'ITAPÉ',2916203,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2356,'ITAPEBI',2916302,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2357,'ITAPECERICA',3133501,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2358,'ITAPECERICA DA SERRA',3522208,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2359,'ITAPECURU MIRIM',2105401,10);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2360,'ITAPEJARA D''OESTE',4111209,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2361,'ITAPEMA',4208302,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2362,'ITAPEMIRIM',3202801,8);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2363,'ITAPERUÇU',4111258,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2364,'ITAPERUNA',3302205,19);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2365,'ITAPETIM',2607703,17);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2366,'ITAPETINGA',2916401,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2367,'ITAPETININGA',3522307,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2368,'ITAPEVA',3522406,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2369,'ITAPEVA',3133600,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2370,'ITAPEVI',3522505,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2371,'ITAPICURU',2916500,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2372,'ITAPIPOCA',2306405,6);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2373,'ITAPIRA',3522604,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2374,'ITAPIRANGA',4208401,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2375,'ITAPIRANGA',1302009,4);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2376,'ITAPIRAPUÃ',5211008,9);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2377,'ITAPIRAPUÃ PAULISTA',3522653,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2378,'ITAPIRATINS',1710904,27);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2379,'ITAPISSUMA',2607752,17);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2380,'ITAPITANGA',2916609,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2381,'ITAPIÚNA',2306504,6);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2382,'ITAPOÁ',4208450,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2383,'ITÁPOLIS',3522703,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2384,'ITAPORÃ',5004502,12);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2385,'ITAPORÃ DO TOCANTINS',1711100,27);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2386,'ITAPORANGA',2507002,15);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2387,'ITAPORANGA',3522802,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2388,'ITAPORANGA D''AJUDA',2803203,26);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2389,'ITAPOROROCA',2507101,15);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2390,'ITAPUÃ DO OESTE',1101104,22);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2391,'ITAPUCA',4310579,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2392,'ITAPUÍ',3522901,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2393,'ITAPURA',3523008,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2394,'ITAPURANGA',5211206,9);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2395,'ITAQUAQUECETUBA',3523107,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2396,'ITAQUARA',2916708,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2397,'ITAQUI',4310603,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2398,'ITAQUIRAÍ',5004601,12);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2399,'ITAQUITINGA',2607802,17);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2400,'ITARANA',3202900,8);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2401,'ITARANTIM',2916807,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2402,'ITARARÉ',3523206,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2403,'ITAREMA',2306553,6);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2404,'ITARIRI',3523305,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2405,'ITARUMÃ',5211305,9);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2406,'ITATI',4310652,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2407,'ITATIAIA',3302254,19);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2408,'ITATIAIUÇU',3133709,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2409,'ITATIBA',3523404,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2410,'ITATIBA DO SUL',4310702,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2411,'ITATIM',2916856,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2412,'ITATINGA',3523503,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2413,'ITATIRA',2306603,6);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2414,'ITATUBA',2507200,15);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2415,'ITAÚ',2404903,20);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2416,'ITAÚ DE MINAS',3133758,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2417,'ITAÚBA',5104559,11);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2418,'ITAUBAL',1600253,3);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2419,'ITAUÇU',5211404,9);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2420,'ITAUEIRA',2205102,18);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2421,'ITAÚNA',3133808,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2422,'ITAÚNA DO SUL',4111308,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2423,'ITAVERAVA',3133907,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2424,'ITINGA',3134004,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2425,'ITINGA DO MARANHÃO',2105427,10);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2426,'ITIQUIRA',5104609,11);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2427,'ITIRAPINA',3523602,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2428,'ITIRAPUÃ',3523701,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2429,'ITIRUÇU',2916906,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2430,'ITIÚBA',2917003,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2431,'ITOBI',3523800,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2432,'ITORORÓ',2917102,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2433,'ITU',3523909,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2434,'ITUAÇU',2917201,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2435,'ITUBERÁ',2917300,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2436,'ITUETA',3134103,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2437,'ITUIUTABA',3134202,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2438,'ITUMBIARA',5211503,9);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2439,'ITUMIRIM',3134301,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2440,'ITUPEVA',3524006,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2441,'ITUPIRANGA',1503705,14);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2442,'ITUPORANGA',4208500,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2443,'ITURAMA',3134400,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2444,'ITUTINGA',3134509,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2445,'ITUVERAVA',3524105,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2446,'IUIÚ',2917334,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2447,'IÚNA',3203007,8);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2448,'IVAÍ',4111407,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2449,'IVAIPORÃ',4111506,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2450,'IVATÉ',4111555,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2451,'IVATUBA',4111605,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2452,'IVINHEMA',5004700,12);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2453,'IVOLÂNDIA',5211602,9);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2454,'IVORÁ',4310751,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2455,'IVOTI',4310801,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2456,'JABOATÃO DOS GUARARAPES',2607901,17);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2457,'JABORÁ',4208609,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2458,'JABORANDI',2917359,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2459,'JABORANDI',3524204,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2460,'JABOTI',4111704,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2461,'JABOTICABA',4310850,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2462,'JABOTICABAL',3524303,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2463,'JABOTICATUBAS',3134608,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2464,'JAÇANÃ',2405009,20);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2465,'JACARACI',2917409,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2466,'JACARAÚ',2507309,15);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2467,'JACARÉ DOS HOMENS',2703403,2);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2468,'JACAREACANGA',1503754,14);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2469,'JACAREÍ',3524402,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2470,'JACAREZINHO',4111803,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2471,'JACI',3524501,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2472,'JACIARA',5104807,11);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2473,'JACINTO',3134707,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2474,'JACINTO MACHADO',4208708,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2475,'JACOBINA',2917508,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2476,'JACOBINA DO PIAUÍ',2205151,18);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2477,'JACUÍ',3134806,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2478,'JACUÍPE',2703502,2);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2479,'JACUIZINHO',4310876,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2480,'JACUNDÁ',1503804,14);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2481,'JACUPIRANGA',3524600,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2482,'JACUTINGA',4310900,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2483,'JACUTINGA',3134905,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2484,'JAGUAPITÃ',4111902,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2485,'JAGUAQUARA',2917607,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2486,'JAGUARAÇU',3135001,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2487,'JAGUARÃO',4311007,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2488,'JAGUARARI',2917706,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2489,'JAGUARÉ',3203056,8);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2490,'JAGUARETAMA',2306702,6);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2491,'JAGUARI',4311106,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2492,'JAGUARIAÍVA',4112009,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2493,'JAGUARIBARA',2306801,6);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2494,'JAGUARIBE',2306900,6);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2495,'JAGUARIPE',2917805,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2496,'JAGUARIÚNA',3524709,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2497,'JAGUARUANA',2307007,6);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2498,'JAGUARUNA',4208807,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2499,'JAÍBA',3135050,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2500,'JAICÓS',2205201,18);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2501,'JALES',3524808,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2502,'JAMBEIRO',3524907,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2503,'JAMPRUCA',3135076,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2504,'JANAÚBA',3135100,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2505,'JANDAIA',5211701,9);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2506,'JANDAIA DO SUL',4112108,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2507,'JANDAÍRA',2917904,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2508,'JANDAÍRA',2405108,20);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2509,'JANDIRA',3525003,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2510,'JANDUÍS',2405207,20);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2511,'JANGADA',5104906,11);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2512,'JANIÓPOLIS',4112207,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2513,'JANUÁRIA',3135209,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2514,'JANUÁRIO CICCO',2405306,20);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2515,'JAPARAÍBA',3135308,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2516,'JAPARATINGA',2703601,2);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2517,'JAPARATUBA',2803302,26);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2518,'JAPERI',3302270,19);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2519,'JAPI',2405405,20);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2520,'JAPIRA',4112306,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2521,'JAPOATÃ',2803401,26);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2522,'JAPONVAR',3135357,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2523,'JAPORÃ',5004809,12);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2524,'JAPURÁ',4112405,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2525,'JAPURÁ',1302108,4);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2526,'JAQUEIRA',2607950,17);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2527,'JAQUIRANA',4311122,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2528,'JARAGUÁ',5211800,9);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2529,'JARAGUÁ DO SUL',4208906,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2530,'JARAGUARI',5004908,12);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2531,'JARAMATAIA',2703700,2);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2532,'JARDIM',2307106,6);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2533,'JARDIM',5005004,12);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2534,'JARDIM ALEGRE',4112504,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2535,'JARDIM DE ANGICOS',2405504,20);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2536,'JARDIM DE PIRANHAS',2405603,20);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2537,'JARDIM DO MULATO',2205250,18);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2538,'JARDIM DO SERIDÓ',2405702,20);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2539,'JARDIM OLINDA',4112603,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2540,'JARDINÓPOLIS',4208955,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2541,'JARDINÓPOLIS',3525102,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2542,'JARI',4311130,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2543,'JARINU',3525201,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2544,'JARU',1100114,22);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2545,'JATAÍ',5211909,9);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2546,'JATAIZINHO',4112702,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2547,'JATAÚBA',2608008,17);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2548,'JATEÍ',5005103,12);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2549,'JATI',2307205,6);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2550,'JATOBÁ',2608057,17);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2551,'JATOBÁ',2105450,10);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2552,'JATOBÁ DO PIAUÍ',2205276,18);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2553,'JAÚ',3525300,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2554,'JAÚ DO TOCANTINS',1711506,27);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2555,'JAUPACI',5212006,9);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2556,'JAURU',5105002,11);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2557,'JECEABA',3135407,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2558,'JENIPAPO DE MINAS',3135456,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2559,'JENIPAPO DOS VIEIRAS',2105476,10);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2560,'JEQUERI',3135506,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2561,'JEQUIÁ DA PRAIA',2703759,2);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2562,'JEQUIÉ',2918001,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2563,'JEQUITAÍ',3135605,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2564,'JEQUITIBÁ',3135704,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2565,'JEQUITINHONHA',3135803,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2566,'JEREMOABO',2918100,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2567,'JERICÓ',2507408,15);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2568,'JERIQUARA',3525409,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2569,'JERÔNIMO MONTEIRO',3203106,8);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2570,'JERUMENHA',2205300,18);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2571,'JESUÂNIA',3135902,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2572,'JESUÍTAS',4112751,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2573,'JESÚPOLIS',5212055,9);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2574,'JI-PARANÁ',1100122,22);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2575,'JIJOCA DE JERICOACOARA',2307254,6);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2576,'JIQUIRIÇÁ',2918209,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2577,'JITAÚNA',2918308,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2578,'JOAÇABA',4209003,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2579,'JOAÍMA',3136009,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2580,'JOANÉSIA',3136108,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2581,'JOANÓPOLIS',3525508,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2582,'JOÃO ALFREDO',2608107,17);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2583,'JOÃO CÂMARA',2405801,20);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2584,'JOÃO COSTA',2205359,18);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2585,'JOÃO DIAS',2405900,20);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2586,'JOÃO DOURADO',2918357,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2587,'JOÃO LISBOA',2105500,10);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2588,'JOÃO MONLEVADE',3136207,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2589,'JOÃO NEIVA',3203130,8);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2590,'JOÃO PESSOA',2507507,15);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2591,'JOÃO PINHEIRO',3136306,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2592,'JOÃO RAMALHO',3525607,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2593,'JOAQUIM FELÍCIO',3136405,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2594,'JOAQUIM GOMES',2703809,2);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2595,'JOAQUIM NABUCO',2608206,17);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2596,'JOAQUIM PIRES',2205409,18);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2597,'JOAQUIM TÁVORA',4112801,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2598,'JOCA MARQUES',2205458,18);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2599,'JÓIA',4311155,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2600,'JOINVILLE',4209102,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2601,'JORDÂNIA',3136504,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2602,'JORDÃO',1200328,1);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2603,'JOSÉ BOITEUX',4209151,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2604,'JOSÉ BONIFÁCIO',3525706,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2605,'JOSÉ DA PENHA',2406007,20);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2606,'JOSÉ DE FREITAS',2205508,18);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2607,'JOSÉ GONÇALVES DE MINAS',3136520,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2608,'JOSÉ RAYDAN',3136553,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2609,'JOSELÂNDIA',2105609,10);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2610,'JOSENÓPOLIS',3136579,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2611,'JOVIÂNIA',5212105,9);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2612,'JUARA',5105101,11);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2613,'JUAREZ TÁVORA',2507606,15);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2614,'JUARINA',1711803,27);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2615,'JUATUBA',3136652,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2616,'JUAZEIRINHO',2507705,15);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2617,'JUAZEIRO',2918407,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2618,'JUAZEIRO DO NORTE',2307304,6);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2619,'JUAZEIRO DO PIAUÍ',2205516,18);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2620,'JUCÁS',2307403,6);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2621,'JUCATI',2608255,17);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2622,'JUCURUÇU',2918456,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2623,'JUCURUTU',2406106,20);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2624,'JUÍNA',5105150,11);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2625,'JUIZ DE FORA',3136702,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2626,'JÚLIO BORGES',2205524,18);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2627,'JÚLIO DE CASTILHOS',4311205,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2628,'JÚLIO MESQUITA',3525805,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2629,'JUMIRIM',3525854,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2630,'JUNCO DO MARANHÃO',2105658,10);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2631,'JUNCO DO SERIDÓ',2507804,15);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2632,'JUNDIÁ',2703908,2);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2633,'JUNDIÁ',2406155,20);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2634,'JUNDIAÍ',3525904,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2635,'JUNDIAÍ DO SUL',4112900,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2636,'JUNQUEIRO',2704005,2);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2637,'JUNQUEIRÓPOLIS',3526001,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2638,'JUPI',2608305,17);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2639,'JUPIÁ',4209177,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2640,'JUQUIÁ',3526100,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2641,'JUQUITIBA',3526209,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2642,'JURAMENTO',3136801,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2643,'JURANDA',4112959,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2644,'JUREMA',2608404,17);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2645,'JUREMA',2205532,18);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2646,'JURIPIRANGA',2507903,15);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2647,'JURU',2508000,15);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2648,'JURUÁ',1302207,4);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2649,'JURUAIA',3136900,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2650,'JURUENA',5105176,11);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2651,'JURUTI',1503903,14);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2652,'JUSCIMEIRA',5105200,11);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2653,'JUSSARA',2918506,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2654,'JUSSARA',4113007,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2655,'JUSSARA',5212204,9);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2656,'JUSSARI',2918555,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2657,'JUSSIAPE',2918605,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2658,'JUTAÍ',1302306,4);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2659,'JUTI',5005152,12);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2660,'JUVENÍLIA',3136959,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2661,'KALORÉ',4113106,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2662,'LÁBREA',1302405,4);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2663,'LACERDÓPOLIS',4209201,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2664,'LADAINHA',3137007,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2665,'LADÁRIO',5005202,12);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2666,'LAFAIETE COUTINHO',2918704,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2667,'LAGAMAR',3137106,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2668,'LAGARTO',2803500,26);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2669,'LAGES',4209300,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2670,'LAGO DA PEDRA',2105708,10);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2671,'LAGO DO JUNCO',2105807,10);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2672,'LAGO DOS RODRIGUES',2105948,10);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2673,'LAGO VERDE',2105906,10);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2674,'LAGOA',2508109,15);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2675,'LAGOA ALEGRE',2205557,18);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2676,'LAGOA BONITA DO SUL',4311239,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2677,'LAGOA D''ANTA',2406205,20);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2678,'LAGOA DA CANOA',2704104,2);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2679,'LAGOA DA CONFUSÃO',1711902,27);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2680,'LAGOA DA PRATA',3137205,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2681,'LAGOA DE DENTRO',2508208,15);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2682,'LAGOA DE PEDRAS',2406304,20);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2683,'LAGOA DE SÃO FRANCISCO',2205573,18);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2684,'LAGOA DE VELHOS',2406403,20);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2685,'LAGOA DO BARRO DO PIAUÍ',2205565,18);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2686,'LAGOA DO CARRO',2608453,17);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2687,'LAGOA DO ITAENGA',2608503,17);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2688,'LAGOA DO MATO',2105922,10);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2689,'LAGOA DO OURO',2608602,17);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2690,'LAGOA DO PIAUÍ',2205581,18);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2691,'LAGOA DO SÍTIO',2205599,18);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2692,'LAGOA DO TOCANTINS',1711951,27);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2693,'LAGOA DOS GATOS',2608701,17);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2694,'LAGOA DOS PATOS',3137304,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2695,'LAGOA DOS TRÊS CANTOS',4311270,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2696,'LAGOA DOURADA',3137403,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2697,'LAGOA FORMOSA',3137502,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2698,'LAGOA GRANDE',2608750,17);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2699,'LAGOA GRANDE',3137536,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2700,'LAGOA GRANDE DO MARANHÃO',2105963,10);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2701,'LAGOA NOVA',2406502,20);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2702,'LAGOA REAL',2918753,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2703,'LAGOA SALGADA',2406601,20);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2704,'LAGOA SANTA',5212253,9);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2705,'LAGOA SANTA',3137601,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2706,'LAGOA SECA',2508307,15);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2707,'LAGOA VERMELHA',4311304,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2708,'LAGOÃO',4311254,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2709,'LAGOINHA',3526308,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2710,'LAGOINHA DO PIAUÍ',2205540,18);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2711,'LAGUNA',4209409,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2712,'LAGUNA CARAPÃ',5005251,12);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2713,'LAJE',2918803,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2714,'LAJE DO MURIAÉ',3302304,19);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2715,'LAJEADO',4311403,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2716,'LAJEADO',1712009,27);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2717,'LAJEADO DO BUGRE',4311429,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2718,'LAJEADO GRANDE',4209458,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2719,'LAJEADO NOVO',2105989,10);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2720,'LAJEDÃO',2918902,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2721,'LAJEDINHO',2919009,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2722,'LAJEDO',2608800,17);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2723,'LAJEDO DO TABOCAL',2919058,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2724,'LAJES',2406700,20);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2725,'LAJES PINTADAS',2406809,20);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2726,'LAJINHA',3137700,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2727,'LAMARÃO',2919108,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2728,'LAMBARI',3137809,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2729,'LAMBARI D''OESTE',5105234,11);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2730,'LAMIM',3137908,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2731,'LANDRI SALES',2205607,18);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2732,'LAPA',4113205,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2733,'LAPÃO',2919157,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2734,'LARANJA DA TERRA',3203163,8);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2735,'LARANJAL',4113254,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2736,'LARANJAL',3138005,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2737,'LARANJAL DO JARI',1600279,3);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2738,'LARANJAL PAULISTA',3526407,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2739,'LARANJEIRAS',2803609,26);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2740,'LARANJEIRAS DO SUL',4113304,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2741,'LASSANCE',3138104,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2742,'LASTRO',2508406,15);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2743,'LAURENTINO',4209508,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2744,'LAURO DE FREITAS',2919207,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2745,'LAURO MULLER',4209607,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2746,'LAVANDEIRA',1712157,27);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2747,'LAVÍNIA',3526506,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2748,'LAVRAS',3138203,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2749,'LAVRAS DA MANGABEIRA',2307502,6);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2750,'LAVRAS DO SUL',4311502,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2751,'LAVRINHAS',3526605,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2752,'LEANDRO FERREIRA',3138302,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2753,'LEBON RÉGIS',4209706,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2754,'LEME',3526704,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2755,'LEME DO PRADO',3138351,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2756,'LENÇÓIS',2919306,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2757,'LENÇÓIS PAULISTA',3526803,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2758,'LEOBERTO LEAL',4209805,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2759,'LEOPOLDINA',3138401,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2760,'LEOPOLDO DE BULHÕES',5212303,9);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2761,'LEÓPOLIS',4113403,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2762,'LIBERATO SALZANO',4311601,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2763,'LIBERDADE',3138500,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2764,'LICÍNIO DE ALMEIDA',2919405,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2765,'LIDIANÓPOLIS',4113429,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2766,'LIMA CAMPOS',2106003,10);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2767,'LIMA DUARTE',3138609,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2768,'LIMEIRA',3526902,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2769,'LIMEIRA DO OESTE',3138625,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2770,'LIMOEIRO',2608909,17);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2771,'LIMOEIRO DE ANADIA',2704203,2);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2772,'LIMOEIRO DO AJURU',1504000,14);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2773,'LIMOEIRO DO NORTE',2307601,6);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2774,'LINDOESTE',4113452,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2775,'LINDÓIA',3527009,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2776,'LINDÓIA DO SUL',4209854,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2777,'LINDOLFO COLLOR',4311627,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2778,'LINHA NOVA',4311643,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2779,'LINHARES',3203205,8);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2780,'LINS',3527108,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2781,'LIVRAMENTO',2508505,15);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2782,'LIVRAMENTO DE NOSSA SENHORA',2919504,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2783,'LIZARDA',1712405,27);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2784,'LOANDA',4113502,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2785,'LOBATO',4113601,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2786,'LOGRADOURO',2508554,15);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2787,'LONDRINA',4113700,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2788,'LONTRA',3138658,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2789,'LONTRAS',4209904,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2790,'LORENA',3527207,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2791,'LORETO',2106102,10);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2792,'LOURDES',3527256,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2793,'LOUVEIRA',3527306,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2794,'LUCAS DO RIO VERDE',5105259,11);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2795,'LUCÉLIA',3527405,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2796,'LUCENA',2508604,15);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2797,'LUCIANÓPOLIS',3527504,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2798,'LUCIARA',5105309,11);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2799,'LUCRÉCIA',2406908,20);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2800,'LUÍS ANTÔNIO',3527603,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2801,'LUÍS CORREIA',2205706,18);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2802,'LUÍS DOMINGUES',2106201,10);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2803,'LUÍS EDUARDO MAGALHÃES',2919553,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2804,'LUÍS GOMES',2407005,20);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2805,'LUISBURGO',3138674,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2806,'LUISLÂNDIA',3138682,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2807,'LUIZ ALVES',4210001,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2808,'LUIZIANA',4113734,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2809,'LUIZIÂNIA',3527702,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2810,'LUMINÁRIAS',3138708,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2811,'LUNARDELLI',4113759,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2812,'LUPÉRCIO',3527801,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2813,'LUPIONÓPOLIS',4113809,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2814,'LUTÉCIA',3527900,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2815,'LUZ',3138807,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2816,'LUZERNA',4210035,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2817,'LUZIÂNIA',5212501,9);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2818,'LUZILÂNDIA',2205805,18);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2819,'LUZINÓPOLIS',1712454,27);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2820,'MACAÉ',3302403,19);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2821,'MACAÍBA',2407104,20);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2822,'MACAJUBA',2919603,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2823,'MAÇAMBARA',4311718,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2824,'MACAMBIRA',2803708,26);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2825,'MACAPÁ',1600303,3);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2826,'MACAPARANA',2609006,17);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2827,'MACARANI',2919702,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2828,'MACATUBA',3528007,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2829,'MACAU',2407203,20);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2830,'MACAUBAL',3528106,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2831,'MACAÚBAS',2919801,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2832,'MACEDÔNIA',3528205,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2833,'MACEIÓ',2704302,2);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2834,'MACHACALIS',3138906,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2835,'MACHADINHO',4311700,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2836,'MACHADINHO D''OESTE',1100130,22);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2837,'MACHADO',3139003,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2838,'MACHADOS',2609105,17);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2839,'MACIEIRA',4210050,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2840,'MACUCO',3302452,19);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2841,'MACURURÉ',2919900,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2842,'MADALENA',2307635,6);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2843,'MADEIRO',2205854,18);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2844,'MADRE DE DEUS',2919926,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2845,'MADRE DE DEUS DE MINAS',3139102,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2846,'MÃE D''ÁGUA',2508703,15);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2847,'MÃE DO RIO',1504059,14);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2848,'MAETINGA',2919959,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2849,'MAFRA',4210100,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2850,'MAGALHÃES BARATA',1504109,14);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2851,'MAGALHÃES DE ALMEIDA',2106300,10);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2852,'MAGDA',3528304,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2853,'MAGÉ',3302502,19);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2854,'MAIQUINIQUE',2920007,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2855,'MAIRI',2920106,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2856,'MAIRINQUE',3528403,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2857,'MAIRIPORÃ',3528502,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2858,'MAIRIPOTABA',5212600,9);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2859,'MAJOR GERCINO',4210209,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2860,'MAJOR ISIDORO',2704401,2);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2861,'MAJOR SALES',2407252,20);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2862,'MAJOR VIEIRA',4210308,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2863,'MALACACHETA',3139201,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2864,'MALHADA',2920205,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2865,'MALHADA DE PEDRAS',2920304,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2866,'MALHADA DOS BOIS',2803807,26);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2867,'MALHADOR',2803906,26);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2868,'MALLET',4113908,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2869,'MALTA',2508802,15);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2870,'MAMANGUAPE',2508901,15);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2871,'MAMBAÍ',5212709,9);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2872,'MAMBORÊ',4114005,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2873,'MAMONAS',3139250,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2874,'MAMPITUBA',4311734,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2875,'MANACAPURU',1302504,4);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2876,'MANAÍRA',2509008,15);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2877,'MANAQUIRI',1302553,4);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2878,'MANARI',2609154,17);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2879,'MANAUS',1302603,4);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2880,'MÂNCIO LIMA',1200336,1);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2881,'MANDAGUAÇU',4114104,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2882,'MANDAGUARI',4114203,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2883,'MANDIRITUBA',4114302,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2884,'MANDURI',3528601,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2885,'MANFRINÓPOLIS',4114351,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2886,'MANGA',3139300,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2887,'MANGARATIBA',3302601,19);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2888,'MANGUEIRINHA',4114401,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2889,'MANHUAÇU',3139409,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2890,'MANHUMIRIM',3139508,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2891,'MANICORÉ',1302702,4);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2892,'MANOEL EMÍDIO',2205904,18);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2893,'MANOEL RIBAS',4114500,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2894,'MANOEL URBANO',1200344,1);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2895,'MANOEL VIANA',4311759,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2896,'MANOEL VITORINO',2920403,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2897,'MANSIDÃO',2920452,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2898,'MANTENA',3139607,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2899,'MANTENÓPOLIS',3203304,8);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2900,'MAQUINÉ',4311775,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2901,'MAR DE ESPANHA',3139805,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2902,'MAR VERMELHO',2704906,2);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2903,'MARA ROSA',5212808,9);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2904,'MARAÃ',1302801,4);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2905,'MARABÁ',1504208,14);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2906,'MARABÁ PAULISTA',3528700,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2907,'MARACAÇUMÉ',2106326,10);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2908,'MARACAÍ',3528809,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2909,'MARACAJÁ',4210407,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2910,'MARACAJU',5005400,12);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2911,'MARACANÃ',1504307,14);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2912,'MARACANAÚ',2307650,6);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2913,'MARACÁS',2920502,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2914,'MARAGOGI',2704500,2);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2915,'MARAGOGIPE',2920601,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2916,'MARAIAL',2609204,17);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2917,'MARAJÁ DO SENA',2106359,10);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2918,'MARANGUAPE',2307700,6);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2919,'MARANHÃOZINHO',2106375,10);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2920,'MARAPANIM',1504406,14);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2921,'MARAPOAMA',3528858,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2922,'MARATÁ',4311791,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2923,'MARATAIZES',3203320,8);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2924,'MARAU',4311809,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2925,'MARAÚ',2920700,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2926,'MARAVILHA',2704609,2);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2927,'MARAVILHA',4210506,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2928,'MARAVILHAS',3139706,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2929,'MARCAÇÃO',2509057,15);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2930,'MARCELÂNDIA',5105580,11);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2931,'MARCELINO RAMOS',4311908,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2932,'MARCELINO VIEIRA',2407302,20);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2933,'MARCIONÍLIO SOUZA',2920809,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2934,'MARCO',2307809,6);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2935,'MARCOLÂNDIA',2205953,18);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2936,'MARCOS PARENTE',2206001,18);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2937,'MARECHAL CÂNDIDO RONDON',4114609,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2938,'MARECHAL CANDIDO RONDON',5300109,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2939,'MARECHAL DEODORO',2704708,2);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2940,'MARECHAL FLORIANO',3203346,8);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2941,'MARECHAL THAUMATURGO',1200351,1);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2942,'MAREMA',4210555,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2943,'MARI',2509107,15);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2944,'MARIA DA FÉ',3139904,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2945,'MARIA HELENA',4114708,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2946,'MARIALVA',4114807,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2947,'MARIANA',3140001,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2948,'MARIANA PIMENTEL',4311981,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2949,'MARIANO MORO',4312005,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2950,'MARIANÓPOLIS DO TOCANTINS',1712504,27);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2951,'MARIÁPOLIS',3528908,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2952,'MARIBONDO',2704807,2);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2953,'MARICÁ',3302700,19);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2954,'MARILAC',3140100,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2955,'MARILÂNDIA',3203353,8);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2956,'MARILÂNDIA DO SUL',4114906,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2957,'MARILENA',4115002,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2958,'MARÍLIA',3529005,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2959,'MARILUZ',4115101,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2960,'MARINGÁ',4115200,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2961,'MARINÓPOLIS',3529104,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2962,'MÁRIO CAMPOS',3140159,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2963,'MARIÓPOLIS',4115309,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2964,'MARIPÁ',4115358,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2965,'MARIPÁ DE MINAS',3140209,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2966,'MARITUBA',1504422,14);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2967,'MARIZÓPOLIS',2509156,15);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2968,'MARLIÉRIA',3140308,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2969,'MARMELEIRO',4115408,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2970,'MARMELÓPOLIS',3140407,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2971,'MARQUES DE SOUZA',4312054,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2972,'MARQUINHO',4115457,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2973,'MARTINHO CAMPOS',3140506,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2974,'MARTINÓPOLE',2307908,6);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2975,'MARTINÓPOLIS',3529203,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2976,'MARTINS',2407401,20);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2977,'MARTINS SOARES',3140530,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2978,'MARUIM',2804003,26);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2979,'MARUMBI',4115507,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2980,'MARZAGÃO',5212907,9);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2981,'MASCOTE',2920908,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2982,'MASSAPÊ',2308005,6);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2983,'MASSÂPE DO PIAUÍ',2206050,18);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2984,'MASSARANDUBA',2509206,15);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2985,'MASSARANDUBA',4210605,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2986,'MATA',4312104,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2987,'MATA DE SÃO JOÃO',2921005,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2988,'MATA GRANDE',2705002,2);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2989,'MATA ROMA',2106409,10);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2990,'MATA VERDE',3140555,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2991,'MATÃO',3529302,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2992,'MATARACA',2509305,15);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2993,'MATEIROS',1712702,27);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2994,'MATELÂNDIA',4115606,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2995,'MATERLÂNDIA',3140605,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2996,'MATEUS LEME',3140704,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2997,'MATHIAS LOBATO',3171501,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2998,'MATIAS BARBOSA',3140803,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (2999,'MATIAS CARDOSO',3140852,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3000,'MATIAS OLÍMPIO',2206100,18);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3001,'MATINA',2921054,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3002,'MATINHA',2106508,10);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3003,'MATINHAS',2509339,15);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3004,'MATINHOS',4115705,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3005,'MATIPÓ',3140902,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3006,'MATO CASTELHANO',4312138,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3007,'MATO GROSSO',2509370,15);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3008,'MATO LEITÃO',4312153,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3009,'MATO QUEIMADO',4312179,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3010,'MATO RICO',4115739,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3011,'MATO VERDE',3141009,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3012,'MATÕES',2106607,10);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3013,'MATÕES DO NORTE',2106631,10);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3014,'MATOS COSTA',4210704,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3015,'MATOZINHOS',3141108,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3016,'MATRINCHÃ',5212956,9);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3017,'MATRIZ DE CAMARAGIBE',2705101,2);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3018,'MATUPÁ',5105606,11);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3019,'MATURÉIA',2509396,15);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3020,'MATUTINA',3141207,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3021,'MAUÁ',3529401,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3022,'MAUÁ DA SERRA',4115754,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3023,'MAUÉS',1302900,4);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3024,'MAURILÂNDIA',5213004,9);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3025,'MAURILÂNDIA DO TOCANTINS',1712801,27);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3026,'MAURITI',2308104,6);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3027,'MAXARANGUAPE',2407500,20);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3028,'MAXIMILIANO DE ALMEIDA',4312203,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3029,'MAZAGÃO',1600402,3);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3030,'MEDEIROS',3141306,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3031,'MEDEIROS NETO',2921104,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3032,'MEDIANEIRA',4115804,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3033,'MEDICILÂNDIA',1504455,14);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3034,'MEDINA',3141405,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3035,'MELEIRO',4210803,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3036,'MELGAÇO',1504505,14);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3037,'MENDES',3302809,19);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3038,'MENDES PIMENTEL',3141504,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3039,'MENDONÇA',3529500,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3040,'MERCEDES',4115853,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3041,'MERCÊS',3141603,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3042,'MERIDIANO',3529609,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3043,'MERUOCA',2308203,6);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3044,'MESÓPOLIS',3529658,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3045,'MESQUITA',3302858,19);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3046,'MESQUITA',3141702,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3047,'MESSIAS',2705200,2);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3048,'MESSIAS TARGINO',2407609,20);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3049,'MIGUEL ALVES',2206209,18);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3050,'MIGUEL CALMON',2921203,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3051,'MIGUEL LEÃO',2206308,18);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3052,'MIGUEL PEREIRA',3302908,19);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3053,'MIGUELÓPOLIS',3529708,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3054,'MILAGRES',2921302,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3055,'MILAGRES',2308302,6);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3056,'MILAGRES DO MARANHÃO',2106672,10);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3057,'MILHÃ',2308351,6);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3058,'MILTON BRANDÃO',2206357,18);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3059,'MIMOSO DE GOIÁS',5213053,9);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3060,'MIMOSO DO SUL',3203403,8);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3061,'MINAÇU',5213087,9);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3062,'MINADOR DO NEGRÃO',2705309,2);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3063,'MINAS DO LEÃO',4312252,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3064,'MINAS NOVAS',3141801,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3065,'MINDURI',3141900,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3066,'MINEIROS',5213103,9);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3067,'MINEIROS DO TIETÊ',3529807,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3068,'MINISTRO ANDREAZZA',1101203,22);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3069,'MIRA ESTRELA',3530003,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3070,'MIRABELA',3142007,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3071,'MIRACATU',3529906,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3072,'MIRACEMA',3303005,19);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3073,'MIRACEMA DO TOCANTINS',1713205,27);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3074,'MIRADOR',4115903,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3075,'MIRADOR',2106706,10);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3076,'MIRADOURO',3142106,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3077,'MIRAGUAÍ',4312302,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3078,'MIRAÍ',3142205,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3079,'MIRAÍMA',2308377,6);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3080,'MIRANDA',5005608,12);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3081,'MIRANDA DO NORTE',2106755,10);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3082,'MIRANDIBA',2609303,17);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3083,'MIRANDÓPOLIS',3530102,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3084,'MIRANGABA',2921401,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3085,'MIRANORTE',1713304,27);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3086,'MIRANTE',2921450,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3087,'MIRANTE DA SERRA',1101302,22);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3088,'MIRANTE DO PARANAPANEMA',3530201,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3089,'MIRASELVA',4116000,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3090,'MIRASSOL',3530300,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3091,'MIRASSOL D''OESTE',5105622,11);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3092,'MIRASSOLÂNDIA',3530409,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3093,'MIRAVÂNIA',3142254,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3094,'MIRIM DOCE',4210852,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3095,'MIRINZAL',2106805,10);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3096,'MISSAL',4116059,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3097,'MISSÃO VELHA',2308401,6);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3098,'MOCAJUBA',1504604,14);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3099,'MOCOCA',3530508,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3100,'MODELO',4210902,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3101,'MOEDA',3142304,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3102,'MOEMA',3142403,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3103,'MOGEIRO',2509404,15);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3104,'MOIPORÁ',5213400,9);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3105,'MOITA BONITA',2804102,26);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3106,'MOJI DAS CRUZES',3530607,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3107,'MOJI GUAÇU',3530706,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3108,'MOJI-MIRIM',3530805,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3109,'MOJU',1504703,14);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3110,'MOMBAÇA',2308500,6);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3111,'MOMBUCA',3530904,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3112,'MONÇÃO',2106904,10);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3113,'MONÇÕES',3531001,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3114,'MONDAÍ',4211009,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3115,'MONGAGUÁ',3531100,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3116,'MONJOLOS',3142502,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3117,'MONSENHOR GIL',2206407,18);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3118,'MONSENHOR HIPÓLITO',2206506,18);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3119,'MONSENHOR PAULO',3142601,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3120,'MONSENHOR TABOSA',2308609,6);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3121,'MONTADAS',2509503,15);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3122,'MONTALVÂNIA',3142700,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3123,'MONTANHA',3203502,8);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3124,'MONTANHAS',2407708,20);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3125,'MONTAURI',4312351,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3126,'MONTE ALEGRE',1504802,14);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3127,'MONTE ALEGRE',2407807,20);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3128,'MONTE ALEGRE DE GOIÁS',5213509,9);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3129,'MONTE ALEGRE DE MINAS',3142809,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3130,'MONTE ALEGRE DE SERGIPE',2804201,26);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3131,'MONTE ALEGRE DO PIAUÍ',2206605,18);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3132,'MONTE ALEGRE DO SUL',3531209,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3133,'MONTE ALEGRE DOS CAMPOS',4312377,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3134,'MONTE ALTO',3531308,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3135,'MONTE APRAZÍVEL',3531407,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3136,'MONTE AZUL',3142908,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3137,'MONTE AZUL PAULISTA',3531506,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3138,'MONTE BELO',3143005,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3139,'MONTE BELO DO SUL',4312385,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3140,'MONTE CARLO',4211058,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3141,'MONTE CARMELO',3143104,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3142,'MONTE CASTELO',3531605,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3143,'MONTE CASTELO',4211108,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3144,'MONTE DAS GAMELEIRAS',2407906,20);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3145,'MONTE DO CARMO',1713601,27);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3146,'MONTE FORMOSO',3143153,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3147,'MONTE HOREBE',2509602,15);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3148,'MONTE MOR',3531803,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3149,'MONTE NEGRO',1101401,22);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3150,'MONTE SANTO',2921500,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3151,'MONTE SANTO DE MINAS',3143203,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3152,'MONTE SANTO DO TOCANTINS',1713700,27);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3153,'MONTE SIÃO',3143401,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3154,'MONTEIRO',2509701,15);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3155,'MONTEIRO LOBATO',3531704,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3156,'MONTEIRÓPOLIS',2705408,2);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3157,'MONTENEGRO',4312401,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3158,'MONTES ALTOS',2107001,10);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3159,'MONTES CLAROS',3143302,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3160,'MONTES CLAROS DE GOIÁS',5213707,9);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3161,'MONTEZUMA',3143450,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3162,'MONTIVIDIU',5213756,9);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3163,'MONTIVIDIU DO NORTE',5213772,9);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3164,'MORADA NOVA',2308708,6);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3165,'MORADA NOVA DE MINAS',3143500,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3166,'MORAÚJO',2308807,6);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3167,'MOREILÂNDIA',2614303,17);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3168,'MOREIRA SALES',4116109,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3169,'MORENO',2609402,17);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3170,'MORMAÇO',4312427,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3171,'MORPARÁ',2921609,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3172,'MORRETES',4116208,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3173,'MORRINHOS',5213806,9);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3174,'MORRINHOS',2308906,6);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3175,'MORRINHOS DO SUL',4312443,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3176,'MORRO AGUDO',3531902,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3177,'MORRO AGUDO DE GOIÁS',5213855,9);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3178,'MORRO CABEÇA NO TEMPO',2206654,18);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3179,'MORRO DA FUMAÇA',4211207,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3180,'MORRO DA GARÇA',3143609,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3181,'MORRO DO CHAPÉU',2921708,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3182,'MORRO DO CHAPÉU DO PIAUÍ',2206670,18);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3183,'MORRO DO PILAR',3143708,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3184,'MORRO GRANDE',4211256,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3185,'MORRO REDONDO',4312450,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3186,'MORRO REUTER',4312476,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3187,'MORROS',2107100,10);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3188,'MORTUGABA',2921807,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3189,'MORUNGABA',3532009,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3190,'MOSSÂMEDES',5213905,9);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3191,'MOSSORÓ',2408003,20);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3192,'MOSTARDAS',4312500,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3193,'MOTUCA',3532058,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3194,'MOZARLÂNDIA',5214002,9);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3195,'MUANÁ',1504901,14);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3196,'MUCAJAÍ',1400308,23);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3197,'MUCAMBO',2309003,6);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3198,'MUCUGÊ',2921906,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3199,'MUÇUM',4312609,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3200,'MUCURI',2922003,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3201,'MUCURICI',3203601,8);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3202,'MUITOS CAPÕES',4312617,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3203,'MULITERNO',4312625,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3204,'MULUNGU',2509800,15);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3205,'MULUNGU',2309102,6);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3206,'MULUNGU DO MORRO',2922052,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3207,'MUNDO NOVO',2922102,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3208,'MUNDO NOVO',5214051,9);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3209,'MUNDO NOVO',5005681,12);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3210,'MUNHOZ',3143807,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3211,'MUNHOZ DE MELO',4116307,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3212,'MUNIZ FERREIRA',2922201,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3213,'MUNIZ FREIRE',3203700,8);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3214,'MUQUÉM DE SÃO FRANCISCO',2922250,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3215,'MUQUI',3203809,8);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3216,'MURIAÉ',3143906,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3217,'MURIBECA',2804300,26);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3218,'MURICI',2705507,2);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3219,'MURICI DOS PORTELAS',2206696,18);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3220,'MURICILÂNDIA',1713957,27);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3221,'MURITIBA',2922300,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3222,'MURUTINGA DO SUL',3532108,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3223,'MUTUÍPE',2922409,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3224,'MUTUM',3144003,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3225,'MUTUNÓPOLIS',5214101,9);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3226,'MUZAMBINHO',3144102,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3227,'NACIP RAYDAN',3144201,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3228,'NANTES',3532157,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3229,'NANUQUE',3144300,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3230,'NÃO-ME-TOQUE',4312658,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3231,'NAQUE',3144359,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3232,'NARANDIBA',3532207,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3233,'NATAL',2408102,20);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3234,'NATALÂNDIA',3144375,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3235,'NATÉRCIA',3144409,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3236,'NATIVIDADE',3303104,19);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3237,'NATIVIDADE',1714203,27);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3238,'NATIVIDADE DA SERRA',3532306,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3239,'NATUBA',2509909,15);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3240,'NAVEGANTES',4211306,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3241,'NAVIRAÍ',5005707,12);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3242,'NAZARÉ',2922508,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3243,'NAZARÉ',1714302,27);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3244,'NAZARÉ DA MATA',2609501,17);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3245,'NAZARÉ DO PIAUÍ',2206704,18);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3246,'NAZARÉ PAULISTA',3532405,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3247,'NAZARENO',3144508,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3248,'NAZAREZINHO',2510006,15);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3249,'NAZÁRIO',5214408,9);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3250,'NEÓPOLIS',2804409,26);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3251,'NEPOMUCENO',3144607,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3252,'NERÓPOLIS',5214507,9);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3253,'NEVES PAULISTA',3532504,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3254,'NHAMUNDÁ',1303007,4);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3255,'NHANDEARA',3532603,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3256,'NICOLAU VERGUEIRO',4312674,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3257,'NILO PEÇANHA',2922607,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3258,'NILÓPOLIS',3303203,19);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3259,'NINA RODRIGUES',2107209,10);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3260,'NINHEIRA',3144656,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3261,'NIOAQUE',5005806,12);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3262,'NIPOÃ',3532702,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3263,'NIQUELÂNDIA',5214606,9);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3264,'NÍSIA FLORESTA',2408201,20);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3265,'NITERÓI',3303302,19);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3266,'NOBRES',5105903,11);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3267,'NONOAI',4312708,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3268,'NORDESTINA',2922656,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3269,'NORMANDIA',1400407,23);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3270,'NORTELÂNDIA',5106000,11);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3271,'NOSSA SENHORA APARECIDA',2804458,26);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3272,'NOSSA SENHORA DA GLÓRIA',2804508,26);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3273,'NOSSA SENHORA DAS DORES',2804607,26);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3274,'NOSSA SENHORA DAS GRAÇAS',4116406,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3275,'NOSSA SENHORA DE LOURDES',2804706,26);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3276,'NOSSA SENHORA DE NAZARÉ',2206753,18);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3277,'NOSSA SENHORA DO LIVRAMENTO',5106109,11);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3278,'NOSSA SENHORA DO SOCORRO',2804805,26);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3279,'NOSSA SENHORA DOS REMÉDIOS',2206803,18);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3280,'NOVA ALIANÇA',3532801,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3281,'NOVA ALIANÇA DO IVAÍ',4116505,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3282,'NOVA ALVORADA',4312757,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3283,'NOVA ALVORADA DO SUL',5006002,12);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3284,'NOVA AMÉRICA',5214705,9);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3285,'NOVA AMÉRICA DA COLINA',4116604,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3286,'NOVA ANDRADINA',5006200,12);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3287,'NOVA ARAÇÁ',4312807,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3288,'NOVA AURORA',4116703,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3289,'NOVA AURORA',5214804,9);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3290,'NOVA BANDEIRANTES',5106158,11);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3291,'NOVA BASSANO',4312906,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3292,'NOVA BELÉM',3144672,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3293,'NOVA BOA VISTA',4312955,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3294,'NOVA BRASILÂNDIA',5106208,11);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3295,'NOVA BRASILÂNDIA D''OESTE',1100148,22);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3296,'NOVA BRÉSCIA',4313003,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3297,'NOVA CAMPINA',3532827,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3298,'NOVA CANAÃ',2922706,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3299,'NOVA CANAÃ DO NORTE',5106216,11);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3300,'NOVA CANAÃ PAULISTA',3532843,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3301,'NOVA CANDELÁRIA',4313011,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3302,'NOVA CANTU',4116802,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3303,'NOVA CASTILHO',3532868,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3304,'NOVA COLINAS',2107258,10);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3305,'NOVA CRIXÁS',5214838,9);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3306,'NOVA CRUZ',2408300,20);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3307,'NOVA ERA',3144706,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3308,'NOVA ERECHIM',4211405,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3309,'NOVA ESPERANÇA',4116901,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3310,'NOVA ESPERANÇA DO PIRIÁ',1504950,14);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3311,'NOVA ESPERANÇA DO SUDOESTE',4116950,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3312,'NOVA ESPERANÇA DO SUL',4313037,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3313,'NOVA EUROPA',3532900,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3314,'NOVA FÁTIMA',2922730,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3315,'NOVA FÁTIMA',4117008,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3316,'NOVA FLORESTA',2510105,15);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3317,'NOVA FRIBURGO',3303401,19);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3318,'NOVA GLÓRIA',5214861,9);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3319,'NOVA GRANADA',3533007,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3320,'NOVA GUARITA',5108808,11);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3321,'NOVA GUATAPORANGA',3533106,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3322,'NOVA HARTZ',4313060,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3323,'NOVA IBIÁ',2922755,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3324,'NOVA IGUAÇU',3303500,19);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3325,'NOVA IGUAÇU DE GOIÁS',5214879,9);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3326,'NOVA INDEPENDÊNCIA',3533205,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3327,'NOVA IORQUE',2107308,10);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3328,'NOVA IPIXUNA',1504976,14);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3329,'NOVA ITABERABA',4211454,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3330,'NOVA ITARANA',2922805,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3331,'NOVA LACERDA',5106182,11);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3332,'NOVA LARANJEIRAS',4117057,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3333,'NOVA LIMA',3144805,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3334,'NOVA LONDRINA',4117107,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3335,'NOVA LUZITÂNIA',3533304,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3336,'NOVA MAMORÉ',1100338,22);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3337,'NOVA MARILÂNDIA',5108857,11);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3338,'NOVA MARINGÁ',5108907,11);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3339,'NOVA MÓDICA',3144904,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3340,'NOVA MONTE VERDE',5108956,11);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3341,'NOVA MUTUM',5106224,11);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3342,'NOVA NAZARÉ',5106174,11);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3343,'NOVA ODESSA',3533403,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3344,'NOVA OLÍMPIA',4117206,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3345,'NOVA OLÍMPIA',5106232,11);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3346,'NOVA OLINDA',2510204,15);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3347,'NOVA OLINDA',2309201,6);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3348,'NOVA OLINDA',1714880,27);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3349,'NOVA OLINDA DO MARANHÃO',2107357,10);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3350,'NOVA OLINDA DO NORTE',1303106,4);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3351,'NOVA PÁDUA',4313086,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3352,'NOVA PALMA',4313102,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3353,'NOVA PALMEIRA',2510303,15);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3354,'NOVA PETRÓPOLIS',4313201,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3355,'NOVA PONTE',3145000,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3356,'NOVA PORTEIRINHA',3145059,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3357,'NOVA PRATA',4313300,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3358,'NOVA PRATA DO IGUAÇU',4117255,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3359,'NOVA RAMADA',4313334,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3360,'NOVA REDENÇÃO',2922854,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3361,'NOVA RESENDE',3145109,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3362,'NOVA ROMA',5214903,9);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3363,'NOVA ROMA DO SUL',4313359,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3364,'NOVA ROSALÂNDIA',1715002,27);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3365,'NOVA RUSSAS',2309300,6);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3366,'NOVA SANTA BÁRBARA',4117214,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3367,'NOVA SANTA HELENA',5106190,11);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3368,'NOVA SANTA RITA',4313375,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3369,'NOVA SANTA RITA',2207959,18);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3370,'NOVA SANTA ROSA',4117222,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3371,'NOVA SERRANA',3145208,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3372,'NOVA SOURE',2922904,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3373,'NOVA TEBAS',4117271,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3374,'NOVA TIMBOTEUA',1505007,14);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3375,'NOVA TRENTO',4211504,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3376,'NOVA UBIRATÃ',5106240,11);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3377,'NOVA UNIÃO',1101435,22);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3378,'NOVA UNIÃO',3136603,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3379,'NOVA VENÉCIA',3203908,8);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3380,'NOVA VENEZA',4211603,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3381,'NOVA VENEZA',5215009,9);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3382,'NOVA VIÇOSA',2923001,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3383,'NOVA XAVANTINA',5106257,11);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3384,'NOVAIS',3533254,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3385,'NOVO ACORDO',1715101,27);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3386,'NOVO AIRÃO',1303205,4);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3387,'NOVO ALEGRE',1715150,27);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3388,'NOVO ARIPUANÃ',1303304,4);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3389,'NOVO BARREIRO',4313490,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3390,'NOVO BRASIL',5215207,9);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3391,'NOVO CABRAIS',4313391,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3392,'NOVO CRUZEIRO',3145307,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3393,'NOVO GAMA',5215231,9);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3394,'NOVO HAMBURGO',4313409,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3395,'NOVO HORIZONTE',2923035,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3396,'NOVO HORIZONTE',3533502,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3397,'NOVO HORIZONTE',4211652,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3398,'NOVO HORIZONTE DO NORTE',5106273,11);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3399,'NOVO HORIZONTE DO OESTE',1100502,22);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3400,'NOVO HORIZONTE DO SUL',5006259,12);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3401,'NOVO ITACOLOMI',4117297,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3402,'NOVO JARDIM',1715259,27);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3403,'NOVO LINO',2705606,2);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3404,'NOVO MACHADO',4313425,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3405,'NOVO MUNDO',5106265,11);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3406,'NOVO ORIENTE',2309409,6);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3407,'NOVO ORIENTE DE MINAS',3145356,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3408,'NOVO ORIENTE DO PIAUÍ',2206902,18);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3409,'NOVO PLANALTO',5215256,9);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3410,'NOVO PROGRESSO',1505031,14);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3411,'NOVO REPARTIMENTO',1505064,14);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3412,'NOVO SANTO ANTÔNIO',2206951,18);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3413,'NOVO SANTO ANTÔNIO',5106315,11);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3414,'NOVO SÃO JOAQUIM',5106281,11);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3415,'NOVO TIRADENTES',4313441,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3416,'NOVO TRIUNFO',2923050,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3417,'NOVO XINGU',4313466,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3418,'NOVORIZONTE',3145372,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3419,'NUPORANGA',3533601,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3420,'ÓBIDOS',1505106,14);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3421,'OCARA',2309458,6);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3422,'OCAUÇU',3533700,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3423,'OEIRAS',2207009,18);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3424,'OEIRAS DO PARÁ',1505205,14);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3425,'OIAPOQUE',1600501,3);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3426,'OLARIA',3145406,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3427,'ÓLEO',3533809,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3428,'OLHO D''ÁGUA',2510402,15);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3429,'OLHO D''ÁGUA DAS CUNHÃS',2107407,10);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3430,'OLHO D''ÁGUA DAS FLORES',2705705,2);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3431,'OLHO D''ÁGUA DO BORGES',2408409,20);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3432,'OLHO D''ÁGUA DO CASADO',2705804,2);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3433,'OLHO D''ÁGUA DO PIAUÍ',2207108,18);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3434,'OLHO D''ÁGUA GRANDE',2705903,2);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3435,'OLHOS D''ÁGUA',3145455,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3436,'OLÍMPIA',3533908,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3437,'OLÍMPIO NORONHA',3145505,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3438,'OLINDA',2609600,17);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3439,'OLINDA NOVA DO MARANHÃO',2107456,10);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3440,'OLINDINA',2923100,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3441,'OLIVEDOS',2510501,15);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3442,'OLIVEIRA',3145604,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3443,'OLIVEIRA DE FÁTIMA',1715507,27);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3444,'OLIVEIRA DOS BREJINHOS',2923209,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3445,'OLIVEIRA FORTES',3145703,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3446,'OLIVENÇA',2706000,2);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3447,'ONÇA DE PITANGUI',3145802,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3448,'ONDA VERDE',3534005,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3449,'ORATÓRIOS',3145851,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3450,'ORIENTE',3534104,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3451,'ORINDIÚVA',3534203,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3452,'ORIXIMINÁ',1505304,14);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3453,'ORIZÂNIA',3145877,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3454,'ORIZONA',5215306,9);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3455,'ORLÂNDIA',3534302,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3456,'ORLEANS',4211702,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3457,'OROBÓ',2609709,17);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3458,'OROCÓ',2609808,17);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3459,'ORÓS',2309508,6);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3460,'ORTIGUEIRA',4117305,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3461,'OSASCO',3534401,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3462,'OSCAR BRESSANE',3534500,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3463,'OSÓRIO',4313508,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3464,'OSVALDO CRUZ',3534609,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3465,'OTACÍLIO COSTA',4211751,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3466,'OURÉM',1505403,14);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3467,'OURIÇANGAS',2923308,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3468,'OURICURI',2609907,17);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3469,'OURILÂNDIA DO NORTE',1505437,14);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3470,'OURINHOS',3534708,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3471,'OURIZONA',4117404,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3472,'OURO',4211801,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3473,'OURO BRANCO',2706109,2);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3474,'OURO BRANCO',2408508,20);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3475,'OURO BRANCO',3145901,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3476,'OURO FINO',3146008,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3477,'OURO PRETO',3146107,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3478,'OURO PRETO DO OESTE',1100155,22);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3479,'OURO VELHO',2510600,15);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3480,'OURO VERDE',3534807,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3481,'OURO VERDE',4211850,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3482,'OURO VERDE DE GOIÁS',5215405,9);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3483,'OURO VERDE DE MINAS',3146206,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3484,'OURO VERDE DO OESTE',4117453,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3485,'OUROESTE',3534757,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3486,'OUROLÂNDIA',2923357,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3487,'OUVIDOR',5215504,9);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3488,'PACAEMBU',3534906,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3489,'PACAJÁ',1505486,14);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3490,'PACAJUS',2309607,6);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3491,'PACARAIMA',1400456,23);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3492,'PACATUBA',2804904,26);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3493,'PACATUBA',2309706,6);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3494,'PAÇO DO LUMIAR',2107506,10);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3495,'PACOTI',2309805,6);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3496,'PACUJÁ',2309904,6);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3497,'PADRE BERNARDO',5215603,9);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3498,'PADRE CARVALHO',3146255,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3499,'PADRE MARCOS',2207207,18);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3500,'PADRE PARAÍSO',3146305,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3501,'PAES LANDIM',2207306,18);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3502,'PAI PEDRO',3146552,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3503,'PAIAL',4211876,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3504,'PAIÇANDU',4117503,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3505,'PAIM FILHO',4313607,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3506,'PAINEIRAS',3146404,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3507,'PAINEL',4211892,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3508,'PAINS',3146503,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3509,'PAIVA',3146602,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3510,'PAJEÚ DO PIAUÍ',2207355,18);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3511,'PALESTINA',2706208,2);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3512,'PALESTINA',3535002,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3513,'PALESTINA DE GOIÁS',5215652,9);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3514,'PALESTINA DO PARÁ',1505494,14);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3515,'PALHANO',2310001,6);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3516,'PALHOÇA',4211900,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3517,'PALMA',3146701,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3518,'PALMA SOLA',4212007,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3519,'PALMÁCIA',2310100,6);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3520,'PALMARES',2610004,17);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3521,'PALMARES DO SUL',4313656,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3522,'PALMARES PAULISTA',3535101,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3523,'PALMAS',4117602,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3524,'PALMAS',1721000,27);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3525,'PALMAS DE MONTE ALTO',2923407,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3526,'PALMEIRA',4212056,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3527,'PALMEIRA',4117701,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3528,'PALMEIRA D''OESTE',3535200,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3529,'PALMEIRA DAS MISSÕES',4313706,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3530,'PALMEIRA DO PIAUÍ',2207405,18);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3531,'PALMEIRA DOS ÍNDIOS',2706307,2);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3532,'PALMEIRAIS',2207504,18);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3533,'PALMEIRÂNDIA',2107605,10);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3534,'PALMEIRANTE',1715705,27);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3535,'PALMEIRAS',2923506,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3536,'PALMEIRAS DE GOIÁS',5215702,9);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3537,'PALMEIRAS DO TOCANTINS',1713809,27);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3538,'PALMEIRINA',2610103,17);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3539,'PALMEIRÓPOLIS',1715754,27);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3540,'PALMELO',5215801,9);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3541,'PALMINÓPOLIS',5215900,9);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3542,'PALMITAL',3535309,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3543,'PALMITAL',4117800,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3544,'PALMITINHO',4313805,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3545,'PALMITOS',4212106,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3546,'PALMÓPOLIS',3146750,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3547,'PALOTINA',4117909,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3548,'PANAMÁ',5216007,9);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3549,'PANAMBI',4313904,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3550,'PANCAS',3204005,8);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3551,'PANELAS',2610202,17);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3552,'PANORAMA',3535408,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3553,'PÂNTANO GRANDE',4313953,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3554,'PÃO DE AÇÚCAR',2706406,2);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3555,'PAPAGAIOS',3146909,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3556,'PAPANDUVA',4212205,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3557,'PAQUETÁ',2207553,18);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3558,'PARÁ DE MINAS',3147105,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3559,'PARACAMBI',3303609,19);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3560,'PARACATU',3147006,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3561,'PARACURU',2310209,6);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3562,'PARAGOMINAS',1505502,14);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3563,'PARAGUAÇU',3147204,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3564,'PARAGUAÇU PAULISTA',3535507,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3565,'PARAÍ',4314001,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3566,'PARAÍBA DO SUL',3303708,19);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3567,'PARAIBANO',2107704,10);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3568,'PARAIBUNA',3535606,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3569,'PARAIPABA',2310258,6);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3570,'PARAÍSO',3535705,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3571,'PARAÍSO',4212239,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3572,'PARAÍSO DO NORTE',4118006,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3573,'PARAÍSO DO SUL',4314027,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3574,'PARAÍSO DO TOCANTINS',1716109,27);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3575,'PARAISÓPOLIS',3147303,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3576,'PARAMBU',2310308,6);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3577,'PARAMIRIM',2923605,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3578,'PARAMOTI',2310407,6);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3579,'PARANÁ',2408607,20);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3580,'PARANÃ',1716208,27);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3581,'PARANACITY',4118105,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3582,'PARANAGUÁ',4118204,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3583,'PARANAÍBA',5006309,12);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3584,'PARANAIGUARA',5216304,9);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3585,'PARANAÍTA',5106299,11);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3586,'PARANAPANEMA',3535804,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3587,'PARANAPOEMA',4118303,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3588,'PARANAPUÃ',3535903,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3589,'PARANATAMA',2610301,17);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3590,'PARANATINGA',5106307,11);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3591,'PARANAVAÍ',4118402,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3592,'PARANHOS',5006358,12);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3593,'PARAOPEBA',3147402,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3594,'PARAPUÃ',3536000,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3595,'PARARI',2510659,15);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3596,'PARATI',3303807,19);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3597,'PARATINGA',2923704,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3598,'PARAÚ',2408706,20);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3599,'PARAUAPEBAS',1505536,14);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3600,'PARAÚNA',5216403,9);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3601,'PARAZINHO',2408805,20);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3602,'PARDINHO',3536109,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3603,'PARECI NOVO',4314035,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3604,'PARECIS',1101450,22);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3605,'PARELHAS',2408904,20);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3606,'PARICONHA',2706422,2);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3607,'PARINTINS',1303403,4);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3608,'PARIPIRANGA',2923803,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3609,'PARIPUEIRA',2706448,2);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3610,'PARIQUERA-AÇU',3536208,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3611,'PARISI',3536257,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3612,'PARNAGUÁ',2207603,18);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3613,'PARNAÍBA',2207702,18);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3614,'PARNAMIRIM',2610400,17);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3615,'PARNAMIRIM',2403251,20);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3616,'PARNARAMA',2107803,10);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3617,'PAROBÉ',4314050,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3618,'PASSA E FICA',2409100,20);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3619,'PASSA QUATRO',3147600,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3620,'PASSA SETE',4314068,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3621,'PASSA TEMPO',3147709,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3622,'PASSA VINTE',3147808,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3623,'PASSABÉM',3147501,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3624,'PASSAGEM',2510709,15);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3625,'PASSAGEM',2409209,20);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3626,'PASSAGEM FRANCA',2107902,10);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3627,'PASSAGEM FRANCA DO PIAUÍ',2207751,18);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3628,'PASSIRA',2610509,17);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3629,'PASSO DE CAMARAGIBE',2706505,2);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3630,'PASSO DE TORRES',4212254,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3631,'PASSO DO SOBRADO',4314076,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3632,'PASSO FUNDO',4314100,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3633,'PASSOS',3147907,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3634,'PASSOS MAIA',4212270,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3635,'PASTOS BONS',2108009,10);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3636,'PATIS',3147956,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3637,'PATO BRAGADO',4118451,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3638,'PATO BRANCO',4118501,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3639,'PATOS',2510808,15);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3640,'PATOS DE MINAS',3148004,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3641,'PATOS DO PIAUÍ',2207777,18);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3642,'PATROCÍNIO',3148103,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3643,'PATROCÍNIO DO MURIAÉ',3148202,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3644,'PATROCÍNIO PAULISTA',3536307,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3645,'PATU',2409308,20);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3646,'PATY DO ALFERES',3303856,19);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3647,'PAU BRASIL',2923902,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3648,'PAU D''ARCO',1505551,14);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3649,'PAU D''ARCO',2207793,18);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3650,'PAU D''ARCO',1716307,27);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3651,'PAU DOS FERROS',2409407,20);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3652,'PAUDALHO',2610608,17);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3653,'PAUINI',1303502,4);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3654,'PAULA CÂNDIDO',3148301,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3655,'PAULA FREITAS',4118600,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3656,'PAULICÉIA',3536406,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3657,'PAULÍNIA',3536505,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3658,'PAULINO NEVES',2108058,10);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3659,'PAULISTA',2610707,17);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3660,'PAULISTA',2510907,15);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3661,'PAULISTANA',2207801,18);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3662,'PAULISTÂNIA',3536570,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3663,'PAULISTAS',3148400,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3664,'PAULO AFONSO',2924009,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3665,'PAULO BENTO',4314134,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3666,'PAULO DE FARIA',3536604,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3667,'PAULO FRONTIN',4118709,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3668,'PAULO JACINTO',2706604,2);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3669,'PAULO LOPES',4212304,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3670,'PAULO RAMOS',2108108,10);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3671,'PAVÃO',3148509,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3672,'PAVERAMA',4314159,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3673,'PAVUSSU',2207850,18);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3674,'PÉ DE SERRA',2924058,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3675,'PEABIRU',4118808,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3676,'PEÇANHA',3148608,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3677,'PEDERNEIRAS',3536703,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3678,'PEDRA',2610806,17);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3679,'PEDRA AZUL',3148707,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3680,'PEDRA BELA',3536802,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3681,'PEDRA BONITA',3148756,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3682,'PEDRA BRANCA',2511004,15);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3683,'PEDRA BRANCA',2310506,6);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3684,'PEDRA BRANCA DO AMAPARI',1600154,3);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3685,'PEDRA DO ANTA',3148806,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3686,'PEDRA DO INDAIÁ',3148905,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3687,'PEDRA DOURADA',3149002,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3688,'PEDRA GRANDE',2409506,20);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3689,'PEDRA LAVRADA',2511103,15);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3690,'PEDRA MOLE',2805000,26);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3691,'PEDRA PRETA',2409605,20);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3692,'PEDRA PRETA',5106372,11);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3693,'PEDRALVA',3149101,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3694,'PEDRANÓPOLIS',3536901,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3695,'PEDRÃO',2924108,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3696,'PEDRAS ALTAS',4314175,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3697,'PEDRAS DE FOGO',2511202,15);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3698,'PEDRAS DE MARIA DA CRUZ',3149150,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3699,'PEDRAS GRANDES',4212403,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3700,'PEDREGULHO',3537008,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3701,'PEDREIRA',3537107,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3702,'PEDREIRAS',2108207,10);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3703,'PEDRINHAS',2805109,26);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3704,'PEDRINHAS PAULISTA',3537156,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3705,'PEDRINÓPOLIS',3149200,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3706,'PEDRO AFONSO',1716505,27);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3707,'PEDRO ALEXANDRE',2924207,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3708,'PEDRO AVELINO',2409704,20);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3709,'PEDRO CANÁRIO',3204054,8);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3710,'PEDRO DE TOLEDO',3537206,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3711,'PEDRO DO ROSÁRIO',2108256,10);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3712,'PEDRO GOMES',5006408,12);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3713,'PEDRO II',2207900,18);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3714,'PEDRO LAURENTINO',2207934,18);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3715,'PEDRO LEOPOLDO',3149309,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3716,'PEDRO OSÓRIO',4314209,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3717,'PEDRO RÉGIS',2512721,15);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3718,'PEDRO TEIXEIRA',3149408,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3719,'PEDRO VELHO',2409803,20);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3720,'PEIXE',1716604,27);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3721,'PEIXE-BOI',1505601,14);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3722,'PEIXOTO DE AZEVEDO',5106422,11);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3723,'PEJUÇARA',4314308,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3724,'PELOTAS',4314407,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3725,'PENAFORTE',2310605,6);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3726,'PENALVA',2108306,10);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3727,'PENÁPOLIS',3537305,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3728,'PENDÊNCIAS',2409902,20);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3729,'PENEDO',2706703,2);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3730,'PENHA',4212502,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3731,'PENTECOSTE',2310704,6);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3732,'PEQUERI',3149507,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3733,'PEQUI',3149606,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3734,'PEQUIZEIRO',1716653,27);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3735,'PERDIGÃO',3149705,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3736,'PERDIZES',3149804,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3737,'PERDÕES',3149903,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3738,'PEREIRA BARRETO',3537404,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3739,'PEREIRAS',3537503,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3740,'PEREIRO',2310803,6);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3741,'PERI MIRIM',2108405,10);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3742,'PERIQUITO',3149952,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3743,'PERITIBA',4212601,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3744,'PERITORÓ',2108454,10);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3745,'PEROBAL',4118857,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3746,'PÉROLA',4118907,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3747,'PÉROLA D''OESTE',4119004,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3748,'PEROLÂNDIA',5216452,9);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3749,'PERUÍBE',3537602,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3750,'PESCADOR',3150000,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3751,'PESQUEIRA',2610905,17);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3752,'PETROLÂNDIA',2611002,17);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3753,'PETROLÂNDIA',4212700,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3754,'PETROLINA',2611101,17);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3755,'PETROLINA DE GOIÁS',5216809,9);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3756,'PETRÓPOLIS',3303906,19);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3757,'PIAÇABUÇU',2706802,2);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3758,'PIACATU',3537701,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3759,'PIANCÓ',2511301,15);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3760,'PIATÃ',2924306,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3761,'PIAU',3150109,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3762,'PICADA CAFÉ',4314423,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3763,'PIÇARRA',1505635,14);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3764,'PIÇARRAS',4212809,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3765,'PICOS',2208007,18);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3766,'PICUÍ',2511400,15);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3767,'PIEDADE',3537800,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3768,'PIEDADE DE CARATINGA',3150158,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3769,'PIEDADE DE PONTE NOVA',3150208,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3770,'PIEDADE DO RIO GRANDE',3150307,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3771,'PIEDADE DOS GERAIS',3150406,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3772,'PIÊN',4119103,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3773,'PILÃO ARCADO',2924405,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3774,'PILAR',2706901,2);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3775,'PILAR',2511509,15);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3776,'PILAR DE GOIÁS',5216908,9);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3777,'PILAR DO SUL',3537909,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3778,'PILÕES',2511608,15);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3779,'PILÕES',2410009,20);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3780,'PILÕEZINHOS',2511707,15);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3781,'PIMENTA',3150505,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3782,'PIMENTA BUENO',1100189,22);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3783,'PIMENTEIRAS',2208106,18);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3784,'PIMENTEIRAS DO OESTE',1101468,22);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3785,'PINDAÍ',2924504,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3786,'PINDAMONHANGABA',3538006,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3787,'PINDARÉ MIRIM',2108504,10);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3788,'PINDOBA',2707008,2);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3789,'PINDOBAÇU',2924603,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3790,'PINDORAMA',3538105,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3791,'PINDORAMA DO TOCANTINS',1717008,27);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3792,'PINDORETAMA',2310852,6);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3793,'PINGO D''ÁGUA',3150539,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3794,'PINHAIS',4119152,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3795,'PINHAL',4314456,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3796,'PINHAL DA SERRA',4314464,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3797,'PINHAL DE SÃO BENTO',4119251,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3798,'PINHAL GRANDE',4314472,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3799,'PINHALÃO',4119202,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3800,'PINHALZINHO',3538204,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3801,'PINHALZINHO',4212908,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3802,'PINHÃO',2805208,26);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3803,'PINHÃO',4119301,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3804,'PINHEIRAL',3303955,19);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3805,'PINHEIRINHO DO VALE',4314498,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3806,'PINHEIRO',2108603,10);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3807,'PINHEIRO MACHADO',4314506,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3808,'PINHEIRO PRETO',4213005,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3809,'PINHEIROS',3204104,8);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3810,'PINTADAS',2924652,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3811,'PINTO BANDEIRA',4314530,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3812,'PINTÓPOLIS',3150570,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3813,'PIO IX',2208205,18);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3814,'PIO XII',2108702,10);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3815,'PIQUEROBI',3538303,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3816,'PIQUET CARNEIRO',2310902,6);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3817,'PIQUETE',3538501,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3818,'PIRACAIA',3538600,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3819,'PIRACANJUBA',5217104,9);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3820,'PIRACEMA',3150604,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3821,'PIRACICABA',3538709,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3822,'PIRACURUCA',2208304,18);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3823,'PIRAÍ',3304003,19);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3824,'PIRAÍ DO NORTE',2924678,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3825,'PIRAÍ DO SUL',4119400,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3826,'PIRAJU',3538808,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3827,'PIRAJUBA',3150703,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3828,'PIRAJUÍ',3538907,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3829,'PIRAMBU',2805307,26);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3830,'PIRANGA',3150802,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3831,'PIRANGI',3539004,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3832,'PIRANGUÇU',3150901,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3833,'PIRANGUINHO',3151008,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3834,'PIRANHAS',2707107,2);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3835,'PIRANHAS',5217203,9);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3836,'PIRAPEMAS',2108801,10);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3837,'PIRAPETINGA',3151107,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3838,'PIRAPÓ',4314555,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3839,'PIRAPORA',3151206,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3840,'PIRAPORA DO BOM JESUS',3539103,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3841,'PIRAPOZINHO',3539202,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3842,'PIRAQUARA',4119509,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3843,'PIRAQUÊ',1717206,27);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3844,'PIRASSUNUNGA',3539301,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3845,'PIRATINI',4314605,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3846,'PIRATININGA',3539400,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3847,'PIRATUBA',4213104,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3848,'PIRAÚBA',3151305,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3849,'PIRENÓPOLIS',5217302,9);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3850,'PIRES DO RIO',5217401,9);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3851,'PIRES FERREIRA',2310951,6);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3852,'PIRIPÁ',2924702,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3853,'PIRIPIRI',2208403,18);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3854,'PIRITIBA',2924801,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3855,'PIRPIRITUBA',2511806,15);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3856,'PITANGA',4119608,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3857,'PITANGUEIRAS',3539509,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3858,'PITANGUEIRAS',4119657,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3859,'PITANGUI',3151404,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3860,'PITIMBU',2511905,15);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3861,'PIUÍ',3151503,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3862,'PIUM',1717503,27);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3863,'PIÚMA',3204203,8);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3864,'PLACAS',1505650,14);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3865,'PLÁCIDO DE CASTRO',1200385,1);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3866,'PLANALTINA',5217609,9);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3867,'PLANALTINA DO PARANÁ',4119707,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3868,'PLANALTINO',2924900,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3869,'PLANALTO',2925006,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3870,'PLANALTO',4314704,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3871,'PLANALTO',3539608,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3872,'PLANALTO',4119806,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3873,'PLANALTO ALEGRE',4213153,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3874,'PLANALTO DA SERRA',5106455,11);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3875,'PLANURA',3151602,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3876,'PLATINA',3539707,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3877,'POÁ',3539806,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3878,'POÇÃO',2611200,17);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3879,'POÇÃO DE PEDRAS',2108900,10);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3880,'POCINHOS',2512002,15);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3881,'POÇO BRANCO',2410108,20);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3882,'POÇO DANTAS',2512036,15);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3883,'POÇO DAS ANTAS',4314753,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3884,'POÇO DAS TRINCHEIRAS',2707206,2);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3885,'POÇO DE JOSÉ DE MOURA',2512077,15);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3886,'POÇO FUNDO',3151701,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3887,'POÇO REDONDO',2805406,26);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3888,'POÇO VERDE',2805505,26);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3889,'POÇÕES',2925105,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3890,'POCONÉ',5106505,11);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3891,'POÇOS DE CALDAS',3151800,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3892,'POCRANE',3151909,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3893,'POJUCA',2925204,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3894,'POLONI',3539905,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3895,'POMBAL',2512101,15);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3896,'POMBOS',2611309,17);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3897,'POMERODE',4213203,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3898,'POMPÉIA',3540002,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3899,'POMPÉU',3152006,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3900,'PONGAÍ',3540101,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3901,'PONTA DE PEDRAS',1505700,14);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3902,'PONTA GROSSA',4119905,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3903,'PONTA PORÃ',5006606,12);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3904,'PONTAL',3540200,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3905,'PONTAL DO ARAGUAIA',5106653,11);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3906,'PONTAL DO PARANÁ',4119954,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3907,'PONTALINA',5217708,9);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3908,'PONTALINDA',3540259,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3909,'PONTÃO',4314779,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3910,'PONTE ALTA',4213302,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3911,'PONTE ALTA DO BOM JESUS',1717800,27);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3912,'PONTE ALTA DO NORTE',4213351,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3913,'PONTE ALTA DO TOCANTINS',1717909,27);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3914,'PONTE BRANCA',5106703,11);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3915,'PONTE NOVA',3152105,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3916,'PONTE PRETA',4314787,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3917,'PONTE SERRADA',4213401,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3918,'PONTES E LACERDA',5106752,11);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3919,'PONTES GESTAL',3540309,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3920,'PONTO BELO',3204252,8);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3921,'PONTO CHIQUE',3152131,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3922,'PONTO DOS VOLANTES',3152170,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3923,'PONTO NOVO',2925253,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3924,'POPULINA',3540408,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3925,'PORANGA',2311009,6);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3926,'PORANGABA',3540507,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3927,'PORANGATU',5218003,9);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3928,'PORCIÚNCULA',3304102,19);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3929,'PORECATU',4120002,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3930,'PORTALEGRE',2410207,20);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3931,'PORTÃO',4314803,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3932,'PORTEIRÃO',5218052,9);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3933,'PORTEIRAS',2311108,6);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3934,'PORTEIRINHA',3152204,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3935,'PORTEL',1505809,14);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3936,'PORTELÂNDIA',5218102,9);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3937,'PORTO',2208502,18);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3938,'PORTO ACRE',1200807,1);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3939,'PORTO ALEGRE',4314902,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3940,'PORTO ALEGRE DO NORTE',5106778,11);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3941,'PORTO ALEGRE DO PIAUÍ',2208551,18);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3942,'PORTO ALEGRE DO TOCANTINS',1718006,27);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3943,'PORTO AMAZONAS',4120101,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3944,'PORTO BARREIRO',4120150,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3945,'PORTO BELO',4213500,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3946,'PORTO CALVO',2707305,2);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3947,'PORTO DA FOLHA',2805604,26);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3948,'PORTO DE MOZ',1505908,14);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3949,'PORTO DE PEDRAS',2707404,2);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3950,'PORTO DO MANGUE',2410256,20);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3951,'PORTO DOS GAÚCHOS',5106802,11);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3952,'PORTO ESPERIDIÃO',5106828,11);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3953,'PORTO ESTRELA',5106851,11);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3954,'PORTO FELIZ',3540606,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3955,'PORTO FERREIRA',3540705,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3956,'PORTO FIRME',3152303,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3957,'PORTO FRANCO',2109007,10);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3958,'PORTO GRANDE',1600535,3);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3959,'PORTO LUCENA',4315008,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3960,'PORTO MAUÁ',4315057,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3961,'PORTO MURTINHO',5006903,12);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3962,'PORTO NACIONAL',1718204,27);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3963,'PORTO REAL',3304110,19);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3964,'PORTO REAL DO COLÉGIO',2707503,2);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3965,'PORTO RICO',4120200,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3966,'PORTO RICO DO MARANHÃO',2109056,10);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3967,'PORTO SEGURO',2925303,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3968,'PORTO UNIÃO',4213609,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3969,'PORTO VELHO',1100205,22);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3970,'PORTO VERA CRUZ',4315073,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3971,'PORTO VITÓRIA',4120309,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3972,'PORTO WALTER',1200393,1);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3973,'PORTO XAVIER',4315107,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3974,'POSSE',5218300,9);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3975,'POTÉ',3152402,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3976,'POTENGI',2311207,6);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3977,'POTIM',3540754,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3978,'POTIRAGUÁ',2925402,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3979,'POTIRENDABA',3540804,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3980,'POTIRETAMA',2311231,6);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3981,'POUSO ALEGRE',3152501,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3982,'POUSO ALTO',3152600,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3983,'POUSO NOVO',4315131,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3984,'POUSO REDONDO',4213708,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3985,'POXORÉO',5107008,11);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3986,'PRACINHA',3540853,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3987,'PRACUÚBA',1600550,3);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3988,'PRADO',2925501,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3989,'PRADO FERREIRA',4120333,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3990,'PRADÓPOLIS',3540903,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3991,'PRADOS',3152709,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3992,'PRAIA GRANDE',3541000,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3993,'PRAIA GRANDE',4213807,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3994,'PRAIA NORTE',1718303,27);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3995,'PRAINHA',1506005,14);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3996,'PRANCHITA',4120358,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3997,'PRATA',2512200,15);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3998,'PRATA',3152808,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (3999,'PRATA DO PIAUÍ',2208601,18);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4000,'PRATÂNIA',3541059,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4001,'PRATÁPOLIS',3152907,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4002,'PRATINHA',3153004,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4003,'PRESIDENTE ALVES',3541109,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4004,'PRESIDENTE BERNARDES',3541208,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4005,'PRESIDENTE BERNARDES',3153103,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4006,'PRESIDENTE CASTELO BRANCO',4213906,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4007,'PRESIDENTE CASTELO BRANCO',4120408,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4008,'PRESIDENTE DUTRA',2925600,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4009,'PRESIDENTE DUTRA',2109106,10);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4010,'PRESIDENTE EPITÁCIO',3541307,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4011,'PRESIDENTE FIGUEIREDO',1303536,4);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4012,'PRESIDENTE GETÚLIO',4214003,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4013,'PRESIDENTE JÂNIO QUADROS',2925709,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4014,'PRESIDENTE JUSCELINO',2410306,20);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4015,'PRESIDENTE JUSCELINO',2109205,10);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4016,'PRESIDENTE JUSCELINO',3153202,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4017,'PRESIDENTE KENNEDY',3204302,8);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4018,'PRESIDENTE KENNEDY',1718402,27);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4019,'PRESIDENTE KUBITSCHEK',3153301,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4020,'PRESIDENTE LUCENA',4315149,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4021,'PRESIDENTE MÉDICI',1100254,22);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4022,'PRESIDENTE MÉDICI',2109239,10);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4023,'PRESIDENTE NEREU',4214102,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4024,'PRESIDENTE OLEGÁRIO',3153400,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4025,'PRESIDENTE PRUDENTE',3541406,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4026,'PRESIDENTE SARNEY',2109270,10);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4027,'PRESIDENTE TANCREDO NEVES',2925758,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4028,'PRESIDENTE VARGAS',2109304,10);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4029,'PRESIDENTE VENCESLAU',3541505,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4030,'PRIMAVERA',1506104,14);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4031,'PRIMAVERA',2611408,17);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4032,'PRIMAVERA DE RONDÔNIA',1101476,22);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4033,'PRIMAVERA DO LESTE',5107040,11);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4034,'PRIMEIRA CRUZ',2109403,10);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4035,'PRIMEIRO DE MAIO',4120507,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4036,'PRINCESA',4214151,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4037,'PRINCESA ISABEL',2512309,15);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4038,'PROFESSOR JAMIL',5218391,9);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4039,'PROGRESSO',4315156,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4040,'PROMISSÃO',3541604,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4041,'PROPRIÁ',2805703,26);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4042,'PROTÁSIO ALVES',4315172,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4043,'PRUDENTE DE MORAIS',3153608,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4044,'PRUDENTÓPOLIS',4120606,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4045,'PUGMIL',1718451,27);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4046,'PUREZA',2410405,20);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4047,'PUTINGA',4315206,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4048,'PUXINANÃ',2512408,15);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4049,'QUADRA',3541653,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4050,'QUARAÍ',4315305,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4051,'QUARTEL GERAL',3153707,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4052,'QUARTO CENTENÁRIO',4120655,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4053,'QUATÁ',3541703,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4054,'QUATIGUÁ',4120705,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4055,'QUATIPURU',1506112,14);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4056,'QUATIS',3304128,19);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4057,'QUATRO BARRAS',4120804,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4058,'QUATRO IRMÃOS',4315313,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4059,'QUATRO PONTES',4120853,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4060,'QUEBRANGULO',2707602,2);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4061,'QUEDAS DO IGUAÇU',4120903,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4062,'QUEIMADA NOVA',2208650,18);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4063,'QUEIMADAS',2925808,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4064,'QUEIMADAS',2512507,15);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4065,'QUEIMADOS',3304144,19);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4066,'QUEIROZ',3541802,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4067,'QUELUZ',3541901,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4068,'QUELUZITA',3153806,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4069,'QUERÊNCIA',5107065,11);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4070,'QUERÊNCIA DO NORTE',4121000,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4071,'QUEVEDOS',4315321,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4072,'QUIJINGUE',2925907,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4073,'QUILOMBO',4214201,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4074,'QUINTA DO SOL',4121109,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4075,'QUINTANA',3542008,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4076,'QUINZE DE NOVEMBRO',4315354,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4077,'QUIPAPÁ',2611507,17);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4078,'QUIRINÓPOLIS',5218508,9);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4079,'QUISSAMÃ',3304151,19);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4080,'QUITANDINHA',4121208,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4081,'QUITERIANÓPOLIS',2311264,6);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4082,'QUIXABÁ',2611533,17);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4083,'QUIXABÁ',2512606,15);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4084,'QUIXABEIRA',2925931,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4085,'QUIXADÁ',2311306,6);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4086,'QUIXELÔ',2311355,6);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4087,'QUIXERAMOBIM',2311405,6);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4088,'QUIXERÉ',2311504,6);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4089,'RAFAEL FERNANDES',2410504,20);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4090,'RAFAEL GODEIRO',2410603,20);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4091,'RAFAEL JAMBEIRO',2925956,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4092,'RAFARD',3542107,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4093,'RAMILÂNDIA',4121257,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4094,'RANCHARIA',3542206,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4095,'RANCHO ALEGRE',4121307,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4096,'RANCHO ALEGRE D''OESTE',4121356,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4097,'RANCHO QUEIMADO',4214300,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4098,'RAPOSA',2109452,10);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4099,'RAPOSOS',3153905,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4100,'RAUL SOARES',3154002,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4101,'REALEZA',4121406,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4102,'REBOUÇAS',4121505,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4103,'RECIFE',2611606,17);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4104,'RECREIO',3154101,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4105,'RECURSOLÂNDIA',1718501,27);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4106,'REDENÇÃO',1506138,14);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4107,'REDENÇÃO',2311603,6);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4108,'REDENÇÃO DA SERRA',3542305,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4109,'REDENÇÃO DO GURGUÉIA',2208700,18);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4110,'REDENTORA',4315404,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4111,'REDUTO',3154150,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4112,'REGENERAÇÃO',2208809,18);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4113,'REGENTE FEIJÓ',3542404,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4114,'REGINÓPOLIS',3542503,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4115,'REGISTRO',3542602,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4116,'RELVADO',4315453,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4117,'REMANSO',2926004,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4118,'REMÍGIO',2512705,15);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4119,'RENASCENÇA',4121604,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4120,'RERIUTABA',2311702,6);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4121,'RESENDE',3304201,19);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4122,'RESENDE COSTA',3154200,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4123,'RESERVA',4121703,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4124,'RESERVA DO CABAÇAL',5107156,11);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4125,'RESERVA DO IGUAÇU',4121752,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4126,'RESPLENDOR',3154309,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4127,'RESSAQUINHA',3154408,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4128,'RESTINGA',3542701,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4129,'RESTINGA SECA',4315503,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4130,'RETIROLÂNDIA',2926103,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4131,'RIACHÃO',2512747,15);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4132,'RIACHÃO',2109502,10);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4133,'RIACHÃO DAS NEVES',2926202,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4134,'RIACHÃO DO BACAMARTE',2512754,15);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4135,'RIACHÃO DO DANTAS',2805802,26);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4136,'RIACHÃO DO JACUÍPE',2926301,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4137,'RIACHÃO DO POÇO',2512762,15);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4138,'RIACHINHO',3154457,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4139,'RIACHINHO',1718550,27);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4140,'RIACHO DA CRUZ',2410702,20);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4141,'RIACHO DAS ALMAS',2611705,17);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4142,'RIACHO DE SANTANA',2926400,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4143,'RIACHO DE SANTANA',2410801,20);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4144,'RIACHO DE SANTO ANTÔNIO',2512788,15);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4145,'RIACHO DOS CAVALOS',2512804,15);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4146,'RIACHO DOS MACHADOS',3154507,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4147,'RIACHO FRIO',2208858,18);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4148,'RIACHUELO',2805901,26);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4149,'RIACHUELO',2410900,20);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4150,'RIALMA',5218607,9);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4151,'RIANÁPOLIS',5218706,9);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4152,'RIBAMAR FIQUENE',2109551,10);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4153,'RIBAS DO RIO PARDO',5007109,12);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4154,'RIBEIRA',3542800,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4155,'RIBEIRA DO AMPARO',2926509,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4156,'RIBEIRA DO PIAUÍ',2208874,18);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4157,'RIBEIRA DO POMBAL',2926608,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4158,'RIBEIRÃO',2611804,17);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4159,'RIBEIRÃO BONITO',3542909,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4160,'RIBEIRÃO BRANCO',3543006,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4161,'RIBEIRÃO CASCALHEIRA',5107180,11);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4162,'RIBEIRÃO CLARO',4121802,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4163,'RIBEIRÃO CORRENTE',3543105,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4164,'RIBEIRÃO DAS NEVES',3154606,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4165,'RIBEIRÃO DO LARGO',2926657,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4166,'RIBEIRÃO DO PINHAL',4121901,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4167,'RIBEIRÃO DO SUL',3543204,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4168,'RIBEIRÃO DOS ÍNDIOS',3543238,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4169,'RIBEIRÃO GRANDE',3543253,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4170,'RIBEIRÃO PIRES',3543303,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4171,'RIBEIRÃO PRETO',3543402,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4172,'RIBEIRÃO VERMELHO',3154705,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4173,'RIBEIRÃOZINHO',5107198,11);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4174,'RIBEIRO GONÇALVES',2208908,18);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4175,'RIBEIRÓPOLIS',2806008,26);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4176,'RIFAINA',3543600,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4177,'RINCÃO',3543709,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4178,'RINÓPOLIS',3543808,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4179,'RIO ACIMA',3154804,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4180,'RIO AZUL',4122008,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4181,'RIO BANANAL',3204351,8);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4182,'RIO BOM',4122107,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4183,'RIO BONITO',3304300,19);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4184,'RIO BONITO DO IGUAÇU',4122156,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4185,'RIO BRANCO',5107206,11);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4186,'RIO BRANCO',1200401,1);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4187,'RIO BRANCO DO IVAÍ',4122172,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4188,'RIO BRANCO DO SUL',4122206,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4189,'RIO BRILHANTE',5007208,12);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4190,'RIO CASCA',3154903,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4191,'RIO CLARO',3304409,19);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4192,'RIO CLARO',3543907,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4193,'RIO CRESPO',1100262,22);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4194,'RIO DA CONCEIÇÃO',1718659,27);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4195,'RIO DAS ANTAS',4214409,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4196,'RIO DAS FLORES',3304508,19);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4197,'RIO DAS OSTRAS',3304524,19);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4198,'RIO DAS PEDRAS',3544004,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4199,'RIO DE CONTAS',2926707,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4200,'RIO DE JANEIRO',3304557,19);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4201,'RIO DO ANTÔNIO',2926806,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4202,'RIO DO CAMPO',4214508,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4203,'RIO DO FOGO',2408953,20);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4204,'RIO DO OESTE',4214607,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4205,'RIO DO PIRES',2926905,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4206,'RIO DO PRADO',3155108,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4207,'RIO DO SUL',4214805,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4208,'RIO DOCE',3155009,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4209,'RIO DOS BOIS',1718709,27);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4210,'RIO DOS CEDROS',4214706,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4211,'RIO DOS ÍNDIOS',4315552,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4212,'RIO ESPERA',3155207,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4213,'RIO FORMOSO',2611903,17);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4214,'RIO FORTUNA',4214904,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4215,'RIO GRANDE',4315602,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4216,'RIO GRANDE DA SERRA',3544103,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4217,'RIO GRANDE DO PIAUÍ',2209005,18);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4218,'RIO LARGO',2707701,2);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4219,'RIO MANSO',3155306,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4220,'RIO MARIA',1506161,14);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4221,'RIO NEGRINHO',4215000,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4222,'RIO NEGRO',5007307,12);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4223,'RIO NEGRO',4122305,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4224,'RIO NOVO',3155405,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4225,'RIO NOVO DO SUL',3204401,8);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4226,'RIO PARANAÍBA',3155504,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4227,'RIO PARDO',4315701,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4228,'RIO PARDO DE MINAS',3155603,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4229,'RIO PIRACICABA',3155702,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4230,'RIO POMBA',3155801,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4231,'RIO PRETO',3155900,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4232,'RIO PRETO DA EVA',1303569,4);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4233,'RIO QUENTE',5218789,9);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4234,'RIO REAL',2927002,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4235,'RIO RUFINO',4215059,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4236,'RIO SONO',1718758,27);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4237,'RIO TINTO',2512903,15);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4238,'RIO VERDE',5218805,9);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4239,'RIO VERDE DE MATO GROSSO',5007406,12);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4240,'RIO VERMELHO',3156007,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4241,'RIOLÂNDIA',3544202,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4242,'RIOZINHO',4315750,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4243,'RIQUEZA',4215075,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4244,'RITÁPOLIS',3156106,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4245,'RIVERSUL',3543501,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4246,'ROCA SALES',4315800,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4247,'ROCHEDO',5007505,12);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4248,'ROCHEDO DE MINAS',3156205,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4249,'RODEIO',4215109,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4250,'RODEIO BONITO',4315909,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4251,'RODEIRO',3156304,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4252,'RODELAS',2927101,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4253,'RODOLFO FERNANDES',2411007,20);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4254,'RODRIGUES ALVES',1200427,1);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4255,'ROLADOR',4315958,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4256,'ROLÂNDIA',4122404,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4257,'ROLANTE',4316006,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4258,'ROLIM DE MOURA',1100288,22);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4259,'ROMARIA',3156403,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4260,'ROMELÂNDIA',4215208,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4261,'RONCADOR',4122503,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4262,'RONDA ALTA',4316105,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4263,'RONDINHA',4316204,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4264,'RONDOLÂNDIA',5107578,11);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4265,'RONDON',4122602,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4266,'RONDON DO PARÁ',1506187,14);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4267,'RONDONÓPOLIS',5107602,11);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4268,'ROQUE GONZALES',4316303,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4269,'RORAINÓPOLIS',1400472,23);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4270,'ROSANA',3544251,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4271,'ROSÁRIO',2109601,10);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4272,'ROSÁRIO DA LIMEIRA',3156452,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4273,'ROSÁRIO DO CATETE',2806107,26);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4274,'ROSÁRIO DO IVAÍ',4122651,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4275,'ROSÁRIO DO SUL',4316402,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4276,'ROSÁRIO OESTE',5107701,11);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4277,'ROSEIRA',3544301,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4278,'ROTEIRO',2707800,2);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4279,'RUBELITA',3156502,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4280,'RUBIÁCEA',3544400,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4281,'RUBIATABA',5218904,9);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4282,'RUBIM',3156601,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4283,'RUBINÉIA',3544509,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4284,'RURÓPOLIS',1506195,14);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4285,'RUSSAS',2311801,6);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4286,'RUY BARBOSA',2927200,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4287,'RUY BARBOSA',2411106,20);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4288,'SABARÁ',3156700,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4289,'SABÁUDIA',4122701,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4290,'SABINO',3544608,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4291,'SABINÓPOLIS',3156809,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4292,'SABOEIRO',2311900,6);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4293,'SACRAMENTO',3156908,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4294,'SAGRADA FAMÍLIA',4316428,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4295,'SAGRES',3544707,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4296,'SAIRÉ',2612000,17);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4297,'SALDANHA MARINHO',4316436,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4298,'SALES',3544806,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4299,'SALES OLIVEIRA',3544905,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4300,'SALESÓPOLIS',3545001,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4301,'SALETE',4215307,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4302,'SALGADINHO',2612109,17);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4303,'SALGADINHO',2513000,15);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4304,'SALGADO',2806206,26);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4305,'SALGADO DE SÃO FÉLIX',2513109,15);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4306,'SALGADO FILHO',4122800,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4307,'SALGUEIRO',2612208,17);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4308,'SALINAS',3157005,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4309,'SALINAS DA MARGARIDA',2927309,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4310,'SALINÓPOLIS',1506203,14);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4311,'SALITRE',2311959,6);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4312,'SALMOURÃO',3545100,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4313,'SALOÁ',2612307,17);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4314,'SALTINHO',4215356,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4315,'SALTINHO',3545159,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4316,'SALTO',3545209,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4317,'SALTO DA DIVISA',3157104,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4318,'SALTO DE PIRAPORA',3545308,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4319,'SALTO DO CÉU',5107750,11);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4320,'SALTO DO ITARARÉ',4122909,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4321,'SALTO DO JACUÍ',4316451,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4322,'SALTO DO LONTRA',4123006,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4323,'SALTO GRANDE',3545407,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4324,'SALTO VELOSO',4215406,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4325,'SALVADOR',2927408,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4326,'SALVADOR DAS MISSÕES',4316477,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4327,'SALVADOR DO SUL',4316501,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4328,'SALVATERRA',1506302,14);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4329,'SAMBAÍBA',2109700,10);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4330,'SAMPAIO',1718808,27);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4331,'SANANDUVA',4316600,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4332,'SANCLERLÂNDIA',5219001,9);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4333,'SANDOLÂNDIA',1718840,27);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4334,'SANDOVALINA',3545506,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4335,'SANGÃO',4215455,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4336,'SANHARÓ',2612406,17);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4337,'SANTA ADÉLIA',3545605,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4338,'SANTA ALBERTINA',3545704,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4339,'SANTA AMÉLIA',4123105,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4340,'SANTA BÁRBARA',2927507,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4341,'SANTA BÁRBARA',3157203,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4342,'SANTA BÁRBARA D''OESTE',3545803,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4343,'SANTA BÁRBARA DE GOIÁS',5219100,9);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4344,'SANTA BÁRBARA DO LESTE',3157252,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4345,'SANTA BÁRBARA DO MONTE VERDE',3157278,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4346,'SANTA BÁRBARA DO PARÁ',1506351,14);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4347,'SANTA BÁRBARA DO SUL',4316709,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4348,'SANTA BÁRBARA DO TUGÚRIO',3157302,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4349,'SANTA BRANCA',3546009,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4350,'SANTA BRÍGIDA',2927606,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4351,'SANTA CARMEM',5107248,11);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4352,'SANTA CECÍLIA',4215505,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4353,'SANTA CECÍLIA',2513158,15);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4354,'SANTA CECÍLIA DO PAVÃO',4123204,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4355,'SANTA CECÍLIA DO SUL',4316733,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4356,'SANTA CLARA D''OESTE',3546108,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4357,'SANTA CLARA DO SUL',4316758,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4358,'SANTA CRUZ',2612455,17);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4359,'SANTA CRUZ',2513208,15);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4360,'SANTA CRUZ',2411205,20);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4361,'SANTA CRUZ CABRÁLIA',2927705,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4362,'SANTA CRUZ DA BAIXA VERDE',2612471,17);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4363,'SANTA CRUZ DA CONCEIÇÃO',3546207,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4364,'SANTA CRUZ DA ESPERANÇA',3546256,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4365,'SANTA CRUZ DA VITÓRIA',2927804,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4366,'SANTA CRUZ DAS PALMEIRAS',3546306,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4367,'SANTA CRUZ DE GOIÁS',5219209,9);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4368,'SANTA CRUZ DE MINAS',3157336,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4369,'SANTA CRUZ DE MONTE CASTELO',4123303,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4370,'SANTA CRUZ DE SALINAS',3157377,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4371,'SANTA CRUZ DO ARARI',1506401,14);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4372,'SANTA CRUZ DO CAPIBARIBE',2612505,17);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4373,'SANTA CRUZ DO ESCALVADO',3157401,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4374,'SANTA CRUZ DO PIAUÍ',2209104,18);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4375,'SANTA CRUZ DO RIO PARDO',3546405,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4376,'SANTA CRUZ DO SUL',4316808,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4377,'SANTA CRUZ DO XINGU',5107743,11);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4378,'SANTA CRUZ DOS MILAGRES',2209153,18);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4379,'SANTA EFIGÊNIA DE MINAS',3157500,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4380,'SANTA ERNESTINA',3546504,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4381,'SANTA FÉ',4123402,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4382,'SANTA FÉ DE GOIÁS',5219258,9);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4383,'SANTA FÉ DE MINAS',3157609,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4384,'SANTA FÉ DO ARAGUAIA',1718865,27);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4385,'SANTA FÉ DO SUL',3546603,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4386,'SANTA FILOMENA',2612554,17);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4387,'SANTA FILOMENA',2209203,18);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4388,'SANTA FILOMENA DO MARANHÃO',2109759,10);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4389,'SANTA GERTRUDES',3546702,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4390,'SANTA HELENA',4215554,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4391,'SANTA HELENA',2513307,15);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4392,'SANTA HELENA',4123501,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4393,'SANTA HELENA',2109809,10);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4394,'SANTA HELENA DE GOIÁS',5219308,9);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4395,'SANTA HELENA DE MINAS',3157658,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4396,'SANTA INÊS',2927903,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4397,'SANTA INÊS',2513356,15);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4398,'SANTA INÊS',4123600,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4399,'SANTA INÊS',2109908,10);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4400,'SANTA ISABEL',3546801,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4401,'SANTA ISABEL',5219357,9);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4402,'SANTA ISABEL DO IVAÍ',4123709,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4403,'SANTA ISABEL DO PARÁ',1506500,14);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4404,'SANTA ISABEL DO RIO NEGRO',1303601,4);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4405,'SANTA IZABEL DO OESTE',4123808,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4406,'SANTA JULIANA',3157708,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4407,'SANTA LEOPOLDINA',3204500,8);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4408,'SANTA LÚCIA',3546900,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4409,'SANTA LÚCIA',4123824,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4410,'SANTA LUZ',2209302,18);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4411,'SANTA LUZIA',2928059,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4412,'SANTA LUZIA',2513406,15);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4413,'SANTA LUZIA',2110005,10);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4414,'SANTA LUZIA',3157807,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4415,'SANTA LUZIA D''OESTE',1100296,22);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4416,'SANTA LUZIA DO ITANHY',2806305,26);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4417,'SANTA LUZIA DO NORTE',2707909,2);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4418,'SANTA LUZIA DO PARÁ',1506559,14);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4419,'SANTA LUZIA DO PARUÁ',2110039,10);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4420,'SANTA MARGARIDA',3157906,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4421,'SANTA MARGARIDA DO SUL',4316972,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4422,'SANTA MARIA',4316907,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4423,'SANTA MARIA',2409332,20);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4424,'SANTA MARIA DA BOA VISTA',2612604,17);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4425,'SANTA MARIA DA SERRA',3547007,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4426,'SANTA MARIA DA VITÓRIA',2928109,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4427,'SANTA MARIA DAS BARREIRAS',1506583,14);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4428,'SANTA MARIA DE ITABIRA',3158003,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4429,'SANTA MARIA DE JETIBÁ',3204559,8);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4430,'SANTA MARIA DO CAMBUCÁ',2612703,17);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4431,'SANTA MARIA DO HERVAL',4316956,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4432,'SANTA MARIA DO OESTE',4123857,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4433,'SANTA MARIA DO PARÁ',1506609,14);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4434,'SANTA MARIA DO SALTO',3158102,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4435,'SANTA MARIA DO SUAÇUÍ',3158201,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4436,'SANTA MARIA DO TOCANTINS',1718881,27);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4437,'SANTA MARIA MADALENA',3304607,19);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4438,'SANTA MARIANA',4123907,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4439,'SANTA MERCEDES',3547106,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4440,'SANTA MÔNICA',4123956,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4441,'SANTA QUITÉRIA',2312205,6);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4442,'SANTA QUITÉRIA DO MARANHÃO',2110104,10);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4443,'SANTA RITA',2513703,15);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4444,'SANTA RITA',2110203,10);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4445,'SANTA RITA D''OESTE',3547403,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4446,'SANTA RITA DE CALDAS',3159209,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4447,'SANTA RITA DE CÁSSIA',2928406,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4448,'SANTA RITA DE IBITIPOCA',3159407,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4449,'SANTA RITA DE JACUTINGA',3159308,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4450,'SANTA RITA DE MINAS',3159357,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4451,'SANTA RITA DO ARAGUAIA',5219407,9);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4452,'SANTA RITA DO ITUETO',3159506,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4453,'SANTA RITA DO NOVO DESTINO',5219456,9);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4454,'SANTA RITA DO PARDO',5007554,12);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4455,'SANTA RITA DO PASSA QUATRO',3547502,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4456,'SANTA RITA DO SAPUCAÍ',3159605,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4457,'SANTA RITA DO TOCANTINS',1718899,27);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4458,'SANTA RITA DO TRIVELATO',5107768,11);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4459,'SANTA ROSA',4317202,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4460,'SANTA ROSA DA SERRA',3159704,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4461,'SANTA ROSA DE GOIÁS',5219506,9);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4462,'SANTA ROSA DE LIMA',4215604,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4463,'SANTA ROSA DE LIMA',2806503,26);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4464,'SANTA ROSA DE VITERBO',3547601,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4465,'SANTA ROSA DO PIAUÍ',2209377,18);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4466,'SANTA ROSA DO PURUS',1200435,1);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4467,'SANTA ROSA DO SUL',4215653,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4468,'SANTA ROSA DO TOCANTINS',1718907,27);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4469,'SANTA SALETE',3547650,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4470,'SANTA TERESA',3204609,8);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4471,'SANTA TERESINHA',2928505,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4472,'SANTA TERESINHA',2513802,15);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4473,'SANTA TEREZA',4317251,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4474,'SANTA TEREZA DE GOIÁS',5219605,9);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4475,'SANTA TEREZA DO OESTE',4124020,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4476,'SANTA TEREZA DO TOCANTINS',1719004,27);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4477,'SANTA TEREZINHA',4215679,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4478,'SANTA TEREZINHA',2612802,17);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4479,'SANTA TEREZINHA',5107776,11);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4480,'SANTA TEREZINHA DE GOIÁS',5219704,9);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4481,'SANTA TEREZINHA DE ITAIPU',4124053,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4482,'SANTA TEREZINHA DO PROGRESSO',4215687,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4483,'SANTA TEREZINHA DO TOCANTINS',1720002,27);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4484,'SANTA VITÓRIA',3159803,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4485,'SANTA VITÓRIA DO PALMAR',4317301,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4486,'SANTALUZ',2928000,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4487,'SANTANA',1600600,3);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4488,'SANTANA',2928208,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4489,'SANTANA DA BOA VISTA',4317004,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4490,'SANTANA DA PONTE PENSA',3547205,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4491,'SANTANA DA VARGEM',3158300,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4492,'SANTANA DE CATAGUASES',3158409,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4493,'SANTANA DE MANGUEIRA',2513505,15);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4494,'SANTANA DE PARNAÍBA',3547304,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4495,'SANTANA DE PIRAPAMA',3158508,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4496,'SANTANA DO ACARAÚ',2312007,6);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4497,'SANTANA DO ARAGUAIA',1506708,14);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4498,'SANTANA DO CARIRI',2312106,6);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4499,'SANTANA DO DESERTO',3158607,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4500,'SANTANA DO GARAMBÉU',3158706,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4501,'SANTANA DO IPANEMA',2708006,2);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4502,'SANTANA DO ITARARÉ',4124004,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4503,'SANTANA DO JACARÉ',3158805,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4504,'SANTANA DO LIVRAMENTO',4317103,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4505,'SANTANA DO MANHUAÇU',3158904,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4506,'SANTANA DO MARANHÃO',2110237,10);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4507,'SANTANA DO MATOS',2411403,20);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4508,'SANTANA DO MUNDAÚ',2708105,2);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4509,'SANTANA DO PARAÍSO',3158953,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4510,'SANTANA DO PIAUÍ',2209351,18);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4511,'SANTANA DO RIACHO',3159001,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4512,'SANTANA DO SÃO FRANCISCO',2806404,26);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4513,'SANTANA DO SERIDÓ',2411429,20);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4514,'SANTANA DOS GARROTES',2513604,15);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4515,'SANTANA DOS MONTES',3159100,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4516,'SANTANÓPOLIS',2928307,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4517,'SANTARÉM',1506807,14);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4518,'SANTARÉM',2513653,15);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4519,'SANTARÉM NOVO',1506906,14);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4520,'SANTIAGO',4317400,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4521,'SANTIAGO DO SUL',4215695,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4522,'SANTO AFONSO',5107263,11);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4523,'SANTO AMARO',2928604,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4524,'SANTO AMARO DA IMPERATRIZ',4215703,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4525,'SANTO AMARO DAS BROTAS',2806602,26);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4526,'SANTO AMARO DO MARANHÃO',2110278,10);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4527,'SANTO ANASTÁCIO',3547700,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4528,'SANTO ANDRÉ',2513851,15);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4529,'SANTO ANDRÉ',3547809,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4530,'SANTO ÂNGELO',4317509,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4531,'SANTO ANTÔNIO',2411502,20);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4532,'SANTO ANTÔNIO DA ALEGRIA',3547908,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4533,'SANTO ANTÔNIO DA BARRA',5219712,9);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4534,'SANTO ANTÔNIO DA PATRULHA',4317608,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4535,'SANTO ANTÔNIO DA PLATINA',4124103,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4536,'SANTO ANTÔNIO DAS MISSÕES',4317707,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4537,'SANTO ANTÔNIO DE GOIÁS',5219738,9);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4538,'SANTO ANTÔNIO DE JESUS',2928703,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4539,'SANTO ANTÔNIO DE LISBOA',2209401,18);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4540,'SANTO ANTÔNIO DE PÁDUA',3304706,19);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4541,'SANTO ANTÔNIO DE POSSE',3548005,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4542,'SANTO ANTÔNIO DO AMPARO',3159902,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4543,'SANTO ANTÔNIO DO ARACANGUÁ',3548054,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4544,'SANTO ANTÔNIO DO AVENTUREIRO',3160009,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4545,'SANTO ANTÔNIO DO CAIUÁ',4124202,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4546,'SANTO ANTÔNIO DO DESCOBERTO',5219753,9);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4547,'SANTO ANTÔNIO DO GRAMA',3160108,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4548,'SANTO ANTÔNIO DO IÇÁ',1303700,4);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4549,'SANTO ANTÔNIO DO ITAMBÉ',3160207,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4550,'SANTO ANTÔNIO DO JACINTO',3160306,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4551,'SANTO ANTÔNIO DO JARDIM',3548104,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4552,'SANTO ANTÔNIO DO LESTE',5107792,11);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4553,'SANTO ANTÔNIO DO LEVERGER',5107800,11);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4554,'SANTO ANTÔNIO DO MONTE',3160405,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4555,'SANTO ANTÔNIO DO PALMA',4317558,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4556,'SANTO ANTÔNIO DO PARAÍSO',4124301,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4557,'SANTO ANTÔNIO DO PINHAL',3548203,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4558,'SANTO ANTÔNIO DO PLANALTO',4317756,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4559,'SANTO ANTÔNIO DO RETIRO',3160454,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4560,'SANTO ANTÔNIO DO RIO ABAIXO',3160504,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4561,'SANTO ANTÔNIO DO SUDOESTE',4124400,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4562,'SANTO ANTÔNIO DO TAUÁ',1507003,14);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4563,'SANTO ANTÔNIO DOS LOPES',2110302,10);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4564,'SANTO ANTÔNIO DOS MILAGRES',2209450,18);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4565,'SANTO AUGUSTO',4317806,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4566,'SANTO CRISTO',4317905,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4567,'SANTO ESTEVÃO',2928802,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4568,'SANTO EXPEDITO',3548302,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4569,'SANTO EXPEDITO DO SUL',4317954,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4570,'SANTO HIPÓLITO',3160603,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4571,'SANTO INÁCIO',4124509,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4572,'SANTO INÁCIO DO PIAUÍ',2209500,18);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4573,'SANTÓPOLIS DO AGUAPEÍ',3548401,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4574,'SANTOS',3548500,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4575,'SANTOS DUMONT',3160702,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4576,'SÃO BENEDITO',2312304,6);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4577,'SÃO BENEDITO DO RIO PRETO',2110401,10);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4578,'SÃO BENEDITO DO SUL',2612901,17);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4579,'SÃO BENTINHO',2513927,15);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4580,'SÃO BENTO',2513901,15);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4581,'SÃO BENTO',2110500,10);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4582,'SÃO BENTO ABADE',3160801,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4583,'SÃO BENTO DO NORTE',2411601,20);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4584,'SÃO BENTO DO SAPUCAÍ',3548609,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4585,'SÃO BENTO DO SUL',4215802,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4586,'SÃO BENTO DO TOCANTINS',1720101,27);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4587,'SÃO BENTO DO TRAIRÍ',2411700,20);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4588,'SÃO BENTO DO UNA',2613008,17);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4589,'SÃO BERNARDINO',4215752,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4590,'SÃO BERNARDO',2110609,10);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4591,'SÃO BERNARDO DO CAMPO',3548708,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4592,'SÃO BONIFÁCIO',4215901,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4593,'SÃO BORJA',4318002,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4594,'SÃO BRÁS',2708204,2);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4595,'SÃO BRÁS DO SUAÇUÍ',3160900,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4596,'SÃO BRAZ DO PIAUÍ',2209559,18);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4597,'SÃO CAETANO DE ODIVELAS',1507102,14);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4598,'SÃO CAETANO DO SUL',3548807,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4599,'SÃO CAITANO',2613107,17);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4600,'SÃO CARLOS',4216008,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4601,'SÃO CARLOS',3548906,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4602,'SÃO CARLOS DO IVAÍ',4124608,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4603,'SÃO CRISTÓVÃO',2806701,26);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4604,'SÃO CRISTÓVÃO DO SUL',4216057,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4605,'SÃO DESIDÉRIO',2928901,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4606,'SÃO DOMINGOS',2928950,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4607,'SÃO DOMINGOS',4216107,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4608,'SÃO DOMINGOS',2806800,26);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4609,'SÃO DOMINGOS',5219803,9);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4610,'SÃO DOMINGOS DAS DORES',3160959,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4611,'SÃO DOMINGOS DE POMBAL',2513968,15);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4612,'SÃO DOMINGOS DO ARAGUAIA',1507151,14);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4613,'SÃO DOMINGOS DO AZEITÃO',2110658,10);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4614,'SÃO DOMINGOS DO CAPIM',1507201,14);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4615,'SÃO DOMINGOS DO CARIRI',2513943,15);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4616,'SÃO DOMINGOS DO MARANHÃO',2110708,10);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4617,'SÃO DOMINGOS DO NORTE',3204658,8);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4618,'SÃO DOMINGOS DO PRATA',3161007,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4619,'SÃO DOMINGOS DO SUL',4318051,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4620,'SÃO FELIPE',2929107,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4621,'SÃO FELIPE D''OESTE',1101484,22);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4622,'SÃO FÉLIX',2929008,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4623,'SÃO FÉLIX DE BALSAS',2110807,10);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4624,'SÃO FÉLIX DE MINAS',3161056,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4625,'SÃO FÉLIX DO ARAGUAIA',5107859,11);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4626,'SÃO FÉLIX DO CORIBE',2929057,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4627,'SÃO FÉLIX DO PIAUÍ',2209609,18);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4628,'SÃO FÉLIX DO TOCANTINS',1720150,27);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4629,'SÃO FÉLIX DO XINGU',1507300,14);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4630,'SÃO FERNANDO',2411809,20);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4631,'SÃO FIDÉLIS',3304805,19);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4632,'SÃO FRANCISCO',2806909,26);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4633,'SÃO FRANCISCO',2513984,15);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4634,'SÃO FRANCISCO',3549003,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4635,'SÃO FRANCISCO',3161106,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4636,'SÃO FRANCISCO DE ASSIS',4318101,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4637,'SÃO FRANCISCO DE ASSIS DO PIAUÍ',2209658,18);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4638,'SÃO FRANCISCO DE GOIÁS',5219902,9);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4639,'SÃO FRANCISCO DE ITABAPOANA',3304755,19);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4640,'SÃO FRANCISCO DE PAULA',4318200,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4641,'SÃO FRANCISCO DE PAULA',3161205,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4642,'SÃO FRANCISCO DE SALES',3161304,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4643,'SÃO FRANCISCO DO BREJÃO',2110856,10);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4644,'SÃO FRANCISCO DO CONDE',2929206,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4645,'SÃO FRANCISCO DO GLÓRIA',3161403,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4646,'SÃO FRANCISCO DO GUAPORÉ',1101492,22);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4647,'SÃO FRANCISCO DO MARANHÃO',2110906,10);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4648,'SÃO FRANCISCO DO OESTE',2411908,20);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4649,'SÃO FRANCISCO DO PARÁ',1507409,14);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4650,'SÃO FRANCISCO DO PIAUÍ',2209708,18);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4651,'SÃO FRANCISCO DO SUL',4216206,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4652,'SÃO GABRIEL',2929255,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4653,'SÃO GABRIEL',4318309,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4654,'SÃO GABRIEL DA CACHOEIRA',1303809,4);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4655,'SÃO GABRIEL DA PALHA',3204708,8);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4656,'SÃO GABRIEL DO OESTE',5007695,12);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4657,'SÃO GERALDO',3161502,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4658,'SÃO GERALDO DA PIEDADE',3161601,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4659,'SÃO GERALDO DO ARAGUAIA',1507458,14);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4660,'SÃO GERALDO DO BAIXIO',3161650,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4661,'SÃO GONÇALO',3304904,19);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4662,'SÃO GONÇALO DO ABAETÉ',3161700,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4663,'SÃO GONÇALO DO AMARANTE',2312403,6);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4664,'SÃO GONÇALO DO AMARANTE',2412005,20);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4665,'SÃO GONÇALO DO GURGUÉIA',2209757,18);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4666,'SÃO GONÇALO DO PARÁ',3161809,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4667,'SÃO GONÇALO DO PIAUÍ',2209807,18);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4668,'SÃO GONÇALO DO RIO ABAIXO',3161908,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4669,'SÃO GONÇALO DO RIO PRETO',3125507,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4670,'SÃO GONÇALO DO SAPUCAÍ',3162005,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4671,'SÃO GONÇALO DOS CAMPOS',2929305,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4672,'SÃO GOTARDO',3162104,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4673,'SÃO JERÔNIMO',4318408,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4674,'SÃO JERÔNIMO DA SERRA',4124707,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4675,'SÃO JOÃO',2613206,17);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4676,'SÃO JOÃO',4124806,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4677,'SÃO JOÃO BATISTA',4216305,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4678,'SÃO JOÃO BATISTA',2111003,10);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4679,'SÃO JOÃO BATISTA DO GLÓRIA',3162203,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4680,'SÃO JOÃO D''ALIANÇA',5220009,9);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4681,'SÃO JOÃO DA BALIZA',1400506,23);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4682,'SÃO JOÃO DA BARRA',3305000,19);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4683,'SÃO JOÃO DA BOA VISTA',3549102,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4684,'SÃO JOÃO DA CANABRAVA',2209856,18);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4685,'SÃO JOÃO DA FRONTEIRA',2209872,18);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4686,'SÃO JOÃO DA LAGOA',3162252,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4687,'SÃO JOÃO DA MATA',3162302,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4688,'SÃO JOÃO DA PARAÚNA',5220058,9);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4689,'SÃO JOÃO DA PONTA',1507466,14);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4690,'SÃO JOÃO DA PONTE',3162401,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4691,'SÃO JOÃO DA SERRA',2209906,18);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4692,'SÃO JOÃO DA URTIGA',4318424,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4693,'SÃO JOÃO DA VARJOTA',2209955,18);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4694,'SÃO JOÃO DAS DUAS PONTES',3549201,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4695,'SÃO JOÃO DAS MISSÕES',3162450,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4696,'SÃO JOÃO DE IRACEMA',3549250,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4697,'SÃO JOÃO DE MERITI',3305109,19);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4698,'SÃO JOÃO DE PIRABAS',1507474,14);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4699,'SÃO JOÃO DEL REI',3162500,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4700,'SÃO JOÃO DO ARAGUAIA',1507508,14);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4701,'SÃO JOÃO DO ARRAIAL',2209971,18);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4702,'SÃO JOÃO DO CAIUÁ',4124905,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4703,'SÃO JOÃO DO CARIRI',2514008,15);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4704,'SÃO JOÃO DO CARU',2111029,10);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4705,'SÃO JOÃO DO ITAPERIÚ',4216354,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4706,'SÃO JOÃO DO IVAÍ',4125001,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4707,'SÃO JOÃO DO JAGUARIBE',2312502,6);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4708,'SÃO JOÃO DO MANHUAÇU',3162559,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4709,'SÃO JOÃO DO MANTENINHA',3162575,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4710,'SÃO JOÃO DO OESTE',4216255,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4711,'SÃO JOÃO DO ORIENTE',3162609,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4712,'SÃO JOÃO DO PACUÍ',3162658,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4713,'SÃO JOÃO DO PARAÍSO',2111052,10);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4714,'SÃO JOÃO DO PARAÍSO',3162708,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4715,'SÃO JOÃO DO PAU D''ALHO',3549300,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4716,'SÃO JOÃO DO PIAUÍ',2210003,18);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4717,'SÃO JOÃO DO POLÊSINE',4318432,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4718,'SÃO JOÃO DO RIO DO PEIXE',2500700,15);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4719,'SÃO JOÃO DO SABUGI',2412104,20);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4720,'SÃO JOÃO DO SOTER',2111078,10);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4721,'SÃO JOÃO DO SUL',4216404,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4722,'SÃO JOÃO DO TIGRE',2514107,15);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4723,'SÃO JOÃO DO TRIUNFO',4125100,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4724,'SÃO JOÃO DOS PATOS',2111102,10);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4725,'SÃO JOÃO EVANGELISTA',3162807,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4726,'SÃO JOÃO NEPOMUCENO',3162906,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4727,'SÃO JOAQUIM',4216503,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4728,'SÃO JOAQUIM DA BARRA',3549409,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4729,'SÃO JOAQUIM DE BICAS',3162922,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4730,'SÃO JOAQUIM DO MONTE',2613305,17);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4731,'SÃO JORGE',4318440,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4732,'SÃO JORGE D''OESTE',4125209,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4733,'SÃO JORGE DO IVAÍ',4125308,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4734,'SÃO JORGE DO PATROCÍNIO',4125357,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4735,'SÃO JOSÉ',4216602,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4736,'SÃO JOSÉ DA BARRA',3162948,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4737,'SÃO JOSÉ DA BELA VISTA',3549508,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4738,'SÃO JOSÉ DA BOA VISTA',4125407,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4739,'SÃO JOSÉ DA COROA GRANDE',2613404,17);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4740,'SÃO JOSÉ DA LAGOA TAPADA',2514206,15);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4741,'SÃO JOSÉ DA LAJE',2708303,2);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4742,'SÃO JOSÉ DA LAPA',3162955,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4743,'SÃO JOSÉ DA SAFIRA',3163003,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4744,'SÃO JOSÉ DA TAPERA',2708402,2);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4745,'SÃO JOSÉ DA VARGINHA',3163102,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4746,'SÃO JOSÉ DA VITÓRIA',2929354,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4747,'SÃO JOSÉ DAS MISSÕES',4318457,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4748,'SÃO JOSÉ DAS PALMEIRAS',4125456,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4749,'SÃO JOSÉ DE CAIANA',2514305,15);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4750,'SÃO JOSÉ DE ESPINHARAS',2514404,15);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4751,'SÃO JOSÉ DE MIPIBU',2412203,20);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4752,'SÃO JOSÉ DE PIRANHAS',2514503,15);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4753,'SÃO JOSÉ DE PRINCESA',2514552,15);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4754,'SÃO JOSÉ DE RIBAMAR',2111201,10);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4755,'SÃO JOSÉ DE UBÁ',3305133,19);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4756,'SÃO JOSÉ DO ALEGRE',3163201,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4757,'SÃO JOSÉ DO BARREIRO',3549607,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4758,'SÃO JOSÉ DO BELMONTE',2613503,17);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4759,'SÃO JOSÉ DO BONFIM',2514602,15);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4760,'SÃO JOSÉ DO BREJO DO CRUZ',2514651,15);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4761,'SÃO JOSÉ DO CALÇADO',3204807,8);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4762,'SÃO JOSÉ DO CAMPESTRE',2412302,20);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4763,'SÃO JOSÉ DO CEDRO',4216701,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4764,'SÃO JOSÉ DO CERRITO',4216800,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4765,'SÃO JOSÉ DO DIVINO',2210052,18);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4766,'SÃO JOSÉ DO DIVINO',3163300,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4767,'SÃO JOSÉ DO EGITO',2613602,17);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4768,'SÃO JOSÉ DO GOIABAL',3163409,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4769,'SÃO JOSÉ DO HERVAL',4318465,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4770,'SÃO JOSÉ DO HORTÊNCIO',4318481,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4771,'SÃO JOSÉ DO INHACORÁ',4318499,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4772,'SÃO JOSÉ DO JACUÍPE',2929370,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4773,'SÃO JOSÉ DO JACURI',3163508,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4774,'SÃO JOSÉ DO MANTIMENTO',3163607,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4775,'SÃO JOSÉ DO NORTE',4318507,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4776,'SÃO JOSÉ DO OURO',4318606,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4777,'SÃO JOSÉ DO PEIXE',2210102,18);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4778,'SÃO JOSÉ DO PIAUÍ',2210201,18);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4779,'SÃO JOSÉ DO POVO',5107297,11);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4780,'SÃO JOSÉ DO RIO CLARO',5107305,11);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4781,'SÃO JOSÉ DO RIO PARDO',3549706,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4782,'SÃO JOSÉ DO RIO PRETO',3549805,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4783,'SÃO JOSÉ DO SABUGI',2514701,15);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4784,'SÃO JOSÉ DO SERIDÓ',2412401,20);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4785,'SÃO JOSÉ DO SUL',4318614,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4786,'SÃO JOSÉ DO VALE DO RIO PRETO',3305158,19);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4787,'SÃO JOSÉ DO XINGU',5107354,11);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4788,'SÃO JOSÉ DOS AUSENTES',4318622,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4789,'SÃO JOSÉ DOS BASÍLIOS',2111250,10);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4790,'SÃO JOSÉ DOS CAMPOS',3549904,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4791,'SÃO JOSÉ DOS CORDEIROS',2514800,15);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4792,'SÃO JOSÉ DOS PINHAIS',4125506,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4793,'SÃO JOSÉ DOS QUATRO MARCOS',5107107,11);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4794,'SÃO JOSÉ DOS RAMOS',2514453,15);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4795,'SÃO JULIÃO',2210300,18);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4796,'SÃO LEOPOLDO',4318705,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4797,'SÃO LOURENÇO',3163706,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4798,'SÃO LOURENÇO DA MATA',2613701,17);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4799,'SÃO LOURENÇO DA SERRA',3549953,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4800,'SÃO LOURENÇO DO OESTE',4216909,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4801,'SÃO LOURENÇO DO PIAUÍ',2210359,18);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4802,'SÃO LOURENÇO DO SUL',4318804,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4803,'SÃO LUDGERO',4217006,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4804,'SÃO LUÍS',2111300,10);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4805,'SÃO LUÍS DE MONTES BELOS',5220108,9);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4806,'SÃO LUÍS DO CURU',2312601,6);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4807,'SÃO LUÍS DO PARAITINGA',3550001,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4808,'SÃO LUIS DO PIAUÍ',2210375,18);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4809,'SÃO LUÍS DO QUITUNDE',2708501,2);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4810,'SÃO LUÍS GONZAGA DO MARANHÃO',2111409,10);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4811,'SÃO LUIZ',1400605,23);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4812,'SÃO LUIZ DO NORTE',5220157,9);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4813,'SÃO LUIZ GONZAGA',4318903,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4814,'SÃO MAMEDE',2514909,15);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4815,'SÃO MANOEL DO PARANÁ',4125555,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4816,'SÃO MANUEL',3550100,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4817,'SÃO MARCOS',4319000,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4818,'SÃO MARTINHO',4217105,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4819,'SÃO MARTINHO',4319109,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4820,'SÃO MARTINHO DA SERRA',4319125,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4821,'SÃO MATEUS',3204906,8);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4822,'SÃO MATEUS DO MARANHÃO',2111508,10);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4823,'SÃO MATEUS DO SUL',4125605,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4824,'SÃO MIGUEL',2412500,20);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4825,'SÃO MIGUEL ARCANJO',3550209,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4826,'SÃO MIGUEL DA BAIXA GRANDE',2210383,18);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4827,'SÃO MIGUEL DA BOA VISTA',4217154,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4828,'SÃO MIGUEL DAS MATAS',2929404,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4829,'SÃO MIGUEL DAS MISSÕES',4319158,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4830,'SÃO MIGUEL DE TAIPU',2515005,15);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4831,'SÃO MIGUEL DE TOUROS',2412559,20);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4832,'SÃO MIGUEL DO ALEIXO',2807006,26);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4833,'SÃO MIGUEL DO ANTA',3163805,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4834,'SÃO MIGUEL DO ARAGUAIA',5220207,9);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4835,'SÃO MIGUEL DO FIDALGO',2210391,18);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4836,'SÃO MIGUEL DO GUAMÁ',1507607,14);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4837,'SÃO MIGUEL DO GUAPORÉ',1100320,22);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4838,'SÃO MIGUEL DO IGUAÇU',4125704,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4839,'SÃO MIGUEL DO OESTE',4217204,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4840,'SÃO MIGUEL DO PASSA QUATRO',5220264,9);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4841,'SÃO MIGUEL DO TAPUIO',2210409,18);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4842,'SÃO MIGUEL DO TOCANTINS',1720200,27);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4843,'SÃO MIGUEL DOS CAMPOS',2708600,2);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4844,'SÃO MIGUEL DOS MILAGRES',2708709,2);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4845,'SÃO NICOLAU',4319208,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4846,'SÃO PATRÍCIO',5220280,9);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4847,'SÃO PAULO',3550308,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4848,'SÃO PAULO DAS MISSÕES',4319307,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4849,'SÃO PAULO DE OLIVENÇA',1303908,4);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4850,'SÃO PAULO DO POTENGI',2412609,20);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4851,'SÃO PEDRO',2412708,20);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4852,'SÃO PEDRO',3550407,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4853,'SÃO PEDRO DA ÁGUA BRANCA',2111532,10);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4854,'SÃO PEDRO DA ALDEIA',3305208,19);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4855,'SÃO PEDRO DA CIPA',5107404,11);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4856,'SÃO PEDRO DA SERRA',4319356,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4857,'SÃO PEDRO DA UNIÃO',3163904,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4858,'SÃO PEDRO DAS MISSÕES',4319364,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4859,'SÃO PEDRO DE ALCÂNTARA',4217253,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4860,'SÃO PEDRO DO BUTIÁ',4319372,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4861,'SÃO PEDRO DO IGUAÇU',4125753,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4862,'SÃO PEDRO DO IVAÍ',4125803,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4863,'SÃO PEDRO DO PARANÁ',4125902,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4864,'SÃO PEDRO DO PIAUÍ',2210508,18);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4865,'SÃO PEDRO DO SUAÇUÍ',3164100,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4866,'SÃO PEDRO DO SUL',4319406,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4867,'SÃO PEDRO DO TURVO',3550506,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4868,'SÃO PEDRO DOS CRENTES',2111573,10);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4869,'SÃO PEDRO DOS FERROS',3164001,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4870,'SÃO RAFAEL',2412807,20);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4871,'SÃO RAIMUNDO DAS MANGABEIRAS',2111607,10);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4872,'SÃO RAIMUNDO DO DOCA BEZERRA',2111631,10);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4873,'SÃO RAIMUNDO NONATO',2210607,18);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4874,'SÃO ROBERTO',2111672,10);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4875,'SÃO ROMÃO',3164209,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4876,'SÃO ROQUE',3550605,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4877,'SÃO ROQUE DE MINAS',3164308,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4878,'SÃO ROQUE DO CANAÃ',3204955,8);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4879,'SÃO SALVADOR DO TOCANTINS',1720259,27);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4880,'SÃO SEBASTIÃO',2708808,2);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4881,'SÃO SEBASTIÃO',3550704,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4882,'SÃO SEBASTIÃO DA AMOREIRA',4126009,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4883,'SÃO SEBASTIÃO DA BELA VISTA',3164407,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4884,'SÃO SEBASTIÃO DA BOA VISTA',1507706,14);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4885,'SÃO SEBASTIÃO DA GRAMA',3550803,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4886,'SÃO SEBASTIÃO DA VARGEM ALEGRE',3164431,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4887,'SÃO SEBASTIÃO DE LAGOA DE ROÇA',2515104,15);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4888,'SÃO SEBASTIÃO DO ALTO',3305307,19);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4889,'SÃO SEBASTIÃO DO ANTA',3164472,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4890,'SÃO SEBASTIÃO DO CAÍ',4319505,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4891,'SÃO SEBASTIÃO DO MARANHÃO',3164506,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4892,'SÃO SEBASTIÃO DO OESTE',3164605,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4893,'SÃO SEBASTIÃO DO PARAÍSO',3164704,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4894,'SÃO SEBASTIÃO DO PASSÉ',2929503,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4895,'SÃO SEBASTIÃO DO RIO PRETO',3164803,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4896,'SÃO SEBASTIÃO DO RIO VERDE',3164902,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4897,'SÃO SEBASTIÃO DO TOCANTINS',1720309,27);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4898,'SÃO SEBASTIÃO DO UATUMÃ',1303957,4);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4899,'SÃO SEBASTIÃO DO UMBUZEIRO',2515203,15);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4900,'SÃO SEPÉ',4319604,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4901,'SÃO SIMÃO',5220405,9);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4902,'SÃO SIMÃO',3550902,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4903,'SÃO THOMÉ DAS LETRAS',3165206,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4904,'SÃO TIAGO',3165008,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4905,'SÃO TOMÁS DE AQUINO',3165107,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4906,'SÃO TOMÉ',2412906,20);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4907,'SÃO TOMÉ',4126108,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4908,'SÃO VALENTIM',4319703,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4909,'SÃO VALENTIM DO SUL',4319711,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4910,'SÃO VALÉRIO DA NATIVIDADE',1720499,27);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4911,'SÃO VALÉRIO DO SUL',4319737,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4912,'SÃO VENDELINO',4319752,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4913,'SÃO VICENTE',2413003,20);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4914,'SÃO VICENTE',3551009,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4915,'SÃO VICENTE DE MINAS',3165305,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4916,'SÃO VICENTE DO SUL',4319802,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4917,'SÃO VICENTE FERRER',2613800,17);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4918,'SÃO VICENTE FERRER',2111706,10);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4919,'SAPÉ',2515302,15);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4920,'SAPEAÇU',2929602,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4921,'SAPEZAL',5107875,11);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4922,'SAPIRANGA',4319901,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4923,'SAPOPEMA',4126207,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4924,'SAPUCAÍ-MIRIM',3165404,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4925,'SAPUCAIA',1507755,14);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4926,'SAPUCAIA',3305406,19);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4927,'SAPUCAIA DO SUL',4320008,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4928,'SAQUAREMA',3305505,19);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4929,'SARANDI',4320107,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4930,'SARANDI',4126256,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4931,'SARAPUÍ',3551108,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4932,'SARDOÁ',3165503,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4933,'SARUTAIÁ',3551207,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4934,'SARZEDO',3165537,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4935,'SÁTIRO DIAS',2929701,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4936,'SATUBA',2708907,2);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4937,'SATUBINHA',2111722,10);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4938,'SAUBARA',2929750,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4939,'SAUDADE DO IGUAÇU',4126272,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4940,'SAUDADES',4217303,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4941,'SAÚDE',2929800,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4942,'SCHROEDER',4217402,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4943,'SEABRA',2929909,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4944,'SEARA',4217501,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4945,'SEBASTIANÓPOLIS DO SUL',3551306,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4946,'SEBASTIÃO BARROS',2210623,18);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4947,'SEBASTIÃO LARANJEIRAS',2930006,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4948,'SEBASTIÃO LEAL',2210631,18);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4949,'SEBERI',4320206,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4950,'SEDE NOVA',4320230,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4951,'SEGREDO',4320263,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4952,'SELBACH',4320305,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4953,'SELVÍRIA',5007802,12);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4954,'SEM-PEIXE',3165560,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4955,'SENA MADUREIRA',1200500,1);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4956,'SENADOR ALEXANDRE COSTA',2111748,10);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4957,'SENADOR AMARAL',3165578,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4958,'SENADOR CANEDO',5220454,9);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4959,'SENADOR CORTES',3165602,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4960,'SENADOR ELÓI DE SOUZA',2413102,20);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4961,'SENADOR FIRMINO',3165701,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4962,'SENADOR GEORGINO AVELINO',2413201,20);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4963,'SENADOR GUIOMARD',1200450,1);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4964,'SENADOR JOSÉ BENTO',3165800,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4965,'SENADOR JOSÉ PORFÍRIO',1507805,14);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4966,'SENADOR LA ROCQUE',2111763,10);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4967,'SENADOR MODESTINO GONÇALVES',3165909,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4968,'SENADOR POMPEU',2312700,6);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4969,'SENADOR RUI PALMEIRA',2708956,2);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4970,'SENADOR SÁ',2312809,6);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4971,'SENADOR SALGADO FILHO',4320321,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4972,'SENGÉS',4126306,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4973,'SENHOR DO BONFIM',2930105,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4974,'SENHORA DE OLIVEIRA',3166006,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4975,'SENHORA DO PORTO',3166105,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4976,'SENHORA DOS REMÉDIOS',3166204,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4977,'SENTINELA DO SUL',4320354,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4978,'SENTO SÉ',2930204,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4979,'SERAFINA CORRÊA',4320404,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4980,'SERICITA',3166303,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4981,'SERIDÓ',2515401,15);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4982,'SERINGUEIRAS',1101500,22);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4983,'SÉRIO',4320453,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4984,'SERITINGA',3166402,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4985,'SEROPÉDICA',3305554,19);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4986,'SERRA',3205002,8);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4987,'SERRA ALTA',4217550,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4988,'SERRA AZUL',3551405,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4989,'SERRA AZUL DE MINAS',3166501,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4990,'SERRA BRANCA',2515500,15);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4991,'SERRA DA RAIZ',2515609,15);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4992,'SERRA DA SAUDADE',3166600,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4993,'SERRA DE SÃO BENTO',2413300,20);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4994,'SERRA DO MEL',2413359,20);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4995,'SERRA DO NAVIO',1600055,3);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4996,'SERRA DO RAMALHO',2930154,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4997,'SERRA DO SALITRE',3166808,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4998,'SERRA DOS AIMORÉS',3166709,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (4999,'SERRA DOURADA',2930303,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5000,'SERRA GRANDE',2515708,15);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5001,'SERRA NEGRA',3551603,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5002,'SERRA NEGRA DO NORTE',2413409,20);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5003,'SERRA NOVA DOURADA',5107883,11);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5004,'SERRA PRETA',2930402,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5005,'SERRA REDONDA',2515807,15);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5006,'SERRA TALHADA',2613909,17);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5007,'SERRANA',3551504,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5008,'SERRANIA',3166907,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5009,'SERRANO DO MARANHÃO',2111789,10);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5010,'SERRANÓPOLIS',5220504,9);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5011,'SERRANÓPOLIS DE MINAS',3166956,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5012,'SERRANÓPOLIS DO IGUAÇU',4126355,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5013,'SERRANOS',3167004,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5014,'SERRARIA',2515906,15);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5015,'SERRINHA',2930501,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5016,'SERRINHA',2413508,20);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5017,'SERRINHA DOS PINTOS',2413557,20);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5018,'SERRITA',2614006,17);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5019,'SERRO',3167103,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5020,'SERROLÂNDIA',2930600,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5021,'SERTANEJA',4126405,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5022,'SERTÂNIA',2614105,17);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5023,'SERTANÓPOLIS',4126504,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5024,'SERTÃO',4320503,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5025,'SERTÃO SANTANA',4320552,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5026,'SERTÃOZINHO',2515930,15);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5027,'SERTÃOZINHO',3551702,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5028,'SETE BARRAS',3551801,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5029,'SETE DE SETEMBRO',4320578,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5030,'SETE LAGOAS',3167202,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5031,'SETE QUEDAS',5007703,12);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5032,'SETUBINHA',3165552,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5033,'SEVERIANO DE ALMEIDA',4320602,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5034,'SEVERIANO MELO',2413607,20);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5035,'SEVERÍNIA',3551900,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5036,'SIDERÓPOLIS',4217600,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5037,'SIDROLÂNDIA',5007901,12);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5038,'SIGEFREDO PACHECO',2210656,18);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5039,'SILVA JARDIM',3305604,19);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5040,'SILVÂNIA',5220603,9);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5041,'SILVANÓPOLIS',1720655,27);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5042,'SILVEIRA MARTINS',4320651,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5043,'SILVEIRÂNIA',3167301,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5044,'SILVEIRAS',3552007,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5045,'SILVES',1304005,4);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5046,'SILVIANÓPOLIS',3167400,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5047,'SIMÃO DIAS',2807105,26);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5048,'SIMÃO PEREIRA',3167509,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5049,'SIMÕES',2210706,18);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5050,'SIMÕES FILHO',2930709,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5051,'SIMOLÂNDIA',5220686,9);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5052,'SIMONÉSIA',3167608,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5053,'SIMPLÍCIO MENDES',2210805,18);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5054,'SINIMBU',4320677,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5055,'SINOP',5107909,11);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5056,'SIQUEIRA CAMPOS',4126603,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5057,'SIRINHAÉM',2614204,17);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5058,'SIRIRI',2807204,26);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5059,'SÍTIO D''ABADIA',5220702,9);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5060,'SÍTIO DO MATO',2930758,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5061,'SÍTIO DO QUINTO',2930766,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5062,'SÍTIO NOVO',2413706,20);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5063,'SÍTIO NOVO',2111805,10);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5064,'SÍTIO NOVO DO TOCANTINS',1720804,27);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5065,'SOBRADINHO',2930774,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5066,'SOBRADINHO',4320701,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5067,'SOBRADO',2515971,15);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5068,'SOBRAL',2312908,6);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5069,'SOBRÁLIA',3167707,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5070,'SOCORRO',3552106,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5071,'SOCORRO DO PIAUÍ',2210904,18);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5072,'SOLÂNEA',2516003,15);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5073,'SOLEDADE',4320800,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5074,'SOLEDADE',2516102,15);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5075,'SOLEDADE DE MINAS',3167806,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5076,'SOLIDÃO',2614402,17);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5077,'SOLONÓPOLE',2313005,6);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5078,'SOMBRIO',4217709,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5079,'SONORA',5007935,12);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5080,'SOORETAMA',3205010,8);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5081,'SOROCABA',3552205,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5082,'SORRISO',5107925,11);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5083,'SOSSEGO',2516151,15);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5084,'SOURE',1507904,14);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5085,'SOUSA',2516201,15);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5086,'SOUTO SOARES',2930808,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5087,'SUCUPIRA',1720853,27);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5088,'SUCUPIRA DO NORTE',2111904,10);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5089,'SUCUPIRA DO RIACHÃO',2111953,10);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5090,'SUD MENNUCCI',3552304,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5091,'SUL BRASIL',4217758,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5092,'SULINA',4126652,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5093,'SUMARÉ',3552403,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5094,'SUMÉ',2516300,15);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5095,'SUMIDOURO',3305703,19);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5096,'SURUBIM',2614501,17);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5097,'SUSSUAPARA',2210938,18);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5098,'SUZANÁPOLIS',3552551,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5099,'SUZANO',3552502,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5100,'TABAÍ',4320859,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5101,'TABAPORÃ',5107941,11);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5102,'TABAPUÃ',3552601,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5103,'TABATINGA',1304062,4);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5104,'TABATINGA',3552700,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5105,'TABIRA',2614600,17);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5106,'TABOÃO DA SERRA',3552809,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5107,'TABOCAS DO BREJO VELHO',2930907,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5108,'TABOLEIRO GRANDE',2413805,20);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5109,'TABULEIRO',3167905,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5110,'TABULEIRO DO NORTE',2313104,6);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5111,'TACAIMBÓ',2614709,17);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5112,'TACARATU',2614808,17);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5113,'TACIBA',3552908,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5114,'TACURU',5007950,12);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5115,'TAGUAÍ',3553005,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5116,'TAGUATINGA',1720903,27);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5117,'TAIAÇU',3553104,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5118,'TAILÂNDIA',1507953,14);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5119,'TAIÓ',4217808,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5120,'TAIOBEIRAS',3168002,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5121,'TAIPAS DO TOCANTINS',1720937,27);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5122,'TAIPU',2413904,20);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5123,'TAIÚVA',3553203,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5124,'TALISMÃ',1720978,27);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5125,'TAMANDARÉ',2614857,17);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5126,'TAMARANA',4126678,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5127,'TAMBAÚ',3553302,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5128,'TAMBOARA',4126702,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5129,'TAMBORIL',2313203,6);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5130,'TAMBORIL DO PIAUÍ',2210953,18);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5131,'TANABI',3553401,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5132,'TANGARÁ',4217907,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5133,'TANGARÁ',2414001,20);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5134,'TANGARÁ DA SERRA',5107958,11);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5135,'TANGUÁ',3305752,19);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5136,'TANHAÇU',2931004,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5137,'TANQUE D''ARCA',2709004,2);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5138,'TANQUE DO PIAUÍ',2210979,18);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5139,'TANQUE NOVO',2931053,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5140,'TANQUINHO',2931103,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5141,'TAPARUBA',3168051,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5142,'TAPAUÁ',1304104,4);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5143,'TAPEJARA',4320909,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5144,'TAPEJARA',4126801,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5145,'TAPERA',4321006,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5146,'TAPEROÁ',2931202,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5147,'TAPEROÁ',2516508,15);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5148,'TAPES',4321105,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5149,'TAPIRA',4126900,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5150,'TAPIRA',3168101,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5151,'TAPIRAÍ',3553500,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5152,'TAPIRAÍ',3168200,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5153,'TAPIRAMUTÁ',2931301,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5154,'TAPIRATIBA',3553609,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5155,'TAPURAH',5108006,11);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5156,'TAQUARA',4321204,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5157,'TAQUARAÇU DE MINAS',3168309,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5158,'TAQUARAL',3553658,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5159,'TAQUARAL DE GOIÁS',5221007,9);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5160,'TAQUARANA',2709103,2);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5161,'TAQUARI',4321303,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5162,'TAQUARITINGA',3553708,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5163,'TAQUARITINGA DO NORTE',2615003,17);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5164,'TAQUARITUBA',3553807,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5165,'TAQUARIVAÍ',3553856,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5166,'TAQUARUÇU DO SUL',4321329,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5167,'TAQUARUSSU',5007976,12);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5168,'TARABAÍ',3553906,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5169,'TARAUACÁ',1200609,1);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5170,'TARRAFAS',2313252,6);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5171,'TARTARUGALZINHO',1600709,3);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5172,'TARUMÃ',3553955,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5173,'TARUMIRIM',3168408,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5174,'TASSO FRAGOSO',2112001,10);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5175,'TATUÍ',3554003,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5176,'TAUÁ',2313302,6);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5177,'TAUBATÉ',3554102,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5178,'TAVARES',4321352,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5179,'TAVARES',2516607,15);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5180,'TEFÉ',1304203,4);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5181,'TEIXEIRA',2516706,15);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5182,'TEIXEIRA DE FREITAS',2931350,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5183,'TEIXEIRA SOARES',4127007,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5184,'TEIXEIRAS',3168507,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5185,'TEIXEIRÓPOLIS',1101559,22);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5186,'TEJUÇUOCA',2313351,6);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5187,'TEJUPÁ',3554201,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5188,'TELÊMACO BORBA',4127106,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5189,'TELHA',2807303,26);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5190,'TENENTE ANANIAS',2414100,20);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5191,'TENENTE LAURENTINO CRUZ',2414159,20);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5192,'TENENTE PORTELA',4321402,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5193,'TENÓRIO',2516755,15);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5194,'TEODORO SAMPAIO',2931400,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5195,'TEODORO SAMPAIO',3554300,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5196,'TEOFILÂNDIA',2931509,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5197,'TEÓFILO OTONI',3168606,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5198,'TEOLÂNDIA',2931608,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5199,'TEOTÔNIO VILELA',2709152,2);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5200,'TERENOS',5008008,12);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5201,'TERESINA',2211001,18);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5202,'TERESINA DE GOIÁS',5221080,9);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5203,'TERESÓPOLIS',3305802,19);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5204,'TEREZINHA',2615102,17);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5205,'TEREZÓPOLIS DE GOIÁS',5221197,9);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5206,'TERRA ALTA',1507961,14);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5207,'TERRA BOA',4127205,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5208,'TERRA DE AREIA',4321436,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5209,'TERRA NOVA',2931707,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5210,'TERRA NOVA',2615201,17);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5211,'TERRA NOVA DO NORTE',5108055,11);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5212,'TERRA RICA',4127304,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5213,'TERRA ROXA',4127403,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5214,'TERRA ROXA',3554409,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5215,'TERRA SANTA',1507979,14);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5216,'TESOURO',5108105,11);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5217,'TEUTÔNIA',4321451,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5218,'THEOBROMA',1101609,22);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5219,'TIANGUÁ',2313401,6);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5220,'TIBAGI',4127502,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5221,'TIBAU',2411056,20);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5222,'TIBAU DO SUL',2414209,20);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5223,'TIETÊ',3554508,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5224,'TIGRINHOS',4217956,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5225,'TIJUCAS',4218004,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5226,'TIJUCAS DO SUL',4127601,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5227,'TIMBAÚBA',2615300,17);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5228,'TIMBAÚBA DOS BATISTAS',2414308,20);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5229,'TIMBÉ DO SUL',4218103,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5230,'TIMBIRAS',2112100,10);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5231,'TIMBÓ',4218202,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5232,'TIMBÓ GRANDE',4218251,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5233,'TIMBURI',3554607,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5234,'TIMON',2112209,10);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5235,'TIMÓTEO',3168705,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5236,'TIO HUGO',4321469,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5237,'TIRADENTES',3168804,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5238,'TIRADENTES DO SUL',4321477,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5239,'TIROS',3168903,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5240,'TOBIAS BARRETO',2807402,26);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5241,'TOCANTÍNIA',1721109,27);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5242,'TOCANTINÓPOLIS',1721208,27);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5243,'TOCANTINS',3169000,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5244,'TOCOS DO MOJI',3169059,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5245,'TOLEDO',4127700,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5246,'TOLEDO',3169109,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5247,'TOMAR DO GERU',2807501,26);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5248,'TOMAZINA',4127809,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5249,'TOMBOS',3169208,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5250,'TOMÉ-AÇU',1508001,14);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5251,'TONANTINS',1304237,4);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5252,'TORITAMA',2615409,17);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5253,'TORIXORÉU',5108204,11);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5254,'TOROPI',4321493,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5255,'TORRE DE PEDRA',3554656,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5256,'TORRES',4321501,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5257,'TORRINHA',3554706,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5258,'TOUROS',2414407,20);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5259,'TRABIJU',3554755,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5260,'TRACUATEUA',1508035,14);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5261,'TRACUNHAÉM',2615508,17);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5262,'TRAIPU',2709202,2);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5263,'TRAIRÃO',1508050,14);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5264,'TRAIRI',2313500,6);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5265,'TRAJANO DE MORAIS',3305901,19);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5266,'TRAMANDAÍ',4321600,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5267,'TRAVESSEIRO',4321626,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5268,'TREMEDAL',2931806,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5269,'TREMEMBÉ',3554805,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5270,'TRÊS ARROIOS',4321634,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5271,'TRÊS BARRAS',4218301,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5272,'TRÊS BARRAS DO PARANÁ',4127858,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5273,'TRÊS CACHOEIRAS',4321667,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5274,'TRÊS CORAÇÕES',3169307,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5275,'TRÊS COROAS',4321709,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5276,'TRÊS DE MAIO',4321808,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5277,'TRÊS FORQUILHAS',4321832,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5278,'TRÊS FRONTEIRAS',3554904,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5279,'TRÊS LAGOAS',5008305,12);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5280,'TRÊS MARIAS',3169356,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5281,'TRÊS PALMEIRAS',4321857,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5282,'TRÊS PASSOS',4321907,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5283,'TRÊS PONTAS',3169406,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5284,'TRÊS RANCHOS',5221304,9);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5285,'TRÊS RIOS',3306008,19);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5286,'TREVISO',4218350,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5287,'TREZE DE MAIO',4218400,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5288,'TREZE TÍLIAS',4218509,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5289,'TRINDADE',2615607,17);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5290,'TRINDADE',5221403,9);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5291,'TRINDADE DO SUL',4321956,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5292,'TRIUNFO',2615706,17);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5293,'TRIUNFO',2516805,15);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5294,'TRIUNFO',4322004,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5295,'TRIUNFO POTIGUAR',2414456,20);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5296,'TRIZIDELA DO VALE',2112233,10);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5297,'TROMBAS',5221452,9);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5298,'TROMBUDO CENTRAL',4218608,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5299,'TUBARÃO',4218707,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5300,'TUCANO',2931905,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5301,'TUCUMÃ',1508084,14);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5302,'TUCUNDUVA',4322103,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5303,'TUCURUÍ',1508100,14);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5304,'TUFILÂNDIA',2112274,10);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5305,'TUIUTI',3554953,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5306,'TUMIRITINGA',3169505,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5307,'TUNÁPOLIS',4218756,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5308,'TUNAS',4322152,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5309,'TUNAS DO PARANÁ',4127882,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5310,'TUNEIRAS DO OESTE',4127908,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5311,'TUNTUM',2112308,10);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5312,'TUPÃ',3555000,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5313,'TUPACIGUARA',3169604,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5314,'TUPANATINGA',2615805,17);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5315,'TUPANCI DO SUL',4322186,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5316,'TUPANCIRETÃ',4322202,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5317,'TUPANDI',4322251,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5318,'TUPARENDI',4322301,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5319,'TUPARETAMA',2615904,17);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5320,'TUPÃSSI',4127957,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5321,'TUPI PAULISTA',3555109,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5322,'TUPIRAMA',1721257,27);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5323,'TUPIRATINS',1721307,27);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5324,'TURIAÇU',2112407,10);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5325,'TURILÂNDIA',2112456,10);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5326,'TURIÚBA',3555208,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5327,'TURMALINA',3169703,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5328,'TURMALINA',3555307,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5329,'TURUÇU',4322327,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5330,'TURURU',2313559,6);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5331,'TURVÂNIA',5221502,9);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5332,'TURVELÂNDIA',5221551,9);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5333,'TURVO',4218806,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5334,'TURVO',4127965,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5335,'TURVOLÂNDIA',3169802,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5336,'TUTÓIA',2112506,10);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5337,'UARINI',1304260,4);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5338,'UAUÁ',2932002,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5339,'UBÁ',3169901,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5340,'UBAÍ',3170008,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5341,'UBAÍRA',2932101,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5342,'UBAITABA',2932200,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5343,'UBAJARA',2313609,6);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5344,'UBAPORANGA',3170057,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5345,'UBARANA',3555356,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5346,'UBATÃ',2932309,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5347,'UBATUBA',3555406,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5348,'UBERABA',3170107,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5349,'UBERLÂNDIA',3170206,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5350,'UBIRAJARA',3555505,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5351,'UBIRATÃ',4128005,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5352,'UBIRETAMA',4322343,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5353,'UCHOA',3555604,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5354,'UIBAÍ',2932408,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5355,'UIRAMUTÃ',1400704,23);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5356,'UIRAPURU',5221577,9);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5357,'UIRAÚNA',2516904,15);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5358,'ULIANÓPOLIS',1508126,14);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5359,'UMARI',2313708,6);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5360,'UMARIZAL',2414506,20);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5361,'UMBAÚBA',2807600,26);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5362,'UMBURANAS',2932457,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5363,'UMBURATIBA',3170305,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5364,'UMBUZEIRO',2517001,15);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5365,'UMIRIM',2313757,6);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5366,'UMUARAMA',4128104,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5367,'UNA',2932507,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5368,'UNAÍ',3170404,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5369,'UNIÃO',2211100,18);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5370,'UNIÃO DA SERRA',4322350,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5371,'UNIÃO DA VITÓRIA',4128203,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5372,'UNIÃO DE MINAS',3170438,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5373,'UNIÃO DO OESTE',4218855,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5374,'UNIÃO DO SUL',5108303,11);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5375,'UNIÃO DOS PALMARES',2709301,2);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5376,'UNIÃO PAULISTA',3555703,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5377,'UNIFLOR',4128302,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5378,'UNISTALDA',4322376,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5379,'UPANEMA',2414605,20);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5380,'URAÍ',4128401,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5381,'URANDI',2932606,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5382,'URÂNIA',3555802,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5383,'URBANO SANTOS',2112605,10);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5384,'URU',3555901,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5385,'URUAÇU',5221601,9);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5386,'URUANA',5221700,9);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5387,'URUANA DE MINAS',3170479,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5388,'URUARÁ',1508159,14);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5389,'URUBICI',4218905,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5390,'URUBURETAMA',2313807,6);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5391,'URUCÂNIA',3170503,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5392,'URUCARÁ',1304302,4);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5393,'URUÇUCA',2932705,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5394,'URUÇUÍ',2211209,18);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5395,'URUCUIA',3170529,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5396,'URUCURITUBA',1304401,4);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5397,'URUGUAIANA',4322400,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5398,'URUOCA',2313906,6);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5399,'URUPÁ',1101708,22);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5400,'URUPEMA',4218954,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5401,'URUPÊS',3556008,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5402,'URUSSANGA',4219002,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5403,'URUTAÍ',5221809,9);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5404,'UTINGA',2932804,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5405,'VACARIA',4322509,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5406,'VALE DE SÃO DOMINGOS',5108352,11);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5407,'VALE DO ANARI',1101757,22);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5408,'VALE DO PARAÍSO',1101807,22);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5409,'VALE DO SOL',4322533,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5410,'VALE REAL',4322541,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5411,'VALE VERDE',4322525,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5412,'VALENÇA',2932903,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5413,'VALENÇA',3306107,19);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5414,'VALENÇA DO PIAUÍ',2211308,18);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5415,'VALENTE',2933000,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5416,'VALENTIM GENTIL',3556107,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5417,'VALINHOS',3556206,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5418,'VALPARAÍSO',3556305,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5419,'VALPARAÍSO DE GOIÁS',5221858,9);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5420,'VANINI',4322558,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5421,'VARGEÃO',4219101,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5422,'VARGEM',4219150,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5423,'VARGEM',3556354,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5424,'VARGEM ALEGRE',3170578,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5425,'VARGEM ALTA',3205036,8);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5426,'VARGEM BONITA',4219176,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5427,'VARGEM BONITA',3170602,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5428,'VARGEM GRANDE',2112704,10);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5429,'VARGEM GRANDE DO RIO PARDO',3170651,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5430,'VARGEM GRANDE DO SUL',3556404,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5431,'VARGEM GRANDE PAULISTA',3556453,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5432,'VARGINHA',3170701,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5433,'VARJÃO',5221908,9);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5434,'VARJÃO DE MINAS',3170750,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5435,'VARJOTA',2313955,6);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5436,'VARRE-SAI',3306156,19);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5437,'VÁRZEA',2517100,15);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5438,'VÁRZEA',2414704,20);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5439,'VÁRZEA ALEGRE',2314003,6);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5440,'VÁRZEA BRANCA',2211357,18);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5441,'VÁRZEA DA PALMA',3170800,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5442,'VÁRZEA DA ROÇA',2933059,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5443,'VÁRZEA DO POÇO',2933109,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5444,'VÁRZEA GRANDE',2211407,18);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5445,'VÁRZEA GRANDE',5108402,11);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5446,'VÁRZEA NOVA',2933158,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5447,'VÁRZEA PAULISTA',3556503,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5448,'VARZEDO',2933174,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5449,'VARZELÂNDIA',3170909,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5450,'VASSOURAS',3306206,19);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5451,'VAZANTE',3171006,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5452,'VENÂNCIO AIRES',4322608,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5453,'VENDA NOVA DO IMIGRANTE',3205069,8);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5454,'VENHA-VER',2414753,20);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5455,'VENTANIA',4128534,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5456,'VENTUROSA',2616001,17);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5457,'VERA',5108501,11);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5458,'VERA CRUZ',2933208,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5459,'VERA CRUZ',2414803,20);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5460,'VERA CRUZ',4322707,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5461,'VERA CRUZ',3556602,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5462,'VERA CRUZ DO OESTE',4128559,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5463,'VERA MENDES',2211506,18);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5464,'VERANÓPOLIS',4322806,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5465,'VERDEJANTE',2616100,17);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5466,'VERDELÂNDIA',3171030,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5467,'VERÊ',4128609,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5468,'VEREDA',2933257,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5469,'VEREDINHA',3171071,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5470,'VERÍSSIMO',3171105,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5471,'VERMELHO NOVO',3171154,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5472,'VERTENTE DO LÉRIO',2616183,17);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5473,'VERTENTES',2616209,17);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5474,'VESPASIANO',3171204,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5475,'VESPASIANO CORREA',4322855,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5476,'VIADUTOS',4322905,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5477,'VIAMÃO',4323002,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5478,'VIANA',3205101,8);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5479,'VIANA',2112803,10);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5480,'VIANÓPOLIS',5222005,9);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5481,'VICÊNCIA',2616308,17);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5482,'VICENTE DUTRA',4323101,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5483,'VICENTINA',5008404,12);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5484,'VICENTINÓPOLIS',5222054,9);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5485,'VIÇOSA',2709400,2);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5486,'VIÇOSA',2414902,20);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5487,'VIÇOSA',3171303,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5488,'VIÇOSA DO CEARÁ',2314102,6);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5489,'VICTOR GRAEFF',4323200,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5490,'VIDAL RAMOS',4219200,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5491,'VIDEIRA',4219309,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5492,'VIEIRAS',3171402,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5493,'VIEIRÓPOLIS',2517209,15);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5494,'VIGIA',1508209,14);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5495,'VILA ALTA',4128625,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5496,'VILA BELA DA SANTÍSSIMA TRINDADE',5105507,11);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5497,'VILA BOA',5222203,9);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5498,'VILA FLOR',2415008,20);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5499,'VILA FLORES',4323309,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5500,'VILA LÂNGARO',4323358,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5501,'VILA MARIA',4323408,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5502,'VILA NOVA DO PIAUÍ',2211605,18);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5503,'VILA NOVA DO SUL',4323457,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5504,'VILA NOVA DOS MARTÍRIOS',2112852,10);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5505,'VILA PAVÃO',3205150,8);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5506,'VILA PROPÍCIO',5222302,9);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5507,'VILA RICA',5108600,11);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5508,'VILA VALÉRIO',3205176,8);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5509,'VILA VELHA',3205200,8);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5510,'VILHENA',1100304,22);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5511,'VINHEDO',3556701,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5512,'VIRADOURO',3556800,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5513,'VIRGEM DA LAPA',3171600,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5514,'VIRGÍNIA',3171709,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5515,'VIRGINÓPOLIS',3171808,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5516,'VIRGOLÂNDIA',3171907,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5517,'VIRMOND',4128658,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5518,'VISCONDE DO RIO BRANCO',3172004,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5519,'VISEU',1508308,14);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5520,'VISTA ALEGRE',4323507,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5521,'VISTA ALEGRE DO ALTO',3556909,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5522,'VISTA ALEGRE DO PRATA',4323606,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5523,'VISTA GAÚCHA',4323705,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5524,'VISTA SERRANA',2505501,15);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5525,'VITOR MEIRELES',4219358,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5526,'VITÓRIA',3205309,8);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5527,'VITÓRIA BRASIL',3556958,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5528,'VITÓRIA DA CONQUISTA',2933307,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5529,'VITÓRIA DAS MISSÕES',4323754,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5530,'VITÓRIA DE SANTO ANTÃO',2616407,17);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5531,'VITÓRIA DO JARI',1600808,3);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5532,'VITÓRIA DO MEARIM',2112902,10);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5533,'VITÓRIA DO XINGU',1508357,14);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5534,'VITORINO',4128708,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5535,'VITORINO FREIRE',2113009,10);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5536,'VOLTA GRANDE',3172103,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5537,'VOLTA REDONDA',3306305,19);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5538,'VOTORANTIM',3557006,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5539,'VOTUPORANGA',3557105,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5540,'WAGNER',2933406,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5541,'WALL FERRAZ',2211704,18);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5542,'WANDERLÂNDIA',1722081,27);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5543,'WANDERLEY',2933455,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5544,'WENCESLAU BRAZ',4128500,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5545,'WENCESLAU BRAZ',3172202,13);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5546,'WENCESLAU GUIMARÃES',2933505,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5547,'WESTFÁLIA',4323770,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5548,'WITMARSUM',4219408,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5549,'XAMBIOÁ',1722107,27);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5550,'XAMBRÊ',4128807,16);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5551,'XANGRI-LÁ',4323804,21);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5552,'XANXERÊ',4219507,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5553,'XAPURI',1200708,1);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5554,'XAVANTINA',4219606,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5555,'XAXIM',4219705,24);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5556,'XEXÉU',2616506,17);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5557,'XINGUARA',1508407,14);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5558,'XIQUE-XIQUE',2933604,5);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5559,'ZABELÊ',2517407,15);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5560,'ZACARIAS',3557154,25);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5561,'ZÉ DOCA',2114007,10);
INSERT INTO cidade(id, nome, codigoibge, uf_id) VALUES (5562,'ZORTÉA',4219853,24);

/* ExecuteSqlStatement INSERT INTO uniquekeys (tablename, nexthi) VALUES ('cidade', 2782); */
INSERT INTO uniquekeys (tablename, nexthi) VALUES ('cidade', 2782);

/* ExecuteEmbeddedSqlScript Fashion.ERP.Migrator.Scripts._201301010000.banco.sql */
INSERT INTO banco(id, codigo, nome)VALUES(1,654,'A. J. RENNER');
INSERT INTO banco(id, codigo, nome)VALUES(2,246,'ABC - BRASIL');
INSERT INTO banco(id, codigo, nome)VALUES(3,025,'ALFA');
INSERT INTO banco(id, codigo, nome)VALUES(4,003,'AMAZÔNIA - BASA');
INSERT INTO banco(id, codigo, nome)VALUES(5,213,'ARBI');
INSERT INTO banco(id, codigo, nome)VALUES(6,019,'AZTECA');
INSERT INTO banco(id, codigo, nome)VALUES(7,096,'BANCO BMFBOVESPA');
INSERT INTO banco(id, codigo, nome)VALUES(8,719,'BANIF');
INSERT INTO banco(id, codigo, nome)VALUES(9,755,'BANK OF AMERICA MERRILL LYNCH');
INSERT INTO banco(id, codigo, nome)VALUES(10,740,'BARCLAYS');
INSERT INTO banco(id, codigo, nome)VALUES(11,107,'BBM');
INSERT INTO banco(id, codigo, nome)VALUES(12,081,'BBN BANCO BRASILEIRO DE NEGÓCIOS');
INSERT INTO banco(id, codigo, nome)VALUES(13,250,'BCV');
INSERT INTO banco(id, codigo, nome)VALUES(14,122,'BERJ');
INSERT INTO banco(id, codigo, nome)VALUES(15,739,'BGN');
INSERT INTO banco(id, codigo, nome)VALUES(16,078,'BI BES - ESPIRITO SANTO');
INSERT INTO banco(id, codigo, nome)VALUES(17,318,'BMG');
INSERT INTO banco(id, codigo, nome)VALUES(18,004,'BNB');
INSERT INTO banco(id, codigo, nome)VALUES(19,752,'BNP PARIBAS BRASIL');
INSERT INTO banco(id, codigo, nome)VALUES(20,017,'BNY MELLON');
INSERT INTO banco(id, codigo, nome)VALUES(21,248,'BOAVISTA INTERATLÂNTICO');
INSERT INTO banco(id, codigo, nome)VALUES(22,218,'BONSUCESSO');
INSERT INTO banco(id, codigo, nome)VALUES(23,069,'BPN BRASIL');
INSERT INTO banco(id, codigo, nome)VALUES(24,065,'BRACCE');
INSERT INTO banco(id, codigo, nome)VALUES(25,063,'BRADESCARD');
INSERT INTO banco(id, codigo, nome)VALUES(26,237,'BRADESCO');
INSERT INTO banco(id, codigo, nome)VALUES(27,036,'BRADESCO BBI');
INSERT INTO banco(id, codigo, nome)VALUES(28,204,'BRADESCO CARTÕES');
INSERT INTO banco(id, codigo, nome)VALUES(29,394,'BRADESCO FINANCIAMENTOS');
INSERT INTO banco(id, codigo, nome)VALUES(30,225,'BRASCAN');
INSERT INTO banco(id, codigo, nome)VALUES(31,001,'BRASIL');
INSERT INTO banco(id, codigo, nome)VALUES(32,125,'BRASIL PLURAL');
INSERT INTO banco(id, codigo, nome)VALUES(33,070,'BRB - BCO. BRASÍLIA');
INSERT INTO banco(id, codigo, nome)VALUES(34,208,'BTG PACTUAL');
INSERT INTO banco(id, codigo, nome)VALUES(35,263,'CACIQUE');
INSERT INTO banco(id, codigo, nome)VALUES(36,104,'CAIXA ECON. FEDERAL');
INSERT INTO banco(id, codigo, nome)VALUES(37,473,'CAIXA GERAL - BRASIL');
INSERT INTO banco(id, codigo, nome)VALUES(38,412,'CAPITAL');
INSERT INTO banco(id, codigo, nome)VALUES(39,040,'CARGILL');
INSERT INTO banco(id, codigo, nome)VALUES(40,114,'CC CECOOPES');
INSERT INTO banco(id, codigo, nome)VALUES(41,085,'CC CECRED');
INSERT INTO banco(id, codigo, nome)VALUES(42,097,'CC CENTRALCREDI');
INSERT INTO banco(id, codigo, nome)VALUES(43,098,'CC CREDIALIANÇA');
INSERT INTO banco(id, codigo, nome)VALUES(44,010,'CC CREDICOAMO');
INSERT INTO banco(id, codigo, nome)VALUES(45,089,'CC REGIÃO DA MOGIANA');
INSERT INTO banco(id, codigo, nome)VALUES(46,112,'CC UNICRED BRASIL CENTRAL');
INSERT INTO banco(id, codigo, nome)VALUES(47,091,'CC UNICRED CENTRAL RS');
INSERT INTO banco(id, codigo, nome)VALUES(48,087,'CC UNICRED CENTRAL SANTA CATARINA');
INSERT INTO banco(id, codigo, nome)VALUES(49,090,'CC UNICRED CENTRAL SP');
INSERT INTO banco(id, codigo, nome)VALUES(50,099,'CC UNIPRIME CENTRAL');
INSERT INTO banco(id, codigo, nome)VALUES(51,084,'CC UNIPRIME NORTE DO PARANÁ');
INSERT INTO banco(id, codigo, nome)VALUES(52,266,'CÉDULA');
INSERT INTO banco(id, codigo, nome)VALUES(53,083,'CHINA BRASIL');
INSERT INTO banco(id, codigo, nome)VALUES(54,233,'CIFRA');
INSERT INTO banco(id, codigo, nome)VALUES(55,477,'CITIBANK N. A.');
INSERT INTO banco(id, codigo, nome)VALUES(56,745,'CITIBANK S. A.');
INSERT INTO banco(id, codigo, nome)VALUES(57,241,'CLÁSSICO');
INSERT INTO banco(id, codigo, nome)VALUES(58,756,'COOPERATIVO BRASIL');
INSERT INTO banco(id, codigo, nome)VALUES(59,075,'CR2');
INSERT INTO banco(id, codigo, nome)VALUES(60,222,'CREDIT AGRÍCOLE BRASIL');
INSERT INTO banco(id, codigo, nome)VALUES(61,505,'CREDIT SUISSE');
INSERT INTO banco(id, codigo, nome)VALUES(62,707,'DAYCOVAL');
INSERT INTO banco(id, codigo, nome)VALUES(63,487,'DEUTSCHE BANK');
INSERT INTO banco(id, codigo, nome)VALUES(64,214,'DIBENS');
INSERT INTO banco(id, codigo, nome)VALUES(65,021,'EST. ES - BANESTES');
INSERT INTO banco(id, codigo, nome)VALUES(66,037,'EST. PA - BANPARÁ');
INSERT INTO banco(id, codigo, nome)VALUES(67,041,'EST. RS - BANRISUL');
INSERT INTO banco(id, codigo, nome)VALUES(68,047,'EST. SE - BANESE');
INSERT INTO banco(id, codigo, nome)VALUES(69,265,'FATOR');
INSERT INTO banco(id, codigo, nome)VALUES(70,224,'FIBRA');
INSERT INTO banco(id, codigo, nome)VALUES(71,626,'FICSA');
INSERT INTO banco(id, codigo, nome)VALUES(72,121,'GERADOR');
INSERT INTO banco(id, codigo, nome)VALUES(73,612,'GUANABARA');
INSERT INTO banco(id, codigo, nome)VALUES(74,062,'HIPERCARD');
INSERT INTO banco(id, codigo, nome)VALUES(75,399,'HSBC BANK');
INSERT INTO banco(id, codigo, nome)VALUES(76,604,'INDL. DO BRASIL');
INSERT INTO banco(id, codigo, nome)VALUES(77,320,'INDL. E COML.');
INSERT INTO banco(id, codigo, nome)VALUES(78,653,'INDUSVAL');
INSERT INTO banco(id, codigo, nome)VALUES(79,492,'ING BANK');
INSERT INTO banco(id, codigo, nome)VALUES(80,630,'INTERCAP');
INSERT INTO banco(id, codigo, nome)VALUES(81,077,'INTERMEDIUM');
INSERT INTO banco(id, codigo, nome)VALUES(82,249,'INVESTCRED');
INSERT INTO banco(id, codigo, nome)VALUES(83,184,'ITAÚ BBA');
INSERT INTO banco(id, codigo, nome)VALUES(84,341,'ITAÚ UNIBANCO');
INSERT INTO banco(id, codigo, nome)VALUES(85,652,'ITAÚ UNIBANCO HOLDING');
INSERT INTO banco(id, codigo, nome)VALUES(86,376,'J. P. MORGAN');
INSERT INTO banco(id, codigo, nome)VALUES(87,074,'J. SAFRA');
INSERT INTO banco(id, codigo, nome)VALUES(88,217,'JOHN DEERE');
INSERT INTO banco(id, codigo, nome)VALUES(89,488,'JP MORGAN CHASE BANK');
INSERT INTO banco(id, codigo, nome)VALUES(90,076,'KDB DO BRASIL');
INSERT INTO banco(id, codigo, nome)VALUES(91,757,'KEB DO BRASIL');
INSERT INTO banco(id, codigo, nome)VALUES(92,600,'LUSO BRASILEIRO');
INSERT INTO banco(id, codigo, nome)VALUES(93,243,'MÁXIMA');
INSERT INTO banco(id, codigo, nome)VALUES(94,389,'MERCANTIL DO BRASIL');
INSERT INTO banco(id, codigo, nome)VALUES(95,746,'MODAL');
INSERT INTO banco(id, codigo, nome)VALUES(96,066,'MORGAN STANLEY');
INSERT INTO banco(id, codigo, nome)VALUES(97,300,'NACION ARGENTINA');
INSERT INTO banco(id, codigo, nome)VALUES(98,753,'NBC BANK');
INSERT INTO banco(id, codigo, nome)VALUES(99,045,'OPPORTUNITY');
INSERT INTO banco(id, codigo, nome)VALUES(100,212,'ORIGINAL');
INSERT INTO banco(id, codigo, nome)VALUES(101,079,'ORIGINAL DO AGRONEGÓCIO');
INSERT INTO banco(id, codigo, nome)VALUES(102,623,'PANAMERICANO');
INSERT INTO banco(id, codigo, nome)VALUES(103,254,'PARANÁ');
INSERT INTO banco(id, codigo, nome)VALUES(104,611,'PAULISTA');
INSERT INTO banco(id, codigo, nome)VALUES(105,613,'PECÚNIA');
INSERT INTO banco(id, codigo, nome)VALUES(106,094,'PETRA');
INSERT INTO banco(id, codigo, nome)VALUES(107,643,'PINE');
INSERT INTO banco(id, codigo, nome)VALUES(108,735,'POTTENCIAL');
INSERT INTO banco(id, codigo, nome)VALUES(109,495,'PROVÍNCIA B. AIRES');
INSERT INTO banco(id, codigo, nome)VALUES(110,747,'RABOBANK');
INSERT INTO banco(id, codigo, nome)VALUES(111,088,'RANDON');
INSERT INTO banco(id, codigo, nome)VALUES(112,633,'RENDIMENTO');
INSERT INTO banco(id, codigo, nome)VALUES(113,494,'REP. OR. URUGUAY');
INSERT INTO banco(id, codigo, nome)VALUES(114,741,'RIBEIRÃO PRETO');
INSERT INTO banco(id, codigo, nome)VALUES(115,120,'RODOBENS');
INSERT INTO banco(id, codigo, nome)VALUES(116,453,'RURAL');
INSERT INTO banco(id, codigo, nome)VALUES(117,072,'RURAL MAIS');
INSERT INTO banco(id, codigo, nome)VALUES(118,422,'SAFRA');
INSERT INTO banco(id, codigo, nome)VALUES(119,033,'SANTANDER');
INSERT INTO banco(id, codigo, nome)VALUES(120,092,'SCFI BRICKELL');
INSERT INTO banco(id, codigo, nome)VALUES(121,751,'SCOTIABANK BRASIL');
INSERT INTO banco(id, codigo, nome)VALUES(122,743,'SEMEAR');
INSERT INTO banco(id, codigo, nome)VALUES(123,748,'SICREDI');
INSERT INTO banco(id, codigo, nome)VALUES(124,749,'SIMPLES');
INSERT INTO banco(id, codigo, nome)VALUES(125,366,'SOCIÉTÉ GÉNÉRALE');
INSERT INTO banco(id, codigo, nome)VALUES(126,637,'SOFISA');
INSERT INTO banco(id, codigo, nome)VALUES(127,464,'SUMITOMO MITSUI');
INSERT INTO banco(id, codigo, nome)VALUES(128,456,'TOKYO - MITSUBISHI UFJ');
INSERT INTO banco(id, codigo, nome)VALUES(129,082,'TOPAZIO');
INSERT INTO banco(id, codigo, nome)VALUES(130,634,'TRIÂNGULO');
INSERT INTO banco(id, codigo, nome)VALUES(131,409,'UNIBANCO');
INSERT INTO banco(id, codigo, nome)VALUES(132,655,'VOTORANTIM');
INSERT INTO banco(id, codigo, nome)VALUES(133,610,'VR');
INSERT INTO banco(id, codigo, nome)VALUES(134,119,'WESTERN UNION');
INSERT INTO banco(id, codigo, nome)VALUES(135,370,'WESTLB');
INSERT INTO banco(id, codigo, nome)VALUES(136,124,'WOORI BANK');
INSERT INTO banco(id, codigo, nome)VALUES(137,641,'ALVORADA');
INSERT INTO banco(id, codigo, nome)VALUES(138,024,'BANDEPE');
INSERT INTO banco(id, codigo, nome)VALUES(139,095,'BCAM CONFIDENCE');
INSERT INTO banco(id, codigo, nome)VALUES(140,126,'BI BR PARTNERS');
INSERT INTO banco(id, codigo, nome)VALUES(141,012,'BI STANDARD');
INSERT INTO banco(id, codigo, nome)VALUES(142,118,'BI STANDARD CHARTERED');
INSERT INTO banco(id, codigo, nome)VALUES(143,044,'BVA-SOB INTERVENÇÃO');
INSERT INTO banco(id, codigo, nome)VALUES(144,016,'CC CREDITRAN');
INSERT INTO banco(id, codigo, nome)VALUES(145,111,'DTVM OLIVEIRA TRUST');
INSERT INTO banco(id, codigo, nome)VALUES(146,101,'DTVM RENASCENÇA');
INSERT INTO banco(id, codigo, nome)VALUES(147,064,'GOLDMAN SACHS');
INSERT INTO banco(id, codigo, nome)VALUES(148,029,'ITAÚ CONSIGNADO');
INSERT INTO banco(id, codigo, nome)VALUES(149,479,'ITAUBANK');
INSERT INTO banco(id, codigo, nome)VALUES(150,014,'NATIXIS BRASIL');
INSERT INTO banco(id, codigo, nome)VALUES(151,117,'SC ADVANCED');
INSERT INTO banco(id, codigo, nome)VALUES(152,080,'SC BT ASSOCIADOS');
INSERT INTO banco(id, codigo, nome)VALUES(153,127,'SC CODEPE');
INSERT INTO banco(id, codigo, nome)VALUES(154,060,'SC CONFIDENCE');
INSERT INTO banco(id, codigo, nome)VALUES(155,011,'SC CREDIT SUISSE HG');
INSERT INTO banco(id, codigo, nome)VALUES(156,103,'SC EBS');
INSERT INTO banco(id, codigo, nome)VALUES(157,015,'SC LINK');
INSERT INTO banco(id, codigo, nome)VALUES(158,113,'SC MAGLIANO');
INSERT INTO banco(id, codigo, nome)VALUES(159,100,'SC PLANNER');
INSERT INTO banco(id, codigo, nome)VALUES(160,013,'SC SENSO');
INSERT INTO banco(id, codigo, nome)VALUES(161,901,'SC SOUZA BARROS');
INSERT INTO banco(id, codigo, nome)VALUES(162,102,'SC XP INVESTIMENTOS');
INSERT INTO banco(id, codigo, nome)VALUES(163,123,'SCFI AGIPLAN');
INSERT INTO banco(id, codigo, nome)VALUES(164,105,'SCFI LECCA');
INSERT INTO banco(id, codigo, nome)VALUES(165,108,'SCFI PORTOCRED');
INSERT INTO banco(id, codigo, nome)VALUES(166,093,'SCM PÓLOCRED');
INSERT INTO banco(id, codigo, nome)VALUES(167,230,'UNICARD');

/* ExecuteEmbeddedSqlScript Fashion.ERP.Migrator.Scripts._201301010000.compensacaocheque.sql */
INSERT INTO compensacaocheque(id, codigo, descricao)VALUES(1,11,'CHEQUE SEM FUNDOS - 1ª APRESENTAÇÃO');
INSERT INTO compensacaocheque(id, codigo, descricao)VALUES(2,12,'CHEQUE SEM FUNDOS - 2ª APRESENTAÇÃO');
INSERT INTO compensacaocheque(id, codigo, descricao)VALUES(3,13,'CONTA ENCERRADA');
INSERT INTO compensacaocheque(id, codigo, descricao)VALUES(4,14,'PRÁTICA ESPÚRIA');
INSERT INTO compensacaocheque(id, codigo, descricao)VALUES(5,20,'CHEQUE SUSTADO OU REVOGADO EM VIRTUDE DE ROUBO, FURTO OU EXTRAVIO DE FOLHAS DE CHEQUE EM BRANCO');
INSERT INTO compensacaocheque(id, codigo, descricao)VALUES(6,21,'CHEQUE SUSTADO OU REVOGADO');
INSERT INTO compensacaocheque(id, codigo, descricao)VALUES(7,22,'DIVERGÊNCIA OU INSUFICIÊNCIA DE ASSINATURA');
INSERT INTO compensacaocheque(id, codigo, descricao)VALUES(8,23,'CHEQUES EMITIDOS POR ENTIDADES E ÓRGÃOS DA ADMINISTRAÇÃO PÚBLICA FEDERAL DIRETA E INDIRETA, EM DESACORDO COM OS REQUISITOS CONSTANTES DO ART. 74, § 2º, DO DECRETO-LEI Nº 200, DE 25.2.1967');
INSERT INTO compensacaocheque(id, codigo, descricao)VALUES(9,24,'BLOQUEIO JUDICIAL OU DETERMINAÇÃO DO BANCO CENTRAL DO BRASIL');
INSERT INTO compensacaocheque(id, codigo, descricao)VALUES(10,25,'CANCELAMENTO DE TALONÁRIO PELO PARTICIPANTE DESTINATÁRIO');
INSERT INTO compensacaocheque(id, codigo, descricao)VALUES(11,26,'INOPERÂNCIA TEMPORÁRIA DE TRANSPORTE');
INSERT INTO compensacaocheque(id, codigo, descricao)VALUES(12,27,'FERIADO MUNICIPAL NÃO PREVISTO');
INSERT INTO compensacaocheque(id, codigo, descricao)VALUES(13,28,'CHEQUE SUSTADO OU REVOGADO EM VIRTUDE DE ROUBO, FURTO OU EXTRAVIO');
INSERT INTO compensacaocheque(id, codigo, descricao)VALUES(14,29,'CHEQUE BLOQUEADO POR FALTA DE CONFIRMAÇÃO DE RECEBIMENTO DO TALONÁRIO PELO CORRENTISTA');
INSERT INTO compensacaocheque(id, codigo, descricao)VALUES(15,30,'FURTO OU ROUBO DE CHEQUE');
INSERT INTO compensacaocheque(id, codigo, descricao)VALUES(16,70,'SUSTAÇÃO OU REVOGAÇÃO PROVISÓRIA');
INSERT INTO compensacaocheque(id, codigo, descricao)VALUES(17,31,'ERRO FORMAL (SEM DATA DE EMISSÃO, COM O MÊS GRAFADO NUMERICAMENTE, AUSÊNCIA DE ASSINATURA OU NÃO REGISTRO DO VALOR POR EXTENSO)');
INSERT INTO compensacaocheque(id, codigo, descricao)VALUES(18,33,'DIVERGÊNCIA DE ENDOSSO');
INSERT INTO compensacaocheque(id, codigo, descricao)VALUES(19,34,'CHEQUE APRESENTADO POR PARTICIPANTE QUE NÃO O INDICADO NO CRUZAMENTO EM PRETO, SEM O ENDOSSO-MANDATO');
INSERT INTO compensacaocheque(id, codigo, descricao)VALUES(20,35,'CHEQUE FRAUDADO, EMITIDO SEM PRÉVIO CONTROLE OU RESPONSABILIDADE DO PARTICIPANTE ("CHEQUE UNIVERSAL"), OU AINDA COM ADULTERAÇÃO DA PRAÇA SACADA, OU AINDA COM RASURA NO PREENCHIMENTO');
INSERT INTO compensacaocheque(id, codigo, descricao)VALUES(21,37,'REGISTRO INCONSISTENTE');
INSERT INTO compensacaocheque(id, codigo, descricao)VALUES(22,38,'ASSINATURA DIGITAL AUSENTE OU INVÁLIDA');
INSERT INTO compensacaocheque(id, codigo, descricao)VALUES(23,39,'IMAGEM FORA DO PADRÃO');
INSERT INTO compensacaocheque(id, codigo, descricao)VALUES(24,40,'MOEDA INVÁLIDA');
INSERT INTO compensacaocheque(id, codigo, descricao)VALUES(25,41,'CHEQUE APRESENTADO A PARTICIPANTE QUE NÃO O DESTINATÁRIO');
INSERT INTO compensacaocheque(id, codigo, descricao)VALUES(26,42,'CHEQUE NÃO COMPENSÁVEL NA SESSÃO OU SISTEMA DE COMPENSAÇÃO EM QUE APRESENTADO');
INSERT INTO compensacaocheque(id, codigo, descricao)VALUES(27,43,'CHEQUE, DEVOLVIDO ANTERIORMENTE PELOS MOTIVOS 21, 22, 23, 24, 31 E 34, NÃO PASSÍVEL DE REAPRESENTAÇÃO EM VIRTUDE DE PERSISTIR O MOTIVO DA DEVOLUÇÃO');
INSERT INTO compensacaocheque(id, codigo, descricao)VALUES(28,44,'CHEQUE PRESCRITO');
INSERT INTO compensacaocheque(id, codigo, descricao)VALUES(29,45,'CHEQUE EMITIDO POR ENTIDADE OBRIGADA A REALIZAR MOVIMENTAÇÃO E UTILIZAÇÃO DE RECURSOS FINANCEIROS DO TESOURO NACIONAL MEDIANTE ORDEM BANCÁRIA');
INSERT INTO compensacaocheque(id, codigo, descricao)VALUES(30,48,'CHEQUE DE VALOR SUPERIOR A R$ 100,00 (CEM REAIS), EMITIDO SEM A IDENTIFICAÇÃO DO BENEFICIÁRIO');
INSERT INTO compensacaocheque(id, codigo, descricao)VALUES(31,49,'REMESSA NULA, CARACTERIZADA PELA REAPRESENTAÇÃO DE CHEQUE DEVOLVIDO PELOS MOTIVOS 12, 13, 14, 20, 25, 28, 30, 35, 43, 44 E 45');
INSERT INTO compensacaocheque(id, codigo, descricao)VALUES(32,59,'INFORMAÇÃO ESSENCIAL FALTANTE OU INCONSISTENTE NÃO PASSÍVEL DE VERIFICAÇÃO PELO PARTICIPANTE REMETENTE E NÃO ENQUADRADA NO MOTIVO 31');
INSERT INTO compensacaocheque(id, codigo, descricao)VALUES(33,60,'INSTRUMENTO INADEQUADO PARA A FINALIDADE');
INSERT INTO compensacaocheque(id, codigo, descricao)VALUES(34,61,'ITEM NÃO COMPENSÁVEL');
INSERT INTO compensacaocheque(id, codigo, descricao)VALUES(35,64,'ARQUIVO LÓGICO NÃO PROCESSADO / PROCESSADO PARCIALMENTE');
INSERT INTO compensacaocheque(id, codigo, descricao)VALUES(36,71,'INADIMPLEMENTO CONTRATUAL DA COOPERATIVA DE CRÉDITO NO ACORDO DE COMPENSAÇÃO');
INSERT INTO compensacaocheque(id, codigo, descricao)VALUES(37,72,'CONTRATO DE COMPENSAÇÃO ENCERRADO');

/* ExecuteEmbeddedSqlScript Fashion.ERP.Migrator.Scripts._201301010000.permissao.sql */
insert into uniquekeys(tablename, nexthi) values('permissao', 0);
update uniquekeys set nexthi = 1 where nexthi = 0 and tablename = 'permissao';
-- Comum
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id, id) VALUES (NULL, 'Comum', NULL, 'Cadastros', 1, 0, NULL, 1);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id, id) VALUES ('Index', 'Comum', 'Usuario', 'Usuário', 1, 1, 1, 2);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id, id) VALUES ('Novo', 'Comum', 'Usuario', 'Novo', 0, 1, 2, 3);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id, id) VALUES ('Editar', 'Comum', 'Usuario', 'Editar', 0, 1, 2, 4);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id, id) VALUES ('Excluir', 'Comum', 'Usuario', 'Excluir', 0, 1, 2, 5);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id, id) VALUES ('Index', 'Comum', 'AreaInteresse', 'Área de interesse', 1, 1, 1, 6);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id, id) VALUES ('Novo', 'Comum', 'AreaInteresse', 'Novo', 0, 1, 6, 7);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id, id) VALUES ('Editar', 'Comum', 'AreaInteresse', 'Editar', 0, 1, 6, 8);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id, id) VALUES ('Excluir', 'Comum', 'AreaInteresse', 'Excluir', 0, 1, 6, 9);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id, id) VALUES ('Index', 'Comum', 'Cliente', 'Cliente', 1, 1, 1, 10);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id, id) VALUES ('Novo', 'Comum', 'Cliente', 'Novo', 0, 1, 10, 11);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id, id) VALUES ('Editar', 'Comum', 'Cliente', 'Editar', 0, 1, 10, 12);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id, id) VALUES ('Excluir', 'Comum', 'Cliente', 'Excluir', 0, 1, 10, 13);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id, id) VALUES ('PesquisarId', 'Comum', 'Cliente', 'PesquisarId', 0, 1, 10, 14);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id, id) VALUES ('Index', 'Comum', 'Fornecedor', 'Fornecedor', 1, 1, 1, 15);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id, id) VALUES ('Novo', 'Comum', 'Fornecedor', 'Novo', 0, 1, 15, 16);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id, id) VALUES ('Editar', 'Comum', 'Fornecedor', 'Editar', 0, 1, 15, 17);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id, id) VALUES ('Excluir', 'Comum', 'Fornecedor', 'Excluir', 0, 1, 15, 18);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id, id) VALUES ('Index', 'Comum', 'Funcionario', 'Funcionário', 1, 1, 1, 19);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id, id) VALUES ('Novo', 'Comum', 'Funcionario', 'Novo', 0, 1, 19, 20);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id, id) VALUES ('Editar', 'Comum', 'Funcionario', 'Editar', 0, 1, 19, 21);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id, id) VALUES ('Excluir', 'Comum', 'Funcionario', 'Excluir', 0, 1, 19, 22);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id, id) VALUES ('Index', 'Comum', 'GrauDependencia', 'Grau de dependência', 1, 1, 1, 23);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id, id) VALUES ('Novo', 'Comum', 'GrauDependencia', 'Novo', 0, 1, 23, 24);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id, id) VALUES ('Editar', 'Comum', 'GrauDependencia', 'Editar', 0, 1, 23, 25);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id, id) VALUES ('Excluir', 'Comum', 'GrauDependencia', 'Excluir', 0, 1, 23, 26);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id, id) VALUES ('Index', 'Comum', 'MeioPagamento', 'Meio de pagamento', 1, 1, 1, 27);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id, id) VALUES ('Novo', 'Comum', 'MeioPagamento', 'Novo', 0, 1, 27, 28);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id, id) VALUES ('Editar', 'Comum', 'MeioPagamento', 'Editar', 0, 1, 27, 29);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id, id) VALUES ('Excluir', 'Comum', 'MeioPagamento', 'Excluir', 0, 1, 27, 30);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id, id) VALUES ('Index', 'Comum', 'PerfilDeAcesso', 'Perfil de acesso', 1, 1, 1, 31);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id, id) VALUES ('Novo', 'Comum', 'PerfilDeAcesso', 'Novo', 0, 1, 31, 32);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id, id) VALUES ('Editar', 'Comum', 'PerfilDeAcesso', 'Editar', 0, 1, 31, 33);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id, id) VALUES ('Excluir', 'Comum', 'PerfilDeAcesso', 'Excluir', 0, 1, 31, 34);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id, id) VALUES ('Index', 'Comum', 'PrestadorServico', 'Prestador de servico', 1, 1, 1, 35);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id, id) VALUES ('Novo', 'Comum', 'PrestadorServico', 'Novo', 0, 1, 35, 36);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id, id) VALUES ('Editar', 'Comum', 'PrestadorServico', 'Editar', 0, 1, 35, 37);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id, id) VALUES ('Excluir', 'Comum', 'PrestadorServico', 'Excluir', 0, 1, 35, 38);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id, id) VALUES ('Index', 'Comum', 'Profissao', 'Profissão', 1, 1, 1, 39);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id, id) VALUES ('Novo', 'Comum', 'Profissao', 'Novo', 0, 1, 39, 40);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id, id) VALUES ('Editar', 'Comum', 'Profissao', 'Editar', 0, 1, 39, 41);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id, id) VALUES ('Excluir', 'Comum', 'Profissao', 'Excluir', 0, 1, 39, 42);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id, id) VALUES ('Index', 'Comum', 'TipoFornecedor', 'Tipo de fornecedor', 1, 1, 1, 43);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id, id) VALUES ('Novo', 'Comum', 'TipoFornecedor', 'Novo', 0, 1, 43, 44);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id, id) VALUES ('Editar', 'Comum', 'TipoFornecedor', 'Editar', 0, 1, 43, 45);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id, id) VALUES ('Excluir', 'Comum', 'TipoFornecedor', 'Excluir', 0, 1, 43, 46);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id, id) VALUES ('Index', 'Comum', 'Unidade', 'Unidade', 1, 1, 1, 47);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id, id) VALUES ('Novo', 'Comum', 'Unidade', 'Novo', 0, 1, 47, 48);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id, id) VALUES ('Editar', 'Comum', 'Unidade', 'Editar', 0, 1, 47, 49);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id, id) VALUES ('Excluir', 'Comum', 'Unidade', 'Excluir', 0, 1, 47, 50);

-- Financeiro
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id, id) VALUES (NULL, 'Financeiro', NULL, 'Financeiro', 1, 0, NULL, 51);

-- Cheque (Financeiro)
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id, id) VALUES (NULL, 'Financeiro', NULL, 'Cheque', 1, 0, 51, 52);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id, id) VALUES ('Index', 'Financeiro', 'ChequeRecebido', 'Cheque recebido', 1, 1, 52, 53);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id, id) VALUES ('Novo', 'Financeiro', 'ChequeRecebido', 'Novo', 0, 1, 53, 54);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id, id) VALUES ('Editar', 'Financeiro', 'ChequeRecebido', 'Editar', 0, 1, 53, 55);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id, id) VALUES ('Excluir', 'Financeiro', 'ChequeRecebido', 'Excluir', 0, 1, 53, 56);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id, id) VALUES ('Baixa', 'Financeiro', 'ChequeRecebido', 'Baixa', 0, 1, 53, 57);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id, id) VALUES ('ExcluirBaixa', 'Financeiro', 'ChequeRecebido', 'Excluir baixa', 0, 1, 57, 58);

-- Relatórios (Comum)
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id, id) VALUES (NULL, 'Comum', NULL, 'Relatório', 1, 0, 1, 59);
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id, id) VALUES ('FichaCliente', 'Comum', 'Relatorio', 'Ficha de cliente', 1, 1, 59, 60);
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id, id) VALUES ('ListaCliente', 'Comum', 'Relatorio', 'Lista de clientes', 1, 1, 59, 61);
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id, id) VALUES ('FichaFornecedor', 'Comum', 'Relatorio', 'Ficha de fornecedor', 1, 1, 59, 62);
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id, id) VALUES ('ListaFornecedor', 'Comum', 'Relatorio', 'Lista de fornecedores', 1, 1, 59, 63);
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id, id) VALUES ('FichaFuncionario', 'Comum', 'Relatorio', 'Ficha de funcionário', 1, 1, 59, 64);
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id, id) VALUES ('ListaFuncionario', 'Comum', 'Relatorio', 'Lista de funcionários', 1, 1, 59, 65);
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id, id) VALUES ('FichaPrestadorServico', 'Comum', 'Relatorio', 'Ficha de prestador de serviço', 1, 1, 59, 66);
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id, id) VALUES ('ListaPrestadorServico', 'Comum', 'Relatorio', 'Lista de prestadores de serviço', 1, 1, 59, 67);
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id, id) VALUES ('ListaChequeRecebido', 'Comum', 'Relatorio', 'Lista de cheques recebido', 1, 1, 59, 68);

-- Bancário (Financeiro)
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id, id) VALUES (NULL, 'Financeiro', NULL, 'Bancário', 1, 0, 51, 69);
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id, id) VALUES ('Index', 'Financeiro', 'ContaBancaria', 'Conta bancária', 1, 1, 69, 70);
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id, id) VALUES ('Novo', 'Financeiro', 'ContaBancaria', 'Novo', 0, 1, 70, 71);
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id, id) VALUES ('Editar', 'Financeiro', 'ContaBancaria', 'Editar', 0, 1, 70, 72);
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id, id) VALUES ('Excluir', 'Financeiro', 'ContaBancaria', 'Excluir', 0, 1, 70, 73);
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id, id) VALUES ('Index', 'Financeiro', 'ExtratoBancario', 'Extrato bancário', 1, 1, 69, 74);
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id, id) VALUES ('Novo', 'Financeiro', 'ExtratoBancario', 'Novo', 0, 1, 74, 75);
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id, id) VALUES ('Editar', 'Financeiro', 'ExtratoBancario', 'Editar', 0, 1, 74, 76);
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id, id) VALUES ('Excluir', 'Financeiro', 'ExtratoBancario', 'Excluir', 0, 1, 74, 77);

-- Relatórios (Financeiro)
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id, id) VALUES (NULL, 'Financeiro', NULL, 'Relatório', 1, 0, 51, 78);
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id, id) VALUES ('ListaChequeRecebido', 'Financeiro', 'Relatorio', 'Lista de cheques recebido', 1, 1, 78, 79);

INSERT INTO [dbo].[usuario] ([id], [nome], [login], [senha]) VALUES (1, 'ADMINISTRADOR', 'ADMIN', 'liGPElArb3rbEoweVxjMZv3cLrloK/aJuLia63zzTWCswhb5ybo94sPEE0+FWQSZYMV11AE6qCQKUsE9sTOYONZ1ajqj')
INSERT INTO [dbo].[uniquekeys] ([tablename], [nexthi]) VALUES ('usuario', 1)
INSERT INTO [dbo].[permissaotousuario] ([usuario_id], [permissao_id]) VALUES (1, 1)
INSERT INTO [dbo].[permissaotousuario] ([usuario_id], [permissao_id]) VALUES (1, 2)
INSERT INTO [dbo].[permissaotousuario] ([usuario_id], [permissao_id]) VALUES (1, 3)
INSERT INTO [dbo].[permissaotousuario] ([usuario_id], [permissao_id]) VALUES (1, 4)
INSERT INTO [dbo].[permissaotousuario] ([usuario_id], [permissao_id]) VALUES (1, 5)
INSERT INTO [dbo].[uniquekeys] ([tablename], [nexthi]) VALUES ('permissaotousuario', 1)
/* -> 8 Insert operations completed in 00:00:00.0080115 taking an average of 00:00:00.0010014 */
INSERT INTO [dbo].[VersionInfo] ([Version], [AppliedOn], [Description]) VALUES (201301010000, '2015-02-04T16:38:11', 'Migration201301010000')
/* Committing Transaction */
/* 201301010000: Migration201301010000 migrated */

/* 201307010000: Migration201307010000 migrating ============================= */

/* Beginning Transaction */
/* CreateTable unidademedida */
CREATE TABLE [dbo].[unidademedida] ([id] BIGINT NOT NULL, [descricao] NVARCHAR(100) NOT NULL, [sigla] NVARCHAR(5) NOT NULL, [fatormultiplicativo] DOUBLE PRECISION NOT NULL, CONSTRAINT [PK_unidademedida] PRIMARY KEY ([id]))

/* CreateTable marca */
CREATE TABLE [dbo].[marca] ([id] BIGINT NOT NULL, [nome] NVARCHAR(100) NOT NULL, CONSTRAINT [PK_marca] PRIMARY KEY ([id]))

/* CreateTable generofiscal */
CREATE TABLE [dbo].[generofiscal] ([id] BIGINT NOT NULL, [descricao] NVARCHAR(500) NOT NULL, [codigo] NVARCHAR(2) NOT NULL, CONSTRAINT [PK_generofiscal] PRIMARY KEY ([id]))

/* CreateTable familia */
CREATE TABLE [dbo].[familia] ([id] BIGINT NOT NULL, [nome] NVARCHAR(100) NOT NULL, CONSTRAINT [PK_familia] PRIMARY KEY ([id]))

/* CreateTable tipoitem */
CREATE TABLE [dbo].[tipoitem] ([id] BIGINT NOT NULL, [descricao] NVARCHAR(100) NOT NULL, [codigo] NVARCHAR(2) NOT NULL, CONSTRAINT [PK_tipoitem] PRIMARY KEY ([id]))

/* CreateTable categoria */
CREATE TABLE [dbo].[categoria] ([id] BIGINT NOT NULL, [nome] NVARCHAR(100) NOT NULL, CONSTRAINT [PK_categoria] PRIMARY KEY ([id]))

/* CreateTable subcategoria */
CREATE TABLE [dbo].[subcategoria] ([id] BIGINT NOT NULL, [nome] NVARCHAR(100) NOT NULL, [categoria_id] BIGINT NOT NULL, CONSTRAINT [PK_subcategoria] PRIMARY KEY ([id]))

/* CreateForeignKey FK_subcategoria_categoria subcategoria(categoria_id) categoria(id) */
ALTER TABLE [dbo].[subcategoria] ADD CONSTRAINT [FK_subcategoria_categoria] FOREIGN KEY ([categoria_id]) REFERENCES [dbo].[categoria] ([id])

/* CreateTable catalogomaterial */
CREATE TABLE [dbo].[catalogomaterial] ([id] BIGINT NOT NULL, [referencia] NVARCHAR(20) NOT NULL, [descricao] NVARCHAR(100) NOT NULL, [detalhamento] NVARCHAR(4000) NOT NULL, [codigobarra] NVARCHAR(50), [ncm] NVARCHAR(8) NOT NULL, [aliquota] DOUBLE PRECISION NOT NULL, [pesobruto] DOUBLE PRECISION NOT NULL, [pesoliquido] DOUBLE PRECISION NOT NULL, [origem] NVARCHAR(256) NOT NULL, [localizacao] NVARCHAR(100), [ativo] BIT NOT NULL, [foto_id] BIGINT NOT NULL, [unidademedida_id] BIGINT NOT NULL, [marca_id] BIGINT NOT NULL, [subcategoria_id] BIGINT NOT NULL, [tipoitem_id] BIGINT NOT NULL, [familia_id] BIGINT NOT NULL, [generofiscal_id] BIGINT NOT NULL, CONSTRAINT [PK_catalogomaterial] PRIMARY KEY ([id]))

/* CreateForeignKey FK_catalogomaterial_foto catalogomaterial(foto_id) arquivo(id) */
ALTER TABLE [dbo].[catalogomaterial] ADD CONSTRAINT [FK_catalogomaterial_foto] FOREIGN KEY ([foto_id]) REFERENCES [dbo].[arquivo] ([id])

/* CreateForeignKey FK_catalogomaterial_unidademedida catalogomaterial(unidademedida_id) unidademedida(id) */
ALTER TABLE [dbo].[catalogomaterial] ADD CONSTRAINT [FK_catalogomaterial_unidademedida] FOREIGN KEY ([unidademedida_id]) REFERENCES [dbo].[unidademedida] ([id])

/* CreateForeignKey FK_catalogomaterial_marca catalogomaterial(marca_id) marca(id) */
ALTER TABLE [dbo].[catalogomaterial] ADD CONSTRAINT [FK_catalogomaterial_marca] FOREIGN KEY ([marca_id]) REFERENCES [dbo].[marca] ([id])

/* CreateForeignKey FK_catalogomaterial_subcategoria catalogomaterial(subcategoria_id) subcategoria(id) */
ALTER TABLE [dbo].[catalogomaterial] ADD CONSTRAINT [FK_catalogomaterial_subcategoria] FOREIGN KEY ([subcategoria_id]) REFERENCES [dbo].[subcategoria] ([id])

/* CreateForeignKey FK_catalogomaterial_tipoitem catalogomaterial(tipoitem_id) tipoitem(id) */
ALTER TABLE [dbo].[catalogomaterial] ADD CONSTRAINT [FK_catalogomaterial_tipoitem] FOREIGN KEY ([tipoitem_id]) REFERENCES [dbo].[tipoitem] ([id])

/* CreateForeignKey FK_catalogomaterial_familia catalogomaterial(familia_id) familia(id) */
ALTER TABLE [dbo].[catalogomaterial] ADD CONSTRAINT [FK_catalogomaterial_familia] FOREIGN KEY ([familia_id]) REFERENCES [dbo].[familia] ([id])

/* CreateForeignKey FK_catalogomaterial_generofiscal catalogomaterial(generofiscal_id) generofiscal(id) */
ALTER TABLE [dbo].[catalogomaterial] ADD CONSTRAINT [FK_catalogomaterial_generofiscal] FOREIGN KEY ([generofiscal_id]) REFERENCES [dbo].[generofiscal] ([id])

/* CreateTable referenciaexterna */
CREATE TABLE [dbo].[referenciaexterna] ([id] BIGINT NOT NULL, [referencia] NVARCHAR(20) NOT NULL, [descricao] NVARCHAR(100) NOT NULL, [codigobarra] NVARCHAR(8), [preco] DOUBLE PRECISION NOT NULL, [catalogomaterial_id] BIGINT NOT NULL, [fornecedor_id] BIGINT NOT NULL, CONSTRAINT [PK_referenciaexterna] PRIMARY KEY ([id]))

/* CreateForeignKey FK_referenciaexterna_catalogomaterial referenciaexterna(catalogomaterial_id) catalogomaterial(id) */
ALTER TABLE [dbo].[referenciaexterna] ADD CONSTRAINT [FK_referenciaexterna_catalogomaterial] FOREIGN KEY ([catalogomaterial_id]) REFERENCES [dbo].[catalogomaterial] ([id])

/* CreateForeignKey FK_referenciaexterna_fornecedor referenciaexterna(fornecedor_id) pessoa(id) */
ALTER TABLE [dbo].[referenciaexterna] ADD CONSTRAINT [FK_referenciaexterna_fornecedor] FOREIGN KEY ([fornecedor_id]) REFERENCES [dbo].[pessoa] ([id])

/* ExecuteEmbeddedSqlScript Fashion.ERP.Migrator.Scripts._201307010000.permissao.sql */
-- Almoxarifado
update uniquekeys set nexthi = 2 where nexthi = 1 and tablename = 'permissao';
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id, id) VALUES (NULL, 'Almoxarifado', NULL, 'Almoxarifado', 1, 0, NULL, 101);
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id, id) VALUES ('Index', 'Almoxarifado', 'CatalogoMaterial', 'Catálogo de material', 1, 1, 101, 102);
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id, id) VALUES ('Editar', 'Almoxarifado', 'CatalogoMaterial', 'Editar', 0, 1, 102, 103);
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id, id) VALUES ('Excluir', 'Almoxarifado', 'CatalogoMaterial', 'Excluir', 0, 1, 102, 104);
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id, id) VALUES ('Novo', 'Almoxarifado', 'CatalogoMaterial', 'Novo', 0, 1, 102, 105);
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id, id) VALUES ('Index', 'Almoxarifado', 'Categoria', 'Categoria', 1, 1, 101, 106);
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id, id) VALUES ('Editar', 'Almoxarifado', 'Categoria', 'Editar', 0, 1, 106, 107);
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id, id) VALUES ('Excluir', 'Almoxarifado', 'Categoria', 'Excluir', 0, 1, 106, 108);
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id, id) VALUES ('Novo', 'Almoxarifado', 'Categoria', 'Novo', 0, 1, 106, 109);
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id, id) VALUES ('Index', 'Almoxarifado', 'Familia', 'Família', 1, 1, 101, 110);
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id, id) VALUES ('Editar', 'Almoxarifado', 'Familia', 'Editar', 0, 1, 110, 111);
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id, id) VALUES ('Excluir', 'Almoxarifado', 'Familia', 'Excluir', 0, 1, 110, 112);
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id, id) VALUES ('Novo', 'Almoxarifado', 'Familia', 'Novo', 0, 1, 110, 113);
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id, id) VALUES ('Index', 'Almoxarifado', 'Marca', 'Marca', 1, 1, 101, 114);
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id, id) VALUES ('Editar', 'Almoxarifado', 'Marca', 'Editar', 0, 1, 114, 115);
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id, id) VALUES ('Excluir', 'Almoxarifado', 'Marca', 'Excluir', 0, 1, 114, 116);
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id, id) VALUES ('Novo', 'Almoxarifado', 'Marca', 'Novo', 0, 1, 114, 117);
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id, id) VALUES ('Index', 'Almoxarifado', 'Subcategoria', 'Subcategoria', 1, 1, 101, 118);
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id, id) VALUES ('Editar', 'Almoxarifado', 'Subcategoria', 'Editar', 0, 1, 118, 119);
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id, id) VALUES ('Excluir', 'Almoxarifado', 'Subcategoria', 'Excluir', 0, 1, 118, 120);
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id, id) VALUES ('Novo', 'Almoxarifado', 'Subcategoria', 'Novo', 0, 1, 118, 121);
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id, id) VALUES ('Index', 'Almoxarifado', 'UnidadeMedida', 'Unidade de medida', 1, 1, 101, 122);
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id, id) VALUES ('Editar', 'Almoxarifado', 'UnidadeMedida', 'Editar', 0, 1, 122, 123);
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id, id) VALUES ('Excluir', 'Almoxarifado', 'UnidadeMedida', 'Excluir', 0, 1, 122, 124);
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id, id) VALUES ('Novo', 'Almoxarifado', 'UnidadeMedida', 'Novo', 0, 1, 122, 125);

/* ExecuteEmbeddedSqlScript Fashion.ERP.Migrator.Scripts._201307010000.generofiscal.sql */
INSERT INTO generofiscal (id, codigo, descricao) VALUES (1, '0' ,'Serviço');
INSERT INTO generofiscal (id, codigo, descricao) VALUES (2, '1' ,'Animais vivos');
INSERT INTO generofiscal (id, codigo, descricao) VALUES (3, '2' ,'Carnes e miudezas, comestíveis');
INSERT INTO generofiscal (id, codigo, descricao) VALUES (4, '3' ,'Peixes e crustáceos, moluscos e os outros invertebrados aquáticos');
INSERT INTO generofiscal (id, codigo, descricao) VALUES (5, '4' ,'Leite e laticínios; ovos de aves; mel natural; produtos comestíveis de origem animal, não especificados nem compreendidos em outros Capítulos da TIPI');
INSERT INTO generofiscal (id, codigo, descricao) VALUES (6, '5' ,'Outros produtos de origem animal, não especificados nem compreendidos em outros Capítulos da TIPI');
INSERT INTO generofiscal (id, codigo, descricao) VALUES (7, '6' ,'Plantas vivas e produtos de floricultura');
INSERT INTO generofiscal (id, codigo, descricao) VALUES (8, '7' ,'Produtos hortícolas, plantas, raízes e tubérculos, comestíveis');
INSERT INTO generofiscal (id, codigo, descricao) VALUES (9, '8' ,'Frutas; cascas de cítricos e de melões');
INSERT INTO generofiscal (id, codigo, descricao) VALUES (10, '9' ,'Café, chá, mate e especiarias');
INSERT INTO generofiscal (id, codigo, descricao) VALUES (11, '10' ,'Cereais');
INSERT INTO generofiscal (id, codigo, descricao) VALUES (12, '11' ,'Produtos da indústria de moagem; malte; amidos e féculas; inulina; glúten de trigo');
INSERT INTO generofiscal (id, codigo, descricao) VALUES (13, '12' ,'Sementes e frutos oleaginosos; grãos, sementes e frutos diversos; plantas industriais ou medicinais; palha e forragem');
INSERT INTO generofiscal (id, codigo, descricao) VALUES (14, '13' ,'Gomas, resinas e outros sucos e extratos vegetais');
INSERT INTO generofiscal (id, codigo, descricao) VALUES (15, '14' ,'Matérias para entrançar e outros produtos de origem vegetal, não especificadas nem compreendidas em outros Capítulos da NCM');
INSERT INTO generofiscal (id, codigo, descricao) VALUES (16, '15' ,'Gorduras e óleos animais ou vegetais; produtos da sua dissociação; gorduras alimentares elaboradas; ceras de origem animal ou vegetal');
INSERT INTO generofiscal (id, codigo, descricao) VALUES (17, '16' ,'Preparações de carne, de peixes ou de crustáceos, de moluscos ou de outros invertebrados aquáticos');
INSERT INTO generofiscal (id, codigo, descricao) VALUES (18, '17' ,'Açúcares e produtos de confeitaria');
INSERT INTO generofiscal (id, codigo, descricao) VALUES (19, '18' ,'Cacau e suas preparações');
INSERT INTO generofiscal (id, codigo, descricao) VALUES (20, '19' ,'Preparações à base de cereais, farinhas, amidos, féculas ou de leite; produtos de pastelaria');
INSERT INTO generofiscal (id, codigo, descricao) VALUES (21, '20' ,'Preparações de produtos hortícolas, de frutas ou de outras partes de plantas');
INSERT INTO generofiscal (id, codigo, descricao) VALUES (22, '21' ,'Preparações alimentícias diversas');
INSERT INTO generofiscal (id, codigo, descricao) VALUES (23, '22' ,'Bebidas, líquidos alcoólicos e vinagres');
INSERT INTO generofiscal (id, codigo, descricao) VALUES (24, '23' ,'Resíduos e desperdícios das indústrias alimentares; alimentos preparados para animais');
INSERT INTO generofiscal (id, codigo, descricao) VALUES (25, '24' ,'Fumo (tabaco) e seus sucedâneos, manufaturados');
INSERT INTO generofiscal (id, codigo, descricao) VALUES (26, '25' ,'Sal; enxofre; terras e pedras; gesso, cal e cimento');
INSERT INTO generofiscal (id, codigo, descricao) VALUES (27, '26' ,'Minérios, escórias e cinzas');
INSERT INTO generofiscal (id, codigo, descricao) VALUES (28, '27' ,'Combustíveis minerais, óleos minerais e produtos de sua destilação; matérias betuminosas; ceras minerais');
INSERT INTO generofiscal (id, codigo, descricao) VALUES (29, '28' ,'Produtos químicos inorgânicos; compostos inorgânicos ou orgânicos de metais preciosos, de elementos radioativos, de metais das terras raras ou de isótopos');
INSERT INTO generofiscal (id, codigo, descricao) VALUES (30, '29' ,'Produtos químicos orgânicos');
INSERT INTO generofiscal (id, codigo, descricao) VALUES (31, '30' ,'Produtos farmacêuticos');
INSERT INTO generofiscal (id, codigo, descricao) VALUES (32, '31' ,'Adubos ou fertilizantes');
INSERT INTO generofiscal (id, codigo, descricao) VALUES (33, '32' ,'Extratos tanantes e tintoriais; taninos e seus derivados; pigmentos e outras matérias corantes, tintas e vernizes, mástiques; tintas de escrever');
INSERT INTO generofiscal (id, codigo, descricao) VALUES (34, '33' ,'Óleos essenciais e resinóides; produtos de perfumaria ou de toucador preparados e preparações cosméticas');
INSERT INTO generofiscal (id, codigo, descricao) VALUES (35, '35' ,'Matérias albuminóides; produtos à base de amidos ou de féculas modificados; colas; enzimas');
INSERT INTO generofiscal (id, codigo, descricao) VALUES (36, '36' ,'Pólvoras e explosivos; artigos de pirotecnia; fósforos; ligas pirofóricas; matérias inflamáveis');
INSERT INTO generofiscal (id, codigo, descricao) VALUES (37, '37' ,'Produtos para fotografia e cinematografia');
INSERT INTO generofiscal (id, codigo, descricao) VALUES (38, '38' ,'Produtos diversos das indústrias químicas');
INSERT INTO generofiscal (id, codigo, descricao) VALUES (39, '39' ,'Plásticos e suas obras');
INSERT INTO generofiscal (id, codigo, descricao) VALUES (40, '40' ,'Borracha e suas obras');
INSERT INTO generofiscal (id, codigo, descricao) VALUES (41, '41' ,'Peles, exceto a peleteria (peles com pêlo*), e couros');
INSERT INTO generofiscal (id, codigo, descricao) VALUES (42, '42' ,'Obras de couro; artigos de correeiro ou de seleiro; artigos de viagem, bolsas e artefatos semelhantes; obras de tripa');
INSERT INTO generofiscal (id, codigo, descricao) VALUES (43, '43' ,'Peleteria (peles com pêlo*) e suas obras; peleteria (peles com pêlo*) artificial');
INSERT INTO generofiscal (id, codigo, descricao) VALUES (44, '44' ,'Madeira, carvão vegetal e obras de madeira');
INSERT INTO generofiscal (id, codigo, descricao) VALUES (45, '45' ,'Cortiça e suas obras');
INSERT INTO generofiscal (id, codigo, descricao) VALUES (46, '46' ,'Obras de espartaria ou de cestaria');
INSERT INTO generofiscal (id, codigo, descricao) VALUES (47, '47' ,'Pastas de madeira ou de outras matérias fibrosas celulósicas; papel ou cartão de reciclar (desperdícios e aparas)');
INSERT INTO generofiscal (id, codigo, descricao) VALUES (48, '48' ,'Papel e cartão; obras de pasta de celulose, de papel ou de cartão');
INSERT INTO generofiscal (id, codigo, descricao) VALUES (49, '49' ,'Livros, jornais, gravuras e outros produtos das indústrias gráficas; textos manuscritos ou datilografados, planos e plantas');
INSERT INTO generofiscal (id, codigo, descricao) VALUES (50, '50' ,'Seda');
INSERT INTO generofiscal (id, codigo, descricao) VALUES (51, '51' ,'Lã e pêlos finos ou grosseiros; fios e tecidos de crina');
INSERT INTO generofiscal (id, codigo, descricao) VALUES (52, '52' ,'Algodão');
INSERT INTO generofiscal (id, codigo, descricao) VALUES (53, '53' ,'Outras fibras têxteis vegetais; fios de papel e tecido de fios de papel');
INSERT INTO generofiscal (id, codigo, descricao) VALUES (54, '54' ,'Filamentos sintéticos ou artificiais');
INSERT INTO generofiscal (id, codigo, descricao) VALUES (55, '55' ,'Fibras sintéticas ou artificiais, descontínuas');
INSERT INTO generofiscal (id, codigo, descricao) VALUES (56, '56' ,'Pastas ("ouates"), feltros e falsos tecidos; fios especiais; cordéis, cordas e cabos; artigos de cordoaria');
INSERT INTO generofiscal (id, codigo, descricao) VALUES (57, '57' ,'Tapetes e outros revestimentos para pavimentos, de matérias têxteis');
INSERT INTO generofiscal (id, codigo, descricao) VALUES (58, '58' ,'Tecidos especiais; tecidos tufados; rendas; tapeçarias; passamanarias; bordados');
INSERT INTO generofiscal (id, codigo, descricao) VALUES (59, '59' ,'Tecidos impregnados, revestidos, recobertos ou estratificados; artigos para usos técnicos de matérias têxteis');
INSERT INTO generofiscal (id, codigo, descricao) VALUES (60, '60' ,'Tecidos de malha');
INSERT INTO generofiscal (id, codigo, descricao) VALUES (61, '61' ,'Vestuário e seus acessórios, de malha');
INSERT INTO generofiscal (id, codigo, descricao) VALUES (62, '62' ,'Vestuário e seus acessórios, exceto de malha');
INSERT INTO generofiscal (id, codigo, descricao) VALUES (63, '63' ,'Outros artefatos têxteis confeccionados; sortidos; artefatos de matérias têxteis, calçados, chapéus e artefatos de uso semelhante, usados; trapos');
INSERT INTO generofiscal (id, codigo, descricao) VALUES (64, '64' ,'Calçados, polainas e artefatos semelhantes, e suas partes');
INSERT INTO generofiscal (id, codigo, descricao) VALUES (65, '65' ,'Chapéus e artefatos de uso semelhante, e suas partes');
INSERT INTO generofiscal (id, codigo, descricao) VALUES (66, '66' ,'Guarda-chuvas, sombrinhas, guarda-sóis, bengalas, bengalas-assentos, chicotes, e suas partes');
INSERT INTO generofiscal (id, codigo, descricao) VALUES (67, '67' ,'Penas e penugem preparadas, e suas obras; flores artificiais; obras de cabelo');
INSERT INTO generofiscal (id, codigo, descricao) VALUES (68, '68' ,'Obras de pedra, gesso, cimento, amianto, mica ou de matérias semelhantes');
INSERT INTO generofiscal (id, codigo, descricao) VALUES (69, '69' ,'Produtos cerâmicos');
INSERT INTO generofiscal (id, codigo, descricao) VALUES (70, '70' ,'Vidro e suas obras');
INSERT INTO generofiscal (id, codigo, descricao) VALUES (71, '71' ,'Pérolas naturais ou cultivadas, pedras preciosas ou semipreciosas e semelhantes, metais preciosos, metais folheados ou chapeados de metais preciosos, e suas obras; bijuterias; moedas');
INSERT INTO generofiscal (id, codigo, descricao) VALUES (72, '72' ,'Ferro fundido, ferro e aço');
INSERT INTO generofiscal (id, codigo, descricao) VALUES (73, '73' ,'Obras de ferro fundido, ferro ou aço');
INSERT INTO generofiscal (id, codigo, descricao) VALUES (74, '74' ,'Cobre e suas obras');
INSERT INTO generofiscal (id, codigo, descricao) VALUES (75, '75' ,'Níquel e suas obras');
INSERT INTO generofiscal (id, codigo, descricao) VALUES (76, '76' ,'Alumínio e suas obras');
INSERT INTO generofiscal (id, codigo, descricao) VALUES (77, '77' ,'(Reservado para uma eventual utilização futura no SH)');
INSERT INTO generofiscal (id, codigo, descricao) VALUES (78, '78' ,'Chumbo e suas obras');
INSERT INTO generofiscal (id, codigo, descricao) VALUES (79, '79' ,'Zinco e suas obras');
INSERT INTO generofiscal (id, codigo, descricao) VALUES (80, '80' ,'Estanho e suas obras');
INSERT INTO generofiscal (id, codigo, descricao) VALUES (81, '81' ,'Outros metais comuns; ceramais ("cermets"); obras dessas matérias');
INSERT INTO generofiscal (id, codigo, descricao) VALUES (82, '82' ,'Ferramentas, artefatos de cutelaria e talheres, e suas partes, de metais comuns');
INSERT INTO generofiscal (id, codigo, descricao) VALUES (83, '83' ,'Obras diversas de metais comuns');
INSERT INTO generofiscal (id, codigo, descricao) VALUES (84, '84' ,'Reatores nucleares, caldeiras, máquinas, aparelhos e instrumentos mecânicos, e suas partes');
INSERT INTO generofiscal (id, codigo, descricao) VALUES (85, '85' ,'Máquinas, aparelhos e materiais elétricos, e suas partes; aparelhos de gravação ou de reprodução de som, aparelhos de gravação ou de reprodução de imagens e de som em televisão, e suas partes e acessórios');
INSERT INTO generofiscal (id, codigo, descricao) VALUES (86, '86' ,'Veículos e material para vias férreas ou semelhantes, e suas partes; aparelhos mecânicos (incluídos os eletromecânicos) de sinalização para vias de comunicação');
INSERT INTO generofiscal (id, codigo, descricao) VALUES (87, '87' ,'Veículos automóveis, tratores, ciclos e outros veículos terrestres, suas partes e acessórios');
INSERT INTO generofiscal (id, codigo, descricao) VALUES (88, '88' ,'Aeronaves e aparelhos espaciais, e suas partes');
INSERT INTO generofiscal (id, codigo, descricao) VALUES (89, '89' ,'Embarcações e estruturas flutuantes');
INSERT INTO generofiscal (id, codigo, descricao) VALUES (90, '90' ,'Instrumentos e aparelhos de óptica, fotografia ou cinematografia, medida, controle ou de precisão; instrumentos e aparelhos médico-cirúrgicos; suas partes e acessórios');
INSERT INTO generofiscal (id, codigo, descricao) VALUES (91, '91' ,'Aparelhos de relojoaria e suas partes');
INSERT INTO generofiscal (id, codigo, descricao) VALUES (92, '92' ,'Instrumentos musicais, suas partes e acessórios');
INSERT INTO generofiscal (id, codigo, descricao) VALUES (93, '93' ,'Armas e munições; suas partes e acessórios');
INSERT INTO generofiscal (id, codigo, descricao) VALUES (94, '94' ,'Móveis, mobiliário médico-cirúrgico; colchões; iluminação e construção pré-fabricadas');
INSERT INTO generofiscal (id, codigo, descricao) VALUES (95, '95' ,'Brinquedos, jogos, artigos para divertimento ou para esporte; suas partes e acessórios');
INSERT INTO generofiscal (id, codigo, descricao) VALUES (96, '96' ,'Obras diversas');
INSERT INTO generofiscal (id, codigo, descricao) VALUES (97, '97' ,'Objetos de arte, de coleção e antiguidades');
INSERT INTO generofiscal (id, codigo, descricao) VALUES (98, '98' ,'(Reservado para usos especiais pelas Partes Contratantes)');
INSERT INTO generofiscal (id, codigo, descricao) VALUES (99, '99' ,'Operações especiais (utilizado exclusivamente pelo Brasil para classificar operações especiais na exportação)');
INSERT INTO generofiscal (id, codigo, descricao) VALUES (100, '34' ,'Sabões, agentes orgânicos de superfície, preparações para lavagem, preparações lubrificantes, ceras artificiais, ceras preparadas, produtos de conservação e limpeza, velas e artigos semelhantes, massas ou pastas de modelar, "ceras" para dentistas e composições para dentista à base de gesso');

/* ExecuteEmbeddedSqlScript Fashion.ERP.Migrator.Scripts._201307010000.tipoitem.sql */
INSERT INTO tipoitem (id,codigo,descricao) VALUES (1,'00','Mercadoria para Revenda');
INSERT INTO tipoitem (id,codigo,descricao) VALUES (2,'01','Matéria-Prima');
INSERT INTO tipoitem (id,codigo,descricao) VALUES (3,'02','Embalagem');
INSERT INTO tipoitem (id,codigo,descricao) VALUES (4,'03','Produto em Processo');
INSERT INTO tipoitem (id,codigo,descricao) VALUES (5,'04','Produto Acabado');
INSERT INTO tipoitem (id,codigo,descricao) VALUES (6,'05','Subproduto');
INSERT INTO tipoitem (id,codigo,descricao) VALUES (7,'06','Produto Intermediário');
INSERT INTO tipoitem (id,codigo,descricao) VALUES (8,'07','Material de Uso e Consumo');
INSERT INTO tipoitem (id,codigo,descricao) VALUES (9,'08','Ativo Imobilizado');
INSERT INTO tipoitem (id,codigo,descricao) VALUES (10,'09','Serviços');
INSERT INTO tipoitem (id,codigo,descricao) VALUES (11,'10','Outros insumos');
INSERT INTO tipoitem (id,codigo,descricao) VALUES (12,'99','Outras');


INSERT INTO [dbo].[VersionInfo] ([Version], [AppliedOn], [Description]) VALUES (201307010000, '2015-02-04T16:38:11', 'Migration201307010000')
/* Committing Transaction */
/* 201307010000: Migration201307010000 migrated */

/* 201308271102: Migration201308271102 migrating ============================= */

/* Beginning Transaction */
/* CreateTable artigo */
CREATE TABLE [dbo].[artigo] ([id] BIGINT NOT NULL, [descricao] NVARCHAR(50) NOT NULL, [ativo] BIT NOT NULL, CONSTRAINT [PK_artigo] PRIMARY KEY ([id]))

/* CreateTable classificacao */
CREATE TABLE [dbo].[classificacao] ([id] BIGINT NOT NULL, [descricao] NVARCHAR(50) NOT NULL, [ativo] BIT NOT NULL, CONSTRAINT [PK_classificacao] PRIMARY KEY ([id]))

/* CreateTable colecao */
CREATE TABLE [dbo].[colecao] ([id] BIGINT NOT NULL, [descricao] NVARCHAR(50) NOT NULL, [ativo] BIT NOT NULL, CONSTRAINT [PK_colecao] PRIMARY KEY ([id]))

/* CreateTable comprimento */
CREATE TABLE [dbo].[comprimento] ([id] BIGINT NOT NULL, [descricao] NVARCHAR(50) NOT NULL, [ativo] BIT NOT NULL, CONSTRAINT [PK_comprimento] PRIMARY KEY ([id]))

/* CreateTable natureza */
CREATE TABLE [dbo].[natureza] ([id] BIGINT NOT NULL, [descricao] NVARCHAR(50) NOT NULL, [ativo] BIT NOT NULL, CONSTRAINT [PK_natureza] PRIMARY KEY ([id]))

/* CreateTable produtobase */
CREATE TABLE [dbo].[produtobase] ([id] BIGINT NOT NULL, [descricao] NVARCHAR(50) NOT NULL, [ativo] BIT NOT NULL, CONSTRAINT [PK_produtobase] PRIMARY KEY ([id]))

/* CreateTable barra */
CREATE TABLE [dbo].[barra] ([id] BIGINT NOT NULL, [descricao] NVARCHAR(50) NOT NULL, [ativo] BIT NOT NULL, CONSTRAINT [PK_barra] PRIMARY KEY ([id]))

/* AlterColumn referenciaexterna codigobarra String */
ALTER TABLE [dbo].[referenciaexterna] ALTER COLUMN [codigobarra] NVARCHAR(50)

/* CreateTable departamentoproducao */
CREATE TABLE [dbo].[departamentoproducao] ([id] BIGINT NOT NULL, [nome] NVARCHAR(50) NOT NULL, [criacao] BIT NOT NULL, [producao] BIT NOT NULL, [ativo] BIT NOT NULL, CONSTRAINT [PK_departamentoproducao] PRIMARY KEY ([id]))

/* CreateTable relatorio */
CREATE TABLE [dbo].[relatorio] ([id] BIGINT NOT NULL, [nome] NVARCHAR(100) NOT NULL, [arquivo_id] BIGINT NOT NULL, CONSTRAINT [PK_relatorio] PRIMARY KEY ([id]))

/* CreateForeignKey FK_relatorio_arquivo relatorio(arquivo_id) arquivo(id) */
ALTER TABLE [dbo].[relatorio] ADD CONSTRAINT [FK_relatorio_arquivo] FOREIGN KEY ([arquivo_id]) REFERENCES [dbo].[arquivo] ([id])

/* CreateTable relatorioparametro */
CREATE TABLE [dbo].[relatorioparametro] ([id] BIGINT NOT NULL, [nome] NVARCHAR(100) NOT NULL, [tiporelatorioparametro] NVARCHAR(255) NOT NULL, [relatorio_id] BIGINT NOT NULL, CONSTRAINT [PK_relatorioparametro] PRIMARY KEY ([id]))

/* CreateForeignKey FK_relatorioparametro_relatorio relatorioparametro(relatorio_id) relatorio(id) */
ALTER TABLE [dbo].[relatorioparametro] ADD CONSTRAINT [FK_relatorioparametro_relatorio] FOREIGN KEY ([relatorio_id]) REFERENCES [dbo].[relatorio] ([id])

/* AlterColumn arquivo nome String */
ALTER TABLE [dbo].[arquivo] ALTER COLUMN [nome] NVARCHAR(260) NOT NULL

/* CreateTable setorproducao */
CREATE TABLE [dbo].[setorproducao] ([id] BIGINT NOT NULL, [nome] NVARCHAR(50) NOT NULL, [ativo] BIT NOT NULL, [departamentoproducao_id] BIGINT NOT NULL, CONSTRAINT [PK_setorproducao] PRIMARY KEY ([id]))

/* CreateForeignKey FK_setorproducao_departamentoproducao setorproducao(departamentoproducao_id) departamentoproducao(id) */
ALTER TABLE [dbo].[setorproducao] ADD CONSTRAINT [FK_setorproducao_departamentoproducao] FOREIGN KEY ([departamentoproducao_id]) REFERENCES [dbo].[departamentoproducao] ([id])

/* CreateTable operacaoproducao */
CREATE TABLE [dbo].[operacaoproducao] ([id] BIGINT NOT NULL, [descricao] NVARCHAR(50) NOT NULL, [tempo] DOUBLE PRECISION NOT NULL, [custo] DOUBLE PRECISION NOT NULL, [ativo] BIT NOT NULL, [setorproducao_id] BIGINT NOT NULL, CONSTRAINT [PK_operacaoproducao] PRIMARY KEY ([id]))

/* CreateForeignKey FK_operacaoproducao_setorproducao operacaoproducao(setorproducao_id) setorproducao(id) */
ALTER TABLE [dbo].[operacaoproducao] ADD CONSTRAINT [FK_operacaoproducao_setorproducao] FOREIGN KEY ([setorproducao_id]) REFERENCES [dbo].[setorproducao] ([id])

/* CreateTable tamanho */
CREATE TABLE [dbo].[tamanho] ([id] BIGINT NOT NULL, [descricao] NVARCHAR(50) NOT NULL, [sigla] NVARCHAR(10) NOT NULL, [ativo] BIT NOT NULL, CONSTRAINT [PK_tamanho] PRIMARY KEY ([id]))

/* ExecuteEmbeddedSqlScript Fashion.ERP.Migrator.Scripts._201308271102.permissao.sql */
--SET @CADASTROSID = (SELECT id FROM permissao WHERE area = 'Comum' AND controller IS NULL AND [action] IS NULL AND [permissaopai_id] IS NULL);
--SET @FINANCEIROID = (SELECT id FROM permissao WHERE area = 'Financeiro' AND controller IS NULL AND [action] IS NULL AND [permissaopai_id] IS NULL);
--SET @ALMOXARIFADOID = (SELECT id FROM permissao WHERE area = 'Almoxarifado' AND controller IS NULL AND [action] IS NULL AND [permissaopai_id] IS NULL);
--SET @ENGENHARIAPRODUTOID = (SELECT id FROM permissao WHERE area = 'EngenhariaProduto' AND controller IS NULL AND [action] IS NULL AND [permissaopai_id] IS NULL);

ALTER TABLE [dbo].[permissaotoperfildeacesso] DROP CONSTRAINT [FK_permissaotoperfildeacesso_permissao]
ALTER TABLE [dbo].[permissaotoperfildeacesso] DROP CONSTRAINT [FK_permissaotoperfildeacesso_perfildeacesso]
DROP TABLE [dbo].[permissaotoperfildeacesso]

ALTER TABLE [dbo].[permissaotousuario] DROP CONSTRAINT [FK_permissaotousuario_usuario]
ALTER TABLE [dbo].[permissaotousuario] DROP CONSTRAINT [FK_permissaotousuario_permissao]
DROP TABLE [dbo].[permissaotousuario]

DROP TABLE [dbo].[permissao];

CREATE TABLE [dbo].[permissao](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[descricao] [nvarchar](50) NOT NULL,
	[action] [nvarchar](50) NULL,
	[area] [nvarchar](50) NULL,
	[controller] [nvarchar](50) NULL,
	[exibenomenu] [bit] NOT NULL,
	[requerpermissao] [bit] NOT NULL,
	[permissaopai_id] [bigint] NULL,
 CONSTRAINT [PK_permissao] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY];

ALTER TABLE [dbo].[permissao]  WITH NOCHECK ADD  CONSTRAINT [FK_permissao_permissao] FOREIGN KEY([permissaopai_id]) REFERENCES [dbo].[permissao] ([id]);

ALTER TABLE [dbo].[permissao] CHECK CONSTRAINT [FK_permissao_permissao];

-- permissaotoperfildeacesso
CREATE TABLE [dbo].[permissaotoperfildeacesso](
	[perfildeacesso_id] [bigint] NOT NULL,
	[permissao_id] [bigint] NOT NULL
) ON [PRIMARY]
ALTER TABLE [dbo].[permissaotoperfildeacesso]  WITH NOCHECK ADD  CONSTRAINT [FK_permissaotoperfildeacesso_perfildeacesso] FOREIGN KEY([perfildeacesso_id]) REFERENCES [dbo].[perfildeacesso] ([id])
ALTER TABLE [dbo].[permissaotoperfildeacesso] CHECK CONSTRAINT [FK_permissaotoperfildeacesso_perfildeacesso]
ALTER TABLE [dbo].[permissaotoperfildeacesso]  WITH NOCHECK ADD  CONSTRAINT [FK_permissaotoperfildeacesso_permissao] FOREIGN KEY([permissao_id]) REFERENCES [dbo].[permissao] ([id])
ALTER TABLE [dbo].[permissaotoperfildeacesso] CHECK CONSTRAINT [FK_permissaotoperfildeacesso_permissao]

-- permissaotousuario
CREATE TABLE [dbo].[permissaotousuario](
	[usuario_id] [bigint] NOT NULL,
	[permissao_id] [bigint] NOT NULL
) ON [PRIMARY]
ALTER TABLE [dbo].[permissaotousuario]  WITH NOCHECK ADD  CONSTRAINT [FK_permissaotousuario_permissao] FOREIGN KEY([permissao_id]) REFERENCES [dbo].[permissao] ([id])
ALTER TABLE [dbo].[permissaotousuario] CHECK CONSTRAINT [FK_permissaotousuario_permissao]
ALTER TABLE [dbo].[permissaotousuario]  WITH NOCHECK ADD  CONSTRAINT [FK_permissaotousuario_usuario] FOREIGN KEY([usuario_id]) REFERENCES [dbo].[usuario] ([id])
ALTER TABLE [dbo].[permissaotousuario] CHECK CONSTRAINT [FK_permissaotousuario_usuario]
-----------------------------------------------------------------------------------

DECLARE @CADASTROSID AS BIGINT, @FINANCEIROID AS BIGINT, @ALMOXARIFADOID AS BIGINT, @ENGENHARIAPRODUTO AS BIGINT, @ID AS BIGINT
-- Comum
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id) VALUES (NULL, 'Comum', NULL, 'Cadastros', 1, 0, NULL);
SET @CADASTROSID = SCOPE_IDENTITY()

-- Usuario
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id) VALUES ('Index', 'Comum', 'Usuario', 'Usuário', 1, 1, @CADASTROSID);
SET @ID = SCOPE_IDENTITY()
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id) VALUES ('Novo', 'Comum', 'Usuario', 'Novo', 0, 1, @ID);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id) VALUES ('Editar', 'Comum', 'Usuario', 'Editar', 0, 1, @ID);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id) VALUES ('Excluir', 'Comum', 'Usuario', 'Excluir', 0, 1, @ID);

-- AreaInteresse
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id) VALUES ('Index', 'Comum', 'AreaInteresse', 'Área de interesse', 1, 1, @CADASTROSID);
SET @ID = SCOPE_IDENTITY()
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id) VALUES ('Novo', 'Comum', 'AreaInteresse', 'Novo', 0, 1, @ID);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id) VALUES ('Editar', 'Comum', 'AreaInteresse', 'Editar', 0, 1, @ID);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id) VALUES ('Excluir', 'Comum', 'AreaInteresse', 'Excluir', 0, 1, @ID);

-- Cliente
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id) VALUES ('Index', 'Comum', 'Cliente', 'Cliente', 1, 1, @CADASTROSID);
SET @ID = SCOPE_IDENTITY()
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id) VALUES ('Novo', 'Comum', 'Cliente', 'Novo', 0, 1, @ID);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id) VALUES ('Editar', 'Comum', 'Cliente', 'Editar', 0, 1, @ID);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id) VALUES ('Excluir', 'Comum', 'Cliente', 'Excluir', 0, 1, @ID);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id) VALUES ('PesquisarId', 'Comum', 'Cliente', 'PesquisarId', 0, 1, @ID);

-- Fornecedor
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id) VALUES ('Index', 'Comum', 'Fornecedor', 'Fornecedor', 1, 1, @CADASTROSID);
SET @ID = SCOPE_IDENTITY()
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id) VALUES ('Novo', 'Comum', 'Fornecedor', 'Novo', 0, 1, @ID);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id) VALUES ('Editar', 'Comum', 'Fornecedor', 'Editar', 0, 1, @ID);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id) VALUES ('Excluir', 'Comum', 'Fornecedor', 'Excluir', 0, 1, @ID);

-- Funcionario
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id) VALUES ('Index', 'Comum', 'Funcionario', 'Funcionário', 1, 1, @CADASTROSID);
SET @ID = SCOPE_IDENTITY()
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id) VALUES ('Novo', 'Comum', 'Funcionario', 'Novo', 0, 1, @ID);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id) VALUES ('Editar', 'Comum', 'Funcionario', 'Editar', 0, 1, @ID);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id) VALUES ('Excluir', 'Comum', 'Funcionario', 'Excluir', 0, 1, @ID);

-- GrauDependencia
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id) VALUES ('Index', 'Comum', 'GrauDependencia', 'Grau de dependência', 1, 1, @CADASTROSID);
SET @ID = SCOPE_IDENTITY()
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id) VALUES ('Novo', 'Comum', 'GrauDependencia', 'Novo', 0, 1, @ID);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id) VALUES ('Editar', 'Comum', 'GrauDependencia', 'Editar', 0, 1, @ID);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id) VALUES ('Excluir', 'Comum', 'GrauDependencia', 'Excluir', 0, 1, @ID);

-- MeioPagamento
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id) VALUES ('Index', 'Comum', 'MeioPagamento', 'Meio de pagamento', 1, 1, @CADASTROSID);
SET @ID = SCOPE_IDENTITY()
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id) VALUES ('Novo', 'Comum', 'MeioPagamento', 'Novo', 0, 1, @ID);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id) VALUES ('Editar', 'Comum', 'MeioPagamento', 'Editar', 0, 1, @ID);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id) VALUES ('Excluir', 'Comum', 'MeioPagamento', 'Excluir', 0, 1, @ID);

-- PerfilDeAcesso
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id) VALUES ('Index', 'Comum', 'PerfilDeAcesso', 'Perfil de acesso', 1, 1, @CADASTROSID);
SET @ID = SCOPE_IDENTITY()
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id) VALUES ('Novo', 'Comum', 'PerfilDeAcesso', 'Novo', 0, 1, @ID);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id) VALUES ('Editar', 'Comum', 'PerfilDeAcesso', 'Editar', 0, 1, @ID);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id) VALUES ('Excluir', 'Comum', 'PerfilDeAcesso', 'Excluir', 0, 1, @ID);

-- PrestadorServico
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id) VALUES ('Index', 'Comum', 'PrestadorServico', 'Prestador de servico', 1, 1, @CADASTROSID);
SET @ID = SCOPE_IDENTITY()
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id) VALUES ('Novo', 'Comum', 'PrestadorServico', 'Novo', 0, 1, @ID);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id) VALUES ('Editar', 'Comum', 'PrestadorServico', 'Editar', 0, 1, @ID);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id) VALUES ('Excluir', 'Comum', 'PrestadorServico', 'Excluir', 0, 1, @ID);

-- Profissao
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id) VALUES ('Index', 'Comum', 'Profissao', 'Profissão', 1, 1, @CADASTROSID);
SET @ID = SCOPE_IDENTITY()
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id) VALUES ('Novo', 'Comum', 'Profissao', 'Novo', 0, 1, @ID);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id) VALUES ('Editar', 'Comum', 'Profissao', 'Editar', 0, 1, @ID);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id) VALUES ('Excluir', 'Comum', 'Profissao', 'Excluir', 0, 1, @ID);

-- TipoFornecedor
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id) VALUES ('Index', 'Comum', 'TipoFornecedor', 'Tipo de fornecedor', 1, 1, @CADASTROSID);
SET @ID = SCOPE_IDENTITY()
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id) VALUES ('Novo', 'Comum', 'TipoFornecedor', 'Novo', 0, 1, @ID);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id) VALUES ('Editar', 'Comum', 'TipoFornecedor', 'Editar', 0, 1, @ID);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id) VALUES ('Excluir', 'Comum', 'TipoFornecedor', 'Excluir', 0, 1, @ID);

-- Unidade
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id) VALUES ('Index', 'Comum', 'Unidade', 'Unidade', 1, 1, @CADASTROSID);
SET @ID = SCOPE_IDENTITY()
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id) VALUES ('Novo', 'Comum', 'Unidade', 'Novo', 0, 1, @ID);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id) VALUES ('Editar', 'Comum', 'Unidade', 'Editar', 0, 1, @ID);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id) VALUES ('Excluir', 'Comum', 'Unidade', 'Excluir', 0, 1, @ID);

-- Financeiro
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id) VALUES (NULL, 'Financeiro', NULL, 'Financeiro', 1, 0, NULL);
SET @FINANCEIROID = SCOPE_IDENTITY()

-- Cheque (Financeiro)
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id) VALUES (NULL, 'Financeiro', 'Cheque', 'Cheque', 1, 0, @FINANCEIROID);
SET @ID = SCOPE_IDENTITY()

-- ChequeRecebido
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id) VALUES ('Index', 'Financeiro', 'ChequeRecebido', 'Cheque recebido', 1, 1, @ID);
SET @ID = SCOPE_IDENTITY()
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id) VALUES ('Novo', 'Financeiro', 'ChequeRecebido', 'Novo', 0, 1, @ID);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id) VALUES ('Editar', 'Financeiro', 'ChequeRecebido', 'Editar', 0, 1, @ID);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id) VALUES ('Excluir', 'Financeiro', 'ChequeRecebido', 'Excluir', 0, 1, @ID);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id) VALUES ('Baixa', 'Financeiro', 'ChequeRecebido', 'Baixa', 0, 1, @ID);
SET @ID = SCOPE_IDENTITY()
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id) VALUES ('ExcluirBaixa', 'Financeiro', 'ChequeRecebido', 'Excluir baixa', 0, 1, @ID);

-- Relatórios (Comum)
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id) VALUES (NULL, 'Comum', 'Relatorio', 'Relatório', 1, 0, @CADASTROSID);
SET @ID = SCOPE_IDENTITY()
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id) VALUES ('FichaCliente', 'Comum', 'Relatorio', 'Ficha de cliente', 1, 1, @ID);
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id) VALUES ('ListaCliente', 'Comum', 'Relatorio', 'Lista de clientes', 1, 1, @ID);
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id) VALUES ('FichaFornecedor', 'Comum', 'Relatorio', 'Ficha de fornecedor', 1, 1, @ID);
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id) VALUES ('ListaFornecedor', 'Comum', 'Relatorio', 'Lista de fornecedores', 1, 1, @ID);
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id) VALUES ('FichaFuncionario', 'Comum', 'Relatorio', 'Ficha de funcionário', 1, 1, @ID);
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id) VALUES ('ListaFuncionario', 'Comum', 'Relatorio', 'Lista de funcionários', 1, 1, @ID);
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id) VALUES ('FichaPrestadorServico', 'Comum', 'Relatorio', 'Ficha de prestador de serviço', 1, 1, @ID);
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id) VALUES ('ListaPrestadorServico', 'Comum', 'Relatorio', 'Lista de prestadores de serviço', 1, 1, @ID);

-- Bancário (Financeiro)
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id) VALUES (NULL, 'Financeiro', 'Bancario', 'Bancário', 1, 0, @FINANCEIROID);
SET @FINANCEIROID = SCOPE_IDENTITY()

-- ContaBancaria
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id) VALUES ('Index', 'Financeiro', 'ContaBancaria', 'Conta bancária', 1, 1, @FINANCEIROID);
SET @ID = SCOPE_IDENTITY()
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id) VALUES ('Novo', 'Financeiro', 'ContaBancaria', 'Novo', 0, 1, @ID);
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id) VALUES ('Editar', 'Financeiro', 'ContaBancaria', 'Editar', 0, 1, @ID);
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id) VALUES ('Excluir', 'Financeiro', 'ContaBancaria', 'Excluir', 0, 1, @ID);

-- ExtratoBancario
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id) VALUES ('Index', 'Financeiro', 'ExtratoBancario', 'Extrato bancário', 1, 1, @FINANCEIROID);
SET @ID = SCOPE_IDENTITY()
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id) VALUES ('Novo', 'Financeiro', 'ExtratoBancario', 'Novo', 0, 1, @ID);
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id) VALUES ('Editar', 'Financeiro', 'ExtratoBancario', 'Editar', 0, 1, @ID);
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id) VALUES ('Excluir', 'Financeiro', 'ExtratoBancario', 'Excluir', 0, 1, @ID);

-- Relatórios (Financeiro)
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id) VALUES (NULL, 'Financeiro', 'Relatorio', 'Relatório', 1, 0, @FINANCEIROID);
SET @ID = SCOPE_IDENTITY()
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id) VALUES ('ListaChequeRecebido', 'Financeiro', 'Relatorio', 'Lista de cheques recebido', 1, 1, @ID);

-- Almoxarifado
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id) VALUES (NULL, 'Almoxarifado', NULL, 'Almoxarifado', 1, 0, NULL);
SET @ALMOXARIFADOID = SCOPE_IDENTITY()

-- CatalogoMaterial
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id) VALUES ('Index', 'Almoxarifado', 'CatalogoMaterial', 'Catálogo de material', 1, 1, @ALMOXARIFADOID);
SET @ID = SCOPE_IDENTITY()
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id) VALUES ('Editar', 'Almoxarifado', 'CatalogoMaterial', 'Editar', 0, 1, @ID);
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id) VALUES ('Excluir', 'Almoxarifado', 'CatalogoMaterial', 'Excluir', 0, 1, @ID);
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id) VALUES ('Novo', 'Almoxarifado', 'CatalogoMaterial', 'Novo', 0, 1, @ID);

-- Categoria
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id) VALUES ('Index', 'Almoxarifado', 'Categoria', 'Categoria', 1, 1, @ALMOXARIFADOID);
SET @ID = SCOPE_IDENTITY()
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id) VALUES ('Editar', 'Almoxarifado', 'Categoria', 'Editar', 0, 1, @ID);
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id) VALUES ('Excluir', 'Almoxarifado', 'Categoria', 'Excluir', 0, 1, @ID);
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id) VALUES ('Novo', 'Almoxarifado', 'Categoria', 'Novo', 0, 1, @ID);

-- Familia
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id) VALUES ('Index', 'Almoxarifado', 'Familia', 'Família', 1, 1, @ALMOXARIFADOID);
SET @ID = SCOPE_IDENTITY()
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id) VALUES ('Editar', 'Almoxarifado', 'Familia', 'Editar', 0, 1, @ID);
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id) VALUES ('Excluir', 'Almoxarifado', 'Familia', 'Excluir', 0, 1, @ID);
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id) VALUES ('Novo', 'Almoxarifado', 'Familia', 'Novo', 0, 1, @ID);

-- Marca
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id) VALUES ('Index', 'Comum', 'Marca', 'Marca', 1, 1, @CADASTROSID);
SET @ID = SCOPE_IDENTITY()
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id) VALUES ('Editar', 'Comum', 'Marca', 'Editar', 0, 1, @ID);
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id) VALUES ('Excluir', 'Comum', 'Marca', 'Excluir', 0, 1, @ID);
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id) VALUES ('Novo', 'Comum', 'Marca', 'Novo', 0, 1, @ID);

-- Subcategoria
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id) VALUES ('Index', 'Almoxarifado', 'Subcategoria', 'Subcategoria', 1, 1, @ALMOXARIFADOID);
SET @ID = SCOPE_IDENTITY()
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id) VALUES ('Editar', 'Almoxarifado', 'Subcategoria', 'Editar', 0, 1, @ID);
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id) VALUES ('Excluir', 'Almoxarifado', 'Subcategoria', 'Excluir', 0, 1, @ID);
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id) VALUES ('Novo', 'Almoxarifado', 'Subcategoria', 'Novo', 0, 1, @ID);

-- UnidadeMedida
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id) VALUES ('Index', 'Almoxarifado', 'UnidadeMedida', 'Unidade de medida', 1, 1, @ALMOXARIFADOID);
SET @ID = SCOPE_IDENTITY()
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id) VALUES ('Editar', 'Almoxarifado', 'UnidadeMedida', 'Editar', 0, 1, @ID);
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id) VALUES ('Excluir', 'Almoxarifado', 'UnidadeMedida', 'Excluir', 0, 1, @ID);
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id) VALUES ('Novo', 'Almoxarifado', 'UnidadeMedida', 'Novo', 0, 1, @ID);

-- Menu criação
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id) VALUES (NULL, 'EngenhariaProduto', NULL, 'Engenharia de produto', 1, 0, NULL);
SET @ENGENHARIAPRODUTO = SCOPE_IDENTITY()

-- Artigo
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id) VALUES ('Index', 'EngenhariaProduto', 'Artigo', 'Artigo', 1, 1, @ENGENHARIAPRODUTO);
SET @ID = SCOPE_IDENTITY()
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id) VALUES ('Novo', 'EngenhariaProduto', 'Artigo', 'Novo', 0, 1, @ID);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id) VALUES ('Editar', 'EngenhariaProduto', 'Artigo', 'Editar', 0, 1, @ID);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id) VALUES ('Excluir', 'EngenhariaProduto', 'Artigo', 'Excluir', 0, 1, @ID);

-- Classificacao
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id) VALUES ('Index', 'EngenhariaProduto', 'Classificacao', 'Classificação', 1, 1, @ENGENHARIAPRODUTO);
SET @ID = SCOPE_IDENTITY()
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id) VALUES ('Novo', 'EngenhariaProduto', 'Classificacao', 'Novo', 0, 1, @ID);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id) VALUES ('Editar', 'EngenhariaProduto', 'Classificacao', 'Editar', 0, 1, @ID);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id) VALUES ('Excluir', 'EngenhariaProduto', 'Classificacao', 'Excluir', 0, 1, @ID);

-- Coleção
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id) VALUES ('Index', 'Comum', 'Colecao', 'Coleção', 1, 1, @ENGENHARIAPRODUTO);
SET @ID = SCOPE_IDENTITY()
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id) VALUES ('Novo', 'Comum', 'Colecao', 'Novo', 0, 1, @ID);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id) VALUES ('Editar', 'Comum', 'Colecao', 'Editar', 0, 1, @ID);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id) VALUES ('Excluir', 'Comum', 'Colecao', 'Excluir', 0, 1, @ID);

-- Comprimento
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id) VALUES ('Index', 'EngenhariaProduto', 'Comprimento', 'Comprimento', 1, 1, @ENGENHARIAPRODUTO);
SET @ID = SCOPE_IDENTITY()
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id) VALUES ('Novo', 'EngenhariaProduto', 'Comprimento', 'Novo', 0, 1, @ID);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id) VALUES ('Editar', 'EngenhariaProduto', 'Comprimento', 'Editar', 0, 1, @ID);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id) VALUES ('Excluir', 'EngenhariaProduto', 'Comprimento', 'Excluir', 0, 1, @ID);

-- Natureza
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id) VALUES ('Index', 'EngenhariaProduto', 'Natureza', 'Natureza do produto', 1, 1, @ENGENHARIAPRODUTO);
SET @ID = SCOPE_IDENTITY()
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id) VALUES ('Novo', 'EngenhariaProduto', 'Natureza', 'Novo', 0, 1, @ID);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id) VALUES ('Editar', 'EngenhariaProduto', 'Natureza', 'Editar', 0, 1, @ID);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id) VALUES ('Excluir', 'EngenhariaProduto', 'Natureza', 'Excluir', 0, 1, @ID);

-- Barra
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id) VALUES ('Index', 'EngenhariaProduto', 'Barra', 'Tipo de barra', 1, 1, @ENGENHARIAPRODUTO);
SET @ID = SCOPE_IDENTITY()
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id) VALUES ('Novo', 'EngenhariaProduto', 'Barra', 'Novo', 0, 1, @ID);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id) VALUES ('Editar', 'EngenhariaProduto', 'Barra', 'Editar', 0, 1, @ID);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id) VALUES ('Excluir', 'EngenhariaProduto', 'Barra', 'Excluir', 0, 1, @ID);

-- ProdutoBase
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id) VALUES ('Index', 'EngenhariaProduto', 'ProdutoBase', 'Produto base', 1, 1, @ENGENHARIAPRODUTO);
SET @ID = SCOPE_IDENTITY()
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id) VALUES ('Novo', 'EngenhariaProduto', 'ProdutoBase', 'Novo', 0, 1, @ID);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id) VALUES ('Editar', 'EngenhariaProduto', 'ProdutoBase', 'Editar', 0, 1, @ID);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id) VALUES ('Excluir', 'EngenhariaProduto', 'ProdutoBase', 'Excluir', 0, 1, @ID);

-- Departamento produtivo
insert into permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id) values ('Index', 'Comum', 'DepartamentoProducao', 'Departamento produtivo', 1, 1, @CADASTROSID);
SET @ID = SCOPE_IDENTITY()
insert into permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id) values ('Novo', 'Comum', 'DepartamentoProducao', 'Novo', 0, 1, @ID);
insert into permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id) values ('Editar', 'Comum', 'DepartamentoProducao', 'Editar', 0, 1, @ID);
insert into permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id) values ('Excluir', 'Comum', 'DepartamentoProducao', 'Excluir', 0, 1, @ID);

-- Operação do setor
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id) VALUES ('Index', 'EngenhariaProduto', 'OperacaoProducao', 'Operação do setor', 1, 1, @ENGENHARIAPRODUTO);
SET @ID = SCOPE_IDENTITY()
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id) VALUES ('Novo', 'EngenhariaProduto', 'OperacaoProducao', 'Novo', 0, 1, @ID);
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id) VALUES ('Editar', 'EngenhariaProduto', 'OperacaoProducao', 'Editar', 0, 1, @ID);
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id) VALUES ('Inativar', 'EngenhariaProduto', 'OperacaoProducao', 'Inativar', 0, 1, @ID);

-- Setor do departamento produtivo
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id) VALUES ('Index', 'EngenhariaProduto', 'SetorProducao', 'Setor do departamento produtivo', 1, 1, @ENGENHARIAPRODUTO);
SET @ID = SCOPE_IDENTITY()
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id) VALUES ('Novo', 'EngenhariaProduto', 'SetorProducao', 'Novo', 0, 1, @ID);
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id) VALUES ('Editar', 'EngenhariaProduto', 'SetorProducao', 'Editar', 0, 1, @ID);
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id) VALUES ('Inativar', 'EngenhariaProduto', 'SetorProducao', 'Inativar', 0, 1, @ID);

-- Tamanho
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id) VALUES ('Index', 'Comum', 'Tamanho', 'Tamanho', 1, 1, @CADASTROSID);
SET @ID = SCOPE_IDENTITY()
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id) VALUES ('Novo', 'Comum', 'Tamanho', 'Novo', 0, 1, @ID);
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id) VALUES ('Editar', 'Comum', 'Tamanho', 'Editar', 0, 1, @ID);
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id) VALUES ('Inativar', 'Comum', 'Tamanho', 'Inativar', 0, 1, @ID);

-- INSERIR PERMISSÕES BÁSICAS DO ADMINSTRADOR
INSERT INTO permissaotousuario VALUES (1,1);
INSERT INTO permissaotousuario VALUES (1,2);
INSERT INTO permissaotousuario VALUES (1,3);
INSERT INTO permissaotousuario VALUES (1,4);
INSERT INTO permissaotousuario VALUES (1,5);

INSERT INTO [dbo].[VersionInfo] ([Version], [AppliedOn], [Description]) VALUES (201308271102, '2015-02-04T16:38:11', 'Migration201308271102')
/* Committing Transaction */
/* 201308271102: Migration201308271102 migrated */

/* 201309021551: Migration201309021551 migrating ============================= */

/* Beginning Transaction */
/* CreateTable grade */
CREATE TABLE [dbo].[grade] ([id] BIGINT NOT NULL, [descricao] NVARCHAR(50) NOT NULL, [datacriacao] DATETIME NOT NULL, [ativo] BIT NOT NULL, CONSTRAINT [PK_grade] PRIMARY KEY ([id]))

/* CreateTable gradetamanho */
CREATE TABLE [dbo].[gradetamanho] ([ordem] INT NOT NULL, [tamanho_id] BIGINT NOT NULL, [grade_id] BIGINT NOT NULL)

/* CreateForeignKey FK_gradetamanho_tamanho gradetamanho(tamanho_id) tamanho(id) */
ALTER TABLE [dbo].[gradetamanho] ADD CONSTRAINT [FK_gradetamanho_tamanho] FOREIGN KEY ([tamanho_id]) REFERENCES [dbo].[tamanho] ([id])

/* CreateForeignKey FK_gradetamanho_grade gradetamanho(grade_id) grade(id) */
ALTER TABLE [dbo].[gradetamanho] ADD CONSTRAINT [FK_gradetamanho_grade] FOREIGN KEY ([grade_id]) REFERENCES [dbo].[grade] ([id])

/* CreateTable segmento */
CREATE TABLE [dbo].[segmento] ([id] BIGINT NOT NULL, [descricao] NVARCHAR(50) NOT NULL, [ativo] BIT NOT NULL, CONSTRAINT [PK_segmento] PRIMARY KEY ([id]))

/* CreateTable linhatravete */
CREATE TABLE [dbo].[linhatravete] ([id] BIGINT NOT NULL, [cor] NVARCHAR(10) NOT NULL, [nomelinha] NVARCHAR(50) NOT NULL, CONSTRAINT [PK_linhatravete] PRIMARY KEY ([id]))

/* CreateTable linhabordado */
CREATE TABLE [dbo].[linhabordado] ([id] BIGINT NOT NULL, [cor] NVARCHAR(10) NOT NULL, [nomelinha] NVARCHAR(50) NOT NULL, CONSTRAINT [PK_linhabordado] PRIMARY KEY ([id]))

/* CreateTable linhapesponto */
CREATE TABLE [dbo].[linhapesponto] ([id] BIGINT NOT NULL, [cor] NVARCHAR(10) NOT NULL, [nomelinha] NVARCHAR(50) NOT NULL, CONSTRAINT [PK_linhapesponto] PRIMARY KEY ([id]))

/* CreateTable modelo */
CREATE TABLE [dbo].[modelo] ([id] BIGINT NOT NULL, [referencia] NVARCHAR(20) NOT NULL, [descricao] NVARCHAR(100) NOT NULL, [tecido] NVARCHAR(50), [detalhamento] NVARCHAR(4000) NOT NULL, [datacriacao] DATETIME NOT NULL, [aprovado] BIT, [dataaprovacao] DATETIME, [observacao] NVARCHAR(4000), [cos] DOUBLE PRECISION, [passante] DOUBLE PRECISION, [entrepernas] DOUBLE PRECISION, [localizacao] NVARCHAR(100), [tamanhopadrao] NVARCHAR(50) NOT NULL, [linhacasa] NVARCHAR(50) NOT NULL, [lavada] NVARCHAR(4000) NOT NULL, [grade_id] BIGINT NOT NULL, [colecao_id] BIGINT NOT NULL, [classificacao_id] BIGINT NOT NULL, [segmento_id] BIGINT NOT NULL, [natureza_id] BIGINT NOT NULL, [barra_id] BIGINT NOT NULL, [comprimento_id] BIGINT NOT NULL, [marca_id] BIGINT NOT NULL, [produtobase_id] BIGINT NOT NULL, [artigo_id] BIGINT NOT NULL, [estilista_id] BIGINT NOT NULL, [modelista_id] BIGINT, CONSTRAINT [PK_modelo] PRIMARY KEY ([id]))

/* CreateForeignKey FK_modelo_grade modelo(grade_id) grade(id) */
ALTER TABLE [dbo].[modelo] ADD CONSTRAINT [FK_modelo_grade] FOREIGN KEY ([grade_id]) REFERENCES [dbo].[grade] ([id])

/* CreateForeignKey FK_modelo_colecao modelo(colecao_id) colecao(id) */
ALTER TABLE [dbo].[modelo] ADD CONSTRAINT [FK_modelo_colecao] FOREIGN KEY ([colecao_id]) REFERENCES [dbo].[colecao] ([id])

/* CreateForeignKey FK_modelo_classificacao modelo(classificacao_id) classificacao(id) */
ALTER TABLE [dbo].[modelo] ADD CONSTRAINT [FK_modelo_classificacao] FOREIGN KEY ([classificacao_id]) REFERENCES [dbo].[classificacao] ([id])

/* CreateForeignKey FK_modelo_segmento modelo(segmento_id) segmento(id) */
ALTER TABLE [dbo].[modelo] ADD CONSTRAINT [FK_modelo_segmento] FOREIGN KEY ([segmento_id]) REFERENCES [dbo].[segmento] ([id])

/* CreateForeignKey FK_modelo_natureza modelo(natureza_id) natureza(id) */
ALTER TABLE [dbo].[modelo] ADD CONSTRAINT [FK_modelo_natureza] FOREIGN KEY ([natureza_id]) REFERENCES [dbo].[natureza] ([id])

/* CreateForeignKey FK_modelo_barra modelo(barra_id) barra(id) */
ALTER TABLE [dbo].[modelo] ADD CONSTRAINT [FK_modelo_barra] FOREIGN KEY ([barra_id]) REFERENCES [dbo].[barra] ([id])

/* CreateForeignKey FK_modelo_comprimento modelo(comprimento_id) comprimento(id) */
ALTER TABLE [dbo].[modelo] ADD CONSTRAINT [FK_modelo_comprimento] FOREIGN KEY ([comprimento_id]) REFERENCES [dbo].[comprimento] ([id])

/* CreateForeignKey FK_modelo_marca modelo(marca_id) marca(id) */
ALTER TABLE [dbo].[modelo] ADD CONSTRAINT [FK_modelo_marca] FOREIGN KEY ([marca_id]) REFERENCES [dbo].[marca] ([id])

/* CreateForeignKey FK_modelo_produtobase modelo(produtobase_id) produtobase(id) */
ALTER TABLE [dbo].[modelo] ADD CONSTRAINT [FK_modelo_produtobase] FOREIGN KEY ([produtobase_id]) REFERENCES [dbo].[produtobase] ([id])

/* CreateForeignKey FK_modelo_artigo modelo(artigo_id) artigo(id) */
ALTER TABLE [dbo].[modelo] ADD CONSTRAINT [FK_modelo_artigo] FOREIGN KEY ([artigo_id]) REFERENCES [dbo].[artigo] ([id])

/* CreateForeignKey FK_modelo_estilista modelo(estilista_id) pessoa(id) */
ALTER TABLE [dbo].[modelo] ADD CONSTRAINT [FK_modelo_estilista] FOREIGN KEY ([estilista_id]) REFERENCES [dbo].[pessoa] ([id])

/* CreateForeignKey FK_modelo_modelista modelo(modelista_id) pessoa(id) */
ALTER TABLE [dbo].[modelo] ADD CONSTRAINT [FK_modelo_modelista] FOREIGN KEY ([modelista_id]) REFERENCES [dbo].[pessoa] ([id])

/* CreateTable modelolinhatravete */
CREATE TABLE [dbo].[modelolinhatravete] ([linhatravete_id] BIGINT NOT NULL, [modelo_id] BIGINT NOT NULL)

/* CreateForeignKey FK_modelolinhatravete_linhatravete modelolinhatravete(linhatravete_id) linhatravete(id) */
ALTER TABLE [dbo].[modelolinhatravete] ADD CONSTRAINT [FK_modelolinhatravete_linhatravete] FOREIGN KEY ([linhatravete_id]) REFERENCES [dbo].[linhatravete] ([id])

/* CreateForeignKey FK_modelolinhatravete_modelo modelolinhatravete(modelo_id) modelo(id) */
ALTER TABLE [dbo].[modelolinhatravete] ADD CONSTRAINT [FK_modelolinhatravete_modelo] FOREIGN KEY ([modelo_id]) REFERENCES [dbo].[modelo] ([id])

/* CreateTable modelolinhabordado */
CREATE TABLE [dbo].[modelolinhabordado] ([linhabordado_id] BIGINT NOT NULL, [modelo_id] BIGINT NOT NULL)

/* CreateForeignKey FK_modelolinhabordado_linhabordado modelolinhabordado(linhabordado_id) linhabordado(id) */
ALTER TABLE [dbo].[modelolinhabordado] ADD CONSTRAINT [FK_modelolinhabordado_linhabordado] FOREIGN KEY ([linhabordado_id]) REFERENCES [dbo].[linhabordado] ([id])

/* CreateForeignKey FK_modelolinhabordado_modelo modelolinhabordado(modelo_id) modelo(id) */
ALTER TABLE [dbo].[modelolinhabordado] ADD CONSTRAINT [FK_modelolinhabordado_modelo] FOREIGN KEY ([modelo_id]) REFERENCES [dbo].[modelo] ([id])

/* CreateTable modelolinhapesponto */
CREATE TABLE [dbo].[modelolinhapesponto] ([linhapesponto_id] BIGINT NOT NULL, [modelo_id] BIGINT NOT NULL)

/* CreateForeignKey FK_modelolinhapesponto_linhapesponto modelolinhapesponto(linhapesponto_id) linhapesponto(id) */
ALTER TABLE [dbo].[modelolinhapesponto] ADD CONSTRAINT [FK_modelolinhapesponto_linhapesponto] FOREIGN KEY ([linhapesponto_id]) REFERENCES [dbo].[linhapesponto] ([id])

/* CreateForeignKey FK_modelolinhapesponto_modelo modelolinhapesponto(modelo_id) modelo(id) */
ALTER TABLE [dbo].[modelolinhapesponto] ADD CONSTRAINT [FK_modelolinhapesponto_modelo] FOREIGN KEY ([modelo_id]) REFERENCES [dbo].[modelo] ([id])

/* CreateTable modelofoto */
CREATE TABLE [dbo].[modelofoto] ([id] BIGINT NOT NULL, [impressao] BIT NOT NULL, [padrao] BIT NOT NULL, [foto_id] BIGINT NOT NULL, [modelo_id] BIGINT NOT NULL, CONSTRAINT [PK_modelofoto] PRIMARY KEY ([id]))

/* CreateForeignKey FK_modelofoto_arquivo modelofoto(foto_id) arquivo(id) */
ALTER TABLE [dbo].[modelofoto] ADD CONSTRAINT [FK_modelofoto_arquivo] FOREIGN KEY ([foto_id]) REFERENCES [dbo].[arquivo] ([id])

/* CreateForeignKey FK_modelofoto_modelo modelofoto(modelo_id) modelo(id) */
ALTER TABLE [dbo].[modelofoto] ADD CONSTRAINT [FK_modelofoto_modelo] FOREIGN KEY ([modelo_id]) REFERENCES [dbo].[modelo] ([id])

/* AlterTable marca */
/* No SQL statement executed. */

/* CreateColumn marca ativo Boolean */
ALTER TABLE [dbo].[marca] ADD [ativo] BIT NOT NULL CONSTRAINT [DF_marca_ativo] DEFAULT 1

/* ExecuteEmbeddedSqlScript Fashion.ERP.Migrator.Scripts._201309021551.permissao.sql */
DECLARE @ENGENHARIAPRODUTOID AS BIGINT, @ID AS BIGINT
SET @ENGENHARIAPRODUTOID = (SELECT id FROM permissao WHERE area = 'EngenhariaProduto' AND controller IS NULL AND [action] IS NULL AND [permissaopai_id] IS NULL);

-- Grade
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id) VALUES ('Index', 'EngenhariaProduto', 'Grade', 'Grade', 1, 1, @ENGENHARIAPRODUTOID);
SET @ID = SCOPE_IDENTITY()
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id) VALUES ('Novo', 'EngenhariaProduto', 'Grade', 'Novo', 0, 1, @ID);
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id) VALUES ('Editar', 'EngenhariaProduto', 'Grade', 'Editar', 0, 1, @ID);
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id) VALUES ('Inativar', 'EngenhariaProduto', 'Grade', 'Inativar', 0, 1, @ID);

-- Segmento
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id) VALUES ('Index', 'EngenhariaProduto', 'Segmento', 'Segmento', 1, 1, @ENGENHARIAPRODUTOID);
SET @ID = SCOPE_IDENTITY()
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id) VALUES ('Novo', 'EngenhariaProduto', 'Segmento', 'Novo', 0, 1, @ID);
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id) VALUES ('Editar', 'EngenhariaProduto', 'Segmento', 'Editar', 0, 1, @ID);
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id) VALUES ('Inativar', 'EngenhariaProduto', 'Segmento', 'Inativar', 0, 1, @ID);

-- LinhaBordado
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id) VALUES ('Index', 'EngenhariaProduto', 'LinhaBordado', 'Linha bordado', 1, 1, @ENGENHARIAPRODUTOID);
SET @ID = SCOPE_IDENTITY()
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id) VALUES ('Novo', 'EngenhariaProduto', 'LinhaBordado', 'Novo', 0, 1, @ID);
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id) VALUES ('Editar', 'EngenhariaProduto', 'LinhaBordado', 'Editar', 0, 1, @ID);
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id) VALUES ('Excluir', 'EngenhariaProduto', 'LinhaBordado', 'Excluir', 0, 1, @ID);

-- Linhapesponto
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id) VALUES ('Index', 'EngenhariaProduto', 'Linhapesponto', 'Linha pesponto', 1, 1, @ENGENHARIAPRODUTOID);
SET @ID = SCOPE_IDENTITY()
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id) VALUES ('Novo', 'EngenhariaProduto', 'Linhapesponto', 'Novo', 0, 1, @ID);
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id) VALUES ('Editar', 'EngenhariaProduto', 'Linhapesponto', 'Editar', 0, 1, @ID);
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id) VALUES ('Excluir', 'EngenhariaProduto', 'Linhapesponto', 'Excluir', 0, 1, @ID);

-- LinhaTravete
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id) VALUES ('Index', 'EngenhariaProduto', 'LinhaTravete', 'Linha travete', 1, 1, @ENGENHARIAPRODUTOID);
SET @ID = SCOPE_IDENTITY()
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id) VALUES ('Novo', 'EngenhariaProduto', 'LinhaTravete', 'Novo', 0, 1, @ID);
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id) VALUES ('Editar', 'EngenhariaProduto', 'LinhaTravete', 'Editar', 0, 1, @ID);
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id) VALUES ('Excluir', 'EngenhariaProduto', 'LinhaTravete', 'Excluir', 0, 1, @ID);

-- Modelo
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id) VALUES ('Index', 'EngenhariaProduto', 'Modelo', 'Criação do croqui', 1, 1, @ENGENHARIAPRODUTOID);
SET @ID = SCOPE_IDENTITY()
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id) VALUES ('Novo', 'EngenhariaProduto', 'Modelo', 'Novo', 0, 1, @ID);
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id) VALUES ('Editar', 'EngenhariaProduto', 'Modelo', 'Editar', 0, 1, @ID);
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id) VALUES ('Excluir', 'EngenhariaProduto', 'Modelo', 'Excluir', 0, 1, @ID);

-- Editar situação
UPDATE permissao SET descricao = 'Editar situação', action = 'EditarSituacao' WHERE action = 'Excluir' AND area = 'EngenhariaProduto' AND controller = 'Artigo';
UPDATE permissao SET descricao = 'Editar situação', action = 'EditarSituacao' WHERE action = 'Excluir' AND area = 'EngenhariaProduto' AND controller = 'Natureza';
UPDATE permissao SET descricao = 'Editar situação', action = 'EditarSituacao' WHERE action = 'Excluir' AND area = 'EngenhariaProduto' AND controller = 'Barra';
UPDATE permissao SET descricao = 'Editar situação', action = 'EditarSituacao' WHERE action = 'Excluir' AND area = 'EngenhariaProduto' AND controller = 'Comprimento';
UPDATE permissao SET descricao = 'Editar situação', action = 'EditarSituacao' WHERE action = 'Excluir' AND area = 'Almoxarifado' AND controller = 'Marca';
UPDATE permissao SET descricao = 'Editar situação', action = 'EditarSituacao' WHERE action = 'Excluir' AND area = 'EngenhariaProduto' AND controller = 'ProdutoBase';
UPDATE permissao SET descricao = 'Editar situação', action = 'EditarSituacao' WHERE action = 'Inativar' AND area = 'EngenhariaProduto' AND controller = 'Grade';
UPDATE permissao SET descricao = 'Editar situação', action = 'EditarSituacao' WHERE action = 'Excluir' AND area = 'Comum' AND controller = 'Colecao';
UPDATE permissao SET descricao = 'Editar situação', action = 'EditarSituacao' WHERE action = 'Excluir' AND area = 'EngenhariaProduto' AND controller = 'Classificacao';
UPDATE permissao SET descricao = 'Editar situação', action = 'EditarSituacao' WHERE action = 'Inativar' AND area = 'EngenhariaProduto' AND controller = 'Segmento';
UPDATE permissao SET descricao = 'Editar situação', action = 'EditarSituacao' WHERE action = 'Inativar' AND area = 'EngenhariaProduto' AND controller = 'SetorProducao';
UPDATE permissao SET descricao = 'Editar situação', action = 'EditarSituacao' WHERE action = 'Inativar' AND area = 'EngenhariaProduto' AND controller = 'OperacaoProducao';
UPDATE permissao SET descricao = 'Editar situação', action = 'EditarSituacao' WHERE action = 'Excluir' AND area = 'Comum' AND controller = 'DepartamentoProducao';

-- Muda cadastro de marcas e coleção para o menu Cadastros
DECLARE @CADASTROSID AS BIGINT;
SET @CADASTROSID = (SELECT id FROM permissao WHERE area = 'Comum' AND controller IS NULL AND [action] IS NULL AND [permissaopai_id] IS NULL);
UPDATE permissao SET area = 'Comum' WHERE area = 'Almoxarifado' AND controller = 'Marca';
UPDATE permissao SET permissaopai_id = @CADASTROSID WHERE action = 'Index' AND area = 'Comum' AND controller = 'Marca';
UPDATE permissao SET permissaopai_id = @CADASTROSID WHERE action = 'Index' AND area = 'Comum' AND controller = 'Colecao';

INSERT INTO [dbo].[VersionInfo] ([Version], [AppliedOn], [Description]) VALUES (201309021551, '2015-02-04T16:38:11', 'Migration201309021551')
/* Committing Transaction */
/* 201309021551: Migration201309021551 migrated */

/* 201309241033: Migration201309241033 migrating ============================= */

/* Beginning Transaction */
/* CreateTable marcamaterial */
CREATE TABLE [dbo].[marcamaterial] ([id] BIGINT NOT NULL, [nome] NVARCHAR(100) NOT NULL, [ativo] BIT NOT NULL, CONSTRAINT [PK_marcamaterial] PRIMARY KEY ([id]))

/* ExecuteSqlStatement INSERT INTO marcamaterial (id, nome, ativo) SELECT id, nome, ativo FROM marca; */
INSERT INTO marcamaterial (id, nome, ativo) SELECT id, nome, ativo FROM marca;

/* AlterTable catalogomaterial */
/* No SQL statement executed. */

/* CreateColumn catalogomaterial marcamaterial_id Int64 */
ALTER TABLE [dbo].[catalogomaterial] ADD [marcamaterial_id] BIGINT

/* ExecuteSqlStatement UPDATE catalogomaterial SET marcamaterial_id = marca_id; */
UPDATE catalogomaterial SET marcamaterial_id = marca_id;

/* AlterTable catalogomaterial */
/* No SQL statement executed. */

/* AlterColumn catalogomaterial marcamaterial_id Int64 */
ALTER TABLE [dbo].[catalogomaterial] ALTER COLUMN [marcamaterial_id] BIGINT NOT NULL

/* CreateForeignKey FK_catalogomaterial_marcamaterial catalogomaterial(marcamaterial_id) marcamaterial(id) */
ALTER TABLE [dbo].[catalogomaterial] ADD CONSTRAINT [FK_catalogomaterial_marcamaterial] FOREIGN KEY ([marcamaterial_id]) REFERENCES [dbo].[marcamaterial] ([id])

/* DeleteForeignKey FK_catalogomaterial_marca catalogomaterial ()  () */
ALTER TABLE [dbo].[catalogomaterial] DROP CONSTRAINT [FK_catalogomaterial_marca]

/* DeleteColumn catalogomaterial marca_id */
DECLARE @default sysname, @sql nvarchar(max);

-- get name of default constraint
SELECT @default = name
FROM sys.default_constraints
WHERE parent_object_id = object_id('[dbo].[catalogomaterial]')
AND type = 'D'
AND parent_column_id = (
SELECT column_id
FROM sys.columns
WHERE object_id = object_id('[dbo].[catalogomaterial]')
AND name = 'marca_id'
);

-- create alter table command to drop constraint as string and run it
SET @sql = N'ALTER TABLE [dbo].[catalogomaterial] DROP CONSTRAINT ' + @default;
EXEC sp_executesql @sql;

-- now we can finally drop column
ALTER TABLE [dbo].[catalogomaterial] DROP COLUMN [marca_id];


/* ExecuteSqlStatement INSERT INTO uniquekeys (tablename, nexthi) VALUES ('marcamaterial', (SELECT ISNULL(MAX(id), 0) + 1 FROM marcamaterial)); */
INSERT INTO uniquekeys (tablename, nexthi) VALUES ('marcamaterial', (SELECT ISNULL(MAX(id), 0) + 1 FROM marcamaterial));

/* AlterTable modelolinhatravete */
/* No SQL statement executed. */

/* CreateColumn modelolinhatravete nome String */
ALTER TABLE [dbo].[modelolinhatravete] ADD [nome] NVARCHAR(50)

/* ExecuteSqlStatement UPDATE modelolinhatravete SET nome = (SELECT lb.nomelinha FROM linhatravete lb WHERE id = linhatravete_id); */
UPDATE modelolinhatravete SET nome = (SELECT lb.nomelinha FROM linhatravete lb WHERE id = linhatravete_id);

/* DeleteForeignKey FK_modelolinhatravete_linhatravete modelolinhatravete ()  () */
ALTER TABLE [dbo].[modelolinhatravete] DROP CONSTRAINT [FK_modelolinhatravete_linhatravete]

/* DeleteColumn modelolinhatravete linhatravete_id */
DECLARE @default sysname, @sql nvarchar(max);

-- get name of default constraint
SELECT @default = name
FROM sys.default_constraints
WHERE parent_object_id = object_id('[dbo].[modelolinhatravete]')
AND type = 'D'
AND parent_column_id = (
SELECT column_id
FROM sys.columns
WHERE object_id = object_id('[dbo].[modelolinhatravete]')
AND name = 'linhatravete_id'
);

-- create alter table command to drop constraint as string and run it
SET @sql = N'ALTER TABLE [dbo].[modelolinhatravete] DROP CONSTRAINT ' + @default;
EXEC sp_executesql @sql;

-- now we can finally drop column
ALTER TABLE [dbo].[modelolinhatravete] DROP COLUMN [linhatravete_id];


/* AlterTable modelolinhabordado */
/* No SQL statement executed. */

/* CreateColumn modelolinhabordado nome String */
ALTER TABLE [dbo].[modelolinhabordado] ADD [nome] NVARCHAR(50)

/* ExecuteSqlStatement UPDATE modelolinhabordado SET nome = (SELECT lb.nomelinha FROM linhabordado lb WHERE id = linhabordado_id); */
UPDATE modelolinhabordado SET nome = (SELECT lb.nomelinha FROM linhabordado lb WHERE id = linhabordado_id);

/* DeleteForeignKey FK_modelolinhabordado_linhabordado modelolinhabordado ()  () */
ALTER TABLE [dbo].[modelolinhabordado] DROP CONSTRAINT [FK_modelolinhabordado_linhabordado]

/* DeleteColumn modelolinhabordado linhabordado_id */
DECLARE @default sysname, @sql nvarchar(max);

-- get name of default constraint
SELECT @default = name
FROM sys.default_constraints
WHERE parent_object_id = object_id('[dbo].[modelolinhabordado]')
AND type = 'D'
AND parent_column_id = (
SELECT column_id
FROM sys.columns
WHERE object_id = object_id('[dbo].[modelolinhabordado]')
AND name = 'linhabordado_id'
);

-- create alter table command to drop constraint as string and run it
SET @sql = N'ALTER TABLE [dbo].[modelolinhabordado] DROP CONSTRAINT ' + @default;
EXEC sp_executesql @sql;

-- now we can finally drop column
ALTER TABLE [dbo].[modelolinhabordado] DROP COLUMN [linhabordado_id];


/* AlterTable modelolinhapesponto */
/* No SQL statement executed. */

/* CreateColumn modelolinhapesponto nome String */
ALTER TABLE [dbo].[modelolinhapesponto] ADD [nome] NVARCHAR(50)

/* ExecuteSqlStatement UPDATE modelolinhapesponto SET nome = (SELECT lb.nomelinha FROM linhapesponto lb WHERE id = linhapesponto_id); */
UPDATE modelolinhapesponto SET nome = (SELECT lb.nomelinha FROM linhapesponto lb WHERE id = linhapesponto_id);

/* DeleteForeignKey FK_modelolinhapesponto_linhapesponto modelolinhapesponto ()  () */
ALTER TABLE [dbo].[modelolinhapesponto] DROP CONSTRAINT [FK_modelolinhapesponto_linhapesponto]

/* DeleteColumn modelolinhapesponto linhapesponto_id */
DECLARE @default sysname, @sql nvarchar(max);

-- get name of default constraint
SELECT @default = name
FROM sys.default_constraints
WHERE parent_object_id = object_id('[dbo].[modelolinhapesponto]')
AND type = 'D'
AND parent_column_id = (
SELECT column_id
FROM sys.columns
WHERE object_id = object_id('[dbo].[modelolinhapesponto]')
AND name = 'linhapesponto_id'
);

-- create alter table command to drop constraint as string and run it
SET @sql = N'ALTER TABLE [dbo].[modelolinhapesponto] DROP CONSTRAINT ' + @default;
EXEC sp_executesql @sql;

-- now we can finally drop column
ALTER TABLE [dbo].[modelolinhapesponto] DROP COLUMN [linhapesponto_id];


/* DeleteTable linhapesponto */
DROP TABLE [dbo].[linhapesponto]

/* DeleteTable linhabordado */
DROP TABLE [dbo].[linhabordado]

/* DeleteTable linhatravete */
DROP TABLE [dbo].[linhatravete]

/* AlterTable categoria */
/* No SQL statement executed. */

/* CreateColumn categoria codigoncm String */
ALTER TABLE [dbo].[categoria] ADD [codigoncm] NVARCHAR(8)

/* CreateColumn categoria generocategoria String */
ALTER TABLE [dbo].[categoria] ADD [generocategoria] NVARCHAR(255)

/* CreateColumn categoria tipocategoria String */
ALTER TABLE [dbo].[categoria] ADD [tipocategoria] NVARCHAR(255)

/* CreateColumn categoria ativo Boolean */
ALTER TABLE [dbo].[categoria] ADD [ativo] BIT

/* ExecuteSqlStatement update categoria set codigoncm = '00000000', generocategoria = 'Tecido', tipocategoria = 'UsoConsumo', ativo = 1; */
update categoria set codigoncm = '00000000', generocategoria = 'Tecido', tipocategoria = 'UsoConsumo', ativo = 1;

/* AlterTable categoria */
/* No SQL statement executed. */

/* AlterColumn categoria nome String */
ALTER TABLE [dbo].[categoria] ALTER COLUMN [nome] NVARCHAR(60) NOT NULL

/* AlterColumn categoria codigoncm String */
ALTER TABLE [dbo].[categoria] ALTER COLUMN [codigoncm] NVARCHAR(8) NOT NULL

/* AlterColumn categoria generocategoria String */
ALTER TABLE [dbo].[categoria] ALTER COLUMN [generocategoria] NVARCHAR(255) NOT NULL

/* AlterColumn categoria tipocategoria String */
ALTER TABLE [dbo].[categoria] ALTER COLUMN [tipocategoria] NVARCHAR(255) NOT NULL

/* AlterColumn categoria ativo Boolean */
ALTER TABLE [dbo].[categoria] ALTER COLUMN [ativo] BIT NOT NULL

/* AlterTable subcategoria */
/* No SQL statement executed. */

/* CreateColumn subcategoria ativo Boolean */
ALTER TABLE [dbo].[subcategoria] ADD [ativo] BIT

/* ExecuteSqlStatement update subcategoria set ativo = 1; */
update subcategoria set ativo = 1;

/* AlterTable subcategoria */
/* No SQL statement executed. */

/* AlterColumn subcategoria ativo Boolean */
ALTER TABLE [dbo].[subcategoria] ALTER COLUMN [ativo] BIT NOT NULL

/* CreateTable bordado */
CREATE TABLE [dbo].[bordado] ([id] BIGINT NOT NULL, [descricao] NVARCHAR(100), [pontos] NVARCHAR(100), [aplicacao] NVARCHAR(100), [observacao] NVARCHAR(100), CONSTRAINT [PK_bordado] PRIMARY KEY ([id]))

/* CreateTable tecido */
CREATE TABLE [dbo].[tecido] ([id] BIGINT NOT NULL, [composicao] NVARCHAR(200), [armacao] NVARCHAR(100), [gramatura] NVARCHAR(100), [largura] NVARCHAR(100), [rendimento] NVARCHAR(100), CONSTRAINT [PK_tecido] PRIMARY KEY ([id]))

/* AlterTable catalogomaterial */
/* No SQL statement executed. */

/* CreateColumn catalogomaterial bordado_id Int64 */
ALTER TABLE [dbo].[catalogomaterial] ADD [bordado_id] BIGINT

/* CreateForeignKey FK_catalogomaterial_bordado catalogomaterial(bordado_id) bordado(id) */
ALTER TABLE [dbo].[catalogomaterial] ADD CONSTRAINT [FK_catalogomaterial_bordado] FOREIGN KEY ([bordado_id]) REFERENCES [dbo].[bordado] ([id])

/* CreateColumn catalogomaterial tecido_id Int64 */
ALTER TABLE [dbo].[catalogomaterial] ADD [tecido_id] BIGINT

/* CreateForeignKey FK_catalogomaterial_tecido catalogomaterial(tecido_id) tecido(id) */
ALTER TABLE [dbo].[catalogomaterial] ADD CONSTRAINT [FK_catalogomaterial_tecido] FOREIGN KEY ([tecido_id]) REFERENCES [dbo].[tecido] ([id])

/* CreateTable origemsituacaotributaria */
CREATE TABLE [dbo].[origemsituacaotributaria] ([id] BIGINT NOT NULL IDENTITY(1,1), [codigo] NVARCHAR(5) NOT NULL, [descricao] NVARCHAR(200) NOT NULL, CONSTRAINT [PK_origemsituacaotributaria] PRIMARY KEY ([id]))

INSERT INTO [dbo].[origemsituacaotributaria] ([codigo], [descricao]) VALUES ('0', 'Nacional, exceto as indicadas nos códigos 3 a 5'); INSERT INTO [dbo].[origemsituacaotributaria] ([codigo], [descricao]) VALUES ('1', 'Estrangeira - Importação direta, exceto a indicada no código 6'); INSERT INTO [dbo].[origemsituacaotributaria] ([codigo], [descricao]) VALUES ('2', 'Estrangeira - Adquirida no mercado interno, exceto a indicada no código 7'); INSERT INTO [dbo].[origemsituacaotributaria] ([codigo], [descricao]) VALUES ('3', 'Nacional, mercadoria ou bem com Conteúdo de Importação superior a 40% (quarenta por cento)'); INSERT INTO [dbo].[origemsituacaotributaria] ([codigo], [descricao]) VALUES ('4', 'Nacional, cuja produção tenha sido feita em conformidade com os processos produtivos básicos de que tratam o Decreto-Lei nº 288/67 e as Leis n°s 8.248/91, 8.387/91,10.176/01 e 11.484/07'); INSERT INTO [dbo].[origemsituacaotributaria] ([codigo], [descricao]) VALUES ('5', 'Nacional, mercadoria ou bem com Conteúdo de Importação inferior ou igual a 40% (quarenta por cento)'); INSERT INTO [dbo].[origemsituacaotributaria] ([codigo], [descricao]) VALUES ('6', 'Estrangeira - Importação direta, sem similar nacional, constante em lista de Resolução CAMEX'); INSERT INTO [dbo].[origemsituacaotributaria] ([codigo], [descricao]) VALUES ('7', 'Estrangeira - Adquirida no mercado interno, sem similar nacional, constante em lista de Resolução CAMEX')
/* AlterTable catalogomaterial */
/* No SQL statement executed. */

/* CreateColumn catalogomaterial origemsituacaotributaria_id Int64 */
ALTER TABLE [dbo].[catalogomaterial] ADD [origemsituacaotributaria_id] BIGINT

/* ExecuteSqlStatement UPDATE catalogomaterial SET origemsituacaotributaria_id = 1; */
UPDATE catalogomaterial SET origemsituacaotributaria_id = 1;

/* AlterTable catalogomaterial */
/* No SQL statement executed. */

/* AlterColumn catalogomaterial origemsituacaotributaria_id Int64 */
ALTER TABLE [dbo].[catalogomaterial] ALTER COLUMN [origemsituacaotributaria_id] BIGINT NOT NULL

/* CreateForeignKey FK_catalogomaterial_origemsituacaotributaria catalogomaterial(origemsituacaotributaria_id) origemsituacaotributaria(id) */
ALTER TABLE [dbo].[catalogomaterial] ADD CONSTRAINT [FK_catalogomaterial_origemsituacaotributaria] FOREIGN KEY ([origemsituacaotributaria_id]) REFERENCES [dbo].[origemsituacaotributaria] ([id])

/* DeleteColumn catalogomaterial origem */
DECLARE @default sysname, @sql nvarchar(max);

-- get name of default constraint
SELECT @default = name
FROM sys.default_constraints
WHERE parent_object_id = object_id('[dbo].[catalogomaterial]')
AND type = 'D'
AND parent_column_id = (
SELECT column_id
FROM sys.columns
WHERE object_id = object_id('[dbo].[catalogomaterial]')
AND name = 'origem'
);

-- create alter table command to drop constraint as string and run it
SET @sql = N'ALTER TABLE [dbo].[catalogomaterial] DROP CONSTRAINT ' + @default;
EXEC sp_executesql @sql;

-- now we can finally drop column
ALTER TABLE [dbo].[catalogomaterial] DROP COLUMN [origem];


/* AlterTable referenciaexterna */
/* No SQL statement executed. */

/* AlterColumn referenciaexterna descricao String */
ALTER TABLE [dbo].[referenciaexterna] ALTER COLUMN [descricao] NVARCHAR(100)

/* AlterColumn referenciaexterna codigobarra String */
ALTER TABLE [dbo].[referenciaexterna] ALTER COLUMN [codigobarra] NVARCHAR(128)

/* AlterColumn referenciaexterna preco Double */
ALTER TABLE [dbo].[referenciaexterna] ALTER COLUMN [preco] DOUBLE PRECISION

/* AlterTable catalogomaterial */
/* No SQL statement executed. */

/* AlterColumn catalogomaterial detalhamento String */
ALTER TABLE [dbo].[catalogomaterial] ALTER COLUMN [detalhamento] NVARCHAR(200)

/* AlterColumn catalogomaterial codigobarra String */
ALTER TABLE [dbo].[catalogomaterial] ALTER COLUMN [codigobarra] NVARCHAR(128)

/* AlterTable familia */
/* No SQL statement executed. */

/* AlterColumn familia nome String */
ALTER TABLE [dbo].[familia] ALTER COLUMN [nome] NVARCHAR(60) NOT NULL

/* AlterTable marcamaterial */
/* No SQL statement executed. */

/* AlterColumn marcamaterial nome String */
ALTER TABLE [dbo].[marcamaterial] ALTER COLUMN [nome] NVARCHAR(60) NOT NULL

/* AlterTable subcategoria */
/* No SQL statement executed. */

/* AlterColumn subcategoria nome String */
ALTER TABLE [dbo].[subcategoria] ALTER COLUMN [nome] NVARCHAR(60) NOT NULL

/* AlterTable unidademedida */
/* No SQL statement executed. */

/* AlterColumn unidademedida descricao String */
ALTER TABLE [dbo].[unidademedida] ALTER COLUMN [descricao] NVARCHAR(60) NOT NULL

/* AlterColumn unidademedida sigla String */
ALTER TABLE [dbo].[unidademedida] ALTER COLUMN [sigla] NVARCHAR(10) NOT NULL

/* AlterTable familia */
/* No SQL statement executed. */

/* CreateColumn familia ativo Boolean */
ALTER TABLE [dbo].[familia] ADD [ativo] BIT

/* ExecuteSqlStatement UPDATE familia SET ativo = 1; */
UPDATE familia SET ativo = 1;

/* AlterTable familia */
/* No SQL statement executed. */

/* AlterColumn familia ativo Boolean */
ALTER TABLE [dbo].[familia] ALTER COLUMN [ativo] BIT NOT NULL

/* AlterTable unidademedida */
/* No SQL statement executed. */

/* CreateColumn unidademedida ativo Boolean */
ALTER TABLE [dbo].[unidademedida] ADD [ativo] BIT

/* ExecuteSqlStatement UPDATE unidademedida SET ativo = 1; */
UPDATE unidademedida SET ativo = 1;

/* AlterTable unidademedida */
/* No SQL statement executed. */

/* AlterColumn unidademedida ativo Boolean */
ALTER TABLE [dbo].[unidademedida] ALTER COLUMN [ativo] BIT NOT NULL

/* AlterTable funcionario */
/* No SQL statement executed. */

/* CreateColumn funcionario funcaofuncionario String */
ALTER TABLE [dbo].[funcionario] ADD [funcaofuncionario] NVARCHAR(255)

/* ExecuteSqlStatement UPDATE funcionario SET funcaofuncionario = tipofuncionario; */
UPDATE funcionario SET funcaofuncionario = tipofuncionario;

/* DeleteColumn funcionario tipofuncionario */
DECLARE @default sysname, @sql nvarchar(max);

-- get name of default constraint
SELECT @default = name
FROM sys.default_constraints
WHERE parent_object_id = object_id('[dbo].[funcionario]')
AND type = 'D'
AND parent_column_id = (
SELECT column_id
FROM sys.columns
WHERE object_id = object_id('[dbo].[funcionario]')
AND name = 'tipofuncionario'
);

-- create alter table command to drop constraint as string and run it
SET @sql = N'ALTER TABLE [dbo].[funcionario] DROP CONSTRAINT ' + @default;
EXEC sp_executesql @sql;

-- now we can finally drop column
ALTER TABLE [dbo].[funcionario] DROP COLUMN [tipofuncionario];


/* AlterTable funcionario */
/* No SQL statement executed. */

/* AlterColumn funcionario funcaofuncionario String */
ALTER TABLE [dbo].[funcionario] ALTER COLUMN [funcaofuncionario] NVARCHAR(255) NOT NULL

/* ExecuteEmbeddedSqlScript Fashion.ERP.Migrator.Scripts._201309241033.permissao.sql */
DECLARE @ALMOXARIFADOID AS BIGINT, @ID AS BIGINT;
SET @ALMOXARIFADOID = (SELECT id FROM permissao WHERE area = 'Almoxarifado' AND controller IS NULL AND [action] IS NULL AND [permissaopai_id] IS NULL);

-- MarcaMaterial
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id) VALUES ('Index', 'Almoxarifado', 'MarcaMaterial', 'Marca do catálogo', 1, 1, @ALMOXARIFADOID);
SET @ID = SCOPE_IDENTITY()
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id) VALUES ('Editar', 'Almoxarifado', 'MarcaMaterial', 'Editar', 0, 1, @ID);
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id) VALUES ('Excluir', 'Almoxarifado', 'MarcaMaterial', 'Excluir', 0, 1, @ID);
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id) VALUES ('Novo', 'Almoxarifado', 'MarcaMaterial', 'Novo', 0, 1, @ID);
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id) VALUES ('EditarSituacao', 'Almoxarifado', 'MarcaMaterial', 'Editar situação', 0, 1, @ID);

SET @ID = (SELECT Id FROM permissao WHERE action = 'Index' AND area = 'EngenhariaProduto' AND controller = 'Artigo');
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id) VALUES ('Excluir', 'EngenhariaProduto', 'Artigo', 'Excluir', 0, 1, @ID);

SET @ID = (SELECT Id FROM permissao WHERE action = 'Index' AND area = 'EngenhariaProduto' AND controller = 'Natureza');
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id) VALUES ('Excluir', 'EngenhariaProduto', 'Natureza', 'Excluir', 0, 1, @ID);

SET @ID = (SELECT Id FROM permissao WHERE action = 'Index' AND area = 'EngenhariaProduto' AND controller = 'Barra');
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id) VALUES ('Excluir', 'EngenhariaProduto', 'Barra', 'Excluir', 0, 1, @ID);

SET @ID = (SELECT Id FROM permissao WHERE action = 'Index' AND area = 'EngenhariaProduto' AND controller = 'Comprimento');
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id) VALUES ('Excluir', 'EngenhariaProduto', 'Comprimento', 'Excluir', 0, 1, @ID);

SET @ID = (SELECT Id FROM permissao WHERE action = 'Index' AND area = 'EngenhariaProduto' AND controller = 'ProdutoBase');
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id) VALUES ('Excluir', 'EngenhariaProduto', 'ProdutoBase', 'Excluir', 0, 1, @ID);

SET @ID = (SELECT Id FROM permissao WHERE action = 'Index' AND area = 'EngenhariaProduto' AND controller = 'Grade');
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id) VALUES ('Excluir', 'EngenhariaProduto', 'Grade', 'Excluir', 0, 1, @ID);

SET @ID = (SELECT Id FROM permissao WHERE action = 'Index' AND area = 'EngenhariaProduto' AND controller = 'Classificacao');
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id) VALUES ('Excluir', 'EngenhariaProduto', 'Classificacao', 'Excluir', 0, 1, @ID);

SET @ID = (SELECT Id FROM permissao WHERE action = 'Index' AND area = 'EngenhariaProduto' AND controller = 'Segmento');
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id) VALUES ('Excluir', 'EngenhariaProduto', 'Segmento', 'Excluir', 0, 1, @ID);

SET @ID = (SELECT Id FROM permissao WHERE action = 'Index' AND area = 'EngenhariaProduto' AND controller = 'SetorProducao');
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id) VALUES ('Excluir', 'EngenhariaProduto', 'SetorProducao', 'Excluir', 0, 1, @ID);

SET @ID = (SELECT Id FROM permissao WHERE action = 'Index' AND area = 'EngenhariaProduto' AND controller = 'OperacaoProducao');
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id) VALUES ('Excluir', 'EngenhariaProduto', 'OperacaoProducao', 'Excluir', 0, 1, @ID);

SET @ID = (SELECT Id FROM permissao WHERE action = 'Index' AND area = 'Comum' AND controller = 'Marca');
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id) VALUES ('Excluir', 'Comum', 'Marca', 'Excluir', 0, 1, @ID);

SET @ID = (SELECT Id FROM permissao WHERE action = 'Index' AND area = 'Comum' AND controller = 'Colecao');
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id) VALUES ('Excluir', 'Comum', 'Colecao', 'Excluir', 0, 1, @ID);

SET @ID = (SELECT Id FROM permissao WHERE action = 'Index' AND area = 'Comum' AND controller = 'DepartamentoProducao');
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id) VALUES ('Excluir', 'Comum', 'DepartamentoProducao', 'Excluir', 0, 1, @ID);

-- Exclui as permissões para os cadastros de LinhaBordado, Linhapesponto e LinhaTravete
DELETE FROM permissaotoperfildeacesso 
	WHERE permissao_id in (SELECT Id FROM permissao WHERE area = 'EngenhariaProduto' AND controller = 'LinhaBordado')
	OR permissao_id in (SELECT Id FROM permissao WHERE area = 'EngenhariaProduto' AND controller = 'Linhapesponto')
	OR permissao_id in (SELECT Id FROM permissao WHERE area = 'EngenhariaProduto' AND controller = 'LinhaTravete');

DELETE FROM permissaotousuario 
	WHERE permissao_id in (SELECT Id FROM permissao WHERE area = 'EngenhariaProduto' AND controller = 'LinhaBordado')
	OR permissao_id in (SELECT Id FROM permissao WHERE area = 'EngenhariaProduto' AND controller = 'Linhapesponto')
	OR permissao_id in (SELECT Id FROM permissao WHERE area = 'EngenhariaProduto' AND controller = 'LinhaTravete');

DELETE FROM permissao WHERE area = 'EngenhariaProduto' AND controller = 'LinhaBordado';
DELETE FROM permissao WHERE area = 'EngenhariaProduto' AND controller = 'Linhapesponto';
DELETE FROM permissao WHERE area = 'EngenhariaProduto' AND controller = 'LinhaTravete';

-- Adicionar as permissões de 'editar situação' ao módulo de almoxarifaso
SET @ID = (SELECT Id FROM permissao WHERE action = 'Index' AND area = 'Almoxarifado' AND controller = 'CatalogoMaterial');
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id) VALUES ('EditarSituacao', 'Almoxarifado', 'CatalogoMaterial', 'Editar situação', 0, 1, @ID);

SET @ID = (SELECT Id FROM permissao WHERE action = 'Index' AND area = 'Almoxarifado' AND controller = 'Categoria');
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id) VALUES ('EditarSituacao', 'Almoxarifado', 'Categoria', 'Editar situação', 0, 1, @ID);

SET @ID = (SELECT Id FROM permissao WHERE action = 'Index' AND area = 'Almoxarifado' AND controller = 'Subcategoria');
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id) VALUES ('EditarSituacao', 'Almoxarifado', 'Subcategoria', 'Editar situação', 0, 1, @ID);

SET @ID = (SELECT Id FROM permissao WHERE action = 'Index' AND area = 'Almoxarifado' AND controller = 'Familia');
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id) VALUES ('EditarSituacao', 'Almoxarifado', 'Familia', 'Editar situação', 0, 1, @ID);

SET @ID = (SELECT Id FROM permissao WHERE action = 'Index' AND area = 'Almoxarifado' AND controller = 'UnidadeMedida');
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id) VALUES ('EditarSituacao', 'Almoxarifado', 'UnidadeMedida', 'Editar situação', 0, 1, @ID);

/* -> 1 Insert operations completed in 00:00:00.0020015 taking an average of 00:00:00.0020015 */
INSERT INTO [dbo].[VersionInfo] ([Version], [AppliedOn], [Description]) VALUES (201309241033, '2015-02-04T16:38:12', 'Migration201309241033')
/* Committing Transaction */
/* 201309241033: Migration201309241033 migrated */

/* 201310161017: Migration201310161017 migrating ============================= */

/* Beginning Transaction */
/* CreateTable cor */
CREATE TABLE [dbo].[cor] ([id] BIGINT NOT NULL, [nome] NVARCHAR(100) NOT NULL, [ativo] BIT NOT NULL, CONSTRAINT [PK_cor] PRIMARY KEY ([id]))

/* CreateTable variacao */
CREATE TABLE [dbo].[variacao] ([id] BIGINT NOT NULL, [nome] NVARCHAR(100) NOT NULL, [modelo_id] BIGINT NOT NULL, CONSTRAINT [PK_variacao] PRIMARY KEY ([id]))

/* CreateForeignKey FK_variacao_modelo variacao(modelo_id) modelo(id) */
ALTER TABLE [dbo].[variacao] ADD CONSTRAINT [FK_variacao_modelo] FOREIGN KEY ([modelo_id]) REFERENCES [dbo].[modelo] ([id])

/* CreateTable variacaocor */
CREATE TABLE [dbo].[variacaocor] ([variacao_id] BIGINT NOT NULL, [cor_id] BIGINT NOT NULL)

/* CreateForeignKey FK_variacaocor_variacao variacaocor(variacao_id) variacao(id) */
ALTER TABLE [dbo].[variacaocor] ADD CONSTRAINT [FK_variacaocor_variacao] FOREIGN KEY ([variacao_id]) REFERENCES [dbo].[variacao] ([id])

/* CreateForeignKey FK_variacaocor_cor variacaocor(cor_id) cor(id) */
ALTER TABLE [dbo].[variacaocor] ADD CONSTRAINT [FK_variacaocor_cor] FOREIGN KEY ([cor_id]) REFERENCES [dbo].[cor] ([id])

/* CreateTable sequenciaprodutiva */
CREATE TABLE [dbo].[sequenciaprodutiva] ([id] BIGINT NOT NULL, [ordem] INT NOT NULL, [dataentrada] DATETIME, [datasaida] DATETIME, [departamentoproducao_id] BIGINT NOT NULL, [modelo_id] BIGINT NOT NULL, CONSTRAINT [PK_sequenciaprodutiva] PRIMARY KEY ([id]))

/* CreateForeignKey FK_sequenciaprodutiva_departamentoproducao sequenciaprodutiva(departamentoproducao_id) departamentoproducao(id) */
ALTER TABLE [dbo].[sequenciaprodutiva] ADD CONSTRAINT [FK_sequenciaprodutiva_departamentoproducao] FOREIGN KEY ([departamentoproducao_id]) REFERENCES [dbo].[departamentoproducao] ([id])

/* CreateForeignKey FK_sequenciaprodutiva_modelo sequenciaprodutiva(modelo_id) modelo(id) */
ALTER TABLE [dbo].[sequenciaprodutiva] ADD CONSTRAINT [FK_sequenciaprodutiva_modelo] FOREIGN KEY ([modelo_id]) REFERENCES [dbo].[modelo] ([id])

/* CreateTable sequenciaprodutivacatalogomaterial */
CREATE TABLE [dbo].[sequenciaprodutivacatalogomaterial] ([sequenciaprodutiva_id] BIGINT NOT NULL, [catalogomaterial_id] BIGINT NOT NULL)

/* CreateForeignKey FK_sequenciaprodutivacatalogomaterial_sequenciaprodutiva sequenciaprodutivacatalogomaterial(sequenciaprodutiva_id) sequenciaprodutiva(id) */
ALTER TABLE [dbo].[sequenciaprodutivacatalogomaterial] ADD CONSTRAINT [FK_sequenciaprodutivacatalogomaterial_sequenciaprodutiva] FOREIGN KEY ([sequenciaprodutiva_id]) REFERENCES [dbo].[sequenciaprodutiva] ([id])

/* CreateForeignKey FK_sequenciaprodutivacatalogomaterial_catalogomaterial sequenciaprodutivacatalogomaterial(catalogomaterial_id) catalogomaterial(id) */
ALTER TABLE [dbo].[sequenciaprodutivacatalogomaterial] ADD CONSTRAINT [FK_sequenciaprodutivacatalogomaterial_catalogomaterial] FOREIGN KEY ([catalogomaterial_id]) REFERENCES [dbo].[catalogomaterial] ([id])

/* AlterTable modelo */
/* No SQL statement executed. */

/* AlterColumn modelo referencia String */
ALTER TABLE [dbo].[modelo] ALTER COLUMN [referencia] NVARCHAR(50) NOT NULL

/* AlterColumn modelo tecido String */
ALTER TABLE [dbo].[modelo] ALTER COLUMN [tecido] NVARCHAR(60)

/* AlterColumn modelo detalhamento String */
ALTER TABLE [dbo].[modelo] ALTER COLUMN [detalhamento] NVARCHAR(200) NOT NULL

/* AlterColumn modelo lavada String */
ALTER TABLE [dbo].[modelo] ALTER COLUMN [lavada] NVARCHAR(200)

/* CreateColumn modelo datamodelagem DateTime */
ALTER TABLE [dbo].[modelo] ADD [datamodelagem] DATETIME

/* CreateColumn modelo boca Double */
ALTER TABLE [dbo].[modelo] ADD [boca] DOUBLE PRECISION

/* AlterColumn modelo tamanhopadrao String */
ALTER TABLE [dbo].[modelo] ALTER COLUMN [tamanhopadrao] NVARCHAR(10)

/* AlterColumn modelo linhacasa String */
ALTER TABLE [dbo].[modelo] ALTER COLUMN [linhacasa] NVARCHAR(100)

/* CreateColumn modelo modelagem String */
ALTER TABLE [dbo].[modelo] ADD [modelagem] NVARCHAR(100)

/* CreateColumn modelo etiquetamarca String */
ALTER TABLE [dbo].[modelo] ADD [etiquetamarca] NVARCHAR(100)

/* CreateColumn modelo etiquetacomposicao String */
ALTER TABLE [dbo].[modelo] ADD [etiquetacomposicao] NVARCHAR(100)

/* CreateColumn modelo tag String */
ALTER TABLE [dbo].[modelo] ADD [tag] NVARCHAR(100)

/* CreateColumn modelo tamanho_id Int64 */
ALTER TABLE [dbo].[modelo] ADD [tamanho_id] BIGINT

/* CreateForeignKey FK_modelo_tamanho modelo(tamanho_id) tamanho(id) */
ALTER TABLE [dbo].[modelo] ADD CONSTRAINT [FK_modelo_tamanho] FOREIGN KEY ([tamanho_id]) REFERENCES [dbo].[tamanho] ([id])

/* AlterTable artigo */
/* No SQL statement executed. */

/* AlterColumn artigo descricao String */
ALTER TABLE [dbo].[artigo] ALTER COLUMN [descricao] NVARCHAR(60) NOT NULL

/* AlterTable barra */
/* No SQL statement executed. */

/* AlterColumn barra descricao String */
ALTER TABLE [dbo].[barra] ALTER COLUMN [descricao] NVARCHAR(60) NOT NULL

/* AlterTable classificacao */
/* No SQL statement executed. */

/* AlterColumn classificacao descricao String */
ALTER TABLE [dbo].[classificacao] ALTER COLUMN [descricao] NVARCHAR(60) NOT NULL

/* AlterTable colecao */
/* No SQL statement executed. */

/* AlterColumn colecao descricao String */
ALTER TABLE [dbo].[colecao] ALTER COLUMN [descricao] NVARCHAR(60) NOT NULL

/* AlterTable comprimento */
/* No SQL statement executed. */

/* AlterColumn comprimento descricao String */
ALTER TABLE [dbo].[comprimento] ALTER COLUMN [descricao] NVARCHAR(60) NOT NULL

/* AlterTable tamanho */
/* No SQL statement executed. */

/* AlterColumn tamanho descricao String */
ALTER TABLE [dbo].[tamanho] ALTER COLUMN [descricao] NVARCHAR(60) NOT NULL

/* AlterTable natureza */
/* No SQL statement executed. */

/* AlterColumn natureza descricao String */
ALTER TABLE [dbo].[natureza] ALTER COLUMN [descricao] NVARCHAR(60) NOT NULL

/* AlterTable produtobase */
/* No SQL statement executed. */

/* AlterColumn produtobase descricao String */
ALTER TABLE [dbo].[produtobase] ALTER COLUMN [descricao] NVARCHAR(60) NOT NULL

/* AlterTable segmento */
/* No SQL statement executed. */

/* AlterColumn segmento descricao String */
ALTER TABLE [dbo].[segmento] ALTER COLUMN [descricao] NVARCHAR(60) NOT NULL

/* AlterTable grade */
/* No SQL statement executed. */

/* AlterColumn grade descricao String */
ALTER TABLE [dbo].[grade] ALTER COLUMN [descricao] NVARCHAR(60) NOT NULL

/* ExecuteEmbeddedSqlScript Fashion.ERP.Migrator.Scripts._201310161017.permissao.sql */
DECLARE @ID AS BIGINT;

SET @ID = (SELECT Id FROM permissao WHERE action = 'Index' AND area = 'Comum' AND controller = 'Funcionario');
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id) VALUES ('EditarSituacao', 'Comum', 'Funcionario', 'Editar situação', 0, 1, @ID);

SET @ID = (SELECT Id FROM permissao WHERE action = 'Index' AND area = 'Comum' AND controller = 'Fornecedor');
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id) VALUES ('EditarSituacao', 'Comum', 'Fornecedor', 'Editar situação', 0, 1, @ID);

SET @ID = (SELECT Id FROM permissao WHERE action = 'Index' AND area = 'Comum' AND controller = 'PrestadorServico');
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id) VALUES ('EditarSituacao', 'Comum', 'PrestadorServico', 'Editar situação', 0, 1, @ID);

SET @ID = (SELECT Id FROM permissao WHERE action = 'Index' AND area = 'Comum' AND controller = 'Unidade');
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id) VALUES ('EditarSituacao', 'Comum', 'Unidade', 'Editar situação', 0, 1, @ID);

DECLARE @ENGENHARIAPRODUTOID AS BIGINT
SET @ENGENHARIAPRODUTOID = (SELECT id FROM permissao WHERE area = 'EngenhariaProduto' AND controller IS NULL AND [action] IS NULL AND [permissaopai_id] IS NULL);

-- Criar menu Básicos
DECLARE @BASICOSID AS BIGINT
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id) VALUES (NULL, 'EngenhariaProduto', NULL, 'Básicos', 1, 0, @ENGENHARIAPRODUTOID);
SET @BASICOSID = SCOPE_IDENTITY()

-- Mudar permissaopai_id
DECLARE @OLDID AS BIGINT
-- Artigo
SET @OLDID = (SELECT id FROM permissao WHERE action = 'Index' AND area = 'EngenhariaProduto' AND controller = 'Artigo');
DELETE FROM permissaotoperfildeacesso WHERE permissao_id = @OLDID;
DELETE FROM permissaotoperfildeacesso WHERE permissao_id in (SELECT id FROM permissao WHERE permissaopai_id = @OLDID);
DELETE FROM permissaotousuario WHERE permissao_id = @OLDID;
DELETE FROM permissaotousuario WHERE permissao_id  in (SELECT id FROM permissao WHERE permissaopai_id = @OLDID);
DELETE FROM permissao WHERE permissaopai_id= @OLDID;
DELETE FROM permissao WHERE id= @OLDID;
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id) VALUES ('Index', 'EngenhariaProduto', 'Artigo', 'Artigo', 1, 1, @BASICOSID);
SET @ID = SCOPE_IDENTITY()
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id) VALUES ('Novo', 'EngenhariaProduto', 'Artigo', 'Novo', 0, 1, @ID);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id) VALUES ('Editar', 'EngenhariaProduto', 'Artigo', 'Editar', 0, 1, @ID);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id) VALUES ('Excluir', 'EngenhariaProduto', 'Artigo', 'Excluir', 0, 1, @ID);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id) VALUES ('EditarSituacao', 'EngenhariaProduto', 'Artigo', 'Editar situação', 0, 1, @ID);

-- Barra
SET @OLDID = (SELECT id FROM permissao WHERE action = 'Index' AND area = 'EngenhariaProduto' AND controller = 'Barra');
DELETE FROM permissaotoperfildeacesso WHERE permissao_id = @OLDID;
DELETE FROM permissaotoperfildeacesso WHERE permissao_id in (SELECT id FROM permissao WHERE permissaopai_id = @OLDID);
DELETE FROM permissaotousuario WHERE permissao_id = @OLDID;
DELETE FROM permissaotousuario WHERE permissao_id  in (SELECT id FROM permissao WHERE permissaopai_id = @OLDID);
DELETE FROM permissao WHERE permissaopai_id= @OLDID;
DELETE FROM permissao WHERE id= @OLDID;
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id) VALUES ('Index', 'EngenhariaProduto', 'Barra', 'Tipo de barra', 1, 1, @BASICOSID);
SET @ID = SCOPE_IDENTITY()
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id) VALUES ('Novo', 'EngenhariaProduto', 'Barra', 'Novo', 0, 1, @ID);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id) VALUES ('Editar', 'EngenhariaProduto', 'Barra', 'Editar', 0, 1, @ID);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id) VALUES ('Excluir', 'EngenhariaProduto', 'Barra', 'Excluir', 0, 1, @ID);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id) VALUES ('EditarSituacao', 'EngenhariaProduto', 'Barra', 'Editar situação', 0, 1, @ID);

-- Classificação
SET @OLDID = (SELECT id FROM permissao WHERE action = 'Index' AND area = 'EngenhariaProduto' AND controller = 'Classificacao');
DELETE FROM permissaotoperfildeacesso WHERE permissao_id = @OLDID;
DELETE FROM permissaotoperfildeacesso WHERE permissao_id in (SELECT id FROM permissao WHERE permissaopai_id = @OLDID);
DELETE FROM permissaotousuario WHERE permissao_id = @OLDID;
DELETE FROM permissaotousuario WHERE permissao_id  in (SELECT id FROM permissao WHERE permissaopai_id = @OLDID);
DELETE FROM permissao WHERE permissaopai_id= @OLDID;
DELETE FROM permissao WHERE id= @OLDID;
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id) VALUES ('Index', 'EngenhariaProduto', 'Classificacao', 'Classificação', 1, 1, @BASICOSID);
SET @ID = SCOPE_IDENTITY()
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id) VALUES ('Novo', 'EngenhariaProduto', 'Classificacao', 'Novo', 0, 1, @ID);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id) VALUES ('Editar', 'EngenhariaProduto', 'Classificacao', 'Editar', 0, 1, @ID);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id) VALUES ('Excluir', 'EngenhariaProduto', 'Classificacao', 'Excluir', 0, 1, @ID);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id) VALUES ('EditarSituacao', 'EngenhariaProduto', 'Classificacao', 'Editar situação', 0, 1, @ID);

-- Comprimento
SET @OLDID = (SELECT id FROM permissao WHERE action = 'Index' AND area = 'EngenhariaProduto' AND controller = 'Comprimento');
DELETE FROM permissaotoperfildeacesso WHERE permissao_id = @OLDID;
DELETE FROM permissaotoperfildeacesso WHERE permissao_id in (SELECT id FROM permissao WHERE permissaopai_id = @OLDID);
DELETE FROM permissaotousuario WHERE permissao_id = @OLDID;
DELETE FROM permissaotousuario WHERE permissao_id  in (SELECT id FROM permissao WHERE permissaopai_id = @OLDID);
DELETE FROM permissao WHERE permissaopai_id= @OLDID;
DELETE FROM permissao WHERE id= @OLDID;
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id) VALUES ('Index', 'EngenhariaProduto', 'Comprimento', 'Comprimento', 1, 1, @BASICOSID);
SET @ID = SCOPE_IDENTITY()
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id) VALUES ('Novo', 'EngenhariaProduto', 'Comprimento', 'Novo', 0, 1, @ID);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id) VALUES ('Editar', 'EngenhariaProduto', 'Comprimento', 'Editar', 0, 1, @ID);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id) VALUES ('Excluir', 'EngenhariaProduto', 'Comprimento', 'Excluir', 0, 1, @ID);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id) VALUES ('EditarSituacao', 'EngenhariaProduto', 'Comprimento', 'Editar situação', 0, 1, @ID);

-- Grade
SET @OLDID = (SELECT id FROM permissao WHERE action = 'Index' AND area = 'EngenhariaProduto' AND controller = 'Grade');
DELETE FROM permissaotoperfildeacesso WHERE permissao_id = @OLDID;
DELETE FROM permissaotoperfildeacesso WHERE permissao_id in (SELECT id FROM permissao WHERE permissaopai_id = @OLDID);
DELETE FROM permissaotousuario WHERE permissao_id = @OLDID;
DELETE FROM permissaotousuario WHERE permissao_id  in (SELECT id FROM permissao WHERE permissaopai_id = @OLDID);
DELETE FROM permissao WHERE permissaopai_id= @OLDID;
DELETE FROM permissao WHERE id= @OLDID;
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id) VALUES ('Index', 'EngenhariaProduto', 'Grade', 'Grade', 1, 1, @BASICOSID);
SET @ID = SCOPE_IDENTITY()
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id) VALUES ('Novo', 'EngenhariaProduto', 'Grade', 'Novo', 0, 1, @ID);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id) VALUES ('Editar', 'EngenhariaProduto', 'Grade', 'Editar', 0, 1, @ID);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id) VALUES ('Excluir', 'EngenhariaProduto', 'Grade', 'Excluir', 0, 1, @ID);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id) VALUES ('EditarSituacao', 'EngenhariaProduto', 'Grade', 'Editar situação', 0, 1, @ID);

-- Natureza
SET @OLDID = (SELECT id FROM permissao WHERE action = 'Index' AND area = 'EngenhariaProduto' AND controller = 'Natureza');
DELETE FROM permissaotoperfildeacesso WHERE permissao_id = @OLDID;
DELETE FROM permissaotoperfildeacesso WHERE permissao_id in (SELECT id FROM permissao WHERE permissaopai_id = @OLDID);
DELETE FROM permissaotousuario WHERE permissao_id = @OLDID;
DELETE FROM permissaotousuario WHERE permissao_id  in (SELECT id FROM permissao WHERE permissaopai_id = @OLDID);
DELETE FROM permissao WHERE permissaopai_id= @OLDID;
DELETE FROM permissao WHERE id= @OLDID;
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id) VALUES ('Index', 'EngenhariaProduto', 'Natureza', 'Natureza', 1, 1, @BASICOSID);
SET @ID = SCOPE_IDENTITY()
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id) VALUES ('Novo', 'EngenhariaProduto', 'Natureza', 'Novo', 0, 1, @ID);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id) VALUES ('Editar', 'EngenhariaProduto', 'Natureza', 'Editar', 0, 1, @ID);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id) VALUES ('Excluir', 'EngenhariaProduto', 'Natureza', 'Excluir', 0, 1, @ID);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id) VALUES ('EditarSituacao', 'EngenhariaProduto', 'Natureza', 'Editar situação', 0, 1, @ID);

-- Produto Base
SET @OLDID = (SELECT id FROM permissao WHERE action = 'Index' AND area = 'EngenhariaProduto' AND controller = 'ProdutoBase');
DELETE FROM permissaotoperfildeacesso WHERE permissao_id = @OLDID;
DELETE FROM permissaotoperfildeacesso WHERE permissao_id in (SELECT id FROM permissao WHERE permissaopai_id = @OLDID);
DELETE FROM permissaotousuario WHERE permissao_id = @OLDID;
DELETE FROM permissaotousuario WHERE permissao_id  in (SELECT id FROM permissao WHERE permissaopai_id = @OLDID);
DELETE FROM permissao WHERE permissaopai_id= @OLDID;
DELETE FROM permissao WHERE id= @OLDID;
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id) VALUES ('Index', 'EngenhariaProduto', 'ProdutoBase', 'Produto base', 1, 1, @BASICOSID);
SET @ID = SCOPE_IDENTITY()
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id) VALUES ('Novo', 'EngenhariaProduto', 'ProdutoBase', 'Novo', 0, 1, @ID);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id) VALUES ('Editar', 'EngenhariaProduto', 'ProdutoBase', 'Editar', 0, 1, @ID);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id) VALUES ('Excluir', 'EngenhariaProduto', 'ProdutoBase', 'Excluir', 0, 1, @ID);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id) VALUES ('EditarSituacao', 'EngenhariaProduto', 'ProdutoBase', 'Editar situação', 0, 1, @ID);

-- Segmento
SET @OLDID = (SELECT id FROM permissao WHERE action = 'Index' AND area = 'EngenhariaProduto' AND controller = 'Segmento');
DELETE FROM permissaotoperfildeacesso WHERE permissao_id = @OLDID;
DELETE FROM permissaotoperfildeacesso WHERE permissao_id in (SELECT id FROM permissao WHERE permissaopai_id = @OLDID);
DELETE FROM permissaotousuario WHERE permissao_id = @OLDID;
DELETE FROM permissaotousuario WHERE permissao_id  in (SELECT id FROM permissao WHERE permissaopai_id = @OLDID);
DELETE FROM permissao WHERE permissaopai_id= @OLDID;
DELETE FROM permissao WHERE id= @OLDID;
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id) VALUES ('Index', 'EngenhariaProduto', 'Segmento', 'Segmento', 1, 1, @BASICOSID);
SET @ID = SCOPE_IDENTITY()
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id) VALUES ('Novo', 'EngenhariaProduto', 'Segmento', 'Novo', 0, 1, @ID);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id) VALUES ('Editar', 'EngenhariaProduto', 'Segmento', 'Editar', 0, 1, @ID);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id) VALUES ('Excluir', 'EngenhariaProduto', 'Segmento', 'Excluir', 0, 1, @ID);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id) VALUES ('EditarSituacao', 'EngenhariaProduto', 'Segmento', 'Editar situação', 0, 1, @ID);

-- Coleção (novo)
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id) VALUES ('Index', 'Comum', 'Colecao', 'Coleção', 1, 1, @BASICOSID);
SET @ID = SCOPE_IDENTITY()
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id) VALUES ('Novo', 'Comum', 'Colecao', 'Novo', 0, 1, @ID);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id) VALUES ('Editar', 'Comum', 'Colecao', 'Editar', 0, 1, @ID);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id) VALUES ('Excluir', 'Comum', 'Colecao', 'Excluir', 0, 1, @ID);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id) VALUES ('EditarSituacao', 'Comum', 'Colecao', 'Editar situação', 0, 1, @ID);

-- Funcionário (novo)
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id) VALUES ('Index', 'Comum', 'Funcionario', 'Funcionário', 1, 1, @BASICOSID);
SET @ID = SCOPE_IDENTITY()
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id) VALUES ('Novo', 'Comum', 'Funcionario', 'Novo', 0, 1, @ID);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id) VALUES ('Editar', 'Comum', 'Funcionario', 'Editar', 0, 1, @ID);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id) VALUES ('Excluir', 'Comum', 'Funcionario', 'Excluir', 0, 1, @ID);
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id) VALUES ('EditarSituacao', 'Comum', 'Funcionario', 'Editar situação', 0, 1, @ID);

-- Marca (novo)
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id) VALUES ('Index', 'Comum', 'Marca', 'Marca', 1, 1, @BASICOSID);
SET @ID = SCOPE_IDENTITY()
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id) VALUES ('Editar', 'Comum', 'Marca', 'Editar', 0, 1, @ID);
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id) VALUES ('Excluir', 'Comum', 'Marca', 'Excluir', 0, 1, @ID);
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id) VALUES ('Novo', 'Comum', 'Marca', 'Novo', 0, 1, @ID);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id) VALUES ('EditarSituacao', 'Comum', 'Marca', 'Editar situação', 0, 1, @ID);

-- Tamanho (novo)
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id) VALUES ('Index', 'Comum', 'Tamanho', 'Tamanho', 1, 1, @BASICOSID);
SET @ID = SCOPE_IDENTITY()
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id) VALUES ('Novo', 'Comum', 'Tamanho', 'Novo', 0, 1, @ID);
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id) VALUES ('Editar', 'Comum', 'Tamanho', 'Editar', 0, 1, @ID);
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id) VALUES ('Excluir', 'Comum', 'Tamanho', 'Excluir', 0, 1, @ID);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id) VALUES ('EditarSituacao', 'Comum', 'Tamanho', 'Editar situação', 0, 1, @ID);

-- Criar menu Fases Produtivas
DECLARE @FASESPRODUTIVASID AS BIGINT
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id) VALUES (NULL, 'EngenhariaProduto', NULL, 'Fases Produtivas', 1, 0, @ENGENHARIAPRODUTOID);
SET @FASESPRODUTIVASID = SCOPE_IDENTITY()

-- Mudar permissaopai_id 
-- 2. Setor Departamento
SET @OLDID = (SELECT id FROM permissao WHERE action = 'Index' AND area = 'EngenhariaProduto' AND controller = 'SetorProducao');
DELETE FROM permissaotoperfildeacesso WHERE permissao_id = @OLDID;
DELETE FROM permissaotoperfildeacesso WHERE permissao_id in (SELECT id FROM permissao WHERE permissaopai_id = @OLDID);
DELETE FROM permissaotousuario WHERE permissao_id = @OLDID;
DELETE FROM permissaotousuario WHERE permissao_id  in (SELECT id FROM permissao WHERE permissaopai_id = @OLDID);
DELETE FROM permissao WHERE permissaopai_id= @OLDID;
DELETE FROM permissao WHERE id= @OLDID;
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id) VALUES ('Index', 'EngenhariaProduto', 'SetorProducao', '2. Setor Departamento', 1, 1, @FASESPRODUTIVASID);
SET @ID = SCOPE_IDENTITY()
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id) VALUES ('Novo', 'EngenhariaProduto', 'SetorProducao', 'Novo', 0, 1, @ID);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id) VALUES ('Editar', 'EngenhariaProduto', 'SetorProducao', 'Editar', 0, 1, @ID);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id) VALUES ('Excluir', 'EngenhariaProduto', 'SetorProducao', 'Excluir', 0, 1, @ID);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id) VALUES ('EditarSituacao', 'EngenhariaProduto', 'SetorProducao', 'Editar situação', 0, 1, @ID);

-- 3. Operação Setor
SET @OLDID = (SELECT id FROM permissao WHERE action = 'Index' AND area = 'EngenhariaProduto' AND controller = 'OperacaoProducao');
DELETE FROM permissaotoperfildeacesso WHERE permissao_id = @OLDID;
DELETE FROM permissaotoperfildeacesso WHERE permissao_id in (SELECT id FROM permissao WHERE permissaopai_id = @OLDID);
DELETE FROM permissaotousuario WHERE permissao_id = @OLDID;
DELETE FROM permissaotousuario WHERE permissao_id  in (SELECT id FROM permissao WHERE permissaopai_id = @OLDID);
DELETE FROM permissao WHERE permissaopai_id= @OLDID;
DELETE FROM permissao WHERE id= @OLDID;
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id) VALUES ('Index', 'EngenhariaProduto', 'OperacaoProducao', '3. Operação Setor', 1, 1, @FASESPRODUTIVASID);
SET @ID = SCOPE_IDENTITY()
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id) VALUES ('Novo', 'EngenhariaProduto', 'OperacaoProducao', 'Novo', 0, 1, @ID);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id) VALUES ('Editar', 'EngenhariaProduto', 'OperacaoProducao', 'Editar', 0, 1, @ID);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id) VALUES ('Excluir', 'EngenhariaProduto', 'OperacaoProducao', 'Excluir', 0, 1, @ID);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id) VALUES ('EditarSituacao', 'EngenhariaProduto', 'OperacaoProducao', 'Editar situação', 0, 1, @ID);

-- Criar menu filho 1. Departamento Produção
insert into permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id) values ('Index', 'Comum', 'DepartamentoProducao', '1. Departamento Produção', 1, 1, @FASESPRODUTIVASID);
SET @ID = SCOPE_IDENTITY()
insert into permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id) values ('Novo', 'Comum', 'DepartamentoProducao', 'Novo', 0, 1, @ID);
insert into permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id) values ('Editar', 'Comum', 'DepartamentoProducao', 'Editar', 0, 1, @ID);
insert into permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id) values ('Excluir', 'Comum', 'DepartamentoProducao', 'Excluir', 0, 1, @ID);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id) VALUES ('EditarSituacao', 'Comum', 'DepartamentoProducao', 'Editar situação', 0, 1, @ID);

-- Atualiza o menu do Modelo
DECLARE @MODELOID AS BIGINT;
SET @MODELOID = (SELECT id FROM permissao WHERE action = 'Index' AND area = 'EngenhariaProduto' AND controller = 'Modelo');
-- Atualiza a descrição do modelo
UPDATE permissao SET descricao = 'Modelo' WHERE id = @MODELOID;
-- Editar
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id) VALUES ('Detalhar', 'EngenhariaProduto', 'Modelo', 'Detalhar', 0, 1, @MODELOID);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id) VALUES ('Variacao', 'EngenhariaProduto', 'Modelo', 'Variação', 0, 1, @MODELOID);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id) VALUES ('SequenciaProducao', 'EngenhariaProduto', 'Modelo', 'Sequência Produção', 0, 1, @MODELOID);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id) VALUES ('Modelagem', 'EngenhariaProduto', 'Modelo', 'Modelagem', 0, 1, @MODELOID);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id) VALUES ('Composicao', 'EngenhariaProduto', 'Modelo', 'Composição', 0, 1, @MODELOID);


DECLARE @COMUMID AS BIGINT
SET @COMUMID = (SELECT id FROM permissao WHERE area = 'Comum' AND controller IS NULL AND [action] IS NULL AND [permissaopai_id] IS NULL);

-- Comum -> Cor
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id) VALUES ('Index', 'Comum', 'Cor', 'Cor', 1, 1, @COMUMID);
SET @ID = SCOPE_IDENTITY()
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id) VALUES ('Novo', 'Comum', 'Cor', 'Novo', 0, 1, @ID);
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id) VALUES ('Editar', 'Comum', 'Cor', 'Editar', 0, 1, @ID);
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id) VALUES ('Excluir', 'Comum', 'Cor', 'Excluir', 0, 1, @ID);
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id) VALUES ('EditarSituacao', 'Comum', 'Cor', 'Editar situação', 0, 1, @ID);

-- EngenhariaProduto -> Básicos -> Cor
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id) VALUES ('Index', 'Comum', 'Cor', 'Cor', 1, 1, @BASICOSID);
SET @ID = SCOPE_IDENTITY()
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id) VALUES ('Novo', 'Comum', 'Cor', 'Novo', 0, 1, @ID);
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id) VALUES ('Editar', 'Comum', 'Cor', 'Editar', 0, 1, @ID);
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id) VALUES ('Excluir', 'Comum', 'Cor', 'Excluir', 0, 1, @ID);
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id) VALUES ('EditarSituacao', 'Comum', 'Cor', 'Editar situação', 0, 1, @ID);

-- Corrigir menu Tamanho
SET @ID = (SELECT Id FROM permissao WHERE Action = 'Index' AND Area = 'Comum' AND Controller = 'Tamanho' AND permissaopai_id = @COMUMID);
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id) VALUES ('EditarSituacao', 'Comum', 'Tamanho', 'EditarSituacao', 0, 1, @ID);
UPDATE permissao SET Action = 'Excluir', Descricao = 'Excluir' WHERE Action = 'Inativar' AND Controller = 'Tamanho';

/* ExecuteEmbeddedSqlScript Fashion.ERP.Migrator.Scripts._201310161017.permissao2.sql */
-- Atualizado MvcSiteMapProvider para versão 4.4.4

-- nova permissão AlterarSenha
DECLARE @USUARIOID AS BIGINT;
SET @USUARIOID = (SELECT id FROM permissao WHERE area = 'Comum' AND controller = 'Usuario' AND [action] = 'Index');
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id) VALUES ('AlterarSenha', 'Comum', 'Usuario', 'Alterar senha', 0, 1, @USUARIOID);

-- Adicionar Ordem à tabela permissao
ALTER TABLE permissao ADD ordem INT NULL;
GO
UPDATE permissao SET ordem = 0;
GO
ALTER TABLE permissao ALTER COLUMN ordem INT NOT NULL;
GO

-- Materiais de Composição
UPDATE permissao SET descricao = 'Materiais de Composição' WHERE area = 'EngenhariaProduto' AND controller = 'Modelo' AND action = 'Composicao';

/* ExecuteSqlStatement sp_rename 'sequenciaprodutiva', 'sequenciaproducao' */
sp_rename 'sequenciaprodutiva', 'sequenciaproducao'

/* AlterTable sequenciaproducao */
/* No SQL statement executed. */

/* CreateColumn sequenciaproducao setorproducao_id Int64 */
ALTER TABLE [dbo].[sequenciaproducao] ADD [setorproducao_id] BIGINT

/* CreateForeignKey FK_sequenciaproducao_setorproducao sequenciaproducao(setorproducao_id) setorproducao(id) */
ALTER TABLE [dbo].[sequenciaproducao] ADD CONSTRAINT [FK_sequenciaproducao_setorproducao] FOREIGN KEY ([setorproducao_id]) REFERENCES [dbo].[setorproducao] ([id])

/* DeleteTable sequenciaprodutivacatalogomaterial */
DROP TABLE [dbo].[sequenciaprodutivacatalogomaterial]

/* CreateTable materialcomposicaomodelo */
CREATE TABLE [dbo].[materialcomposicaomodelo] ([id] BIGINT NOT NULL, [quantidade] DOUBLE PRECISION NOT NULL, [unidademedida_id] BIGINT NOT NULL, [catalogomaterial_id] BIGINT NOT NULL, [tamanho_id] BIGINT, [cor_id] BIGINT, [variacao_id] BIGINT, [sequenciaproducao_id] BIGINT NOT NULL, CONSTRAINT [PK_materialcomposicaomodelo] PRIMARY KEY ([id]))

/* CreateForeignKey FK_materialcomposicaomodelo_unidademedida materialcomposicaomodelo(unidademedida_id) unidademedida(id) */
ALTER TABLE [dbo].[materialcomposicaomodelo] ADD CONSTRAINT [FK_materialcomposicaomodelo_unidademedida] FOREIGN KEY ([unidademedida_id]) REFERENCES [dbo].[unidademedida] ([id])

/* CreateForeignKey FK_materialcomposicaomodelo_catalogomaterial materialcomposicaomodelo(catalogomaterial_id) catalogomaterial(id) */
ALTER TABLE [dbo].[materialcomposicaomodelo] ADD CONSTRAINT [FK_materialcomposicaomodelo_catalogomaterial] FOREIGN KEY ([catalogomaterial_id]) REFERENCES [dbo].[catalogomaterial] ([id])

/* CreateForeignKey FK_materialcomposicaomodelo_tamanho materialcomposicaomodelo(tamanho_id) tamanho(id) */
ALTER TABLE [dbo].[materialcomposicaomodelo] ADD CONSTRAINT [FK_materialcomposicaomodelo_tamanho] FOREIGN KEY ([tamanho_id]) REFERENCES [dbo].[tamanho] ([id])

/* CreateForeignKey FK_materialcomposicaomodelo_cor materialcomposicaomodelo(cor_id) cor(id) */
ALTER TABLE [dbo].[materialcomposicaomodelo] ADD CONSTRAINT [FK_materialcomposicaomodelo_cor] FOREIGN KEY ([cor_id]) REFERENCES [dbo].[cor] ([id])

/* CreateForeignKey FK_materialcomposicaomodelo_variacao materialcomposicaomodelo(variacao_id) variacao(id) */
ALTER TABLE [dbo].[materialcomposicaomodelo] ADD CONSTRAINT [FK_materialcomposicaomodelo_variacao] FOREIGN KEY ([variacao_id]) REFERENCES [dbo].[variacao] ([id])

/* CreateForeignKey FK_materialcomposicaomodelo_sequenciaproducao materialcomposicaomodelo(sequenciaproducao_id) sequenciaproducao(id) */
ALTER TABLE [dbo].[materialcomposicaomodelo] ADD CONSTRAINT [FK_materialcomposicaomodelo_sequenciaproducao] FOREIGN KEY ([sequenciaproducao_id]) REFERENCES [dbo].[sequenciaproducao] ([id])

/* AlterTable modelo */
/* No SQL statement executed. */

/* CreateColumn modelo complemento String */
ALTER TABLE [dbo].[modelo] ADD [complemento] NVARCHAR(50)

/* AlterTable modelofoto */
/* No SQL statement executed. */

/* AlterColumn modelofoto modelo_id Int64 */
ALTER TABLE [dbo].[modelofoto] ALTER COLUMN [modelo_id] BIGINT

INSERT INTO [dbo].[VersionInfo] ([Version], [AppliedOn], [Description]) VALUES (201310161017, '2015-02-04T16:38:13', 'Migration201310161017')
/* Committing Transaction */
/* 201310161017: Migration201310161017 migrated */

/* 201311280940: Migration201311280940 migrating ============================= */

/* Beginning Transaction */
/* AlterTable modelo */
/* No SQL statement executed. */

/* AlterColumn modelo segmento_id Int64 */
ALTER TABLE [dbo].[modelo] ALTER COLUMN [segmento_id] BIGINT

/* ExecuteSqlStatement UPDATE funcionario SET funcaofuncionario = 'SupervisorLoja' WHERE funcaofuncionario = 'Supervisor'; */
UPDATE funcionario SET funcaofuncionario = 'SupervisorLoja' WHERE funcaofuncionario = 'Supervisor';

/* AlterTable referencia */
/* No SQL statement executed. */

/* AlterColumn referencia telefone String */
ALTER TABLE [dbo].[referencia] ALTER COLUMN [telefone] NVARCHAR(20)

/* AlterColumn referencia celular String */
ALTER TABLE [dbo].[referencia] ALTER COLUMN [celular] NVARCHAR(20)

/* AlterColumn referencia observacao String */
ALTER TABLE [dbo].[referencia] ALTER COLUMN [observacao] NVARCHAR(4000)

INSERT INTO [dbo].[VersionInfo] ([Version], [AppliedOn], [Description]) VALUES (201311280940, '2015-02-04T16:38:13', 'Migration201311280940')
/* Committing Transaction */
/* 201311280940: Migration201311280940 migrated */

/* 201312020551: Migration201312020551 migrating ============================= */

/* Beginning Transaction */
/* CreateTable auditoria */
CREATE TABLE [dbo].[auditoria] ([operacao] NVARCHAR(7) NOT NULL, [tabela] NVARCHAR(100) NOT NULL, [registro] BIGINT NOT NULL, [usuario] BIGINT NOT NULL, [login] NVARCHAR(50) NOT NULL, [detalhe] nvarchar(max), [data] DATETIME NOT NULL)

/* CreateTable depositomaterial */
CREATE TABLE [dbo].[depositomaterial] ([id] BIGINT NOT NULL, [nome] NVARCHAR(60) NOT NULL, [dataabertura] DATETIME NOT NULL, [ativo] BIT NOT NULL, [unidade_id] BIGINT NOT NULL, CONSTRAINT [PK_depositomaterial] PRIMARY KEY ([id]))

/* CreateForeignKey FK_depositomaterial_unidade depositomaterial(unidade_id) pessoa(id) */
ALTER TABLE [dbo].[depositomaterial] ADD CONSTRAINT [FK_depositomaterial_unidade] FOREIGN KEY ([unidade_id]) REFERENCES [dbo].[pessoa] ([id])

/* CreateTable depositomaterialfuncionario */
CREATE TABLE [dbo].[depositomaterialfuncionario] ([depositomaterial_id] BIGINT NOT NULL, [funcionario_id] BIGINT NOT NULL)

/* CreateForeignKey FK_depositomaterialfuncionario_deposito depositomaterialfuncionario(depositomaterial_id) depositomaterial(id) */
ALTER TABLE [dbo].[depositomaterialfuncionario] ADD CONSTRAINT [FK_depositomaterialfuncionario_deposito] FOREIGN KEY ([depositomaterial_id]) REFERENCES [dbo].[depositomaterial] ([id])

/* CreateForeignKey FK_depositomaterialfuncionario_funcionario depositomaterialfuncionario(funcionario_id) pessoa(id) */
ALTER TABLE [dbo].[depositomaterialfuncionario] ADD CONSTRAINT [FK_depositomaterialfuncionario_funcionario] FOREIGN KEY ([funcionario_id]) REFERENCES [dbo].[pessoa] ([id])

/* CreateTable estoquecatalogomaterial */
CREATE TABLE [dbo].[estoquecatalogomaterial] ([id] BIGINT NOT NULL, [quantidade] DOUBLE PRECISION NOT NULL, [reserva] DOUBLE PRECISION NOT NULL, [catalogomaterial_id] BIGINT NOT NULL, [depositomaterial_id] BIGINT NOT NULL, CONSTRAINT [PK_estoquecatalogomaterial] PRIMARY KEY ([id]))

/* CreateForeignKey FK_estoquecatalogomaterial_catalogomaterial estoquecatalogomaterial(catalogomaterial_id) catalogomaterial(id) */
ALTER TABLE [dbo].[estoquecatalogomaterial] ADD CONSTRAINT [FK_estoquecatalogomaterial_catalogomaterial] FOREIGN KEY ([catalogomaterial_id]) REFERENCES [dbo].[catalogomaterial] ([id])

/* CreateForeignKey FK_estoquecatalogomaterial_depositomaterial estoquecatalogomaterial(depositomaterial_id) depositomaterial(id) */
ALTER TABLE [dbo].[estoquecatalogomaterial] ADD CONSTRAINT [FK_estoquecatalogomaterial_depositomaterial] FOREIGN KEY ([depositomaterial_id]) REFERENCES [dbo].[depositomaterial] ([id])

/* CreateTable entradacatalogomaterial */
CREATE TABLE [dbo].[entradacatalogomaterial] ([id] BIGINT NOT NULL, [dataentrada] DATETIME NOT NULL, [depositomaterialdestino_id] BIGINT NOT NULL, [depositomaterialorigem_id] BIGINT, [fornecedor_id] BIGINT, CONSTRAINT [PK_entradacatalogomaterial] PRIMARY KEY ([id]))

/* CreateForeignKey FK_entradacatalogomaterial_depositomaterialdestino entradacatalogomaterial(depositomaterialdestino_id) depositomaterial(id) */
ALTER TABLE [dbo].[entradacatalogomaterial] ADD CONSTRAINT [FK_entradacatalogomaterial_depositomaterialdestino] FOREIGN KEY ([depositomaterialdestino_id]) REFERENCES [dbo].[depositomaterial] ([id])

/* CreateForeignKey FK_entradacatalogomaterial_depositomaterialorigem entradacatalogomaterial(depositomaterialorigem_id) depositomaterial(id) */
ALTER TABLE [dbo].[entradacatalogomaterial] ADD CONSTRAINT [FK_entradacatalogomaterial_depositomaterialorigem] FOREIGN KEY ([depositomaterialorigem_id]) REFERENCES [dbo].[depositomaterial] ([id])

/* CreateForeignKey FK_entradacatalogomaterial_fornecedor entradacatalogomaterial(fornecedor_id) pessoa(id) */
ALTER TABLE [dbo].[entradacatalogomaterial] ADD CONSTRAINT [FK_entradacatalogomaterial_fornecedor] FOREIGN KEY ([fornecedor_id]) REFERENCES [dbo].[pessoa] ([id])

/* CreateTable entradaitemcatalogomaterial */
CREATE TABLE [dbo].[entradaitemcatalogomaterial] ([id] BIGINT NOT NULL, [quantidadecompra] DOUBLE PRECISION NOT NULL, [fatormultiplicativo] DOUBLE PRECISION NOT NULL, [quantidade] DOUBLE PRECISION NOT NULL, [entradacatalogomaterial_id] BIGINT NOT NULL, [catalogomaterial_id] BIGINT NOT NULL, [unidademedida_id] BIGINT NOT NULL, CONSTRAINT [PK_entradaitemcatalogomaterial] PRIMARY KEY ([id]))

/* CreateForeignKey FK_entradaitemcatalogomaterial_entradacatalogomaterial entradaitemcatalogomaterial(entradacatalogomaterial_id) entradacatalogomaterial(id) */
ALTER TABLE [dbo].[entradaitemcatalogomaterial] ADD CONSTRAINT [FK_entradaitemcatalogomaterial_entradacatalogomaterial] FOREIGN KEY ([entradacatalogomaterial_id]) REFERENCES [dbo].[entradacatalogomaterial] ([id])

/* CreateForeignKey FK_entradaitemcatalogomaterial_catalogomaterial entradaitemcatalogomaterial(catalogomaterial_id) catalogomaterial(id) */
ALTER TABLE [dbo].[entradaitemcatalogomaterial] ADD CONSTRAINT [FK_entradaitemcatalogomaterial_catalogomaterial] FOREIGN KEY ([catalogomaterial_id]) REFERENCES [dbo].[catalogomaterial] ([id])

/* CreateForeignKey FK_entradaitemcatalogomaterial_unidademedida entradaitemcatalogomaterial(unidademedida_id) unidademedida(id) */
ALTER TABLE [dbo].[entradaitemcatalogomaterial] ADD CONSTRAINT [FK_entradaitemcatalogomaterial_unidademedida] FOREIGN KEY ([unidademedida_id]) REFERENCES [dbo].[unidademedida] ([id])

/* CreateTable centrocusto */
CREATE TABLE [dbo].[centrocusto] ([id] BIGINT NOT NULL, [codigo] BIGINT NOT NULL, [nome] NVARCHAR(60) NOT NULL, [ativo] BIT NOT NULL, CONSTRAINT [PK_centrocusto] PRIMARY KEY ([id]))

/* CreateTable saidacatalogomaterial */
CREATE TABLE [dbo].[saidacatalogomaterial] ([id] BIGINT NOT NULL, [datasaida] DATETIME NOT NULL, [depositomaterialdestino_id] BIGINT, [depositomaterialorigem_id] BIGINT NOT NULL, [centrocusto_id] BIGINT, CONSTRAINT [PK_saidacatalogomaterial] PRIMARY KEY ([id]))

/* CreateForeignKey FK_saidacatalogomaterial_depositomaterialdestino saidacatalogomaterial(depositomaterialdestino_id) depositomaterial(id) */
ALTER TABLE [dbo].[saidacatalogomaterial] ADD CONSTRAINT [FK_saidacatalogomaterial_depositomaterialdestino] FOREIGN KEY ([depositomaterialdestino_id]) REFERENCES [dbo].[depositomaterial] ([id])

/* CreateForeignKey FK_saidacatalogomaterial_depositomaterialorigem saidacatalogomaterial(depositomaterialorigem_id) depositomaterial(id) */
ALTER TABLE [dbo].[saidacatalogomaterial] ADD CONSTRAINT [FK_saidacatalogomaterial_depositomaterialorigem] FOREIGN KEY ([depositomaterialorigem_id]) REFERENCES [dbo].[depositomaterial] ([id])

/* CreateForeignKey FK_saidacatalogomaterial_centrocusto saidacatalogomaterial(centrocusto_id) centrocusto(id) */
ALTER TABLE [dbo].[saidacatalogomaterial] ADD CONSTRAINT [FK_saidacatalogomaterial_centrocusto] FOREIGN KEY ([centrocusto_id]) REFERENCES [dbo].[centrocusto] ([id])

/* CreateTable saidaitemcatalogomaterial */
CREATE TABLE [dbo].[saidaitemcatalogomaterial] ([id] BIGINT NOT NULL, [quantidade] DOUBLE PRECISION NOT NULL, [saidacatalogomaterial_id] BIGINT NOT NULL, [catalogomaterial_id] BIGINT NOT NULL, CONSTRAINT [PK_saidaitemcatalogomaterial] PRIMARY KEY ([id]))

/* CreateForeignKey FK_saidaitemcatalogomaterial_saidacatalogomaterial saidaitemcatalogomaterial(saidacatalogomaterial_id) saidacatalogomaterial(id) */
ALTER TABLE [dbo].[saidaitemcatalogomaterial] ADD CONSTRAINT [FK_saidaitemcatalogomaterial_saidacatalogomaterial] FOREIGN KEY ([saidacatalogomaterial_id]) REFERENCES [dbo].[saidacatalogomaterial] ([id])

/* CreateForeignKey FK_saidaitemcatalogomaterial_catalogomaterial saidaitemcatalogomaterial(catalogomaterial_id) catalogomaterial(id) */
ALTER TABLE [dbo].[saidaitemcatalogomaterial] ADD CONSTRAINT [FK_saidaitemcatalogomaterial_catalogomaterial] FOREIGN KEY ([catalogomaterial_id]) REFERENCES [dbo].[catalogomaterial] ([id])

/* AlterTable modelo */
/* No SQL statement executed. */

/* CreateColumn modelo tecidocomplementar String */
ALTER TABLE [dbo].[modelo] ADD [tecidocomplementar] NVARCHAR(100)

/* CreateColumn modelo forro String */
ALTER TABLE [dbo].[modelo] ADD [forro] NVARCHAR(100)

/* CreateColumn modelo ziperbraguilha String */
ALTER TABLE [dbo].[modelo] ADD [ziperbraguilha] NVARCHAR(100)

/* CreateColumn modelo ziperdetalhe String */
ALTER TABLE [dbo].[modelo] ADD [ziperdetalhe] NVARCHAR(100)

/* AlterTable setorproducao */
/* No SQL statement executed. */

/* AlterColumn setorproducao nome String */
ALTER TABLE [dbo].[setorproducao] ALTER COLUMN [nome] NVARCHAR(100) NOT NULL

/* AlterTable operacaoproducao */
/* No SQL statement executed. */

/* AlterColumn operacaoproducao descricao String */
ALTER TABLE [dbo].[operacaoproducao] ALTER COLUMN [descricao] NVARCHAR(100) NOT NULL

/* ExecuteEmbeddedSqlScript Fashion.ERP.Migrator.Scripts._201312020551.permissao.sql */
DECLARE @ALMOXARIFADOID AS BIGINT, @ID AS BIGINT, @BASICOSID AS BIGINT, @CADASTROID AS BIGINT, @MOVIMENTOID AS BIGINT, @CONSULTAID AS BIGINT;
SET @ALMOXARIFADOID = (SELECT id FROM permissao WHERE area = 'Almoxarifado' AND controller IS NULL AND [action] IS NULL AND [permissaopai_id] IS NULL);

-- Basicos
-- Mover: Categoria, Família, Marca, SubCategoria, Unidade de Medida
-- Copiar: Fornecedor
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, Ordem, permissaopai_id) VALUES (NULL, 'Almoxarifado', NULL, 'Básicos', 1, 1, 0, @ALMOXARIFADOID);
SET @BASICOSID = SCOPE_IDENTITY();
-- Categoria
SET @ID = (SELECT id FROM permissao WHERE area = 'Almoxarifado' AND controller = 'Categoria' AND [action] = 'Index' AND [permissaopai_id] = @ALMOXARIFADOID);
UPDATE permissao SET [permissaopai_id] = @BASICOSID WHERE id = @ID;
-- Família
SET @ID = (SELECT id FROM permissao WHERE area = 'Almoxarifado' AND controller = 'Familia' AND [action] = 'Index' AND [permissaopai_id] = @ALMOXARIFADOID);
UPDATE permissao SET [permissaopai_id] = @BASICOSID WHERE id = @ID;
-- Marca
SET @ID = (SELECT id FROM permissao WHERE area = 'Almoxarifado' AND controller = 'MarcaMaterial' AND [action] = 'Index' AND [permissaopai_id] = @ALMOXARIFADOID);
UPDATE permissao SET [permissaopai_id] = @BASICOSID WHERE id = @ID;
-- SubCategoria
SET @ID = (SELECT id FROM permissao WHERE area = 'Almoxarifado' AND controller = 'Subcategoria' AND [action] = 'Index' AND [permissaopai_id] = @ALMOXARIFADOID);
UPDATE permissao SET [permissaopai_id] = @BASICOSID WHERE id = @ID;
-- Unidade de Medida
SET @ID = (SELECT id FROM permissao WHERE area = 'Almoxarifado' AND controller = 'UnidadeMedida' AND [action] = 'Index' AND [permissaopai_id] = @ALMOXARIFADOID);
UPDATE permissao SET [permissaopai_id] = @BASICOSID WHERE id = @ID;
-- Fornecedor
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, Ordem, permissaopai_id) VALUES ('Index', 'Comum', 'Fornecedor', 'Fornecedor', 1, 1, 0, @BASICOSID);
SET @ID = SCOPE_IDENTITY()
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, Ordem, permissaopai_id) VALUES ('Novo', 'Comum', 'Fornecedor', 'Novo', 0, 1, 0, @ID);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, Ordem, permissaopai_id) VALUES ('Editar', 'Comum', 'Fornecedor', 'Editar', 0, 1, 0, @ID);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, Ordem, permissaopai_id) VALUES ('Excluir', 'Comum', 'Fornecedor', 'Excluir', 0, 1, 0, @ID);
-- Centro de custos
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, Ordem, permissaopai_id) VALUES ('Index', 'Comum', 'CentroCusto', 'Centro de Custo', 1, 1, 0, @BASICOSID);
SET @ID = SCOPE_IDENTITY()
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, Ordem, permissaopai_id) VALUES ('Novo', 'Comum', 'CentroCusto', 'Novo', 0, 1, 0, @ID);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, Ordem, permissaopai_id) VALUES ('Editar', 'Comum', 'CentroCusto', 'Editar', 0, 1, 0, @ID);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, Ordem, permissaopai_id) VALUES ('Excluir', 'Comum', 'CentroCusto', 'Excluir', 0, 1, 0, @ID);
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, Ordem, permissaopai_id) VALUES ('EditarSituacao', 'Comum', 'CentroCusto', 'Editar situação', 0, 1, 0, @ID);

-- Cadastro
-- Catalogo de Material
-- Novo: Depósito de Material
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, Ordem, permissaopai_id) VALUES (NULL, 'Almoxarifado', NULL, 'Cadastro', 1, 1, 0, @ALMOXARIFADOID);
SET @CADASTROID = SCOPE_IDENTITY();
-- CatalogoMaterial
SET @ID = (SELECT id FROM permissao WHERE area = 'Almoxarifado' AND controller = 'CatalogoMaterial' AND [action] = 'Index' AND [permissaopai_id] = @ALMOXARIFADOID);
UPDATE permissao SET [permissaopai_id] = @CADASTROID WHERE id = @ID;
-- DepositoMaterial
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, Ordem, permissaopai_id) VALUES ('Index', 'Almoxarifado', 'DepositoMaterial', 'Depósito de material', 1, 1, 0, @CADASTROID);
SET @ID = SCOPE_IDENTITY();
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, Ordem, permissaopai_id) VALUES ('Editar', 'Almoxarifado', 'DepositoMaterial', 'Editar', 0, 1, 0, @ID);
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, Ordem, permissaopai_id) VALUES ('Excluir', 'Almoxarifado', 'DepositoMaterial', 'Excluir', 0, 1, 0, @ID);
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, Ordem, permissaopai_id) VALUES ('Novo', 'Almoxarifado', 'DepositoMaterial', 'Novo', 0, 1, 0, @ID);
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, Ordem, permissaopai_id) VALUES ('EditarSituacao', 'Almoxarifado', 'DepositoMaterial', 'Editar situação', 0, 1, 0, @ID);

-- Movimentação Estoque
-- Novo: Entrada, Saída
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, Ordem, permissaopai_id) VALUES (NULL, 'Almoxarifado', NULL, 'Movimentação Estoque', 1, 1, 0, @ALMOXARIFADOID);
SET @MOVIMENTOID = SCOPE_IDENTITY();
-- EntradaCatalogoMaterial
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, Ordem, permissaopai_id) VALUES ('Index', 'Almoxarifado', 'EntradaCatalogoMaterial', 'Entrada de mercadoria', 1, 1, 0, @MOVIMENTOID);
SET @ID = SCOPE_IDENTITY();
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, Ordem, permissaopai_id) VALUES ('Editar', 'Almoxarifado', 'EntradaCatalogoMaterial', 'Editar', 0, 1, 0, @ID);
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, Ordem, permissaopai_id) VALUES ('Excluir', 'Almoxarifado', 'EntradaCatalogoMaterial', 'Excluir', 0, 1, 0, @ID);
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, Ordem, permissaopai_id) VALUES ('Novo', 'Almoxarifado', 'EntradaCatalogoMaterial', 'Novo', 0, 1, 0, @ID);
-- SaidaCatalogoMaterial
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, Ordem, permissaopai_id) VALUES ('Index', 'Almoxarifado', 'SaidaCatalogoMaterial', 'Saída de mercadoria', 1, 1, 0, @MOVIMENTOID);
SET @ID = SCOPE_IDENTITY();
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, Ordem, permissaopai_id) VALUES ('Editar', 'Almoxarifado', 'SaidaCatalogoMaterial', 'Editar', 0, 1, 0, @ID);
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, Ordem, permissaopai_id) VALUES ('Excluir', 'Almoxarifado', 'SaidaCatalogoMaterial', 'Excluir', 0, 1, 0, @ID);
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, Ordem, permissaopai_id) VALUES ('Novo', 'Almoxarifado', 'SaidaCatalogoMaterial', 'Novo', 0, 1, 0, @ID);
-- Relatório\Consulta
-- Novo: Curva ABC, Extrato do Item, Giro Estoque, Saldo
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, Ordem, permissaopai_id) VALUES (NULL, 'Almoxarifado', NULL, 'Relatório\Consulta', 1, 1, 0, @ALMOXARIFADOID);
SET @CONSULTAID = SCOPE_IDENTITY();
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, Ordem, permissaopai_id) VALUES ('EstoqueMaterial', 'Almoxarifado', 'Consulta', 'Estoque de material', 1, 1, 0, @CONSULTAID);

/* ExecuteEmbeddedSqlScript Fashion.ERP.Migrator.Scripts._201312020551.ExtratoItemEstoqueView.sql */
CREATE VIEW [dbo].[ExtratoItemEstoqueView]
AS
SELECT        ROW_NUMBER() OVER (ORDER BY data) AS id, data, tipomovimentacao, catalogomaterial, qtdentrada, qtdSaida, depositomaterial
FROM            (SELECT        SUM(ei.quantidade) qtdentrada, 0 qtdSaida, ei.catalogomaterial_id AS catalogomaterial, e.depositomaterialdestino_id AS depositomaterial, 
                                                    e.dataentrada AS data, 'e' AS tipomovimentacao
                          FROM            entradaitemcatalogomaterial ei INNER JOIN
                                                    entradacatalogomaterial e ON e.id = ei.entradacatalogomaterial_id
                          GROUP BY ei.catalogomaterial_id, e.depositomaterialdestino_id, e.dataentrada
                          UNION ALL
                          SELECT        0 qtdentrada, SUM(si.quantidade) qtdSaida, si.catalogomaterial_id AS catalogomaterial, s.depositomaterialorigem_id AS depositomaterial, 
                                                   s.datasaida AS data, 's' AS tipomovimentacao
                          FROM            saidaitemcatalogomaterial si INNER JOIN
                                                   saidacatalogomaterial s ON s.id = si.saidacatalogomaterial_id
                          GROUP BY si.catalogomaterial_id, s.depositomaterialorigem_id, s.datasaida) AS extrato

/* ExecuteEmbeddedSqlScript Fashion.ERP.Migrator.Scripts._201312020551.SaldoEstoqueCatalogoMaterial.sql */
CREATE PROCEDURE uspSaldoEstoqueCatalogoMaterial
	@IdCatalogoMaterial bigint,
	@IdDepositoMaterial bigint,
	@Data datetime
AS
BEGIN
	SET NOCOUNT ON;
	
	DECLARE @SaldoAtual float = (SELECT quantidade FROM estoquecatalogomaterial
		WHERE catalogomaterial_id = @IdCatalogoMaterial AND depositomaterial_id = @IdDepositoMaterial);

	DECLARE @TotalEntrada float = (SELECT COALESCE(SUM(ei.quantidade), 0)
		FROM entradaitemcatalogomaterial ei INNER JOIN entradacatalogomaterial e ON e.id = ei.entradacatalogomaterial_id
		WHERE ei.catalogomaterial_id = @IdCatalogoMaterial 
			AND e.depositomaterialdestino_id = @IdDepositoMaterial
			AND e.dataentrada > @Data);

    DECLARE @TotalSaida float = (SELECT COALESCE(SUM(si.quantidade), 0)
		FROM saidaitemcatalogomaterial si INNER JOIN saidacatalogomaterial s ON s.id = si.saidacatalogomaterial_id
		where si.catalogomaterial_id = @IdCatalogoMaterial 
			AND s.depositomaterialorigem_id = @IdDepositoMaterial
			AND s.datasaida > @Data);

	SELECT @SaldoAtual - @TotalEntrada + @TotalSaida;
END

INSERT INTO [dbo].[VersionInfo] ([Version], [AppliedOn], [Description]) VALUES (201312020551, '2015-02-04T16:38:13', 'Migration201312020551')
/* Committing Transaction */
/* 201312020551: Migration201312020551 migrated */

/* 201401091440: Migration201401091440 migrating ============================= */

/* Beginning Transaction */
/* CreateTable programacaobordado */
CREATE TABLE [dbo].[programacaobordado] ([id] BIGINT NOT NULL, [descricao] NVARCHAR(100) NOT NULL, [nomearquivo] NVARCHAR(100), [data] DATE NOT NULL, [quantidadepontos] INT NOT NULL, [quantidadecores] INT NOT NULL, [aplicacao] NVARCHAR(250), [observacao] NVARCHAR(250), [arquivo_id] BIGINT, [modelo_id] BIGINT NOT NULL, [programadorbordado_id] BIGINT NOT NULL, CONSTRAINT [PK_programacaobordado] PRIMARY KEY ([id]))

/* CreateForeignKey FK_programacaobordado_arquivo programacaobordado(arquivo_id) arquivo(id) */
ALTER TABLE [dbo].[programacaobordado] ADD CONSTRAINT [FK_programacaobordado_arquivo] FOREIGN KEY ([arquivo_id]) REFERENCES [dbo].[arquivo] ([id])

/* CreateForeignKey FK_programacaobordado_modelo programacaobordado(modelo_id) modelo(id) */
ALTER TABLE [dbo].[programacaobordado] ADD CONSTRAINT [FK_programacaobordado_modelo] FOREIGN KEY ([modelo_id]) REFERENCES [dbo].[modelo] ([id])

/* CreateForeignKey FK_programacaobordado_programadorbordado programacaobordado(programadorbordado_id) pessoa(id) */
ALTER TABLE [dbo].[programacaobordado] ADD CONSTRAINT [FK_programacaobordado_programadorbordado] FOREIGN KEY ([programadorbordado_id]) REFERENCES [dbo].[pessoa] ([id])

/* ExecuteEmbeddedSqlScript Fashion.ERP.Migrator.Scripts._201401091440.permissao.sql */
-- Atualiza o menu do Modelo
DECLARE @MODELOID AS BIGINT;
SET @MODELOID = (SELECT id FROM permissao WHERE action = 'Index' AND area = 'EngenhariaProduto' AND controller = 'Modelo');

DECLARE @ID AS BIGINT;
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, Ordem, permissaopai_id) VALUES ('ProgramacaoBordado', 'EngenhariaProduto', 'Modelo', 'Programação do bordado', 0, 1, 0, @MODELOID);
SET @ID = SCOPE_IDENTITY()
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, Ordem, permissaopai_id) VALUES ('NovoProgramacaoBordado', 'EngenhariaProduto', 'Modelo', 'Novo', 0, 1, 0, @ID);
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, Ordem, permissaopai_id) VALUES ('EditarProgramacaoBordado', 'EngenhariaProduto', 'Modelo', 'Editar', 0, 1, 0, @ID);
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, Ordem, permissaopai_id) VALUES ('ExcluirProgramacaoBordado', 'EngenhariaProduto', 'Modelo', 'Excluir', 0, 1, 0, @ID);

INSERT INTO [dbo].[VersionInfo] ([Version], [AppliedOn], [Description]) VALUES (201401091440, '2015-02-04T16:38:13', 'Migration201401091440')
/* Committing Transaction */
/* 201401091440: Migration201401091440 migrated */

/* 201401241630: Migration201401241630 migrating ============================= */

/* Beginning Transaction */
/* AlterTable modelo */
/* No SQL statement executed. */

/* CreateColumn modelo observacaoaprovacao String */
ALTER TABLE [dbo].[modelo] ADD [observacaoaprovacao] NVARCHAR(250)

/* ExecuteEmbeddedSqlScript Fashion.ERP.Migrator.Scripts._201401241630.permissao.sql */
-- Modelo
--	1. Manutenção
--	2. Aprovação
--	3. Criação Mix

DECLARE @ENGENHARIAPRODUTOID AS BIGINT, @MODELOID AS BIGINT;
SET @ENGENHARIAPRODUTOID = (SELECT id FROM permissao WHERE area = 'EngenhariaProduto' AND controller IS NULL AND [action] IS NULL AND [permissaopai_id] IS NULL);

-- Cria o submenu Modelo
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, Ordem, permissaopai_id) VALUES (NULL, 'EngenhariaProduto', NULL, 'Modelo', 1, 1, 0, @ENGENHARIAPRODUTOID);
SET @MODELOID = SCOPE_IDENTITY()

-- Atualiza o menu do cadastro do modelo (Manutenção)
UPDATE permissao SET permissaopai_id = @MODELOID, descricao = '1. Manutenção', ExibeNoMenu = 1 WHERE action = 'Index' AND area = 'EngenhariaProduto' AND controller = 'Modelo'

-- Cria o menu Aprovação
DECLARE @ID AS BIGINT;
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, Ordem, permissaopai_id) VALUES ('Index', 'EngenhariaProduto', 'AprovarModelo', '2. Aprovação', 1, 1, 0, @MODELOID);
SET @ID = SCOPE_IDENTITY()
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, Ordem, permissaopai_id) VALUES ('Aprovar', 'EngenhariaProduto', 'AprovarModelo', 'Aprovar', 0, 1, 0, @ID);
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, Ordem, permissaopai_id) VALUES ('Desaprovar', 'EngenhariaProduto', 'AprovarModelo', 'Desaprovar', 0, 1, 0, @ID);

INSERT INTO [dbo].[VersionInfo] ([Version], [AppliedOn], [Description]) VALUES (201401241630, '2015-02-04T16:38:13', 'Migration201401241630')
/* Committing Transaction */
/* 201401241630: Migration201401241630 migrated */

/* 201402031628: Migration201402031628 migrating ============================= */

/* Beginning Transaction */
/* CreateTable prazo */
CREATE TABLE [dbo].[prazo] ([id] BIGINT NOT NULL, [descricao] NVARCHAR(100) NOT NULL, [avista] BIT NOT NULL, [quantidadeparcelas] INT NOT NULL, [prazoprimeiraparcela] INT NOT NULL, [intervalo] INT, [ativo] BIT NOT NULL, [padrao] BIT NOT NULL, CONSTRAINT [PK_prazo] PRIMARY KEY ([id]))

/* DeleteForeignKey FK_modelo_barra modelo ()  () */
ALTER TABLE [dbo].[modelo] DROP CONSTRAINT [FK_modelo_barra]

/* DeleteForeignKey FK_modelo_produtobase modelo ()  () */
ALTER TABLE [dbo].[modelo] DROP CONSTRAINT [FK_modelo_produtobase]

/* DeleteForeignKey FK_modelo_comprimento modelo ()  () */
ALTER TABLE [dbo].[modelo] DROP CONSTRAINT [FK_modelo_comprimento]

/* AlterTable modelo */
/* No SQL statement executed. */

/* AlterColumn modelo barra_id Int64 */
ALTER TABLE [dbo].[modelo] ALTER COLUMN [barra_id] BIGINT

/* CreateForeignKey FK_modelo_barra modelo(barra_id) barra(id) */
ALTER TABLE [dbo].[modelo] ADD CONSTRAINT [FK_modelo_barra] FOREIGN KEY ([barra_id]) REFERENCES [dbo].[barra] ([id])

/* AlterColumn modelo produtobase_id Int64 */
ALTER TABLE [dbo].[modelo] ALTER COLUMN [produtobase_id] BIGINT

/* CreateForeignKey FK_modelo_produtobase modelo(produtobase_id) produtobase(id) */
ALTER TABLE [dbo].[modelo] ADD CONSTRAINT [FK_modelo_produtobase] FOREIGN KEY ([produtobase_id]) REFERENCES [dbo].[produtobase] ([id])

/* AlterColumn modelo comprimento_id Int64 */
ALTER TABLE [dbo].[modelo] ALTER COLUMN [comprimento_id] BIGINT

/* CreateForeignKey FK_modelo_comprimento modelo(comprimento_id) comprimento(id) */
ALTER TABLE [dbo].[modelo] ADD CONSTRAINT [FK_modelo_comprimento] FOREIGN KEY ([comprimento_id]) REFERENCES [dbo].[comprimento] ([id])

/* ExecuteEmbeddedSqlScript Fashion.ERP.Migrator.Scripts._201402031628.permissao.sql */
DECLARE @CADASTROSID AS BIGINT, @ID AS BIGINT;
SET @CADASTROSID = (SELECT id FROM permissao WHERE area = 'Comum' AND controller IS NULL AND [action] IS NULL AND [permissaopai_id] IS NULL);

-- Prazo
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, Ordem, permissaopai_id) VALUES ('Index', 'Comum', 'Prazo', 'Prazo', 1, 1, 0, @CADASTROSID);
SET @ID = SCOPE_IDENTITY()
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, Ordem, permissaopai_id) VALUES ('Novo', 'Comum', 'Prazo', 'Novo', 0, 1, 0, @ID);
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, Ordem, permissaopai_id) VALUES ('Editar', 'Comum', 'Prazo', 'Editar', 0, 1, 0, @ID);
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, Ordem, permissaopai_id) VALUES ('Excluir', 'Comum', 'Prazo', 'Excluir', 0, 1, 0, @ID);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, Ordem, permissaopai_id) VALUES ('EditarSituacao', 'Comum', 'Prazo', 'Editar situação', 0, 1, 0, @ID);

INSERT INTO [dbo].[VersionInfo] ([Version], [AppliedOn], [Description]) VALUES (201402031628, '2015-02-04T16:38:13', 'Migration201402031628')
/* Committing Transaction */
/* 201402031628: Migration201402031628 migrated */

/* 201402191515: Migration201402191515 migrating ============================= */

/* Beginning Transaction */
/* AlterTable modelo */
/* No SQL statement executed. */

/* CreateColumn modelo dificuldade String */
ALTER TABLE [dbo].[modelo] ADD [dificuldade] NVARCHAR(100)

/* CreateColumn modelo quantidademix Int32 */
ALTER TABLE [dbo].[modelo] ADD [quantidademix] INT

/* CreateColumn modelo dataremessaproducao DateTime */
ALTER TABLE [dbo].[modelo] ADD [dataremessaproducao] DATETIME

/* CreateTable classificacaodificuldade */
CREATE TABLE [dbo].[classificacaodificuldade] ([id] BIGINT NOT NULL, [descricao] NVARCHAR(50) NOT NULL, [criacao] BIT NOT NULL, [producao] BIT NOT NULL, [ativo] BIT NOT NULL, CONSTRAINT [PK_classificacaodificuldade] PRIMARY KEY ([id]))

/* ExecuteEmbeddedSqlScript Fashion.ERP.Migrator.Scripts._201402191515.permissao.sql */
DECLARE @CADASTROSID AS BIGINT, @ID AS BIGINT;
SET @CADASTROSID = (SELECT id FROM permissao WHERE area = 'Comum' AND controller IS NULL AND [action] IS NULL AND [permissaopai_id] IS NULL);

-- Prazo
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, Ordem, permissaopai_id) VALUES ('Index', 'Comum', 'ClassificacaoDificuldade', 'Classificação Dificuldade', 1, 1, 0, @CADASTROSID);
SET @ID = SCOPE_IDENTITY()
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, Ordem, permissaopai_id) VALUES ('Novo', 'Comum', 'ClassificacaoDificuldade', 'Novo', 0, 1, 0, @ID);
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, Ordem, permissaopai_id) VALUES ('Editar', 'Comum', 'ClassificacaoDificuldade', 'Editar', 0, 1, 0, @ID);
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, Ordem, permissaopai_id) VALUES ('Excluir', 'Comum', 'ClassificacaoDificuldade', 'Excluir', 0, 1, 0, @ID);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, Ordem, permissaopai_id) VALUES ('EditarSituacao', 'Comum', 'ClassificacaoDificuldade', 'Editar situação', 0, 1, 0, @ID);

INSERT INTO [dbo].[VersionInfo] ([Version], [AppliedOn], [Description]) VALUES (201402191515, '2015-02-04T16:38:13', 'Migration201402191515')
/* Committing Transaction */
/* 201402191515: Migration201402191515 migrated */

/* 201402251151: Migration201402251151 migrating ============================= */

/* Beginning Transaction */
/* CreateTable fichatecnica */
CREATE TABLE [dbo].[fichatecnica] ([id] BIGINT NOT NULL, [referencia] NVARCHAR(50) NOT NULL, [descricao] NVARCHAR(100) NOT NULL, [detalhamento] NVARCHAR(200), [sequencia] INT, [programacaoproducao] DATETIME, [datacadastro] DATETIME NOT NULL, [modelagem] NVARCHAR(100), [quantidadeproducao] INT NOT NULL, [modelo_id] BIGINT NOT NULL, [marca_id] BIGINT NOT NULL, [colecao_id] BIGINT NOT NULL, [barra_id] BIGINT, [segmento_id] BIGINT, [produtobase_id] BIGINT, [comprimento_id] BIGINT, [natureza_id] BIGINT NOT NULL, [classificacaodificuldade_id] BIGINT, [grade_id] BIGINT NOT NULL, CONSTRAINT [PK_fichatecnica] PRIMARY KEY ([id]))

/* CreateForeignKey FK_fichatecnica_modelo fichatecnica(modelo_id) modelo(id) */
ALTER TABLE [dbo].[fichatecnica] ADD CONSTRAINT [FK_fichatecnica_modelo] FOREIGN KEY ([modelo_id]) REFERENCES [dbo].[modelo] ([id])

/* CreateForeignKey FK_fichatecnica_marca fichatecnica(marca_id) marca(id) */
ALTER TABLE [dbo].[fichatecnica] ADD CONSTRAINT [FK_fichatecnica_marca] FOREIGN KEY ([marca_id]) REFERENCES [dbo].[marca] ([id])

/* CreateForeignKey FK_fichatecnica_colecao fichatecnica(colecao_id) colecao(id) */
ALTER TABLE [dbo].[fichatecnica] ADD CONSTRAINT [FK_fichatecnica_colecao] FOREIGN KEY ([colecao_id]) REFERENCES [dbo].[colecao] ([id])

/* CreateForeignKey FK_fichatecnica_barra fichatecnica(barra_id) barra(id) */
ALTER TABLE [dbo].[fichatecnica] ADD CONSTRAINT [FK_fichatecnica_barra] FOREIGN KEY ([barra_id]) REFERENCES [dbo].[barra] ([id])

/* CreateForeignKey FK_fichatecnica_segmento fichatecnica(segmento_id) segmento(id) */
ALTER TABLE [dbo].[fichatecnica] ADD CONSTRAINT [FK_fichatecnica_segmento] FOREIGN KEY ([segmento_id]) REFERENCES [dbo].[segmento] ([id])

/* CreateForeignKey FK_fichatecnica_produtobase fichatecnica(produtobase_id) produtobase(id) */
ALTER TABLE [dbo].[fichatecnica] ADD CONSTRAINT [FK_fichatecnica_produtobase] FOREIGN KEY ([produtobase_id]) REFERENCES [dbo].[produtobase] ([id])

/* CreateForeignKey FK_fichatecnica_comprimento fichatecnica(comprimento_id) comprimento(id) */
ALTER TABLE [dbo].[fichatecnica] ADD CONSTRAINT [FK_fichatecnica_comprimento] FOREIGN KEY ([comprimento_id]) REFERENCES [dbo].[comprimento] ([id])

/* CreateForeignKey FK_fichatecnica_natureza fichatecnica(natureza_id) natureza(id) */
ALTER TABLE [dbo].[fichatecnica] ADD CONSTRAINT [FK_fichatecnica_natureza] FOREIGN KEY ([natureza_id]) REFERENCES [dbo].[natureza] ([id])

/* CreateForeignKey FK_fichatecnica_classificacaodificuldade fichatecnica(classificacaodificuldade_id) classificacaodificuldade(id) */
ALTER TABLE [dbo].[fichatecnica] ADD CONSTRAINT [FK_fichatecnica_classificacaodificuldade] FOREIGN KEY ([classificacaodificuldade_id]) REFERENCES [dbo].[classificacaodificuldade] ([id])

/* CreateForeignKey FK_fichatecnica_grade fichatecnica(grade_id) grade(id) */
ALTER TABLE [dbo].[fichatecnica] ADD CONSTRAINT [FK_fichatecnica_grade] FOREIGN KEY ([grade_id]) REFERENCES [dbo].[grade] ([id])

/* AlterTable modelo */
/* No SQL statement executed. */

/* CreateColumn modelo anoaprovacao Int32 */
ALTER TABLE [dbo].[modelo] ADD [anoaprovacao] INT

/* CreateColumn modelo numeroaprovacao Int32 */
ALTER TABLE [dbo].[modelo] ADD [numeroaprovacao] INT

/* ExecuteEmbeddedSqlScript Fashion.ERP.Migrator.Scripts._201402251151.permissao.sql */
DECLARE @ENGENHARIAPRODUTOID AS BIGINT, @MODELOID AS BIGINT;
SET @ENGENHARIAPRODUTOID = (SELECT id FROM permissao WHERE area = 'EngenhariaProduto' AND controller IS NULL AND [action] IS NULL AND [permissaopai_id] IS NULL);

-- Cria o menu Relatórios
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, Ordem, permissaopai_id) VALUES (NULL, 'EngenhariaProduto', NULL, 'Relatórios', 1, 1, 0, @ENGENHARIAPRODUTOID);
SET @MODELOID = SCOPE_IDENTITY()

-- Listagem Modelos Aprovados
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, Ordem, permissaopai_id) VALUES ('ListagemModelosAprovados', 'EngenhariaProduto', 'Relatorio', 'Listagem Modelos Aprovados', 1, 1, 0, @MODELOID);

-- Consumo Material da Coleção
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, Ordem, permissaopai_id) VALUES ('ConsumoMaterialColecao', 'EngenhariaProduto', 'Relatorio', 'Consumo Material da Coleção', 1, 1, 0, @MODELOID);

/* ExecuteEmbeddedSqlScript Fashion.ERP.Migrator.Scripts._201402251151.ConsumoMaterialColecaoView.sql */
CREATE VIEW [dbo].[ConsumoMaterialColecaoView]
AS
SELECT        ROW_NUMBER() OVER (ORDER BY cm.referencia) AS id, cm.referencia, cm.descricao, um.sigla AS unidade, mcm.quantidade, 0 AS compras, COALESCE (ecm.quantidade, 0) AS estoque, 0 AS diferenca, c.id AS colecao, 
                         scat.id AS subcategoria, cat.id AS categoria, f.id AS familia
FROM            dbo.catalogomaterial AS cm INNER JOIN
                         dbo.materialcomposicaomodelo AS mcm ON cm.id = mcm.catalogomaterial_id INNER JOIN
                         dbo.sequenciaproducao AS sp ON mcm.sequenciaproducao_id = sp.id INNER JOIN
                         dbo.modelo AS m ON sp.modelo_id = m.id INNER JOIN
                         dbo.colecao AS c ON m.colecao_id = c.id LEFT OUTER JOIN
                         dbo.estoquecatalogomaterial AS ecm ON ecm.catalogomaterial_id = cm.id INNER JOIN
                         dbo.unidademedida AS um ON cm.unidademedida_id = um.id INNER JOIN
                         dbo.subcategoria AS scat ON cm.subcategoria_id = scat.id INNER JOIN
                         dbo.categoria AS cat ON scat.categoria_id = cat.id INNER JOIN
                         dbo.familia AS f ON cm.familia_id = f.id

INSERT INTO [dbo].[VersionInfo] ([Version], [AppliedOn], [Description]) VALUES (201402251151, '2015-02-04T16:38:13', 'Migration201402251151')
/* Committing Transaction */
/* 201402251151: Migration201402251151 migrated */

/* 201405200840: Migration201405200840 migrating ============================= */

/* Beginning Transaction */
/* ExecuteEmbeddedSqlScript Fashion.ERP.Migrator.Scripts._201405200840.permissao.sql */
DECLARE @MODELOID AS BIGINT;
SET @MODELOID = (SELECT id FROM permissao WHERE action = 'Index' AND area = 'EngenhariaProduto' AND controller = 'Modelo');

-- Cria o menu Copiar modelo
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, Ordem, permissaopai_id) VALUES ('Copiar', 'EngenhariaProduto', 'Modelo', 'Copiar modelo', 0, 1, 0, @MODELOID);

INSERT INTO [dbo].[VersionInfo] ([Version], [AppliedOn], [Description]) VALUES (201405200840, '2015-02-04T16:38:13', 'Migration201405200840')
/* Committing Transaction */
/* 201405200840: Migration201405200840 migrated */

/* 201405270840: Migration201405270840 migrating ============================= */

/* Beginning Transaction */
/* AlterTable modelo */
/* No SQL statement executed. */

/* CreateColumn modelo dataalteracao DateTime */
ALTER TABLE [dbo].[modelo] ADD [dataalteracao] DATETIME

/* ExecuteSqlStatement UPDATE modelo SET dataalteracao = datacriacao; */
UPDATE modelo SET dataalteracao = datacriacao;

/* AlterTable modelo */
/* No SQL statement executed. */

/* AlterColumn modelo dataalteracao DateTime */
ALTER TABLE [dbo].[modelo] ALTER COLUMN [dataalteracao] DATETIME NOT NULL

/* ExecuteEmbeddedSqlScript Fashion.ERP.Migrator.Scripts._201405270840.ConsumoMaterialColecaoView.sql */
ALTER VIEW [dbo].[ConsumoMaterialColecaoView]
AS
SELECT        ROW_NUMBER() OVER (ORDER BY cm.referencia) AS id, cm.referencia, cm.descricao, um.sigla AS unidade, mcm.quantidade * COALESCE(ft.quantidadeproducao, 1) AS quantidade, 0 AS compras, COALESCE (ecm.quantidade, 0) AS estoque, 0 AS diferenca, 
COALESCE(c2.id, c.id) AS colecao, scat.id AS subcategoria, cat.id AS categoria, f.id AS familia, ft.quantidadeproducao
FROM            dbo.catalogomaterial AS cm INNER JOIN
                         dbo.materialcomposicaomodelo AS mcm ON cm.id = mcm.catalogomaterial_id INNER JOIN
                         dbo.sequenciaproducao AS sp ON mcm.sequenciaproducao_id = sp.id INNER JOIN
                         dbo.modelo AS m ON sp.modelo_id = m.id INNER JOIN
                         dbo.colecao AS c ON m.colecao_id = c.id LEFT OUTER JOIN
                         dbo.estoquecatalogomaterial AS ecm ON ecm.catalogomaterial_id = cm.id INNER JOIN
                         dbo.unidademedida AS um ON mcm.unidademedida_id = um.id INNER JOIN
                         dbo.subcategoria AS scat ON cm.subcategoria_id = scat.id INNER JOIN
                         dbo.categoria AS cat ON scat.categoria_id = cat.id INNER JOIN
                         dbo.familia AS f ON cm.familia_id = f.id LEFT OUTER JOIN
						 dbo.fichatecnica AS ft ON ft.modelo_id = m.id LEFT OUTER JOIN
						 dbo.colecao AS c2 ON c2.id = ft.colecao_id

INSERT INTO [dbo].[VersionInfo] ([Version], [AppliedOn], [Description]) VALUES (201405270840, '2015-02-04T16:38:13', 'Migration201405270840')
/* Committing Transaction */
/* 201405270840: Migration201405270840 migrated */

/* 201405311034: Migration201405311034 migrating ============================= */

/* Beginning Transaction */
/* CreateTable pedidocompra */
CREATE TABLE [dbo].[pedidocompra] ([id] BIGINT NOT NULL, [numero] BIGINT NOT NULL, [datacompra] DATETIME NOT NULL, [previsaofaturamento] DATETIME NOT NULL, [previsaoentrega] DATETIME NOT NULL, [dataentrega] DATETIME, [tipocobrancafrete] NVARCHAR(255) NOT NULL, [valorfrete] DOUBLE PRECISION NOT NULL, [valordesconto] DOUBLE PRECISION NOT NULL, [valorcompra] DOUBLE PRECISION NOT NULL, [observacao] NVARCHAR(4000), [autorizado] BIT NOT NULL, [dataautorizacao] DATETIME, [observacaoautorizacao] NVARCHAR(4000), [situacaocompra] NVARCHAR(255) NOT NULL, [contato] NVARCHAR(50) NOT NULL, [comprador_id] BIGINT NOT NULL, [fornecedor_id] BIGINT NOT NULL, [unidadeestocadora_id] BIGINT NOT NULL, [prazo_id] BIGINT, [meiopagamento_id] BIGINT, CONSTRAINT [PK_pedidocompra] PRIMARY KEY ([id]))

/* CreateForeignKey FK_pedidocompra_comprador pedidocompra(comprador_id) pessoa(id) */
ALTER TABLE [dbo].[pedidocompra] ADD CONSTRAINT [FK_pedidocompra_comprador] FOREIGN KEY ([comprador_id]) REFERENCES [dbo].[pessoa] ([id])

/* CreateForeignKey FK_pedidocompra_fornecedor pedidocompra(fornecedor_id) pessoa(id) */
ALTER TABLE [dbo].[pedidocompra] ADD CONSTRAINT [FK_pedidocompra_fornecedor] FOREIGN KEY ([fornecedor_id]) REFERENCES [dbo].[pessoa] ([id])

/* CreateForeignKey FK_pedidocompra_unidadeestocadora pedidocompra(unidadeestocadora_id) pessoa(id) */
ALTER TABLE [dbo].[pedidocompra] ADD CONSTRAINT [FK_pedidocompra_unidadeestocadora] FOREIGN KEY ([unidadeestocadora_id]) REFERENCES [dbo].[pessoa] ([id])

/* CreateForeignKey FK_pedidocompra_prazo pedidocompra(prazo_id) prazo(id) */
ALTER TABLE [dbo].[pedidocompra] ADD CONSTRAINT [FK_pedidocompra_prazo] FOREIGN KEY ([prazo_id]) REFERENCES [dbo].[prazo] ([id])

/* CreateForeignKey FK_pedidocompra_meiopagamento pedidocompra(meiopagamento_id) meiopagamento(id) */
ALTER TABLE [dbo].[pedidocompra] ADD CONSTRAINT [FK_pedidocompra_meiopagamento] FOREIGN KEY ([meiopagamento_id]) REFERENCES [dbo].[meiopagamento] ([id])

/* CreateTable pedidocompraitem */
CREATE TABLE [dbo].[pedidocompraitem] ([id] BIGINT NOT NULL, [quantidade] DOUBLE PRECISION NOT NULL, [valorunitario] DOUBLE PRECISION NOT NULL, [previsaoentrega] DATETIME, [quantidadeentrega] DOUBLE PRECISION NOT NULL, [dataentrega] DATETIME, [situacaocompra] NVARCHAR(255) NOT NULL, [pedidocompra_id] BIGINT NOT NULL, [catalogomaterial_id] BIGINT NOT NULL, [unidademedida_id] BIGINT NOT NULL, CONSTRAINT [PK_pedidocompraitem] PRIMARY KEY ([id]))

/* CreateForeignKey FK_pedidocompraitem_pedidocompra pedidocompraitem(pedidocompra_id) pedidocompra(id) */
ALTER TABLE [dbo].[pedidocompraitem] ADD CONSTRAINT [FK_pedidocompraitem_pedidocompra] FOREIGN KEY ([pedidocompra_id]) REFERENCES [dbo].[pedidocompra] ([id])

/* CreateForeignKey FK_pedidocompraitem_catalogomaterial pedidocompraitem(catalogomaterial_id) catalogomaterial(id) */
ALTER TABLE [dbo].[pedidocompraitem] ADD CONSTRAINT [FK_pedidocompraitem_catalogomaterial] FOREIGN KEY ([catalogomaterial_id]) REFERENCES [dbo].[catalogomaterial] ([id])

/* CreateForeignKey FK_pedidocompraitem_unidademedida pedidocompraitem(unidademedida_id) unidademedida(id) */
ALTER TABLE [dbo].[pedidocompraitem] ADD CONSTRAINT [FK_pedidocompraitem_unidademedida] FOREIGN KEY ([unidademedida_id]) REFERENCES [dbo].[unidademedida] ([id])

/* ExecuteEmbeddedSqlScript Fashion.ERP.Migrator.Scripts._201405311034.permissao.sql */
DECLARE @COMPRASID AS BIGINT, @BASICOSID AS BIGINT, @MOVIMENTACAOID AS BIGINT, @ID AS BIGINT;
-- COMPRAS
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, ordem, permissaopai_id) VALUES (NULL, 'Compras', NULL, 'Compras', 1, 0, 0, NULL);
SET @COMPRASID = SCOPE_IDENTITY()

-- Basicos
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, ordem, permissaopai_id) VALUES (NULL, 'Compras', NULL, 'Básicos', 1, 0, 0, @COMPRASID);
SET @BASICOSID = SCOPE_IDENTITY()

--	 Fornecedor
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, ordem, permissaopai_id) VALUES ('Index', 'Comum', 'Fornecedor', 'Fornecedor', 1, 1, 0, @BASICOSID);
SET @ID = SCOPE_IDENTITY()
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, ordem, permissaopai_id) VALUES ('Novo', 'Comum', 'Fornecedor', 'Novo', 0, 1, 0, @ID);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, ordem, permissaopai_id) VALUES ('Editar', 'Comum', 'Fornecedor', 'Editar', 0, 1, 0, @ID);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, ordem, permissaopai_id) VALUES ('Excluir', 'Comum', 'Fornecedor', 'Excluir', 0, 1, 0, @ID);

--   Meios Pagamento
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, ordem, permissaopai_id) VALUES ('Index', 'Comum', 'MeioPagamento', 'Meio de pagamento', 1, 1, 0, @BASICOSID);
SET @ID = SCOPE_IDENTITY()
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, ordem, permissaopai_id) VALUES ('Novo', 'Comum', 'MeioPagamento', 'Novo', 0, 1, 0, @ID);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, ordem, permissaopai_id) VALUES ('Editar', 'Comum', 'MeioPagamento', 'Editar', 0, 1, 0, @ID);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, ordem, permissaopai_id) VALUES ('Excluir', 'Comum', 'MeioPagamento', 'Excluir', 0, 1, 0, @ID);

--   Prazo
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, Ordem, permissaopai_id) VALUES ('Index', 'Comum', 'Prazo', 'Prazo', 1, 1, 0, @BASICOSID);
SET @ID = SCOPE_IDENTITY()
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, Ordem, permissaopai_id) VALUES ('Novo', 'Comum', 'Prazo', 'Novo', 0, 1, 0, @ID);
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, Ordem, permissaopai_id) VALUES ('Editar', 'Comum', 'Prazo', 'Editar', 0, 1, 0, @ID);
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, Ordem, permissaopai_id) VALUES ('Excluir', 'Comum', 'Prazo', 'Excluir', 0, 1, 0, @ID);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, Ordem, permissaopai_id) VALUES ('EditarSituacao', 'Comum', 'Prazo', 'Editar situação', 0, 1, 0, @ID);

-- Movimentação Estoque
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, ordem, permissaopai_id) VALUES (NULL, 'Compras', NULL, 'Movimentação Estoque', 1, 0, 0, @COMPRASID);
SET @MOVIMENTACAOID = SCOPE_IDENTITY()

--   Pedido de Compra
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, ordem, permissaopai_id) VALUES ('Index', 'Compras', 'PedidoCompra', 'Pedido de Compra', 1, 1, 0, @MOVIMENTACAOID);
SET @ID = SCOPE_IDENTITY()
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, ordem, permissaopai_id) VALUES ('Novo', 'Compras', 'PedidoCompra', 'Novo', 0, 1, 0, @ID);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, ordem, permissaopai_id) VALUES ('Editar', 'Compras', 'PedidoCompra', 'Editar', 0, 1, 0, @ID);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, ordem, permissaopai_id) VALUES ('Excluir', 'Compras', 'PedidoCompra', 'Excluir', 0, 1, 0, @ID);

INSERT INTO [dbo].[VersionInfo] ([Version], [AppliedOn], [Description]) VALUES (201405311034, '2015-02-04T16:38:13', 'Migration201405311034')
/* Committing Transaction */
/* 201405311034: Migration201405311034 migrated */

/* 201406151118: Migration201406151118 migrating ============================= */

/* Beginning Transaction */
/* CreateTable parametromodulocompra */
CREATE TABLE [dbo].[parametromodulocompra] ([id] BIGINT NOT NULL, [validarecebimentopedido] BIT NOT NULL, CONSTRAINT [PK_parametromodulocompra] PRIMARY KEY ([id]))

/* ExecuteEmbeddedSqlScript Fashion.ERP.Migrator.Scripts._201406151118.permissao.sql */
DECLARE @COMPRASID AS BIGINT, @PARAMETROSID AS BIGINT;
SET @COMPRASID = (SELECT id FROM permissao WHERE area = 'Compras' AND controller IS NULL AND [action] IS NULL AND [permissaopai_id] IS NULL);

-- Parâmetros
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, ordem, permissaopai_id) VALUES (NULL, 'Compras', NULL, 'Parâmetros', 1, 0, 0, @COMPRASID);
SET @PARAMETROSID = SCOPE_IDENTITY()

-- Gestão de Compra
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, ordem, permissaopai_id) VALUES ('Index', 'Compras', 'ParametroModuloCompra', 'Gestão de Compra', 1, 1, 0, @PARAMETROSID);










INSERT INTO [dbo].[VersionInfo] ([Version], [AppliedOn], [Description]) VALUES (201406151118, '2015-02-04T16:38:13', 'Migration201406151118')
/* Committing Transaction */
/* 201406151118: Migration201406151118 migrated */

/* 201406201043: Migration201406201043 migrating ============================= */

/* Beginning Transaction */
/* CreateTable despesaReceita */
CREATE TABLE [dbo].[despesaReceita] ([id] BIGINT NOT NULL, [descricao] NVARCHAR(60) NOT NULL, [ativo] BIT NOT NULL, [tipoDespesaReceita] NVARCHAR(255) NOT NULL, CONSTRAINT [PK_despesaReceita] PRIMARY KEY ([id]))

/* ExecuteEmbeddedSqlScript Fashion.ERP.Migrator.Scripts._201406201043.permissao.sql */
UPDATE permissao SET descricao = 'Básicos' 
WHERE  controller IS NULL AND [action] IS NULL and permissaopai_id is not null and area = 'Financeiro' and descricao = 'Básico';

DECLARE @FINANCEIROID AS BIGINT;
SET @FINANCEIROID = (SELECT id FROM permissao WHERE area = 'Financeiro' AND controller IS NULL AND [action] IS NULL AND [permissaopai_id] IS NULL);

-- Cria o submenu básicos
DECLARE @BASICOSID AS BIGINT;
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, ordem, permissaopai_id) VALUES (NULL, 'Financeiro', NULL, 'Básicos', 1, 0, 0, @FINANCEIROID);
SET @BASICOSID = SCOPE_IDENTITY()

-- Despesa/Receita
DECLARE @ID AS BIGINT;
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, ordem, permissaopai_id) VALUES ('Index', 'Financeiro', 'DespesaReceita', 'Despesa/Receita', 1, 1, 0, @BASICOSID);
SET @ID = SCOPE_IDENTITY()
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, ordem, permissaopai_id) VALUES ('Novo', 'Financeiro', 'DespesaReceita', 'Novo', 0, 1, 0, @ID);
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, ordem, permissaopai_id) VALUES ('Editar', 'Financeiro', 'DespesaReceita', 'Editar', 0, 1, 0, @ID);
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, ordem, permissaopai_id) VALUES ('Excluir', 'Financeiro', 'DespesaReceita', 'Excluir', 0, 1, 0, @ID);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, ordem, permissaopai_id) VALUES ('EditarSituacao', 'Financeiro', 'DespesaReceita', 'Editar situação', 0, 1, 0, @ID);

INSERT INTO [dbo].[VersionInfo] ([Version], [AppliedOn], [Description]) VALUES (201406201043, '2015-02-04T16:38:13', 'Migration201406201043')
/* Committing Transaction */
/* 201406201043: Migration201406201043 migrated */

/* 201406232038: Migration201406232038 migrating ============================= */

/* Beginning Transaction */
/* CreateTable procedimentomodulocompras */
CREATE TABLE [dbo].[procedimentomodulocompras] ([id] BIGINT NOT NULL, [codigo] BIGINT NOT NULL, [descricao] NVARCHAR(60) NOT NULL, CONSTRAINT [PK_procedimentomodulocompras] PRIMARY KEY ([id]))

/* CreateTable procedimentomodulocomprasfuncionario */
CREATE TABLE [dbo].[procedimentomodulocomprasfuncionario] ([procedimentomodulocompras_id] BIGINT NOT NULL, [funcionario_id] BIGINT NOT NULL)

/* CreateForeignKey FK_procedimentomodulocomprasfuncionario_procedimentomodulocompras procedimentomodulocomprasfuncionario(procedimentomodulocompras_id) procedimentomodulocompras(id) */
ALTER TABLE [dbo].[procedimentomodulocomprasfuncionario] ADD CONSTRAINT [FK_procedimentomodulocomprasfuncionario_procedimentomodulocompras] FOREIGN KEY ([procedimentomodulocompras_id]) REFERENCES [dbo].[procedimentomodulocompras] ([id])

/* CreateForeignKey FK_procedimentomodulocomprasfuncionario_funcionario procedimentomodulocomprasfuncionario(funcionario_id) pessoa(id) */
ALTER TABLE [dbo].[procedimentomodulocomprasfuncionario] ADD CONSTRAINT [FK_procedimentomodulocomprasfuncionario_funcionario] FOREIGN KEY ([funcionario_id]) REFERENCES [dbo].[pessoa] ([id])

/* ExecuteEmbeddedSqlScript Fashion.ERP.Migrator.Scripts._201406232038.permissao.sql */
DECLARE @COMPRASID AS BIGINT, @PARAMETROSID AS BIGINT, @ID AS BIGINT;
SET @COMPRASID = (SELECT id FROM permissao WHERE area = 'Compras' AND controller IS NULL AND [action] IS NULL AND [permissaopai_id] IS NULL);

-- Parâmetros
SET @PARAMETROSID = (SELECT id FROM permissao WHERE area = 'Compras' AND descricao = 'Parâmetros' AND controller IS NULL AND [action] IS NULL AND [permissaopai_id] = @COMPRASID);

-- Autorizações
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, ordem, permissaopai_id) VALUES ('Index', 'Compras', 'ProcedimentoModuloCompras', 'Autorizações', 1, 1, 0, @PARAMETROSID);
SET @ID = SCOPE_IDENTITY()
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, ordem, permissaopai_id) VALUES ('Novo', 'Compras', 'ProcedimentoModuloCompras', 'Novo', 0, 1, 0, @ID);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, ordem, permissaopai_id) VALUES ('Editar', 'Compras', 'ProcedimentoModuloCompras', 'Editar', 0, 1, 0, @ID);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, ordem, permissaopai_id) VALUES ('Excluir', 'Compras', 'ProcedimentoModuloCompras', 'Excluir', 0, 1, 0, @ID);

INSERT INTO [dbo].[VersionInfo] ([Version], [AppliedOn], [Description]) VALUES (201406232038, '2015-02-04T16:38:13', 'Migration201406232038')
/* Committing Transaction */
/* 201406232038: Migration201406232038 migrated */

/* 201406240858: Migration201406240858 migrating ============================= */

/* Beginning Transaction */
/* CreateTable motivocancelamentopedidocompra */
CREATE TABLE [dbo].[motivocancelamentopedidocompra] ([id] BIGINT NOT NULL, [descricao] NVARCHAR(60) NOT NULL, [ativo] BIT NOT NULL, CONSTRAINT [PK_motivocancelamentopedidocompra] PRIMARY KEY ([id]))

/* ExecuteEmbeddedSqlScript Fashion.ERP.Migrator.Scripts._201406240858.permissao.sql */
DECLARE @COMPRASID AS BIGINT;
SET @COMPRASID = (SELECT id FROM permissao WHERE area = 'Compras' AND controller IS NULL AND [action] IS NULL AND [permissaopai_id] IS NULL);

DECLARE @BASICOSID AS BIGINT;
SET @BASICOSID = (SELECT id FROM permissao WHERE descricao = 'Básicos' AND area = 'Compras' AND permissaopai_id = @COMPRASID);

-- MotivoCancelamentoPedidoCompra
DECLARE @ID AS BIGINT;
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, ordem, permissaopai_id) VALUES ('Index', 'Compras', 'MotivoCancelamentoPedidoCompra', 'Motivo de Cancelamento do Pedido de Compra', 1, 1, 0, @BASICOSID);
SET @ID = SCOPE_IDENTITY()
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, ordem, permissaopai_id) VALUES ('Novo', 'Compras', 'MotivoCancelamentoPedidoCompra', 'Novo', 0, 1, 0, @ID);
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, ordem, permissaopai_id) VALUES ('Editar', 'Compras', 'MotivoCancelamentoPedidoCompra', 'Editar', 0, 1, 0, @ID);
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, ordem, permissaopai_id) VALUES ('Excluir', 'Compras', 'MotivoCancelamentoPedidoCompra', 'Excluir', 0, 1, 0, @ID);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, ordem, permissaopai_id) VALUES ('EditarSituacao', 'Compras', 'MotivoCancelamentoPedidoCompra', 'Editar situação', 0, 1, 0, @ID);

INSERT INTO [dbo].[VersionInfo] ([Version], [AppliedOn], [Description]) VALUES (201406240858, '2015-02-04T16:38:13', 'Migration201406240858')
/* Committing Transaction */
/* 201406240858: Migration201406240858 migrated */

/* 201406260840: Migration201406260840 migrating ============================= */

/* Beginning Transaction */
/* FluentMigrator.Expressions.DeleteDataExpression */
DELETE FROM [dbo].[procedimentomodulocomprasfuncionario] WHERE 1 = 1

/* FluentMigrator.Expressions.DeleteDataExpression */
DELETE FROM [dbo].[procedimentomodulocompras] WHERE 1 = 1

INSERT INTO [dbo].[VersionInfo] ([Version], [AppliedOn], [Description]) VALUES (201406260840, '2015-02-04T16:38:13', 'Migration201406260840')
/* Committing Transaction */
/* 201406260840: Migration201406260840 migrated */

/* 201406291045: Migration201406291045 migrating ============================= */

/* Beginning Transaction */
/* DeleteForeignKey FK_procedimentomodulocomprasfuncionario_funcionario procedimentomodulocomprasfuncionario ()  () */
ALTER TABLE [dbo].[procedimentomodulocomprasfuncionario] DROP CONSTRAINT [FK_procedimentomodulocomprasfuncionario_funcionario]

/* DeleteForeignKey FK_procedimentomodulocomprasfuncionario_procedimentomodulocompras procedimentomodulocomprasfuncionario ()  () */
ALTER TABLE [dbo].[procedimentomodulocomprasfuncionario] DROP CONSTRAINT [FK_procedimentomodulocomprasfuncionario_procedimentomodulocompras]

/* DeleteTable procedimentomodulocomprasfuncionario */
DROP TABLE [dbo].[procedimentomodulocomprasfuncionario]

/* DeleteTable procedimentomodulocompras */
DROP TABLE [dbo].[procedimentomodulocompras]

/* CreateTable procedimentomodulocompras */
CREATE TABLE [dbo].[procedimentomodulocompras] ([id] BIGINT NOT NULL IDENTITY(1,1), [codigo] BIGINT NOT NULL, [descricao] NVARCHAR(60) NOT NULL, CONSTRAINT [PK_procedimentomodulocompras] PRIMARY KEY ([id]))

/* CreateTable procedimentomodulocomprasfuncionario */
CREATE TABLE [dbo].[procedimentomodulocomprasfuncionario] ([procedimentomodulocompras_id] BIGINT NOT NULL, [funcionario_id] BIGINT NOT NULL)

/* CreateForeignKey FK_procedimentomodulocomprasfuncionario_procedimentomodulocompras procedimentomodulocomprasfuncionario(procedimentomodulocompras_id) procedimentomodulocompras(id) */
ALTER TABLE [dbo].[procedimentomodulocomprasfuncionario] ADD CONSTRAINT [FK_procedimentomodulocomprasfuncionario_procedimentomodulocompras] FOREIGN KEY ([procedimentomodulocompras_id]) REFERENCES [dbo].[procedimentomodulocompras] ([id])

/* CreateForeignKey FK_procedimentomodulocomprasfuncionario_funcionario procedimentomodulocomprasfuncionario(funcionario_id) pessoa(id) */
ALTER TABLE [dbo].[procedimentomodulocomprasfuncionario] ADD CONSTRAINT [FK_procedimentomodulocomprasfuncionario_funcionario] FOREIGN KEY ([funcionario_id]) REFERENCES [dbo].[pessoa] ([id])

INSERT INTO [dbo].[procedimentomodulocompras] ([codigo], [descricao]) VALUES (1, 'Validar pedido de compra'); INSERT INTO [dbo].[procedimentomodulocompras] ([codigo], [descricao]) VALUES (2, 'Atualizar o preço de custo de mercadoria')
/* ExecuteEmbeddedSqlScript Fashion.ERP.Migrator.Scripts._201406291045.permissao.sql */
DECLARE @AUTORIZACOESID AS BIGINT;

-- Autorizações
-- Excluir menu antigo
SET @AUTORIZACOESID = (SELECT id FROM permissao WHERE area = 'Compras' AND controller = 'ProcedimentoModuloCompras' AND [action] = 'Index');
UPDATE permissao SET controller = 'Autorizacoes' WHERE id = @AUTORIZACOESID;
DELETE FROM permissaotousuario WHERE permissao_id in (SELECT id FROM permissao WHERE permissaopai_id = @AUTORIZACOESID);
DELETE FROM permissaotoperfildeacesso WHERE permissao_id in (SELECT id FROM permissao WHERE permissaopai_id = @AUTORIZACOESID);
DELETE FROM permissao WHERE permissaopai_id = @AUTORIZACOESID;

-- Adicionar menu novo
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, ordem, permissaopai_id) VALUES ('Autorizar', 'Compras', 'Autorizacoes', 'Autorizar', 0, 1, 0, @AUTORIZACOESID);

DECLARE @GESTAOCOMPRASID AS BIGINT;
-- Corrigir menu Gestão de Compras
SET @GESTAOCOMPRASID = (SELECT id FROM permissao WHERE [action] IS NULL AND area = 'Compras' AND controller IS NULL AND descricao = 'Movimentação Estoque');
UPDATE permissao SET descricao = 'Gestão de Compras' WHERE id = @GESTAOCOMPRASID;

-- Corrigir descrição Pedido de Compra
UPDATE permissao SET descricao = '1. Pedido de Compra' WHERE [action] = 'Index' AND area = 'Compras' AND controller = 'PedidoCompra' AND descricao = 'Pedido de Compra';

-- Validação pedido compra
DECLARE @ID AS BIGINT;
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, ordem, permissaopai_id) VALUES ('Index', 'Compras', 'ValidaPedidoCompra', '2. Validação da Compra', 1, 1, 0, @GESTAOCOMPRASID);
SET @ID = SCOPE_IDENTITY()
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, ordem, permissaopai_id) VALUES ('Validar', 'Compras', 'ValidaPedidoCompra', 'Validar', 0, 1, 0, @ID);

-- Corrigir Módulo de Compras
UPDATE permissao SET descricao = 'Módulo de Compras' WHERE [action] = 'Index' AND area = 'Compras' AND controller = 'ParametroModuloCompra' AND descricao = 'Gestão de Compra';

--Compras
--	Básicos
--		Fornecedor
--		Meios Pagamento
--		Prazos
--	Cotação
--		Cotação para Compra
--	Gestão de Compras
--		1. Pedido de Compra
--		2. Validação da Compra
--		3. Recebimento de Compra
--		4. Manutenção de Compra
--	Relatório\Consulta
--		Cotação para Compra
--		Pedidos de Compra
--		Posição Pedidos
--	Parâmetros
--		Módulo de Compras
--		Autorizações

/* -> 1 Insert operations completed in 00:00:00.0020014 taking an average of 00:00:00.0020014 */
INSERT INTO [dbo].[VersionInfo] ([Version], [AppliedOn], [Description]) VALUES (201406291045, '2015-02-04T16:38:13', 'Migration201406291045')
/* Committing Transaction */
/* 201406291045: Migration201406291045 migrated */

/* 201406300921: Migration201406300921 migrating ============================= */

/* Beginning Transaction */
/* ExecuteEmbeddedSqlScript Fashion.ERP.Migrator.Scripts._201406300921.permissao.sql */
DECLARE @FINANCEIROID AS BIGINT;
SET @FINANCEIROID = (SELECT id FROM permissao WHERE area = 'Financeiro' AND controller IS NULL AND [action] IS NULL AND [permissaopai_id] IS NULL);

DECLARE @BASICOSID AS BIGINT;
SET @BASICOSID = (SELECT id FROM permissao WHERE descricao = 'Básicos' AND area = 'Financeiro' AND permissaopai_id = @FINANCEIROID);

-- CentroCusto
DECLARE @ID AS BIGINT;
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, ordem, permissaopai_id) VALUES ('Index', 'Comum', 'CentroCusto', 'Centro de Custo', 1, 1, 0, @BASICOSID);
SET @ID = SCOPE_IDENTITY()
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, ordem, permissaopai_id) VALUES ('Novo', 'Comum', 'CentroCusto', 'Novo', 0, 1, 0, @ID);
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, ordem, permissaopai_id) VALUES ('Editar', 'Comum', 'CentroCusto', 'Editar', 0, 1, 0, @ID);
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, ordem, permissaopai_id) VALUES ('Excluir', 'Comum', 'CentroCusto', 'Excluir', 0, 1, 0, @ID);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, ordem, permissaopai_id) VALUES ('EditarSituacao', 'Comum', 'CentroCusto', 'Editar situação', 0, 1, 0, @ID);

INSERT INTO [dbo].[VersionInfo] ([Version], [AppliedOn], [Description]) VALUES (201406300921, '2015-02-04T16:38:13', 'Migration201406300921')
/* Committing Transaction */
/* 201406300921: Migration201406300921 migrated */

/* 201407071228: Migration201407071228 migrating ============================= */

/* Beginning Transaction */
/* CreateTable empresa */
CREATE TABLE [dbo].[empresa] ([id] BIGINT NOT NULL, [codigo] BIGINT NOT NULL, [datacadastro] DATETIME NOT NULL, [dataatualizacao] DATETIME, [ativo] BIT NOT NULL, CONSTRAINT [PK_empresa] PRIMARY KEY ([id]))

/* AlterTable pessoa */
/* No SQL statement executed. */

/* CreateColumn pessoa empresa_id Int64 */
ALTER TABLE [dbo].[pessoa] ADD [empresa_id] BIGINT

/* CreateForeignKey FK_pessoa_empresa pessoa(empresa_id) empresa(id) */
ALTER TABLE [dbo].[pessoa] ADD CONSTRAINT [FK_pessoa_empresa] FOREIGN KEY ([empresa_id]) REFERENCES [dbo].[empresa] ([id])

/* ExecuteEmbeddedSqlScript Fashion.ERP.Migrator.Scripts._201407071228.permissao.sql */
DECLARE @COMUMID AS BIGINT;
SET @COMUMID = (SELECT id FROM permissao WHERE area = 'Comum' AND controller IS NULL AND [action] IS NULL AND [permissaopai_id] IS NULL);

-- Empresa
DECLARE @ID AS BIGINT;
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, ordem, permissaopai_id) VALUES ('Index', 'Comum', 'Empresa', 'Empresa', 1, 1, 0, @COMUMID);
SET @ID = SCOPE_IDENTITY()
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, ordem, permissaopai_id) VALUES ('Novo', 'Comum', 'Empresa', 'Novo', 0, 1, 0, @ID);
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, ordem, permissaopai_id) VALUES ('Editar', 'Comum', 'Empresa', 'Editar', 0, 1, 0, @ID);
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, ordem, permissaopai_id) VALUES ('Excluir', 'Comum', 'Empresa', 'Excluir', 0, 1, 0, @ID);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, ordem, permissaopai_id) VALUES ('EditarSituacao', 'Comum', 'Empresa', 'Editar situação', 0, 1, 0, @ID);

INSERT INTO [dbo].[VersionInfo] ([Version], [AppliedOn], [Description]) VALUES (201407071228, '2015-02-04T16:38:13', 'Migration201407071228')
/* Committing Transaction */
/* 201407071228: Migration201407071228 migrated */

/* 201407111731: Migration201407111731 migrating ============================= */

/* Beginning Transaction */
/* DeleteColumn centrocusto codigo */
DECLARE @default sysname, @sql nvarchar(max);

-- get name of default constraint
SELECT @default = name
FROM sys.default_constraints
WHERE parent_object_id = object_id('[dbo].[centrocusto]')
AND type = 'D'
AND parent_column_id = (
SELECT column_id
FROM sys.columns
WHERE object_id = object_id('[dbo].[centrocusto]')
AND name = 'codigo'
);

-- create alter table command to drop constraint as string and run it
SET @sql = N'ALTER TABLE [dbo].[centrocusto] DROP CONSTRAINT ' + @default;
EXEC sp_executesql @sql;

-- now we can finally drop column
ALTER TABLE [dbo].[centrocusto] DROP COLUMN [codigo];


INSERT INTO [dbo].[VersionInfo] ([Version], [AppliedOn], [Description]) VALUES (201407111731, '2015-02-04T16:38:13', 'Migration201407111731')
/* Committing Transaction */
/* 201407111731: Migration201407111731 migrated */

/* 201407141034: Migration201407141034 migrating ============================= */

/* Beginning Transaction */
/* CreateConstraint cpf_cnpj_uniqueconstraint */
ALTER TABLE [dbo].[pessoa] ADD CONSTRAINT [cpf_cnpj_uniqueconstraint] UNIQUE ([cpfcnpj])

INSERT INTO [dbo].[VersionInfo] ([Version], [AppliedOn], [Description]) VALUES (201407141034, '2015-02-04T16:38:13', 'Migration201407141034')
/* Committing Transaction */
/* 201407141034: Migration201407141034 migrated */

/* 201407151443: Migration201407151443 migrating ============================= */

/* Beginning Transaction */
/* CreateColumn empresa idtenant Int64 */
ALTER TABLE [dbo].[empresa] ADD [idtenant] BIGINT NOT NULL CONSTRAINT [DF_empresa_idtenant] DEFAULT 1

INSERT INTO [dbo].[VersionInfo] ([Version], [AppliedOn], [Description]) VALUES (201407151443, '2015-02-04T16:38:13', 'Migration201407151443')
/* Committing Transaction */
/* 201407151443: Migration201407151443 migrated */

/* 201407171755: Migration201407171755 migrating ============================= */

/* Beginning Transaction */
/* CreateColumn unidade idempresa Int64 */
ALTER TABLE [dbo].[unidade] ADD [idempresa] BIGINT NOT NULL CONSTRAINT [DF_unidade_idempresa] DEFAULT 1

INSERT INTO [dbo].[VersionInfo] ([Version], [AppliedOn], [Description]) VALUES (201407171755, '2015-02-04T16:38:13', 'Migration201407171755')
/* Committing Transaction */
/* 201407171755: Migration201407171755 migrated */

/* 201407190000: Migration201407190000 migrating ============================= */

/* Beginning Transaction */
/* CreateTable ordementradacompra */
CREATE TABLE [dbo].[ordementradacompra] ([id] BIGINT NOT NULL, [numero] BIGINT NOT NULL, [situacaoordementradacompra] NVARCHAR(254) NOT NULL, [data] DATETIME NOT NULL, [dataalteracao] DATETIME NOT NULL, [observacao] NVARCHAR(4000), [unidadeestocadora_id] BIGINT NOT NULL, [comprador_id] BIGINT NOT NULL, [fornecedor_id] BIGINT NOT NULL, CONSTRAINT [PK_ordementradacompra] PRIMARY KEY ([id]))

/* CreateForeignKey FK_ordementradacompra_unidadeestocadora ordementradacompra(unidadeestocadora_id) pessoa(id) */
ALTER TABLE [dbo].[ordementradacompra] ADD CONSTRAINT [FK_ordementradacompra_unidadeestocadora] FOREIGN KEY ([unidadeestocadora_id]) REFERENCES [dbo].[pessoa] ([id])

/* CreateForeignKey FK_ordementradacompra_comprador ordementradacompra(comprador_id) pessoa(id) */
ALTER TABLE [dbo].[ordementradacompra] ADD CONSTRAINT [FK_ordementradacompra_comprador] FOREIGN KEY ([comprador_id]) REFERENCES [dbo].[pessoa] ([id])

/* CreateForeignKey fk_ordementradacompra_fornecedor ordementradacompra(fornecedor_id) pessoa(id) */
ALTER TABLE [dbo].[ordementradacompra] ADD CONSTRAINT [fk_ordementradacompra_fornecedor] FOREIGN KEY ([fornecedor_id]) REFERENCES [dbo].[pessoa] ([id])

/* CreateTable ordementradacomprapedidocompra */
CREATE TABLE [dbo].[ordementradacomprapedidocompra] ([ordementradacompra_id] BIGINT NOT NULL, [pedidocompra_id] BIGINT NOT NULL)

/* CreateForeignKey FK_ordementradacomprapedidocompra_ordementradacompra ordementradacomprapedidocompra(ordementradacompra_id) ordementradacompra(id) */
ALTER TABLE [dbo].[ordementradacomprapedidocompra] ADD CONSTRAINT [FK_ordementradacomprapedidocompra_ordementradacompra] FOREIGN KEY ([ordementradacompra_id]) REFERENCES [dbo].[ordementradacompra] ([id])

/* CreateForeignKey FK_ordementradacomprapedidocompra_pedidocompra ordementradacomprapedidocompra(pedidocompra_id) pedidocompra(id) */
ALTER TABLE [dbo].[ordementradacomprapedidocompra] ADD CONSTRAINT [FK_ordementradacomprapedidocompra_pedidocompra] FOREIGN KEY ([pedidocompra_id]) REFERENCES [dbo].[pedidocompra] ([id])

/* CreateTable ordementradacatalogomaterial */
CREATE TABLE [dbo].[ordementradacatalogomaterial] ([id] BIGINT NOT NULL, [numero] BIGINT NOT NULL, [situacaoordementrada] NVARCHAR(254) NOT NULL, [data] DATETIME NOT NULL, [dataatualizacao] DATETIME NOT NULL, [observacao] NVARCHAR(4000), [comprador_id] BIGINT NOT NULL, CONSTRAINT [PK_ordementradacatalogomaterial] PRIMARY KEY ([id]))

/* CreateForeignKey FK_ordementradacatalogomaterial_comprador ordementradacatalogomaterial(comprador_id) pessoa(id) */
ALTER TABLE [dbo].[ordementradacatalogomaterial] ADD CONSTRAINT [FK_ordementradacatalogomaterial_comprador] FOREIGN KEY ([comprador_id]) REFERENCES [dbo].[pessoa] ([id])

/* CreateTable ordementradaitemcatalogomaterial */
CREATE TABLE [dbo].[ordementradaitemcatalogomaterial] ([id] BIGINT NOT NULL, [quantidade] DOUBLE PRECISION NOT NULL, [pedidocompraitem_id] BIGINT NOT NULL, [ordementradacatalogomaterial_id] BIGINT NOT NULL, CONSTRAINT [PK_ordementradaitemcatalogomaterial] PRIMARY KEY ([id]))

/* CreateForeignKey fk_ordementradaitemcatalogomaterial_pedidocompraitem ordementradaitemcatalogomaterial(pedidocompraitem_id) pedidocompraitem(id) */
ALTER TABLE [dbo].[ordementradaitemcatalogomaterial] ADD CONSTRAINT [fk_ordementradaitemcatalogomaterial_pedidocompraitem] FOREIGN KEY ([pedidocompraitem_id]) REFERENCES [dbo].[pedidocompraitem] ([id])

/* CreateForeignKey fk_ordementradaitemcatalogomaterial_ordementradacatalogomaterial ordementradaitemcatalogomaterial(ordementradacatalogomaterial_id) ordementradacatalogomaterial(id) */
ALTER TABLE [dbo].[ordementradaitemcatalogomaterial] ADD CONSTRAINT [fk_ordementradaitemcatalogomaterial_ordementradacatalogomaterial] FOREIGN KEY ([ordementradacatalogomaterial_id]) REFERENCES [dbo].[ordementradacatalogomaterial] ([id])

/* AlterTable ordementradacompra */
/* No SQL statement executed. */

/* CreateColumn ordementradacompra ordementradacatalogomaterial_id Int64 */
ALTER TABLE [dbo].[ordementradacompra] ADD [ordementradacatalogomaterial_id] BIGINT NOT NULL

/* CreateForeignKey fk_ordementradacompra_ordementradacatalogomaterial ordementradacompra(ordementradacatalogomaterial_id) ordementradacatalogomaterial(id) */
ALTER TABLE [dbo].[ordementradacompra] ADD CONSTRAINT [fk_ordementradacompra_ordementradacatalogomaterial] FOREIGN KEY ([ordementradacatalogomaterial_id]) REFERENCES [dbo].[ordementradacatalogomaterial] ([id])

/* ExecuteEmbeddedSqlScript Fashion.ERP.Migrator.Scripts._201407190000.permissao.sql */
DECLARE @GESTAOCOMPRASID AS BIGINT;
SET @GESTAOCOMPRASID = (SELECT id FROM permissao WHERE [action] IS NULL AND area = 'Compras' AND controller IS NULL AND descricao = 'Gestão de Compras');

-- 3. Recebimento de Compra
DECLARE @ID AS BIGINT;
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, ordem, permissaopai_id) VALUES ('Index', 'Compras', 'OrdemEntradaCompra', '3. Recebimento de Compra', 1, 1, 0, @GESTAOCOMPRASID);
SET @ID = SCOPE_IDENTITY()
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, ordem, permissaopai_id) VALUES ('Novo', 'Compras', 'OrdemEntradaCompra', 'Novo', 0, 1, 0, @ID);
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, ordem, permissaopai_id) VALUES ('Editar', 'Compras', 'OrdemEntradaCompra', 'Editar', 0, 1, 0, @ID);
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, ordem, permissaopai_id) VALUES ('Detalhar', 'Compras', 'OrdemEntradaCompra', 'Detalhar', 0, 1, 0, @ID);

INSERT INTO [dbo].[VersionInfo] ([Version], [AppliedOn], [Description]) VALUES (201407190000, '2015-02-04T16:38:13', 'Migration201407190000')
/* Committing Transaction */
/* 201407190000: Migration201407190000 migrated */

/* 201407211440: Migration201407211440 migrating ============================= */

/* Beginning Transaction */
/* CreateColumn unidade idtenant Int64 */
ALTER TABLE [dbo].[unidade] ADD [idtenant] BIGINT NOT NULL CONSTRAINT [DF_unidade_idtenant] DEFAULT 1

INSERT INTO [dbo].[VersionInfo] ([Version], [AppliedOn], [Description]) VALUES (201407211440, '2015-02-04T16:38:13', 'Migration201407211440')
/* Committing Transaction */
/* 201407211440: Migration201407211440 migrated */

/* 201408011535: Migration201408011535 migrating ============================= */

/* Beginning Transaction */
/* CreateColumn centrocusto codigo Int64 */
ALTER TABLE [dbo].[centrocusto] ADD [codigo] BIGINT NOT NULL CONSTRAINT [DF_centrocusto_codigo] DEFAULT 0

INSERT INTO [dbo].[VersionInfo] ([Version], [AppliedOn], [Description]) VALUES (201408011535, '2015-02-04T16:38:13', 'Migration201408011535')
/* Committing Transaction */
/* 201408011535: Migration201408011535 migrated */

/* 201408041044: Migration201408041044 migrating ============================= */

/* Beginning Transaction */
/* ExecuteEmbeddedSqlScript Fashion.ERP.Migrator.Scripts._201408041044.permissao.sql */

-- Obtem o ID do menu Relatórios

DECLARE @RELATORIOID AS BIGINT;
SET @RELATORIOID = (SELECT ID FROM permissao WHERE area = 'EngenhariaProduto' AND controller IS NULL AND descricao ='Relatórios' AND [action] IS NULL AND [permissaopai_id] IS NOT NULL);

-- Listagem Modelos Aprovados
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, Ordem, permissaopai_id) VALUES ('ListagemModelos', 'EngenhariaProduto', 'Relatorio', 'Listagem de Modelos', 1, 1, 0, @RELATORIOID);


INSERT INTO [dbo].[VersionInfo] ([Version], [AppliedOn], [Description]) VALUES (201408041044, '2015-02-04T16:38:13', 'Migration201408041044')
/* Committing Transaction */
/* 201408041044: Migration201408041044 migrated */

/* 201408061140: Migration201408061140 migrating ============================= */

/* Beginning Transaction */
/* ExecuteEmbeddedSqlScript Fashion.ERP.Migrator.Scripts._201408061140.ConsumoMaterialColecaoView.sql */
ALTER VIEW [dbo].[ConsumoMaterialColecaoView]
AS
SELECT        ROW_NUMBER() OVER (ORDER BY cm.referencia) AS id, cm.referencia, cm.descricao, um.sigla AS unidade, mcm.quantidade * COALESCE (ft.quantidadeproducao, 1) AS quantidade, 0 AS compras, 
COALESCE (ecm.quantidade, 0) AS estoque, 0 AS diferenca,  c.id AS colecao, (select colecao_id from fichatecnica where fichatecnica.id = ft.id) as ColecaoAprovada, scat.id AS subcategoria, cat.id AS categoria, f.id AS familia, ft.quantidadeproducao
FROM            dbo.catalogomaterial AS cm INNER JOIN
                         dbo.materialcomposicaomodelo AS mcm ON cm.id = mcm.catalogomaterial_id INNER JOIN
                         dbo.sequenciaproducao AS sp ON mcm.sequenciaproducao_id = sp.id INNER JOIN
                         dbo.modelo AS m ON sp.modelo_id = m.id INNER JOIN
                         dbo.colecao AS c ON m.colecao_id = c.id LEFT OUTER JOIN
                         dbo.estoquecatalogomaterial AS ecm ON ecm.catalogomaterial_id = cm.id INNER JOIN
                         dbo.unidademedida AS um ON mcm.unidademedida_id = um.id INNER JOIN
                         dbo.subcategoria AS scat ON cm.subcategoria_id = scat.id INNER JOIN
                         dbo.categoria AS cat ON scat.categoria_id = cat.id INNER JOIN
                         dbo.familia AS f ON cm.familia_id = f.id LEFT OUTER JOIN
                         dbo.fichatecnica AS ft ON ft.modelo_id = m.id

INSERT INTO [dbo].[VersionInfo] ([Version], [AppliedOn], [Description]) VALUES (201408061140, '2015-02-04T16:38:13', 'Migration201408061140')
/* Committing Transaction */
/* 201408061140: Migration201408061140 migrated */

/* 201408211313: Migration201408211313 migrating ============================= */

/* Beginning Transaction */
/* ExecuteSqlStatement UPDATE sequenciaproducao SET ordem = ordem - 1 */
UPDATE sequenciaproducao SET ordem = ordem - 1

INSERT INTO [dbo].[VersionInfo] ([Version], [AppliedOn], [Description]) VALUES (201408211313, '2015-02-04T16:38:13', 'Migration201408211313')
/* Committing Transaction */
/* 201408211313: Migration201408211313 migrated */

/* 201409031644: Migration201409031644 migrating ============================= */

/* Beginning Transaction */
/* ExecuteSqlStatement UPDATE permissao
                SET action = 'MaterialComposicaoModelo',
                controller = 'MaterialComposicaoModelo'
                WHERE descricao = 'Materiais de Composição' and area = 'EngenhariaProduto' */
UPDATE permissao
                SET action = 'MaterialComposicaoModelo',
                controller = 'MaterialComposicaoModelo'
                WHERE descricao = 'Materiais de Composição' and area = 'EngenhariaProduto'

/* ExecuteSqlStatement UPDATE permissao
                SET action = 'SequenciaProducao',
                controller = 'SequenciaProducao'
                WHERE descricao = 'Sequência Produção' and area = 'EngenhariaProduto' */
UPDATE permissao
                SET action = 'SequenciaProducao',
                controller = 'SequenciaProducao'
                WHERE descricao = 'Sequência Produção' and area = 'EngenhariaProduto'

INSERT INTO [dbo].[VersionInfo] ([Version], [AppliedOn], [Description]) VALUES (201409031644, '2015-02-04T16:38:13', 'Migration201409031644')
/* Committing Transaction */
/* 201409031644: Migration201409031644 migrated */

/* 201409031649: Migration201409031649 migrating ============================= */

/* Beginning Transaction */
/* CreateTable pedidocompraitemcancelado */
CREATE TABLE [dbo].[pedidocompraitemcancelado] ([id] BIGINT NOT NULL, [data] DATETIME NOT NULL, [quantidadecancelada] DOUBLE PRECISION NOT NULL, [observacao] NVARCHAR(4000), [motivocancelamentopedidocompra_id] BIGINT NOT NULL, CONSTRAINT [PK_pedidocompraitemcancelado] PRIMARY KEY ([id]))

/* CreateForeignKey FK_pedidocompraitemcancelado_motivocancelamentopedidocompra pedidocompraitemcancelado(motivocancelamentopedidocompra_id) motivocancelamentopedidocompra(id) */
ALTER TABLE [dbo].[pedidocompraitemcancelado] ADD CONSTRAINT [FK_pedidocompraitemcancelado_motivocancelamentopedidocompra] FOREIGN KEY ([motivocancelamentopedidocompra_id]) REFERENCES [dbo].[motivocancelamentopedidocompra] ([id])

/* AlterTable pedidocompraitem */
/* No SQL statement executed. */

/* CreateColumn pedidocompraitem pedidocompraitemcancelado_id Int64 */
ALTER TABLE [dbo].[pedidocompraitem] ADD [pedidocompraitemcancelado_id] BIGINT

/* CreateForeignKey FK_pedidocompraitemcancelado_pedidocompraitem pedidocompraitem(pedidocompraitemcancelado_id) pedidocompraitemcancelado(id) */
ALTER TABLE [dbo].[pedidocompraitem] ADD CONSTRAINT [FK_pedidocompraitemcancelado_pedidocompraitem] FOREIGN KEY ([pedidocompraitemcancelado_id]) REFERENCES [dbo].[pedidocompraitemcancelado] ([id])

/* ExecuteEmbeddedSqlScript Fashion.ERP.Migrator.Scripts._201409031649.permissao.sql */
DECLARE @COMPRASID AS BIGINT, @PARAMETROSID AS BIGINT;
SET @COMPRASID = (SELECT id FROM permissao WHERE area = 'Compras' AND controller IS NULL AND [action] IS NULL AND [permissaopai_id] IS NULL);

-- Parâmetros
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, ordem, permissaopai_id) VALUES (NULL, 'Compras', NULL, 'Parâmetros', 1, 0, 0, @COMPRASID);
SET @PARAMETROSID = SCOPE_IDENTITY()

-- Gestão de Compra
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, ordem, permissaopai_id) VALUES ('Index', 'Compras', 'pedidocompracancelamento', 'Gestão de Compra', 1, 1, 0, @PARAMETROSID);

-- Cancelamento de Pedido de Compra
DECLARE @ID AS BIGINT;
SET @ID  = (select id from permissao where action = 'Index' and controller = 'PedidoCompra')
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, ordem, permissaopai_id) VALUES ('CancelamentoPedido', 'Compras', 'PedidoCompraCancelamento', 'Cancelamento', 0, 1, 0, @ID);

INSERT INTO [dbo].[VersionInfo] ([Version], [AppliedOn], [Description]) VALUES (201409031649, '2015-02-04T16:38:14', 'Migration201409031649')
/* Committing Transaction */
/* 201409031649: Migration201409031649 migrated */

/* 201409081504: Migration201409081504 migrating ============================= */

/* Beginning Transaction */
/* ExecuteSqlStatement UPDATE permissao
	            SET controller = 'Material'
	            where controller = 'CatalogoMaterial' */
UPDATE permissao
	            SET controller = 'Material'
	            where controller = 'CatalogoMaterial'

/* ExecuteSqlStatement UPDATE permissao
	            SET descricao = 'Material'
	            where descricao = 'Catálogo de material' */
UPDATE permissao
	            SET descricao = 'Material'
	            where descricao = 'Catálogo de material'

/* ExecuteEmbeddedSqlScript Fashion.ERP.Migrator.Scripts._201409081504.renomeieCatalogoMaterial.sql */
sp_rename 'catalogomaterial','material';
GO
sp_rename 'PK_catalogomaterial','PK_material';
GO
sp_rename 'FK_catalogomaterial_bordado','FK_material_bordado';
GO
sp_rename 'FK_catalogomaterial_familia','FK_material_familia';
GO
sp_rename 'FK_catalogomaterial_foto','FK_material_foto';
GO
sp_rename 'FK_catalogomaterial_generofiscal','FK_material_generofiscal';
GO
sp_rename 'FK_catalogomaterial_marcamaterial','FK_material_marcamaterial';
GO
sp_rename 'FK_catalogomaterial_origemsituacaotributaria','FK_material_origemsituacaotributaria';
GO
sp_rename 'FK_catalogomaterial_subcategoria','FK_material_subcategoria';
GO
sp_rename 'FK_catalogomaterial_tecido','FK_material_tecido';
GO
sp_rename 'FK_catalogomaterial_tipoitem','FK_material_tipoitem';
GO
sp_rename 'FK_catalogomaterial_unidademedida','FK_material_unidademedida';
GO

sp_rename 'entradacatalogomaterial','entradamaterial';
GO
sp_rename 'PK_entradacatalogomaterial','PK_entradamaterial';
GO
sp_rename 'FK_entradacatalogomaterial_depositomaterialdestino','FK_entradamaterial_depositomaterialdestino';
GO
sp_rename 'FK_entradacatalogomaterial_depositomaterialorigem','FK_entradamaterial_depositomaterialorigem';
GO
sp_rename 'FK_entradacatalogomaterial_fornecedor','FK_entradamaterial_fornecedor';
GO

sp_rename 'entradaitemcatalogomaterial','entradaitemmaterial';
GO
sp_rename 'PK_entradaitemcatalogomaterial','PK_entradaitemmaterial';
GO
sp_rename 'FK_entradaitemcatalogomaterial_catalogomaterial','FK_entradaitemmaterial_material';
GO
sp_rename 'FK_entradaitemcatalogomaterial_entradacatalogomaterial','FK_entradaitemmaterial_entradamaterial';
GO
sp_rename 'FK_entradaitemcatalogomaterial_unidademedida','FK_entradaitemmaterial_unidademedida';
GO
sp_rename 'entradaitemmaterial.catalogomaterial_id', 'material_id' , 'COLUMN';
GO
sp_rename 'entradaitemmaterial.entradacatalogomaterial_id', 'entradamaterial_id' , 'COLUMN';
GO

sp_rename 'saidacatalogomaterial','saidamaterial';
GO
sp_rename 'PK_saidacatalogomaterial', 'PK_saidamaterial';
GO
sp_rename 'FK_saidacatalogomaterial_centrocusto','FK_saidamaterial_centrocusto';
GO
sp_rename 'FK_saidacatalogomaterial_depositomaterialdestino','FK_saidamaterial_depositomaterialdestino';
GO
sp_rename 'FK_saidacatalogomaterial_depositomaterialorigem','FK_saidamaterial_depositomaterialorigem';
GO

sp_rename 'saidaitemcatalogomaterial','saidaitemmaterial';
GO
sp_rename 'PK_saidaitemcatalogomaterial', 'PK_saidaitemmaterial';
GO
sp_rename 'saidaitemmaterial.catalogomaterial_id', 'material_id' , 'COLUMN';
GO
sp_rename 'saidaitemmaterial.saidacatalogomaterial_id', 'saidamaterial_id' , 'COLUMN';
GO
sp_rename 'FK_saidaitemcatalogomaterial_catalogomaterial','FK_saidaitemmaterial_material';
GO
sp_rename 'FK_saidaitemcatalogomaterial_saidacatalogomaterial','FK_saidaitemmaterial_saidamaterial';
GO

sp_rename 'estoquecatalogomaterial','estoquematerial';
GO
sp_rename 'PK_estoquecatalogomaterial', 'PK_estoquematerial';
GO
sp_rename 'estoquematerial.catalogomaterial_id', 'material_id' , 'COLUMN';
GO
sp_rename 'FK_estoquecatalogomaterial_catalogomaterial','FK_estoquematerial_material';
GO
sp_rename 'FK_estoquecatalogomaterial_depositomaterial','FK_estoquematerial_depositomaterial';
GO

sp_rename 'pedidocompraitem.catalogomaterial_id', 'material_id' , 'COLUMN';
GO
sp_rename 'FK_pedidocompraitem_catalogomaterial','FK_pedidocompraitem_material';
GO

sp_rename 'ordementradaitemcatalogomaterial','ordementradaitemmaterial';
GO
sp_rename 'PK_ordementradaitemcatalogomaterial', 'PK_ordementradaitemmaterial';
GO
sp_rename 'ordementradaitemmaterial.ordementradacatalogomaterial_id', 'ordementradamaterial_id' , 'COLUMN';
GO
sp_rename 'fk_ordementradaitemcatalogomaterial_ordementradacatalogomaterial','fk_ordementradaitemmaterial_ordementradamaterial';
GO
sp_rename 'fk_ordementradaitemcatalogomaterial_pedidocompraitem','fk_ordementradaitemmaterial_pedidocompraitem';
GO

sp_rename 'materialcomposicaomodelo.catalogomaterial_id', 'material_id' , 'COLUMN';
GO
sp_rename 'FK_materialcomposicaomodelo_catalogomaterial','FK_materialcomposicaomodelo_material';
GO

sp_rename 'ordementradacatalogomaterial', 'ordementradamaterial';
GO
sp_rename 'PK_ordementradacatalogomaterial', 'PK_ordementradamaterial';
GO
sp_rename 'ordementradacompra.ordementradacatalogomaterial_id', 'ordementradamaterial_id' , 'COLUMN';
GO
sp_rename 'fk_ordementradacompra_ordementradacatalogomaterial','fk_ordementradacompra_ordementradamaterial';
GO
sp_rename 'FK_ordementradacatalogomaterial_comprador','FK_ordementradamaterial_comprador';
GO

sp_rename 'referenciaexterna.catalogomaterial_id', 'material_id' , 'COLUMN';
GO
sp_rename 'FK_referenciaexterna_catalogomaterial','FK_referenciaexterna_material';
GO

drop VIEW [dbo].[ConsumoMaterialColecaoView];
GO
CREATE VIEW [dbo].[ConsumoMaterialColecaoView]
AS
SELECT        ROW_NUMBER() OVER (ORDER BY cm.referencia) AS id, cm.referencia, cm.descricao, um.sigla AS unidade, mcm.quantidade * COALESCE (ft.quantidadeproducao, 1) AS quantidade, 0 AS compras, 
COALESCE (ecm.quantidade, 0) AS estoque, 0 AS diferenca,  c.id AS colecao, (select colecao_id from fichatecnica where fichatecnica.id = ft.id) as ColecaoAprovada, scat.id AS subcategoria, cat.id AS categoria, f.id AS familia, ft.quantidadeproducao
FROM            dbo.material AS cm INNER JOIN
                         dbo.materialcomposicaomodelo AS mcm ON cm.id = mcm.material_id INNER JOIN
                         dbo.sequenciaproducao AS sp ON mcm.sequenciaproducao_id = sp.id INNER JOIN
                         dbo.modelo AS m ON sp.modelo_id = m.id INNER JOIN
                         dbo.colecao AS c ON m.colecao_id = c.id LEFT OUTER JOIN
                         dbo.estoquematerial AS ecm ON ecm.material_id = cm.id INNER JOIN
                         dbo.unidademedida AS um ON mcm.unidademedida_id = um.id INNER JOIN
                         dbo.subcategoria AS scat ON cm.subcategoria_id = scat.id INNER JOIN
                         dbo.categoria AS cat ON scat.categoria_id = cat.id INNER JOIN
                         dbo.familia AS f ON cm.familia_id = f.id LEFT OUTER JOIN
                         dbo.fichatecnica AS ft ON ft.modelo_id = m.id

GO

drop VIEW [dbo].[ExtratoItemEstoqueView];
GO
CREATE VIEW [dbo].[ExtratoItemEstoqueView]
AS
SELECT        ROW_NUMBER() OVER (ORDER BY data) AS id, data, tipomovimentacao, material, qtdentrada, qtdSaida, depositomaterial
FROM            (SELECT        SUM(ei.quantidade) qtdentrada, 0 qtdSaida, ei.material_id AS material, e.depositomaterialdestino_id AS depositomaterial, 
                                                    e.dataentrada AS data, 'e' AS tipomovimentacao
                          FROM            entradaitemmaterial ei INNER JOIN
                                                    entradamaterial e ON e.id = ei.entradamaterial_id
                          GROUP BY ei.material_id, e.depositomaterialdestino_id, e.dataentrada
                          UNION ALL
                          SELECT        0 qtdentrada, SUM(si.quantidade) qtdSaida, si.material_id AS material, s.depositomaterialorigem_id AS depositomaterial, 
                                                   s.datasaida AS data, 's' AS tipomovimentacao
                          FROM            saidaitemmaterial si INNER JOIN
                                                   saidamaterial s ON s.id = si.saidamaterial_id
                          GROUP BY si.material_id, s.depositomaterialorigem_id, s.datasaida) AS extrato

GO

Drop PROCEDURE [dbo].[uspSaldoEstoqueCatalogoMaterial];
GO
CREATE PROCEDURE [dbo].[uspSaldoEstoquematerial]
	@Idmaterial bigint,
	@IdDepositoMaterial bigint,
	@Data datetime
AS
BEGIN
	SET NOCOUNT ON;
	
	DECLARE @SaldoAtual float = (SELECT quantidade FROM estoquematerial
		WHERE material_id = @Idmaterial AND depositomaterial_id = @IdDepositoMaterial);
	DECLARE @TotalEntrada float = (SELECT COALESCE(SUM(ei.quantidade), 0)
		FROM entradaitemmaterial ei INNER JOIN entradamaterial e ON e.id = ei.entradamaterial_id
		WHERE ei.material_id = @Idmaterial 
			AND e.depositomaterialdestino_id = @IdDepositoMaterial
			AND e.dataentrada > @Data);
    DECLARE @TotalSaida float = (SELECT COALESCE(SUM(si.quantidade), 0)
		FROM saidaitemmaterial si INNER JOIN saidamaterial s ON s.id = si.saidamaterial_id
		where si.material_id = @Idmaterial 
			AND s.depositomaterialorigem_id = @IdDepositoMaterial
			AND s.datasaida > @Data);
	SELECT @SaldoAtual - @TotalEntrada + @TotalSaida;
END

GO

INSERT INTO [dbo].[VersionInfo] ([Version], [AppliedOn], [Description]) VALUES (201409081504, '2015-02-04T16:38:14', 'Migration201409081504')
/* Committing Transaction */
/* 201409081504: Migration201409081504 migrated */

/* 201409091432: Migration201409091432 migrating ============================= */

/* Beginning Transaction */
/* ExecuteSqlStatement 
                DECLARE @id int
                select @id = id from permissao where action = 'Detalhar' and controller = 'Modelo'
                UPDATE permissao
	                SET permissaopai_id = @id, descricao = 'Sequência de Produção'
	                WHERE descricao = 'Sequência Produção' and area = 'EngenhariaProduto' */

                DECLARE @id int
                select @id = id from permissao where action = 'Detalhar' and controller = 'Modelo'
                UPDATE permissao
	                SET permissaopai_id = @id, descricao = 'Sequência de Produção'
	                WHERE descricao = 'Sequência Produção' and area = 'EngenhariaProduto'

/* ExecuteSqlStatement 
                declare @id int

                select @id = id from permissao where action = 'Detalhar' and controller = 'Modelo'
                UPDATE permissao
	                SET permissaopai_id = @id
	                WHERE ACTION = 'materialcomposicaomodelo' and area = 'EngenhariaProduto' */

                declare @id int

                select @id = id from permissao where action = 'Detalhar' and controller = 'Modelo'
                UPDATE permissao
	                SET permissaopai_id = @id
	                WHERE ACTION = 'materialcomposicaomodelo' and area = 'EngenhariaProduto'

INSERT INTO [dbo].[VersionInfo] ([Version], [AppliedOn], [Description]) VALUES (201409091432, '2015-02-04T16:38:14', 'Migration201409091432')
/* Committing Transaction */
/* 201409091432: Migration201409091432 migrated */

/* 201409100907: Migration201409100907 migrating ============================= */

/* Beginning Transaction */
/* ExecuteEmbeddedSqlScript Fashion.ERP.Migrator.Scripts._201409100907.createtabelasconferencia.sql */
ALTER TABLE ordementradacompra drop constraint fk_ordementradacompra_ordementradamaterial
drop table ordementradaitemmaterial
drop table ordementradamaterial

CREATE TABLE ConferenciaEntradaMaterial(
	id bigint NOT NULL,
	numero bigint NOT NULL,	
	data datetime NOT NULL,
	dataatualizacao datetime NOT NULL,
	observacao nvarchar(4000) NULL,
	comprador_id bigint NOT NULL,
	autorizado bit NOT NULL,
	conferido bit NOT NULL,
	CONSTRAINT PK_conferenicaentradamaterial PRIMARY KEY (id)
)
GO
ALTER TABLE conferenciaentradamaterial ADD  CONSTRAINT FK_conferenicaentradamaterial_comprador FOREIGN KEY(comprador_id) REFERENCES pessoa (id)
GO

CREATE TABLE conferenciaentradamaterialitem(
	id bigint NOT NULL,
	quantidade float NOT NULL,
	quantidadeconferida float NOT NULL,
	situacaoconferencia nvarchar (254) NOT NULL,
	unidademedida_id bigint NOT NULL,
	material_id bigint NOT NULL,
	conferenciaentradamaterial_id bigint NOT NULL,

CONSTRAINT PK_conferenciaentradamaterialitem PRIMARY KEY (id)
)
GO

ALTER TABLE conferenciaentradamaterialitem  ADD  CONSTRAINT fk_conferenciaentradamaterialitem_conferenciaentradamaterial
FOREIGN KEY(conferenciaentradamaterial_id) REFERENCES conferenciaentradamaterial (id)
GO

ALTER TABLE conferenciaentradamaterialitem  ADD  CONSTRAINT fk_conferenciaentradamaterialitem_material
FOREIGN KEY(material_id) REFERENCES material (id)
GO

ALTER TABLE conferenciaentradamaterialitem  ADD  CONSTRAINT fk_conferenciaentradamaterialitem_unidademedida
FOREIGN KEY(unidademedida_id) REFERENCES unidademedida (id)
GO

INSERT INTO [dbo].[VersionInfo] ([Version], [AppliedOn], [Description]) VALUES (201409100907, '2015-02-04T16:38:14', 'Migration201409100907')
/* Committing Transaction */
/* 201409100907: Migration201409100907 migrated */

/* 201409101646: Migration201409101646 migrating ============================= */

/* Beginning Transaction */
/* ExecuteSqlStatement 
                DECLARE @id int
                SELECT @id = id from permissao where action = 'Index' and controller = 'PedidoCompra'
                UPDATE permissao
	                SET permissaopai_id = @id, descricao = 'Validação'
	                WHERE action = 'Validar' and area = 'Compras'
		        
                DELETE FROM permissaotousuario where permissao_id in (
                    SELECT id from permissao WHERE descricao = '2. Validação da Compra' and area = 'Compras')
                DELETE FROM permissao WHERE descricao = '2. Validação da Compra' and area = 'Compras'	
                
                UPDATE permissao
	                SET descricao = 'Pedido de Compra'
	                WHERE action = 'Index' and area = 'Compras' and controller = 'PedidoCompra'
                UPDATE permissao
	                SET descricao = 'Recebimento de Compra'
	                WHERE descricao = '3. Recebimento de Compra' */

                DECLARE @id int
                SELECT @id = id from permissao where action = 'Index' and controller = 'PedidoCompra'
                UPDATE permissao
	                SET permissaopai_id = @id, descricao = 'Validação'
	                WHERE action = 'Validar' and area = 'Compras'
		        
                DELETE FROM permissaotousuario where permissao_id in (
                    SELECT id from permissao WHERE descricao = '2. Validação da Compra' and area = 'Compras')
                DELETE FROM permissao WHERE descricao = '2. Validação da Compra' and area = 'Compras'	
                
                UPDATE permissao
	                SET descricao = 'Pedido de Compra'
	                WHERE action = 'Index' and area = 'Compras' and controller = 'PedidoCompra'
                UPDATE permissao
	                SET descricao = 'Recebimento de Compra'
	                WHERE descricao = '3. Recebimento de Compra'

INSERT INTO [dbo].[VersionInfo] ([Version], [AppliedOn], [Description]) VALUES (201409101646, '2015-02-04T16:38:14', 'Migration201409101646')
/* Committing Transaction */
/* 201409101646: Migration201409101646 migrated */

/* 201409121005: Migration201409121005 migrating ============================= */

/* Beginning Transaction */
/* AlterTable modelo */
/* No SQL statement executed. */

/* CreateColumn modelo dataprevisaoenvio DateTime */
ALTER TABLE [dbo].[modelo] ADD [dataprevisaoenvio] DATETIME

/* DeleteColumn pedidocompra dataentrega */
DECLARE @default sysname, @sql nvarchar(max);

-- get name of default constraint
SELECT @default = name
FROM sys.default_constraints
WHERE parent_object_id = object_id('[dbo].[pedidocompra]')
AND type = 'D'
AND parent_column_id = (
SELECT column_id
FROM sys.columns
WHERE object_id = object_id('[dbo].[pedidocompra]')
AND name = 'dataentrega'
);

-- create alter table command to drop constraint as string and run it
SET @sql = N'ALTER TABLE [dbo].[pedidocompra] DROP CONSTRAINT ' + @default;
EXEC sp_executesql @sql;

-- now we can finally drop column
ALTER TABLE [dbo].[pedidocompra] DROP COLUMN [dataentrega];


/* ExecuteEmbeddedSqlScript Fashion.ERP.Migrator.Scripts._201409121005.createrecebimentocompra.sql */
drop table ordementradacomprapedidocompra
drop table ordementradacompra

--Recebimento da Compra
CREATE TABLE recebimentocompra(
	id bigint NOT NULL,
	numero bigint NOT NULL,	
	data datetime NOT NULL,
	dataalteracao datetime NOT NULL,
	observacao nvarchar(4000) NULL,
	valor float NOT NULL,
	--comprador_id bigint NOT NULL,
	fornecedor_id bigint NOT NULL,
	unidade_id bigint NOT NULL,
	situacaorecebimentocompra nvarchar(400) NOT NULL,
	CONSTRAINT PK_recebimentocompra PRIMARY KEY (id)
)
GO

--ALTER TABLE recebimentocompra ADD CONSTRAINT FK_recebimentocompra_comprador FOREIGN KEY(comprador_id) REFERENCES pessoa (id)
--GO
ALTER TABLE recebimentocompra ADD CONSTRAINT FK_recebimentocompra_fornecedor FOREIGN KEY(fornecedor_id) REFERENCES pessoa (id)
GO
ALTER TABLE recebimentocompra ADD CONSTRAINT FK_recebimentocompra_unidade FOREIGN KEY(unidade_id) REFERENCES pessoa (id)
GO

-- Tabela de relacionamento recebimentocompra e pedidocompra
CREATE TABLE recebimentocomprapedidocompra(	
	pedidocompra_id bigint NOT NULL,
	recebimentocompra_id bigint NOT NULL) 
GO    
	
ALTER TABLE recebimentocomprapedidocompra ADD CONSTRAINT FK_recebimentocomprapedidocompra_recebimentocompra FOREIGN KEY(recebimentocompra_id)
REFERENCES recebimentocompra (id)
GO

ALTER TABLE recebimentocomprapedidocompra ADD CONSTRAINT FK_recebimentocomprapedidocompra_pedidocompra FOREIGN KEY(pedidocompra_id)
REFERENCES pedidocompra (id)
GO

-- Recebimento Compra Item
CREATE TABLE recebimentocompraitem(
	id bigint NOT NULL,
	quantidade float NOT NULL,
	custo float NOT NULL,
	valortotal float NOT NULL,	
	material_id bigint NOT NULL,
	recebimentocompra_id bigint NOT NULL,
	CONSTRAINT PK_recebimentocompraitem PRIMARY KEY (id)
)
GO

ALTER TABLE recebimentocompraitem  ADD  CONSTRAINT fk_conferenciaentradamaterialitem_recebimentocompra
FOREIGN KEY(recebimentocompra_id) REFERENCES recebimentocompra (id)
GO

ALTER TABLE recebimentocompraitem  ADD  CONSTRAINT fk_recebimentocompraitem_material
FOREIGN KEY(material_id) REFERENCES material (id)
GO

ALTER TABLE conferenciaentradamaterial ADD recebimentocompra_id bigint;

ALTER TABLE conferenciaentradamaterial ADD CONSTRAINT FK_conferenciaentradamaterial_recebimentocompra FOREIGN KEY(recebimentocompra_id)
REFERENCES recebimentocompra (id)
GO

-- Tabela de DetalhamentoRecebimentoCompraItem
CREATE TABLE detalhamentorecebimentocompraitem (	
	quantidade float NOT NULL,
	pedidocompraitem_id bigint NOT NULL,
	pedidocompra_id bigint NOT NULL,
	recebimentocompra_id bigint NOT NULL)
GO    
	
ALTER TABLE detalhamentorecebimentocompraitem ADD CONSTRAINT FK_detalhamentorecebimentocompraitem_pedidocompra FOREIGN KEY(pedidocompra_id)
REFERENCES pedidocompra (id)
GO

ALTER TABLE detalhamentorecebimentocompraitem ADD CONSTRAINT FK_detalhamentorecebimentocompraitem_recebimentocompra FOREIGN KEY(recebimentocompra_id)
REFERENCES recebimentocompra (id)
GO

ALTER TABLE detalhamentorecebimentocompraitem ADD CONSTRAINT FK_detalhamentorecebimentocompraitem_pedidocompraitem FOREIGN KEY(pedidocompraitem_id)
REFERENCES pedidocompraitem (id)
GO

INSERT INTO [dbo].[VersionInfo] ([Version], [AppliedOn], [Description]) VALUES (201409121005, '2015-02-04T16:38:14', 'Migration201409121005')
/* Committing Transaction */
/* 201409121005: Migration201409121005 migrated */

/* 201410031432: Migration201410031432 migrating ============================= */

/* Beginning Transaction */
/* ExecuteEmbeddedSqlScript Fashion.ERP.Migrator.Scripts._201410031432.atualizeViewConsumoMaterial.sql */

drop VIEW [dbo].[ConsumoMaterialColecaoView];
GO
CREATE VIEW [dbo].[ConsumoMaterialColecaoView]
AS
SELECT        ROW_NUMBER() OVER (ORDER BY cm.referencia) AS id, cm.referencia, cm.descricao, 
um.sigla AS unidade, mcm.quantidade * COALESCE (ft.quantidadeproducao, 1) AS quantidade, 0 AS compras, 
COALESCE (ecm.quantidade, 0) AS estoque, 0 AS diferenca, c.id AS colecao, m.dataprevisaoenvio, 
    (SELECT        colecao_id
      FROM            fichatecnica
      WHERE        fichatecnica.id = ft.id) AS ColecaoAprovada, scat.id AS subcategoria, cat.id AS categoria, f.id AS familia, ft.quantidadeproducao
FROM            dbo.material AS cm INNER JOIN
                         dbo.materialcomposicaomodelo AS mcm ON cm.id = mcm.material_id INNER JOIN
                         dbo.sequenciaproducao AS sp ON mcm.sequenciaproducao_id = sp.id INNER JOIN
                         dbo.modelo AS m ON sp.modelo_id = m.id INNER JOIN
                         dbo.colecao AS c ON m.colecao_id = c.id LEFT OUTER JOIN
                         dbo.estoquematerial AS ecm ON ecm.material_id = cm.id INNER JOIN
                         dbo.unidademedida AS um ON mcm.unidademedida_id = um.id INNER JOIN
                         dbo.subcategoria AS scat ON cm.subcategoria_id = scat.id INNER JOIN
                         dbo.categoria AS cat ON scat.categoria_id = cat.id INNER JOIN
                         dbo.familia AS f ON cm.familia_id = f.id LEFT OUTER JOIN
                         dbo.fichatecnica AS ft ON ft.modelo_id = m.id

GO


INSERT INTO [dbo].[VersionInfo] ([Version], [AppliedOn], [Description]) VALUES (201410031432, '2015-02-04T16:38:14', 'Migration201410031432')
/* Committing Transaction */
/* 201410031432: Migration201410031432 migrated */

/* 201410081556: Migration201410081556 migrating ============================= */

/* Beginning Transaction */
/* AlterTable detalhamentorecebimentocompraitem */
/* No SQL statement executed. */

/* CreateColumn detalhamentorecebimentocompraitem id Int64 */
ALTER TABLE [dbo].[detalhamentorecebimentocompraitem] ADD [id] BIGINT NOT NULL

/* AlterTable detalhamentorecebimentocompraitem */
/* No SQL statement executed. */

/* CreateColumn detalhamentorecebimentocompraitem recebimentocompraitem_id Int64 */
ALTER TABLE [dbo].[detalhamentorecebimentocompraitem] ADD [recebimentocompraitem_id] BIGINT

/* CreateForeignKey FK_detalhamentorecebimentocompraitem_recebimentocompraitem detalhamentorecebimentocompraitem(recebimentocompraitem_id) recebimentocompraitem(id) */
ALTER TABLE [dbo].[detalhamentorecebimentocompraitem] ADD CONSTRAINT [FK_detalhamentorecebimentocompraitem_recebimentocompraitem] FOREIGN KEY ([recebimentocompraitem_id]) REFERENCES [dbo].[recebimentocompraitem] ([id])

/* ExecuteEmbeddedSqlScript Fashion.ERP.Migrator.Scripts._201410081556.atualizerecebimentocompra.sql */
sp_rename 'recebimentocompraitem.custo', 'valorunitario' , 'COLUMN';
GO

INSERT INTO [dbo].[VersionInfo] ([Version], [AppliedOn], [Description]) VALUES (201410081556, '2015-02-04T16:38:14', 'Migration201410081556')
/* Committing Transaction */
/* 201410081556: Migration201410081556 migrated */

/* 201410142157: Migration201410142157 migrating ============================= */

/* Beginning Transaction */
/* CreateTable titulopagar */
CREATE TABLE [dbo].[titulopagar] ([id] BIGINT NOT NULL, [idtenant] BIGINT NOT NULL, [idempresa] BIGINT NOT NULL, [numero] NVARCHAR(100) NOT NULL, [parcela] INT NOT NULL, [plano] INT NOT NULL, [emissao] DATETIME NOT NULL, [vencimento] DATETIME NOT NULL, [prorrogacao] DATETIME NOT NULL, [valor] DOUBLE PRECISION NOT NULL, [saldodevedor] DOUBLE PRECISION NOT NULL, [historico] NVARCHAR(100), [observacao] NVARCHAR(4000), [provisorio] BIT NOT NULL, [situacaotitulo] NVARCHAR(255) NOT NULL, [datacadastro] DATETIME NOT NULL, [dataalteracao] DATETIME NOT NULL, [fornecedor_id] BIGINT NOT NULL, [unidade_id] BIGINT NOT NULL, [banco_id] BIGINT NOT NULL, CONSTRAINT [PK_titulopagar] PRIMARY KEY ([id]))

/* CreateForeignKey FK_titulopagar_fornecedor titulopagar(fornecedor_id) pessoa(id) */
ALTER TABLE [dbo].[titulopagar] ADD CONSTRAINT [FK_titulopagar_fornecedor] FOREIGN KEY ([fornecedor_id]) REFERENCES [dbo].[pessoa] ([id])

/* CreateForeignKey FK_titulopagar_unidade titulopagar(unidade_id) pessoa(id) */
ALTER TABLE [dbo].[titulopagar] ADD CONSTRAINT [FK_titulopagar_unidade] FOREIGN KEY ([unidade_id]) REFERENCES [dbo].[pessoa] ([id])

/* CreateForeignKey FK_titulopagar_banco titulopagar(banco_id) banco(id) */
ALTER TABLE [dbo].[titulopagar] ADD CONSTRAINT [FK_titulopagar_banco] FOREIGN KEY ([banco_id]) REFERENCES [dbo].[banco] ([id])

/* CreateTable titulopagarbaixa */
CREATE TABLE [dbo].[titulopagarbaixa] ([id] BIGINT NOT NULL, [idtenant] BIGINT NOT NULL, [idempresa] BIGINT NOT NULL, [numerobaixa] BIGINT NOT NULL, [datapagamento] DATETIME NOT NULL, [dataalteracao] DATETIME NOT NULL, [juros] DOUBLE PRECISION NOT NULL, [descontos] DOUBLE PRECISION NOT NULL, [despesas] DOUBLE PRECISION NOT NULL, [valor] DOUBLE PRECISION NOT NULL, [historico] NVARCHAR(100), [observacao] NVARCHAR(4000), [titulopagar_id] BIGINT NOT NULL, CONSTRAINT [PK_titulopagarbaixa] PRIMARY KEY ([id]))

/* CreateForeignKey FK_titulopagarbaixa_titulopagar titulopagarbaixa(titulopagar_id) titulopagar(id) */
ALTER TABLE [dbo].[titulopagarbaixa] ADD CONSTRAINT [FK_titulopagarbaixa_titulopagar] FOREIGN KEY ([titulopagar_id]) REFERENCES [dbo].[titulopagar] ([id])

/* CreateTable rateiodespesareceita */
CREATE TABLE [dbo].[rateiodespesareceita] ([id] BIGINT NOT NULL, [idtenant] BIGINT NOT NULL, [idempresa] BIGINT NOT NULL, [valor] DOUBLE PRECISION NOT NULL, [titulopagar_id] BIGINT NOT NULL, [despesareceita_id] BIGINT NOT NULL, CONSTRAINT [PK_rateiodespesareceita] PRIMARY KEY ([id]))

/* CreateForeignKey FK_rateiodespesareceita_titulopagar rateiodespesareceita(titulopagar_id) titulopagar(id) */
ALTER TABLE [dbo].[rateiodespesareceita] ADD CONSTRAINT [FK_rateiodespesareceita_titulopagar] FOREIGN KEY ([titulopagar_id]) REFERENCES [dbo].[titulopagar] ([id])

/* CreateForeignKey FK_rateiodespesareceita_despesareceita rateiodespesareceita(despesareceita_id) despesareceita(id) */
ALTER TABLE [dbo].[rateiodespesareceita] ADD CONSTRAINT [FK_rateiodespesareceita_despesareceita] FOREIGN KEY ([despesareceita_id]) REFERENCES [dbo].[despesareceita] ([id])

/* CreateTable rateiocentrocusto */
CREATE TABLE [dbo].[rateiocentrocusto] ([id] BIGINT NOT NULL, [idtenant] BIGINT NOT NULL, [idempresa] BIGINT NOT NULL, [valor] DOUBLE PRECISION NOT NULL, [titulopagar_id] BIGINT NOT NULL, [centrocusto_id] BIGINT NOT NULL, CONSTRAINT [PK_rateiocentrocusto] PRIMARY KEY ([id]))

/* CreateForeignKey FK_rateiocentrocusto_titulopagar rateiocentrocusto(titulopagar_id) titulopagar(id) */
ALTER TABLE [dbo].[rateiocentrocusto] ADD CONSTRAINT [FK_rateiocentrocusto_titulopagar] FOREIGN KEY ([titulopagar_id]) REFERENCES [dbo].[titulopagar] ([id])

/* CreateForeignKey FK_rateiocentrocusto_centrocusto rateiocentrocusto(centrocusto_id) centrocusto(id) */
ALTER TABLE [dbo].[rateiocentrocusto] ADD CONSTRAINT [FK_rateiocentrocusto_centrocusto] FOREIGN KEY ([centrocusto_id]) REFERENCES [dbo].[centrocusto] ([id])

/* ExecuteEmbeddedSqlScript Fashion.ERP.Migrator.Scripts._201410142157.permissao.sql */
-- Pagar
-- 1. Lançamento
-- 2. Baixa
-- 3. Previsão Despesas

DECLARE @FINANCEIROID AS BIGINT, @PAGARID AS BIGINT, @ID AS BIGINT;
SET @FINANCEIROID = (SELECT id FROM permissao WHERE area = 'Financeiro' AND controller IS NULL AND [action] IS NULL AND [permissaopai_id] IS NULL);

-- Cria o submenu Pagar
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, Ordem, permissaopai_id) VALUES (NULL, 'Financeiro', NULL, 'Pagar', 1, 1, 0, @FINANCEIROID);
SET @PAGARID = SCOPE_IDENTITY()

-- Cria o menu Contas a pagar
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, Ordem, permissaopai_id) VALUES ('Index', 'Financeiro', 'TituloPagar', 'Lançamento', 1, 1, 0, @PAGARID);
SET @ID = SCOPE_IDENTITY()
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, Ordem, permissaopai_id) VALUES ('Novo', 'Financeiro', 'TituloPagar', 'Novo', 0, 1, 0, @ID);
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, Ordem, permissaopai_id) VALUES ('Editar', 'Financeiro', 'TituloPagar', 'Editar', 0, 1, 0, @ID);
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, Ordem, permissaopai_id) VALUES ('Baixar', 'Financeiro', 'TituloPagar', 'Baixar', 0, 1, 0, @ID);

INSERT INTO [dbo].[VersionInfo] ([Version], [AppliedOn], [Description]) VALUES (201410142157, '2015-02-04T16:38:14', 'Migration201410142157')
/* Committing Transaction */
/* 201410142157: Migration201410142157 migrated */

/* 201410200907: Migration201410200907 migrating ============================= */

/* Beginning Transaction */
/* ExecuteEmbeddedSqlScript Fashion.ERP.Migrator.Scripts._201410200907.permissao.sql */
UPDATE permissao SET controller='RecebimentoCompra' WHERE controller = 'OrdemEntradaCompra'

INSERT INTO [dbo].[VersionInfo] ([Version], [AppliedOn], [Description]) VALUES (201410200907, '2015-02-04T16:38:14', 'Migration201410200907')
/* Committing Transaction */
/* 201410200907: Migration201410200907 migrated */

/* 201411051458: Migration201411051458 migrating ============================= */

/* Beginning Transaction */
/* ExecuteEmbeddedSqlScript Fashion.ERP.Migrator.Scripts._201411051458.permissao.sql */
UPDATE permissao SET controller = 'EntradaMaterial' WHERE controller = 'EntradaCatalogoMaterial';
UPDATE permissao SET controller = 'SaidaMaterial' WHERE controller = 'SaidaCatalogoMaterial';

INSERT INTO [dbo].[VersionInfo] ([Version], [AppliedOn], [Description]) VALUES (201411051458, '2015-02-04T16:38:14', 'Migration201411051458')
/* Committing Transaction */
/* 201411051458: Migration201411051458 migrated */

/* 201411051737: Migration201411051737 migrating ============================= */

/* Beginning Transaction */
/* ExecuteEmbeddedSqlScript Fashion.ERP.Migrator.Scripts._201411051737.permissao.sql */
DECLARE @RELATORIOID AS BIGINT, @ID AS BIGINT;
SET @RELATORIOID = (SELECT id FROM PERMISsao where descricao = 'Relatórios' and area ='EngenhariaProduto');

INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, Ordem, permissaopai_id) 
VALUES ('ConsumoMaterialPorModelo', 'EngenhariaProduto', 'Relatorio', 'Consumo Material Por Modelo',1 ,1,0, @RELATORIOID);

INSERT INTO [dbo].[VersionInfo] ([Version], [AppliedOn], [Description]) VALUES (201411051737, '2015-02-04T16:38:14', 'Migration201411051737')
/* Committing Transaction */
/* 201411051737: Migration201411051737 migrated */

/* 201411071557: Migration201411071557 migrating ============================= */

/* Beginning Transaction */
/* AlterTable recebimentocompra */
/* No SQL statement executed. */

/* CreateColumn recebimentocompra entradamaterial_id Int64 */
ALTER TABLE [dbo].[recebimentocompra] ADD [entradamaterial_id] BIGINT

/* CreateForeignKey FK_recebimentocompra_entradamaterial recebimentocompra(entradamaterial_id) entradamaterial(id) */
ALTER TABLE [dbo].[recebimentocompra] ADD CONSTRAINT [FK_recebimentocompra_entradamaterial] FOREIGN KEY ([entradamaterial_id]) REFERENCES [dbo].[entradamaterial] ([id])

INSERT INTO [dbo].[VersionInfo] ([Version], [AppliedOn], [Description]) VALUES (201411071557, '2015-02-04T16:38:14', 'Migration201411071557')
/* Committing Transaction */
/* 201411071557: Migration201411071557 migrated */

/* 201411101735: Migration201411101735 migrating ============================= */

/* Beginning Transaction */
/* ExecuteEmbeddedSqlScript Fashion.ERP.Migrator.Scripts._201411101735.estoquematerial.sql */
delete from entradaitemmaterial
delete from entradamaterial
delete from saidaitemmaterial
delete from saidamaterial

IF OBJECT_ID('dbo.entradaitemmaterial', 'U') IS NOT NULL
  DROP TABLE dbo.entradaitemmaterial

IF OBJECT_ID('dbo.saidaitemmaterial', 'U') IS NOT NULL
  DROP TABLE dbo.saidaitemmaterial

IF OBJECT_ID('dbo.movimentacaoestoquematerial', 'U') IS NOT NULL
  DROP TABLE dbo.movimentacaoestoquematerial

IF OBJECT_ID('dbo.estoquematerial', 'U') IS NOT NULL
  DROP TABLE dbo.estoquematerial

CREATE TABLE estoquematerial(
	[id] [bigint] NOT NULL,
	[quantidade] [float] NOT NULL,
	[reserva] [float] NOT NULL,
	[material_id] [bigint] NOT NULL,
	[depositomaterial_id] [bigint] NOT NULL,
	CONSTRAINT [PK_estoquematerial] PRIMARY KEY (id)
 )
GO

ALTER TABLE estoquematerial  WITH CHECK ADD  CONSTRAINT [FK_estoquematerial_depositomaterial] FOREIGN KEY([depositomaterial_id])
REFERENCES [dbo].[depositomaterial] ([id])
GO

ALTER TABLE estoquematerial  WITH CHECK ADD  CONSTRAINT [FK_estoquematerial_material] FOREIGN KEY([material_id])
REFERENCES [dbo].[material] ([id])
GO

CREATE TABLE [dbo].movimentacaoestoquematerial(
	[id] [bigint] NOT NULL,
	[quantidade] [float] NOT NULL,	
	[tipomovimentacaoestoquematerial] [nvarchar](255) NOT NULL,
	[data] [datetime] NOT NULL,
	[estoquematerial_id] [bigint] NOT NULL,	
 CONSTRAINT [PK_movimentacaoestoquematerial] PRIMARY KEY (id)
) 
GO

ALTER TABLE [dbo].movimentacaoestoquematerial  WITH CHECK ADD  CONSTRAINT [FK_movimentacaoestoquematerial_estoquematerial] FOREIGN KEY([estoquematerial_id])
REFERENCES [dbo].estoquematerial ([id])
GO

CREATE TABLE [dbo].[entradaitemmaterial](
	[id] [bigint] NOT NULL,
	[quantidadecompra] [float] NOT NULL,	
	[entradamaterial_id] [bigint] NOT NULL,
	[material_id] [bigint] NOT NULL,
	[unidademedidacompra_id] [bigint] NOT NULL,
	[movimentacaoestoquematerial_id] [bigint] NOT NULL
 CONSTRAINT [PK_entradaitemmaterial] PRIMARY KEY (id)
)
GO

ALTER TABLE [dbo].[entradaitemmaterial]  WITH CHECK ADD  CONSTRAINT [FK_entradaitemmaterial_entradamaterial] FOREIGN KEY([entradamaterial_id])
REFERENCES [dbo].[entradamaterial] ([id])
GO

ALTER TABLE [dbo].[entradaitemmaterial]  WITH CHECK ADD  CONSTRAINT [FK_entradaitemmaterial_material] FOREIGN KEY([material_id])
REFERENCES [dbo].[material] ([id])
GO

ALTER TABLE [dbo].[entradaitemmaterial]  WITH CHECK ADD  CONSTRAINT [FK_entradaitemmaterial_unidademedida] FOREIGN KEY([unidademedidacompra_id])
REFERENCES [dbo].[unidademedida] ([id])
GO

ALTER TABLE [dbo].[entradaitemmaterial]  WITH CHECK ADD  CONSTRAINT [FK_entradaitemmaterial_movimentacaoestoquematerial] FOREIGN KEY([movimentacaoestoquematerial_id])
REFERENCES [dbo].movimentacaoestoquematerial ([id])
GO



CREATE TABLE [dbo].[saidaitemmaterial](
	[id] [bigint] NOT NULL,	
	[saidamaterial_id] [bigint] NOT NULL,
	[material_id] [bigint] NOT NULL,
	[movimentacaoestoquematerial_id] [bigint] NOT NULL
 CONSTRAINT [PK_saidaitemmaterial] PRIMARY KEY (id)
 )
GO

ALTER TABLE [dbo].[saidaitemmaterial]  WITH CHECK ADD  CONSTRAINT [FK_saidaitemmaterial_material] FOREIGN KEY([material_id])
REFERENCES [dbo].[material] ([id])
GO

ALTER TABLE [dbo].[saidaitemmaterial]  WITH CHECK ADD  CONSTRAINT [FK_saidaitemmaterial_saidamaterial] FOREIGN KEY([saidamaterial_id])
REFERENCES [dbo].[saidamaterial] ([id])
GO

ALTER TABLE [dbo].saidaitemmaterial  WITH CHECK ADD  CONSTRAINT [FK_saidaitemmaterial_movimentacaoestoquematerial] FOREIGN KEY([movimentacaoestoquematerial_id])
REFERENCES [dbo].movimentacaoestoquematerial ([id])
GO

INSERT INTO [dbo].[VersionInfo] ([Version], [AppliedOn], [Description]) VALUES (201411101735, '2015-02-04T16:38:14', 'Migration201411101735')
/* Committing Transaction */
/* 201411101735: Migration201411101735 migrated */

/* 201411141420: Migration201411141420 migrating ============================= */

/* Beginning Transaction */
/* AlterTable modelo */
/* No SQL statement executed. */

/* CreateColumn modelo chaveexterna String */
ALTER TABLE [dbo].[modelo] ADD [chaveexterna] NVARCHAR(255)

INSERT INTO [dbo].[VersionInfo] ([Version], [AppliedOn], [Description]) VALUES (201411141420, '2015-02-04T16:38:14', 'Migration201411141420')
/* Committing Transaction */
/* 201411141420: Migration201411141420 migrated */

/* 201411190852: Migration201411190852 migrating ============================= */

/* Beginning Transaction */
/* AlterTable material */
/* No SQL statement executed. */

/* AlterColumn material ncm String */
ALTER TABLE [dbo].[material] ALTER COLUMN [ncm] NVARCHAR(255)

/* AlterTable material */
/* No SQL statement executed. */

/* AlterColumn material familia_id Int64 */
ALTER TABLE [dbo].[material] ALTER COLUMN [familia_id] BIGINT

/* AlterTable material */
/* No SQL statement executed. */

/* AlterColumn material generofiscal_id Int64 */
ALTER TABLE [dbo].[material] ALTER COLUMN [generofiscal_id] BIGINT

/* ExecuteEmbeddedSqlScript Fashion.ERP.Migrator.Scripts._201411190852.atualizacaoUniquekeys.sql */
-- O código abaixo gera e executa uma sql para cada tabela do banco que possui Id e atualiza a tabela uniqueKeys com o maior valor de Id.
delete from uniquekeys

declare @uniqueQueries table
(
	Strings        varchar(max)    not null
)

insert into @uniqueQueries 
select 'INSERT uniquekeys SELECT * FROM (SELECT ''' + name + ''' as nome, MAX(id) + 1 as id FROM ' + name + ') as result WHERE result.id is not null;'
from sys.sysobjects as tabela
where type = 'U' 
and id in (SELECT object_id FROM sys.columns WHERE [name] = 'id')

-- Determine loop boundaries.
declare @StringSql nvarchar(500)
declare @counter int = 0
declare @total int = isnull((select count(1) from @uniqueQueries), 0)

while (@counter <> (@total))
begin          
       set @StringSql = (select Strings from @uniqueQueries order by Strings offset @counter rows fetch next 1 rows only)	         
       
	   exec sp_executesql  @StringSql        

       set @counter = @counter + 1
end


INSERT INTO [dbo].[VersionInfo] ([Version], [AppliedOn], [Description]) VALUES (201411190852, '2015-02-04T16:38:14', 'Migration201411190852')
/* Committing Transaction */
/* 201411190852: Migration201411190852 migrated */

/* 201411211020: Migration201411211020 migrating ============================= */

/* Beginning Transaction */
/* ExecuteEmbeddedSqlScript Fashion.ERP.Migrator.Scripts._201411211020.permissao.sql */
DECLARE @IDPAI AS BIGINT, @ID AS BIGINT;
SET @IDPAI = (select permissaopai_id from permissao where descricao = 'Gestão de Compra');

SET @ID = (select id from permissao where descricao = 'Gestão de Compra');

delete from permissaotousuario where  permissao_id = @ID;
delete from permissaotousuario where  permissao_id = @IDPAI;
delete from permissao where  id = @ID;
delete from permissao where  id = @IDPAI;

DECLARE @RELATORIOID AS BIGINT, @IDCONSUMOMATERIAL as BIGINT;
SET @RELATORIOID = (SELECT id FROM PERMISSAO where descricao = 'Relatórios' and area ='EngenhariaProduto');
SET @IDCONSUMOMATERIAL = (SELECT id from permissao where descricao = 'Consumo Material Por Modelo' and permissaopai_id != @RELATORIOID);

delete from permissaotousuario where permissao_id = @IDCONSUMOMATERIAL;
delete from permissao where id = @IDCONSUMOMATERIAL;

----------------------------

drop VIEW [dbo].ExtratoItemEstoqueView
GO
CREATE VIEW [dbo].ExtratoItemEstoqueView
AS
SELECT     ROW_NUMBER() OVER (ORDER BY data) AS id, data, tipomovimentacao, material, qtdentrada, qtdSaida, depositomaterial
FROM         (SELECT     SUM(mem.quantidade) qtdentrada, 0 qtdSaida, ei.material_id AS material, e.depositomaterialdestino_id AS depositomaterial, e.dataentrada AS data,
                                              'e' AS tipomovimentacao
                       FROM          movimentacaoestoquematerial mem inner join (entradaitemmaterial ei INNER JOIN
                                              entradamaterial e ON e.id = ei.entradamaterial_id) ON mem.id = ei.movimentacaoestoquematerial_id
 
                       GROUP BY ei.material_id, e.depositomaterialdestino_id, e.dataentrada
                       UNION ALL
                       SELECT     0 qtdentrada, SUM(mem.quantidade) qtdSaida, si.material_id AS material, s.depositomaterialorigem_id AS depositomaterial, s.datasaida AS data,
                                             's' AS tipomovimentacao
                       FROM          movimentacaoestoquematerial mem inner join (saidaitemmaterial si INNER JOIN
                                             saidamaterial s ON s.id = si.saidamaterial_id) ON mem.id = si.movimentacaoestoquematerial_id
 
                       GROUP BY si.material_id, s.depositomaterialorigem_id, s.datasaida) AS extrato
GO

INSERT INTO [dbo].[VersionInfo] ([Version], [AppliedOn], [Description]) VALUES (201411211020, '2015-02-04T16:38:14', 'Migration201411211020')
/* Committing Transaction */
/* 201411211020: Migration201411211020 migrated */

/* 201411211534: Migration201411211534 migrating ============================= */

/* Beginning Transaction */
/* AlterTable operacaoproducao */
/* No SQL statement executed. */

/* CreateColumn operacaoproducao pesoProdutividade Double */
ALTER TABLE [dbo].[operacaoproducao] ADD [pesoProdutividade] DOUBLE PRECISION

/* ExecuteEmbeddedSqlScript Fashion.ERP.Migrator.Scripts._201411211534.permissao.sql */
update permissao set descricao='Operação Setor', area='Comum', permissaopai_id=1 where id=267
update permissao set permissaopai_id= 1, descricao='Setor Departamento', area='Comum' where id=262

INSERT INTO [dbo].[VersionInfo] ([Version], [AppliedOn], [Description]) VALUES (201411211534, '2015-02-04T16:38:14', 'Migration201411211534')
/* Committing Transaction */
/* 201411211534: Migration201411211534 migrated */

/* 201411251155: Migration201411251155 migrating ============================= */

/* Beginning Transaction */
/* ExecuteEmbeddedSqlScript Fashion.ERP.Migrator.Scripts._201411251155.permissao.sql */
--Sequencia Operacional Natureza
CREATE TABLE [dbo].[sequenciaoperacionalnatureza](
	[id] [bigint] NOT NULL,
	[sequencia] [int] NULL,
	[natureza_id] [bigint] NOT NULL,
	[departamentoproducao_id] [bigint] NOT NULL,
	[setorproducao_id] [bigint] NOT NULL,
	[operacaoproducao_id] [bigint] NOT NULL,
 CONSTRAINT [PK_sequenciaoperacionalnatureza] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[sequenciaoperacionalnatureza]  WITH CHECK ADD  CONSTRAINT [FK_sequenciaoperacionalnatureza_departamento] FOREIGN KEY([departamentoproducao_id])
REFERENCES [dbo].[departamentoproducao] ([id])
GO

ALTER TABLE [dbo].[sequenciaoperacionalnatureza] CHECK CONSTRAINT [FK_sequenciaoperacionalnatureza_departamento]
GO

ALTER TABLE [dbo].[sequenciaoperacionalnatureza]  WITH CHECK ADD  CONSTRAINT [FK_sequenciaoperacionalnatureza_natureza] FOREIGN KEY([natureza_id])
REFERENCES [dbo].[natureza] ([id])
GO

ALTER TABLE [dbo].[sequenciaoperacionalnatureza] CHECK CONSTRAINT [FK_sequenciaoperacionalnatureza_natureza]
GO

ALTER TABLE [dbo].[sequenciaoperacionalnatureza]  WITH CHECK ADD  CONSTRAINT [FK_sequenciaoperacionalnatureza_operacaoproducao] FOREIGN KEY([operacaoproducao_id])
REFERENCES [dbo].[operacaoproducao] ([id])
GO

ALTER TABLE [dbo].[sequenciaoperacionalnatureza] CHECK CONSTRAINT [FK_sequenciaoperacionalnatureza_operacaoproducao]
GO

ALTER TABLE [dbo].[sequenciaoperacionalnatureza]  WITH CHECK ADD  CONSTRAINT [FK_sequenciaoperacionalnatureza_setorproducao] FOREIGN KEY([setorproducao_id])
REFERENCES [dbo].[setorproducao] ([id])
GO

ALTER TABLE [dbo].[sequenciaoperacionalnatureza] CHECK CONSTRAINT [FK_sequenciaoperacionalnatureza_setorproducao]
GO

INSERT INTO [dbo].[VersionInfo] ([Version], [AppliedOn], [Description]) VALUES (201411251155, '2015-02-04T16:38:14', 'Migration201411251155')
/* Committing Transaction */
/* 201411251155: Migration201411251155 migrated */

/* 201411251815: Migration201411251815 migrating ============================= */

/* Beginning Transaction */
/* AlterTable parametromodulocompra */
/* No SQL statement executed. */

/* CreateColumn parametromodulocompra percentualcriacaopedidoautorizadorecebimento Double */
ALTER TABLE [dbo].[parametromodulocompra] ADD [percentualcriacaopedidoautorizadorecebimento] DOUBLE PRECISION

/* UpdateData  */
UPDATE [dbo].[parametromodulocompra] SET [percentualcriacaopedidoautorizadorecebimento] = 0 WHERE 1 = 1

/* AlterTable parametromodulocompra */
/* No SQL statement executed. */

/* AlterColumn parametromodulocompra percentualcriacaopedidoautorizadorecebimento Double */
ALTER TABLE [dbo].[parametromodulocompra] ALTER COLUMN [percentualcriacaopedidoautorizadorecebimento] DOUBLE PRECISION NOT NULL

INSERT INTO [dbo].[VersionInfo] ([Version], [AppliedOn], [Description]) VALUES (201411251815, '2015-02-04T16:38:14', 'Migration201411251815')
/* Committing Transaction */
/* 201411251815: Migration201411251815 migrated */

/* 201412030148: Migration201412030148 migrating ============================= */

/* Beginning Transaction */
/* ExecuteEmbeddedSqlScript Fashion.ERP.Migrator.Scripts._201412030148.permissao.sql */
declare @faseProdutivaId as bigint, @id_pai as bigint;
set @faseProdutivaId = (Select Id from permissao where area='EngenhariaProduto' and descricao='Fases Produtivas' and controller is null)

update permissao set descricao='Operação Setor', area='Comum', permissaopai_id=1 where id=267
update permissao set area='Comum' where permissaopai_id=267
INSERT INTO [dbo].[permissao]
           ([descricao]
           ,[action]
           ,[area]
           ,[controller]
           ,[exibenomenu]
           ,[requerpermissao]
           ,[permissaopai_id]
           ,[ordem])
     VALUES
           ('3.Operação Setor'
           ,'Index'
           ,'Comum'
           ,'OperacaoProducao'
           ,1
           ,1
           ,@faseProdutivaId
           ,0
		   )
		   SET @id_pai = SCOPE_IDENTITY()

		   INSERT INTO [dbo].[permissao]
           ([descricao]
           ,[action]
           ,[area]
           ,[controller]
           ,[exibenomenu]
           ,[requerpermissao]
           ,[permissaopai_id]
           ,[ordem])
     VALUES
           ('Novo'
           ,'Novo'
           ,'Comum'
           ,'OperacaoProducao'
           ,0
           ,1
           ,@id_pai
           ,0
		   )

		   INSERT INTO [dbo].[permissao]
           ([descricao]
           ,[action]
           ,[area]
           ,[controller]
           ,[exibenomenu]
           ,[requerpermissao]
           ,[permissaopai_id]
           ,[ordem])
     VALUES
           ('Editar'
           ,'Editar'
           ,'Comum'
           ,'OperacaoProducao'
           ,0
           ,1
           ,@id_pai
           ,0
		   )

		   INSERT INTO [dbo].[permissao]
           ([descricao]
           ,[action]
           ,[area]
           ,[controller]
           ,[exibenomenu]
           ,[requerpermissao]
           ,[permissaopai_id]
           ,[ordem])
     VALUES
           ('Excluir'
           ,'Excluir'
           ,'Comum'
           ,'OperacaoProducao'
           ,0
           ,1
           ,@id_pai
           ,0
		   )

		   INSERT INTO [dbo].[permissao]
           ([descricao]
           ,[action]
           ,[area]
           ,[controller]
           ,[exibenomenu]
           ,[requerpermissao]
           ,[permissaopai_id]
           ,[ordem])
     VALUES
           ('Editar situação'
           ,'EditarSituacao'
           ,'Comum'
           ,'OperacaoProducao'
           ,0
           ,1
           ,@id_pai
           ,0
		   )


update permissao set permissaopai_id= 1, descricao='Setor Departamento', area='Comum' where id=262
update permissao set area='Comum' where permissaopai_id=262
INSERT INTO [dbo].[permissao]
           ([descricao]
           ,[action]
           ,[area]
           ,[controller]
           ,[exibenomenu]
           ,[requerpermissao]
           ,[permissaopai_id]
           ,[ordem])
     VALUES
           ('2.Setor Departamento'
           ,'Index'
           ,'Comum'
           ,'SetorProducao'
           ,1
           ,1
           ,@faseProdutivaId
           ,0
		   )
		   SET @id_pai = SCOPE_IDENTITY()

		   INSERT INTO [dbo].[permissao]
           ([descricao]
           ,[action]
           ,[area]
           ,[controller]
           ,[exibenomenu]
           ,[requerpermissao]
           ,[permissaopai_id]
           ,[ordem])
     VALUES
           ('Novo'
           ,'Novo'
           ,'Comum'
           ,'SetorProducao'
           ,0
           ,1
           ,@id_pai
           ,0
		   )

		   INSERT INTO [dbo].[permissao]
           ([descricao]
           ,[action]
           ,[area]
           ,[controller]
           ,[exibenomenu]
           ,[requerpermissao]
           ,[permissaopai_id]
           ,[ordem])
     VALUES
           ('Editar'
           ,'Editar'
           ,'Comum'
           ,'SetorProducao'
           ,0
           ,1
           ,@id_pai
           ,0
		   )

		   INSERT INTO [dbo].[permissao]
           ([descricao]
           ,[action]
           ,[area]
           ,[controller]
           ,[exibenomenu]
           ,[requerpermissao]
           ,[permissaopai_id]
           ,[ordem])
     VALUES
           ('Excluir'
           ,'Excluir'
           ,'Comum'
           ,'SetorProducao'
           ,0
           ,1
           ,@id_pai
           ,0
		   )

		   INSERT INTO [dbo].[permissao]
           ([descricao]
           ,[action]
           ,[area]
           ,[controller]
           ,[exibenomenu]
           ,[requerpermissao]
           ,[permissaopai_id]
           ,[ordem])
     VALUES
           ('Editar situação'
           ,'EditarSituacao'
           ,'Comum'
           ,'SetorProducao'
           ,0
           ,1
           ,@id_pai
           ,0
		   )

INSERT INTO [dbo].[VersionInfo] ([Version], [AppliedOn], [Description]) VALUES (201412030148, '2015-02-04T16:38:14', 'Migration201412030148')
/* Committing Transaction */
/* 201412030148: Migration201412030148 migrated */

/* 201412041239: Migration201412041239 migrating ============================= */

/* Beginning Transaction */
/* ExecuteEmbeddedSqlScript Fashion.ERP.Migrator.Scripts._201412041239.permissao.sql */
declare @comumId as bigint, @id_pai as bigint, @basicoId as bigint

-- Artigo
update permissao set area='Comum' where controller='Artigo'
set @comumId = (Select Id from permissao where area='Comum' and descricao='Cadastros' and controller is null)
update permissao set permissaopai_id=@comumId where controller='Artigo' and action='Index' and exibenomenu=1

set @basicoId = (Select Id from permissao where area='EngenhariaProduto' and descricao='Básicos' and controller is null)
insert into permissao (descricao, action,area, controller,exibenomenu,requerpermissao,permissaopai_id,ordem)
select descricao,action,area,controller,exibenomenu,requerpermissao, @basicoId, ordem from permissao where controller='Artigo' and action='Index'
SET @id_pai = SCOPE_IDENTITY()

insert into permissao (descricao, action,area, controller,exibenomenu,requerpermissao,permissaopai_id,ordem)
select descricao,action,area,controller,exibenomenu,requerpermissao, @id_pai, ordem from permissao where controller='Artigo' and action<>'Index'


-- Barra
update permissao set area='Comum' where controller='Barra'
set @comumId = (Select Id from permissao where area='Comum' and descricao='Cadastros' and controller is null)
update permissao set permissaopai_id=@comumId where controller='Barra' and action='Index' and exibenomenu=1

set @basicoId = (Select Id from permissao where area='EngenhariaProduto' and descricao='Básicos' and controller is null)
insert into permissao (descricao, action,area, controller,exibenomenu,requerpermissao,permissaopai_id,ordem)
select descricao,action,area,controller,exibenomenu,requerpermissao, @basicoId, ordem from permissao where controller='Barra' and action='Index'
SET @id_pai = SCOPE_IDENTITY()

insert into permissao (descricao, action,area, controller,exibenomenu,requerpermissao,permissaopai_id,ordem)
select descricao,action,area,controller,exibenomenu,requerpermissao, @id_pai, ordem from permissao where controller='Barra' and action<>'Index'


-- Classificação
update permissao set area='Comum' where controller='Classificacao'
set @comumId = (Select Id from permissao where area='Comum' and descricao='Cadastros' and controller is null)
update permissao set permissaopai_id=@comumId where controller='Classificacao' and action='Index' and exibenomenu=1

set @basicoId = (Select Id from permissao where area='EngenhariaProduto' and descricao='Básicos' and controller is null)
insert into permissao (descricao, action,area, controller,exibenomenu,requerpermissao,permissaopai_id,ordem)
select descricao,action,area,controller,exibenomenu,requerpermissao, @basicoId, ordem from permissao where controller='Classificacao' and action='Index'
SET @id_pai = SCOPE_IDENTITY()

insert into permissao (descricao, action,area, controller,exibenomenu,requerpermissao,permissaopai_id,ordem)
select descricao,action,area,controller,exibenomenu,requerpermissao, @id_pai, ordem from permissao where controller='Classificacao' and action<>'Index'

-- Comprimento
update permissao set area='Comum' where controller='Comprimento'
set @comumId = (Select Id from permissao where area='Comum' and descricao='Cadastros' and controller is null)
update permissao set permissaopai_id=@comumId where controller='Comprimento' and action='Index' and exibenomenu=1

set @basicoId = (Select Id from permissao where area='EngenhariaProduto' and descricao='Básicos' and controller is null)
insert into permissao (descricao, action,area, controller,exibenomenu,requerpermissao,permissaopai_id,ordem)
select descricao,action,area,controller,exibenomenu,requerpermissao, @basicoId, ordem from permissao where controller='Comprimento' and action='Index'
SET @id_pai = SCOPE_IDENTITY()

insert into permissao (descricao, action,area, controller,exibenomenu,requerpermissao,permissaopai_id,ordem)
select descricao,action,area,controller,exibenomenu,requerpermissao, @id_pai, ordem from permissao where controller='Comprimento' and action<>'Index'


-- ProdutoBase
update permissao set area='Comum' where controller='ProdutoBase'
set @comumId = (Select Id from permissao where area='Comum' and descricao='Cadastros' and controller is null)
update permissao set permissaopai_id=@comumId where controller='ProdutoBase' and action='Index' and exibenomenu=1

set @basicoId = (Select Id from permissao where area='EngenhariaProduto' and descricao='Básicos' and controller is null)
insert into permissao (descricao, action,area, controller,exibenomenu,requerpermissao,permissaopai_id,ordem)
select descricao,action,area,controller,exibenomenu,requerpermissao, @basicoId, ordem from permissao where controller='ProdutoBase' and action='Index'
SET @id_pai = SCOPE_IDENTITY()

insert into permissao (descricao, action,area, controller,exibenomenu,requerpermissao,permissaopai_id,ordem)
select descricao,action,area,controller,exibenomenu,requerpermissao, @id_pai, ordem from permissao where controller='ProdutoBase' and action<>'Index'

-- Segmento
update permissao set area='Comum' where controller='Segmento'
set @comumId = (Select Id from permissao where area='Comum' and descricao='Cadastros' and controller is null)
update permissao set permissaopai_id=@comumId where controller='Segmento' and action='Index' and exibenomenu=1

set @basicoId = (Select Id from permissao where area='EngenhariaProduto' and descricao='Básicos' and controller is null)
insert into permissao (descricao, action,area, controller,exibenomenu,requerpermissao,permissaopai_id,ordem)
select descricao,action,area,controller,exibenomenu,requerpermissao, @basicoId, ordem from permissao where controller='Segmento' and action='Index'
SET @id_pai = SCOPE_IDENTITY()

insert into permissao (descricao, action,area, controller,exibenomenu,requerpermissao,permissaopai_id,ordem)
select descricao,action,area,controller,exibenomenu,requerpermissao, @id_pai, ordem from permissao where controller='Segmento' and action<>'Index'

INSERT INTO [dbo].[VersionInfo] ([Version], [AppliedOn], [Description]) VALUES (201412041239, '2015-02-04T16:38:14', 'Migration201412041239')
/* Committing Transaction */
/* 201412041239: Migration201412041239 migrated */

/* 201412041329: Migration201412041329 migrating ============================= */

/* Beginning Transaction */
/* CreateTable customaterial */
CREATE TABLE [dbo].[customaterial] ([id] BIGINT NOT NULL, [idtenant] BIGINT NOT NULL, [idempresa] BIGINT NOT NULL, [data] DATETIME NOT NULL, [custoaquisicao] DOUBLE PRECISION NOT NULL, [customedio] DOUBLE PRECISION NOT NULL, [custo] DOUBLE PRECISION NOT NULL, [ativo] BIT NOT NULL, [fornecedor_id] BIGINT NOT NULL, [material_id] BIGINT NOT NULL, [custoanterior_id] BIGINT, CONSTRAINT [PK_customaterial] PRIMARY KEY ([id]))

/* CreateForeignKey FK_customaterial_fornecedor customaterial(fornecedor_id) pessoa(id) */
ALTER TABLE [dbo].[customaterial] ADD CONSTRAINT [FK_customaterial_fornecedor] FOREIGN KEY ([fornecedor_id]) REFERENCES [dbo].[pessoa] ([id])

/* CreateForeignKey FK_customaterial_material customaterial(material_id) material(id) */
ALTER TABLE [dbo].[customaterial] ADD CONSTRAINT [FK_customaterial_material] FOREIGN KEY ([material_id]) REFERENCES [dbo].[material] ([id])

/* CreateForeignKey FK_customaterial_customaterial customaterial(custoanterior_id) customaterial(id) */
ALTER TABLE [dbo].[customaterial] ADD CONSTRAINT [FK_customaterial_customaterial] FOREIGN KEY ([custoanterior_id]) REFERENCES [dbo].[customaterial] ([id])

/* AlterTable pedidocompra */
/* No SQL statement executed. */

/* AlterColumn pedidocompra contato String */
ALTER TABLE [dbo].[pedidocompra] ALTER COLUMN [contato] NVARCHAR(255)

INSERT INTO [dbo].[VersionInfo] ([Version], [AppliedOn], [Description]) VALUES (201412041329, '2015-02-04T16:38:14', 'Migration201412041329')
/* Committing Transaction */
/* 201412041329: Migration201412041329 migrated */

/* 201412051446: Migration201412051446 migrating ============================= */

/* Beginning Transaction */
/* AlterTable recebimentocompraitem */
/* No SQL statement executed. */

/* CreateColumn recebimentocompraitem customaterial_id Int64 */
ALTER TABLE [dbo].[recebimentocompraitem] ADD [customaterial_id] BIGINT

/* CreateForeignKey FK_recebimentocompraitem_customaterial recebimentocompraitem(customaterial_id) customaterial(id) */
ALTER TABLE [dbo].[recebimentocompraitem] ADD CONSTRAINT [FK_recebimentocompraitem_customaterial] FOREIGN KEY ([customaterial_id]) REFERENCES [dbo].[customaterial] ([id])

INSERT INTO [dbo].[VersionInfo] ([Version], [AppliedOn], [Description]) VALUES (201412051446, '2015-02-04T16:38:14', 'Migration201412051446')
/* Committing Transaction */
/* 201412051446: Migration201412051446 migrated */

/* 201412101433: Migration201412101433 migrating ============================= */

/* Beginning Transaction */
/* CreateTable processooperacional */
CREATE TABLE [dbo].[processooperacional] ([id] BIGINT NOT NULL, [descricao] NVARCHAR(60) NOT NULL, [ativo] BIT NOT NULL, CONSTRAINT [PK_processooperacional] PRIMARY KEY ([id]))

/* CreateTable transportadora */
CREATE TABLE [dbo].[transportadora] ([id] BIGINT NOT NULL, [codigo] BIGINT NOT NULL, [datacadastro] DATETIME NOT NULL, [ativo] BIT NOT NULL, CONSTRAINT [PK_transportadora] PRIMARY KEY ([id]))

/* ExecuteEmbeddedSqlScript Fashion.ERP.Migrator.Scripts._201412101433.processooperacional.sql */
-- Alteração Natureza
alter table sequenciaoperacionalnatureza DROP Constraint FK_sequenciaoperacionalnatureza_natureza
GO
alter table sequenciaoperacionalnatureza DROP COLUMN natureza_id
GO
alter table sequenciaoperacionalnatureza ADD processooperacional_id bigint

ALTER TABLE sequenciaoperacionalnatureza  WITH CHECK ADD  CONSTRAINT [FK_sequenciaoperacionalnatureza_processooperacional] FOREIGN KEY([processooperacional_id])
REFERENCES processooperacional ([id])
GO
ALTER TABLE [dbo].[sequenciaoperacionalnatureza] CHECK CONSTRAINT [FK_sequenciaoperacionalnatureza_processooperacional]

declare @comumId as bigint, @id_pai as bigint
set @comumId = (Select Id from permissao where area='Comum' and descricao='Cadastros' and controller is null)


-- Permissão Processos Operacionais
INSERT INTO [dbo].[permissao]
           ([descricao]
           ,[action]
           ,[area]
           ,[controller]
           ,[exibenomenu]
           ,[requerpermissao]
           ,[permissaopai_id]
           ,[ordem])
     VALUES
           ('Processo Operacional'
           ,'Index'
           ,'Comum'
           ,'ProcessoOperacional'
           ,1
           ,1
           ,@comumId
           ,0
		   )
		   SET @id_pai = SCOPE_IDENTITY()

		   INSERT INTO [dbo].[permissao]
           ([descricao]
           ,[action]
           ,[area]
           ,[controller]
           ,[exibenomenu]
           ,[requerpermissao]
           ,[permissaopai_id]
           ,[ordem])
     VALUES
           ('Novo'
           ,'Novo'
           ,'Comum'
           ,'ProcessoOperacional'
           ,0
           ,1
           ,@id_pai
           ,0
		   )

		   INSERT INTO [dbo].[permissao]
           ([descricao]
           ,[action]
           ,[area]
           ,[controller]
           ,[exibenomenu]
           ,[requerpermissao]
           ,[permissaopai_id]
           ,[ordem])
     VALUES
           ('Editar'
           ,'Editar'
           ,'Comum'
           ,'ProcessoOperacional'
           ,0
           ,1
           ,@id_pai
           ,0
		   )

		   INSERT INTO [dbo].[permissao]
           ([descricao]
           ,[action]
           ,[area]
           ,[controller]
           ,[exibenomenu]
           ,[requerpermissao]
           ,[permissaopai_id]
           ,[ordem])
     VALUES
           ('Excluir'
           ,'Excluir'
           ,'Comum'
           ,'ProcessoOperacional'
           ,0
           ,1
           ,@id_pai
           ,0
		   )

		   INSERT INTO [dbo].[permissao]
           ([descricao]
           ,[action]
           ,[area]
           ,[controller]
           ,[exibenomenu]
           ,[requerpermissao]
           ,[permissaopai_id]
           ,[ordem])
     VALUES
           ('Editar situação'
           ,'EditarSituacao'
           ,'Comum'
           ,'ProcessoOperacional'
           ,0
           ,1
           ,@id_pai
           ,0
		   )


-- Permissão Transportadora

declare @basicoId as bigint
set @basicoId = (Select id from permissao where area='Compras' and descricao='Básicos' and controller is null)

INSERT INTO [dbo].[permissao]
           ([descricao]
           ,[action]
           ,[area]
           ,[controller]
           ,[exibenomenu]
           ,[requerpermissao]
           ,[permissaopai_id]
           ,[ordem])
     VALUES
           ('Transportadora'
           ,'Index'
           ,'Compras'
           ,'Transportadora'
           ,1
           ,1
           ,@basicoId
           ,0
		   )
		   SET @id_pai = SCOPE_IDENTITY()

		   INSERT INTO [dbo].[permissao]
           ([descricao]
           ,[action]
           ,[area]
           ,[controller]
           ,[exibenomenu]
           ,[requerpermissao]
           ,[permissaopai_id]
           ,[ordem])
     VALUES
           ('Novo'
           ,'Novo'
           ,'Compras'
           ,'Transportadora'
           ,0
           ,1
           ,@id_pai
           ,0
		   )

		   INSERT INTO [dbo].[permissao]
           ([descricao]
           ,[action]
           ,[area]
           ,[controller]
           ,[exibenomenu]
           ,[requerpermissao]
           ,[permissaopai_id]
           ,[ordem])
     VALUES
           ('Editar'
           ,'Editar'
           ,'Compras'
           ,'Transportadora'
           ,0
           ,1
           ,@id_pai
           ,0
		   )

		   INSERT INTO [dbo].[permissao]
           ([descricao]
           ,[action]
           ,[area]
           ,[controller]
           ,[exibenomenu]
           ,[requerpermissao]
           ,[permissaopai_id]
           ,[ordem])
     VALUES
           ('Excluir'
           ,'Excluir'
           ,'Compras'
           ,'Transportadora'
           ,0
           ,1
           ,@id_pai
           ,0
		   )


-- Alteração na tabela de pessoa (inclusão da chave de transportadora)
alter table pessoa ADD transportadora_id bigint

ALTER TABLE pessoa  WITH CHECK ADD  CONSTRAINT [FK_pessoa_transportadora] FOREIGN KEY([transportadora_id])
REFERENCES transportadora ([id])
GO
ALTER TABLE [dbo].[pessoa] CHECK CONSTRAINT [FK_pessoa_transportadora]

INSERT INTO [dbo].[VersionInfo] ([Version], [AppliedOn], [Description]) VALUES (201412101433, '2015-02-04T16:38:14', 'Migration201412101433')
/* Committing Transaction */
/* 201412101433: Migration201412101433 migrated */

/* 201412121416: Migration201412121416 migrating ============================= */

/* Beginning Transaction */
/* ExecuteSqlStatement update pessoa set cpfcnpj = REPLACE(REPLACE(REPLACE(cpfcnpj,'/',''),'.',''),'-','')  */
update pessoa set cpfcnpj = REPLACE(REPLACE(REPLACE(cpfcnpj,'/',''),'.',''),'-','') 

/* DeleteConstraint cpf_cnpj_uniqueconstraint */
ALTER TABLE [dbo].[pessoa] DROP CONSTRAINT [cpf_cnpj_uniqueconstraint]

/* AlterTable pessoa */
/* No SQL statement executed. */

/* AlterColumn pessoa cpfcnpj Int64 */
ALTER TABLE [dbo].[pessoa] ALTER COLUMN [cpfcnpj] BIGINT

/* CreateConstraint cpf_cnpj_uniqueconstraint */
ALTER TABLE [dbo].[pessoa] ADD CONSTRAINT [cpf_cnpj_uniqueconstraint] UNIQUE ([cpfcnpj])

INSERT INTO [dbo].[VersionInfo] ([Version], [AppliedOn], [Description]) VALUES (201412121416, '2015-02-04T16:38:14', 'Migration201412121416')
/* Committing Transaction */
/* 201412121416: Migration201412121416 migrated */

/* 201412151522: Migration201412151522 migrating ============================= */

/* Beginning Transaction */
/* DeleteConstraint cpf_cnpj_uniqueconstraint */
ALTER TABLE [dbo].[pessoa] DROP CONSTRAINT [cpf_cnpj_uniqueconstraint]

/* AlterTable pessoa */
/* No SQL statement executed. */

/* CreateColumn pessoa cpfcnpj2 Int64 */
ALTER TABLE [dbo].[pessoa] ADD [cpfcnpj2] BIGINT

/* ExecuteSqlStatement update pessoa set cpfcnpj2 = cpfcnpj */
update pessoa set cpfcnpj2 = cpfcnpj

/* AlterTable pessoa */
/* No SQL statement executed. */

/* AlterColumn pessoa cpfcnpj String */
ALTER TABLE [dbo].[pessoa] ALTER COLUMN [cpfcnpj] NVARCHAR(255)

/* ExecuteSqlStatement update pessoa set cpfcnpj = FORMAT (cpfcnpj2, '00000000000000') where tipopessoa = 'Juridica' */
update pessoa set cpfcnpj = FORMAT (cpfcnpj2, '00000000000000') where tipopessoa = 'Juridica'

/* ExecuteSqlStatement update pessoa set cpfcnpj = FORMAT (cpfcnpj2, '00000000000')  where tipopessoa = 'Fisica' */
update pessoa set cpfcnpj = FORMAT (cpfcnpj2, '00000000000')  where tipopessoa = 'Fisica'

/* CreateConstraint cpf_cnpj_uniqueconstraint */
ALTER TABLE [dbo].[pessoa] ADD CONSTRAINT [cpf_cnpj_uniqueconstraint] UNIQUE ([cpfcnpj])

/* DeleteColumn pessoa cpfcnpj2 */
DECLARE @default sysname, @sql nvarchar(max);

-- get name of default constraint
SELECT @default = name
FROM sys.default_constraints
WHERE parent_object_id = object_id('[dbo].[pessoa]')
AND type = 'D'
AND parent_column_id = (
SELECT column_id
FROM sys.columns
WHERE object_id = object_id('[dbo].[pessoa]')
AND name = 'cpfcnpj2'
);

-- create alter table command to drop constraint as string and run it
SET @sql = N'ALTER TABLE [dbo].[pessoa] DROP CONSTRAINT ' + @default;
EXEC sp_executesql @sql;

-- now we can finally drop column
ALTER TABLE [dbo].[pessoa] DROP COLUMN [cpfcnpj2];


INSERT INTO [dbo].[VersionInfo] ([Version], [AppliedOn], [Description]) VALUES (201412151522, '2015-02-04T16:38:14', 'Migration201412151522')
/* Committing Transaction */
/* 201412151522: Migration201412151522 migrated */

/* 201412161509: Migration201412161509 migrating ============================= */

/* Beginning Transaction */
/* ExecuteEmbeddedSqlScript Fashion.ERP.Migrator.Scripts._201412161509.transportadora.sql */

-- Permissão Transportadora Editar Situação

declare @id_pai as bigint
set @id_pai = (select id from permissao where controller = 'Transportadora' and action = 'Index')

	 INSERT INTO [dbo].[permissao]
           ([descricao]
           ,[action]
           ,[area]
           ,[controller]
           ,[exibenomenu]
           ,[requerpermissao]
           ,[permissaopai_id]
           ,[ordem])
     VALUES
           ('Editar situação'
           ,'EditarSituacao'
           ,'Compras'
           ,'Transportadora'
           ,0
           ,1
           ,@id_pai
           ,0
		   )

INSERT INTO [dbo].[VersionInfo] ([Version], [AppliedOn], [Description]) VALUES (201412161509, '2015-02-04T16:38:14', 'Migration201412161509')
/* Committing Transaction */
/* 201412161509: Migration201412161509 migrated */

/* 201412170947: Migration201412170947 migrating ============================= */

/* Beginning Transaction */
/* AlterTable pedidocompra */
/* No SQL statement executed. */

/* CreateColumn pedidocompra valorencargos Double */
ALTER TABLE [dbo].[pedidocompra] ADD [valorencargos] DOUBLE PRECISION

/* AlterTable pedidocompra */
/* No SQL statement executed. */

/* CreateColumn pedidocompra valorembalagem Double */
ALTER TABLE [dbo].[pedidocompra] ADD [valorembalagem] DOUBLE PRECISION

/* AlterTable pedidocompra */
/* No SQL statement executed. */

/* CreateColumn pedidocompra transportadora_id Int64 */
ALTER TABLE [dbo].[pedidocompra] ADD [transportadora_id] BIGINT

/* CreateForeignKey FK_pedidocompra_transportadora pedidocompra(transportadora_id) pessoa(id) */
ALTER TABLE [dbo].[pedidocompra] ADD CONSTRAINT [FK_pedidocompra_transportadora] FOREIGN KEY ([transportadora_id]) REFERENCES [dbo].[pessoa] ([id])

/* AlterTable pedidocompra */
/* No SQL statement executed. */

/* CreateColumn pedidocompra funcionarioautorizador_id Int64 */
ALTER TABLE [dbo].[pedidocompra] ADD [funcionarioautorizador_id] BIGINT

/* CreateForeignKey FK_pedidocompra_funcionarioautorizador pedidocompra(funcionarioautorizador_id) pessoa(id) */
ALTER TABLE [dbo].[pedidocompra] ADD CONSTRAINT [FK_pedidocompra_funcionarioautorizador] FOREIGN KEY ([funcionarioautorizador_id]) REFERENCES [dbo].[pessoa] ([id])

/* AlterTable pedidocompraitem */
/* No SQL statement executed. */

/* CreateColumn pedidocompraitem valordesconto Double */
ALTER TABLE [dbo].[pedidocompraitem] ADD [valordesconto] DOUBLE PRECISION

INSERT INTO [dbo].[VersionInfo] ([Version], [AppliedOn], [Description]) VALUES (201412170947, '2015-02-04T16:38:14', 'Migration201412170947')
/* Committing Transaction */
/* 201412170947: Migration201412170947 migrated */

/* 201412171041: Migration201412171041 migrating ============================= */

/* Beginning Transaction */
/* CreateTable reservamaterial */
CREATE TABLE [dbo].[reservamaterial] ([id] BIGINT NOT NULL, [idempresa] BIGINT NOT NULL, [idtenant] BIGINT NOT NULL, [numero] BIGINT NOT NULL, [data] DATETIME NOT NULL, [previsaoprimeirautilizacao] DATETIME NOT NULL, [observacao] NVARCHAR(255) NOT NULL, [referencia] NVARCHAR(255) NOT NULL, [finalizada] BIT NOT NULL, [colecao_id] BIGINT NOT NULL, [requerente_id] BIGINT NOT NULL, [unidade_id] BIGINT NOT NULL, CONSTRAINT [PK_reservamaterial] PRIMARY KEY ([id]))

/* CreateForeignKey FK_reservamaterial_colecao reservamaterial(colecao_id) colecao(id) */
ALTER TABLE [dbo].[reservamaterial] ADD CONSTRAINT [FK_reservamaterial_colecao] FOREIGN KEY ([colecao_id]) REFERENCES [dbo].[colecao] ([id])

/* CreateForeignKey FK_reservamaterial_requerente reservamaterial(requerente_id) pessoa(id) */
ALTER TABLE [dbo].[reservamaterial] ADD CONSTRAINT [FK_reservamaterial_requerente] FOREIGN KEY ([requerente_id]) REFERENCES [dbo].[pessoa] ([id])

/* CreateForeignKey FK_reservamaterial_unidade reservamaterial(unidade_id) pessoa(id) */
ALTER TABLE [dbo].[reservamaterial] ADD CONSTRAINT [FK_reservamaterial_unidade] FOREIGN KEY ([unidade_id]) REFERENCES [dbo].[pessoa] ([id])

/* CreateTable reservaestoquematerial */
CREATE TABLE [dbo].[reservaestoquematerial] ([id] BIGINT NOT NULL, [idempresa] BIGINT NOT NULL, [idtenant] BIGINT NOT NULL, [quantidade] BIGINT NOT NULL, CONSTRAINT [PK_reservaestoquematerial] PRIMARY KEY ([id]))

/* CreateTable reservamaterialitem */
CREATE TABLE [dbo].[reservamaterialitem] ([id] BIGINT NOT NULL, [idempresa] BIGINT NOT NULL, [idtenant] BIGINT NOT NULL, [quantidadereserva] BIGINT NOT NULL, [quantidadeatendida] BIGINT NOT NULL, [previsaoutilizacao] DATETIME NOT NULL, [situacaoreservamaterialitem] NVARCHAR(255) NOT NULL, [reservamaterial_id] BIGINT, [reservaestoquematerial_id] BIGINT, [material_id] BIGINT NOT NULL, [reservamaterialitem_id] BIGINT, CONSTRAINT [PK_reservamaterialitem] PRIMARY KEY ([id]))

/* CreateForeignKey FK_reservamaterialitem_reservamaterial reservamaterialitem(reservamaterial_id) reservamaterial(id) */
ALTER TABLE [dbo].[reservamaterialitem] ADD CONSTRAINT [FK_reservamaterialitem_reservamaterial] FOREIGN KEY ([reservamaterial_id]) REFERENCES [dbo].[reservamaterial] ([id])

/* CreateForeignKey FK_reservamaterialitem_reservaestoquematerial reservamaterialitem(reservaestoquematerial_id) reservaestoquematerial(id) */
ALTER TABLE [dbo].[reservamaterialitem] ADD CONSTRAINT [FK_reservamaterialitem_reservaestoquematerial] FOREIGN KEY ([reservaestoquematerial_id]) REFERENCES [dbo].[reservaestoquematerial] ([id])

/* CreateForeignKey FK_reservamaterialitem_material reservamaterialitem(material_id) material(id) */
ALTER TABLE [dbo].[reservamaterialitem] ADD CONSTRAINT [FK_reservamaterialitem_material] FOREIGN KEY ([material_id]) REFERENCES [dbo].[material] ([id])

/* CreateForeignKey FK_reservamaterialitem_reservamaterialitem reservamaterialitem(reservamaterialitem_id) reservamaterialitem(id) */
ALTER TABLE [dbo].[reservamaterialitem] ADD CONSTRAINT [FK_reservamaterialitem_reservamaterialitem] FOREIGN KEY ([reservamaterialitem_id]) REFERENCES [dbo].[reservamaterialitem] ([id])

/* AlterTable estoquematerial */
/* No SQL statement executed. */

/* CreateColumn estoquematerial reservaestoquematerial_id Int64 */
ALTER TABLE [dbo].[estoquematerial] ADD [reservaestoquematerial_id] BIGINT

/* CreateForeignKey FK_estoquematerial_reservaestoquematerial estoquematerial(reservaestoquematerial_id) reservaestoquematerial(id) */
ALTER TABLE [dbo].[estoquematerial] ADD CONSTRAINT [FK_estoquematerial_reservaestoquematerial] FOREIGN KEY ([reservaestoquematerial_id]) REFERENCES [dbo].[reservaestoquematerial] ([id])

/* ExecuteEmbeddedSqlScript Fashion.ERP.Migrator.Scripts._201412171041.sequenciaoperacional.sql */
drop table sequenciaoperacionalnatureza
--Sequencia Operacional Natureza
CREATE TABLE [dbo].[sequenciaoperacional](
	[id] [bigint] NOT NULL,
	[sequencia] [int] NULL,
	[departamentoproducao_id] [bigint] NOT NULL,
	[setorproducao_id] [bigint] NOT NULL,
	[operacaoproducao_id] [bigint] NOT NULL,
	[processooperacional_id] [bigint] NOT NULL,
 CONSTRAINT [PK_sequenciaoperacional] PRIMARY KEY CLUSTERED ([id] ASC)) ON [PRIMARY]

GO

ALTER TABLE [dbo].[sequenciaoperacional]  WITH CHECK ADD  CONSTRAINT [FK_sequenciaoperacional_departamento] FOREIGN KEY([departamentoproducao_id])
REFERENCES [dbo].[departamentoproducao] ([id])
GO

ALTER TABLE [dbo].[sequenciaoperacional]  WITH CHECK ADD  CONSTRAINT [FK_sequenciaoperacional_operacaoproducao] FOREIGN KEY([operacaoproducao_id])
REFERENCES [dbo].[operacaoproducao] ([id])
GO

ALTER TABLE [dbo].[sequenciaoperacional]  WITH CHECK ADD  CONSTRAINT [FK_sequenciaoperacional_setorproducao] FOREIGN KEY([setorproducao_id])
REFERENCES [dbo].[setorproducao] ([id])
GO

ALTER TABLE [dbo].[sequenciaoperacional]  WITH CHECK ADD  CONSTRAINT [FK_sequenciaoperacional_processooperacional] FOREIGN KEY([processooperacional_id])
REFERENCES [dbo].processooperacional ([id])
GO


INSERT INTO [dbo].[VersionInfo] ([Version], [AppliedOn], [Description]) VALUES (201412171041, '2015-02-04T16:38:14', 'Migration201412171041')
/* Committing Transaction */
/* 201412171041: Migration201412171041 migrated */

/* 201412181838: Migration201412181838 migrating ============================= */

/* Beginning Transaction */
/* ExecuteEmbeddedSqlScript Fashion.ERP.Migrator.Scripts._201412181838.permissao.sql */
-- Cheque recebido / Devolução

DECLARE @CHEQUERECEBIDOID AS BIGINT, @ID AS BIGINT;
SET @CHEQUERECEBIDOID = (SELECT id FROM permissao WHERE area = 'Financeiro' AND controller = 'ChequeRecebido' AND [action] = 'Index');

-- Cria o item Devolução
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, Ordem, permissaopai_id) VALUES ('Devolucao', 'Financeiro', 'ChequeRecebido', 'Devolver', 0, 1, 0, @CHEQUERECEBIDOID);

INSERT INTO [dbo].[VersionInfo] ([Version], [AppliedOn], [Description]) VALUES (201412181838, '2015-02-04T16:38:14', 'Migration201412181838')
/* Committing Transaction */
/* 201412181838: Migration201412181838 migrated */

/* 201412191021: Migration201412191021 migrating ============================= */

/* Beginning Transaction */
/* DeleteTable reservamaterialitem */
DROP TABLE [dbo].[reservamaterialitem]

/* DeleteTable reservamaterial */
DROP TABLE [dbo].[reservamaterial]

/* CreateTable reservamaterial */
CREATE TABLE [dbo].[reservamaterial] ([id] BIGINT NOT NULL, [idempresa] BIGINT NOT NULL, [idtenant] BIGINT NOT NULL, [numero] BIGINT NOT NULL, [data] DATETIME NOT NULL, [dataprogramacao] DATETIME NOT NULL, [observacao] NVARCHAR(255), [referenciaorigem] NVARCHAR(255) NOT NULL, [situacaoreservamaterial] NVARCHAR(255) NOT NULL, [colecao_id] BIGINT NOT NULL, [requerente_id] BIGINT NOT NULL, [unidade_id] BIGINT NOT NULL, CONSTRAINT [PK_reservamaterial] PRIMARY KEY ([id]))

/* CreateForeignKey FK_reservamaterial_colecao reservamaterial(colecao_id) colecao(id) */
ALTER TABLE [dbo].[reservamaterial] ADD CONSTRAINT [FK_reservamaterial_colecao] FOREIGN KEY ([colecao_id]) REFERENCES [dbo].[colecao] ([id])

/* CreateForeignKey FK_reservamaterial_requerente reservamaterial(requerente_id) pessoa(id) */
ALTER TABLE [dbo].[reservamaterial] ADD CONSTRAINT [FK_reservamaterial_requerente] FOREIGN KEY ([requerente_id]) REFERENCES [dbo].[pessoa] ([id])

/* CreateForeignKey FK_reservamaterial_unidade reservamaterial(unidade_id) pessoa(id) */
ALTER TABLE [dbo].[reservamaterial] ADD CONSTRAINT [FK_reservamaterial_unidade] FOREIGN KEY ([unidade_id]) REFERENCES [dbo].[pessoa] ([id])

/* CreateTable reservamaterialitemcancelado */
CREATE TABLE [dbo].[reservamaterialitemcancelado] ([id] BIGINT NOT NULL, [idempresa] BIGINT NOT NULL, [idtenant] BIGINT NOT NULL, [data] DATETIME NOT NULL, [observacao] NVARCHAR(255) NOT NULL, [quantidadecancelada] DOUBLE PRECISION NOT NULL, CONSTRAINT [PK_reservamaterialitemcancelado] PRIMARY KEY ([id]))

/* CreateTable reservamaterialitem */
CREATE TABLE [dbo].[reservamaterialitem] ([id] BIGINT NOT NULL, [idempresa] BIGINT NOT NULL, [idtenant] BIGINT NOT NULL, [quantidadereserva] DOUBLE PRECISION NOT NULL, [quantidadeatendida] DOUBLE PRECISION NOT NULL, [situacaoreservamaterial] NVARCHAR(255) NOT NULL, [reservamaterial_id] BIGINT, [reservaestoquematerial_id] BIGINT, [material_id] BIGINT NOT NULL, [reservamaterialitemcancelado_id] BIGINT, [reservamaterialitem_id] BIGINT, CONSTRAINT [PK_reservamaterialitem] PRIMARY KEY ([id]))

/* CreateForeignKey FK_reservamaterialitem_reservamaterial reservamaterialitem(reservamaterial_id) reservamaterial(id) */
ALTER TABLE [dbo].[reservamaterialitem] ADD CONSTRAINT [FK_reservamaterialitem_reservamaterial] FOREIGN KEY ([reservamaterial_id]) REFERENCES [dbo].[reservamaterial] ([id])

/* CreateForeignKey FK_reservamaterialitem_reservaestoquematerial reservamaterialitem(reservaestoquematerial_id) reservaestoquematerial(id) */
ALTER TABLE [dbo].[reservamaterialitem] ADD CONSTRAINT [FK_reservamaterialitem_reservaestoquematerial] FOREIGN KEY ([reservaestoquematerial_id]) REFERENCES [dbo].[reservaestoquematerial] ([id])

/* CreateForeignKey FK_reservamaterialitem_material reservamaterialitem(material_id) material(id) */
ALTER TABLE [dbo].[reservamaterialitem] ADD CONSTRAINT [FK_reservamaterialitem_material] FOREIGN KEY ([material_id]) REFERENCES [dbo].[material] ([id])

/* CreateForeignKey FK_reservamaterialitem_reservamaterialitemcancelado reservamaterialitem(reservamaterialitemcancelado_id) reservamaterialitemcancelado(id) */
ALTER TABLE [dbo].[reservamaterialitem] ADD CONSTRAINT [FK_reservamaterialitem_reservamaterialitemcancelado] FOREIGN KEY ([reservamaterialitemcancelado_id]) REFERENCES [dbo].[reservamaterialitemcancelado] ([id])

/* CreateForeignKey FK_reservamaterialitem_reservamaterialitem reservamaterialitem(reservamaterialitem_id) reservamaterialitem(id) */
ALTER TABLE [dbo].[reservamaterialitem] ADD CONSTRAINT [FK_reservamaterialitem_reservamaterialitem] FOREIGN KEY ([reservamaterialitem_id]) REFERENCES [dbo].[reservamaterialitem] ([id])

INSERT INTO [dbo].[VersionInfo] ([Version], [AppliedOn], [Description]) VALUES (201412191021, '2015-02-04T16:38:15', 'Migration201412191021')
/* Committing Transaction */
/* 201412191021: Migration201412191021 migrated */

/* 201412231650: Migration201412231650 migrating ============================= */

/* Beginning Transaction */
/* ExecuteEmbeddedSqlScript Fashion.ERP.Migrator.Scripts._201412231650.permissao.sql */
update permissao set descricao = 'Estoque de Material' where descricao = 'Movimentação Estoque';

declare @estoquematerialId as bigint, @id as bigint;
set @estoquematerialId = (Select Id from permissao where area='Almoxarifado' and descricao='Estoque de Material' and controller is null)

-- Cria o menu Reserva Material
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, Ordem, permissaopai_id) VALUES ('Index', 'Almoxarifado', 'ReservaMaterial', 'Reserva de Material', 1, 1, 0, @estoquematerialId);
SET @ID = SCOPE_IDENTITY()
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, Ordem, permissaopai_id) VALUES ('Novo', 'Almoxarifado', 'ReservaMaterial', 'Novo', 0, 1, 0, @ID);
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, Ordem, permissaopai_id) VALUES ('Editar', 'Almoxarifado', 'ReservaMaterial', 'Editar', 0, 1, 0, @ID);
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, Ordem, permissaopai_id) VALUES ('Cancelar', 'Almoxarifado', 'ReservaMaterialCancelamento', 'CancelamentoReserva', 0, 1, 0, @ID);

INSERT INTO [dbo].[VersionInfo] ([Version], [AppliedOn], [Description]) VALUES (201412231650, '2015-02-04T16:38:15', 'Migration201412231650')
/* Committing Transaction */
/* 201412231650: Migration201412231650 migrated */

/* 201412291428: Migration201412291428 migrating ============================= */

/* Beginning Transaction */
/* DeleteForeignKey FK_reservamaterialitem_reservaestoquematerial reservamaterialitem ()  () */
ALTER TABLE [dbo].[reservamaterialitem] DROP CONSTRAINT [FK_reservamaterialitem_reservaestoquematerial]

/* DeleteColumn reservamaterialitem reservaestoquematerial_id */
DECLARE @default sysname, @sql nvarchar(max);

-- get name of default constraint
SELECT @default = name
FROM sys.default_constraints
WHERE parent_object_id = object_id('[dbo].[reservamaterialitem]')
AND type = 'D'
AND parent_column_id = (
SELECT column_id
FROM sys.columns
WHERE object_id = object_id('[dbo].[reservamaterialitem]')
AND name = 'reservaestoquematerial_id'
);

-- create alter table command to drop constraint as string and run it
SET @sql = N'ALTER TABLE [dbo].[reservamaterialitem] DROP CONSTRAINT ' + @default;
EXEC sp_executesql @sql;

-- now we can finally drop column
ALTER TABLE [dbo].[reservamaterialitem] DROP COLUMN [reservaestoquematerial_id];


/* DeleteForeignKey FK_reservamaterialitem_reservamaterialitem reservamaterialitem ()  () */
ALTER TABLE [dbo].[reservamaterialitem] DROP CONSTRAINT [FK_reservamaterialitem_reservamaterialitem]

/* DeleteColumn reservamaterialitem reservamaterialitem_id */
DECLARE @default sysname, @sql nvarchar(max);

-- get name of default constraint
SELECT @default = name
FROM sys.default_constraints
WHERE parent_object_id = object_id('[dbo].[reservamaterialitem]')
AND type = 'D'
AND parent_column_id = (
SELECT column_id
FROM sys.columns
WHERE object_id = object_id('[dbo].[reservamaterialitem]')
AND name = 'reservamaterialitem_id'
);

-- create alter table command to drop constraint as string and run it
SET @sql = N'ALTER TABLE [dbo].[reservamaterialitem] DROP CONSTRAINT ' + @default;
EXEC sp_executesql @sql;

-- now we can finally drop column
ALTER TABLE [dbo].[reservamaterialitem] DROP COLUMN [reservamaterialitem_id];


/* DeleteForeignKey FK_estoquematerial_reservaestoquematerial estoquematerial ()  () */
ALTER TABLE [dbo].[estoquematerial] DROP CONSTRAINT [FK_estoquematerial_reservaestoquematerial]

/* DeleteColumn estoquematerial reservaestoquematerial_id */
DECLARE @default sysname, @sql nvarchar(max);

-- get name of default constraint
SELECT @default = name
FROM sys.default_constraints
WHERE parent_object_id = object_id('[dbo].[estoquematerial]')
AND type = 'D'
AND parent_column_id = (
SELECT column_id
FROM sys.columns
WHERE object_id = object_id('[dbo].[estoquematerial]')
AND name = 'reservaestoquematerial_id'
);

-- create alter table command to drop constraint as string and run it
SET @sql = N'ALTER TABLE [dbo].[estoquematerial] DROP CONSTRAINT ' + @default;
EXEC sp_executesql @sql;

-- now we can finally drop column
ALTER TABLE [dbo].[estoquematerial] DROP COLUMN [reservaestoquematerial_id];


/* DeleteTable reservaestoquematerial */
DROP TABLE [dbo].[reservaestoquematerial]

/* CreateTable reservaestoquematerial */
CREATE TABLE [dbo].[reservaestoquematerial] ([id] BIGINT NOT NULL, [idempresa] BIGINT NOT NULL, [idtenant] BIGINT NOT NULL, [quantidade] BIGINT NOT NULL, [material_id] BIGINT NOT NULL, [unidade_id] BIGINT NOT NULL, CONSTRAINT [PK_reservaestoquematerial] PRIMARY KEY ([id]))

/* CreateForeignKey FK_reservaestoquematerial_material reservaestoquematerial(material_id) material(id) */
ALTER TABLE [dbo].[reservaestoquematerial] ADD CONSTRAINT [FK_reservaestoquematerial_material] FOREIGN KEY ([material_id]) REFERENCES [dbo].[material] ([id])

/* CreateForeignKey FK_reservaestoquematerial_unidade reservaestoquematerial(unidade_id) pessoa(id) */
ALTER TABLE [dbo].[reservaestoquematerial] ADD CONSTRAINT [FK_reservaestoquematerial_unidade] FOREIGN KEY ([unidade_id]) REFERENCES [dbo].[pessoa] ([id])

INSERT INTO [dbo].[VersionInfo] ([Version], [AppliedOn], [Description]) VALUES (201412291428, '2015-02-04T16:38:15', 'Migration201412291428')
/* Committing Transaction */
/* 201412291428: Migration201412291428 migrated */

/* 201501050947: Migration201501050947 migrating ============================= */

/* Beginning Transaction */
/* CreateTable requisicaomaterial */
CREATE TABLE [dbo].[requisicaomaterial] ([id] BIGINT NOT NULL, [idempresa] BIGINT NOT NULL, [idtenant] BIGINT NOT NULL, [numero] BIGINT NOT NULL, [data] DATETIME NOT NULL, [observacao] NVARCHAR(255), [origem] NVARCHAR(255), [situacaorequisicaomaterial] NVARCHAR(255) NOT NULL, [reservamaterial_id] BIGINT, [centrocusto_id] BIGINT NOT NULL, [requerente_id] BIGINT NOT NULL, [unidaderequerente_id] BIGINT NOT NULL, [unidaderequisitada_id] BIGINT NOT NULL, [tipoitem_id] BIGINT NOT NULL, CONSTRAINT [PK_requisicaomaterial] PRIMARY KEY ([id]))

/* CreateForeignKey FK_requisicaomaterial_reservamaterial requisicaomaterial(reservamaterial_id) reservamaterial(id) */
ALTER TABLE [dbo].[requisicaomaterial] ADD CONSTRAINT [FK_requisicaomaterial_reservamaterial] FOREIGN KEY ([reservamaterial_id]) REFERENCES [dbo].[reservamaterial] ([id])

/* CreateForeignKey FK_requisicaomaterial_centrocusto requisicaomaterial(centrocusto_id) centrocusto(id) */
ALTER TABLE [dbo].[requisicaomaterial] ADD CONSTRAINT [FK_requisicaomaterial_centrocusto] FOREIGN KEY ([centrocusto_id]) REFERENCES [dbo].[centrocusto] ([id])

/* CreateForeignKey FK_requisicaomaterial_requerente requisicaomaterial(requerente_id) pessoa(id) */
ALTER TABLE [dbo].[requisicaomaterial] ADD CONSTRAINT [FK_requisicaomaterial_requerente] FOREIGN KEY ([requerente_id]) REFERENCES [dbo].[pessoa] ([id])

/* CreateForeignKey FK_requisicaomaterial_unidaderequerente requisicaomaterial(unidaderequerente_id) pessoa(id) */
ALTER TABLE [dbo].[requisicaomaterial] ADD CONSTRAINT [FK_requisicaomaterial_unidaderequerente] FOREIGN KEY ([unidaderequerente_id]) REFERENCES [dbo].[pessoa] ([id])

/* CreateForeignKey FK_requisicaomaterial_unidaderequisitada requisicaomaterial(unidaderequisitada_id) pessoa(id) */
ALTER TABLE [dbo].[requisicaomaterial] ADD CONSTRAINT [FK_requisicaomaterial_unidaderequisitada] FOREIGN KEY ([unidaderequisitada_id]) REFERENCES [dbo].[pessoa] ([id])

/* CreateForeignKey FK_requisicaomaterial_tipoitem requisicaomaterial(tipoitem_id) tipoitem(id) */
ALTER TABLE [dbo].[requisicaomaterial] ADD CONSTRAINT [FK_requisicaomaterial_tipoitem] FOREIGN KEY ([tipoitem_id]) REFERENCES [dbo].[tipoitem] ([id])

/* CreateTable requisicaomaterialitemcancelado */
CREATE TABLE [dbo].[requisicaomaterialitemcancelado] ([id] BIGINT NOT NULL, [idempresa] BIGINT NOT NULL, [idtenant] BIGINT NOT NULL, [data] DATETIME NOT NULL, [observacao] NVARCHAR(255) NOT NULL, [quantidadecancelada] DOUBLE PRECISION NOT NULL, CONSTRAINT [PK_requisicaomaterialitemcancelado] PRIMARY KEY ([id]))

/* CreateTable requisicaomaterialitem */
CREATE TABLE [dbo].[requisicaomaterialitem] ([id] BIGINT NOT NULL, [idempresa] BIGINT NOT NULL, [idtenant] BIGINT NOT NULL, [quantidadesolicitada] DOUBLE PRECISION NOT NULL, [quantidadeatendida] DOUBLE PRECISION NOT NULL, [situacaorequisicaomaterial] NVARCHAR(255) NOT NULL, [requisicaomaterial_id] BIGINT NOT NULL, [material_id] BIGINT NOT NULL, [requisicaomaterialitemcancelado_id] BIGINT, CONSTRAINT [PK_requisicaomaterialitem] PRIMARY KEY ([id]))

/* CreateForeignKey FK_requisicaomaterialitem_requisicaomaterial requisicaomaterialitem(requisicaomaterial_id) requisicaomaterial(id) */
ALTER TABLE [dbo].[requisicaomaterialitem] ADD CONSTRAINT [FK_requisicaomaterialitem_requisicaomaterial] FOREIGN KEY ([requisicaomaterial_id]) REFERENCES [dbo].[requisicaomaterial] ([id])

/* CreateForeignKey FK_requisicaomaterialitem_material requisicaomaterialitem(material_id) material(id) */
ALTER TABLE [dbo].[requisicaomaterialitem] ADD CONSTRAINT [FK_requisicaomaterialitem_material] FOREIGN KEY ([material_id]) REFERENCES [dbo].[material] ([id])

/* CreateForeignKey FK_requisicaomaterialitem_requisicaomaterialitemcancelado requisicaomaterialitem(requisicaomaterialitemcancelado_id) requisicaomaterialitemcancelado(id) */
ALTER TABLE [dbo].[requisicaomaterialitem] ADD CONSTRAINT [FK_requisicaomaterialitem_requisicaomaterialitemcancelado] FOREIGN KEY ([requisicaomaterialitemcancelado_id]) REFERENCES [dbo].[requisicaomaterialitemcancelado] ([id])

/* AlterTable reservamaterial */
/* No SQL statement executed. */

/* AlterColumn reservamaterial dataprogramacao DateTime */
ALTER TABLE [dbo].[reservamaterial] ALTER COLUMN [dataprogramacao] DATETIME

/* AlterTable reservamaterial */
/* No SQL statement executed. */

/* AlterColumn reservamaterial colecao_id Int64 */
ALTER TABLE [dbo].[reservamaterial] ALTER COLUMN [colecao_id] BIGINT

/* AlterTable saidamaterial */
/* No SQL statement executed. */

/* CreateColumn saidamaterial requisicaomaterial_id Int64 */
ALTER TABLE [dbo].[saidamaterial] ADD [requisicaomaterial_id] BIGINT

/* CreateForeignKey FK_saidamaterial_requisicaomaterial saidamaterial(requisicaomaterial_id) requisicaomaterial(id) */
ALTER TABLE [dbo].[saidamaterial] ADD CONSTRAINT [FK_saidamaterial_requisicaomaterial] FOREIGN KEY ([requisicaomaterial_id]) REFERENCES [dbo].[requisicaomaterial] ([id])

/* ExecuteSqlStatement update tipoitem set descricao = UPPER(descricao); */
update tipoitem set descricao = UPPER(descricao);

/* ExecuteEmbeddedSqlScript Fashion.ERP.Migrator.Scripts._201501050947.permissao.sql */
declare @estoquematerialId as bigint, @id as bigint;
set @estoquematerialId = (Select Id from permissao where area='Almoxarifado' and descricao='Estoque de Material' and controller is null)

-- Cria o menu Requisição de Material
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, Ordem, permissaopai_id) VALUES ('Index', 'Almoxarifado', 'RequisicaoMaterial', 'Requisição de Material', 1, 1, 0, @estoquematerialId);
SET @ID = SCOPE_IDENTITY()
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, Ordem, permissaopai_id) VALUES ('Novo', 'Almoxarifado', 'RequisicaoMaterial', 'Novo', 0, 1, 0, @ID);
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, Ordem, permissaopai_id) VALUES ('Editar', 'Almoxarifado', 'RequisicaoMaterial', 'Editar', 0, 1, 0, @ID);
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, Ordem, permissaopai_id) VALUES ('Cancelar', 'Almoxarifado', 'RequisicaoMaterial', 'CancelamentoRequisicaoMaterial', 0, 1, 0, @ID);

INSERT INTO [dbo].[VersionInfo] ([Version], [AppliedOn], [Description]) VALUES (201501050947, '2015-02-04T16:38:15', 'Migration201501050947')
/* Committing Transaction */
/* 201501050947: Migration201501050947 migrated */

/* 201501071743: Migration201501071743 migrating ============================= */

/* Beginning Transaction */
/* AlterTable baixachequerecebido */
/* No SQL statement executed. */

/* CreateColumn baixachequerecebido despesa Double */
ALTER TABLE [dbo].[baixachequerecebido] ADD [despesa] DOUBLE PRECISION

/* UpdateData  */
UPDATE [dbo].[baixachequerecebido] SET [despesa] = 0 WHERE 1 = 1

/* AlterTable baixachequerecebido */
/* No SQL statement executed. */

/* AlterColumn baixachequerecebido despesa Double */
ALTER TABLE [dbo].[baixachequerecebido] ALTER COLUMN [despesa] DOUBLE PRECISION NOT NULL

/* DeleteColumn baixachequerecebido taxajuros */
DECLARE @default sysname, @sql nvarchar(max);

-- get name of default constraint
SELECT @default = name
FROM sys.default_constraints
WHERE parent_object_id = object_id('[dbo].[baixachequerecebido]')
AND type = 'D'
AND parent_column_id = (
SELECT column_id
FROM sys.columns
WHERE object_id = object_id('[dbo].[baixachequerecebido]')
AND name = 'taxajuros'
);

-- create alter table command to drop constraint as string and run it
SET @sql = N'ALTER TABLE [dbo].[baixachequerecebido] DROP CONSTRAINT ' + @default;
EXEC sp_executesql @sql;

-- now we can finally drop column
ALTER TABLE [dbo].[baixachequerecebido] DROP COLUMN [taxajuros];


INSERT INTO [dbo].[VersionInfo] ([Version], [AppliedOn], [Description]) VALUES (201501071743, '2015-02-04T16:38:15', 'Migration201501071743')
/* Committing Transaction */
/* 201501071743: Migration201501071743 migrated */

/* 201501131653: Migration201501131653 migrating ============================= */

/* Beginning Transaction */
/* AlterTable reservamaterial */
/* No SQL statement executed. */

/* AlterColumn reservamaterial referenciaorigem String */
ALTER TABLE [dbo].[reservamaterial] ALTER COLUMN [referenciaorigem] NVARCHAR(255)

/* ExecuteEmbeddedSqlScript Fashion.ERP.Migrator.Scripts._201501131653.permissao.sql */
-- Requisição de Material / Baixar

DECLARE @REQUISICAOMATERIALID AS BIGINT, @ID AS BIGINT;
SET @REQUISICAOMATERIALID = (SELECT id FROM permissao WHERE area = 'Almoxarifado' AND controller = 'RequisicaoMaterial' AND [action] = 'Index');

-- Cria o item Baixar
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, Ordem, permissaopai_id) VALUES ('Baixar', 'Almoxarifado', 'RequisicaoMaterialBaixa', 'Baixar', 0, 1, 0, @REQUISICAOMATERIALID);

INSERT INTO [dbo].[VersionInfo] ([Version], [AppliedOn], [Description]) VALUES (201501131653, '2015-02-04T16:38:15', 'Migration201501131653')
/* Committing Transaction */
/* 201501131653: Migration201501131653 migrated */

/* 201501201054: Migration201501201054 migrating ============================= */

/* Beginning Transaction */
/* AlterTable requisicaomaterial */
/* No SQL statement executed. */

/* CreateColumn requisicaomaterial dataalteracao DateTime */
ALTER TABLE [dbo].[requisicaomaterial] ADD [dataalteracao] DATETIME NOT NULL CONSTRAINT [DF_requisicaomaterial_dataalteracao] DEFAULT '2015-02-04T14:38:15'

/* ExecuteEmbeddedSqlScript Fashion.ERP.Migrator.Scripts._201501201054.permissao.sql */
-- Requisição de Material / Cancelamento

DECLARE @REQUISICAOMATERIALID AS BIGINT, @ID AS BIGINT;
SET @REQUISICAOMATERIALID = (SELECT id FROM permissao WHERE area = 'Almoxarifado' AND controller = 'RequisicaoMaterial' AND [action] = 'Index');

-- Cria o item Cancelar
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, Ordem, permissaopai_id) VALUES ('Cancelar', 'Almoxarifado', 'RequisicaoMaterialCancelamento', 'Cancelar', 0, 1, 0, @REQUISICAOMATERIALID);

INSERT INTO [dbo].[VersionInfo] ([Version], [AppliedOn], [Description]) VALUES (201501201054, '2015-02-04T16:38:15', 'Migration201501201054')
/* Committing Transaction */
/* 201501201054: Migration201501201054 migrated */

/* 201501231346: Migration201501231346 migrating ============================= */

/* Beginning Transaction */
/* CreateTable simboloconservacao */
CREATE TABLE [dbo].[simboloconservacao] ([id] BIGINT NOT NULL, [descricao] NVARCHAR(100) NOT NULL, [categoriaconservacao] NVARCHAR(255) NOT NULL, [foto_id] BIGINT NOT NULL, CONSTRAINT [PK_simboloconservacao] PRIMARY KEY ([id]))

/* ExecuteEmbeddedSqlScript Fashion.ERP.Migrator.Scripts._201501231346.permissao.sql */
DECLARE @BASICOID AS BIGINT, @id_pai AS BIGINT;
SET @BASICOID = (SELECT id FROM permissao WHERE area = 'Almoxarifado' AND controller is null AND descricao='Básicos');

INSERT INTO [dbo].[permissao]
           ([descricao]
           ,[action]
           ,[area]
           ,[controller]
           ,[exibenomenu]
           ,[requerpermissao]
           ,[permissaopai_id]
           ,[ordem])
     VALUES
           ('Símbolo Conservação'
           ,'Index'
           ,'Almoxarifado'
           ,'SimboloConservacao'
           ,1
           ,1
           ,@BASICOID
           ,0
		   )
		   SET @id_pai = SCOPE_IDENTITY()

		   INSERT INTO [dbo].[permissao]
           ([descricao]
           ,[action]
           ,[area]
           ,[controller]
           ,[exibenomenu]
           ,[requerpermissao]
           ,[permissaopai_id]
           ,[ordem])
     VALUES
           ('Novo'
           ,'Novo'
           ,'Almoxarifado'
           ,'SimboloConservacao'
           ,0
           ,1
           ,@id_pai
           ,0
		   )

		   INSERT INTO [dbo].[permissao]
           ([descricao]
           ,[action]
           ,[area]
           ,[controller]
           ,[exibenomenu]
           ,[requerpermissao]
           ,[permissaopai_id]
           ,[ordem])
     VALUES
           ('Editar'
           ,'Editar'
           ,'Almoxarifado'
           ,'SimboloConservacao'
           ,0
           ,1
           ,@id_pai
           ,0
		   )

		   INSERT INTO [dbo].[permissao]
           ([descricao]
           ,[action]
           ,[area]
           ,[controller]
           ,[exibenomenu]
           ,[requerpermissao]
           ,[permissaopai_id]
           ,[ordem])
     VALUES
           ('Excluir'
           ,'Excluir'
           ,'Almoxarifado'
           ,'SimboloConservacao'
           ,0
           ,1
           ,@id_pai
           ,0
		   )



INSERT INTO [dbo].[VersionInfo] ([Version], [AppliedOn], [Description]) VALUES (201501231346, '2015-02-04T16:38:15', 'Migration201501231346')
/* Committing Transaction */
/* 201501231346: Migration201501231346 migrated */

/* 201501232115: Migration201501232115 migrating ============================= */

/* Beginning Transaction */
/* ExecuteEmbeddedSqlScript Fashion.ERP.Migrator.Scripts._201501232115.permissao.sql */
-- Altera o menu
-- Coloca o item Excluir Baixa no mesmo nível de Baixa

DECLARE @CHEQUERECEBIDOID AS BIGINT;
SET @CHEQUERECEBIDOID = (SELECT permissaopai_id FROM permissao WHERE [action] = 'Baixa' and controller = 'ChequeRecebido' and area = 'Financeiro');

UPDATE permissao SET permissaopai_id = @CHEQUERECEBIDOID WHERE [action] = 'ExcluirBaixa' and controller = 'ChequeRecebido' and area = 'Financeiro'

INSERT INTO [dbo].[VersionInfo] ([Version], [AppliedOn], [Description]) VALUES (201501232115, '2015-02-04T16:38:15', 'Migration201501232115')
/* Committing Transaction */
/* 201501232115: Migration201501232115 migrated */

/* 201501292315: Migration201501292315 migrating ============================= */

/* Beginning Transaction */
/* ExecuteEmbeddedSqlScript Fashion.ERP.Migrator.Scripts._201501292315.permissao.sql */
-- Cheque recebido / Devolução

DECLARE @CHEQUEID AS BIGINT;
SET @CHEQUEID = (SELECT id FROM permissao WHERE area = 'Financeiro' AND descricao = 'Cheque' AND [action] is NULL);

-- Cria o item Devolução
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, Ordem, permissaopai_id) VALUES ('Index', 'Financeiro', 'DepositoChequeRecebido', 'Depósito cheques', 1, 1, 0, @CHEQUEID);

INSERT INTO [dbo].[VersionInfo] ([Version], [AppliedOn], [Description]) VALUES (201501292315, '2015-02-04T16:38:15', 'Migration201501292315')
/* Committing Transaction */
/* 201501292315: Migration201501292315 migrated */

/* 201502031545: Migration201502031545 migrating ============================= */

/* Beginning Transaction */
/* AlterTable simboloconservacao */
/* No SQL statement executed. */

/* AlterColumn simboloconservacao descricao String */
ALTER TABLE [dbo].[simboloconservacao] ALTER COLUMN [descricao] NVARCHAR(200) NOT NULL

INSERT INTO [dbo].[VersionInfo] ([Version], [AppliedOn], [Description]) VALUES (201502031545, '2015-02-04T16:38:15', 'Migration201502031545')
/* Committing Transaction */
/* 201502031545: Migration201502031545 migrated */

/* 201502041339: Migration201502041339 migrating ============================= */

/* Beginning Transaction */
/* AlterTable pedidocompra */
/* No SQL statement executed. */

/* CreateColumn pedidocompra dataalteracao DateTime */
ALTER TABLE [dbo].[pedidocompra] ADD [dataalteracao] DATETIME NOT NULL CONSTRAINT [DF_pedidocompra_dataalteracao] DEFAULT '2015-02-04T14:38:15'

INSERT INTO [dbo].[VersionInfo] ([Version], [AppliedOn], [Description]) VALUES (201502041339, '2015-02-04T16:38:15', 'Migration201502041339')
/* Committing Transaction */
/* 201502041339: Migration201502041339 migrated */

/* Task completed. */
