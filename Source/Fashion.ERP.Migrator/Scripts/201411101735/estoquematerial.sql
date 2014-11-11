delete from entradamaterial
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