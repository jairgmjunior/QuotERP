namespace Fashion.ERP.Domain.Almoxarifado
{
    public struct StoredProcedure
    {
        /// <summary>
        /// Retorna a quantidade de itens no estoque em determinada data.
        /// Parâmetros:
        /// @IdMaterial bigint,
	    /// @IdDepositoMaterial bigint,
	    /// @Data datetime
        /// </summary>
        public static string SaldoEstoqueMaterial = "SaldoEstoqueMaterial";
    }
}