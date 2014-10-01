DECLARE @ENGENHARIAPRODUTOID AS BIGINT, @ID AS BIGINT
SET @ENGENHARIAPRODUTOID = (SELECT id FROM permissao WHERE area = 'EngenhariaProduto' AND controller IS NULL AND [action] IS NULL AND [permissaopai_id] IS NULL);

-- Grade
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id) VALUES ('Index', 'EngenhariaProduto', 'Grade', 'Grade', 1, 1, @ENGENHARIAPRODUTOID);
SET @ID = SCOPE_IDENTITY()
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id) VALUES ('Novo', 'EngenhariaProduto', 'Grade', 'Novo', 0, 1, @ID);
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id) VALUES ('Editar', 'EngenhariaProduto', 'Grade', 'Editar', 0, 1, @ID);
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id) VALUES ('Inativar', 'EngenhariaProduto', 'Grade', 'Inativar', 0, 1, @ID);

-- Segmento
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id) VALUES ('Index', 'EngenhariaProduto', 'Segmento', 'Segmento', 1, 1, @ENGENHARIAPRODUTOID);
SET @ID = SCOPE_IDENTITY()
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id) VALUES ('Novo', 'EngenhariaProduto', 'Segmento', 'Novo', 0, 1, @ID);
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id) VALUES ('Editar', 'EngenhariaProduto', 'Segmento', 'Editar', 0, 1, @ID);
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id) VALUES ('Inativar', 'EngenhariaProduto', 'Segmento', 'Inativar', 0, 1, @ID);

-- LinhaBordado
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id) VALUES ('Index', 'EngenhariaProduto', 'LinhaBordado', 'Linha bordado', 1, 1, @ENGENHARIAPRODUTOID);
SET @ID = SCOPE_IDENTITY()
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id) VALUES ('Novo', 'EngenhariaProduto', 'LinhaBordado', 'Novo', 0, 1, @ID);
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id) VALUES ('Editar', 'EngenhariaProduto', 'LinhaBordado', 'Editar', 0, 1, @ID);
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id) VALUES ('Excluir', 'EngenhariaProduto', 'LinhaBordado', 'Excluir', 0, 1, @ID);

-- Linhapesponto
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id) VALUES ('Index', 'EngenhariaProduto', 'Linhapesponto', 'Linha pesponto', 1, 1, @ENGENHARIAPRODUTOID);
SET @ID = SCOPE_IDENTITY()
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id) VALUES ('Novo', 'EngenhariaProduto', 'Linhapesponto', 'Novo', 0, 1, @ID);
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id) VALUES ('Editar', 'EngenhariaProduto', 'Linhapesponto', 'Editar', 0, 1, @ID);
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id) VALUES ('Excluir', 'EngenhariaProduto', 'Linhapesponto', 'Excluir', 0, 1, @ID);

-- LinhaTravete
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id) VALUES ('Index', 'EngenhariaProduto', 'LinhaTravete', 'Linha travete', 1, 1, @ENGENHARIAPRODUTOID);
SET @ID = SCOPE_IDENTITY()
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id) VALUES ('Novo', 'EngenhariaProduto', 'LinhaTravete', 'Novo', 0, 1, @ID);
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id) VALUES ('Editar', 'EngenhariaProduto', 'LinhaTravete', 'Editar', 0, 1, @ID);
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id) VALUES ('Excluir', 'EngenhariaProduto', 'LinhaTravete', 'Excluir', 0, 1, @ID);

-- Modelo
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id) VALUES ('Index', 'EngenhariaProduto', 'Modelo', 'Criação do croqui', 1, 1, @ENGENHARIAPRODUTOID);
SET @ID = SCOPE_IDENTITY()
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id) VALUES ('Novo', 'EngenhariaProduto', 'Modelo', 'Novo', 0, 1, @ID);
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id) VALUES ('Editar', 'EngenhariaProduto', 'Modelo', 'Editar', 0, 1, @ID);
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id) VALUES ('Excluir', 'EngenhariaProduto', 'Modelo', 'Excluir', 0, 1, @ID);

-- Editar situação
UPDATE permissao SET descricao = 'Editar situação', action = 'EditarSituacao' WHERE action = 'Excluir' AND area = 'EngenhariaProduto' AND controller = 'Artigo';
UPDATE permissao SET descricao = 'Editar situação', action = 'EditarSituacao' WHERE action = 'Excluir' AND area = 'EngenhariaProduto' AND controller = 'Natureza';
UPDATE permissao SET descricao = 'Editar situação', action = 'EditarSituacao' WHERE action = 'Excluir' AND area = 'EngenhariaProduto' AND controller = 'Barra';
UPDATE permissao SET descricao = 'Editar situação', action = 'EditarSituacao' WHERE action = 'Excluir' AND area = 'EngenhariaProduto' AND controller = 'Comprimento';
UPDATE permissao SET descricao = 'Editar situação', action = 'EditarSituacao' WHERE action = 'Excluir' AND area = 'Almoxarifado' AND controller = 'Marca';
UPDATE permissao SET descricao = 'Editar situação', action = 'EditarSituacao' WHERE action = 'Excluir' AND area = 'EngenhariaProduto' AND controller = 'ProdutoBase';
UPDATE permissao SET descricao = 'Editar situação', action = 'EditarSituacao' WHERE action = 'Inativar' AND area = 'EngenhariaProduto' AND controller = 'Grade';
UPDATE permissao SET descricao = 'Editar situação', action = 'EditarSituacao' WHERE action = 'Excluir' AND area = 'Comum' AND controller = 'Colecao';
UPDATE permissao SET descricao = 'Editar situação', action = 'EditarSituacao' WHERE action = 'Excluir' AND area = 'EngenhariaProduto' AND controller = 'Classificacao';
UPDATE permissao SET descricao = 'Editar situação', action = 'EditarSituacao' WHERE action = 'Inativar' AND area = 'EngenhariaProduto' AND controller = 'Segmento';
UPDATE permissao SET descricao = 'Editar situação', action = 'EditarSituacao' WHERE action = 'Inativar' AND area = 'EngenhariaProduto' AND controller = 'SetorProducao';
UPDATE permissao SET descricao = 'Editar situação', action = 'EditarSituacao' WHERE action = 'Inativar' AND area = 'EngenhariaProduto' AND controller = 'OperacaoProducao';
UPDATE permissao SET descricao = 'Editar situação', action = 'EditarSituacao' WHERE action = 'Excluir' AND area = 'Comum' AND controller = 'DepartamentoProducao';

-- Muda cadastro de marcas e coleção para o menu Cadastros
DECLARE @CADASTROSID AS BIGINT;
SET @CADASTROSID = (SELECT id FROM permissao WHERE area = 'Comum' AND controller IS NULL AND [action] IS NULL AND [permissaopai_id] IS NULL);
UPDATE permissao SET area = 'Comum' WHERE area = 'Almoxarifado' AND controller = 'Marca';
UPDATE permissao SET permissaopai_id = @CADASTROSID WHERE action = 'Index' AND area = 'Comum' AND controller = 'Marca';
UPDATE permissao SET permissaopai_id = @CADASTROSID WHERE action = 'Index' AND area = 'Comum' AND controller = 'Colecao';