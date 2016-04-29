DECLARE @FICHATECNCIAINDEXID AS BIGINT;
SET @FICHATECNCIAINDEXID = (SELECT id FROM permissao WHERE area = 'Producao' AND controller ='FichaTecnica' AND [action] = 'Index');

update permissao set permissaopai_id = @FICHATECNCIAINDEXID where controller = 'FichaTecnica' and action <> 'Index'

INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, ordem, permissaopai_id) VALUES ('Fotos', 'Producao', 'FichaTecnica', 'Fotos', 0, 1, 0, @FICHATECNCIAINDEXID);
