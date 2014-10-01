USE master ;
GO
-- Create the sql to kill the active database connections
declare @execSql varchar(1000), @databaseName varchar(100)
-- Set the database name for which to kill the connections
set @databaseName = 'FashionERPTest'

set @execSql = '' 
select  @execSql = @execSql + 'kill ' + convert(char(10), spid) + ' '
from    master.dbo.sysprocesses
where   db_name(dbid) = @databaseName
     and
     DBID <> 0
     and
     spid <> @@spid
exec(@execSql)

DROP DATABASE FashionERPTest;
GO