using Fashion.ERP.Domain.Almoxarifado;
using Fashion.ERP.Domain.Comum;
using Fashion.ERP.Domain.EngenhariaProduto;
using Fashion.Framework.UnitOfWork;
using NUnit.Framework;

namespace Fashion.ERP.Testes.Persistencia.EngenhariaProduto
{
    [TestFixture]
    public class ModeloPersistencia : TestPersistentObject<Modelo>
    {
        private Grade _grade;
        private Classificacao _classificacao;
        private Colecao _colecao;
        private Natureza _natureza;
        private Artigo _artigo;
        private Marca _marca;
        private Pessoa _funcionario;
        private SequenciaProducao _sequenciaProducao;
        private SequenciaProducao _sequenciaProducao2;
        private DepartamentoProducao _departamentoProducao;
        private SetorProducao _setorProducao;
        private ProgramacaoBordado _programacaoBordado;
        private Variacao _variacao;
        private Cor _cor;
        private MaterialComposicaoModelo _materialComposicaoModelo;
        private MaterialComposicaoModelo _materialComposicaoModelo2;
        private Material _material;
        private UnidadeMedida _unidadeMedida;

        public override Modelo GetPersistentObject()
        {
            var modelo = FabricaObjetos.ObtenhaModelo();

            modelo.Grade = _grade;
            modelo.Classificacao = _classificacao;
            modelo.Colecao = _colecao;
            modelo.Natureza = _natureza;
            modelo.Artigo = _artigo;
            modelo.Marca = _marca;
            modelo.Estilista = _funcionario;
            modelo.AddSequenciaProducao(_sequenciaProducao);
            modelo.AddSequenciaProducao(_sequenciaProducao2);
            modelo.AddProgramacaoBordado(_programacaoBordado);
            modelo.AddVariacao(_variacao);

            return modelo;
        }

        public override void Init()
        {
            _grade = FabricaObjetosPersistidos.ObtenhaGrade();
            _classificacao = FabricaObjetosPersistidos.ObtenhaClassificacao();
            _colecao = FabricaObjetosPersistidos.ObtenhaColecao();
            _natureza = FabricaObjetosPersistidos.ObtenhaNatureza();
            _marca = FabricaObjetosPersistidos.ObtenhaMarca();
            _artigo = FabricaObjetosPersistidos.ObtenhaArtigo();
            _funcionario = FabricaObjetosPersistidos.ObtenhaFuncionario();
            _departamentoProducao = FabricaObjetosPersistidos.ObtenhaDepartamentoProducao();
            _setorProducao = FabricaObjetosPersistidos.ObtenhaSetorProducao();
            _cor = FabricaObjetosPersistidos.ObtenhaCor();
            _material = FabricaObjetosPersistidos.ObtenhaMaterial();
            _unidadeMedida = FabricaObjetosPersistidos.ObtenhaUnidadeMedida();
            
            _materialComposicaoModelo = FabricaObjetos.ObtenhaMaterialComposicaoModelo();
            _materialComposicaoModelo.Cor = _cor;
            _materialComposicaoModelo.Material = _material;
            _materialComposicaoModelo.UnidadeMedida = _unidadeMedida;

            _materialComposicaoModelo2 = FabricaObjetos.ObtenhaMaterialComposicaoModelo();
            _materialComposicaoModelo2.Cor = _cor;
            _materialComposicaoModelo2.Material = _material;
            _materialComposicaoModelo2.UnidadeMedida = _unidadeMedida;

            _sequenciaProducao = FabricaObjetos.ObtenhaSequenciaProducao();
            _sequenciaProducao.DepartamentoProducao = _departamentoProducao;
            _sequenciaProducao.SetorProducao = _setorProducao;
            _sequenciaProducao.MaterialComposicaoModelos.Add(_materialComposicaoModelo);
            _sequenciaProducao.MaterialComposicaoModelos.Add(_materialComposicaoModelo2);

            _sequenciaProducao2 = FabricaObjetos.ObtenhaSequenciaProducao();
            _sequenciaProducao2.DepartamentoProducao = _departamentoProducao;
            _sequenciaProducao2.SetorProducao = _setorProducao;

            _programacaoBordado = FabricaObjetos.ObtenhaProgramacaoBordado();
            _programacaoBordado.ProgramadorBordado = _funcionario;
            _programacaoBordado.Arquivo = FabricaObjetosPersistidos.ObtenhaArquivo();

            _variacao = FabricaObjetos.ObtenhaVariacao();
            _variacao.AddCor(_cor);

            Session.Current.Flush();
        }

        public override void Cleanup()
        {
            FabricaObjetosPersistidos.ExcluaGrade(_grade);
            FabricaObjetosPersistidos.ExcluaClassificacao(_classificacao);
            FabricaObjetosPersistidos.ExcluaColecao(_colecao);
            FabricaObjetosPersistidos.ExcluaMarca(_marca);
            FabricaObjetosPersistidos.ExcluaArtigo(_artigo);
            FabricaObjetosPersistidos.ExcluaPessoa(_funcionario);
            FabricaObjetosPersistidos.ExcluaDepartamentoProducao(_departamentoProducao);
            FabricaObjetosPersistidos.ExcluaSetorProducao(_setorProducao);
            FabricaObjetosPersistidos.ExcluaCor(_cor);
            FabricaObjetosPersistidos.ExcluaMaterial(_material);
            FabricaObjetosPersistidos.ExcluaUnidadeMedida(_unidadeMedida);

            Session.Current.Flush();
        }
    }
}