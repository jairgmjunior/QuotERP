using System.Collections.Generic;
using System.Collections.ObjectModel;
using Fashion.ERP.Domain.Almoxarifado;
using Fashion.ERP.Domain.Comum;

namespace Fashion.ERP.Domain.Producao
{
    public class OrdemProducaoMaterial : DomainBase<OrdemProducaoMaterial>
    {
        private readonly IList<OrdemProducaoMaterial> _ordemProducaoMateriais;

        public OrdemProducaoMaterial()
        {
            _ordemProducaoMateriais = new List<OrdemProducaoMaterial>();            
        }

        public virtual double Quantidade { get; set; }
        public virtual double QuantidadeSubstituida { get; set; }
        public virtual bool Requisitado { get; set; }
        public virtual Material Material { get; set; }
        public virtual OrdemProducaoMaterial OrdemProducaoMaterialPai { get; set; }
        public virtual DepartamentoProducao DepartamentoProducao { get; set; }
        public virtual OrdemProducao OrdemProducao { get; set; }

        #region OrdemProducaoMateriais

        public virtual IReadOnlyCollection<OrdemProducaoMaterial> OrdemProducaoMateriais
        {
            get { return new ReadOnlyCollection<OrdemProducaoMaterial>(_ordemProducaoMateriais); }
        }

        public virtual void AddOrdemProducaoMaterial(params OrdemProducaoMaterial[] ordemProducaoMateriais)
        {
            foreach (var ordemProducaoMaterial in ordemProducaoMateriais)
                if (!_ordemProducaoMateriais.Contains(ordemProducaoMaterial))
                {
                    ordemProducaoMaterial.OrdemProducaoMaterialPai = this;
                    _ordemProducaoMateriais.Add(ordemProducaoMaterial);
                }
        }

        public virtual void RemoveOrdemProducaoMaterial(params OrdemProducaoMaterial[] ordemProducaoMateriais)
        {
            foreach (var ordemProducaoMaterial in ordemProducaoMateriais)
                if (_ordemProducaoMateriais.Contains(ordemProducaoMaterial))
                    _ordemProducaoMateriais.Remove(ordemProducaoMaterial);
        }

        #endregion
    }
}