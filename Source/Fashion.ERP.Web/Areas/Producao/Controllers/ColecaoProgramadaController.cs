using Fashion.ERP.Domain.Almoxarifado;
using Fashion.ERP.Domain.Compras;
using Fashion.ERP.Domain.Comum;
using Fashion.ERP.Domain.Producao;
using Fashion.ERP.Web.Controllers;
using Fashion.Framework.Repository;
using Ninject.Extensions.Logging;

namespace Fashion.ERP.Web.Areas.Producao.Controllers
{
    public partial class ColecaoProgramadaController : BaseController
    {
        private readonly ILogger _logger;
        private readonly IRepository<ProgramacaoProducao> _programacaoProducaoRepository;
        private readonly IRepository<MarcaMaterial> _marcaMaterialRepository;
        private readonly IRepository<Colecao> _colecaoRepository;
        private readonly IRepository<Categoria> _categoriaRepository;
        private readonly IRepository<Subcategoria> _subcategoriaRepository;
        private readonly IRepository<EstoqueMaterial> _estoqueMaterialRepository;
        private readonly IRepository<ReservaEstoqueMaterial> _reservaEstoqueMaterialRepository;
        private readonly IRepository<PedidoCompraItem> _pedidoCompraItemRepository;
        private readonly IRepository<Material> _materialRepository;
        
        #region Construtores
        public ColecaoProgramadaController(ILogger logger, 
            IRepository<MarcaMaterial> marcaMaterialRepository, IRepository<Colecao> colecaoRepository, 
            IRepository<Categoria> categoriaRepository, IRepository<Subcategoria> subcategoriaRepository,
            IRepository<EstoqueMaterial> estoqueMaterialRepository, IRepository<ReservaEstoqueMaterial> reservaMaterialRepository,
            IRepository<PedidoCompraItem> pedidoCompraItemRepository, IRepository<ProgramacaoProducao> programacaoProducaoRepository,
            IRepository<Material> materialRepository)
        {
            _logger = logger;
            _colecaoRepository = colecaoRepository;
            _categoriaRepository = categoriaRepository;
            _subcategoriaRepository = subcategoriaRepository;
            _marcaMaterialRepository = marcaMaterialRepository;
            _estoqueMaterialRepository = estoqueMaterialRepository;
            _reservaEstoqueMaterialRepository = reservaMaterialRepository;
            _pedidoCompraItemRepository = pedidoCompraItemRepository;
            _programacaoProducaoRepository = programacaoProducaoRepository;
            _materialRepository = materialRepository;
        }
        #endregion

    }
}