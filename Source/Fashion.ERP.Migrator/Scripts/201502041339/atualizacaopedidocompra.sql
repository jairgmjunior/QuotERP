UPDATE
    pedidocompra
SET
    pedidocompra.valormercadoria = tabelax.valortotalitem    
FROM
    pedidocompra
INNER JOIN
    (select sum((pedidocompraitem.ValorUnitario * pedidocompraitem.Quantidade) - ISNULL(pedidocompraitem.valordesconto, 0)) as valortotalitem, pedidocompra_id
from pedidocompraitem , pedidocompra where pedidocompra_id = pedidocompra.id 
group by pedidocompra_id) as tabelax
ON
    pedidocompra.id = tabelax.pedidocompra_id


UPDATE
    pedidocompra
SET
    pedidocompra.valordesconto = tabelax.valordesconto    
FROM
    pedidocompra
INNER JOIN
    (select SUM(ISNULL(pedidocompraitem.valordesconto, 0)) as valordesconto, pedidocompra_id
from pedidocompraitem , pedidocompra where pedidocompra_id = pedidocompra.id 
group by pedidocompra_id) as tabelax
ON
    pedidocompra.id = tabelax.pedidocompra_id


UPDATE
    pedidocompra
SET
    valorcompra = (valormercadoria + ISNULL(valorencargos, 0) + valorfrete + ISNULL(valorembalagem, 0)) - valordesconto    