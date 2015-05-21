using Fashion.ERP.Domain.Producao;
using FluentNHibernate.Mapping;

namespace Fashion.ERP.Mapping.Producao
{
    public class FichaTecnicaJeansMap : SubclassMap<FichaTecnicaJeans>
    {
        public FichaTecnicaJeansMap()
        {
            DiscriminatorValue("FichaTecnicaJeans");  

            Map(x => x.Lavada).Length(200).Nullable();
            Map(x => x.Pesponto).Length(200).Nullable();
            Map(x => x.MedidaCos).Length(100).Nullable();
            Map(x => x.MedidaPassante).Length(100).Nullable();
            Map(x => x.MedidaComprimento).Length(100).Nullable();
            Map(x => x.MedidaBarra).Length(100).Nullable();

            References(x => x.ProdutoBase);
            References(x => x.Barra);
            References(x => x.Comprimento);
        }
    }
}