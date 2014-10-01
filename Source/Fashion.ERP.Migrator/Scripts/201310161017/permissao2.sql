-- Atualizado MvcSiteMapProvider para versão 4.4.4

-- nova permissão AlterarSenha
DECLARE @USUARIOID AS BIGINT;
SET @USUARIOID = (SELECT id FROM permissao WHERE area = 'Comum' AND controller = 'Usuario' AND [action] = 'Index');
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id) VALUES ('AlterarSenha', 'Comum', 'Usuario', 'Alterar senha', 0, 1, @USUARIOID);

-- Adicionar Ordem à tabela permissao
ALTER TABLE permissao ADD ordem INT NULL;
GO
UPDATE permissao SET ordem = 0;
GO
ALTER TABLE permissao ALTER COLUMN ordem INT NOT NULL;
GO

-- Materiais de Composição
UPDATE permissao SET descricao = 'Materiais de Composição' WHERE area = 'EngenhariaProduto' AND controller = 'Modelo' AND action = 'Composicao';