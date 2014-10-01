﻿-- Almoxarifado
update uniquekeys set nexthi = 2 where nexthi = 1 and tablename = 'permissao';
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id, id) VALUES (NULL, 'Almoxarifado', NULL, 'Almoxarifado', 1, 0, NULL, 101);
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id, id) VALUES ('Index', 'Almoxarifado', 'CatalogoMaterial', 'Catálogo de material', 1, 1, 101, 102);
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id, id) VALUES ('Editar', 'Almoxarifado', 'CatalogoMaterial', 'Editar', 0, 1, 102, 103);
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id, id) VALUES ('Excluir', 'Almoxarifado', 'CatalogoMaterial', 'Excluir', 0, 1, 102, 104);
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id, id) VALUES ('Novo', 'Almoxarifado', 'CatalogoMaterial', 'Novo', 0, 1, 102, 105);
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id, id) VALUES ('Index', 'Almoxarifado', 'Categoria', 'Categoria', 1, 1, 101, 106);
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id, id) VALUES ('Editar', 'Almoxarifado', 'Categoria', 'Editar', 0, 1, 106, 107);
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id, id) VALUES ('Excluir', 'Almoxarifado', 'Categoria', 'Excluir', 0, 1, 106, 108);
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id, id) VALUES ('Novo', 'Almoxarifado', 'Categoria', 'Novo', 0, 1, 106, 109);
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id, id) VALUES ('Index', 'Almoxarifado', 'Familia', 'Família', 1, 1, 101, 110);
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id, id) VALUES ('Editar', 'Almoxarifado', 'Familia', 'Editar', 0, 1, 110, 111);
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id, id) VALUES ('Excluir', 'Almoxarifado', 'Familia', 'Excluir', 0, 1, 110, 112);
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id, id) VALUES ('Novo', 'Almoxarifado', 'Familia', 'Novo', 0, 1, 110, 113);
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id, id) VALUES ('Index', 'Almoxarifado', 'Marca', 'Marca', 1, 1, 101, 114);
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id, id) VALUES ('Editar', 'Almoxarifado', 'Marca', 'Editar', 0, 1, 114, 115);
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id, id) VALUES ('Excluir', 'Almoxarifado', 'Marca', 'Excluir', 0, 1, 114, 116);
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id, id) VALUES ('Novo', 'Almoxarifado', 'Marca', 'Novo', 0, 1, 114, 117);
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id, id) VALUES ('Index', 'Almoxarifado', 'Subcategoria', 'Subcategoria', 1, 1, 101, 118);
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id, id) VALUES ('Editar', 'Almoxarifado', 'Subcategoria', 'Editar', 0, 1, 118, 119);
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id, id) VALUES ('Excluir', 'Almoxarifado', 'Subcategoria', 'Excluir', 0, 1, 118, 120);
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id, id) VALUES ('Novo', 'Almoxarifado', 'Subcategoria', 'Novo', 0, 1, 118, 121);
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id, id) VALUES ('Index', 'Almoxarifado', 'UnidadeMedida', 'Unidade de medida', 1, 1, 101, 122);
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id, id) VALUES ('Editar', 'Almoxarifado', 'UnidadeMedida', 'Editar', 0, 1, 122, 123);
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id, id) VALUES ('Excluir', 'Almoxarifado', 'UnidadeMedida', 'Excluir', 0, 1, 122, 124);
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id, id) VALUES ('Novo', 'Almoxarifado', 'UnidadeMedida', 'Novo', 0, 1, 122, 125);