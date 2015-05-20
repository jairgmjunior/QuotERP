
DECLARE @ESTOQUEMATERIALID AS BIGINT;

set @ESTOQUEMATERIALID = (select id from  [dbo].[permissao] where action = 'EstoqueMaterial' and controller = 'Consulta')


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
           ('Custo do Material'
           ,'CustoMaterial'
           ,'Almoxarifado'
           ,'Consulta'
           ,0
           ,1
           ,@ESTOQUEMATERIALID
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
           ('Extrato do Item'
           ,'ExtratoItem'
           ,'Almoxarifado'
           ,'Consulta'
           ,0
           ,1
           ,@ESTOQUEMATERIALID
           ,0
		   )     
  