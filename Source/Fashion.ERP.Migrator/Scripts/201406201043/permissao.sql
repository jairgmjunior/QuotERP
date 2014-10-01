UPDATE permissao SET descricao = 'Básicos' 
WHERE  controller IS NULL AND [action] IS NULL and permissaopai_id is not null and area = 'Financeiro' and descricao = 'Básico';

DECLARE @FINANCEIROID AS BIGINT;
SET @FINANCEIROID = (SELECT id FROM permissao WHERE area = 'Financeiro' AND controller IS NULL AND [action] IS NULL AND [permissaopai_id] IS NULL);

-- Cria o submenu básicos
DECLARE @BASICOSID AS BIGINT;
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, ordem, permissaopai_id) VALUES (NULL, 'Financeiro', NULL, 'Básicos', 1, 0, 0, @FINANCEIROID);
SET @BASICOSID = SCOPE_IDENTITY()

-- Despesa/Receita
DECLARE @ID AS BIGINT;
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, ordem, permissaopai_id) VALUES ('Index', 'Financeiro', 'DespesaReceita', 'Despesa/Receita', 1, 1, 0, @BASICOSID);
SET @ID = SCOPE_IDENTITY()
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, ordem, permissaopai_id) VALUES ('Novo', 'Financeiro', 'DespesaReceita', 'Novo', 0, 1, 0, @ID);
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, ordem, permissaopai_id) VALUES ('Editar', 'Financeiro', 'DespesaReceita', 'Editar', 0, 1, 0, @ID);
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, ordem, permissaopai_id) VALUES ('Excluir', 'Financeiro', 'DespesaReceita', 'Excluir', 0, 1, 0, @ID);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, ordem, permissaopai_id) VALUES ('EditarSituacao', 'Financeiro', 'DespesaReceita', 'Editar situação', 0, 1, 0, @ID);