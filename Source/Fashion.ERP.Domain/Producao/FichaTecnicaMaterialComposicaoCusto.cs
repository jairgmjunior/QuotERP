using System;
using Fashion.ERP.Domain.Almoxarifado;

namespace Fashion.ERP.Domain.Producao
{
    public class FichaTecnicaMaterialComposicaoCusto : DomainEmpresaBase<FichaTecnicaMaterialComposicaoCusto>
    {
        public virtual Double Custo { get; set; }
        public virtual Material Material { get; set; }
    }
}