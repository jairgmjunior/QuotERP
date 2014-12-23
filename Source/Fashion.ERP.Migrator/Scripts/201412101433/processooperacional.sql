-- Alteração Natureza
alter table sequenciaoperacionalnatureza DROP Constraint FK_sequenciaoperacionalnatureza_natureza
GO
alter table sequenciaoperacionalnatureza DROP COLUMN natureza_id
GO
alter table sequenciaoperacionalnatureza ADD processooperacional_id bigint

ALTER TABLE sequenciaoperacionalnatureza  WITH CHECK ADD  CONSTRAINT [FK_sequenciaoperacionalnatureza_processooperacional] FOREIGN KEY([processooperacional_id])
REFERENCES processooperacional ([id])
GO
ALTER TABLE [dbo].[sequenciaoperacionalnatureza] CHECK CONSTRAINT [FK_sequenciaoperacionalnatureza_processooperacional]

declare @comumId as bigint, @id_pai as bigint
set @comumId = (Select Id from permissao where area='Comum' and descricao='Cadastros' and controller is null)


-- Permissão Processos Operacionais
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
           ('Processo Operacional'
           ,'Index'
           ,'Comum'
           ,'ProcessoOperacional'
           ,1
           ,1
           ,@comumId
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
           ,'Comum'
           ,'ProcessoOperacional'
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
           ,'Comum'
           ,'ProcessoOperacional'
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
           ,'Comum'
           ,'ProcessoOperacional'
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
           ('Editar situação'
           ,'EditarSituacao'
           ,'Comum'
           ,'ProcessoOperacional'
           ,0
           ,1
           ,@id_pai
           ,0
		   )


-- Permissão Transportadora

declare @basicoId as bigint
set @basicoId = (Select id from permissao where area='Compras' and descricao='Básicos' and controller is null)

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
           ('Transportadora'
           ,'Index'
           ,'Compras'
           ,'Transportadora'
           ,1
           ,1
           ,@basicoId
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
           ,'Compras'
           ,'Transportadora'
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
           ,'Compras'
           ,'Transportadora'
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
           ,'Compras'
           ,'Transportadora'
           ,0
           ,1
           ,@id_pai
           ,0
		   )


-- Alteração na tabela de pessoa (inclusão da chave de transportadora)
alter table pessoa ADD transportadora_id bigint

ALTER TABLE pessoa  WITH CHECK ADD  CONSTRAINT [FK_pessoa_transportadora] FOREIGN KEY([transportadora_id])
REFERENCES transportadora ([id])
GO
ALTER TABLE [dbo].[pessoa] CHECK CONSTRAINT [FK_pessoa_transportadora]