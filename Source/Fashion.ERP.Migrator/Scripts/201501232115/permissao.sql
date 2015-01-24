-- Altera o menu
-- Coloca o item Excluir Baixa no mesmo nível de Baixa

DECLARE @CHEQUERECEBIDOID AS BIGINT;
SET @CHEQUERECEBIDOID = (SELECT permissaopai_id FROM permissao WHERE [action] = 'Baixa' and controller = 'ChequeRecebido' and area = 'Financeiro');

UPDATE permissao SET permissaopai_id = @CHEQUERECEBIDOID WHERE [action] = 'ExcluirBaixa' and controller = 'ChequeRecebido' and area = 'Financeiro'