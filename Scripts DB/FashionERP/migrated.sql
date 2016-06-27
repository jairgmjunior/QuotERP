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

----MIGRAÇÃO BOOK DA COLEÇÃO PROGRAMADA PARA REMESSA DE PRODUÇÃO
DECLARE @INDEXID AS BIGINT;
SET @INDEXID = (SELECT id FROM permissao WHERE action = 'Index' and controller = 'RemessaProducao');

UPDATE permissao SET controller = 'RemessaProducao', permissaopai_id = @INDEXID WHERE action = 'Book';

----CUSTO DO MATERIAL
DECLARE @INDEXMATERIALID AS BIGINT;
SET @INDEXMATERIALID = (SELECT id FROM permissao WHERE action = 'Index' and controller = 'Material');

INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, ordem, permissaopai_id) VALUES ('MaterialCusto', 'Almoxarifado', 'Material', 'Custo', 0, 1, 0, @INDEXMATERIALID);

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

/* AlterTable customaterial */
/* No SQL statement executed. */

/* CreateColumn customaterial funcionario_id Int64 */
ALTER TABLE [dbo].[customaterial] ADD [funcionario_id] BIGINT

/* CreateForeignKey FK_customaterial_pessoa customaterial(funcionario_id) pessoa(id) */
ALTER TABLE [dbo].[customaterial] ADD CONSTRAINT [FK_customaterial_pessoa] FOREIGN KEY ([funcionario_id]) REFERENCES [dbo].[pessoa] ([id])

/* AlterTable customaterial */
/* No SQL statement executed. */

/* CreateColumn customaterial cadastromanual Boolean */
ALTER TABLE [dbo].[customaterial] ADD [cadastromanual] BIT NOT NULL CONSTRAINT [DF_customaterial_cadastromanual] DEFAULT 0

/* DeleteForeignKey FK_customaterial_customaterial customaterial ()  () */
ALTER TABLE [dbo].[customaterial] DROP CONSTRAINT [FK_customaterial_customaterial]

/* DeleteColumn customaterial custoanterior_id */
DECLARE @default sysname, @sql nvarchar(max);

-- get name of default constraint
SELECT @default = name
FROM sys.default_constraints
WHERE parent_object_id = object_id('[dbo].[customaterial]')
AND type = 'D'
AND parent_column_id = (
SELECT column_id
FROM sys.columns
WHERE object_id = object_id('[dbo].[customaterial]')
AND name = 'custoanterior_id'
);

-- create alter table command to drop constraint as string and run it
SET @sql = N'ALTER TABLE [dbo].[customaterial] DROP CONSTRAINT ' + @default;
EXEC sp_executesql @sql;

-- now we can finally drop column
ALTER TABLE [dbo].[customaterial] DROP COLUMN [custoanterior_id];


/* DeleteColumn customaterial customedio */
DECLARE @default sysname, @sql nvarchar(max);

-- get name of default constraint
SELECT @default = name
FROM sys.default_constraints
WHERE parent_object_id = object_id('[dbo].[customaterial]')
AND type = 'D'
AND parent_column_id = (
SELECT column_id
FROM sys.columns
WHERE object_id = object_id('[dbo].[customaterial]')
AND name = 'customedio'
);

-- create alter table command to drop constraint as string and run it
SET @sql = N'ALTER TABLE [dbo].[customaterial] DROP CONSTRAINT ' + @default;
EXEC sp_executesql @sql;

-- now we can finally drop column
ALTER TABLE [dbo].[customaterial] DROP COLUMN [customedio];


INSERT INTO [dbo].[VersionInfo] ([Version], [AppliedOn], [Description]) VALUES (201603091702, '2016-06-06T19:41:47', 'Migration201603091702')
/* Committing Transaction */
/* 201603091702: Migration201603091702 migrated */

/* 201604291434: Migration201604291434 migrating ============================= */

/* Beginning Transaction */
/* ExecuteEmbeddedSqlScript Fashion.ERP.Migrator.Scripts._201604291434.permissao.sql */
DECLARE @FICHATECNCIAINDEXID AS BIGINT;
SET @FICHATECNCIAINDEXID = (SELECT id FROM permissao WHERE area = 'Producao' AND controller ='FichaTecnica' AND [action] = 'Index');

update permissao set permissaopai_id = @FICHATECNCIAINDEXID where controller = 'FichaTecnica' and action <> 'Index'

INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, ordem, permissaopai_id) VALUES ('Fotos', 'Producao', 'FichaTecnica', 'Fotos', 0, 1, 0, @FICHATECNCIAINDEXID);


INSERT INTO [dbo].[VersionInfo] ([Version], [AppliedOn], [Description]) VALUES (201604291434, '2016-06-06T19:41:47', 'Migration201604291434')
/* Committing Transaction */
/* 201604291434: Migration201604291434 migrated */

/* 201605021625: Migration201605021625 migrating ============================= */

/* Beginning Transaction */
/* ExecuteSqlStatement delete from uniquekeys where tablename = 'remessaproducao'; */
delete from uniquekeys where tablename = 'remessaproducao';

/* ExecuteSqlStatement INSERT INTO uniquekeys (tablename, nexthi) VALUES ('remessaproducao', (SELECT ISNULL(MAX(id), 0) + 1 FROM remessaproducao)); */
INSERT INTO uniquekeys (tablename, nexthi) VALUES ('remessaproducao', (SELECT ISNULL(MAX(id), 0) + 1 FROM remessaproducao));

INSERT INTO [dbo].[VersionInfo] ([Version], [AppliedOn], [Description]) VALUES (201605021625, '2016-06-06T19:41:47', 'Migration201605021625')
/* Committing Transaction */
/* 201605021625: Migration201605021625 migrated */

/* 201605131717: Migration201605131717 migrating ============================= */

/* Beginning Transaction */
/* CreateTable producaomatrizcorte */
CREATE TABLE [dbo].[producaomatrizcorte] ([id] BIGINT NOT NULL, [idtenant] BIGINT NOT NULL, [idempresa] BIGINT NOT NULL, [tipoenfestotecido] NVARCHAR(255) NOT NULL, CONSTRAINT [PK_producaomatrizcorte] PRIMARY KEY ([id]))

/* CreateTable producaomatrizcorteitem */
CREATE TABLE [dbo].[producaomatrizcorteitem] ([id] BIGINT NOT NULL, [idtenant] BIGINT NOT NULL, [idempresa] BIGINT NOT NULL, [quantidadeprogramada] BIGINT NOT NULL, [quantidadeproducao] BIGINT NOT NULL, [quantidadeadicional] BIGINT NOT NULL, [quantidadecorte] BIGINT NOT NULL, [quantidadevezes] BIGINT NOT NULL, [tamanho_id] BIGINT NOT NULL, [producaomatrizcorte_id] BIGINT NOT NULL, CONSTRAINT [PK_producaomatrizcorteitem] PRIMARY KEY ([id]))

/* CreateForeignKey FK_producaomatrizcorteitem_tamanho producaomatrizcorteitem(tamanho_id) tamanho(id) */
ALTER TABLE [dbo].[producaomatrizcorteitem] ADD CONSTRAINT [FK_producaomatrizcorteitem_tamanho] FOREIGN KEY ([tamanho_id]) REFERENCES [dbo].[tamanho] ([id])

/* CreateForeignKey FK_producaomatrizcorteitem_producaomatrizcorte producaomatrizcorteitem(producaomatrizcorte_id) producaomatrizcorte(id) */
ALTER TABLE [dbo].[producaomatrizcorteitem] ADD CONSTRAINT [FK_producaomatrizcorteitem_producaomatrizcorte] FOREIGN KEY ([producaomatrizcorte_id]) REFERENCES [dbo].[producaomatrizcorte] ([id])

/* CreateTable producaoprogramacao */
CREATE TABLE [dbo].[producaoprogramacao] ([id] BIGINT NOT NULL, [idtenant] BIGINT NOT NULL, [idempresa] BIGINT NOT NULL, [data] DATE NOT NULL, [dataprogramada] DATE NOT NULL, [observacao] NVARCHAR(255), [quantidade] BIGINT NOT NULL, [funcionario_id] BIGINT NOT NULL, CONSTRAINT [PK_producaoprogramacao] PRIMARY KEY ([id]))

/* CreateForeignKey FK_producaoprogramacao_funcionario producaoprogramacao(funcionario_id) pessoa(id) */
ALTER TABLE [dbo].[producaoprogramacao] ADD CONSTRAINT [FK_producaoprogramacao_funcionario] FOREIGN KEY ([funcionario_id]) REFERENCES [dbo].[pessoa] ([id])

/* CreateTable producao */
CREATE TABLE [dbo].[producao] ([id] BIGINT NOT NULL, [idtenant] BIGINT NOT NULL, [idempresa] BIGINT NOT NULL, [data] DATE NOT NULL, [dataalteracao] DATE NOT NULL, [observacao] NVARCHAR(255), [lote] BIGINT NOT NULL, [ano] BIGINT NOT NULL, [descricao] NVARCHAR(255), [tipoproducao] NVARCHAR(255) NOT NULL, [situacaoproducao] NVARCHAR(255) NOT NULL, [producaoprogramacao_id] BIGINT, [remessaproducao_id] BIGINT NOT NULL, [funcionario_id] BIGINT NOT NULL, CONSTRAINT [PK_producao] PRIMARY KEY ([id]))

/* CreateForeignKey FK_producao_producaoprogramacao producao(producaoprogramacao_id) producaoprogramacao(id) */
ALTER TABLE [dbo].[producao] ADD CONSTRAINT [FK_producao_producaoprogramacao] FOREIGN KEY ([producaoprogramacao_id]) REFERENCES [dbo].[producaoprogramacao] ([id])

/* CreateForeignKey FK_producao_remessaproducao producao(remessaproducao_id) remessaproducao(id) */
ALTER TABLE [dbo].[producao] ADD CONSTRAINT [FK_producao_remessaproducao] FOREIGN KEY ([remessaproducao_id]) REFERENCES [dbo].[remessaproducao] ([id])

/* CreateForeignKey FK_producao_funcionario producao(funcionario_id) pessoa(id) */
ALTER TABLE [dbo].[producao] ADD CONSTRAINT [FK_producao_funcionario] FOREIGN KEY ([funcionario_id]) REFERENCES [dbo].[pessoa] ([id])

/* CreateTable producaoitem */
CREATE TABLE [dbo].[producaoitem] ([id] BIGINT NOT NULL, [idtenant] BIGINT NOT NULL, [idempresa] BIGINT NOT NULL, [quantidadeprogramada] BIGINT NOT NULL, [quantidadeproducao] BIGINT NOT NULL, [fichatecnica_id] BIGINT NOT NULL, [producaomatrizcorte_id] BIGINT, [producao_id] BIGINT NOT NULL, CONSTRAINT [PK_producaoitem] PRIMARY KEY ([id]))

/* CreateForeignKey FK_producaoitem_fichatecnica producaoitem(fichatecnica_id) fichatecnica(id) */
ALTER TABLE [dbo].[producaoitem] ADD CONSTRAINT [FK_producaoitem_fichatecnica] FOREIGN KEY ([fichatecnica_id]) REFERENCES [dbo].[fichatecnica] ([id])

/* CreateForeignKey FK_producaoitem_producaomatrizcorte producaoitem(producaomatrizcorte_id) producaomatrizcorte(id) */
ALTER TABLE [dbo].[producaoitem] ADD CONSTRAINT [FK_producaoitem_producaomatrizcorte] FOREIGN KEY ([producaomatrizcorte_id]) REFERENCES [dbo].[producaomatrizcorte] ([id])

/* CreateForeignKey FK_producaoitem_producao producaoitem(producao_id) producao(id) */
ALTER TABLE [dbo].[producaoitem] ADD CONSTRAINT [FK_producaoitem_producao] FOREIGN KEY ([producao_id]) REFERENCES [dbo].[producao] ([id])

/* CreateTable producaoitemmaterial */
CREATE TABLE [dbo].[producaoitemmaterial] ([id] BIGINT NOT NULL, [idtenant] BIGINT NOT NULL, [idempresa] BIGINT NOT NULL, [quantidadeprogramada] BIGINT NOT NULL, [quantidadenecessidade] BIGINT NOT NULL, [quantidadeusada] BIGINT NOT NULL, [quantidadecancelada] BIGINT NOT NULL, [departamentoproducao_id] BIGINT NOT NULL, [responsavel_id] BIGINT, [material_id] BIGINT NOT NULL, [producaoitem_id] BIGINT NOT NULL, [producaoitemmaterial_id] BIGINT, CONSTRAINT [PK_producaoitemmaterial] PRIMARY KEY ([id]))

/* CreateForeignKey FK_producaoitemmaterial_departamentoproducao producaoitemmaterial(departamentoproducao_id) departamentoproducao(id) */
ALTER TABLE [dbo].[producaoitemmaterial] ADD CONSTRAINT [FK_producaoitemmaterial_departamentoproducao] FOREIGN KEY ([departamentoproducao_id]) REFERENCES [dbo].[departamentoproducao] ([id])

/* CreateForeignKey FK_producaoitemmaterial_pessoa producaoitemmaterial(responsavel_id) pessoa(id) */
ALTER TABLE [dbo].[producaoitemmaterial] ADD CONSTRAINT [FK_producaoitemmaterial_pessoa] FOREIGN KEY ([responsavel_id]) REFERENCES [dbo].[pessoa] ([id])

/* CreateForeignKey FK_producaoitemmaterial_material producaoitemmaterial(material_id) material(id) */
ALTER TABLE [dbo].[producaoitemmaterial] ADD CONSTRAINT [FK_producaoitemmaterial_material] FOREIGN KEY ([material_id]) REFERENCES [dbo].[material] ([id])

/* CreateForeignKey FK_producaoitemmaterial_producaoitem producaoitemmaterial(producaoitem_id) producaoitem(id) */
ALTER TABLE [dbo].[producaoitemmaterial] ADD CONSTRAINT [FK_producaoitemmaterial_producaoitem] FOREIGN KEY ([producaoitem_id]) REFERENCES [dbo].[producaoitem] ([id])

/* CreateForeignKey FK_producaoitemmaterial_producaoitemmaterial producaoitemmaterial(producaoitemmaterial_id) producaoitemmaterial(id) */
ALTER TABLE [dbo].[producaoitemmaterial] ADD CONSTRAINT [FK_producaoitemmaterial_producaoitemmaterial] FOREIGN KEY ([producaoitemmaterial_id]) REFERENCES [dbo].[producaoitemmaterial] ([id])

/* ExecuteEmbeddedSqlScript Fashion.ERP.Migrator.Scripts._201605131717.permissao.sql */
delete permissaotousuario where permissao_id = 20433;
delete permissao where controller = 'ColecaoProgramada';


DECLARE @BASICOID AS BIGINT, @id_pai AS BIGINT;
SET @BASICOID = (SELECT id FROM permissao WHERE area = 'Producao' AND controller is null AND descricao='Básicos');

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
           ('Produção'
           ,'Index'
           ,'Producao'
           ,'Producao'
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
           ,'Producao'
           ,'Producao'
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
           ,'Producao'
           ,'Producao'
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
           ('Programação'
           ,'Programacao'
           ,'Producao'
           ,'Producao'
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
           ('Materiais'
           ,'Materiais'
           ,'Producao'
           ,'Producao'
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
           ,'Producao'
           ,'Producao'
           ,0
           ,1
           ,@id_pai
           ,0
		   )



INSERT INTO [dbo].[VersionInfo] ([Version], [AppliedOn], [Description]) VALUES (201605131717, '2016-06-06T19:41:48', 'Migration201605131717')
/* Committing Transaction */
/* 201605131717: Migration201605131717 migrated */

/* Task completed. */
