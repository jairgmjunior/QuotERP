DECLARE @APROVACAOANTIGOID AS BIGINT,@APROVARANTIGOID AS BIGINT, @DESAPROVARANTIGOID AS BIGINT;
SET @APROVACAOANTIGOID = (select id from permissao where descricao = '2. Aprovacao' and area = 'EngenhariaProduto');
SET @APROVARANTIGOID = (SELECT id from permissao where descricao = 'Aprovar' and permissaopai_id =@APROVACAOANTIGOID);
SET @DESAPROVARANTIGOID = (SELECT id from permissao where descricao = 'Desaprovar' and permissaopai_id =@APROVACAOANTIGOID);
delete from permissaotousuario where permissao_id = @APROVARANTIGOID;
delete from permissaotoperfildeacesso where permissao_id = @APROVARANTIGOID;
DELETE from permissao where descricao = 'Aprovar' and permissaopai_id = @APROVACAOANTIGOID ;

delete from permissaotousuario where permissao_id = @DESAPROVARANTIGOID;
delete from permissaotoperfildeacesso where permissao_id = @DESAPROVARANTIGOID;
DELETE from permissao where descricao = 'Desaprovar' and permissaopai_id =@APROVACAOANTIGOID ;

delete from permissaotousuario where permissao_id = @APROVACAOANTIGOID;
delete from permissaotoperfildeacesso where permissao_id = @APROVACAOANTIGOID;
DELETE from permissao where id =@APROVACAOANTIGOID ;

update permissao set descricao = 'Modelos Aprovados', action = 'ModelosAprovados', controller = 'RelatorioModelosAprovados' 
where descricao = 'Listagem Modelos Aprovados';

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
