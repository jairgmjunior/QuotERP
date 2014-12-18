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
