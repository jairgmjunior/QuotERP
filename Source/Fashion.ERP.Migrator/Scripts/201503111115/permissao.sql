DECLARE @PRODUCAOID AS BIGINT, @BASICOID AS BIGINT, @INDEXID AS BIGINT, @NOVOID AS BIGINT;

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
           ,null
           ,'Producao'
           ,null
           ,1
           ,0
           ,null
           ,0
		   )

SET @PRODUCAOID = SCOPE_IDENTITY()

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
           ('Básicos'
           ,null
           ,'Producao'
           ,null
           ,1
           ,1
           ,@PRODUCAOID
           ,0
		   )

SET @BASICOID = SCOPE_IDENTITY()

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
           ('Ficha Técnica'
           ,'Index'
           ,'Producao'
           ,'FichaTecnica'
           ,1
           ,1
           ,@BASICOID
           ,0
		   )

SET @INDEXID = SCOPE_IDENTITY()

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
           ,'FichaTecnica'
           ,0
           ,1
           ,@INDEXID
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
           ,'FichaTecnica'
           ,0
           ,1
           ,@INDEXID
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
           ,'FichaTecnica'
           ,0
           ,1
           ,@INDEXID
           ,0
		   )

