cls
@echo off

set UName="sa"
set Pwd="123456"

:begin

sqlcmd -S .\SqlExpress -U %UName% -P %Pwd% -I -i excluiBanco.sql 

:end


cls
@echo off

set SName="SQLEXPRESS"
set UName="sa"
set Pwd="123456"
set DbName="FashonERP"

:begin

sqlcmd -S .\SqlExpress -U %UName% -P %Pwd% -I -i criaBanco.sql 


:end


"%FASHION.ERP_SOURCE%\packages\FluentMigrator.Tools.1.1.2.1\tools\x86\40\migrate.EXE" -c "server=.\SQLEXPRESS;Database=FashionERPTest;User Id=sa;Password=123456;" -db sqlserver2012 -a "%FASHION.ERP_SOURCE%\Fashion.ERP.Migrator\bin\Debug\Fashion.ERP.Migrator.dll" -t migrate -o -of migrated.sql

@ECHO OFF
PAUSE