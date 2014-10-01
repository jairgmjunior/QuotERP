using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Fashion.ERP.Domain.Comum;

namespace Fashion.ERP.Domain.Almoxarifado
{
    public class EntradaMaterial : DomainBase<EntradaMaterial>
    {
        private readonly IList<EntradaItemMaterial> _entradaItemMateriais;

        public EntradaMaterial()
        {
            _entradaItemMateriais = new List<EntradaItemMaterial>();
        }

        public virtual DateTime DataEntrada { get; set; }

        public virtual DepositoMaterial DepositoMaterialOrigem { get; set; }
        public virtual DepositoMaterial DepositoMaterialDestino { get; set; }
        public virtual Pessoa Fornecedor { get; set; }

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
    }
}