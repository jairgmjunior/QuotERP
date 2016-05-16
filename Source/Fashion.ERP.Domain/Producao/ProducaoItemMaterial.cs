using System.Collections.Generic;
using Fashion.ERP.Domain.Almoxarifado;
using Fashion.ERP.Domain.Comum;

namespace Fashion.ERP.Domain.Producao
{
    public class ProducaoItemMaterial : DomainEmpresaBase<ProducaoItemMaterial>
    {
        private IList<ProducaoItemMaterial> _producaoItensMateriais = new List<ProducaoItemMaterial>();

        public virtual double QuantidadeProgramada { get; set; }
        public virtual double QuantidadeNecessidade { get; set; } // deve existir?
        public virtual double QuantidadeCancelada { get; set; }
        public virtual double QuantidadeUsada { get; set; }
        public virtual Material Material { get; set; }
        public virtual Pessoa Responsavel { get; set; }
        public virtual DepartamentoProducao DepartamentoProducao { get; set; }
        public virtual SituacaoItemProducaoMaterial SituacaoProducaoMaterial { get; set; }
        
        public virtual IList<ProducaoItemMaterial> ProducaoItemMateriais
        {
            get { return _producaoItensMateriais; }
            set { _producaoItensMateriais = value; }
        }
    }
}