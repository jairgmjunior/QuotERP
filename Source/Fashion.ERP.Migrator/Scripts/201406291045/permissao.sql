DECLARE @AUTORIZACOESID AS BIGINT;

-- Autorizações
-- Excluir menu antigo
SET @AUTORIZACOESID = (SELECT id FROM permissao WHERE area = 'Compras' AND controller = 'ProcedimentoModuloCompras' AND [action] = 'Index');
UPDATE permissao SET controller = 'Autorizacoes' WHERE id = @AUTORIZACOESID;
DELETE FROM permissaotousuario WHERE permissao_id in (SELECT id FROM permissao WHERE permissaopai_id = @AUTORIZACOESID);
DELETE FROM permissaotoperfildeacesso WHERE permissao_id in (SELECT id FROM permissao WHERE permissaopai_id = @AUTORIZACOESID);
DELETE FROM permissao WHERE permissaopai_id = @AUTORIZACOESID;

-- Adicionar menu novo
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, ordem, permissaopai_id) VALUES ('Autorizar', 'Compras', 'Autorizacoes', 'Autorizar', 0, 1, 0, @AUTORIZACOESID);

DECLARE @GESTAOCOMPRASID AS BIGINT;
-- Corrigir menu Gestão de Compras
SET @GESTAOCOMPRASID = (SELECT id FROM permissao WHERE [action] IS NULL AND area = 'Compras' AND controller IS NULL AND descricao = 'Movimentação Estoque');
UPDATE permissao SET descricao = 'Gestão de Compras' WHERE id = @GESTAOCOMPRASID;

-- Corrigir descrição Pedido de Compra
UPDATE permissao SET descricao = '1. Pedido de Compra' WHERE [action] = 'Index' AND area = 'Compras' AND controller = 'PedidoCompra' AND descricao = 'Pedido de Compra';

-- Validação pedido compra
DECLARE @ID AS BIGINT;
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, ordem, permissaopai_id) VALUES ('Index', 'Compras', 'ValidaPedidoCompra', '2. Validação da Compra', 1, 1, 0, @GESTAOCOMPRASID);
SET @ID = SCOPE_IDENTITY()
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, ordem, permissaopai_id) VALUES ('Validar', 'Compras', 'ValidaPedidoCompra', 'Validar', 0, 1, 0, @ID);

-- Corrigir Módulo de Compras
UPDATE permissao SET descricao = 'Módulo de Compras' WHERE [action] = 'Index' AND area = 'Compras' AND controller = 'ParametroModuloCompra' AND descricao = 'Gestão de Compra';

--Compras
--	Básicos
--		Fornecedor
--		Meios Pagamento
--		Prazos
--	Cotação
--		Cotação para Compra
--	Gestão de Compras
--		1. Pedido de Compra
--		2. Validação da Compra
--		3. Recebimento de Compra
--		4. Manutenção de Compra
--	Relatório\Consulta
--		Cotação para Compra
--		Pedidos de Compra
--		Posição Pedidos
--	Parâmetros
--		Módulo de Compras
--		Autorizações