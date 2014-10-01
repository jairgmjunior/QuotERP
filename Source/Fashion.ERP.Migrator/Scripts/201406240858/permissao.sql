DECLARE @COMPRASID AS BIGINT;
SET @COMPRASID = (SELECT id FROM permissao WHERE area = 'Compras' AND controller IS NULL AND [action] IS NULL AND [permissaopai_id] IS NULL);

DECLARE @BASICOSID AS BIGINT;
SET @BASICOSID = (SELECT id FROM permissao WHERE descricao = 'Básicos' AND area = 'Compras' AND permissaopai_id = @COMPRASID);

-- MotivoCancelamentoPedidoCompra
DECLARE @ID AS BIGINT;
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, ordem, permissaopai_id) VALUES ('Index', 'Compras', 'MotivoCancelamentoPedidoCompra', 'Motivo de Cancelamento do Pedido de Compra', 1, 1, 0, @BASICOSID);
SET @ID = SCOPE_IDENTITY()
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, ordem, permissaopai_id) VALUES ('Novo', 'Compras', 'MotivoCancelamentoPedidoCompra', 'Novo', 0, 1, 0, @ID);
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, ordem, permissaopai_id) VALUES ('Editar', 'Compras', 'MotivoCancelamentoPedidoCompra', 'Editar', 0, 1, 0, @ID);
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, ordem, permissaopai_id) VALUES ('Excluir', 'Compras', 'MotivoCancelamentoPedidoCompra', 'Excluir', 0, 1, 0, @ID);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, ordem, permissaopai_id) VALUES ('EditarSituacao', 'Compras', 'MotivoCancelamentoPedidoCompra', 'Editar situação', 0, 1, 0, @ID);