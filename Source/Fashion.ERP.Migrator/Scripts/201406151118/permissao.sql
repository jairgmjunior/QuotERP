DECLARE @COMPRASID AS BIGINT, @PARAMETROSID AS BIGINT;
SET @COMPRASID = (SELECT id FROM permissao WHERE area = 'Compras' AND controller IS NULL AND [action] IS NULL AND [permissaopai_id] IS NULL);

-- Parâmetros
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, ordem, permissaopai_id) VALUES (NULL, 'Compras', NULL, 'Parâmetros', 1, 0, 0, @COMPRASID);
SET @PARAMETROSID = SCOPE_IDENTITY()

-- Gestão de Compra
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, ordem, permissaopai_id) VALUES ('Index', 'Compras', 'ParametroModuloCompra', 'Gestão de Compra', 1, 1, 0, @PARAMETROSID);








