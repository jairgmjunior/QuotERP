DECLARE @MODELOID AS BIGINT;
SET @MODELOID = (SELECT id FROM permissao WHERE action = 'Index' AND area = 'EngenhariaProduto' AND controller = 'Modelo');

-- Cria o menu Copiar modelo
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, Ordem, permissaopai_id) VALUES ('Copiar', 'EngenhariaProduto', 'Modelo', 'Copiar modelo', 0, 1, 0, @MODELOID);