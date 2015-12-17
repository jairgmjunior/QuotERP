INSERT INTO programacaoproducaoitem
(id, quantidade, fichatecnica_id, programacaoproducaomatrizcorte_id, idempresa, idtenant, programacaoproducao_id)
SELECT id, quantidade, fichatecnica_id, programacaoproducaomatrizcorte_id, idempresa, idtenant, id
FROM programacaoproducao;

INSERT INTO uniquekeys
(tablename, nexthi)
SELECT 'programacaoproducaoitem', nexthi
FROM uniquekeys where tablename = 'programacaoproducao';

ALTER TABLE programacaoproducao DROP CONSTRAINT fk_programacaoproducao_programacaoproducaomatrizcorte;
ALTER TABLE programacaoproducao DROP COLUMN programacaoproducaomatrizcorte_id;

ALTER TABLE programacaoproducao DROP CONSTRAINT fk_programacaoproducao_fichatecnica;
ALTER TABLE programacaoproducao DROP COLUMN fichatecnica_id;

ALTER TABLE programacaoproducao ALTER COLUMN numero NVARCHAR(100) NOT NULL;

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
           ('Requisição da Programação de Produção'
           ,'ProgramacaoProducaoRequisicao'
           ,'Producao'
           ,'ProgramacaoProducao'
           ,0
           ,1
           ,@PROGRAMACAOPRODUCAOID
           ,0
		   ) 