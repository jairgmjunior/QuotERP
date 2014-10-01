DECLARE @FINANCEIROID AS BIGINT;
SET @FINANCEIROID = (SELECT id FROM permissao WHERE area = 'Financeiro' AND controller IS NULL AND [action] IS NULL AND [permissaopai_id] IS NULL);

DECLARE @BASICOSID AS BIGINT;
SET @BASICOSID = (SELECT id FROM permissao WHERE descricao = 'Básicos' AND area = 'Financeiro' AND permissaopai_id = @FINANCEIROID);

-- CentroCusto
DECLARE @ID AS BIGINT;
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, ordem, permissaopai_id) VALUES ('Index', 'Comum', 'CentroCusto', 'Centro de Custo', 1, 1, 0, @BASICOSID);
SET @ID = SCOPE_IDENTITY()
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, ordem, permissaopai_id) VALUES ('Novo', 'Comum', 'CentroCusto', 'Novo', 0, 1, 0, @ID);
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, ordem, permissaopai_id) VALUES ('Editar', 'Comum', 'CentroCusto', 'Editar', 0, 1, 0, @ID);
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, ordem, permissaopai_id) VALUES ('Excluir', 'Comum', 'CentroCusto', 'Excluir', 0, 1, 0, @ID);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, ordem, permissaopai_id) VALUES ('EditarSituacao', 'Comum', 'CentroCusto', 'Editar situação', 0, 1, 0, @ID);