using Fashion.ERP.Domain.Almoxarifado;
using Fashion.ERP.Domain.Comum;

namespace Fashion.ERP.Domain.EngenhariaProduto
{
    public class ModeloMaterialConsumo : DomainEmpresaBase<ModeloMaterialConsumo>
    {
        public virtual double Quantidade { get; set; }
        public virtual UnidadeMedida UnidadeMedida { get; set; }
        public virtual Material Material { get; set; }
        public virtual DepartamentoProducao DepartamentoProducao { get; set; }
    }
}