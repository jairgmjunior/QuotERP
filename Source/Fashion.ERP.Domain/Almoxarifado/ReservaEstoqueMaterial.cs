using System;
using Fashion.ERP.Domain.Comum;

namespace Fashion.ERP.Domain.Almoxarifado
{
    public class ReservaEstoqueMaterial : DomainEmpresaBase<ReservaEstoqueMaterial>
    {
        public virtual Material Material { get; set; }
        public virtual Pessoa Unidade { get; set; }
        public virtual Double Quantidade { get; set; }

        public virtual void AtualizeQuantidade(double valorAdicional)
        {
            Quantidade += valorAdicional;
        }
    }
}