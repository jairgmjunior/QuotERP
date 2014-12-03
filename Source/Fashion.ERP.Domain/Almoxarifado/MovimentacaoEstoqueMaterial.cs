using System;

namespace Fashion.ERP.Domain.Almoxarifado
{
    public class MovimentacaoEstoqueMaterial : DomainBase<MovimentacaoEstoqueMaterial>
    {
        public virtual double Quantidade { get; set; }
        public virtual DateTime Data { get; set; }
        public virtual TipoMovimentacaoEstoqueMaterial TipoMovimentacaoEstoqueMaterial { get; set; }
        public virtual EstoqueMaterial EstoqueMaterial { get; set; }

    }
}