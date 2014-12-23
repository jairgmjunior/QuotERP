-- Cheque recebido / Devolução

DECLARE @CHEQUERECEBIDOID AS BIGINT, @ID AS BIGINT;
SET @CHEQUERECEBIDOID = (SELECT id FROM permissao WHERE area = 'Financeiro' AND controller = 'ChequeRecebido' AND [action] = 'Index');

-- Cria o item Devolução
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, Ordem, permissaopai_id) VALUES ('Devolucao', 'Financeiro', 'ChequeRecebido', 'Devolver', 0, 1, 0, @CHEQUERECEBIDOID);