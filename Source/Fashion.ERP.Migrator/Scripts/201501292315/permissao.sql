-- Cheque recebido / Devolução

DECLARE @CHEQUEID AS BIGINT;
SET @CHEQUEID = (SELECT id FROM permissao WHERE area = 'Financeiro' AND descricao = 'Cheque' AND [action] is NULL);

-- Cria o item Devolução
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, Ordem, permissaopai_id) VALUES ('Index', 'Financeiro', 'DepositoChequeRecebido', 'Depósito cheques', 1, 1, 0, @CHEQUEID);