using System;
using System.Collections.Generic;
using System.Linq;
using Fashion.ERP.Domain.Almoxarifado;
using Fashion.ERP.Domain.Comum;
using Fashion.Framework.Common.Extensions;
using Fashion.Framework.Repository;
using FluentNHibernate.Conventions;

namespace Fashion.ERP.Domain.Compras
{
    public class RecebimentoCompra : DomainBase<RecebimentoCompra>
    {
        private IList<ConferenciaEntradaMaterial> _conferenciaEntradaMateriais = new List<ConferenciaEntradaMaterial>();
        private IList<PedidoCompra> _pedidosCompras = new List<PedidoCompra>();
        private IList<RecebimentoCompraItem> _recebimentoCompraItens = new List<RecebimentoCompraItem>();
        private IList<DetalhamentoRecebimentoCompraItem> _detalhamentoRecebimentoCompraItens = new List<DetalhamentoRecebimentoCompraItem>();

        public virtual long Numero { get; set; }
        public virtual SituacaoRecebimentoCompra SituacaoRecebimentoCompra { get; set; }
        public virtual DateTime Data { get; set; }
        public virtual DateTime DataAlteracao { get; set; }
        public virtual string Observacao { get; set; }
        public virtual double Valor { get; set; }
        public virtual Pessoa Unidade { get; set; }
        public virtual Pessoa Fornecedor { get; set; }
        public virtual EntradaMaterial EntradaMaterial { get; set; }

        public virtual IList<ConferenciaEntradaMaterial> ConferenciaEntradaMateriais
        {
            get { return _conferenciaEntradaMateriais; }
            set { _conferenciaEntradaMateriais = value; }
        }

        public virtual IList<PedidoCompra> PedidoCompras
        {
            get { return _pedidosCompras; }
            set { _pedidosCompras = value; }
        }

        public virtual IList<RecebimentoCompraItem> RecebimentoCompraItens
        {
            get { return _recebimentoCompraItens; }
            set { _recebimentoCompraItens = value; }
        }

        public virtual IList<DetalhamentoRecebimentoCompraItem> DetalhamentoRecebimentoCompraItens
        {
            get { return _detalhamentoRecebimentoCompraItens; }
            set { _detalhamentoRecebimentoCompraItens = value; }
        }

        public virtual void AtualizeCustoMaterial(IRepository<Material> materialRepository)
        {
            foreach (var x in RecebimentoCompraItens)
            {
                var valorUnitario = x.ValorUnitario;
                var custoAtual = x.CustoMaterial;

                custoAtual.Custo = valorUnitario;
                custoAtual.CustoAquisicao = valorUnitario;
                custoAtual.CustoMedio = valorUnitario;

                x.Material.AtualizeCustoAtual();
                materialRepository.SaveOrUpdate(x.Material);
            }
        }

        public virtual EntradaMaterial SalveEntradaMaterial(long? depositoId,
            IRepository<DepositoMaterial> depositoMaterialRepository,
            IRepository<MovimentacaoEstoqueMaterial> movimentacaoEstoqueMaterialRepository,
            IRepository<EstoqueMaterial> estoqueMaterialRepository,
            IRepository<EntradaMaterial> entradaMaterialRepository)
        {
            var depositoMaterial = depositoMaterialRepository.Get(depositoId);

            var entradaMaterial = new EntradaMaterial
            {
                DataEntrada = DateTime.Now,
                Fornecedor = Fornecedor,
                DepositoMaterialDestino = depositoMaterial
            };

            RecebimentoCompraItens.Each(x =>
            {
                var entradaItemMaterial =  x.ObtenhaEntradaItemMaterial(estoqueMaterialRepository, depositoMaterial);
                entradaMaterial.AddEntradaItemMaterial(entradaItemMaterial);
            });

            return entradaMaterialRepository.SaveOrUpdate(entradaMaterial);
        }

        public virtual EntradaMaterial AtualizeEntradaMaterial(long? depositoId,
            IRepository<DepositoMaterial> depositoMaterialRepository,
            IRepository<MovimentacaoEstoqueMaterial> movimentacaoEstoqueMaterialRepository,
            IRepository<EstoqueMaterial> estoqueMaterialRepository,
            IRepository<EntradaMaterial> entradaMaterialRepository)
        {
            var depositoMaterial = depositoMaterialRepository.Get(depositoId);

            var entradaMaterial = EntradaMaterial;

            entradaMaterial.DepositoMaterialDestino = depositoMaterial;

            RecebimentoCompraItens.Each(x => 
                x.AtualizeEntradaItemMaterial(entradaMaterial, estoqueMaterialRepository, 
                    movimentacaoEstoqueMaterialRepository, depositoMaterial));

            return entradaMaterialRepository.SaveOrUpdate(entradaMaterial);
        }

        protected virtual double ObtenhaDiferencaQuantidadeEstoque(EntradaItemMaterial entradaItemMaterial, double quantidade)
        {
            var quantidadeMovimentacaoAtual = entradaItemMaterial.MovimentacaoEstoqueMaterial.Quantidade;
            return quantidade - quantidadeMovimentacaoAtual;
        }

        public virtual void ExcluaCustos(IRepository<Material> materialRepository)
        {
            foreach (var recebimentoCompraItem in RecebimentoCompraItens)
            {
                recebimentoCompraItem.Material.ExcluaCusto(recebimentoCompraItem.CustoMaterial, materialRepository);
            }
        }

        public virtual void ExcluaEntradaMaterial(IRepository<EstoqueMaterial> estoqueMaterialRepository,
            IRepository<EntradaMaterial> entradaMaterialRepository)
        {
            foreach (var entradaItemMaterial in EntradaMaterial.EntradaItemMateriais)
			{
                EstoqueMaterial.AtualizarEstoque(estoqueMaterialRepository,
                    EntradaMaterial.DepositoMaterialDestino, entradaItemMaterial.Material, entradaItemMaterial.MovimentacaoEstoqueMaterial.Quantidade * -1);
			}

			if (EntradaMaterial != null)
			{
				entradaMaterialRepository.Delete(EntradaMaterial);
			}
        }

        public virtual void AtualizePedidosCompra(IRepository<RecebimentoCompra> recebimentoCompraRepository,
            IRepository<PedidoCompra> pedidoCompraRepository)
        {
            foreach (var pedidoCompra in PedidoCompras)
            {
                var recebimentos = recebimentoCompraRepository.Find().Where(p => p.PedidoCompras.Any(i => i.Id == pedidoCompra.Id)).ToList();

                foreach (var pedidoCompraItem in pedidoCompra.PedidoCompraItens)
                {
                    var detalhamentos = recebimentos.SelectMany(r => r.DetalhamentoRecebimentoCompraItens);
                    var detalhamentosDoPedidoCompraItem = detalhamentos.Where(d => d.PedidoCompraItem.Id == pedidoCompraItem.Id).ToList();

                    double quantidadeEntregue = 0;

                    if (!detalhamentosDoPedidoCompraItem.IsEmpty())
                        quantidadeEntregue = detalhamentosDoPedidoCompraItem.Select(p => p.Quantidade)
                            .Aggregate((quantidadeTotal, quantidade) => quantidadeTotal + quantidade);

                    pedidoCompraItem.QuantidadeEntrega = pedidoCompraItem.UnidadeMedida.ConvertaQuantidadeParaEntrada(quantidadeEntregue);
                    pedidoCompraItem.AtualizeSituacao();
                }

                pedidoCompra.AtualizeSituacao();
                pedidoCompraRepository.Update(pedidoCompra);
            }
        }

        public virtual void AtualizePedidosCompraAoExcluir(IRepository<RecebimentoCompra> recebimentoCompraRepository,
            IRepository<PedidoCompra> pedidoCompraRepository)
        {
            foreach (var pedidoCompra in PedidoCompras)
            {
                var recebimentos = recebimentoCompraRepository.Find().Where(p => p.PedidoCompras.Any(i => i.Id == pedidoCompra.Id)
                    && p.Id != Id).ToList();

                foreach (var pedidoCompraItem in pedidoCompra.PedidoCompraItens)
                {
                    var detalhamentos = recebimentos.SelectMany(r => r.DetalhamentoRecebimentoCompraItens);
                    var detalhamentosDoPedidoCompraItem = detalhamentos.Where(d => d.PedidoCompraItem.Id == pedidoCompraItem.Id).ToList();

                    double quantidadeEntregue = 0;

                    if (!detalhamentosDoPedidoCompraItem.IsEmpty())
                        quantidadeEntregue = detalhamentosDoPedidoCompraItem.Select(p => p.Quantidade)
                            .Aggregate((quantidadeTotal, quantidade) => quantidadeTotal + quantidade);

                    pedidoCompraItem.QuantidadeEntrega = pedidoCompraItem.UnidadeMedida.ConvertaQuantidadeParaEntrada(quantidadeEntregue);
                    pedidoCompraItem.AtualizeSituacao();
                }

                pedidoCompra.AtualizeSituacao();
                pedidoCompraRepository.Update(pedidoCompra);
            }
        }
    }
}