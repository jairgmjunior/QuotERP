using System;
using System.Runtime.Remoting.Messaging;
using Fashion.ERP.Domain;
using Fashion.ERP.Domain.Almoxarifado;
using Fashion.ERP.Domain.Compras;
using Fashion.ERP.Domain.Comum;
using Fashion.ERP.Domain.EngenhariaProduto;
using Fashion.ERP.Domain.Financeiro;
using Fashion.ERP.Domain.Producao;

namespace Fashion.ERP.Testes.Persistencia
{
    /**
     * Responsável por fornecer objetos não persistidos para testes de unidade 
     * a serem utilizados principalmente no método GetPersistentObject.
     * Alterações realizadas nesses objetos podem causar bugs nos testes já implementados.
     */
    public class FabricaObjetos
    {
        #region Almoxarifado

        public ReservaEstoqueMaterial ObtenhaReservaEstoqueMaterial()
        {
            return new ReservaEstoqueMaterial()
            {
                Quantidade = 1
            };
        }

        public RequisicaoMaterialItemCancelado ObtenhaRequisicaoMaterialItemCancelado()
        {
            return new RequisicaoMaterialItemCancelado
            {
                Data = new DateTime(2014, 1, 1),
                Observacao = "OBSERVAÇÃO",
                QuantidadeCancelada = 1
            };
        }

        public ReservaMaterialItemCancelado ObtenhaReservaMaterialItemCancelado()
        {
            return new ReservaMaterialItemCancelado
            {
                Data = new DateTime(2014, 1, 1),
                Observacao = "OBSERVAÇÃO",
                QuantidadeCancelada = 1
            };
        }

        public ReservaMaterialItem ObtenhaReservaMaterialItem()
        {
            return new ReservaMaterialItem
            {
                QuantidadeAtendida = 1,
                QuantidadeReserva = 1,
                SituacaoReservaMaterial = SituacaoReservaMaterial.AtendidaTotal
            };
        }

        public RequisicaoMaterialItem ObtenhaRequisicaoMaterialItem()
        {
            return new RequisicaoMaterialItem()
            {
                QuantidadeAtendida = 1,
                QuantidadeSolicitada = 1,
                SituacaoRequisicaoMaterial = SituacaoRequisicaoMaterial.AtendidoParcial
            };
        }

        public RequisicaoMaterial ObtenhaRequisicaoMaterial()
        {
            return new RequisicaoMaterial()
            {
                Data = new DateTime(2014, 1, 1),
                Numero = 1,
                Origem = "ORIGEM",
                Observacao = "OBSERVAÇÃO"
            };
        }

        public ReservaMaterial ObtenhaReservaMaterial()
        {
            return new ReservaMaterial()
            {
                Data = new DateTime(2014, 1, 1),
                Numero = 1,
                ReferenciaOrigem = "REF 1",
                DataProgramacao = new DateTime(2014, 1, 1),
                Observacao = "OBSERVAÇÃO",
                SituacaoReservaMaterial = SituacaoReservaMaterial.AtendidaTotal
            };
        }

        public CustoMaterial ObtenhaCustoMaterial()
        {
            return new CustoMaterial
            {
                Ativo = true,
                Custo = 1,
                CustoAquisicao = 1,
                CustoMedio = 1,
                Data = new DateTime(2014,1,1)
            };
        }

        public Bordado ObtenhaBordado()
        {
            return new Bordado
            {
                Aplicacao = "APLICAÇÃO DO BORDADO",
                Descricao = "DESCRIÇÃO DO BORDADO",
                Observacao = "OBSERVAÇÃO",
                Pontos = "PONTOS"
            };
        }

        public Material ObtenhaMaterial()
        {
            return new Material
            {
                Aliquota = 1.0,
                Ativo = true,
                CodigoBarra = "CODBARRA",
                Descricao = "DESCRIÇÃO CAT.MATERIAL",
                Detalhamento = "DETALHAMENTO",
                Referencia = "REFERENCIA",
                Ncm = "NCM",
                PesoBruto = 1.0,
                PesoLiquido = 1.0,
                Localizacao = "LOCALIZAÇÃO DO MATERIAL",
                TipoItem = ObtenhaTipoItem(),
                GeneroFiscal = ObtenhaGeneroFiscal(),
                Tecido = ObtenhaTecido(),
                Bordado = ObtenhaBordado()
            };
        }

        public Categoria ObtenhaCategoria()
        {
            return new Categoria
            {
                Ativo = true,
                CodigoNcm = "CODNCM",
                Nome = "NOME CATEGORIA",
                TipoCategoria = TipoCategoria.MateriaPrima,
                GeneroCategoria = GeneroCategoria.Bordado
            };
        }

        public DepositoMaterial ObtenhaDepositoMaterial()
        {
            return new DepositoMaterial()
            {
                Ativo = true,
                DataAbertura = new DateTime(2014, 1, 1),
                Nome = "NOME DEPOSITO MATERIAL"
            };
        }

        public EntradaMaterial ObtenhaEntradaMaterial()
        {
            return new EntradaMaterial
            {
                DataEntrada = new DateTime(2014, 1, 1)
            };
        }

        public EstoqueMaterial ObtenhaEstoqueMaterial()
        {
            return new EstoqueMaterial
            {
                Quantidade = 1.0,
                Reserva = 1.0
            };
        }

        public Familia ObtenhaFamilia()
        {
            return new Familia
            {
                Ativo = true,
                Nome = "NOME DA FAMÍLIA"
            };
        }

        public GeneroFiscal ObtenhaGeneroFiscal()
        {
            return new GeneroFiscal
            {
                Codigo = "00",
                Descricao = "Serviço",
                Id = 1
            };
        }

        public MarcaMaterial ObtenhaMarcaMaterial()
        {
            return new MarcaMaterial
            {
                Ativo = true,
                Nome = "NOME MARCA MATERIAL"
            };
        }

        public ConferenciaEntradaMaterial ObtenhaConferenciaEntradaMaterial()
        {
            return new ConferenciaEntradaMaterial
            {
                Data = new DateTime(2014, 1, 1),
                Observacao = "OBSERVAÇÃO DA CONFERÊNCIA",
                DataAtualizacao = new DateTime(2014, 1, 1),
                Numero = 1,
                Autorizado = true,
                Conferido = true
            };
        }

        public ConferenciaEntradaMaterialItem ObtenhaConferenciaEntradaMaterialItem()
        {
            return new ConferenciaEntradaMaterialItem
            {
                Quantidade = 5,
                QuantidadeConferida = 5,
                SituacaoConferencia = SituacaoConferencia.Conferida
            };
        }

        public OrigemSituacaoTributaria ObtenhaOrigemSituacaoTributaria()
        {
            return new OrigemSituacaoTributaria
            {
                Codigo = "CODOR",
                Descricao = "DESCRIÇÃO ORIGEM"
            };
        }

        public ReferenciaExterna ObtenhaReferenciaExterna()
        {
            return new ReferenciaExterna
            {
                CodigoBarra = "CODBARRA",
                Descricao = "DESCRIÇÃO REF EXTERNA",
                Preco = 1.0,
                Referencia = "REFERÊNCIA EXTERNA"
            };
        }

        public SaidaMaterial ObtenhaSaidaMaterial()
        {
            return new SaidaMaterial
            {
                DataSaida = new DateTime(2014, 1, 1)
            };
        }

        public SaidaItemMaterial ObtenhaSaidaItemMaterial()
        {
            return new SaidaItemMaterial();
        }

        public Subcategoria ObtenhaSubcategoria()
        {
            return new Subcategoria
            {
                Ativo = true,
                Nome = "NOME SUBCATEGORIA"
            };
        }

        public Tecido ObtenhaTecido()
        {
            return new Tecido
            {
                Armacao = "ARMAÇÃO",
                Composicao = "COMPOSIÇÃO",
                Gramatura = "GRAMATURA",
                Largura = "LARGURA",
                Rendimento = "RENDIMENTO"
            };
        }

        public TipoItem ObtenhaTipoItem()
        {
            return new TipoItem
            {
                Codigo = "00",
                Descricao = "Mercadoria para Revenda",
                Id = 1
            };
        }

        public UnidadeMedida ObtenhaUnidadeMedida()
        {
            return new UnidadeMedida
            {
                Ativo = true,
                Descricao = "DESCRIÇÃO",
                FatorMultiplicativo = 1.0,
                Sigla = "SIGLA"
            };
        }
        
        public EntradaItemMaterial ObtenhaEntradaItemMaterial()
        {
            return new EntradaItemMaterial()
            {
                QuantidadeCompra = 1
            };
        }

        public MovimentacaoEstoqueMaterial ObtenhaMovimentacaoEstoqueMaterial()
        {
            return new MovimentacaoEstoqueMaterial()
            {
                TipoMovimentacaoEstoqueMaterial = TipoMovimentacaoEstoqueMaterial.Entrada,
                Quantidade = 1,
                Data = new DateTime(2014, 1, 1)
            };
        }

        public SimboloConservacao ObtenhaSimboloConservacao()
        {
            return new SimboloConservacao
            {
                Descricao = "DESCRIÇÃO",
                CategoriaConservacao = CategoriaConservacao.Alvejamento,
            };
        }

        #endregion

        #region Engenharia de Produto

        public ModeloAprovacao ObtenhaModeloAprovacao()
        {
            return new ModeloAprovacao
            {
                Descricao = "DESCRIÇÃO MODELO APROVACAO",
                IdEmpresa = 1,
                IdTenant = 1,
                Quantidade = 1,
                MedidaBarra = 1,
                MedidaComprimento = 1,
                Observacao = "OBSERVAÇÃO",
                Referencia = "REFERENCIA"
            };
        }

        public ModeloAvaliacao ObtenhaModeloAvaliacao()
        {
            return new ModeloAvaliacao
            {
                Ano = 2000,
                Aprovado = true,
                Catalogo = true,
                Complemento = "COMPLEMENTO",
                Data = new DateTime(2010, 1, 1),
                Observacao = "OBSERVAÇÃO",
                Tag = "654",
                IdEmpresa = 1,
                IdTenant = 1
            };
        }

        public ModeloReprovacao ObtenhamModeloReprovacao()
        {
            return new ModeloReprovacao
            {
                IdEmpresa = 1,
                IdTenant = 1,
                Motivo = "MOTIVO REPROVAÇÃO"
            };
        }

        public ProgramacaoBordado ObtenhaProgramacaoBordado()
        {
            return new ProgramacaoBordado
            {
                Aplicacao = "APLICAÇÃO",
                Data = new DateTime(2014, 1, 1),
                Descricao = "DESCRIÇÃO",
                NomeArquivo = "NOMEARQUIVO.EXE",
                Observacao = "OBSERVAÇÃO",
                QuantidadeCores = 1,
                QuantidadePontos = 1
            };
        }

        public Barra ObtenhaBarra()
        {
            return new Barra
            {
                Descricao = "DESCRIÇÃO DA BARRA",
                Ativo = true
            };
        }

        public Comprimento ObtenhaComprimento()
        {
            return new Comprimento()
            {
                Ativo = true,
                Descricao = "COMPRIMENTO"
            };
        }

        public ProdutoBase ObtenhaProdutoBase()
        {
            return new ProdutoBase()
            {
                Ativo = true,
                Descricao = "DESCRIÇÃO PRODUTO BASE"
            };
        }

        public Grade ObtenhaGrade()
        {
            return new Grade
            {
                Descricao = "DESCRIÇÃO DA GRADE",
                Ativo = true,
                DataCriacao = new DateTime(2014, 7, 7)
            };
        }

        public Artigo ObtenhaArtigo()
        {
            return new Artigo
            {
                Ativo = true,
                Descricao = "DESCRIÇÃO DO ARTIGO"
            };
        }

        public Segmento ObtenhaSegmento()
        {
            return new Segmento()
            {
                Ativo = true,
                Descricao = "DESCRIÇÃO DO SEGMENTO"
            };
        }

        public Classificacao ObtenhaClassificacao()
        {
            return new Classificacao
            {
                Ativo = true,
                Descricao = "DESCRIÇÃO DA CLASSIFICAÇÃO"
            };
        }

        public Marca ObtenhaMarca()
        {
            return new Marca
            {
                Ativo = true,
                Nome = "NOME DA MARCA"
            };
        }

        public Natureza ObtenhaNatureza()
        {
            return new Natureza
            {
                Ativo = true,
                Descricao = "DESCRIÇÃO DA NATUREZA"
            };
        }

        public SequenciaProducao ObtenhaSequenciaProducao()
        {
            return new SequenciaProducao
            {
                DataEntrada = new DateTime(2014, 1, 1),
                DataSaida = new DateTime(2014, 1, 1)
            };
        }
        
        public ModeloMaterialConsumo ObtenhaModeloMaterialConsumo()
        {
            return new ModeloMaterialConsumo
            {
                Quantidade = 1,
            };
        }

        public SetorProducao ObtenhaSetorProducao()
        {
            return new SetorProducao
            {
                Ativo = true,
                Nome = "NOME SETOR PRODUÇÂO"
            };
        }

        public Colecao ObtenhaColecao()
        {
            return new Colecao
            {
                Ativo = true,
                Descricao = "DESCRIÇÃO COLEÇÃO"
            };
        }

        public Modelo ObtenhaModelo()
        {
            var modelo =  new Modelo
            {
                Descricao = "DESCRIÇÃO DO MODELO",
                Referencia = "0001",
                Tecido = "TECIDO DO MODELO",
                Detalhamento = "DETALHAMENTO DO TECIDO",
                Complemento = "COMPLEMENTO DO TECIDO",
                DataCriacao = new DateTime(2014, 1, 1),
                DataAlteracao = new DateTime(2014, 1, 1),
                Aprovado = true,
                //DataAprovacao = new DateTime(2014, 1, 1),
                //ObservacaoAprovacao = "OBSERVAÇÃO APROVAÇÃO",
                Observacao = "OBSERVAÇÃO",
                Cos = 10,
                Passante = 10,
                Entrepernas = 10,
                Localizacao = "LOCALIZAÇÃO",
                TamanhoPadrao = "TAMANHO",
                LinhaCasa = "LINHA CASA",
                Lavada = "LAVADA",
                Boca = 10,
                Modelagem = "MODELAGEM",
                EtiquetaMarca = "ETIQUETA MARCA",
                EtiquetaComposicao = "ETIQUETA COMPOSIÇÃO",
                //Tag = "TAG",
                DataModelagem = new DateTime(2014, 01, 01),
                TecidoComplementar = "TECIDO COMPLEMENTAR",
                Forro = "FORRO",
                ZiperBraguilha = "ZIPER BRAGUILHA",
                ZiperDetalhe = "ZIPER DETALHE",
                Dificuldade = "DIFICULDADE",
                //QuantidadeMix = 1,
                DataRemessaProducao = new DateTime(2014, 01, 01),
                //AnoAprovacao = 2014,
                //NumeroAprovacao = 5,
                Grade = ObtenhaGrade(),
                Colecao = ObtenhaColecao(),
                Natureza = ObtenhaNatureza(),
                Marca = ObtenhaMarca(),
                Artigo = ObtenhaArtigo(),
                //DataPrevisaoEnvio = new DateTime(2014, 01, 01),
                ChaveExterna = "ADFRONFY"
            };

            modelo.AddLinhaBordado(new[] { "linha bodado 1", "linha bodado 2" });
            modelo.AddLinhaPesponto(new[] {"linha pestonto 1", "linha pesponto 2"});
            modelo.AddLinhaTravete(new[] { "linha travete 1", "linha travete 2" });

            return modelo;
        }

        public VariacaoModelo ObtenhaVariacaoModelo()
        {
            return new VariacaoModelo();
        }
        
        #endregion

        #region Financeiro

        public DespesaReceita ObtenhaDespesaReceita()
        {
            return new DespesaReceita
            {
                Descricao = "DESCRIÇÃO DA GRADE",
                Ativo = true,
                TipoDespesaReceita = TipoDespesaReceita.Impostos
            };
        }

        #endregion

        #region Compras
        public PedidoCompraItem ObtenhaPedidoCompraItem()
        {
            return new PedidoCompraItem
            {
                DataEntrega = new DateTime(2014,1,1),
                Quantidade = 1.0,
                PrevisaoEntrega = new DateTime(2014,1,1),
                SituacaoCompra = SituacaoCompra.AtendidoParcial,
                ValorUnitario = 1.0,
                QuantidadeEntrega = 1.0
            };
        }

        public PedidoCompraItemCancelado ObtenhaPedidoCompraItemCancelado()
        {
            return new PedidoCompraItemCancelado
            {
                Data = new DateTime(2014, 1, 1),
                QuantidadeCancelada = 1.0,
                Observacao = "OBSERVAÇÃO PEDIDO COMPRA ITEM CANCELADO"
            };
        }

        public PedidoCompra ObtenhaPedidoCompra()
        {
            return new PedidoCompra
            {
                Autorizado = true,
                Contato = "CONTATO",
                DataAutorizacao = new DateTime(2014, 1, 1),
                Numero = 1,
                DataCompra = new DateTime(2014, 1, 1),
                PrevisaoFaturamento = new DateTime(2014, 1, 1),
                PrevisaoEntrega = new DateTime(2014, 1, 1),
                TipoCobrancaFrete = TipoCobrancaFrete.Destinatario,
                ValorFrete = 1,
                ValorDesconto = 1,
                ValorCompra = 1,
                Observacao = "OBSERVAÇÃO PEDIDO COMPRA",
                ObservacaoAutorizacao = "OBSERVAÇÃO AUTORIZAÇÃO",
                SituacaoCompra = SituacaoCompra.AtendidoParcial,
                ValorEncargos = 1,
                ValorEmbalagem = 1
                
            };
        }

        public RecebimentoCompra ObtenhaRecebimentoCompra()
        {
            return new RecebimentoCompra
            {
                Numero = 1,
                SituacaoRecebimentoCompra = SituacaoRecebimentoCompra.Finalizada,
                Data = new DateTime(2014, 1, 1),
                DataAlteracao = new DateTime(2014, 1, 1),
                Observacao = "OBSERVAÇÃO DO RECEBIMENTO",
                Valor = 1.0
            };
        }

        public RecebimentoCompraItem ObtenhaRecebimentoCompraItem()
        {
            return new RecebimentoCompraItem
            {
                ValorUnitario = 1,
                Quantidade = 1,
                ValorTotal = 1
            };
        }

        public DetalhamentoRecebimentoCompraItem ObtenhaDetalhamentoRecebimentoPedidoCompra()
        {
            return new DetalhamentoRecebimentoCompraItem
            {
                Quantidade = 1
            };
        }
        
        public MotivoCancelamentoPedidoCompra ObtenhaMotivoCancelamentoPedidoCompra()
        {
            return new MotivoCancelamentoPedidoCompra
            {
                Ativo = true,
                Descricao = "DESCRIÇÃO MOTIVO"
            };
        }

        public OrdemEntradaCompra ObtenhaOrdemEntradaCompra()
        {
            return new OrdemEntradaCompra
            {
                Data = new DateTime(2014, 1, 1),
                DataAlteracao = new DateTime(2014, 1, 1),
                Numero = 10,
                Observacao = "OBSERVAÇÃO DA ORDEM ENTRADA",
                SituacaoOrdemEntradaCompra = SituacaoOrdemEntradaCompra.Cancelada,

            };
        }

        public ParametroModuloCompra ObtenhaParametroModuloCompra()
        {
            return new ParametroModuloCompra
            {
                ValidaRecebimentoPedido = true
            };
        }

        public ProcedimentoModuloCompras ObtenhaProcedimentoModuloCompras()
        {
            return new ProcedimentoModuloCompras()
            {
                Codigo = 1,
                Descricao = "DESCRIÇÃO PROCEDIMENTO"
            };
        }

        public Pessoa ObtenhaTransportadora()
        {
            var pessoa = ObtenhaPessoa();
            pessoa.Transportadora = CrieTransportadora();
            pessoa.AddContato(ObtenhaContato());
            pessoa.AddEndereco(ObtenhaEndereco());

            return pessoa;
        }

        private Transportadora CrieTransportadora()
        {
            return new Transportadora
            {
                Ativo = true,
                Codigo = 1,
                DataCadastro = new DateTime(2014, 1, 1)
            };
        }

        #endregion

        #region Comum

        public Variacao ObtenhaVariacao()
        {
            return new Variacao()
            {
                Ativo = true,
                Nome = "NOME DA VARIAÇÃO"
            };
        }

        public UltimoNumero ObtenhaUltimoNumero()
        {
            return new UltimoNumero
            {
                NomeTabela = "NOMEDATABELA",
                Numero = 1
            };
        }

        public SequenciaOperacional ObtenhaSequenciaOperacional()
        {
            return new SequenciaOperacional
            {
                Sequencia = 1
            };
        }

        public ProcessoOperacional ObtenhaProcessoOperacional()
        {
            return new ProcessoOperacional
            {
                Descricao = "DESCRIÇÃO",
                Ativo = true
            };
        }

        public Arquivo ObtenhaArquivo()
        {
            return new Arquivo()
            {
                Data = DateTime.Now,
                Extensao = "txt",
                Nome = "NOME ARQUIVO",
                Tamanho = 1.5,
                Titulo = "TITULO DO ARQUIVO"
            };
        }

        public Pessoa ObtenhaEmpresa()
        {
            var pessoa = ObtenhaPessoa();
            pessoa.Empresa = CrieEmpresa();
            pessoa.AddContato(ObtenhaContato());
            pessoa.AddEndereco(ObtenhaEndereco());
            return pessoa;
        }

        private Empresa CrieEmpresa()
        {
            return new Empresa
            {
                Ativo = true,
                DataCadastro = new DateTime(2014, 07, 15),
                Codigo = 1
            };
        }

        public Pessoa ObtenhaUnidade()
        {
            var pessoa = ObtenhaPessoa();
            pessoa.Unidade = CrieUnidade();
            pessoa.AddContato(ObtenhaContato());
            pessoa.AddEndereco(ObtenhaEndereco());
            return pessoa;
        }

        private Unidade CrieUnidade()
        {
            return new Unidade
            {
                Ativo = true,
                Codigo = 10,
                DataAbertura = new DateTime(2014, 1, 1),
                DataCadastro = new DateTime(2014, 1, 1)
            };
        }

        public TipoFornecedor ObtenhaTipoFornecedor()
        {
            return new TipoFornecedor
            {
                Descricao = "DESCRIÇÃO TIPO FORNECEDOR"
            };
        }

        public AreaInteresse ObtenhaAreaInteresse()
        {
            return new AreaInteresse
            {
                Nome = "NOME DA ÁREA DE INTERESSE"
            };
        }


        public Pessoa ObtenhaFornecedor()
        {
            var pessoa = ObtenhaPessoa();
            pessoa.Fornecedor = CrieFornecedor();
            pessoa.AddContato(ObtenhaContato());
            pessoa.AddEndereco(ObtenhaEndereco());

            return pessoa;
        }

        private Fornecedor CrieFornecedor()
        {
            return new Fornecedor
            {
                Ativo = true,
                Codigo = 1,
                DataCadastro = new DateTime(2014, 1, 1)
            };
        }

        public Dependente ObtenhaDependente()
        {
            return new Dependente
            {
                Cpf = "321654987",
                Nome = "NOME DO DEPENDENTE",
                OrgaoExpedidor = "DGPC",
                Rg = "321654"
            };
        }

        public Cliente ObtenhaCliente()
        {
            return new Cliente
            {
                Codigo = 10,
                DataCadastro = new DateTime(2014, 1, 1),
                DataValidade = new DateTime(2014, 1, 1),
                EstadoCivil = EstadoCivil.Casado,
                NomeMae = "NOME DA MÃE",
                Observacao = "OBSERVAÇÃO DO CLIENTE",
                Sexo = Sexo.Masculino
            };
        }

        public ClassificacaoDificuldade ObtenhaClassificacaoDificuldade()
        {
            return new ClassificacaoDificuldade
            {
                Ativo = true,
                Criacao = true,
                Descricao = "DESCRICAO CLASSIFICACAO DIFICULDADE",
                Producao = true
            };
        }

        public CentroCusto ObtenhaCentroCusto()
        {
            return new CentroCusto
            {
                Ativo = true,
                Codigo = 1,
                Nome = "NOME DO CENTRO DE CUSTO"
            };
        }

        public Usuario ObtenhaUsuario()
        {
            return new Usuario
            {
                Administrador = true,
                ConcedeAcesso = true,
                Login = "LOGIN DO USUÁRIO",
                Nome = "NOME DO USUÁRIO",
                Senha = "SENHA"
            };
        }

        public Tamanho ObtenhaTamanho()
        {
            return new Tamanho
            {
                Ativo = true,
                Descricao = "DESCRIÇÃO DO TAMANHO",
                Sigla = "SIGLA"
            };
        }

        public OperacaoProducao ObtenhaOperacaoProducao()
        {
            return new OperacaoProducao
            {
                Ativo = true,
                Custo = 1,
                Descricao = "Descrição",
                PesoProdutividade = 1,
                Tempo = 1
            };
        }

        public Referencia ObtenhaReferencia()
        {
            return new Referencia
            {
                Celular = "99999999",
                Nome = "NOME DA REFERÊNCIA",
                Observacao = "OBSERVAÇÃO DA REFERÊNCIA",
                Telefone = "3214 3214",
                TipoReferencia = TipoReferencia.Comercial
            };
        }

        public Profissao ObtenhaProfissao()
        {
            return new Profissao
            {
                Nome = "NOME DA PROFISSÃO"
            };
        }

        public PrestadorServico ObtenhaPrestadorServico()
        {
            var prestador = new PrestadorServico
            {
                Ativo = true,
                Codigo = 5,
                Comissao = 5.1,
                DataCadastro = new DateTime(2014, 1, 1),
            };

            prestador.AddTipoPrestadorServico(new []
            {
                TipoPrestadorServico.Assessor, 
                TipoPrestadorServico.Representante, 
                TipoPrestadorServico.Transportador
            });

            return prestador;
        }

        public Prazo ObtenhaPrazo()
        {
            return new Prazo
            {
                Ativo = true,
                AVista = true,
                Descricao = "DESCRICAO DO PRAZO",
                Intervalo = 5,
                Padrao = true,
                PrazoPrimeiraParcela = 10,
                QuantidadeParcelas = 5
            };
        }

        public Permissao ObtenhaPermissao()
        {
            return new Permissao
            {
                Action = "ACTION DA PERMISSAO",
                Area = "AREA DA PERMISSAO",
                Controller = "CONTROLLER DA PERMISSAO",
                Descricao = "DESCRICAO DA PERMISSAO",
                ExibeNoMenu = true,
                Ordem = 10
            };
        }

        public PerfilDeAcesso ObtenhaPerfilDeAcesso()
        {
            return new PerfilDeAcesso
            {
                Nome = "NOME PERFIL DE ACESSO"
            };
        }

        public Pais ObtenhaPais()
        {
            return new Pais
            {
                CodigoBacen = 100,
                Nome = "NOME PAIS"
            };
        }

        public MeioPagamento ObtenhaMeioPagamento()
        {
            return new MeioPagamento
            {
                Descricao = "DESCRIÇÃO MEIO PAGAMENTO"
            };
        }

        public InformacaoBancaria ObtenhaInformacaoBancaria()
        {
            return new InformacaoBancaria
            {
                Agencia = "0235",
                Banco = ObtenhaBanco(),
                Conta = "1321351",
                DataAbertura = new DateTime(2014, 1, 1),
                Telefone = "9999999",
                TipoConta = TipoConta.Corrente,
                Titular = "TITULAR"
            };
        }

        public Banco ObtenhaBanco()
        {
            return new Banco
            {
                Nome = "BARCLAYS",
                Codigo = 740,
                Id = 10
            };
        }

        public GrauDependencia ObtenhaGrauDependencia()
        {
            return new GrauDependencia
            {
                Descricao = "DESCRIÇÃO DO GRAU DEPENDÊNCIA"
            };
        }

        public DepartamentoProducao ObtenhaDepartamentoProducao()
        {
            return new DepartamentoProducao
            {
                Ativo = true,
                Criacao = true,
                Nome = "NOME DEPARTAMENTO PRODUCAO",
                Producao = true
            };
        }

        public Pessoa ObtenhaFuncionario(FuncaoFuncionario funcao)
        {
            var pessoa = ObtenhaPessoa();

            pessoa.Funcionario = CrieFuncionario(FuncaoFuncionario.Estilista);
            pessoa.AddContato(ObtenhaContato());
            pessoa.AddEndereco(ObtenhaEndereco());

            return pessoa;
        }

        private Funcionario CrieFuncionario(FuncaoFuncionario funcao)
        {
            return new Funcionario
            {
                Ativo = true,
                Codigo = 1,
                DataCadastro = new DateTime(2014, 1, 1),
                DataDesligamento = new DateTime(2014, 1, 1),
                FuncaoFuncionario = funcao,
                PercentualComissao = 2
            };
        }

        public Pessoa ObtenhaPessoa()
        {
            var pessoa = new Pessoa
            {
                CpfCnpj = "7704727" + new Random(Guid.NewGuid().GetHashCode()).Next(900000, 1000000),
                Nome = "NOME DA PESSOA",
                NomeFantasia = "NOME FANTASIA",
                DataCadastro = new DateTime(2014, 1, 1),
                DataNascimento = new DateTime(2014, 1, 1),
                DocumentoIdentidade = "111111",
            };

            pessoa.AddContato(ObtenhaContato());
            pessoa.AddEndereco(ObtenhaEndereco());

            return pessoa;
        }

        public Contato ObtenhaContato()
        {
            return new Contato
            {
                Email = "contato@email.com",
                Nome = "NOME DO CONTATO",
                Operadora = "RRR",
                Telefone = "9999-9999",
                TipoContato = TipoContato.Comercial
            };
        }

        public Endereco ObtenhaEndereco()
        {
            return new Endereco
            {
                Bairro = "BAIRRO DO ENDEREÇO",
                Logradouro = "LOGRADOURO DO ENDEREÇO",
                Cep = "75000000",
                Cidade = ObtenhaCidade()
            };
        }

        public Cidade ObtenhaCidade()
        {
            return new Cidade
            {
                Id = 1924,
                CodigoIbge = 5208707,
                UF = new UF
                {
                    Id = 9
                }
            };
        }

        public Cor ObtenhaCor()
        {
            return new Cor
            {
                Ativo = true,
                Nome = "NOME DA COR"
            };
        }

        public OperacaoProducao ObtenhaOperacao()
        {
            return new OperacaoProducao
            {
                Ativo = true,
                Descricao = "DESCRIÇÃO DA OPERAÇÃO",
                Custo = 10,
                Tempo = 10,
                PesoProdutividade = 10
            };
        }
        #endregion

        #region Producao

        public FichaTecnicaModelagem ObtenhaFichaTecnicaModelagem()
        {
            return new FichaTecnicaModelagem()
            {
                DataModelagem = new DateTime(2015, 1, 1),
                Observacao = "OBSERVAÇÃO"
            };
        }

        public FichaTecnicaModelagemMedida ObtenhaFichaTecnicaModelagemMedida()
        {
            return new FichaTecnicaModelagemMedida()
            {
                DescricaoMedida = "DESCRIÇÃO MEDIDA"
            };
        }

        public FichaTecnicaModelagemMedidaItem ObtenhaFichaTecnicaModelagemMedidaItem()
        {
            return new FichaTecnicaModelagemMedidaItem()
            {
                Medida = 1.1
            };
        }

        public FichaTecnicaMaterialConsumoVariacao ObtenhaMaterialConsumoItem()
        {
            return new FichaTecnicaMaterialConsumoVariacao
            {
                Custo = 1,
                Quantidade = 1,
                CompoeCusto = true
            };
        }

        public FichaTecnicaMaterialConsumo ObtenhaMaterialConsumo()
        {
            return new FichaTecnicaMaterialConsumo()
            {
                Custo = 1,
                Quantidade = 1
            };
        }

        public FichaTecnicaMaterialComposicaoCusto ObtenhaMaterialComposicaoCusto()
        {
            return new FichaTecnicaMaterialComposicaoCusto()
            {
                Custo = 1
            };
        }

        public FichaTecnicaSequenciaOperacional OBtenhaFichaTecnicaSequenciaOperacional()
        {
            return new FichaTecnicaSequenciaOperacional
            {
                Custo = 1,
                PesoProdutividade = 1,
                Tempo = 1
            };
        }

        public FichaTecnicaVariacaoMatriz ObtenhaFichaTecnicaVariacaoMatriz()
        {
            return new FichaTecnicaVariacaoMatriz();
        }

        public FichaTecnicaFoto ObtenhaFichaTecnicaFoto()
        {
            return new FichaTecnicaFoto()
            {
                Descricao = "DESCRIÇÃO",
                Impressao = true,
                Padrao = true
            };
        }

        public FichaTecnicaMatriz ObtenhaFichaTecnicaMatriz()
        {
            return new FichaTecnicaMatriz();
        }

        public FichaTecnicaJeans ObtenhaFichaTecnicaJeans()
        {
            var fichaTecnicajeans = new FichaTecnicaJeans()
            {
                Referencia = "12364",
                Descricao = "DESCRIÇÃO DO MODELO",
                Detalhamento = "DETALHAMENTO DO TECIDO",
                DataAlteracao = new DateTime(2014, 1, 1),
                Observacao = "OBSERVAÇÃO",
                MedidaCos = 1,
                MedidaPassante = 1,
                MedidaComprimento = 1,
                Lavada = "LAVADA",
                MedidaBarra = 1,
                Tag = "TAG",
                Ano = 2015,
                DataCadastro = new DateTime(2015, 1, 1),
                Pedraria = "PEDRARIA",
                Silk = "SILK",
                Catalogo = true,
                Complemento = "COMPLEMENTO"
                //Variante = 1
            };

            return fichaTecnicajeans;
        }

        public ProgramacaoProducao ObtenhaProgramacaoProducao()
        {
            return new ProgramacaoProducao()
            {
                Data = new DateTime(2015, 1, 1),
                DataProgramada = new DateTime(2015, 1, 1),
                Lote = 1,
                Ano = 2015,
                Quantidade = 1,
                Observacao = "OBSERVAÇÃO"
            };
        }

        public RemessaProducao ObtenhaRemessaProducao()
        {
            return new RemessaProducao
            {
                Ano = 2015,
                DataAlteracao = new DateTime(2015, 1, 1),
                DataLimite = new DateTime(2015, 1, 1),
                Descricao = "DESCRIÇÃO DA REMESSA DE PRODUÇÃO",
                DataInicio = new DateTime(2015, 1, 1),
                Numero = 2
            };
        }

        public RemessaProducaoCapacidadeProdutiva ObrenhaRemessaProducaoCapacidadeProdutiva()
        {
            return new RemessaProducaoCapacidadeProdutiva
            {
                Quantidade = 1
            };
        }

        public ProgramacaoProducaoItem ObtenhaProgramacaoProducaoItem()
        {
            return new ProgramacaoProducaoItem
            {
                Quantidade = 1
            };            
        }

        public ProgramacaoProducaoMaterial ObtenhaProducaoProducaoMaterial()
        {
            return new ProgramacaoProducaoMaterial
            {
                Quantidade = 1
            };            
        }
        
        public ProgramacaoProducaoMatrizCorte ObtenhaProgramacaoProducaoMatrizCorte()
        {
            return new ProgramacaoProducaoMatrizCorte
            {
                TipoEnfestoTecido = TipoEnfestoTecido.Folha
            };
        }

        public ProgramacaoProducaoMatrizCorteItem ObtenhaProgramacaoProducaoMatrizCorteItem()
        {
            return new ProgramacaoProducaoMatrizCorteItem
            {
                Quantidade = 1,
                QuantidadeVezes = 1
            };
        }

        #endregion
    }
}