DECLARE @ENGENHARIAPRODUTOID AS BIGINT, @MODELOID AS BIGINT;
SET @ENGENHARIAPRODUTOID = (SELECT id FROM permissao WHERE area = 'EngenhariaProduto' AND controller IS NULL AND [action] IS NULL AND [permissaopai_id] IS NULL);

-- Cria o menu Relatórios
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, Ordem, permissaopai_id) VALUES (NULL, 'EngenhariaProduto', NULL, 'Relatórios', 1, 1, 0, @ENGENHARIAPRODUTOID);
SET @MODELOID = SCOPE_IDENTITY()

-- Listagem Modelos Aprovados
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, Ordem, permissaopai_id) VALUES ('ListagemModelosAprovados', 'EngenhariaProduto', 'Relatorio', 'Listagem Modelos Aprovados', 1, 1, 0, @MODELOID);

-- Consumo Material da Coleção
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, Ordem, permissaopai_id) VALUES ('ConsumoMaterialColecao', 'EngenhariaProduto', 'Relatorio', 'Consumo Material da Coleção', 1, 1, 0, @MODELOID);