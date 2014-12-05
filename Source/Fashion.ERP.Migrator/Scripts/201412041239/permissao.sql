declare @comumId as bigint, @id_pai as bigint, @basicoId as bigint

-- Artigo
update permissao set area='Comum' where controller='Artigo'
set @comumId = (Select Id from permissao where area='Comum' and descricao='Cadastros' and controller is null)
update permissao set permissaopai_id=@comumId where controller='Artigo' and action='Index' and exibenomenu=1

set @basicoId = (Select Id from permissao where area='EngenhariaProduto' and descricao='Básicos' and controller is null)
insert into permissao (descricao, action,area, controller,exibenomenu,requerpermissao,permissaopai_id,ordem)
select descricao,action,area,controller,exibenomenu,requerpermissao, @basicoId, ordem from permissao where controller='Artigo' and action='Index'
SET @id_pai = SCOPE_IDENTITY()

insert into permissao (descricao, action,area, controller,exibenomenu,requerpermissao,permissaopai_id,ordem)
select descricao,action,area,controller,exibenomenu,requerpermissao, @id_pai, ordem from permissao where controller='Artigo' and action<>'Index'


-- Barra
update permissao set area='Comum' where controller='Barra'
set @comumId = (Select Id from permissao where area='Comum' and descricao='Cadastros' and controller is null)
update permissao set permissaopai_id=@comumId where controller='Barra' and action='Index' and exibenomenu=1

set @basicoId = (Select Id from permissao where area='EngenhariaProduto' and descricao='Básicos' and controller is null)
insert into permissao (descricao, action,area, controller,exibenomenu,requerpermissao,permissaopai_id,ordem)
select descricao,action,area,controller,exibenomenu,requerpermissao, @basicoId, ordem from permissao where controller='Barra' and action='Index'
SET @id_pai = SCOPE_IDENTITY()

insert into permissao (descricao, action,area, controller,exibenomenu,requerpermissao,permissaopai_id,ordem)
select descricao,action,area,controller,exibenomenu,requerpermissao, @id_pai, ordem from permissao where controller='Barra' and action<>'Index'


-- Classificação
update permissao set area='Comum' where controller='Classificacao'
set @comumId = (Select Id from permissao where area='Comum' and descricao='Cadastros' and controller is null)
update permissao set permissaopai_id=@comumId where controller='Classificacao' and action='Index' and exibenomenu=1

set @basicoId = (Select Id from permissao where area='EngenhariaProduto' and descricao='Básicos' and controller is null)
insert into permissao (descricao, action,area, controller,exibenomenu,requerpermissao,permissaopai_id,ordem)
select descricao,action,area,controller,exibenomenu,requerpermissao, @basicoId, ordem from permissao where controller='Classificacao' and action='Index'
SET @id_pai = SCOPE_IDENTITY()

insert into permissao (descricao, action,area, controller,exibenomenu,requerpermissao,permissaopai_id,ordem)
select descricao,action,area,controller,exibenomenu,requerpermissao, @id_pai, ordem from permissao where controller='Classificacao' and action<>'Index'

-- Comprimento
update permissao set area='Comum' where controller='Comprimento'
set @comumId = (Select Id from permissao where area='Comum' and descricao='Cadastros' and controller is null)
update permissao set permissaopai_id=@comumId where controller='Comprimento' and action='Index' and exibenomenu=1

set @basicoId = (Select Id from permissao where area='EngenhariaProduto' and descricao='Básicos' and controller is null)
insert into permissao (descricao, action,area, controller,exibenomenu,requerpermissao,permissaopai_id,ordem)
select descricao,action,area,controller,exibenomenu,requerpermissao, @basicoId, ordem from permissao where controller='Comprimento' and action='Index'
SET @id_pai = SCOPE_IDENTITY()

insert into permissao (descricao, action,area, controller,exibenomenu,requerpermissao,permissaopai_id,ordem)
select descricao,action,area,controller,exibenomenu,requerpermissao, @id_pai, ordem from permissao where controller='Comprimento' and action<>'Index'


-- ProdutoBase
update permissao set area='Comum' where controller='ProdutoBase'
set @comumId = (Select Id from permissao where area='Comum' and descricao='Cadastros' and controller is null)
update permissao set permissaopai_id=@comumId where controller='ProdutoBase' and action='Index' and exibenomenu=1

set @basicoId = (Select Id from permissao where area='EngenhariaProduto' and descricao='Básicos' and controller is null)
insert into permissao (descricao, action,area, controller,exibenomenu,requerpermissao,permissaopai_id,ordem)
select descricao,action,area,controller,exibenomenu,requerpermissao, @basicoId, ordem from permissao where controller='ProdutoBase' and action='Index'
SET @id_pai = SCOPE_IDENTITY()

insert into permissao (descricao, action,area, controller,exibenomenu,requerpermissao,permissaopai_id,ordem)
select descricao,action,area,controller,exibenomenu,requerpermissao, @id_pai, ordem from permissao where controller='ProdutoBase' and action<>'Index'

-- Segmento
update permissao set area='Comum' where controller='Segmento'
set @comumId = (Select Id from permissao where area='Comum' and descricao='Cadastros' and controller is null)
update permissao set permissaopai_id=@comumId where controller='Segmento' and action='Index' and exibenomenu=1

set @basicoId = (Select Id from permissao where area='EngenhariaProduto' and descricao='Básicos' and controller is null)
insert into permissao (descricao, action,area, controller,exibenomenu,requerpermissao,permissaopai_id,ordem)
select descricao,action,area,controller,exibenomenu,requerpermissao, @basicoId, ordem from permissao where controller='Segmento' and action='Index'
SET @id_pai = SCOPE_IDENTITY()

insert into permissao (descricao, action,area, controller,exibenomenu,requerpermissao,permissaopai_id,ordem)
select descricao,action,area,controller,exibenomenu,requerpermissao, @id_pai, ordem from permissao where controller='Segmento' and action<>'Index'