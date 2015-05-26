CREATE TABLE modelomaterialconsumo(
	[id] [bigint] NOT NULL,
	[idempresa] [bigint] NOT NULL,
	[idtenant] [bigint] NOT NULL,	
	[quantidade] [float] NULL,					
	[material_id] [bigint]  NULL,
	[unidademedida_id] [bigint]  NULL,
	[departamentoproducao_id] [bigint] NULL,	
	[modelo_id] [bigint] NULL,	
	CONSTRAINT [PK_modelomaterialconsumo] PRIMARY KEY (id)
 )
GO

ALTER TABLE modelomaterialconsumo  WITH CHECK ADD  CONSTRAINT [FK_modelomaterialconsumo_material] FOREIGN KEY([material_id])
REFERENCES [dbo].[material] ([id])
GO

ALTER TABLE modelomaterialconsumo  WITH CHECK ADD  CONSTRAINT [FK_modelomaterialconsumo_unidademedida] FOREIGN KEY([unidademedida_id])
REFERENCES [dbo].[unidademedida] ([id])
GO

ALTER TABLE modelomaterialconsumo  WITH CHECK ADD  CONSTRAINT [FK_modelomaterialconsumo_departamentoproducao] FOREIGN KEY([departamentoproducao_id])
REFERENCES [dbo].[departamentoproducao] ([id])
GO

ALTER TABLE modelomaterialconsumo  WITH CHECK ADD  CONSTRAINT [FK_modelomaterialconsumo_modelo] FOREIGN KEY([modelo_id])
REFERENCES [dbo].[modelo] ([id])
GO

update permissao set descricao = 'Materiais de Consumo', action = 'ModeloMaterialConsumo', controller = 'ModeloMaterialConsumo' where controller = 'MaterialComposicaoModelo'


INSERT INTO modelomaterialconsumo
(id, idempresa, idtenant, quantidade, material_id, unidademedida_id, departamentoproducao_id, modelo_id)
SELECT materialcomposicaomodelo.id, 0, 0, quantidade, material_id, unidademedida_id, departamentoproducao_id, modelo_id
FROM materialcomposicaomodelo, sequenciaproducao where sequenciaproducao_id = sequenciaproducao.id;

drop table materialcomposicaomodelo;

update uniquekeys set nexthi = (select nexthi from uniquekeys where tablename = 'materialcomposicaomodelo') where tablename = 'modelomaterialconsumo';