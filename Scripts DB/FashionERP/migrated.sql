/* Using Database sqlserver2012 and Connection String server=.\SQLEXPRESS;Database=FashionERP;User Id=sa;Password=123456; */
/* 201501231346: Migration201501231346 migrating ============================= */

/* Beginning Transaction */
/* CreateTable simboloconservacao */
CREATE TABLE [dbo].[simboloconservacao] ([id] BIGINT NOT NULL, [descricao] NVARCHAR(100) NOT NULL, [categoriaconservacao] NVARCHAR(255) NOT NULL, [foto_id] BIGINT NOT NULL, CONSTRAINT [PK_simboloconservacao] PRIMARY KEY ([id]))

/* ExecuteEmbeddedSqlScript Fashion.ERP.Migrator.Scripts._201501231346.permissao.sql */
DECLARE @BASICOID AS BIGINT, @id_pai AS BIGINT;
SET @BASICOID = (SELECT id FROM permissao WHERE area = 'Almoxarifado' AND controller is null AND descricao='Básicos');

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
           ('Símbolo Conservação'
           ,'Index'
           ,'Almoxarifado'
           ,'SimboloConservacao'
           ,1
           ,1
           ,@BASICOID
           ,0
		   )
		   SET @id_pai = SCOPE_IDENTITY()

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
           ('Novo'
           ,'Novo'
           ,'Almoxarifado'
           ,'SimboloConservacao'
           ,0
           ,1
           ,@id_pai
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
           ('Editar'
           ,'Editar'
           ,'Almoxarifado'
           ,'SimboloConservacao'
           ,0
           ,1
           ,@id_pai
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
           ('Excluir'
           ,'Excluir'
           ,'Almoxarifado'
           ,'SimboloConservacao'
           ,0
           ,1
           ,@id_pai
           ,0
		   )



INSERT INTO [dbo].[VersionInfo] ([Version], [AppliedOn], [Description]) VALUES (201501231346, '2015-01-29T13:10:28', 'Migration201501231346')
/* Committing Transaction */
/* 201501231346: Migration201501231346 migrated */

/* Task completed. */
