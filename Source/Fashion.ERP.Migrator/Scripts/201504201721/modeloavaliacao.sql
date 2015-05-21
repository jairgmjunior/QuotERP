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
	[ano] [bigint] NULL,
	[sequenciatag] [bigint] NULL,
	[tag] [nvarchar](255)NULL,
	[data] [datetime] NULL,
	[aprovado] [bit] NULL,
	[catalogo] [bit] NULL,
	[observacao] [nvarchar](255),
	[quantidadetotaaprovacao] [float] NULL,			
	[complemento] [nvarchar](255),
	[colecao_id] [bigint]  NULL,
	[classificacaodificuldade_id] [bigint] NULL,
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

CREATE TABLE [dbo].modeloaprovacaomatrizcorte(
	[id] [bigint] NOT NULL,
	[idempresa] [bigint] NOT NULL,
	[idtenant] [bigint] NOT NULL,	
	[tipoenfestotecido] [nvarchar](255) NOT NULL	
 CONSTRAINT [PK_modeloaprovacaomatrizcorte] PRIMARY KEY (id)
) 
GO

CREATE TABLE [dbo].modeloaprovacaomatrizcorteitem(
	[id] [bigint] NOT NULL,
	[idempresa] [bigint] NOT NULL,
	[idtenant] [bigint] NOT NULL,	
	[quantidade] [bigint] NOT NULL,			
	[quantidadevezes] [bigint] NOT NULL,			
	[tamanho_id] [bigint] ,			
	[modeloaprovacaomatrizcorte_id] [bigint],				
 CONSTRAINT [PK_modeloaprovacaomatrizcorteitem] PRIMARY KEY (id)
) 
GO

ALTER TABLE [dbo].modeloaprovacaomatrizcorteitem  WITH CHECK ADD  CONSTRAINT [FK_modeloaprovacaomatrizcorteitem_tamanho] FOREIGN KEY([tamanho_id])
REFERENCES [dbo].tamanho ([id])
GO

ALTER TABLE [dbo].modeloaprovacaomatrizcorteitem  WITH CHECK ADD  CONSTRAINT [FK_modeloaprovacaomatrizcorteitem_modeloaprovacaomatrizcorte] FOREIGN KEY([modeloaprovacaomatrizcorte_id])
REFERENCES [dbo].modeloaprovacaomatrizcorte ([id])
GO

CREATE TABLE [dbo].modeloaprovacao(
	[id] [bigint] NOT NULL,
	[idempresa] [bigint] NOT NULL,
	[idtenant] [bigint] NOT NULL,
	[observacao] [nvarchar](255),
	[complemento] [nvarchar](255),
	[referencia] [nvarchar](255)NOT NULL,
	[descricao] [nvarchar](255)NOT NULL,
	[medidabarra] [bigint],
	[medidacomprimento] [bigint],
	[quantidade] [float] NOT NULL,			
	[barra_id] [bigint] ,			
	[comprimento_id] [bigint],			
	[produtobase_id] [bigint],			
	[fichatecnica_id] [bigint],		
	[modeloavaliacao_id] [bigint],	
	[modeloaprovacaomatrizcorte_id] [bigint],	
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

ALTER TABLE [dbo].modeloaprovacao  WITH CHECK ADD  CONSTRAINT [FK_modeloaprovacao_modeloaprovacaomatrizcorte] FOREIGN KEY([modeloaprovacaomatrizcorte_id])
REFERENCES [dbo].modeloaprovacaomatrizcorte ([id])
GO

ALTER TABLE modelo ADD [modeloavaliacao_id] bigint;

ALTER TABLE modelo ADD [situacao] [nvarchar](255)NOT NULL default('NaoAvaliado');

ALTER TABLE [dbo].modelo  WITH CHECK ADD  CONSTRAINT [FK_modelo_modeloavaliacao] FOREIGN KEY([modeloavaliacao_id])
REFERENCES [dbo].[modeloavaliacao] ([id])
GO


-----------------------------
--ALTERAÇÕES NA FICHA TECNICA
-----------------------------

EXEC sp_rename 'fichatecnica.boca', 'medidabarra', 'COLUMN';
EXEC sp_rename 'fichatecnica.cos', 'medidacos', 'COLUMN';
EXEC sp_rename 'fichatecnica.passante', 'medidapassante', 'COLUMN';
EXEC sp_rename 'fichatecnica.entrepernas', 'medidacomprimento', 'COLUMN';

ALTER TABLE fichatecnica ALTER COLUMN medidabarra [float]; 
ALTER TABLE fichatecnica ALTER COLUMN medidacos [float]; 
ALTER TABLE fichatecnica ALTER COLUMN medidapassante [float]; 
ALTER TABLE fichatecnica ALTER COLUMN medidacomprimento [float]; 

EXEC sp_rename 'materialconsumomatriz', 'fichatecnicamaterialconsumo';
EXEC sp_rename 'materialconsumoitem', 'fichatecnicamaterialconsumovariacao';
EXEC sp_rename 'materialcomposicaocustomatriz', 'fichatecnicamaterialcomposicaocusto';

ALTER TABLE fichatecnica ADD catalogo bit NULL, complemento [nvarchar](255) NULL;