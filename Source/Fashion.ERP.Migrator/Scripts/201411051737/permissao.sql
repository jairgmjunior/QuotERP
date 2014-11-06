DECLARE @RELATORIOID AS BIGINT, @ID AS BIGINT;
SET @RELATORIOID = (SELECT id FROM PERMISsao where descricao = 'Relatórios' and area ='EngenhariaProduto');

SET @ID = SCOPE_IDENTITY()

INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, Ordem, permissaopai_id) 
VALUES ('ConsumoMaterialPorModelo', 'EngenhariaProduto', 'Relatorio', 'Consumo Material Por Modelo',1 ,1, @RELATORIOID, @ID);