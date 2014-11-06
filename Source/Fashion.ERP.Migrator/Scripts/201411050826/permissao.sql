-- Receber
-- 1. Lançamento

DECLARE @FINANCEIROID AS BIGINT, @RECEBERID AS BIGINT, @ID AS BIGINT;
SET @FINANCEIROID = (SELECT id FROM permissao WHERE area = 'Financeiro' AND controller IS NULL AND [action] IS NULL AND [permissaopai_id] IS NULL);

-- Cria o submenu Receber
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, Ordem, permissaopai_id) VALUES (NULL, 'Financeiro', NULL, 'Receber', 1, 1, 0, @FINANCEIROID);
SET @RECEBERID = SCOPE_IDENTITY()

-- Cria o menu Contas a receber
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, Ordem, permissaopai_id) VALUES ('Index', 'Financeiro', 'TituloReceber', 'Lançamento', 1, 1, 0, @RECEBERID);
SET @ID = SCOPE_IDENTITY()
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, Ordem, permissaopai_id) VALUES ('Novo', 'Financeiro', 'TituloReceber', 'Novo', 0, 1, 0, @ID);
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, Ordem, permissaopai_id) VALUES ('Editar', 'Financeiro', 'TituloReceber', 'Editar', 0, 1, 0, @ID);
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, Ordem, permissaopai_id) VALUES ('Excluir', 'Financeiro', 'TituloReceber', 'Excluir', 0, 1, 0, @ID);
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, Ordem, permissaopai_id) VALUES ('Baixar', 'Financeiro', 'TituloReceber', 'Baixar', 0, 1, 0, @ID);

-- Seleciona o menu Contas a pagar, e insere o item Excluir
DECLARE @PAGARID AS BIGINT;
SET @PAGARID = (SELECT id FROM permissao WHERE area = 'Financeiro' AND controller = 'TituloPagar' AND [action] = 'Index' AND [permissaopai_id] IS NOT NULL AND exibenomenu = 1);
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, Ordem, permissaopai_id) VALUES ('Excluir', 'Financeiro', 'TituloPagar', 'Excluir', 0, 1, 0, @PAGARID);