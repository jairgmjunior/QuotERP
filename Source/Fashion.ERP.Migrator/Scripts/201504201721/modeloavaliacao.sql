EXEC sp_rename 'dbo.modeloaprovado', 'modeloaprovadoantigo';      

CREATE TABLE [dbo].modeloreprovacao(	
	[id] [bigint] NOT NULL,
	[idempresa] [bigint] NOT NULL,
	[idtenant] [bigint] NOT NULL,
	[motivo] [nvarchar](255)NOT NULL,
 CONSTRAINT [PK_modeloreprovacao] PRIMARY KEY (id)
 )
GO
	
CREATE TABLE modeloavaliacao(
	[id] [bigint] NOT NULL,
	[idempresa] [bigint] NOT NULL,
	[idtenant] [bigint] NOT NULL,
	[ano] [bigint] NOT NULL,
	[tag] [nvarchar](255)NOT NULL,
	[data] [datetime] NOT NULL,
	[aprovado] [bit] NOT NULL,
	[catalogo] [bit] NOT NULL,
	[observacao] [nvarchar](255),
	[quantidadetotaaprovacao] [float] NOT NULL,			
	[complemento] [nvarchar](255),
	[colecao_id] [bigint] NOT NULL,
	[classificacaodificuldade_id] [bigint] NOT NULL,
	[modeloreprovacao_id] [bigint],
	CONSTRAINT [PK_modeloavaliacao] PRIMARY KEY (id)
 )
GO

ALTER TABLE modeloavaliacao  WITH CHECK ADD  CONSTRAINT [FK_modeloavaliacao_colecao] FOREIGN KEY([colecao_id])
REFERENCES [dbo].[colecao] ([id])
GO

ALTER TABLE modeloavaliacao  WITH CHECK ADD  CONSTRAINT [FK_modeloavaliacao_classificacaodificuldade] FOREIGN KEY([classificacaodificuldade_id])
REFERENCES [dbo].[classificacaodificuldade] ([id])
GO

ALTER TABLE modeloavaliacao  WITH CHECK ADD  CONSTRAINT [FK_modeloavaliacao_modeloreprovacao] FOREIGN KEY([modeloreprovacao_id])
REFERENCES [dbo].[modeloreprovacao] ([id])
GO

CREATE TABLE [dbo].modeloaprovacao(
	[id] [bigint] NOT NULL,
	[idempresa] [bigint] NOT NULL,
	[idtenant] [bigint] NOT NULL,
	[observacao] [nvarchar](255),
	[complemento] [nvarchar](255),
	[referencia] [nvarchar](255)NOT NULL,
	[descricao] [nvarchar](255)NOT NULL,
	[medidabarra] [nvarchar](255),
	[quantidade] [float] NOT NULL,			
	[barra_id] [bigint] ,			
	[comprimento_id] [bigint],			
	[produtobase_id] [bigint],			
	[fichatecnica_id] [bigint],		
	[modeloavaliacao_id] [bigint],	
 CONSTRAINT [PK_modeloaprovacao] PRIMARY KEY (id)
) 
GO

ALTER TABLE [dbo].modeloaprovacao  WITH CHECK ADD  CONSTRAINT [FK_modeloaprovacao_barra] FOREIGN KEY([barra_id])
REFERENCES [dbo].barra ([id])
GO

ALTER TABLE [dbo].modeloaprovacao  WITH CHECK ADD  CONSTRAINT [FK_modeloaprovacao_comprimento] FOREIGN KEY([comprimento_id])
REFERENCES [dbo].comprimento ([id])
GO

ALTER TABLE [dbo].modeloaprovacao  WITH CHECK ADD  CONSTRAINT [FK_modeloaprovacao_produtobase] FOREIGN KEY([produtobase_id])
REFERENCES [dbo].produtobase ([id])
GO

ALTER TABLE [dbo].modeloaprovacao  WITH CHECK ADD  CONSTRAINT [FK_modeloaprovacao_fichatecnica] FOREIGN KEY([fichatecnica_id])
REFERENCES [dbo].fichatecnica ([id])
GO

ALTER TABLE [dbo].modeloaprovacao  WITH CHECK ADD  CONSTRAINT [FK_modeloaprovacao_modeloavaliacao] FOREIGN KEY([modeloavaliacao_id])
REFERENCES [dbo].modeloavaliacao ([id])
GO

ALTER TABLE modelo ADD [modeloavaliacao_id] bigint;

ALTER TABLE [dbo].modelo  WITH CHECK ADD  CONSTRAINT [FK_modelo_modeloavaliacao] FOREIGN KEY([modeloavaliacao_id])
REFERENCES [dbo].[modeloavaliacao] ([id])
GO