DECLARE @IDPAI AS BIGINT, @ID AS BIGINT;
SET @IDPAI = (select permissaopai_id from permissao where descricao = 'Gestão de Compra');

SET @ID = (select id from permissao where descricao = 'Gestão de Compra');

delete from permissaotousuario where  permissao_id = @ID;
delete from permissaotousuario where  permissao_id = @IDPAI;
delete from permissao where  id = @ID;
delete from permissao where  id = @IDPAI;

DECLARE @RELATORIOID AS BIGINT, @IDCONSUMOMATERIAL as BIGINT;
SET @RELATORIOID = (SELECT id FROM PERMISSAO where descricao = 'Relatórios' and area ='EngenhariaProduto');
SET @IDCONSUMOMATERIAL = (SELECT id from permissao where descricao = 'Consumo Material Por Modelo' and permissaopai_id != @RELATORIOID);

delete from permissaotousuario where permissao_id = @IDCONSUMOMATERIAL;
delete from permissao where id = @IDCONSUMOMATERIAL;

----------------------------

drop VIEW [dbo].ExtratoItemEstoqueView
GO
CREATE VIEW [dbo].ExtratoItemEstoqueView
AS
SELECT     ROW_NUMBER() OVER (ORDER BY data) AS id, data, tipomovimentacao, material, qtdentrada, qtdSaida, depositomaterial
FROM         (SELECT     SUM(mem.quantidade) qtdentrada, 0 qtdSaida, ei.material_id AS material, e.depositomaterialdestino_id AS depositomaterial, e.dataentrada AS data,
                                              'e' AS tipomovimentacao
                       FROM          movimentacaoestoquematerial mem inner join (entradaitemmaterial ei INNER JOIN
                                              entradamaterial e ON e.id = ei.entradamaterial_id) ON mem.id = ei.movimentacaoestoquematerial_id
 
                       GROUP BY ei.material_id, e.depositomaterialdestino_id, e.dataentrada
                       UNION ALL
                       SELECT     0 qtdentrada, SUM(mem.quantidade) qtdSaida, si.material_id AS material, s.depositomaterialorigem_id AS depositomaterial, s.datasaida AS data,
                                             's' AS tipomovimentacao
                       FROM          movimentacaoestoquematerial mem inner join (saidaitemmaterial si INNER JOIN
                                             saidamaterial s ON s.id = si.saidamaterial_id) ON mem.id = si.movimentacaoestoquematerial_id
 
                       GROUP BY si.material_id, s.depositomaterialorigem_id, s.datasaida) AS extrato
GO