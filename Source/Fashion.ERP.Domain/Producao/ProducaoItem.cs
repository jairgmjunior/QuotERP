using System.Collections.Generic;

namespace Fashion.ERP.Domain.Producao
{
    public class ProducaoItem : DomainEmpresaBase<ProducaoItem>
    {
        private IList<ProducaoItemMaterial> _producaoItemMateriais = new List<ProducaoItemMaterial>();

        public virtual long QuantidadeProgramada { get; set; }
        public virtual long QuantidadeProducao { get; set; }
        public virtual FichaTecnica FichaTecnica { get; set; }
        public virtual ProducaoMatrizCorte ProducaoMatrizCorte { get; set; }
        
        public virtual IList<ProducaoItemMaterial> ProducaoItemMateriais
        {
            get { return _producaoItemMateriais; }
            set { _producaoItemMateriais = value; }
        }
    }
}