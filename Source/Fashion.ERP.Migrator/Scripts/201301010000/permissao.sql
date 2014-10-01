insert into uniquekeys(tablename, nexthi) values('permissao', 0);
update uniquekeys set nexthi = 1 where nexthi = 0 and tablename = 'permissao';
-- Comum
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id, id) VALUES (NULL, 'Comum', NULL, 'Cadastros', 1, 0, NULL, 1);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id, id) VALUES ('Index', 'Comum', 'Usuario', 'Usuário', 1, 1, 1, 2);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id, id) VALUES ('Novo', 'Comum', 'Usuario', 'Novo', 0, 1, 2, 3);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id, id) VALUES ('Editar', 'Comum', 'Usuario', 'Editar', 0, 1, 2, 4);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id, id) VALUES ('Excluir', 'Comum', 'Usuario', 'Excluir', 0, 1, 2, 5);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id, id) VALUES ('Index', 'Comum', 'AreaInteresse', 'Área de interesse', 1, 1, 1, 6);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id, id) VALUES ('Novo', 'Comum', 'AreaInteresse', 'Novo', 0, 1, 6, 7);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id, id) VALUES ('Editar', 'Comum', 'AreaInteresse', 'Editar', 0, 1, 6, 8);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id, id) VALUES ('Excluir', 'Comum', 'AreaInteresse', 'Excluir', 0, 1, 6, 9);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id, id) VALUES ('Index', 'Comum', 'Cliente', 'Cliente', 1, 1, 1, 10);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id, id) VALUES ('Novo', 'Comum', 'Cliente', 'Novo', 0, 1, 10, 11);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id, id) VALUES ('Editar', 'Comum', 'Cliente', 'Editar', 0, 1, 10, 12);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id, id) VALUES ('Excluir', 'Comum', 'Cliente', 'Excluir', 0, 1, 10, 13);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id, id) VALUES ('PesquisarId', 'Comum', 'Cliente', 'PesquisarId', 0, 1, 10, 14);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id, id) VALUES ('Index', 'Comum', 'Fornecedor', 'Fornecedor', 1, 1, 1, 15);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id, id) VALUES ('Novo', 'Comum', 'Fornecedor', 'Novo', 0, 1, 15, 16);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id, id) VALUES ('Editar', 'Comum', 'Fornecedor', 'Editar', 0, 1, 15, 17);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id, id) VALUES ('Excluir', 'Comum', 'Fornecedor', 'Excluir', 0, 1, 15, 18);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id, id) VALUES ('Index', 'Comum', 'Funcionario', 'Funcionário', 1, 1, 1, 19);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id, id) VALUES ('Novo', 'Comum', 'Funcionario', 'Novo', 0, 1, 19, 20);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id, id) VALUES ('Editar', 'Comum', 'Funcionario', 'Editar', 0, 1, 19, 21);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id, id) VALUES ('Excluir', 'Comum', 'Funcionario', 'Excluir', 0, 1, 19, 22);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id, id) VALUES ('Index', 'Comum', 'GrauDependencia', 'Grau de dependência', 1, 1, 1, 23);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id, id) VALUES ('Novo', 'Comum', 'GrauDependencia', 'Novo', 0, 1, 23, 24);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id, id) VALUES ('Editar', 'Comum', 'GrauDependencia', 'Editar', 0, 1, 23, 25);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id, id) VALUES ('Excluir', 'Comum', 'GrauDependencia', 'Excluir', 0, 1, 23, 26);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id, id) VALUES ('Index', 'Comum', 'MeioPagamento', 'Meio de pagamento', 1, 1, 1, 27);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id, id) VALUES ('Novo', 'Comum', 'MeioPagamento', 'Novo', 0, 1, 27, 28);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id, id) VALUES ('Editar', 'Comum', 'MeioPagamento', 'Editar', 0, 1, 27, 29);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id, id) VALUES ('Excluir', 'Comum', 'MeioPagamento', 'Excluir', 0, 1, 27, 30);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id, id) VALUES ('Index', 'Comum', 'PerfilDeAcesso', 'Perfil de acesso', 1, 1, 1, 31);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id, id) VALUES ('Novo', 'Comum', 'PerfilDeAcesso', 'Novo', 0, 1, 31, 32);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id, id) VALUES ('Editar', 'Comum', 'PerfilDeAcesso', 'Editar', 0, 1, 31, 33);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id, id) VALUES ('Excluir', 'Comum', 'PerfilDeAcesso', 'Excluir', 0, 1, 31, 34);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id, id) VALUES ('Index', 'Comum', 'PrestadorServico', 'Prestador de servico', 1, 1, 1, 35);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id, id) VALUES ('Novo', 'Comum', 'PrestadorServico', 'Novo', 0, 1, 35, 36);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id, id) VALUES ('Editar', 'Comum', 'PrestadorServico', 'Editar', 0, 1, 35, 37);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id, id) VALUES ('Excluir', 'Comum', 'PrestadorServico', 'Excluir', 0, 1, 35, 38);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id, id) VALUES ('Index', 'Comum', 'Profissao', 'Profissão', 1, 1, 1, 39);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id, id) VALUES ('Novo', 'Comum', 'Profissao', 'Novo', 0, 1, 39, 40);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id, id) VALUES ('Editar', 'Comum', 'Profissao', 'Editar', 0, 1, 39, 41);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id, id) VALUES ('Excluir', 'Comum', 'Profissao', 'Excluir', 0, 1, 39, 42);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id, id) VALUES ('Index', 'Comum', 'TipoFornecedor', 'Tipo de fornecedor', 1, 1, 1, 43);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id, id) VALUES ('Novo', 'Comum', 'TipoFornecedor', 'Novo', 0, 1, 43, 44);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id, id) VALUES ('Editar', 'Comum', 'TipoFornecedor', 'Editar', 0, 1, 43, 45);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id, id) VALUES ('Excluir', 'Comum', 'TipoFornecedor', 'Excluir', 0, 1, 43, 46);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id, id) VALUES ('Index', 'Comum', 'Unidade', 'Unidade', 1, 1, 1, 47);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id, id) VALUES ('Novo', 'Comum', 'Unidade', 'Novo', 0, 1, 47, 48);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id, id) VALUES ('Editar', 'Comum', 'Unidade', 'Editar', 0, 1, 47, 49);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id, id) VALUES ('Excluir', 'Comum', 'Unidade', 'Excluir', 0, 1, 47, 50);

-- Financeiro
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id, id) VALUES (NULL, 'Financeiro', NULL, 'Financeiro', 1, 0, NULL, 51);

-- Cheque (Financeiro)
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id, id) VALUES (NULL, 'Financeiro', NULL, 'Cheque', 1, 0, 51, 52);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id, id) VALUES ('Index', 'Financeiro', 'ChequeRecebido', 'Cheque recebido', 1, 1, 52, 53);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id, id) VALUES ('Novo', 'Financeiro', 'ChequeRecebido', 'Novo', 0, 1, 53, 54);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id, id) VALUES ('Editar', 'Financeiro', 'ChequeRecebido', 'Editar', 0, 1, 53, 55);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id, id) VALUES ('Excluir', 'Financeiro', 'ChequeRecebido', 'Excluir', 0, 1, 53, 56);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id, id) VALUES ('Baixa', 'Financeiro', 'ChequeRecebido', 'Baixa', 0, 1, 53, 57);
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, permissaopai_id, id) VALUES ('ExcluirBaixa', 'Financeiro', 'ChequeRecebido', 'Excluir baixa', 0, 1, 57, 58);

-- Relatórios (Comum)
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id, id) VALUES (NULL, 'Comum', NULL, 'Relatório', 1, 0, 1, 59);
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id, id) VALUES ('FichaCliente', 'Comum', 'Relatorio', 'Ficha de cliente', 1, 1, 59, 60);
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id, id) VALUES ('ListaCliente', 'Comum', 'Relatorio', 'Lista de clientes', 1, 1, 59, 61);
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id, id) VALUES ('FichaFornecedor', 'Comum', 'Relatorio', 'Ficha de fornecedor', 1, 1, 59, 62);
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id, id) VALUES ('ListaFornecedor', 'Comum', 'Relatorio', 'Lista de fornecedores', 1, 1, 59, 63);
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id, id) VALUES ('FichaFuncionario', 'Comum', 'Relatorio', 'Ficha de funcionário', 1, 1, 59, 64);
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id, id) VALUES ('ListaFuncionario', 'Comum', 'Relatorio', 'Lista de funcionários', 1, 1, 59, 65);
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id, id) VALUES ('FichaPrestadorServico', 'Comum', 'Relatorio', 'Ficha de prestador de serviço', 1, 1, 59, 66);
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id, id) VALUES ('ListaPrestadorServico', 'Comum', 'Relatorio', 'Lista de prestadores de serviço', 1, 1, 59, 67);
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id, id) VALUES ('ListaChequeRecebido', 'Comum', 'Relatorio', 'Lista de cheques recebido', 1, 1, 59, 68);

-- Bancário (Financeiro)
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id, id) VALUES (NULL, 'Financeiro', NULL, 'Bancário', 1, 0, 51, 69);
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id, id) VALUES ('Index', 'Financeiro', 'ContaBancaria', 'Conta bancária', 1, 1, 69, 70);
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id, id) VALUES ('Novo', 'Financeiro', 'ContaBancaria', 'Novo', 0, 1, 70, 71);
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id, id) VALUES ('Editar', 'Financeiro', 'ContaBancaria', 'Editar', 0, 1, 70, 72);
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id, id) VALUES ('Excluir', 'Financeiro', 'ContaBancaria', 'Excluir', 0, 1, 70, 73);
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id, id) VALUES ('Index', 'Financeiro', 'ExtratoBancario', 'Extrato bancário', 1, 1, 69, 74);
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id, id) VALUES ('Novo', 'Financeiro', 'ExtratoBancario', 'Novo', 0, 1, 74, 75);
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id, id) VALUES ('Editar', 'Financeiro', 'ExtratoBancario', 'Editar', 0, 1, 74, 76);
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id, id) VALUES ('Excluir', 'Financeiro', 'ExtratoBancario', 'Excluir', 0, 1, 74, 77);

-- Relatórios (Financeiro)
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id, id) VALUES (NULL, 'Financeiro', NULL, 'Relatório', 1, 0, 51, 78);
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, permissaopai_id, id) VALUES ('ListaChequeRecebido', 'Financeiro', 'Relatorio', 'Lista de cheques recebido', 1, 1, 78, 79);