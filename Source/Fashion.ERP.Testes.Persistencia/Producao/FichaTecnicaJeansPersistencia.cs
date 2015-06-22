using Fashion.ERP.Domain;
using Fashion.ERP.Domain.Almoxarifado;
using Fashion.ERP.Domain.Comum;
using Fashion.ERP.Domain.EngenhariaProduto;
using Fashion.ERP.Domain.Producao;
using Fashion.Framework.UnitOfWork;
using NUnit.Framework;

namespace Fashion.ERP.Testes.Persistencia.Producao
{
    [TestFixture]
    public class FichaTecnicaJeansPersistencia : TestPersistentObject<FichaTecnicaJeans>
    {
        private Grade _grade;
        private Classificacao _classificacao;
        private Colecao _colecao;
        private Natureza _natureza;
        private Artigo _artigo;
        private Marca _marca;
        private Variacao _variacao;
        private FichaTecnicaVariacaoMatriz _fichaTecnicaVariacaoMatriz;
        private FichaTecnicaMatriz _fichaTecnicaMatriz;
        private FichaTecnicaFoto _fichaTecnicaFoto;
        private Cor _cor;
        private ClassificacaoDificuldade _classificacaoDificuldade;
        private Segmento _segmento;
        private Barra _barra;
        private ProdutoBase _produtoBase;
        private Comprimento _comprimento;
        private Arquivo _arquivo;
        private SetorProducao _setorProducao;
        private DepartamentoProducao _departamentoProducao;
        private OperacaoProducao _operacaoProducao;
        private FichaTecnicaSequenciaOperacional _fichaTecnicaSequenciaOperacional;
        private FichaTecnicaMaterialComposicaoCusto _materialComposicaoCusto;
        private Material _material;
        private Tamanho _tamanho;
        private FichaTecnicaMaterialConsumo _materialConsumo;
        private FichaTecnicaMaterialConsumoVariacao _materialConsumoVariacao;
        private Pessoa _estilista;
        private FichaTecnicaModelagem _fichaTecnicaModelagem;
        
        public override FichaTecnicaJeans GetPersistentObject()
        {
            var fichaTecnicaJeans = FabricaObjetos.ObtenhaFichaTecnicaJeans();
            
            fichaTecnicaJeans.Classificacao = _classificacao;
            fichaTecnicaJeans.Colecao = _colecao;
            fichaTecnicaJeans.Natureza = _natureza;
            fichaTecnicaJeans.Artigo = _artigo;
            fichaTecnicaJeans.Marca = _marca;
            fichaTecnicaJeans.FichaTecnicaMatriz = _fichaTecnicaMatriz;
            fichaTecnicaJeans.Barra = _barra;
            fichaTecnicaJeans.Segmento = _segmento;
            fichaTecnicaJeans.ClassificacaoDificuldade = _classificacaoDificuldade;
            fichaTecnicaJeans.ProdutoBase = _produtoBase;
            fichaTecnicaJeans.Comprimento = _comprimento;
            fichaTecnicaJeans.Estilista = _estilista;
            fichaTecnicaJeans.FichaTecnicaSequenciaOperacionals.Add(_fichaTecnicaSequenciaOperacional);
            fichaTecnicaJeans.MateriaisComposicaoCusto.Add(_materialComposicaoCusto);
            fichaTecnicaJeans.MateriaisConsumo.Add(_materialConsumo);
            fichaTecnicaJeans.FichaTecnicaModelagem = _fichaTecnicaModelagem;
            fichaTecnicaJeans.FichaTecnicaFotos.Add(_fichaTecnicaFoto);
            return fichaTecnicaJeans;
        }

        public override void Init()
        {
            _grade = FabricaObjetosPersistidos.ObtenhaGrade();
            _classificacao = FabricaObjetosPersistidos.ObtenhaClassificacao();
            _colecao = FabricaObjetosPersistidos.ObtenhaColecao();
            _natureza = FabricaObjetosPersistidos.ObtenhaNatureza();
            _marca = FabricaObjetosPersistidos.ObtenhaMarca();
            _artigo = FabricaObjetosPersistidos.ObtenhaArtigo();
            _cor = FabricaObjetosPersistidos.ObtenhaCor();
            _variacao = FabricaObjetosPersistidos.ObtenhaVariacao();
            _classificacaoDificuldade = FabricaObjetosPersistidos.ObtenhaClassificacaoDificuldade();
            _segmento = FabricaObjetosPersistidos.ObtenhaSegmento();
            _barra = FabricaObjetosPersistidos.ObtenhaBarra();
            _produtoBase = FabricaObjetosPersistidos.ObtenhaProdutoBase();
            _comprimento = FabricaObjetosPersistidos.ObtenhaComprimento();
            _arquivo = FabricaObjetosPersistidos.ObtenhaArquivo();
            _setorProducao = FabricaObjetosPersistidos.ObtenhaSetorProducao();
            _departamentoProducao = FabricaObjetosPersistidos.ObtenhaDepartamentoProducao();
            _operacaoProducao = FabricaObjetosPersistidos.ObtenhaOperacaoProducao();
            _material = FabricaObjetosPersistidos.ObtenhaMaterial();
            _tamanho = FabricaObjetosPersistidos.ObtenhaTamanho();
            _estilista = FabricaObjetosPersistidos.ObtenhaFuncionario();

            _fichaTecnicaVariacaoMatriz = FabricaObjetos.ObtenhaFichaTecnicaVariacaoMatriz();
            _fichaTecnicaVariacaoMatriz.Variacao = _variacao;
            _fichaTecnicaVariacaoMatriz.AddCor(_cor);
            
            _fichaTecnicaMatriz = FabricaObjetos.ObtenhaFichaTecnicaMatriz();
            _fichaTecnicaMatriz.Grade = _grade;
            _fichaTecnicaMatriz.FichaTecnicaVariacaoMatrizs.Add(_fichaTecnicaVariacaoMatriz);
            
            _fichaTecnicaFoto = FabricaObjetos.ObtenhaFichaTecnicaFoto();
            _fichaTecnicaFoto.Arquivo = _arquivo;

            _fichaTecnicaSequenciaOperacional = FabricaObjetos.OBtenhaFichaTecnicaSequenciaOperacional();
            _fichaTecnicaSequenciaOperacional.SetorProducao = _setorProducao;
            _fichaTecnicaSequenciaOperacional.DepartamentoProducao = _departamentoProducao;
            _fichaTecnicaSequenciaOperacional.OperacaoProducao = _operacaoProducao;

            _materialComposicaoCusto = FabricaObjetos.ObtenhaMaterialComposicaoCusto();
            _materialComposicaoCusto.Material = _material;

            _materialConsumo = FabricaObjetos.ObtenhaMaterialConsumo();
            _materialConsumo.DepartamentoProducao = _departamentoProducao;
            _materialConsumo.Material = _material;
            
            _materialConsumoVariacao = FabricaObjetos.ObtenhaMaterialConsumoItem();
            _materialConsumoVariacao.DepartamentoProducao = _departamentoProducao;
            _materialConsumoVariacao.Material = _material;
            _materialConsumoVariacao.Tamanho = _tamanho;
            _materialConsumoVariacao.FichaTecnicaVariacaoMatriz = _fichaTecnicaVariacaoMatriz;
            
            var fichaTecnicaModelagemMedida = FabricaObjetos.ObtenhaFichaTecnicaModelagemMedida();

            var fichaTecnicaModelagemMedidaItem = FabricaObjetos.ObtenhaFichaTecnicaModelagemMedidaItem();
            fichaTecnicaModelagemMedidaItem.Tamanho = _tamanho;
            fichaTecnicaModelagemMedida.Itens.Add(fichaTecnicaModelagemMedidaItem);

            _fichaTecnicaModelagem = FabricaObjetos.ObtenhaFichaTecnicaModelagem();
            _fichaTecnicaModelagem.Modelista = _estilista;
            _fichaTecnicaModelagem.Arquivo = FabricaObjetos.ObtenhaArquivo();
            _fichaTecnicaModelagem.Medidas.Add(fichaTecnicaModelagemMedida);

            Session.Current.Flush();
        }

        public override void Cleanup()
        {
            FabricaObjetosPersistidos.ExcluaGrade(_grade);
            FabricaObjetosPersistidos.ExcluaClassificacao(_classificacao);
            FabricaObjetosPersistidos.ExcluaColecao(_colecao);
            FabricaObjetosPersistidos.ExcluaMarca(_marca);
            FabricaObjetosPersistidos.ExcluaArtigo(_artigo);
            FabricaObjetosPersistidos.ExcluaCor(_cor);
            FabricaObjetosPersistidos.ExcluaVariacao(_variacao);
            FabricaObjetosPersistidos.ExcluaComprimento(_comprimento);
            FabricaObjetosPersistidos.ExcluaBarra(_barra);
            FabricaObjetosPersistidos.ExcluaProdutoBase(_produtoBase);
            FabricaObjetosPersistidos.ExcluaSegmento(_segmento);
            FabricaObjetosPersistidos.ExcluaClassificacaoDificuldade(_classificacaoDificuldade);
            FabricaObjetosPersistidos.ExcluaSetorProducao(_setorProducao);
            FabricaObjetosPersistidos.ExcluaDepartamentoProducao(_departamentoProducao);
            FabricaObjetosPersistidos.ExcluaOperacaoProducao(_operacaoProducao);
            FabricaObjetosPersistidos.ExcluaPessoa(_estilista);
            Session.Current.Flush();
        }
    }
}