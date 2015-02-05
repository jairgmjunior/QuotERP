using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Fashion.ERP.Domain.Comum;
using Fashion.Framework.Common.Base;
using Fashion.Framework.Repository;

namespace Fashion.ERP.Domain.Almoxarifado
{
    public class SaidaMaterial : DomainBase<SaidaMaterial>, IPesquisavelPorData
    {
        private readonly IList<SaidaItemMaterial> _saidaItemMateriais;

        public SaidaMaterial()
        {
            _saidaItemMateriais = new List<SaidaItemMaterial>();
        }

        public virtual DateTime DataSaida { get; set; }
        public virtual DateTime DataAlteracao { get; set; }
        public virtual DepositoMaterial DepositoMaterialOrigem { get; set; }
        public virtual DepositoMaterial DepositoMaterialDestino { get; set; }
        public virtual CentroCusto CentroCusto { get; set; }

        #region SaidaItemMateriais
        public virtual IReadOnlyCollection<SaidaItemMaterial> SaidaItemMateriais
        {
            get { return new ReadOnlyCollection<SaidaItemMaterial>(_saidaItemMateriais); }
        }

        public virtual void AddSaidaItemMaterial(params SaidaItemMaterial[] saidaItemMateriais)
        {
            foreach (var saidaItemMaterial in saidaItemMateriais)
            {
                saidaItemMaterial.SaidaMaterial = this;
                _saidaItemMateriais.Add(saidaItemMaterial);
            }
        }

        public virtual void RemoveSaidaItemMaterial(params SaidaItemMaterial[] saidaItemMateriais)
        {
            foreach (var saidaItemMaterial in saidaItemMateriais)
            {
                if (_saidaItemMateriais.Contains(saidaItemMaterial))
                    _saidaItemMateriais.Remove(saidaItemMaterial);
            }
        }

        #endregion

        public virtual void CrieSaidaItemMaterial(IRepository<EstoqueMaterial> estoqueMaterialRepository,
            Material material, double quantidade)
        {
            var estoqueMaterial = EstoqueMaterial.AtualizarEstoque(estoqueMaterialRepository,
                                DepositoMaterialOrigem, material, quantidade * -1);

            var saidaItemMaterial = new SaidaItemMaterial
            {
                Material = material,
                MovimentacaoEstoqueMaterial = new MovimentacaoEstoqueMaterial
                {
                    Data = DateTime.Now,
                    EstoqueMaterial = estoqueMaterial,
                    Quantidade = quantidade,
                    TipoMovimentacaoEstoqueMaterial = TipoMovimentacaoEstoqueMaterial.Saida
                }
            };

            AddSaidaItemMaterial(saidaItemMaterial);
        }
    }
}