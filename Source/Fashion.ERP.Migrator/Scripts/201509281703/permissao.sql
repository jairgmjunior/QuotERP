DECLARE @COMPRASID AS BIGINT, @RELATORIOID AS BIGINT, @ID AS BIGINT;
SET @COMPRASID = (select id from permissao where area = 'Compras' and descricao = 'Compras');

INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, Ordem, permissaopai_id) 
VALUES (NULL, 'Compras', null, 'Relatórios', 1 ,0, 0, @COMPRASID);	

SET @RELATORIOID = (select id from permissao where area = 'Compras' and descricao = 'Relatórios');

INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, Ordem, permissaopai_id) 
VALUES ('MateriaisPedidosCompra', 'Compras', 'RelatorioMateriaisPedidosCompra', 'Materiais de Pedido de Compra', 1 ,1,0, @RELATORIOID);	

INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, Ordem, permissaopai_id) 
VALUES ('NecessidadeCompraMaterial', 'Compras', 'RelatorioNecessidadeCompraMaterial', 'Necessidade de Compra de Material', 1 ,1,0, @RELATORIOID);	