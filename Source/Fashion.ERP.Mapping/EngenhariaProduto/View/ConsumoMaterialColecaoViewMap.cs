using Fashion.ERP.Domain.EngenhariaProduto.Views;
using FluentNHibernate.Mapping;

namespace Fashion.ERP.Mapping.EngenhariaProduto.View
{
    public class ConsumoMaterialColecaoViewMap : ClassMap<ConsumoMaterialColecaoView>
    {
        public ConsumoMaterialColecaoViewMap()
        {
            ReadOnly();
            Id(x => x.Id);
            Map(x => x.Referencia);
            Map(x => x.Descricao);
            Map(x => x.Unidade);
            Map(x => x.Quantidade);
            Map(x => x.Compras);
            Map(x => x.Estoque);
            Map(x => x.Diferenca);
            Map(x => x.Colecao);
            Map(x => x.ColecaoAprovada);
            Map(x => x.Categoria);
            Map(x => x.Subcategoria);
            Map(x => x.Familia);
            Map(x => x.DataPrevisaoEnvio);
        }
    }
}