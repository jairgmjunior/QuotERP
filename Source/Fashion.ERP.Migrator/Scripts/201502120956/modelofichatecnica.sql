UPDATE
    modelo
SET
    modelo.fichatecnica_id = tabelax.fichatecnica_id    
FROM
    modelo
INNER JOIN
    (select modelo.id as modelo_id, fichatecnica.id as fichatecnica_id 
		from modelo, fichatecnica
		where fichatecnica.modelo_id = modelo.id) as tabelax
ON
    modelo.id = tabelax.modelo_id