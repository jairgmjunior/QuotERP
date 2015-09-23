DECLARE @PRODUCAOID AS BIGINT, @FICHATECNICAID AS BIGINT, @ID AS BIGINT;
SET @PRODUCAOID = (select id from permissao where area = 'Producao' and descricao = 'Produção');

SET @FICHATECNICAID = (select id from permissao where area = 'Producao' and descricao = 'Ficha Técnica');

INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, Ordem, permissaopai_id) 
VALUES ('FichaTecnicaEstimativaCusto', 'Producao', 'RelatorioFichaTecnicaEstimativaCusto', 'Estimativa de Custo',1 ,1,0, @FICHATECNICAID);	