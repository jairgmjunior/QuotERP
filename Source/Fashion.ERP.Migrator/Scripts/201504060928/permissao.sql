DECLARE @EDITARID AS BIGINT;
set @EDITARID = (select id from  [dbo].[permissao] where descricao = 'Editar' and action = 'Editar' and area = 'Producao' and controller = 'FichaTecnica')

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
           ,'Basicos'
           ,'Producao'
           ,'FichaTecnica'
           ,0
           ,1
           ,@EDITARID
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
           ('Processos'
           ,'Processos'
           ,'Producao'
           ,'FichaTecnica'
           ,0
           ,1
           ,@EDITARID
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
           ('Material'
           ,'Material'
           ,'Producao'
           ,'FichaTecnica'
           ,0
           ,1
           ,@EDITARID
           ,0
		   )  