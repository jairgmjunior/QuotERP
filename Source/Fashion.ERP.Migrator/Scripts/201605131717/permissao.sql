DECLARE @BASICOID AS BIGINT, @id_pai AS BIGINT;
SET @BASICOID = (SELECT id FROM permissao WHERE area = 'Producao' AND controller is null AND descricao='Básicos');

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
           ('Produção'
           ,'Index'
           ,'Producao'
           ,'Producao'
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
           ,'Producao'
           ,'Producao'
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
           ,'Producao'
           ,'Producao'
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
           ,'Producao'
           ,'Producao'
           ,0
           ,1
           ,@id_pai
           ,0
		   )

