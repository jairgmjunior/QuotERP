-- Requisição de Material / Cancelamento

DECLARE @REQUISICAOMATERIALID AS BIGINT, @ID AS BIGINT;
SET @REQUISICAOMATERIALID = (SELECT id FROM permissao WHERE area = 'Almoxarifado' AND controller = 'RequisicaoMaterial' AND [action] = 'Index');

-- Cria o item Cancelar
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, Ordem, permissaopai_id) VALUES ('Cancelar', 'Almoxarifado', 'RequisicaoMaterialCancelamento', 'Cancelar', 0, 1, 0, @REQUISICAOMATERIALID);