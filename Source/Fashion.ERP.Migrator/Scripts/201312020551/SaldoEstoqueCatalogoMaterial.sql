CREATE PROCEDURE uspSaldoEstoqueCatalogoMaterial
	@IdCatalogoMaterial bigint,
	@IdDepositoMaterial bigint,
	@Data datetime
AS
BEGIN
	SET NOCOUNT ON;
	
	DECLARE @SaldoAtual float = (SELECT quantidade FROM estoquecatalogomaterial
		WHERE catalogomaterial_id = @IdCatalogoMaterial AND depositomaterial_id = @IdDepositoMaterial);

	DECLARE @TotalEntrada float = (SELECT COALESCE(SUM(ei.quantidade), 0)
		FROM entradaitemcatalogomaterial ei INNER JOIN entradacatalogomaterial e ON e.id = ei.entradacatalogomaterial_id
		WHERE ei.catalogomaterial_id = @IdCatalogoMaterial 
			AND e.depositomaterialdestino_id = @IdDepositoMaterial
			AND e.dataentrada > @Data);

    DECLARE @TotalSaida float = (SELECT COALESCE(SUM(si.quantidade), 0)
		FROM saidaitemcatalogomaterial si INNER JOIN saidacatalogomaterial s ON s.id = si.saidacatalogomaterial_id
		where si.catalogomaterial_id = @IdCatalogoMaterial 
			AND s.depositomaterialorigem_id = @IdDepositoMaterial
			AND s.datasaida > @Data);

	SELECT @SaldoAtual - @TotalEntrada + @TotalSaida;
END