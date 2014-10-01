-- Atualiza o menu do Modelo
DECLARE @MODELOID AS BIGINT;
SET @MODELOID = (SELECT id FROM permissao WHERE action = 'Index' AND area = 'EngenhariaProduto' AND controller = 'Modelo');

DECLARE @ID AS BIGINT;
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, Ordem, permissaopai_id) VALUES ('ProgramacaoBordado', 'EngenhariaProduto', 'Modelo', 'Programação do bordado', 0, 1, 0, @MODELOID);
SET @ID = SCOPE_IDENTITY()
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, Ordem, permissaopai_id) VALUES ('NovoProgramacaoBordado', 'EngenhariaProduto', 'Modelo', 'Novo', 0, 1, 0, @ID);
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, Ordem, permissaopai_id) VALUES ('EditarProgramacaoBordado', 'EngenhariaProduto', 'Modelo', 'Editar', 0, 1, 0, @ID);
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, Ordem, permissaopai_id) VALUES ('ExcluirProgramacaoBordado', 'EngenhariaProduto', 'Modelo', 'Excluir', 0, 1, 0, @ID);