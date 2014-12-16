
-- Permissão Transportadora Editar Situação

declare @id_pai as bigint
set @id_pai = (select id from permissao where controller = 'Transportadora' and action = 'Index')

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
           ,'Compras'
           ,'Transportadora'
           ,0
           ,1
           ,@id_pai
           ,0
		   )