DECLARE @ALMOXARIFADOID AS BIGINT, @ID AS BIGINT, @BASICOSID AS BIGINT, @CADASTROID AS BIGINT, @MOVIMENTOID AS BIGINT, @CONSULTAID AS BIGINT;
SET @ALMOXARIFADOID = (SELECT id FROM permissao WHERE area = 'Almoxarifado' AND controller IS NULL AND [action] IS NULL AND [permissaopai_id] IS NULL);

-- Basicos
-- Mover: Categoria, Família, Marca, SubCategoria, Unidade de Medida
-- Copiar: Fornecedor
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, Ordem, permissaopai_id) VALUES (NULL, 'Almoxarifado', NULL, 'Básicos', 1, 1, 0, @ALMOXARIFADOID);
SET @BASICOSID = SCOPE_IDENTITY();
-- Categoria
SET @ID = (SELECT id FROM permissao WHERE area = 'Almoxarifado' AND controller = 'Categoria' AND [action] = 'Index' AND [permissaopai_id] = @ALMOXARIFADOID);
UPDATE permissao SET [permissaopai_id] = @BASICOSID WHERE id = @ID;
-- Família
SET @ID = (SELECT id FROM permissao WHERE area = 'Almoxarifado' AND controller = 'Familia' AND [action] = 'Index' AND [permissaopai_id] = @ALMOXARIFADOID);
UPDATE permissao SET [permissaopai_id] = @BASICOSID WHERE id = @ID;
-- Marca
SET @ID = (SELECT id FROM permissao WHERE area = 'Almoxarifado' AND controller = 'MarcaMaterial' AND [action] = 'Index' AND [permissaopai_id] = @ALMOXARIFADOID);
UPDATE permissao SET [permissaopai_id] = @BASICOSID WHERE id = @ID;
-- SubCategoria
SET @ID = (SELECT id FROM permissao WHERE area = 'Almoxarifado' AND controller = 'Subcategoria' AND [action] = 'Index' AND [permissaopai_id] = @ALMOXARIFADOID);
UPDATE permissao SET [permissaopai_id] = @BASICOSID WHERE id = @ID;
-- Unidade de Medida
SET @ID = (SELECT id FROM permissao WHERE area = 'Almoxarifado' AND controller = 'UnidadeMedida' AND [action] = 'Index' AND [permissaopai_id] = @ALMOXARIFADOID);
UPDATE permissao SET [permissaopai_id] = @BASICOSID WHERE id = @ID;
-- Fornecedor
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, Ordem, permissaopai_id) VALUES ('Index', 'Comum', 'Fornecedor', 'Fornecedor', 1, 1, 0, @BASICOSID);
SET @ID = SCOPE_IDENTITY()
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, Ordem, permissaopai_id) VALUES ('Novo', 'Comum', 'Fornecedor', 'Novo', 0, 1, 0, @ID);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, Ordem, permissaopai_id) VALUES ('Editar', 'Comum', 'Fornecedor', 'Editar', 0, 1, 0, @ID);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, Ordem, permissaopai_id) VALUES ('Excluir', 'Comum', 'Fornecedor', 'Excluir', 0, 1, 0, @ID);
-- Centro de custos
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, Ordem, permissaopai_id) VALUES ('Index', 'Comum', 'CentroCusto', 'Centro de Custo', 1, 1, 0, @BASICOSID);
SET @ID = SCOPE_IDENTITY()
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, Ordem, permissaopai_id) VALUES ('Novo', 'Comum', 'CentroCusto', 'Novo', 0, 1, 0, @ID);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, Ordem, permissaopai_id) VALUES ('Editar', 'Comum', 'CentroCusto', 'Editar', 0, 1, 0, @ID);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, Ordem, permissaopai_id) VALUES ('Excluir', 'Comum', 'CentroCusto', 'Excluir', 0, 1, 0, @ID);
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, Ordem, permissaopai_id) VALUES ('EditarSituacao', 'Comum', 'CentroCusto', 'Editar situação', 0, 1, 0, @ID);

-- Cadastro
-- Catalogo de Material
-- Novo: Depósito de Material
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, Ordem, permissaopai_id) VALUES (NULL, 'Almoxarifado', NULL, 'Cadastro', 1, 1, 0, @ALMOXARIFADOID);
SET @CADASTROID = SCOPE_IDENTITY();
-- CatalogoMaterial
SET @ID = (SELECT id FROM permissao WHERE area = 'Almoxarifado' AND controller = 'CatalogoMaterial' AND [action] = 'Index' AND [permissaopai_id] = @ALMOXARIFADOID);
UPDATE permissao SET [permissaopai_id] = @CADASTROID WHERE id = @ID;
-- DepositoMaterial
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, Ordem, permissaopai_id) VALUES ('Index', 'Almoxarifado', 'DepositoMaterial', 'Depósito de material', 1, 1, 0, @CADASTROID);
SET @ID = SCOPE_IDENTITY();
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, Ordem, permissaopai_id) VALUES ('Editar', 'Almoxarifado', 'DepositoMaterial', 'Editar', 0, 1, 0, @ID);
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, Ordem, permissaopai_id) VALUES ('Excluir', 'Almoxarifado', 'DepositoMaterial', 'Excluir', 0, 1, 0, @ID);
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, Ordem, permissaopai_id) VALUES ('Novo', 'Almoxarifado', 'DepositoMaterial', 'Novo', 0, 1, 0, @ID);
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, Ordem, permissaopai_id) VALUES ('EditarSituacao', 'Almoxarifado', 'DepositoMaterial', 'Editar situação', 0, 1, 0, @ID);

-- Movimentação Estoque
-- Novo: Entrada, Saída
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, Ordem, permissaopai_id) VALUES (NULL, 'Almoxarifado', NULL, 'Movimentação Estoque', 1, 1, 0, @ALMOXARIFADOID);
SET @MOVIMENTOID = SCOPE_IDENTITY();
-- EntradaCatalogoMaterial
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, Ordem, permissaopai_id) VALUES ('Index', 'Almoxarifado', 'EntradaCatalogoMaterial', 'Entrada de mercadoria', 1, 1, 0, @MOVIMENTOID);
SET @ID = SCOPE_IDENTITY();
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, Ordem, permissaopai_id) VALUES ('Editar', 'Almoxarifado', 'EntradaCatalogoMaterial', 'Editar', 0, 1, 0, @ID);
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, Ordem, permissaopai_id) VALUES ('Excluir', 'Almoxarifado', 'EntradaCatalogoMaterial', 'Excluir', 0, 1, 0, @ID);
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, Ordem, permissaopai_id) VALUES ('Novo', 'Almoxarifado', 'EntradaCatalogoMaterial', 'Novo', 0, 1, 0, @ID);
-- SaidaCatalogoMaterial
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, Ordem, permissaopai_id) VALUES ('Index', 'Almoxarifado', 'SaidaCatalogoMaterial', 'Saída de mercadoria', 1, 1, 0, @MOVIMENTOID);
SET @ID = SCOPE_IDENTITY();
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, Ordem, permissaopai_id) VALUES ('Editar', 'Almoxarifado', 'SaidaCatalogoMaterial', 'Editar', 0, 1, 0, @ID);
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, Ordem, permissaopai_id) VALUES ('Excluir', 'Almoxarifado', 'SaidaCatalogoMaterial', 'Excluir', 0, 1, 0, @ID);
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, Ordem, permissaopai_id) VALUES ('Novo', 'Almoxarifado', 'SaidaCatalogoMaterial', 'Novo', 0, 1, 0, @ID);
-- Relatório\Consulta
-- Novo: Curva ABC, Extrato do Item, Giro Estoque, Saldo
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, Ordem, permissaopai_id) VALUES (NULL, 'Almoxarifado', NULL, 'Relatório\Consulta', 1, 1, 0, @ALMOXARIFADOID);
SET @CONSULTAID = SCOPE_IDENTITY();
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, Ordem, permissaopai_id) VALUES ('EstoqueMaterial', 'Almoxarifado', 'Consulta', 'Estoque de material', 1, 1, 0, @CONSULTAID);