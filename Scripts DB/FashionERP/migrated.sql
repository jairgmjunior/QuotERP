/* Using Database sqlserver2012 and Connection String server=.\SQLEXPRESS;Database=FashionERP;User Id=sa;Password=123456; */
/* 201502041339: Migration201502041339 migrating ============================= */

/* Beginning Transaction */
/* AlterTable pedidocompra */
/* No SQL statement executed. */

/* CreateColumn pedidocompra dataalteracao DateTime */
ALTER TABLE [dbo].[pedidocompra] ADD [dataalteracao] DATETIME NOT NULL CONSTRAINT [DF_pedidocompra_dataalteracao] DEFAULT '2015-02-05T11:10:26'

/* AlterTable entradamaterial */
/* No SQL statement executed. */

/* CreateColumn entradamaterial dataalteracao DateTime */
ALTER TABLE [dbo].[entradamaterial] ADD [dataalteracao] DATETIME NOT NULL CONSTRAINT [DF_entradamaterial_dataalteracao] DEFAULT '2015-02-05T11:10:26'

/* AlterTable saidamaterial */
/* No SQL statement executed. */

/* CreateColumn saidamaterial dataalteracao DateTime */
ALTER TABLE [dbo].[saidamaterial] ADD [dataalteracao] DATETIME NOT NULL CONSTRAINT [DF_saidamaterial_dataalteracao] DEFAULT '2015-02-05T11:10:26'

/* AlterTable reservamaterial */
/* No SQL statement executed. */

/* CreateColumn reservamaterial dataalteracao DateTime */
ALTER TABLE [dbo].[reservamaterial] ADD [dataalteracao] DATETIME NOT NULL CONSTRAINT [DF_reservamaterial_dataalteracao] DEFAULT '2015-02-05T11:10:26'

/* AlterTable material */
/* No SQL statement executed. */

/* CreateColumn material dataalteracao DateTime */
ALTER TABLE [dbo].[material] ADD [dataalteracao] DATETIME NOT NULL CONSTRAINT [DF_material_dataalteracao] DEFAULT '2015-02-05T11:10:26'

/* AlterTable pessoa */
/* No SQL statement executed. */

/* CreateColumn pessoa dataalteracao DateTime */
ALTER TABLE [dbo].[pessoa] ADD [dataalteracao] DATETIME NOT NULL CONSTRAINT [DF_pessoa_dataalteracao] DEFAULT '2015-02-05T11:10:26'

INSERT INTO [dbo].[VersionInfo] ([Version], [AppliedOn], [Description]) VALUES (201502041339, '2015-02-05T13:10:26', 'Migration201502041339')
/* Committing Transaction */
/* 201502041339: Migration201502041339 migrated */

/* Task completed. */
