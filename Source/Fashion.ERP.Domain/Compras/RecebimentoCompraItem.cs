using System;
using System.Collections.Generic;
using System.Linq;
using Fashion.ERP.Domain.Almoxarifado;
using Fashion.Framework.Repository;

namespace Fashion.ERP.Domain.Compras
{
    public class RecebimentoCompraItem : DomainBase<RecebimentoCompraItem>
    {
        private IList<ConferenciaEntradaMaterialItem> _conferenciaEntradaMaterialItens = new List<ConferenciaEntradaMaterialItem>();
        private IList<DetalhamentoRecebimentoCompraItem>  _detalhamentoRecebimentoCompraItens = new List<DetalhamentoRecebimentoCompraItem>();

        public virtual double Quantidade { get; set; }
        public virtual double ValorUnitario { get; set; }
        public virtual double ValorTotal { get; set; }
        public virtual Material Material { get; set; }
        public virtual CustoMaterial CustoMaterial { get; set; }

        public virtual IList<ConferenciaEntradaMaterialItem> ConferenciaEntradaMaterialItens
        {
            get { return _conferenciaEntradaMaterialItens; }
            set { _conferenciaEntradaMaterialItens = value; }
        }

        public virtual IList<DetalhamentoRecebimentoCompraItem> DetalhamentoRecebimentoCompraItens
        {
            get { return _detalhamentoRecebimentoCompraItens; }
            set { _detalhamentoRecebimentoCompraItens = value; }
        }

        public virtual EntradaItemMaterial ObtenhaEntradaItemMaterial(IRepository<EstoqueMaterial> estoqueMaterialRepository,
            DepositoMaterial depositoMaterial)
        {
            var unidadeMedidaCompra = DetalhamentoRecebimentoCompraItens.First().PedidoCompraItem.UnidadeMedida;
            var quantidadeCompra = DetalhamentoRecebimentoCompraItens.Sum(y => y.Quantidade);
                
            var diferencaQuantidade = Quantidade;
            var entradaItemMaterial = new EntradaItemMaterial
            {
                UnidadeMedidaCompra = unidadeMedidaCompra,
                Material = Material,
                QuantidadeCompra = quantidadeCompra,
                MovimentacaoEstoqueMaterial = new MovimentacaoEstoqueMaterial
                {
                    Data = DateTime.Now,
                    Quantidade = Quantidade,
                    EstoqueMaterial =
                        EstoqueMaterial.AtualizarEstoque(estoqueMaterialRepository, depositoMaterial, Material,
                            diferencaQuantidade)
                }
            };

            return entradaItemMaterial;
        }

        public virtual DetalhamentoRecebimentoCompraItem ObtenhaDetalhamentoRecebimentoCompraItem(
            long? pedidoCompraId)
        {
            return DetalhamentoRecebimentoCompraItens.SingleOrDefault(
                d => d.PedidoCompra.Id == pedidoCompraId);
        }

        public virtual void ExcluaEntradaItemMaterial(EntradaMaterial entradaMaterial, IRepository<EntradaMaterial> entradaMaterialRepository)
        {
            var entradaItemMaterial = entradaMaterial.EntradaItemMateriais.FirstOrDefault(x => x.Material.Id == Material.Id);
            entradaMaterial.RemoveEntradaItemMaterial(entradaItemMaterial);
            entradaMaterialRepository.SaveOrUpdate(entradaMaterial);
        }
    }
}