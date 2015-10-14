UPDATE modeloaprovacao SET grade_id = modelo.grade_id
    from modelo, modeloavaliacao, modeloaprovacao
	where modelo.modeloavaliacao_id = modeloavaliacao.id and
	modeloaprovacao.modeloavaliacao_id = modeloavaliacao.id;