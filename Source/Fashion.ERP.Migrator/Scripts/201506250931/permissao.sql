DECLARE @ID AS BIGINT;

SET @ID = (select max(id) from modelomaterialconsumo)

UPDATE uniquekeys
SET nexthi= (@ID + 1)
WHERE tablename='modelomaterialconsumo';


DECLARE @PERMISSAOID AS BIGINT;
SET @PERMISSAOID = (select id from permissao where action = 'EsbocarCorte' and area = 'EngenhariaProduto');
DELETE FROM permissaotousuario WHERE permissao_id = @PERMISSAOID;
DELETE FROM permissaotoperfildeacesso WHERE permissao_id = @PERMISSAOID;
DELETE FROM permissao WHERE ID = @PERMISSAOID;

ALTER TABLE modeloaprovacao DROP CONSTRAINT FK_modeloaprovacao_modeloaprovacaomatrizcorte;
ALTER TABLE modeloaprovacao DROP COLUMN modeloaprovacaomatrizcorte_id;
drop table modeloaprovacaomatrizcorteitem;
drop table modeloaprovacaomatrizcorte;
