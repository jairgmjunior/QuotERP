DECLARE @CADASTROSID AS BIGINT, @ID AS BIGINT;
SET @CADASTROSID = (SELECT id FROM permissao WHERE area = 'Comum' AND controller IS NULL AND [action] IS NULL AND [permissaopai_id] IS NULL);

-- Prazo
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, Ordem, permissaopai_id) VALUES ('Index', 'Comum', 'ClassificacaoDificuldade', 'Classificação Dificuldade', 1, 1, 0, @CADASTROSID);
SET @ID = SCOPE_IDENTITY()
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, Ordem, permissaopai_id) VALUES ('Novo', 'Comum', 'ClassificacaoDificuldade', 'Novo', 0, 1, 0, @ID);
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, Ordem, permissaopai_id) VALUES ('Editar', 'Comum', 'ClassificacaoDificuldade', 'Editar', 0, 1, 0, @ID);
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, Ordem, permissaopai_id) VALUES ('Excluir', 'Comum', 'ClassificacaoDificuldade', 'Excluir', 0, 1, 0, @ID);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, Ordem, permissaopai_id) VALUES ('EditarSituacao', 'Comum', 'ClassificacaoDificuldade', 'Editar situação', 0, 1, 0, @ID);