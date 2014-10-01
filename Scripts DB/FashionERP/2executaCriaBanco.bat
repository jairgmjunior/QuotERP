cls
@echo off

set SName="SQLEXPRESS"
set UName="sa"
set Pwd="123456"
set DbName="FashonERP"

:begin

sqlcmd -S .\SqlExpress -U %UName% -P %Pwd% -I -i criaBanco.sql 

pause

:end