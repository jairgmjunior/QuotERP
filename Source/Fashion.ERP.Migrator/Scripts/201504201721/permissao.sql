DECLARE @MODELOID AS BIGINT, @INDEXID AS BIGINT;

SET @MODELOID = (select id from permissao where descricao = 'Modelo' and area = 'EngenhariaProduto');

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
           ('2. Avaliação'
           ,'Index'
           ,'EngenhariaProduto'
           ,'ModeloAvaliacao'
           ,1
           ,1
           ,@MODELOID
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
           ('Avaliar'
           ,'Avaliar'
           ,'EngenhariaProduto'
           ,'ModeloAvaliacao'
           ,0
           ,1
           ,@INDEXID
           ,0
		   )
