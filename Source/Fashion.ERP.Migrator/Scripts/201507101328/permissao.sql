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
           ('Materiais da Programação de Produção'
           ,'MateriaisProgramacaoProducao'
           ,'Producao'
           ,'ProgramacaoProducao'
           ,0
           ,1
           ,@PROGRAMACAOPRODUCAOID
           ,0
		   ) 