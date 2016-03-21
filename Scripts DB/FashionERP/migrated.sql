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

INSERT INTO [dbo].[VersionInfo] ([Version], [AppliedOn], [Description]) VALUES (201603091702, '2016-03-11T13:58:56', 'Migration201603091702')
/* Committing Transaction */
/* 201603091702: Migration201603091702 migrated */

/* Task completed. */
