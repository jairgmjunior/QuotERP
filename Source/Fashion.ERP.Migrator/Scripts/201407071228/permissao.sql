DECLARE @COMUMID AS BIGINT;
SET @COMUMID = (SELECT id FROM permissao WHERE area = 'Comum' AND controller IS NULL AND [action] IS NULL AND [permissaopai_id] IS NULL);

-- Empresa
DECLARE @ID AS BIGINT;
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, ordem, permissaopai_id) VALUES ('Index', 'Comum', 'Empresa', 'Empresa', 1, 1, 0, @COMUMID);
SET @ID = SCOPE_IDENTITY()
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, ordem, permissaopai_id) VALUES ('Novo', 'Comum', 'Empresa', 'Novo', 0, 1, 0, @ID);
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, ordem, permissaopai_id) VALUES ('Editar', 'Comum', 'Empresa', 'Editar', 0, 1, 0, @ID);
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, ordem, permissaopai_id) VALUES ('Excluir', 'Comum', 'Empresa', 'Excluir', 0, 1, 0, @ID);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, ordem, permissaopai_id) VALUES ('EditarSituacao', 'Comum', 'Empresa', 'Editar situação', 0, 1, 0, @ID);