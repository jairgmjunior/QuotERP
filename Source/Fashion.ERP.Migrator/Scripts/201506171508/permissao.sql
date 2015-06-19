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
           ('Modelagem'
           ,'Modelagem'
           ,'Producao'
           ,'FichaTecnica'
           ,0
           ,1
           ,@EDITARID
           ,0
		   )  