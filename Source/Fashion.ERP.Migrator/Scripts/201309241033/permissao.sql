DECLARE @ALMOXARIFADOID AS BIGINT, @ID AS BIGINT;
SET @ALMOXARIFADOID = (SELECT id FROM permissao WHERE area = 'Almoxarifado' AND controller IS NULL AND [action] IS NULL AND [permissaopai_id] IS NULL);

-- MarcaMaterial
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id) VALUES ('Index', 'Almoxarifado', 'MarcaMaterial', 'Marca do catálogo', 1, 1, @ALMOXARIFADOID);
SET @ID = SCOPE_IDENTITY()
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id) VALUES ('Editar', 'Almoxarifado', 'MarcaMaterial', 'Editar', 0, 1, @ID);
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id) VALUES ('Excluir', 'Almoxarifado', 'MarcaMaterial', 'Excluir', 0, 1, @ID);
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id) VALUES ('Novo', 'Almoxarifado', 'MarcaMaterial', 'Novo', 0, 1, @ID);
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id) VALUES ('EditarSituacao', 'Almoxarifado', 'MarcaMaterial', 'Editar situação', 0, 1, @ID);

SET @ID = (SELECT Id FROM permissao WHERE action = 'Index' AND area = 'EngenhariaProduto' AND controller = 'Artigo');
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id) VALUES ('Excluir', 'EngenhariaProduto', 'Artigo', 'Excluir', 0, 1, @ID);

SET @ID = (SELECT Id FROM permissao WHERE action = 'Index' AND area = 'EngenhariaProduto' AND controller = 'Natureza');
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id) VALUES ('Excluir', 'EngenhariaProduto', 'Natureza', 'Excluir', 0, 1, @ID);

SET @ID = (SELECT Id FROM permissao WHERE action = 'Index' AND area = 'EngenhariaProduto' AND controller = 'Barra');
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id) VALUES ('Excluir', 'EngenhariaProduto', 'Barra', 'Excluir', 0, 1, @ID);

SET @ID = (SELECT Id FROM permissao WHERE action = 'Index' AND area = 'EngenhariaProduto' AND controller = 'Comprimento');
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id) VALUES ('Excluir', 'EngenhariaProduto', 'Comprimento', 'Excluir', 0, 1, @ID);

SET @ID = (SELECT Id FROM permissao WHERE action = 'Index' AND area = 'EngenhariaProduto' AND controller = 'ProdutoBase');
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id) VALUES ('Excluir', 'EngenhariaProduto', 'ProdutoBase', 'Excluir', 0, 1, @ID);

SET @ID = (SELECT Id FROM permissao WHERE action = 'Index' AND area = 'EngenhariaProduto' AND controller = 'Grade');
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id) VALUES ('Excluir', 'EngenhariaProduto', 'Grade', 'Excluir', 0, 1, @ID);

SET @ID = (SELECT Id FROM permissao WHERE action = 'Index' AND area = 'EngenhariaProduto' AND controller = 'Classificacao');
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id) VALUES ('Excluir', 'EngenhariaProduto', 'Classificacao', 'Excluir', 0, 1, @ID);

SET @ID = (SELECT Id FROM permissao WHERE action = 'Index' AND area = 'EngenhariaProduto' AND controller = 'Segmento');
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id) VALUES ('Excluir', 'EngenhariaProduto', 'Segmento', 'Excluir', 0, 1, @ID);

SET @ID = (SELECT Id FROM permissao WHERE action = 'Index' AND area = 'EngenhariaProduto' AND controller = 'SetorProducao');
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id) VALUES ('Excluir', 'EngenhariaProduto', 'SetorProducao', 'Excluir', 0, 1, @ID);

SET @ID = (SELECT Id FROM permissao WHERE action = 'Index' AND area = 'EngenhariaProduto' AND controller = 'OperacaoProducao');
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id) VALUES ('Excluir', 'EngenhariaProduto', 'OperacaoProducao', 'Excluir', 0, 1, @ID);

SET @ID = (SELECT Id FROM permissao WHERE action = 'Index' AND area = 'Comum' AND controller = 'Marca');
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id) VALUES ('Excluir', 'Comum', 'Marca', 'Excluir', 0, 1, @ID);

SET @ID = (SELECT Id FROM permissao WHERE action = 'Index' AND area = 'Comum' AND controller = 'Colecao');
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id) VALUES ('Excluir', 'Comum', 'Colecao', 'Excluir', 0, 1, @ID);

SET @ID = (SELECT Id FROM permissao WHERE action = 'Index' AND area = 'Comum' AND controller = 'DepartamentoProducao');
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id) VALUES ('Excluir', 'Comum', 'DepartamentoProducao', 'Excluir', 0, 1, @ID);

-- Exclui as permissões para os cadastros de LinhaBordado, Linhapesponto e LinhaTravete
DELETE FROM permissaotoperfildeacesso 
	WHERE permissao_id in (SELECT Id FROM permissao WHERE area = 'EngenhariaProduto' AND controller = 'LinhaBordado')
	OR permissao_id in (SELECT Id FROM permissao WHERE area = 'EngenhariaProduto' AND controller = 'Linhapesponto')
	OR permissao_id in (SELECT Id FROM permissao WHERE area = 'EngenhariaProduto' AND controller = 'LinhaTravete');

DELETE FROM permissaotousuario 
	WHERE permissao_id in (SELECT Id FROM permissao WHERE area = 'EngenhariaProduto' AND controller = 'LinhaBordado')
	OR permissao_id in (SELECT Id FROM permissao WHERE area = 'EngenhariaProduto' AND controller = 'Linhapesponto')
	OR permissao_id in (SELECT Id FROM permissao WHERE area = 'EngenhariaProduto' AND controller = 'LinhaTravete');

DELETE FROM permissao WHERE area = 'EngenhariaProduto' AND controller = 'LinhaBordado';
DELETE FROM permissao WHERE area = 'EngenhariaProduto' AND controller = 'Linhapesponto';
DELETE FROM permissao WHERE area = 'EngenhariaProduto' AND controller = 'LinhaTravete';

-- Adicionar as permissões de 'editar situação' ao módulo de almoxarifaso
SET @ID = (SELECT Id FROM permissao WHERE action = 'Index' AND area = 'Almoxarifado' AND controller = 'CatalogoMaterial');
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id) VALUES ('EditarSituacao', 'Almoxarifado', 'CatalogoMaterial', 'Editar situação', 0, 1, @ID);

SET @ID = (SELECT Id FROM permissao WHERE action = 'Index' AND area = 'Almoxarifado' AND controller = 'Categoria');
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id) VALUES ('EditarSituacao', 'Almoxarifado', 'Categoria', 'Editar situação', 0, 1, @ID);

SET @ID = (SELECT Id FROM permissao WHERE action = 'Index' AND area = 'Almoxarifado' AND controller = 'Subcategoria');
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id) VALUES ('EditarSituacao', 'Almoxarifado', 'Subcategoria', 'Editar situação', 0, 1, @ID);

SET @ID = (SELECT Id FROM permissao WHERE action = 'Index' AND area = 'Almoxarifado' AND controller = 'Familia');
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id) VALUES ('EditarSituacao', 'Almoxarifado', 'Familia', 'Editar situação', 0, 1, @ID);

SET @ID = (SELECT Id FROM permissao WHERE action = 'Index' AND area = 'Almoxarifado' AND controller = 'UnidadeMedida');
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id) VALUES ('EditarSituacao', 'Almoxarifado', 'UnidadeMedida', 'Editar situação', 0, 1, @ID);