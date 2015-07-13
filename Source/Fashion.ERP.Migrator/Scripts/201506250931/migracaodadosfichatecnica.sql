DECLARE @id bigint
DECLARE @getid CURSOR
DECLARE @maxfichatecnicamodelagemid bigint
DECLARE @fichatecnicamodelagemid bigint
DECLARE @maxfichatecnicamatrizid bigint
DECLARE @fichatecnicamatrizid bigint
DECLARE @maxfichatecnicaid bigint
DECLARE @fichatecnicaid bigint
DECLARE @maxfichatecnicamaterialid bigint
DECLARE @fichatecnicamaterialid bigint
DECLARE @maxmodeloavaliacaoid bigint
DECLARE @maxmodeloaprovacaoid bigint
DECLARE @modeloaprovacaoid bigint
DECLARE @maxfichatecnicavariacaomatrizid bigint
DECLARE @maxfichatecnicavariacaomatrizcorid bigint
DECLARE @maxprogramacaoproducaoid bigint
DECLARE @maxprogramacaoproducaomatrizcorteid bigint


	SET @getid = CURSOR FOR SELECT id FROM  modelo WHERE modeloaprovado_id IS NOT NULL

	OPEN @getid
	FETCH NEXT
	FROM @getid INTO @id
	WHILE @@FETCH_STATUS = 0
	BEGIN
		-- fichatecnicamodelagem
		if @maxfichatecnicamodelagemid is null
		BEGIN
			SET @maxfichatecnicamodelagemid = (select max(id) from fichatecnicamodelagem)
			if @maxfichatecnicamodelagemid is null
			BEGIN
				SET @maxfichatecnicamodelagemid = 0	
			END
		END
		SET @maxfichatecnicamodelagemid = @maxfichatecnicamodelagemid + 1

		INSERT INTO fichatecnicamodelagem (id, idtenant, idempresa, observacao, datamodelagem, modelista_id)
			SELECT @maxfichatecnicamodelagemid, 0, 0, null, datamodelagem, modelista_id FROM modelo WHERE id = @id and modelista_id is not null and datamodelagem is not null

		if NOT EXISTS(SELECT * from modelo where id = @id and modelista_id is not null and datamodelagem is not null)
		BEGIN 
			SET @fichatecnicamodelagemid = null
		END 
		ELSE BEGIN 
			SET @fichatecnicamodelagemid = @maxfichatecnicamodelagemid
		END

		-- fichatecnicamatriz
		if @maxfichatecnicamatrizid is null
		BEGIN
			SET @maxfichatecnicamatrizid = (select max(id) from fichatecnicamatriz)
			if @maxfichatecnicamatrizid is null
			BEGIN
				SET @maxfichatecnicamatrizid = 0	
			END
		END
		SET @maxfichatecnicamatrizid = @maxfichatecnicamatrizid + 1
	
		INSERT INTO fichatecnicamatriz(id, idtenant, idempresa, grade_id)
			SELECT @maxfichatecnicamatrizid, 0, 0, grade_id FROM modelo WHERE id = @id and grade_id is not null

		if NOT EXISTS(SELECT * from modelo where id = @id and grade_id is not null)
		BEGIN 
			SET @fichatecnicamatrizid = null
		END 
		ELSE BEGIN 
			SET @fichatecnicamatrizid = @maxfichatecnicamodelagemid
		END

		-- fichatecnica
		if @maxfichatecnicaid is null
		BEGIN
			SET @maxfichatecnicaid = (select max(id) from fichatecnica)
			if @maxfichatecnicaid is null
			BEGIN
				SET @maxfichatecnicaid = 0	
			END
		END
		SET @maxfichatecnicaid = @maxfichatecnicaid + 1
	
		INSERT INTO fichatecnica(
			id, 		
			idtenant, 		
			idempresa, 		
			tipo,		
			tag,		
			ano,
			descricao,		
			detalhamento, 		
			observacao,
			silk,		
			bordado,		
			pedraria,		
			datacadastro,		
			dataalteracao,		
			artigo_id,		
			colecao_id,		
			marca_id,		
			segmento_id,
			natureza_id,
			classificacao_id,
			classificacaodificuldade_id,
			fichatecnicamatriz_id,
			lavada,		
			pesponto,		
			medidacos,		
			medidapassante,		
			medidacomprimento,		
			medidabarra,		
			produtobase_id,		
			barra_id,		
			comprimento_id,		
			modeloaprovado_id,		
			catalogo,		
			complemento,
			estilista_id,
			referencia,
			fichatecnicamodelagem_id)
			SELECT @maxfichatecnicaid, 
				0, 
				0, 
				'FichaTecnicaJeans',
				modeloaprovadoantigo.tag,
				modeloaprovadoantigo.ano,
				modelo.descricao,
				modelo.detalhamento,
				CONCAT ( modelo.observacao, ' REF.MODELO:', modelo.referencia ),
				null,
				null,
				null, 
				modelo.datacriacao,
				modelo.datacriacao,
				modelo.artigo_id,
				modeloaprovadoantigo.colecao_id,
				modelo.marca_id,
				modelo.segmento_id,
				modelo.natureza_id,
				modelo.classificacao_id,
				modeloaprovadoantigo.classificacaodificuldade_id,
				@fichatecnicamatrizid,
				null,
				null,
				null,
				null,
				null, 
				null,
				modelo.produtobase_id,
				modelo.barra_id,
				modelo.comprimento_id,
				null,
				IIF(CHARINDEX('CATALOGO', modeloaprovadoantigo.observacao) > 0, 1, 0), 
				modelo.complemento,
				modelo.estilista_id,
				CONCAT ( modeloaprovadoantigo.tag, '/', modeloaprovadoantigo.ano, '-1' ),
				@fichatecnicamodelagemid
				FROM modelo, modeloaprovadoantigo 
				WHERE modeloaprovado_id = modeloaprovadoantigo.id and modelo.id = @id

		if NOT EXISTS(SELECT * from modelo where id = @id and grade_id is not null)
		BEGIN 
			SET @fichatecnicaid = null
		END 
		ELSE BEGIN 
			SET @fichatecnicaid = @maxfichatecnicaid
		END

		--fichatecnicamaterialconsumo
		DECLARE @idmaterial bigint
		DECLARE @getmaterialid CURSOR

		SET @getmaterialid = CURSOR FOR
		SELECT id			   
		FROM modelomaterialconsumo where modelo_id = @id

		OPEN @getmaterialid
		FETCH NEXT
		FROM @getmaterialid INTO @idmaterial
		WHILE @@FETCH_STATUS = 0
		BEGIN
					
			if @maxfichatecnicamaterialid is null
			BEGIN
				set @maxfichatecnicamaterialid = (SELECT ISNULL(MAX(id) + 1, 1) FROM fichatecnicamaterialconsumo)
			END

			SET @maxfichatecnicamaterialid = @maxfichatecnicamaterialid + 1

			INSERT INTO fichatecnicamaterialconsumo (id, idtenant, idempresa, custo, quantidade, departamentoproducao_id, material_id, fichatecnica_id)
				SELECT @maxfichatecnicamaterialid, 
				0, 
				0, 
				(select coalesce((select custo from customaterial where ativo = 1 and customaterial.material_id = modelomaterialconsumo.material_id), 0)), 				
				(quantidade * (Select fatormultiplicativo from material, unidademedida where material.id=material_id and unidademedida.id = unidademedida_id)),
				departamentoproducao_id, 
				material_id, 
				@fichatecnicaid 
				FROM modelomaterialconsumo where id = @idmaterial

			FETCH NEXT
			FROM @getmaterialid INTO @idmaterial
		END
		CLOSE @getmaterialid
		DEALLOCATE @getmaterialid
		
		--fichatecnicavariacaomatriz
		DECLARE @idvariacaomatriz bigint
		DECLARE @getvariacaomatrizid CURSOR

		SET @getvariacaomatrizid = CURSOR FOR
		SELECT id			   
		FROM variacaomodelo where modelo_id = @id

		OPEN @getvariacaomatrizid
		FETCH NEXT
		FROM @getvariacaomatrizid INTO @idvariacaomatriz
		WHILE @@FETCH_STATUS = 0
		BEGIN
					
			if @maxfichatecnicavariacaomatrizid is null
			BEGIN
				set @maxfichatecnicavariacaomatrizid = (SELECT ISNULL(MAX(id) + 1, 0) FROM fichatecnicavariacaomatriz)
			END

			SET @maxfichatecnicavariacaomatrizid = @maxfichatecnicavariacaomatrizid + 1
			
			INSERT INTO fichatecnicavariacaomatriz(
				id, 
				idtenant, 
				idempresa, 
				variacao_id, fichatecnicamatriz_id)			
				SELECT @maxfichatecnicavariacaomatrizid, 0, 0, variacao_id, @fichatecnicamatrizid
				FROM variacaomodelo where modelo_id = @id and id = @idvariacaomatriz

			--fichatecnicavariacaomatrizcor
			DECLARE @idcor bigint
			DECLARE @getcorid CURSOR

			SET @getcorid = CURSOR FOR
			SELECT cor_id
			FROM variacaomodelocor where variacaomodelo_id = @idvariacaomatriz

			OPEN @getcorid
			FETCH NEXT
			FROM @getcorid INTO @idcor
			WHILE @@FETCH_STATUS = 0
			BEGIN					
				
				INSERT INTO fichatecnicavariacaomatrizcor(fichatecnicavariacaomatriz_id, cor_id)			
					values (@maxfichatecnicavariacaomatrizid, @idcor)

				FETCH NEXT
				FROM @getcorid INTO @idcor
		
			END
			CLOSE @getcorid
			DEALLOCATE @getcorid
						
			FETCH NEXT
			FROM @getvariacaomatrizid INTO @idvariacaomatriz
		
		END
		CLOSE @getvariacaomatrizid
		DEALLOCATE @getvariacaomatrizid

		--modeloavaliacao
		if @maxmodeloavaliacaoid is null
		BEGIN
			set @maxmodeloavaliacaoid = (SELECT ISNULL(MAX(id) + 1, 1) FROM modeloavaliacao)
		END

		SET @maxmodeloavaliacaoid = @maxmodeloavaliacaoid + 1

		INSERT INTO modeloavaliacao(
			id, 		
			idtenant, 		
			idempresa, 					
			tag,		
			ano,
			sequenciatag,		
			data, 		
			aprovado,
			catalogo,		
			observacao,		
			quantidadetotaaprovacao,		
			complemento,		
			colecao_id,		
			classificacaodificuldade_id,		
			modeloreprovacao_id)
			SELECT @maxmodeloavaliacaoid, 
				0, 
				0, 				
				modeloaprovadoantigo.tag,
				modeloaprovadoantigo.ano,
				1,
				modeloaprovadoantigo.data,
				1,
				IIF(CHARINDEX('CATALOGO', modeloaprovadoantigo.observacao) > 0, 1, 0), 
				modeloaprovadoantigo.observacao,
				modeloaprovadoantigo.quantidade, 
				null,
				modeloaprovadoantigo.colecao_id,
				modeloaprovadoantigo.classificacaodificuldade_id,
				null
				FROM modelo, modeloaprovadoantigo 
				WHERE modeloaprovado_id = modeloaprovadoantigo.id and modelo.id = @id

		UPDATE modelo SET modeloavaliacao_id = @maxmodeloavaliacaoid, situacao = 'Aprovado' WHERE id = @id

		--modeloaprovacao
		if @maxmodeloaprovacaoid is null
		BEGIN
			set @maxmodeloaprovacaoid = (SELECT ISNULL(MAX(id) + 1, 1) FROM modeloaprovacao)
		END

		SET @maxmodeloaprovacaoid = @maxmodeloaprovacaoid + 1
		
		INSERT INTO modeloaprovacao(
			id, 		
			idtenant, 		
			idempresa, 					
			observacao,		
			complemento,
			referencia,		
			descricao, 		
			medidabarra,
			medidacomprimento,		
			quantidade,		
			barra_id,		
			comprimento_id,		
			produtobase_id,		
			fichatecnica_id,		
			modeloavaliacao_id)
			SELECT @maxmodeloaprovacaoid, 
				0, 
				0, 				
				null,
				null,
				CONCAT ( modeloaprovadoantigo.tag, '/', modeloaprovadoantigo.ano, '-1' ),
				modelo.descricao,
				0,
				0, 				
				modeloaprovadoantigo.quantidade, 
				null,
				null,
				null,
				@fichatecnicaid,
				@maxmodeloavaliacaoid
				FROM modelo, modeloaprovadoantigo 
				WHERE modeloaprovado_id = modeloaprovadoantigo.id and modelo.id = @id
				
		-- programacaoproducaomatrizcorte
		if @maxprogramacaoproducaomatrizcorteid is null
		BEGIN
			set @maxprogramacaoproducaomatrizcorteid = (SELECT ISNULL(MAX(id) + 1, 1) FROM programacaoproducaomatrizcorte)
		END
		SET @maxprogramacaoproducaomatrizcorteid = @maxprogramacaoproducaomatrizcorteid + 1
			
		INSERT INTO programacaoproducaomatrizcorte(
			id, 
			idtenant, 
			idempresa, 
			tipoenfestotecido)
			values ( @maxprogramacaoproducaomatrizcorteid, 0, 0, 'Pares')
		
		-- programacaoproducao
		if @maxprogramacaoproducaoid is null
		BEGIN
			set @maxprogramacaoproducaoid = (SELECT ISNULL(MAX(id) + 1, 1) FROM programacaoproducao)
		END
		SET @maxprogramacaoproducaoid = @maxprogramacaoproducaoid + 1
			
		INSERT INTO programacaoproducao(
			id, 
			idtenant, 
			idempresa, 
			numero,
			data,
			dataprogramada,
			dataalteracao,
			observacao,
			quantidade,
			funcionario_id,
			colecao_id,
			fichatecnica_id,
			programacaoproducaomatrizcorte_id)
			SELECT @maxprogramacaoproducaoid, 0, 0, 
				@maxprogramacaoproducaoid,
				modeloaprovadoantigo.data,
				modeloaprovadoantigo.dataprogramacaoproducao,
				modeloaprovadoantigo.data,
				null,
				modeloaprovadoantigo.quantidade,
				modeloaprovadoantigo.funcionario_id,
				modeloaprovadoantigo.colecao_id,
				@fichatecnicaid,
				@maxprogramacaoproducaomatrizcorteid 
			FROM modelo, modeloaprovadoantigo 
				WHERE modeloaprovado_id = modeloaprovadoantigo.id and modelo.id = @id


		--programacaoproducaomaterial
		--DECLARE @idmaterial bigint
		--DECLARE @getmaterialid CURSOR

		
		DECLARE @quantidadeprogramacaoproducao bigint

		SET @getmaterialid = CURSOR FOR
		SELECT id			   
		FROM modelomaterialconsumo where modelo_id = @id

		OPEN @getmaterialid
		FETCH NEXT
		FROM @getmaterialid INTO @idmaterial
		WHILE @@FETCH_STATUS = 0
		BEGIN
					
			if @maxfichatecnicamaterialid is null
			BEGIN
				set @maxfichatecnicamaterialid = (SELECT ISNULL(MAX(id) + 1, 1) FROM fichatecnicamaterialconsumo)
			END

			SET @maxfichatecnicamaterialid = @maxfichatecnicamaterialid + 1
						
			SET @quantidadeprogramacaoproducao = (SELECT quantidade from programacaoproducao where id = @maxprogramacaoproducaoid)
			
			INSERT INTO programacaoproducaomaterial(id, idtenant, idempresa, quantidade, reservamaterial_id, material_id, programacaoproducao_id, departamentoproducao_id)
				SELECT @maxfichatecnicamaterialid, 0, 0, 
				((quantidade * (Select fatormultiplicativo from material, unidademedida where material.id=material_id and unidademedida.id = unidademedida_id)) * @quantidadeprogramacaoproducao), 
				null, material_id, @maxprogramacaoproducaoid, departamentoproducao_id 
				FROM modelomaterialconsumo where id = @idmaterial

			FETCH NEXT
			FROM @getmaterialid INTO @idmaterial
		END
		CLOSE @getmaterialid
		DEALLOCATE @getmaterialid
														
		FETCH NEXT
		FROM @getid INTO @id

	END
	CLOSE @getid
	DEALLOCATE @getid
	
	delete from uniquekeys where tablename = 'programacaoproducao';
	delete from uniquekeys where tablename = 'programacaoproducaomatrizcorte';
	delete from uniquekeys where tablename = 'modeloaprovacao';
	delete from uniquekeys where tablename = 'modeloavaliacao';
	delete from uniquekeys where tablename = 'fichatecnicamaterialconsumo';
	delete from uniquekeys where tablename = 'fichatecnica';
	delete from uniquekeys where tablename = 'fichatecnicamodelagem';
	delete from uniquekeys where tablename = 'fichatecnicavariacaomatriz';
	delete from uniquekeys where tablename = 'fichatecnicamatriz';
	delete from uniquekeys where tablename = 'programacaoproducaomaterial';
	

	insert into uniquekeys(tablename, nexthi) values('programacaoproducao', (SELECT ISNULL(MAX(id) + 1, 1) FROM programacaoproducao))
	insert into uniquekeys(tablename, nexthi) values('programacaoproducaomatrizcorte', (SELECT ISNULL(MAX(id) + 1, 1) FROM programacaoproducaomatrizcorte))	
	insert into uniquekeys(tablename, nexthi) values('modeloaprovacao', (SELECT ISNULL(MAX(id) + 1, 1) FROM modeloaprovacao))
	insert into uniquekeys(tablename, nexthi) values('modeloavaliacao', (SELECT ISNULL(MAX(id) + 1, 1) FROM modeloavaliacao))
	insert into uniquekeys(tablename, nexthi) values('fichatecnicamaterialconsumo', (SELECT ISNULL(MAX(id) + 1, 1) FROM fichatecnicamaterialconsumo))
	insert into uniquekeys(tablename, nexthi) values('fichatecnica', (SELECT ISNULL(MAX(id) + 1, 1) FROM fichatecnica))
	insert into uniquekeys(tablename, nexthi) values('fichatecnicamodelagem', (SELECT ISNULL(MAX(id) + 1, 1) FROM fichatecnicamodelagem))
	insert into uniquekeys(tablename, nexthi) values('fichatecnicavariacaomatriz', (SELECT ISNULL(MAX(id) + 1, 1) FROM fichatecnicavariacaomatriz))
	insert into uniquekeys(tablename, nexthi) values('fichatecnicamatriz', (SELECT ISNULL(MAX(id) + 1, 1) FROM fichatecnicamatriz))
	insert into uniquekeys(tablename, nexthi) values('programacaoproducaomaterial', (SELECT ISNULL(MAX(id) + 1, 1) FROM programacaoproducaomaterial))


