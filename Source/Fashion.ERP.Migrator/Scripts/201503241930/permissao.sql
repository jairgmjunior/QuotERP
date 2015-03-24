-- Produção
--   Ordem Produção

DECLARE @PRODUCAOID AS BIGINT, @ID AS BIGINT;
SET @PRODUCAOID = (SELECT id FROM permissao WHERE area = 'Producao' AND controller IS NULL AND [action] IS NULL AND [permissaopai_id] IS NULL);

-- Cria o menu Ordem de Produção
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, Ordem, permissaopai_id) VALUES ('Index', 'Producao', 'OrdemProducao', 'Ordem Produção', 1, 1, 0, @PRODUCAOID);
SET @ID = SCOPE_IDENTITY()
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, Ordem, permissaopai_id) VALUES ('Novo', 'Producao', 'OrdemProducao', 'Novo', 0, 1, 0, @ID);
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, Ordem, permissaopai_id) VALUES ('Editar', 'Producao', 'OrdemProducao', 'Editar', 0, 1, 0, @ID);
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, Ordem, permissaopai_id) VALUES ('Excluir', 'Producao', 'OrdemProducao', 'Excluir', 0, 1, 0, @ID);