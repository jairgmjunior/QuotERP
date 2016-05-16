using Fashion.ERP.Domain.Almoxarifado;
using Fashion.ERP.Domain.Comum;
using Fashion.ERP.Domain.Producao;
using Fashion.Framework.UnitOfWork;
using NUnit.Framework;

namespace Fashion.ERP.Testes.Persistencia.Producao
{
    [TestFixture]
    public class ProducaoPersistencia : TestPersistentObject<Domain.Producao.Producao>
    {
        private Pessoa _funcionario;
        private RemessaProducao _remessaProducao;
        private FichaTecnicaJeans _fichaTecnica;
        private Tamanho _tamanho;
        private Material _material;
        private DepartamentoProducao _departamentoProducao;
        private ProducaoMatrizCorte _producaoMatrizCorte;
        private ProducaoMatrizCorteItem _producaoMatrizCorteItem;
        private ProducaoItemMaterial _producaoItemMaterial;
        private ProducaoItemMaterial _producaoItemMaterial2;
        private ProducaoItem _producaoItem;

        public override Domain.Producao.Producao GetPersistentObject()
        {
            var producao = FabricaObjetos.ObtenhaProducao();
            
            producao.RemessaProducao = _remessaProducao;
            producao.ProducaoItens.Add(_producaoItem);

            return producao;
        }

        public override void Init()
        {
            _funcionario = FabricaObjetosPersistidos.ObtenhaFuncionario();
            _remessaProducao = FabricaObjetosPersistidos.ObtenhaRemessaProducao();
            _tamanho = FabricaObjetosPersistidos.ObtenhaTamanho();
            _fichaTecnica = FabricaObjetosPersistidos.ObtenhaFichaTecnica();
            _material = FabricaObjetosPersistidos.ObtenhaMaterial();
            _departamentoProducao = FabricaObjetosPersistidos.ObtenhaDepartamentoProducao();
            
            _producaoMatrizCorte = FabricaObjetos.ObtenhaProducaoMatrizCorte();
            _producaoMatrizCorteItem = FabricaObjetos.ObtenhaProducaoMatrizCorteItem();
            _producaoMatrizCorteItem.Tamanho = _tamanho;
            _producaoMatrizCorte.ProducaoMatrizCorteItens.Add(_producaoMatrizCorteItem);

            _producaoItemMaterial2 = FabricaObjetos.ObtenhaProducaoItemMaterial();
            _producaoItemMaterial2.Material = _material;
            _producaoItemMaterial2.DepartamentoProducao = _departamentoProducao;
            _producaoItemMaterial2.Responsavel = _funcionario;

            _producaoItemMaterial = FabricaObjetos.ObtenhaProducaoItemMaterial();
            _producaoItemMaterial.Material = _material;
            _producaoItemMaterial.DepartamentoProducao = _departamentoProducao;
            _producaoItemMaterial.Responsavel = _funcionario;
            _producaoItemMaterial.ProducaoItemMateriais.Add(_producaoItemMaterial2);
            
            _producaoItem = FabricaObjetos.ObtenhaProducaoItem();
            _producaoItem.FichaTecnica = _fichaTecnica;
            _producaoItem.ProducaoMatrizCorte = _producaoMatrizCorte;
            _producaoItem.ProducaoItemMateriais.Add(_producaoItemMaterial);
            _producaoItem.ProducaoItemMateriais.Add(_producaoItemMaterial2);

            Session.Current.Flush();
        }

        public override void Cleanup()
        {
            FabricaObjetosPersistidos.ExcluaFichaTecnicaJeans(_fichaTecnica);
            FabricaObjetosPersistidos.ExcluaRemessaProducao(_remessaProducao);
            FabricaObjetosPersistidos.ExcluaTamanho(_tamanho);
            FabricaObjetosPersistidos.ExcluaPessoa(_funcionario);
            FabricaObjetosPersistidos.ExcluaMaterial(_material);
            FabricaObjetosPersistidos.ExcluaDepartamentoProducao(_departamentoProducao);

            Session.Current.Flush();
        }
    }
}