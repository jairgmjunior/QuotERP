INSERT INTO variacao (id, nome, ativo)
Select row_number() over(order by nome), nome, cast('true' as bit) as ativo
FROM variacaoantiga group by nome;

INSERT INTO variacaomodelo (id, variacao_id, modelo_id)
Select row_number() over(order by variacao.nome), variacao.id, modelo_id 
from variacaoantiga, variacao where variacaoantiga.nome = variacao.nome;

INSERT INTO variacaomodelocor (variacaomodelo_id, cor_id)
select variacaomodelo.id as variacaomodelo_id, variacaocor.cor_id as cor_id
from variacaoantiga, variacao, variacaocor, variacaomodelo 
where variacaoantiga.nome = variacao.nome 
and variacaoantiga.id = variacaocor.variacao_id
and variacaomodelo.variacao_id = variacao.id 
group by variacaomodelo.id, variacaocor.cor_id;

UPDATE
    materialcomposicaomodelo
SET
    materialcomposicaomodelo.variacaomodelo_id = tabelax.variacaomodelo_id    
FROM
    materialcomposicaomodelo
INNER JOIN
    (select variacaomodelo.id as variacaomodelo_id, materialcomposicaomodelo.id as materialcomposicaomodelo_id 
		from materialcomposicaomodelo, sequenciaproducao, variacaoantiga, variacao, variacaomodelo
		where materialcomposicaomodelo.sequenciaproducao_id = sequenciaproducao.id
		and materialcomposicaomodelo.variacao_id = variacaoantiga.id
		and variacao.nome = variacaoantiga.nome
		and variacaomodelo.variacao_id = variacao.id
		and variacaomodelo.modelo_id = sequenciaproducao.modelo_id) as tabelax
ON
    materialcomposicaomodelo.id = tabelax.materialcomposicaomodelo_id


update uniquekeys set nexthi = (select max(id) from variacaomodelo) where tablename = 'variacaomodelo'

--------------------------------

DECLARE @BASICOID AS BIGINT, @id_pai AS BIGINT;
SET @BASICOID = (SELECT id FROM permissao WHERE area = 'Comum' AND controller is null AND descricao='Cadastros');

INSERT INTO [dbo].[permissao]
           ([descricao]
           ,[action]
           ,[area]
           ,[controller]
           ,[exibenomenu]
           ,[requerpermissao]
           ,[permissaopai_id]
           ,[ordem])
     VALUES
           ('Variação'
           ,'Index'
           ,'Comum'
           ,'Variacao'
           ,1
           ,1
           ,@BASICOID
           ,0
		   )
		   SET @id_pai = SCOPE_IDENTITY()

INSERT INTO [dbo].[permissao]
           ([descricao]
           ,[action]
           ,[area]
           ,[controller]
           ,[exibenomenu]
           ,[requerpermissao]
           ,[permissaopai_id]
           ,[ordem])
     VALUES
           ('Novo'
           ,'Novo'
           ,'Comum'
           ,'Variacao'
           ,0
           ,1
           ,@id_pai
           ,0
		   )

INSERT INTO [dbo].[permissao]
           ([descricao]
           ,[action]
           ,[area]
           ,[controller]
           ,[exibenomenu]
           ,[requerpermissao]
           ,[permissaopai_id]
           ,[ordem])
     VALUES
           ('Editar'
           ,'Editar'
           ,'Comum'
           ,'Variacao'
           ,0
           ,1
           ,@id_pai
           ,0
		   )

INSERT INTO [dbo].[permissao]
           ([descricao]
           ,[action]
           ,[area]
           ,[controller]
           ,[exibenomenu]
           ,[requerpermissao]
           ,[permissaopai_id]
           ,[ordem])
     VALUES
           ('Excluir'
           ,'Excluir'
           ,'Comum'
           ,'Variacao'
           ,0
           ,1
           ,@id_pai
           ,0
		   )

INSERT INTO [dbo].[permissao]
           ([descricao]
           ,[action]
           ,[area]
           ,[controller]
           ,[exibenomenu]
           ,[requerpermissao]
           ,[permissaopai_id]
           ,[ordem])
     VALUES
           ('Editar situação'
           ,'EditarSituacao'
           ,'Comum'
           ,'Variacao'
           ,0
           ,1
           ,@id_pai
           ,0
		   )