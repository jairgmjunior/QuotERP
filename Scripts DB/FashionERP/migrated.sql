/* Using Database sqlserver2012 and Connection String server=.\SQLEXPRESS;Database=FashionERP;User Id=sa;Password=123456; */
/* 201505191523: Migration201505191523 migrating ============================= */

/* Beginning Transaction */
/* ExecuteEmbeddedSqlScript Fashion.ERP.Migrator.Scripts._201505191523.permissao.sql */

DECLARE @ESTOQUEMATERIALID AS BIGINT;

set @ESTOQUEMATERIALID = (select id from  [dbo].[permissao] where action = 'EstoqueMaterial' and controller = 'Consulta')


INSERT INTO [dbo].[permissao]
           ([descricao]
           ,[action]
           ,[area]
           ,[controller]
           ,[exibenomenu]
           ,[requerpermissao]
           ,[permissaopai_id]
           ,[ordem])
     VALUES
           ('Custo do Material'
           ,'CustoMaterial'
           ,'Almoxarifado'
           ,'Consulta'
           ,0
           ,1
           ,@ESTOQUEMATERIALID
           ,0
		   )     
  
INSERT INTO [dbo].[permissao]
           ([descricao]
           ,[action]
           ,[area]
           ,[controller]
           ,[exibenomenu]
           ,[requerpermissao]
           ,[permissaopai_id]
           ,[ordem])
     VALUES
           ('Extrato do Item'
           ,'ExtratoItem'
           ,'Almoxarifado'
           ,'Consulta'
           ,0
           ,1
           ,@ESTOQUEMATERIALID
           ,0
		   )     
  

INSERT INTO [dbo].[VersionInfo] ([Version], [AppliedOn], [Description]) VALUES (201505191523, '2015-06-08T13:21:33', 'Migration201505191523')
/* Committing Transaction */
/* 201505191523: Migration201505191523 migrated */

/* 201505251307: Migration201505251307 migrating ============================= */

/* Beginning Transaction */
/* ExecuteEmbeddedSqlScript Fashion.ERP.Migrator.Scripts._201505251307.modelomaterialconsumo.sql */
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
/* !!! An error occurred executing the following sql:
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
GO
The error was Já existe um objeto com nome 'modelomaterialconsumo' no banco de dados.
 */
/* Rolling back transaction */
