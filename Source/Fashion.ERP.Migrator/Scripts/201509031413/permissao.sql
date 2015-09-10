DECLARE @PERMISSAOID AS BIGINT;
SET @PERMISSAOID = (select id from permissao where action = 'ConsumoMaterialColecao' and area = 'EngenhariaProduto');
DELETE FROM permissaotousuario WHERE permissao_id = @PERMISSAOID;
DELETE FROM permissaotoperfildeacesso WHERE permissao_id = @PERMISSAOID;
DELETE FROM permissao WHERE ID = @PERMISSAOID;

SET @PERMISSAOID = (select id from permissao where action = 'MateriaisModelosAprovados' and area = 'EngenhariaProduto');
DELETE FROM permissaotousuario WHERE permissao_id = @PERMISSAOID;
DELETE FROM permissaotoperfildeacesso WHERE permissao_id = @PERMISSAOID;
DELETE FROM permissao WHERE ID = @PERMISSAOID;

DECLARE @PRODUCAOID AS BIGINT, @RELATORIOID AS BIGINT, @ID AS BIGINT;
SET @PRODUCAOID = (select id from permissao where area = 'Producao' and descricao = 'Produção');

SET @RELATORIOID = (select id from permissao where area = 'Producao' and descricao = 'Relatórios');

INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, Ordem, permissaopai_id) 
VALUES ('MateriaisProgramacaoProducao', 'Producao', 'RelatorioMateriaisProgramacaoProducao', 'Materiais da Programação de Produção',1 ,1,0, @RELATORIOID);	

INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, Ordem, permissaopai_id) 
VALUES ('ConsumoMaterialProgramado', 'Producao', 'RelatorioConsumoMaterialProgramado', 'Consumo de Material Programado',1 ,1,0, @RELATORIOID);	