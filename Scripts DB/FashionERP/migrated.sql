/* Using Database sqlserver2012 and Connection String server=.\SQLEXPRESS;Database=FashionERP;User Id=sa;Password=123456; */
/* 201604291434: Migration201604291434 migrating ============================= */

/* Beginning Transaction */
/* ExecuteEmbeddedSqlScript Fashion.ERP.Migrator.Scripts._201604291434.permissao.sql */
DECLARE @FICHATECNCIAINDEXID AS BIGINT;
SET @FICHATECNCIAINDEXID = (SELECT id FROM permissao WHERE area = 'Producao' AND controller ='FichaTecnica' AND [action] = 'Index');

update permissao set permissaopai_id = @FICHATECNCIAINDEXID where controller = 'FichaTecnica' and action <> 'Index'

INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, ordem, permissaopai_id) VALUES ('Fotos', 'Producao', 'FichaTecnica', 'Fotos', 0, 1, 0, @FICHATECNCIAINDEXID);


INSERT INTO [dbo].[VersionInfo] ([Version], [AppliedOn], [Description]) VALUES (201604291434, '2016-05-19T20:46:55', 'Migration201604291434')
/* Committing Transaction */
/* 201604291434: Migration201604291434 migrated */

/* 201605021625: Migration201605021625 migrating ============================= */

/* Beginning Transaction */
/* ExecuteSqlStatement delete from uniquekeys where tablename = 'remessaproducao'; */
delete from uniquekeys where tablename = 'remessaproducao';

/* ExecuteSqlStatement INSERT INTO uniquekeys (tablename, nexthi) VALUES ('remessaproducao', (SELECT ISNULL(MAX(id), 0) + 1 FROM remessaproducao)); */
INSERT INTO uniquekeys (tablename, nexthi) VALUES ('remessaproducao', (SELECT ISNULL(MAX(id), 0) + 1 FROM remessaproducao));

INSERT INTO [dbo].[VersionInfo] ([Version], [AppliedOn], [Description]) VALUES (201605021625, '2016-05-19T20:46:55', 'Migration201605021625')
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
CREATE TABLE [dbo].[producaoprogramacao] ([id] BIGINT NOT NULL, [idtenant] BIGINT NOT NULL, [idempresa] BIGINT NOT NULL, [data] DATE NOT NULL, [dataprogramada] DATE NOT NULL, [observacao] NVARCHAR(255), [quantidade] BIGINT NOT NULL, [unidade_id] BIGINT NOT NULL, [funcionario_id] BIGINT NOT NULL, CONSTRAINT [PK_producaoprogramacao] PRIMARY KEY ([id]))

/* CreateForeignKey FK_producaoprogramacao_unidade producaoprogramacao(unidade_id) pessoa(id) */
ALTER TABLE [dbo].[producaoprogramacao] ADD CONSTRAINT [FK_producaoprogramacao_unidade] FOREIGN KEY ([unidade_id]) REFERENCES [dbo].[pessoa] ([id])

/* CreateForeignKey FK_producaoprogramacao_responsavel producaoprogramacao(funcionario_id) pessoa(id) */
ALTER TABLE [dbo].[producaoprogramacao] ADD CONSTRAINT [FK_producaoprogramacao_responsavel] FOREIGN KEY ([funcionario_id]) REFERENCES [dbo].[pessoa] ([id])

/* CreateTable producao */
CREATE TABLE [dbo].[producao] ([id] BIGINT NOT NULL, [idtenant] BIGINT NOT NULL, [idempresa] BIGINT NOT NULL, [data] DATE NOT NULL, [dataalteracao] DATE NOT NULL, [observacao] NVARCHAR(255), [lote] BIGINT NOT NULL, [ano] BIGINT NOT NULL, [descricao] NVARCHAR(255), [tipoproducao] NVARCHAR(255) NOT NULL, [situacaoproducao] NVARCHAR(255) NOT NULL, [producaoprogramacao_id] BIGINT, [remessaproducao_id] BIGINT NOT NULL, CONSTRAINT [PK_producao] PRIMARY KEY ([id]))

/* CreateForeignKey FK_producao_producaoprogramacao producao(producaoprogramacao_id) producaoprogramacao(id) */
ALTER TABLE [dbo].[producao] ADD CONSTRAINT [FK_producao_producaoprogramacao] FOREIGN KEY ([producaoprogramacao_id]) REFERENCES [dbo].[producaoprogramacao] ([id])

/* CreateForeignKey FK_producao_remessaproducao producao(remessaproducao_id) remessaproducao(id) */
ALTER TABLE [dbo].[producao] ADD CONSTRAINT [FK_producao_remessaproducao] FOREIGN KEY ([remessaproducao_id]) REFERENCES [dbo].[remessaproducao] ([id])

/* CreateTable producaoitem */
CREATE TABLE [dbo].[producaoitem] ([id] BIGINT NOT NULL, [idtenant] BIGINT NOT NULL, [idempresa] BIGINT NOT NULL, [quantidadeprogramada] BIGINT NOT NULL, [quantidadeproducao] BIGINT NOT NULL, [fichatecnica_id] BIGINT NOT NULL, [producaomatrizcorte_id] BIGINT NOT NULL, [producao_id] BIGINT NOT NULL, CONSTRAINT [PK_producaoitem] PRIMARY KEY ([id]))

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

INSERT INTO [dbo].[VersionInfo] ([Version], [AppliedOn], [Description]) VALUES (201605131717, '2016-05-19T20:46:55', 'Migration201605131717')
/* Committing Transaction */
/* 201605131717: Migration201605131717 migrated */

/* Task completed. */
