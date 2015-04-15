DECLARE @RELATORIOID AS BIGINT, @ID AS BIGINT;
SET @RELATORIOID = (SELECT id FROM PERMISsao where descricao = 'Relatórios' and area ='EngenhariaProduto');

INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, Ordem, permissaopai_id) 
VALUES ('SolicitacaoMaterialCompra', 'EngenhariaProduto', 'RelatorioSolicitacaoMaterialCompra', 'Solicitações de Materiais Para Compra',1 ,1,0, @RELATORIOID);


update permissao  set controller= 'RelatorioConsumoMaterialPorModelo' where action = 'ConsumoMaterialPorModelo';