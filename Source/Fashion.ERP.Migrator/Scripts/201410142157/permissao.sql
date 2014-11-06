-- Pagar
-- 1. Lançamento
-- 2. Baixa
-- 3. Previsão Despesas

DECLARE @FINANCEIROID AS BIGINT, @PAGARID AS BIGINT, @ID AS BIGINT;
SET @FINANCEIROID = (SELECT id FROM permissao WHERE area = 'Financeiro' AND controller IS NULL AND [action] IS NULL AND [permissaopai_id] IS NULL);

-- Cria o submenu Pagar
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, Ordem, permissaopai_id) VALUES (NULL, 'Financeiro', NULL, 'Pagar', 1, 1, 0, @FINANCEIROID);
SET @PAGARID = SCOPE_IDENTITY()

-- Cria o menu Contas a pagar
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, Ordem, permissaopai_id) VALUES ('Index', 'Financeiro', 'TituloPagar', 'Lançamento', 1, 1, 0, @PAGARID);
SET @ID = SCOPE_IDENTITY()
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, Ordem, permissaopai_id) VALUES ('Novo', 'Financeiro', 'TituloPagar', 'Novo', 0, 1, 0, @ID);
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, Ordem, permissaopai_id) VALUES ('Editar', 'Financeiro', 'TituloPagar', 'Editar', 0, 1, 0, @ID);
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, Ordem, permissaopai_id) VALUES ('Baixar', 'Financeiro', 'TituloPagar', 'Baixar', 0, 1, 0, @ID);