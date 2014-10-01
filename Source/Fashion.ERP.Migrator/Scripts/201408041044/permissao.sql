
-- Obtem o ID do menu Relatórios

DECLARE @RELATORIOID AS BIGINT;
SET @RELATORIOID = (SELECT ID FROM permissao WHERE area = 'EngenhariaProduto' AND controller IS NULL AND descricao ='Relatórios' AND [action] IS NULL AND [permissaopai_id] IS NOT NULL);

-- Listagem Modelos Aprovados
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, Ordem, permissaopai_id) VALUES ('ListagemModelos', 'EngenhariaProduto', 'Relatorio', 'Listagem de Modelos', 1, 1, 0, @RELATORIOID);
