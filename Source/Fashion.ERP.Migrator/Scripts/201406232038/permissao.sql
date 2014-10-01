DECLARE @COMPRASID AS BIGINT, @PARAMETROSID AS BIGINT, @ID AS BIGINT;
SET @COMPRASID = (SELECT id FROM permissao WHERE area = 'Compras' AND controller IS NULL AND [action] IS NULL AND [permissaopai_id] IS NULL);

-- Parâmetros
SET @PARAMETROSID = (SELECT id FROM permissao WHERE area = 'Compras' AND descricao = 'Parâmetros' AND controller IS NULL AND [action] IS NULL AND [permissaopai_id] = @COMPRASID);

-- Autorizações
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, ordem, permissaopai_id) VALUES ('Index', 'Compras', 'ProcedimentoModuloCompras', 'Autorizações', 1, 1, 0, @PARAMETROSID);
SET @ID = SCOPE_IDENTITY()
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, ordem, permissaopai_id) VALUES ('Novo', 'Compras', 'ProcedimentoModuloCompras', 'Novo', 0, 1, 0, @ID);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, ordem, permissaopai_id) VALUES ('Editar', 'Compras', 'ProcedimentoModuloCompras', 'Editar', 0, 1, 0, @ID);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, ordem, permissaopai_id) VALUES ('Excluir', 'Compras', 'ProcedimentoModuloCompras', 'Excluir', 0, 1, 0, @ID);