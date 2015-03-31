using System;
using Fashion.ERP.Domain.Almoxarifado;

namespace Fashion.ERP.Domain.Producao
{
    public class MaterialComposicaoCustoMatriz : DomainEmpresaBase<MaterialComposicaoCustoMatriz>
    {
        public virtual Double Custo { get; set; }
        public virtual Material Material { get; set; }
    }
}