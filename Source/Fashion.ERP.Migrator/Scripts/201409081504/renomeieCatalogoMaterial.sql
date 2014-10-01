sp_rename 'catalogomaterial','material';
GO
sp_rename 'PK_catalogomaterial','PK_material';
GO
sp_rename 'FK_catalogomaterial_bordado','FK_material_bordado';
GO
sp_rename 'FK_catalogomaterial_familia','FK_material_familia';
GO
sp_rename 'FK_catalogomaterial_foto','FK_material_foto';
GO
sp_rename 'FK_catalogomaterial_generofiscal','FK_material_generofiscal';
GO
sp_rename 'FK_catalogomaterial_marcamaterial','FK_material_marcamaterial';
GO
sp_rename 'FK_catalogomaterial_origemsituacaotributaria','FK_material_origemsituacaotributaria';
GO
sp_rename 'FK_catalogomaterial_subcategoria','FK_material_subcategoria';
GO
sp_rename 'FK_catalogomaterial_tecido','FK_material_tecido';
GO
sp_rename 'FK_catalogomaterial_tipoitem','FK_material_tipoitem';
GO
sp_rename 'FK_catalogomaterial_unidademedida','FK_material_unidademedida';
GO

sp_rename 'entradacatalogomaterial','entradamaterial';
GO
sp_rename 'PK_entradacatalogomaterial','PK_entradamaterial';
GO
sp_rename 'FK_entradacatalogomaterial_depositomaterialdestino','FK_entradamaterial_depositomaterialdestino';
GO
sp_rename 'FK_entradacatalogomaterial_depositomaterialorigem','FK_entradamaterial_depositomaterialorigem';
GO
sp_rename 'FK_entradacatalogomaterial_fornecedor','FK_entradamaterial_fornecedor';
GO

sp_rename 'entradaitemcatalogomaterial','entradaitemmaterial';
GO
sp_rename 'PK_entradaitemcatalogomaterial','PK_entradaitemmaterial';
GO
sp_rename 'FK_entradaitemcatalogomaterial_catalogomaterial','FK_entradaitemmaterial_material';
GO
sp_rename 'FK_entradaitemcatalogomaterial_entradacatalogomaterial','FK_entradaitemmaterial_entradamaterial';
GO
sp_rename 'FK_entradaitemcatalogomaterial_unidademedida','FK_entradaitemmaterial_unidademedida';
GO
sp_rename 'entradaitemmaterial.catalogomaterial_id', 'material_id' , 'COLUMN';
GO
sp_rename 'entradaitemmaterial.entradacatalogomaterial_id', 'entradamaterial_id' , 'COLUMN';
GO

sp_rename 'saidacatalogomaterial','saidamaterial';
GO
sp_rename 'PK_saidacatalogomaterial', 'PK_saidamaterial';
GO
sp_rename 'FK_saidacatalogomaterial_centrocusto','FK_saidamaterial_centrocusto';
GO
sp_rename 'FK_saidacatalogomaterial_depositomaterialdestino','FK_saidamaterial_depositomaterialdestino';
GO
sp_rename 'FK_saidacatalogomaterial_depositomaterialorigem','FK_saidamaterial_depositomaterialorigem';
GO

sp_rename 'saidaitemcatalogomaterial','saidaitemmaterial';
GO
sp_rename 'PK_saidaitemcatalogomaterial', 'PK_saidaitemmaterial';
GO
sp_rename 'saidaitemmaterial.catalogomaterial_id', 'material_id' , 'COLUMN';
GO
sp_rename 'saidaitemmaterial.saidacatalogomaterial_id', 'saidamaterial_id' , 'COLUMN';
GO
sp_rename 'FK_saidaitemcatalogomaterial_catalogomaterial','FK_saidaitemmaterial_material';
GO
sp_rename 'FK_saidaitemcatalogomaterial_saidacatalogomaterial','FK_saidaitemmaterial_saidamaterial';
GO

sp_rename 'estoquecatalogomaterial','estoquematerial';
GO
sp_rename 'PK_estoquecatalogomaterial', 'PK_estoquematerial';
GO
sp_rename 'estoquematerial.catalogomaterial_id', 'material_id' , 'COLUMN';
GO
sp_rename 'FK_estoquecatalogomaterial_catalogomaterial','FK_estoquematerial_material';
GO
sp_rename 'FK_estoquecatalogomaterial_depositomaterial','FK_estoquematerial_depositomaterial';
GO

sp_rename 'pedidocompraitem.catalogomaterial_id', 'material_id' , 'COLUMN';
GO
sp_rename 'FK_pedidocompraitem_catalogomaterial','FK_pedidocompraitem_material';
GO

sp_rename 'ordementradaitemcatalogomaterial','ordementradaitemmaterial';
GO
sp_rename 'PK_ordementradaitemcatalogomaterial', 'PK_ordementradaitemmaterial';
GO
sp_rename 'ordementradaitemmaterial.ordementradacatalogomaterial_id', 'ordementradamaterial_id' , 'COLUMN';
GO
sp_rename 'fk_ordementradaitemcatalogomaterial_ordementradacatalogomaterial','fk_ordementradaitemmaterial_ordementradamaterial';
GO
sp_rename 'fk_ordementradaitemcatalogomaterial_pedidocompraitem','fk_ordementradaitemmaterial_pedidocompraitem';
GO

sp_rename 'materialcomposicaomodelo.catalogomaterial_id', 'material_id' , 'COLUMN';
GO
sp_rename 'FK_materialcomposicaomodelo_catalogomaterial','FK_materialcomposicaomodelo_material';
GO

sp_rename 'ordementradacatalogomaterial', 'ordementradamaterial';
GO
sp_rename 'PK_ordementradacatalogomaterial', 'PK_ordementradamaterial';
GO
sp_rename 'ordementradacompra.ordementradacatalogomaterial_id', 'ordementradamaterial_id' , 'COLUMN';
GO
sp_rename 'fk_ordementradacompra_ordementradacatalogomaterial','fk_ordementradacompra_ordementradamaterial';
GO
sp_rename 'FK_ordementradacatalogomaterial_comprador','FK_ordementradamaterial_comprador';
GO

sp_rename 'referenciaexterna.catalogomaterial_id', 'material_id' , 'COLUMN';
GO
sp_rename 'FK_referenciaexterna_catalogomaterial','FK_referenciaexterna_material';
GO

drop VIEW [dbo].[ConsumoMaterialColecaoView];
GO
CREATE VIEW [dbo].[ConsumoMaterialColecaoView]
AS
SELECT        ROW_NUMBER() OVER (ORDER BY cm.referencia) AS id, cm.referencia, cm.descricao, um.sigla AS unidade, mcm.quantidade * COALESCE (ft.quantidadeproducao, 1) AS quantidade, 0 AS compras, 
COALESCE (ecm.quantidade, 0) AS estoque, 0 AS diferenca,  c.id AS colecao, (select colecao_id from fichatecnica where fichatecnica.id = ft.id) as ColecaoAprovada, scat.id AS subcategoria, cat.id AS categoria, f.id AS familia, ft.quantidadeproducao
FROM            dbo.material AS cm INNER JOIN
                         dbo.materialcomposicaomodelo AS mcm ON cm.id = mcm.material_id INNER JOIN
                         dbo.sequenciaproducao AS sp ON mcm.sequenciaproducao_id = sp.id INNER JOIN
                         dbo.modelo AS m ON sp.modelo_id = m.id INNER JOIN
                         dbo.colecao AS c ON m.colecao_id = c.id LEFT OUTER JOIN
                         dbo.estoquematerial AS ecm ON ecm.material_id = cm.id INNER JOIN
                         dbo.unidademedida AS um ON mcm.unidademedida_id = um.id INNER JOIN
                         dbo.subcategoria AS scat ON cm.subcategoria_id = scat.id INNER JOIN
                         dbo.categoria AS cat ON scat.categoria_id = cat.id INNER JOIN
                         dbo.familia AS f ON cm.familia_id = f.id LEFT OUTER JOIN
                         dbo.fichatecnica AS ft ON ft.modelo_id = m.id

GO

drop VIEW [dbo].[ExtratoItemEstoqueView];
GO
CREATE VIEW [dbo].[ExtratoItemEstoqueView]
AS
SELECT        ROW_NUMBER() OVER (ORDER BY data) AS id, data, tipomovimentacao, material, qtdentrada, qtdSaida, depositomaterial
FROM            (SELECT        SUM(ei.quantidade) qtdentrada, 0 qtdSaida, ei.material_id AS material, e.depositomaterialdestino_id AS depositomaterial, 
                                                    e.dataentrada AS data, 'e' AS tipomovimentacao
                          FROM            entradaitemmaterial ei INNER JOIN
                                                    entradamaterial e ON e.id = ei.entradamaterial_id
                          GROUP BY ei.material_id, e.depositomaterialdestino_id, e.dataentrada
                          UNION ALL
                          SELECT        0 qtdentrada, SUM(si.quantidade) qtdSaida, si.material_id AS material, s.depositomaterialorigem_id AS depositomaterial, 
                                                   s.datasaida AS data, 's' AS tipomovimentacao
                          FROM            saidaitemmaterial si INNER JOIN
                                                   saidamaterial s ON s.id = si.saidamaterial_id
                          GROUP BY si.material_id, s.depositomaterialorigem_id, s.datasaida) AS extrato

GO

Drop PROCEDURE [dbo].[uspSaldoEstoqueCatalogoMaterial];
GO
CREATE PROCEDURE [dbo].[uspSaldoEstoquematerial]
	@Idmaterial bigint,
	@IdDepositoMaterial bigint,
	@Data datetime
AS
BEGIN
	SET NOCOUNT ON;
	
	DECLARE @SaldoAtual float = (SELECT quantidade FROM estoquematerial
		WHERE material_id = @Idmaterial AND depositomaterial_id = @IdDepositoMaterial);
	DECLARE @TotalEntrada float = (SELECT COALESCE(SUM(ei.quantidade), 0)
		FROM entradaitemmaterial ei INNER JOIN entradamaterial e ON e.id = ei.entradamaterial_id
		WHERE ei.material_id = @Idmaterial 
			AND e.depositomaterialdestino_id = @IdDepositoMaterial
			AND e.dataentrada > @Data);
    DECLARE @TotalSaida float = (SELECT COALESCE(SUM(si.quantidade), 0)
		FROM saidaitemmaterial si INNER JOIN saidamaterial s ON s.id = si.saidamaterial_id
		where si.material_id = @Idmaterial 
			AND s.depositomaterialorigem_id = @IdDepositoMaterial
			AND s.datasaida > @Data);
	SELECT @SaldoAtual - @TotalEntrada + @TotalSaida;
END

GO