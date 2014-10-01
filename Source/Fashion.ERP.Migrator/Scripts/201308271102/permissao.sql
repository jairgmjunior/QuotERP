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