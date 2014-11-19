-- O código abaixo gera e executa uma sql para cada tabela do banco que possui Id e atualiza a tabela uniqueKeys com o maior valor de Id.
delete from uniquekeys

declare @uniqueQueries table
(
	Strings        varchar(max)    not null
)

insert into @uniqueQueries 
select 'INSERT uniquekeys SELECT * FROM (SELECT ''' + name + ''' as nome, MAX(id) + 1 as id FROM ' + name + ') as result WHERE result.id is not null;'
from sys.sysobjects as tabela
where type = 'U' 
and id in (SELECT object_id FROM sys.columns WHERE [name] = 'id')

-- Determine loop boundaries.
declare @StringSql nvarchar(500)
declare @counter int = 0
declare @total int = isnull((select count(1) from @uniqueQueries), 0)

while (@counter <> (@total))
begin          
       set @StringSql = (select Strings from @uniqueQueries order by Strings offset @counter rows fetch next 1 rows only)	         
       
	   exec sp_executesql  @StringSql        

       set @counter = @counter + 1
end
