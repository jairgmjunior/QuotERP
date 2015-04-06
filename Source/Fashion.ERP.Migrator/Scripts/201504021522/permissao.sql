DECLARE @RELATORIOID AS BIGINT, @ID AS BIGINT;
SET @RELATORIOID = (SELECT id FROM PERMISsao where descricao = 'Relatórios' and area ='EngenhariaProduto');

INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, Ordem, permissaopai_id) 
VALUES ('MateriaisModelosAprovados', 'EngenhariaProduto', 'Relatorio', 'Materiais de Modelos Aprovados',1 ,1,0, @RELATORIOID);