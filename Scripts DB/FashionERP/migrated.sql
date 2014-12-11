/* Using Database sqlserver2012 and Connection String server=.\SQLEXPRESS;Database=FashionERP;User Id=sa;Password=123456; */
/* 201411211534: Migration201411211534 migrating ============================= */

/* Beginning Transaction */
/* AlterTable operacaoproducao */
/* No SQL statement executed. */

/* CreateColumn operacaoproducao pesoProdutividade Double */
ALTER TABLE [dbo].[operacaoproducao] ADD [pesoProdutividade] DOUBLE PRECISION

INSERT INTO [dbo].[VersionInfo] ([Version], [AppliedOn], [Description]) VALUES (201411211534, '2014-11-21T17:55:28', 'Migration201411211534')
/* Committing Transaction */
/* 201411211534: Migration201411211534 migrated */

/* Task completed. */
