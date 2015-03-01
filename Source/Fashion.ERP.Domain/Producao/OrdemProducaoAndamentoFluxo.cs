using System;
using Fashion.ERP.Domain.Comum;

namespace Fashion.ERP.Domain.Producao
{
    public class OrdemProducaoAndamentoFluxo : DomainEmpresaBase<OrdemProducaoAndamentoFluxo>
    {
        public virtual DateTime Data { get; set; }
        public virtual TipoAndamento TipoAndamento { get; set; }
        public virtual double Quantidade { get; set; }

        public virtual OrdemProducao OrdemProducao { get; set; }
        public virtual DepartamentoProducao DepartamentoProducao { get; set; }
    }
}