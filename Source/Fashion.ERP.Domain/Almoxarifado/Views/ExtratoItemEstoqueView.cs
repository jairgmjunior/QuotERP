using System;

namespace Fashion.ERP.Domain.Almoxarifado.Views
{
    public class ExtratoItemEstoqueView : DomainBase<ExtratoItemEstoqueView>
    {
        public virtual DateTime Data { get; set; }
        public virtual string TipoMovimentacao { get; set; }
        public virtual long Material { get; set; }
        public virtual long DepositoMaterial { get; set; }
        public virtual double QtdEntrada { get; set; }
        public virtual double QtdSaida { get; set; }
    }
}