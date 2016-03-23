/* Using Database sqlserver2012 and Connection String server=.\SQLEXPRESS;Database=FashionERP;User Id=sa;Password=123456; */
/* 201603091702: Migration201603091702 migrating ============================= */

/* Beginning Transaction */
/* CreateTable remessaproducao */
CREATE TABLE [dbo].[remessaproducao] ([id] BIGINT NOT NULL, [idtenant] BIGINT NOT NULL, [idempresa] BIGINT NOT NULL, [numero] BIGINT NOT NULL, [ano] BIGINT NOT NULL, [descricao] NVARCHAR(255) NOT NULL, [datainicio] DATETIME NOT NULL, [datalimite] DATETIME NOT NULL, [dataalteracao] DATETIME NOT NULL, [observacao] NVARCHAR(255), [colecao_id] BIGINT NOT NULL, CONSTRAINT [PK_remessaproducao] PRIMARY KEY ([id]))

/* CreateForeignKey FK_remessaproducao_colecao remessaproducao(colecao_id) colecao(id) */
ALTER TABLE [dbo].[remessaproducao] ADD CONSTRAINT [FK_remessaproducao_colecao] FOREIGN KEY ([colecao_id]) REFERENCES [dbo].[colecao] ([id])

/* CreateTable remessaproducaocapacidadeprodutiva */
CREATE TABLE [dbo].[remessaproducaocapacidadeprodutiva] ([id] BIGINT NOT NULL, [idtenant] BIGINT NOT NULL, [idempresa] BIGINT NOT NULL, [quantidade] REAL NOT NULL, [remessaproducao_id] BIGINT NOT NULL, [classificacaodificuldade_id] BIGINT NOT NULL, CONSTRAINT [PK_remessaproducaocapacidadeprodutiva] PRIMARY KEY ([id]))

/* CreateForeignKey FK_remessaproducaocapacidadeprodutiva_remessaproducao remessaproducaocapacidadeprodutiva(remessaproducao_id) remessaproducao(id) */
ALTER TABLE [dbo].[remessaproducaocapacidadeprodutiva] ADD CONSTRAINT [FK_remessaproducaocapacidadeprodutiva_remessaproducao] FOREIGN KEY ([remessaproducao_id]) REFERENCES [dbo].[remessaproducao] ([id])

/* CreateForeignKey FK_remessaproducaocapacidadeprodutiva_classificacaodificuldade remessaproducaocapacidadeprodutiva(classificacaodificuldade_id) classificacaodificuldade(id) */
ALTER TABLE [dbo].[remessaproducaocapacidadeprodutiva] ADD CONSTRAINT [FK_remessaproducaocapacidadeprodutiva_classificacaodificuldade] FOREIGN KEY ([classificacaodificuldade_id]) REFERENCES [dbo].[classificacaodificuldade] ([id])

/* ExecuteEmbeddedSqlScript Fashion.ERP.Migrator.Scripts._201603091702.permissao.sql */
DECLARE @PRODUCAOID AS BIGINT;
SET @PRODUCAOID = (SELECT id FROM permissao WHERE area = 'Producao' AND controller IS NULL AND [action] IS NULL AND [permissaopai_id] IS NULL);

DECLARE @BASICOSID AS BIGINT;
SET @BASICOSID = (SELECT id FROM permissao WHERE descricao = 'Básicos' AND area = 'Producao' AND permissaopai_id = @PRODUCAOID);

-- Remessa de produção
DECLARE @ID AS BIGINT;
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, ordem, permissaopai_id) VALUES ('Index', 'Producao', 'RemessaProducao', 'Remessa de Produção', 1, 1, 0, @BASICOSID);
SET @ID = SCOPE_IDENTITY()
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, ordem, permissaopai_id) VALUES ('Novo', 'Producao', 'RemessaProducao', 'Novo', 0, 1, 0, @ID);
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, ordem, permissaopai_id) VALUES ('Editar', 'Producao', 'RemessaProducao', 'Editar', 0, 1, 0, @ID);
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, ordem, permissaopai_id) VALUES ('Excluir', 'Producao', 'RemessaProducao', 'Excluir', 0, 1, 0, @ID);

INSERT INTO remessaproducao(id, idtenant, idempresa, numero, ano, descricao, datainicio, datalimite, dataalteracao, observacao, colecao_id)
	SELECT id, 0, 0, id, 2016, descricao, GETDATE(), GETDATE(), GETDATE(), NULL, id	
		FROM COLECAO


/* AlterTable programacaoproducao */
/* No SQL statement executed. */

/* CreateColumn programacaoproducao remessaproducao_id Int64 */
ALTER TABLE [dbo].[programacaoproducao] ADD [remessaproducao_id] BIGINT

/* CreateForeignKey FK_programacaoproducao_remessaproducao programacaoproducao(remessaproducao_id) remessaproducao(id) */
ALTER TABLE [dbo].[programacaoproducao] ADD CONSTRAINT [FK_programacaoproducao_remessaproducao] FOREIGN KEY ([remessaproducao_id]) REFERENCES [dbo].[remessaproducao] ([id])

/* ExecuteSqlStatement UPDATE programacaoproducao SET remessaproducao_id = colecao_id */
UPDATE programacaoproducao SET remessaproducao_id = colecao_id

/* DeleteForeignKey FK_programacaoproducao_colecao programacaoproducao ()  () */
ALTER TABLE [dbo].[programacaoproducao] DROP CONSTRAINT [FK_programacaoproducao_colecao]

/* DeleteColumn programacaoproducao colecao_id */
DECLARE @default sysname, @sql nvarchar(max);

-- get name of default constraint
SELECT @default = name
FROM sys.default_constraints
WHERE parent_object_id = object_id('[dbo].[programacaoproducao]')
AND type = 'D'
AND parent_column_id = (
SELECT column_id
FROM sys.columns
WHERE object_id = object_id('[dbo].[programacaoproducao]')
AND name = 'colecao_id'
);

-- create alter table command to drop constraint as string and run it
SET @sql = N'ALTER TABLE [dbo].[programacaoproducao] DROP CONSTRAINT ' + @default;
EXEC sp_executesql @sql;

-- now we can finally drop column
ALTER TABLE [dbo].[programacaoproducao] DROP COLUMN [colecao_id];


/* AlterTable fichatecnicamodelagem */
/* No SQL statement executed. */

/* CreateColumn fichatecnicamodelagem descricao String */
ALTER TABLE [dbo].[fichatecnicamodelagem] ADD [descricao] NVARCHAR(255)

INSERT INTO [dbo].[VersionInfo] ([Version], [AppliedOn], [Description]) VALUES (201603091702, '2016-03-23T19:11:27', 'Migration201603091702')
/* Committing Transaction */
/* 201603091702: Migration201603091702 migrated */

/* Task completed. */
