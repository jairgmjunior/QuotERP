using Fashion.ERP.Domain.Almoxarifado.Views;
using FluentNHibernate.Mapping;

namespace Fashion.ERP.Mapping.Almoxarifado.View
{
    public class ExtratoItemEstoqueViewMap : ClassMap<ExtratoItemEstoqueView>
    {
        public ExtratoItemEstoqueViewMap()
        {
            ReadOnly();
            Id(x => x.Id);
            Map(x => x.Material);
            Map(x => x.DepositoMaterial);
            Map(x => x.Data);
            Map(x => x.TipoMovimentacao);
            Map(x => x.QtdEntrada);
            Map(x => x.QtdSaida);
        }
    }
}