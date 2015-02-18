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
            Map(x => x.Cos).Length(100).Nullable();
            Map(x => x.Passante).Length(100).Nullable();
            Map(x => x.Entrepernas).Length(100).Nullable();
            Map(x => x.Boca).Length(100).Nullable();

            References(x => x.ProdutoBase);
            References(x => x.Barra);
            References(x => x.Comprimento);
        }
    }
}