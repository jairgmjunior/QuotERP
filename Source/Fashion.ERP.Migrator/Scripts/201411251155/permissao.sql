--Sequencia Operacional Natureza
CREATE TABLE [dbo].[sequenciaoperacionalnatureza](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
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