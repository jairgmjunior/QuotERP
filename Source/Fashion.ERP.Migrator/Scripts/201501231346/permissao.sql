DECLARE @BASICOID AS BIGINT, @id_pai AS BIGINT;
SET @BASICOID = (SELECT id FROM permissao WHERE area = 'Almoxarifado' AND controller is null AND descricao='Básicos');

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
           ('Símbolo Conservação'
           ,'Index'
           ,'Almoxarifado'
           ,'SimboloConservacao'
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
           ,'Almoxarifado'
           ,'SimboloConservacao'
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
           ,'Almoxarifado'
           ,'SimboloConservacao'
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
           ,'Almoxarifado'
           ,'SimboloConservacao'
           ,0
           ,1
           ,@id_pai
           ,0
		   )

