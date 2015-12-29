DECLARE @PROGRAMACAOPRODUCAOID AS BIGINT;
set @PROGRAMACAOPRODUCAOID = (select id from [permissao] where controller = 'ProgramacaoProducao' and descricao = 'Programação da Produção')

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
           ('Imprimir'
           ,'Imprimir'
           ,'Producao'
           ,'RelatorioProgramacaoProducao'
           ,0
           ,1
           ,@PROGRAMACAOPRODUCAOID
           ,0
		   ) 