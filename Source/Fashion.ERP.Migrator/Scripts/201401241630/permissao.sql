-- Modelo
--	1. Manutenção
--	2. Aprovação
--	3. Criação Mix

DECLARE @ENGENHARIAPRODUTOID AS BIGINT, @MODELOID AS BIGINT;
SET @ENGENHARIAPRODUTOID = (SELECT id FROM permissao WHERE area = 'EngenhariaProduto' AND controller IS NULL AND [action] IS NULL AND [permissaopai_id] IS NULL);

-- Cria o submenu Modelo
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, Ordem, permissaopai_id) VALUES (NULL, 'EngenhariaProduto', NULL, 'Modelo', 1, 1, 0, @ENGENHARIAPRODUTOID);
SET @MODELOID = SCOPE_IDENTITY()

-- Atualiza o menu do cadastro do modelo (Manutenção)
UPDATE permissao SET permissaopai_id = @MODELOID, descricao = '1. Manutenção', ExibeNoMenu = 1 WHERE action = 'Index' AND area = 'EngenhariaProduto' AND controller = 'Modelo'

-- Cria o menu Aprovação
DECLARE @ID AS BIGINT;
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, Ordem, permissaopai_id) VALUES ('Index', 'EngenhariaProduto', 'AprovarModelo', '2. Aprovação', 1, 1, 0, @MODELOID);
SET @ID = SCOPE_IDENTITY()
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, Ordem, permissaopai_id) VALUES ('Aprovar', 'EngenhariaProduto', 'AprovarModelo', 'Aprovar', 0, 1, 0, @ID);
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, Ordem, permissaopai_id) VALUES ('Desaprovar', 'EngenhariaProduto', 'AprovarModelo', 'Desaprovar', 0, 1, 0, @ID);