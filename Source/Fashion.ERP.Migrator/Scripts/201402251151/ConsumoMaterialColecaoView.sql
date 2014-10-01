﻿CREATE VIEW [dbo].[ConsumoMaterialColecaoView]
AS
SELECT        ROW_NUMBER() OVER (ORDER BY cm.referencia) AS id, cm.referencia, cm.descricao, um.sigla AS unidade, mcm.quantidade, 0 AS compras, COALESCE (ecm.quantidade, 0) AS estoque, 0 AS diferenca, c.id AS colecao, 
                         scat.id AS subcategoria, cat.id AS categoria, f.id AS familia
FROM            dbo.catalogomaterial AS cm INNER JOIN
                         dbo.materialcomposicaomodelo AS mcm ON cm.id = mcm.catalogomaterial_id INNER JOIN
                         dbo.sequenciaproducao AS sp ON mcm.sequenciaproducao_id = sp.id INNER JOIN
                         dbo.modelo AS m ON sp.modelo_id = m.id INNER JOIN
                         dbo.colecao AS c ON m.colecao_id = c.id LEFT OUTER JOIN
                         dbo.estoquecatalogomaterial AS ecm ON ecm.catalogomaterial_id = cm.id INNER JOIN
                         dbo.unidademedida AS um ON cm.unidademedida_id = um.id INNER JOIN
                         dbo.subcategoria AS scat ON cm.subcategoria_id = scat.id INNER JOIN
                         dbo.categoria AS cat ON scat.categoria_id = cat.id INNER JOIN
                         dbo.familia AS f ON cm.familia_id = f.id