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
           ('Coleção Programada'
           ,'Index'
           ,'Producao'
           ,'ColecaoProgramada'
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
           ('Book'
           ,'Book'
           ,'Producao'
           ,'ColecaoProgramada'
           ,0
           ,1
           ,@id_pai
           ,0
		   )

UPDATE reservamaterial 
SET requisicaomaterial_id = requisicaomaterial.id 
FROM requisicaomaterial, reservamaterial
WHERE requisicaomaterial.reservamaterial_id = reservamaterial.id;