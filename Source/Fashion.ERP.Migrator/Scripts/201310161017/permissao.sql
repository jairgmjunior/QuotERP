DECLARE @ID AS BIGINT;

SET @ID = (SELECT Id FROM permissao WHERE action = 'Index' AND area = 'Comum' AND controller = 'Funcionario');
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id) VALUES ('EditarSituacao', 'Comum', 'Funcionario', 'Editar situação', 0, 1, @ID);

SET @ID = (SELECT Id FROM permissao WHERE action = 'Index' AND area = 'Comum' AND controller = 'Fornecedor');
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id) VALUES ('EditarSituacao', 'Comum', 'Fornecedor', 'Editar situação', 0, 1, @ID);

SET @ID = (SELECT Id FROM permissao WHERE action = 'Index' AND area = 'Comum' AND controller = 'PrestadorServico');
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id) VALUES ('EditarSituacao', 'Comum', 'PrestadorServico', 'Editar situação', 0, 1, @ID);

SET @ID = (SELECT Id FROM permissao WHERE action = 'Index' AND area = 'Comum' AND controller = 'Unidade');
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id) VALUES ('EditarSituacao', 'Comum', 'Unidade', 'Editar situação', 0, 1, @ID);

DECLARE @ENGENHARIAPRODUTOID AS BIGINT
SET @ENGENHARIAPRODUTOID = (SELECT id FROM permissao WHERE area = 'EngenhariaProduto' AND controller IS NULL AND [action] IS NULL AND [permissaopai_id] IS NULL);

-- Criar menu Básicos
DECLARE @BASICOSID AS BIGINT
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id) VALUES (NULL, 'EngenhariaProduto', NULL, 'Básicos', 1, 0, @ENGENHARIAPRODUTOID);
SET @BASICOSID = SCOPE_IDENTITY()

-- Mudar permissaopai_id
DECLARE @OLDID AS BIGINT
-- Artigo
SET @OLDID = (SELECT id FROM permissao WHERE action = 'Index' AND area = 'EngenhariaProduto' AND controller = 'Artigo');
DELETE FROM permissaotoperfildeacesso WHERE permissao_id = @OLDID;
DELETE FROM permissaotoperfildeacesso WHERE permissao_id in (SELECT id FROM permissao WHERE permissaopai_id = @OLDID);
DELETE FROM permissaotousuario WHERE permissao_id = @OLDID;
DELETE FROM permissaotousuario WHERE permissao_id  in (SELECT id FROM permissao WHERE permissaopai_id = @OLDID);
DELETE FROM permissao WHERE permissaopai_id= @OLDID;
DELETE FROM permissao WHERE id= @OLDID;
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id) VALUES ('Index', 'EngenhariaProduto', 'Artigo', 'Artigo', 1, 1, @BASICOSID);
SET @ID = SCOPE_IDENTITY()
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id) VALUES ('Novo', 'EngenhariaProduto', 'Artigo', 'Novo', 0, 1, @ID);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id) VALUES ('Editar', 'EngenhariaProduto', 'Artigo', 'Editar', 0, 1, @ID);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id) VALUES ('Excluir', 'EngenhariaProduto', 'Artigo', 'Excluir', 0, 1, @ID);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id) VALUES ('EditarSituacao', 'EngenhariaProduto', 'Artigo', 'Editar situação', 0, 1, @ID);

-- Barra
SET @OLDID = (SELECT id FROM permissao WHERE action = 'Index' AND area = 'EngenhariaProduto' AND controller = 'Barra');
DELETE FROM permissaotoperfildeacesso WHERE permissao_id = @OLDID;
DELETE FROM permissaotoperfildeacesso WHERE permissao_id in (SELECT id FROM permissao WHERE permissaopai_id = @OLDID);
DELETE FROM permissaotousuario WHERE permissao_id = @OLDID;
DELETE FROM permissaotousuario WHERE permissao_id  in (SELECT id FROM permissao WHERE permissaopai_id = @OLDID);
DELETE FROM permissao WHERE permissaopai_id= @OLDID;
DELETE FROM permissao WHERE id= @OLDID;
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id) VALUES ('Index', 'EngenhariaProduto', 'Barra', 'Tipo de barra', 1, 1, @BASICOSID);
SET @ID = SCOPE_IDENTITY()
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id) VALUES ('Novo', 'EngenhariaProduto', 'Barra', 'Novo', 0, 1, @ID);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id) VALUES ('Editar', 'EngenhariaProduto', 'Barra', 'Editar', 0, 1, @ID);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id) VALUES ('Excluir', 'EngenhariaProduto', 'Barra', 'Excluir', 0, 1, @ID);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id) VALUES ('EditarSituacao', 'EngenhariaProduto', 'Barra', 'Editar situação', 0, 1, @ID);

-- Classificação
SET @OLDID = (SELECT id FROM permissao WHERE action = 'Index' AND area = 'EngenhariaProduto' AND controller = 'Classificacao');
DELETE FROM permissaotoperfildeacesso WHERE permissao_id = @OLDID;
DELETE FROM permissaotoperfildeacesso WHERE permissao_id in (SELECT id FROM permissao WHERE permissaopai_id = @OLDID);
DELETE FROM permissaotousuario WHERE permissao_id = @OLDID;
DELETE FROM permissaotousuario WHERE permissao_id  in (SELECT id FROM permissao WHERE permissaopai_id = @OLDID);
DELETE FROM permissao WHERE permissaopai_id= @OLDID;
DELETE FROM permissao WHERE id= @OLDID;
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id) VALUES ('Index', 'EngenhariaProduto', 'Classificacao', 'Classificação', 1, 1, @BASICOSID);
SET @ID = SCOPE_IDENTITY()
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id) VALUES ('Novo', 'EngenhariaProduto', 'Classificacao', 'Novo', 0, 1, @ID);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id) VALUES ('Editar', 'EngenhariaProduto', 'Classificacao', 'Editar', 0, 1, @ID);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id) VALUES ('Excluir', 'EngenhariaProduto', 'Classificacao', 'Excluir', 0, 1, @ID);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id) VALUES ('EditarSituacao', 'EngenhariaProduto', 'Classificacao', 'Editar situação', 0, 1, @ID);

-- Comprimento
SET @OLDID = (SELECT id FROM permissao WHERE action = 'Index' AND area = 'EngenhariaProduto' AND controller = 'Comprimento');
DELETE FROM permissaotoperfildeacesso WHERE permissao_id = @OLDID;
DELETE FROM permissaotoperfildeacesso WHERE permissao_id in (SELECT id FROM permissao WHERE permissaopai_id = @OLDID);
DELETE FROM permissaotousuario WHERE permissao_id = @OLDID;
DELETE FROM permissaotousuario WHERE permissao_id  in (SELECT id FROM permissao WHERE permissaopai_id = @OLDID);
DELETE FROM permissao WHERE permissaopai_id= @OLDID;
DELETE FROM permissao WHERE id= @OLDID;
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id) VALUES ('Index', 'EngenhariaProduto', 'Comprimento', 'Comprimento', 1, 1, @BASICOSID);
SET @ID = SCOPE_IDENTITY()
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id) VALUES ('Novo', 'EngenhariaProduto', 'Comprimento', 'Novo', 0, 1, @ID);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id) VALUES ('Editar', 'EngenhariaProduto', 'Comprimento', 'Editar', 0, 1, @ID);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id) VALUES ('Excluir', 'EngenhariaProduto', 'Comprimento', 'Excluir', 0, 1, @ID);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id) VALUES ('EditarSituacao', 'EngenhariaProduto', 'Comprimento', 'Editar situação', 0, 1, @ID);

-- Grade
SET @OLDID = (SELECT id FROM permissao WHERE action = 'Index' AND area = 'EngenhariaProduto' AND controller = 'Grade');
DELETE FROM permissaotoperfildeacesso WHERE permissao_id = @OLDID;
DELETE FROM permissaotoperfildeacesso WHERE permissao_id in (SELECT id FROM permissao WHERE permissaopai_id = @OLDID);
DELETE FROM permissaotousuario WHERE permissao_id = @OLDID;
DELETE FROM permissaotousuario WHERE permissao_id  in (SELECT id FROM permissao WHERE permissaopai_id = @OLDID);
DELETE FROM permissao WHERE permissaopai_id= @OLDID;
DELETE FROM permissao WHERE id= @OLDID;
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id) VALUES ('Index', 'EngenhariaProduto', 'Grade', 'Grade', 1, 1, @BASICOSID);
SET @ID = SCOPE_IDENTITY()
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id) VALUES ('Novo', 'EngenhariaProduto', 'Grade', 'Novo', 0, 1, @ID);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id) VALUES ('Editar', 'EngenhariaProduto', 'Grade', 'Editar', 0, 1, @ID);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id) VALUES ('Excluir', 'EngenhariaProduto', 'Grade', 'Excluir', 0, 1, @ID);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id) VALUES ('EditarSituacao', 'EngenhariaProduto', 'Grade', 'Editar situação', 0, 1, @ID);

-- Natureza
SET @OLDID = (SELECT id FROM permissao WHERE action = 'Index' AND area = 'EngenhariaProduto' AND controller = 'Natureza');
DELETE FROM permissaotoperfildeacesso WHERE permissao_id = @OLDID;
DELETE FROM permissaotoperfildeacesso WHERE permissao_id in (SELECT id FROM permissao WHERE permissaopai_id = @OLDID);
DELETE FROM permissaotousuario WHERE permissao_id = @OLDID;
DELETE FROM permissaotousuario WHERE permissao_id  in (SELECT id FROM permissao WHERE permissaopai_id = @OLDID);
DELETE FROM permissao WHERE permissaopai_id= @OLDID;
DELETE FROM permissao WHERE id= @OLDID;
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id) VALUES ('Index', 'EngenhariaProduto', 'Natureza', 'Natureza', 1, 1, @BASICOSID);
SET @ID = SCOPE_IDENTITY()
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id) VALUES ('Novo', 'EngenhariaProduto', 'Natureza', 'Novo', 0, 1, @ID);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id) VALUES ('Editar', 'EngenhariaProduto', 'Natureza', 'Editar', 0, 1, @ID);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id) VALUES ('Excluir', 'EngenhariaProduto', 'Natureza', 'Excluir', 0, 1, @ID);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id) VALUES ('EditarSituacao', 'EngenhariaProduto', 'Natureza', 'Editar situação', 0, 1, @ID);

-- Produto Base
SET @OLDID = (SELECT id FROM permissao WHERE action = 'Index' AND area = 'EngenhariaProduto' AND controller = 'ProdutoBase');
DELETE FROM permissaotoperfildeacesso WHERE permissao_id = @OLDID;
DELETE FROM permissaotoperfildeacesso WHERE permissao_id in (SELECT id FROM permissao WHERE permissaopai_id = @OLDID);
DELETE FROM permissaotousuario WHERE permissao_id = @OLDID;
DELETE FROM permissaotousuario WHERE permissao_id  in (SELECT id FROM permissao WHERE permissaopai_id = @OLDID);
DELETE FROM permissao WHERE permissaopai_id= @OLDID;
DELETE FROM permissao WHERE id= @OLDID;
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id) VALUES ('Index', 'EngenhariaProduto', 'ProdutoBase', 'Produto base', 1, 1, @BASICOSID);
SET @ID = SCOPE_IDENTITY()
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id) VALUES ('Novo', 'EngenhariaProduto', 'ProdutoBase', 'Novo', 0, 1, @ID);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id) VALUES ('Editar', 'EngenhariaProduto', 'ProdutoBase', 'Editar', 0, 1, @ID);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id) VALUES ('Excluir', 'EngenhariaProduto', 'ProdutoBase', 'Excluir', 0, 1, @ID);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id) VALUES ('EditarSituacao', 'EngenhariaProduto', 'ProdutoBase', 'Editar situação', 0, 1, @ID);

-- Segmento
SET @OLDID = (SELECT id FROM permissao WHERE action = 'Index' AND area = 'EngenhariaProduto' AND controller = 'Segmento');
DELETE FROM permissaotoperfildeacesso WHERE permissao_id = @OLDID;
DELETE FROM permissaotoperfildeacesso WHERE permissao_id in (SELECT id FROM permissao WHERE permissaopai_id = @OLDID);
DELETE FROM permissaotousuario WHERE permissao_id = @OLDID;
DELETE FROM permissaotousuario WHERE permissao_id  in (SELECT id FROM permissao WHERE permissaopai_id = @OLDID);
DELETE FROM permissao WHERE permissaopai_id= @OLDID;
DELETE FROM permissao WHERE id= @OLDID;
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id) VALUES ('Index', 'EngenhariaProduto', 'Segmento', 'Segmento', 1, 1, @BASICOSID);
SET @ID = SCOPE_IDENTITY()
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id) VALUES ('Novo', 'EngenhariaProduto', 'Segmento', 'Novo', 0, 1, @ID);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id) VALUES ('Editar', 'EngenhariaProduto', 'Segmento', 'Editar', 0, 1, @ID);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id) VALUES ('Excluir', 'EngenhariaProduto', 'Segmento', 'Excluir', 0, 1, @ID);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id) VALUES ('EditarSituacao', 'EngenhariaProduto', 'Segmento', 'Editar situação', 0, 1, @ID);

-- Coleção (novo)
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id) VALUES ('Index', 'Comum', 'Colecao', 'Coleção', 1, 1, @BASICOSID);
SET @ID = SCOPE_IDENTITY()
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id) VALUES ('Novo', 'Comum', 'Colecao', 'Novo', 0, 1, @ID);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id) VALUES ('Editar', 'Comum', 'Colecao', 'Editar', 0, 1, @ID);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id) VALUES ('Excluir', 'Comum', 'Colecao', 'Excluir', 0, 1, @ID);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id) VALUES ('EditarSituacao', 'Comum', 'Colecao', 'Editar situação', 0, 1, @ID);

-- Funcionário (novo)
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id) VALUES ('Index', 'Comum', 'Funcionario', 'Funcionário', 1, 1, @BASICOSID);
SET @ID = SCOPE_IDENTITY()
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id) VALUES ('Novo', 'Comum', 'Funcionario', 'Novo', 0, 1, @ID);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id) VALUES ('Editar', 'Comum', 'Funcionario', 'Editar', 0, 1, @ID);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id) VALUES ('Excluir', 'Comum', 'Funcionario', 'Excluir', 0, 1, @ID);
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id) VALUES ('EditarSituacao', 'Comum', 'Funcionario', 'Editar situação', 0, 1, @ID);

-- Marca (novo)
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id) VALUES ('Index', 'Comum', 'Marca', 'Marca', 1, 1, @BASICOSID);
SET @ID = SCOPE_IDENTITY()
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id) VALUES ('Editar', 'Comum', 'Marca', 'Editar', 0, 1, @ID);
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id) VALUES ('Excluir', 'Comum', 'Marca', 'Excluir', 0, 1, @ID);
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id) VALUES ('Novo', 'Comum', 'Marca', 'Novo', 0, 1, @ID);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id) VALUES ('EditarSituacao', 'Comum', 'Marca', 'Editar situação', 0, 1, @ID);

-- Tamanho (novo)
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id) VALUES ('Index', 'Comum', 'Tamanho', 'Tamanho', 1, 1, @BASICOSID);
SET @ID = SCOPE_IDENTITY()
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id) VALUES ('Novo', 'Comum', 'Tamanho', 'Novo', 0, 1, @ID);
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id) VALUES ('Editar', 'Comum', 'Tamanho', 'Editar', 0, 1, @ID);
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id) VALUES ('Excluir', 'Comum', 'Tamanho', 'Excluir', 0, 1, @ID);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id) VALUES ('EditarSituacao', 'Comum', 'Tamanho', 'Editar situação', 0, 1, @ID);

-- Criar menu Fases Produtivas
DECLARE @FASESPRODUTIVASID AS BIGINT
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id) VALUES (NULL, 'EngenhariaProduto', NULL, 'Fases Produtivas', 1, 0, @ENGENHARIAPRODUTOID);
SET @FASESPRODUTIVASID = SCOPE_IDENTITY()

-- Mudar permissaopai_id 
-- 2. Setor Departamento
SET @OLDID = (SELECT id FROM permissao WHERE action = 'Index' AND area = 'EngenhariaProduto' AND controller = 'SetorProducao');
DELETE FROM permissaotoperfildeacesso WHERE permissao_id = @OLDID;
DELETE FROM permissaotoperfildeacesso WHERE permissao_id in (SELECT id FROM permissao WHERE permissaopai_id = @OLDID);
DELETE FROM permissaotousuario WHERE permissao_id = @OLDID;
DELETE FROM permissaotousuario WHERE permissao_id  in (SELECT id FROM permissao WHERE permissaopai_id = @OLDID);
DELETE FROM permissao WHERE permissaopai_id= @OLDID;
DELETE FROM permissao WHERE id= @OLDID;
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id) VALUES ('Index', 'EngenhariaProduto', 'SetorProducao', '2. Setor Departamento', 1, 1, @FASESPRODUTIVASID);
SET @ID = SCOPE_IDENTITY()
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id) VALUES ('Novo', 'EngenhariaProduto', 'SetorProducao', 'Novo', 0, 1, @ID);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id) VALUES ('Editar', 'EngenhariaProduto', 'SetorProducao', 'Editar', 0, 1, @ID);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id) VALUES ('Excluir', 'EngenhariaProduto', 'SetorProducao', 'Excluir', 0, 1, @ID);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id) VALUES ('EditarSituacao', 'EngenhariaProduto', 'SetorProducao', 'Editar situação', 0, 1, @ID);

-- 3. Operação Setor
SET @OLDID = (SELECT id FROM permissao WHERE action = 'Index' AND area = 'EngenhariaProduto' AND controller = 'OperacaoProducao');
DELETE FROM permissaotoperfildeacesso WHERE permissao_id = @OLDID;
DELETE FROM permissaotoperfildeacesso WHERE permissao_id in (SELECT id FROM permissao WHERE permissaopai_id = @OLDID);
DELETE FROM permissaotousuario WHERE permissao_id = @OLDID;
DELETE FROM permissaotousuario WHERE permissao_id  in (SELECT id FROM permissao WHERE permissaopai_id = @OLDID);
DELETE FROM permissao WHERE permissaopai_id= @OLDID;
DELETE FROM permissao WHERE id= @OLDID;
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id) VALUES ('Index', 'EngenhariaProduto', 'OperacaoProducao', '3. Operação Setor', 1, 1, @FASESPRODUTIVASID);
SET @ID = SCOPE_IDENTITY()
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id) VALUES ('Novo', 'EngenhariaProduto', 'OperacaoProducao', 'Novo', 0, 1, @ID);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id) VALUES ('Editar', 'EngenhariaProduto', 'OperacaoProducao', 'Editar', 0, 1, @ID);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id) VALUES ('Excluir', 'EngenhariaProduto', 'OperacaoProducao', 'Excluir', 0, 1, @ID);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id) VALUES ('EditarSituacao', 'EngenhariaProduto', 'OperacaoProducao', 'Editar situação', 0, 1, @ID);

-- Criar menu filho 1. Departamento Produção
insert into permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id) values ('Index', 'Comum', 'DepartamentoProducao', '1. Departamento Produção', 1, 1, @FASESPRODUTIVASID);
SET @ID = SCOPE_IDENTITY()
insert into permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id) values ('Novo', 'Comum', 'DepartamentoProducao', 'Novo', 0, 1, @ID);
insert into permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id) values ('Editar', 'Comum', 'DepartamentoProducao', 'Editar', 0, 1, @ID);
insert into permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id) values ('Excluir', 'Comum', 'DepartamentoProducao', 'Excluir', 0, 1, @ID);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id) VALUES ('EditarSituacao', 'Comum', 'DepartamentoProducao', 'Editar situação', 0, 1, @ID);

-- Atualiza o menu do Modelo
DECLARE @MODELOID AS BIGINT;
SET @MODELOID = (SELECT id FROM permissao WHERE action = 'Index' AND area = 'EngenhariaProduto' AND controller = 'Modelo');
-- Atualiza a descrição do modelo
UPDATE permissao SET descricao = 'Modelo' WHERE id = @MODELOID;
-- Editar
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id) VALUES ('Detalhar', 'EngenhariaProduto', 'Modelo', 'Detalhar', 0, 1, @MODELOID);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id) VALUES ('Variacao', 'EngenhariaProduto', 'Modelo', 'Variação', 0, 1, @MODELOID);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id) VALUES ('SequenciaProducao', 'EngenhariaProduto', 'Modelo', 'Sequência Produção', 0, 1, @MODELOID);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id) VALUES ('Modelagem', 'EngenhariaProduto', 'Modelo', 'Modelagem', 0, 1, @MODELOID);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id) VALUES ('Composicao', 'EngenhariaProduto', 'Modelo', 'Composição', 0, 1, @MODELOID);


DECLARE @COMUMID AS BIGINT
SET @COMUMID = (SELECT id FROM permissao WHERE area = 'Comum' AND controller IS NULL AND [action] IS NULL AND [permissaopai_id] IS NULL);

-- Comum -> Cor
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id) VALUES ('Index', 'Comum', 'Cor', 'Cor', 1, 1, @COMUMID);
SET @ID = SCOPE_IDENTITY()
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id) VALUES ('Novo', 'Comum', 'Cor', 'Novo', 0, 1, @ID);
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id) VALUES ('Editar', 'Comum', 'Cor', 'Editar', 0, 1, @ID);
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id) VALUES ('Excluir', 'Comum', 'Cor', 'Excluir', 0, 1, @ID);
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id) VALUES ('EditarSituacao', 'Comum', 'Cor', 'Editar situação', 0, 1, @ID);

-- EngenhariaProduto -> Básicos -> Cor
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id) VALUES ('Index', 'Comum', 'Cor', 'Cor', 1, 1, @BASICOSID);
SET @ID = SCOPE_IDENTITY()
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id) VALUES ('Novo', 'Comum', 'Cor', 'Novo', 0, 1, @ID);
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id) VALUES ('Editar', 'Comum', 'Cor', 'Editar', 0, 1, @ID);
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id) VALUES ('Excluir', 'Comum', 'Cor', 'Excluir', 0, 1, @ID);
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id) VALUES ('EditarSituacao', 'Comum', 'Cor', 'Editar situação', 0, 1, @ID);

-- Corrigir menu Tamanho
SET @ID = (SELECT Id FROM permissao WHERE Action = 'Index' AND Area = 'Comum' AND Controller = 'Tamanho' AND permissaopai_id = @COMUMID);
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id) VALUES ('EditarSituacao', 'Comum', 'Tamanho', 'EditarSituacao', 0, 1, @ID);
UPDATE permissao SET Action = 'Excluir', Descricao = 'Excluir' WHERE Action = 'Inativar' AND Controller = 'Tamanho';