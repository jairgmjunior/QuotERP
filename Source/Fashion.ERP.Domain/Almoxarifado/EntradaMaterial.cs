using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Fashion.ERP.Domain.Comum;
using Fashion.Framework.Common.Base;
using Fashion.Framework.Repository;

namespace Fashion.ERP.Domain.Almoxarifado
{
    public class EntradaMaterial : DomainBase<EntradaMaterial>, IPesquisavelPorData
    {
        private readonly IList<EntradaItemMaterial> _entradaItemMateriais;

        public EntradaMaterial()
        {
            _entradaItemMateriais = new List<EntradaItemMaterial>();
        }

        public virtual DateTime DataEntrada { get; set; }
        public virtual DateTime DataAlteracao { get; set; }
        public virtual DepositoMaterial DepositoMaterialOrigem { get; set; }
        public virtual DepositoMaterial DepositoMaterialDestino { get; set; }
        public virtual Pessoa Fornecedor { get; set; }
        public virtual String Observacao { get; set; }

        #region EntradaItemMateriais
        public virtual IReadOnlyCollection<EntradaItemMaterial> EntradaItemMateriais
        {
            get { return new ReadOnlyCollection<EntradaItemMaterial>(_entradaItemMateriais); }
        }

        public virtual void AddEntradaItemMaterial(params EntradaItemMaterial[] entradaItemMateriais)
        {
            foreach (var entradaItemMaterial in entradaItemMateriais)
            {
                entradaItemMaterial.EntradaMaterial = this;
                _entradaItemMateriais.Add(entradaItemMaterial);
            }
        }

        public virtual void RemoveEntradaItemMaterial(params EntradaItemMaterial[] entradaItemMateriais)
        {
            foreach (var entradaItemMaterial in entradaItemMateriais)
            {
                if (_entradaItemMateriais.Contains(entradaItemMaterial))
                    _entradaItemMateriais.Remove(entradaItemMaterial);
            }
        }

        #endregion

        public virtual void AtualizeEntradaItemMaterial(IRepository<EstoqueMaterial> estoqueMaterialRepository,
            IRepository<MovimentacaoEstoqueMaterial> movimentacaoEstoqueMaterialRepository,
            DepositoMaterial depositoMaterial, Material material, double quantidadeCompra, UnidadeMedida unidadeMedida, double quantidadeEntrada)
        {
            var entradaItemMaterial =
                EntradaItemMateriais.FirstOrDefault(q => q.Material.Id == material.Id);

            if (entradaItemMaterial == null)
            {
                entradaItemMaterial = new EntradaItemMaterial
                {
                    Material = material,
                    QuantidadeCompra = quantidadeCompra,
                    UnidadeMedidaCompra = unidadeMedida
                };
                AddEntradaItemMaterial(entradaItemMaterial);
            }

            var diferencaQuantidade = quantidadeEntrada;

            if (entradaItemMaterial.MovimentacaoEstoqueMaterial != null)
            {
                diferencaQuantidade = ObtenhaDiferencaQuantidadeEstoque(entradaItemMaterial, quantidadeEntrada);

                movimentacaoEstoqueMaterialRepository.Delete(entradaItemMaterial.MovimentacaoEstoqueMaterial);
            }

            entradaItemMaterial.MovimentacaoEstoqueMaterial = new MovimentacaoEstoqueMaterial
            {
                Data = DateTime.Now,
                Quantidade = quantidadeEntrada,
                EstoqueMaterial =
                    EstoqueMaterial.AtualizarEstoque(estoqueMaterialRepository, depositoMaterial, material, diferencaQuantidade)
            };
        }

        protected virtual double ObtenhaDiferencaQuantidadeEstoque(EntradaItemMaterial entradaItemMaterial, double quantidade)
        {
            var quantidadeMovimentacaoAtual = entradaItemMaterial.MovimentacaoEstoqueMaterial.Quantidade;
            return quantidade - quantidadeMovimentacaoAtual;
        }

        public virtual string ObtenhaOrigem()
        {
            if (Fornecedor != null)
                return Fornecedor.Nome;

            return DepositoMaterialDestino.Nome;
        }
    }
}