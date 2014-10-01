CREATE VIEW [dbo].[ExtratoItemEstoqueView]
AS
SELECT        ROW_NUMBER() OVER (ORDER BY data) AS id, data, tipomovimentacao, catalogomaterial, qtdentrada, qtdSaida, depositomaterial
FROM            (SELECT        SUM(ei.quantidade) qtdentrada, 0 qtdSaida, ei.catalogomaterial_id AS catalogomaterial, e.depositomaterialdestino_id AS depositomaterial, 
                                                    e.dataentrada AS data, 'e' AS tipomovimentacao
                          FROM            entradaitemcatalogomaterial ei INNER JOIN
                                                    entradacatalogomaterial e ON e.id = ei.entradacatalogomaterial_id
                          GROUP BY ei.catalogomaterial_id, e.depositomaterialdestino_id, e.dataentrada
                          UNION ALL
                          SELECT        0 qtdentrada, SUM(si.quantidade) qtdSaida, si.catalogomaterial_id AS catalogomaterial, s.depositomaterialorigem_id AS depositomaterial, 
                                                   s.datasaida AS data, 's' AS tipomovimentacao
                          FROM            saidaitemcatalogomaterial si INNER JOIN
                                                   saidacatalogomaterial s ON s.id = si.saidacatalogomaterial_id
                          GROUP BY si.catalogomaterial_id, s.depositomaterialorigem_id, s.datasaida) AS extrato