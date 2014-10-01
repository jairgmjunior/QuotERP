cls
@echo off

set UName="sa"
set Pwd="123456"

:begin

sqlcmd -S .\SqlExpress -U %UName% -P %Pwd% -I -i excluiBanco.sql 

pause

:end