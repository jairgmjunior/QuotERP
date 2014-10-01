DECLARE @COMPRASID AS BIGINT, @BASICOSID AS BIGINT, @MOVIMENTACAOID AS BIGINT, @ID AS BIGINT;
-- COMPRAS
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, ordem, permissaopai_id) VALUES (NULL, 'Compras', NULL, 'Compras', 1, 0, 0, NULL);
SET @COMPRASID = SCOPE_IDENTITY()

-- Basicos
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, ordem, permissaopai_id) VALUES (NULL, 'Compras', NULL, 'Básicos', 1, 0, 0, @COMPRASID);
SET @BASICOSID = SCOPE_IDENTITY()

--	 Fornecedor
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, ordem, permissaopai_id) VALUES ('Index', 'Comum', 'Fornecedor', 'Fornecedor', 1, 1, 0, @BASICOSID);
SET @ID = SCOPE_IDENTITY()
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, ordem, permissaopai_id) VALUES ('Novo', 'Comum', 'Fornecedor', 'Novo', 0, 1, 0, @ID);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, ordem, permissaopai_id) VALUES ('Editar', 'Comum', 'Fornecedor', 'Editar', 0, 1, 0, @ID);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, ordem, permissaopai_id) VALUES ('Excluir', 'Comum', 'Fornecedor', 'Excluir', 0, 1, 0, @ID);

--   Meios Pagamento
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, ordem, permissaopai_id) VALUES ('Index', 'Comum', 'MeioPagamento', 'Meio de pagamento', 1, 1, 0, @BASICOSID);
SET @ID = SCOPE_IDENTITY()
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, ordem, permissaopai_id) VALUES ('Novo', 'Comum', 'MeioPagamento', 'Novo', 0, 1, 0, @ID);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, ordem, permissaopai_id) VALUES ('Editar', 'Comum', 'MeioPagamento', 'Editar', 0, 1, 0, @ID);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, ordem, permissaopai_id) VALUES ('Excluir', 'Comum', 'MeioPagamento', 'Excluir', 0, 1, 0, @ID);

--   Prazo
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, Ordem, permissaopai_id) VALUES ('Index', 'Comum', 'Prazo', 'Prazo', 1, 1, 0, @BASICOSID);
SET @ID = SCOPE_IDENTITY()
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, Ordem, permissaopai_id) VALUES ('Novo', 'Comum', 'Prazo', 'Novo', 0, 1, 0, @ID);
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, Ordem, permissaopai_id) VALUES ('Editar', 'Comum', 'Prazo', 'Editar', 0, 1, 0, @ID);
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, Ordem, permissaopai_id) VALUES ('Excluir', 'Comum', 'Prazo', 'Excluir', 0, 1, 0, @ID);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, Ordem, permissaopai_id) VALUES ('EditarSituacao', 'Comum', 'Prazo', 'Editar situação', 0, 1, 0, @ID);

-- Movimentação Estoque
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, ordem, permissaopai_id) VALUES (NULL, 'Compras', NULL, 'Movimentação Estoque', 1, 0, 0, @COMPRASID);
SET @MOVIMENTACAOID = SCOPE_IDENTITY()

--   Pedido de Compra
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, ordem, permissaopai_id) VALUES ('Index', 'Compras', 'PedidoCompra', 'Pedido de Compra', 1, 1, 0, @MOVIMENTACAOID);
SET @ID = SCOPE_IDENTITY()
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, ordem, permissaopai_id) VALUES ('Novo', 'Compras', 'PedidoCompra', 'Novo', 0, 1, 0, @ID);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, ordem, permissaopai_id) VALUES ('Editar', 'Compras', 'PedidoCompra', 'Editar', 0, 1, 0, @ID);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, ordem, permissaopai_id) VALUES ('Excluir', 'Compras', 'PedidoCompra', 'Excluir', 0, 1, 0, @ID);