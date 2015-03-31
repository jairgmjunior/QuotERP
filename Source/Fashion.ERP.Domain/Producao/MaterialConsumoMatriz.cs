using System;
using Fashion.ERP.Domain.Almoxarifado;
using Fashion.ERP.Domain.Comum;

namespace Fashion.ERP.Domain.Producao
{
    public class MaterialConsumoMatriz : DomainEmpresaBase<MaterialConsumoMatriz>
    {
        public virtual Double Custo { get; set; }
        public virtual Double Quantidade { get; set; }
        public virtual Material Material { get; set; }
        public virtual DepartamentoProducao DepartamentoProducao { get; set; }
    }
}