-- Requisição de Material / Baixar

DECLARE @REQUISICAOMATERIALID AS BIGINT, @ID AS BIGINT;
SET @REQUISICAOMATERIALID = (SELECT id FROM permissao WHERE area = 'Almoxarifado' AND controller = 'RequisicaoMaterial' AND [action] = 'Index');

-- Cria o item Baixar
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, Ordem, permissaopai_id) VALUES ('Baixar', 'Almoxarifado', 'RequisicaoMaterialBaixa', 'Baixar', 0, 1, 0, @REQUISICAOMATERIALID);