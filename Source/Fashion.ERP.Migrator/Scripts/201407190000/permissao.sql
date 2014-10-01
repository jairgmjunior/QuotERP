DECLARE @GESTAOCOMPRASID AS BIGINT;
SET @GESTAOCOMPRASID = (SELECT id FROM permissao WHERE [action] IS NULL AND area = 'Compras' AND controller IS NULL AND descricao = 'Gestão de Compras');

-- 3. Recebimento de Compra
DECLARE @ID AS BIGINT;
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, ordem, permissaopai_id) VALUES ('Index', 'Compras', 'OrdemEntradaCompra', '3. Recebimento de Compra', 1, 1, 0, @GESTAOCOMPRASID);
SET @ID = SCOPE_IDENTITY()
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, ordem, permissaopai_id) VALUES ('Novo', 'Compras', 'OrdemEntradaCompra', 'Novo', 0, 1, 0, @ID);
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, ordem, permissaopai_id) VALUES ('Editar', 'Compras', 'OrdemEntradaCompra', 'Editar', 0, 1, 0, @ID);
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, ordem, permissaopai_id) VALUES ('Detalhar', 'Compras', 'OrdemEntradaCompra', 'Detalhar', 0, 1, 0, @ID);