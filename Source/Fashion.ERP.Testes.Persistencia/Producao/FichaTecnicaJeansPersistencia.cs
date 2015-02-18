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
        private Cor _cor;
        private ClassificacaoDificuldade _classificacaoDificuldade;
        private Segmento _segmento;
        private Barra _barra;
        private ProdutoBase _produtoBase;
        private Comprimento _comprimento;

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
            
            _fichaTecnicaVariacaoMatriz = FabricaObjetos.ObtenhaFichaTecnicaVariacaoMatriz();
            _fichaTecnicaVariacaoMatriz.Variacao = _variacao;
            _fichaTecnicaVariacaoMatriz.AddCor(_cor);
            _fichaTecnicaMatriz = FabricaObjetos.ObtenhaFichaTecnicaMatriz();
            _fichaTecnicaMatriz.Grade = _grade;
            _fichaTecnicaMatriz.FichaTecnicaVariacaoMatrizs.Add(_fichaTecnicaVariacaoMatriz);

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
            
            Session.Current.Flush();
        }
    }
}